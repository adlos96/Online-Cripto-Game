#!/bin/bash
# setup-server.sh
# Script di inizializzazione per server Ubuntu 24 fresh
# Da eseguire UNA VOLTA su un server appena noleggiato.
# ── Configurazione ──────────────────────────────────────────
DOTNET_VERSION="8.0"
# ────────────────────────────────────────────────────────────

fail() { echo "[ERRORE] $1"; exit 1; }

echo "============================================"
echo " Warrior & Wealth - Setup Server Iniziale"
echo "============================================"
echo ""

# 1. Aggiornamento sistema
echo "[APT] Aggiornamento pacchetti..."
apt-get update -y || fail "apt update fallito"
apt-get upgrade -y || fail "apt upgrade fallito"
echo "[APT] Sistema aggiornato."

# 2. Dipendenze base
echo "[APT] Installazione dipendenze base..."
apt-get install -y \
    git \
    screen \
    curl \
    wget \
    unzip \
    htop \
    ufw \
    || fail "Installazione dipendenze fallita"
echo "[APT] Dipendenze installate."

# 3. .NET SDK (per compilare) e Runtime (per eseguire)
echo "[DOTNET] Installazione .NET SDK ${DOTNET_VERSION}..."
apt-get install -y dotnet-sdk-${DOTNET_VERSION} || {
    echo "[DOTNET] Pacchetto apt non trovato, uso script ufficiale Microsoft..."
    curl -fsSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --version latest --channel ${DOTNET_VERSION}
    ln -sf "$HOME/.dotnet/dotnet" /usr/local/bin/dotnet
}
echo "[DOTNET] .NET installato."

# 4. Verifica dotnet
dotnet --version || fail "dotnet non disponibile dopo installazione"

# 5. Firewall base
echo "[UFW] Configurazione firewall..."
ufw allow ssh
ufw allow 8443/tcp    # porta WatsonTcp del server di gioco
ufw --force enable
echo "[UFW] Firewall configurato."

# 6. Verifica finale
echo ""
echo "============================================"
echo " Verifica installazioni"
echo "============================================"
echo -n " git     : "; git --version
echo -n " screen  : "; screen --version | head -1
echo -n " dotnet  : "; dotnet --version
echo -n " ufw     : "; ufw status | head -1
echo ""
echo "[OK] Setup completato. Puoi ora eseguire update-server.sh"