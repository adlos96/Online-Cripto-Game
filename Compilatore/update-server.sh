#!/bin/bash
# update-server.sh
# ── Configurazione ──────────────────────────────────────────
REPO_URL="https://github.com/adlos96/Warrior-and-Wealth.git"
BASE_DIR="$HOME/Warrior-and-Wealth"
SERVER_DIR="$BASE_DIR/Server Strategico"
SCREEN_NAME="warrior_server"
DOTNET_VERSION="net8.0"
DOTNET_SDK="/usr/bin/dotnet"
DOTNET_RUNTIME="/usr/bin/dotnet"
# ────────────────────────────────────────────────────────────
fail() { echo "[ERRORE] $1"; exit 1; }

# 0. Auto-setup se mancano i requisiti
SETUP_SCRIPT="$(dirname "$0")/update-VPS.sh"
if ! command -v git &>/dev/null || ! command -v screen &>/dev/null || ! command -v dotnet &>/dev/null; then
    echo "[SETUP] Dipendenze mancanti. Avvio setup iniziale..."
    if [ -f "$SETUP_SCRIPT" ]; then
        bash "$SETUP_SCRIPT" || fail "Setup iniziale fallito"
    else
        fail "update-VPS.sh non trovato."
    fi
fi

17:34Claude ha risposto: Ecco il blocco 1 aggiornato per update-server.Ecco il blocco 1 aggiornato per update-server.sh:
bash# 1. Clone o aggiornamento repository
if [ ! -d "$BASE_DIR/.git" ]; then
    echo "[GIT] Repository non trovato. Clone in corso (solo Server Strategico)..."
    git clone --no-checkout "$REPO_URL" "$BASE_DIR" || fail "git clone fallito"
    cd "$BASE_DIR" || fail "Directory non trovata: $BASE_DIR"
    git sparse-checkout init --cone
    git sparse-checkout set "Server Strategico"
    git checkout main || fail "git checkout fallito"
    echo "[GIT] Clone completato."
else
    echo "[GIT] Aggiornamento repository..."
    cd "$BASE_DIR" || fail "Directory non trovata: $BASE_DIR"
    git pull origin main || fail "git pull fallito"
    echo "[GIT] Aggiornamento completato."
fi

# 2. Compilazione
echo "[BUILD] Compilazione server in corso..."
cd "$SERVER_DIR" || fail "Directory server non trovata: $SERVER_DIR"
"$DOTNET_SDK" publish -c Release --self-contained false || fail "dotnet publish fallito"
echo "[BUILD] Compilazione completata."

# 3. Chiusura sessione precedente
if screen -list | grep -q "$SCREEN_NAME"; then
    echo "[SCREEN] Chiusura sessione '$SCREEN_NAME' in corso..."
    screen -S "$SCREEN_NAME" -X quit
    sleep 1
fi

# 4. Avvio server
echo "[SCREEN] Avvio server nella sessione '$SCREEN_NAME'..."
screen -dmS "$SCREEN_NAME" bash -c \
    "cd \"$SERVER_DIR\" && \"$DOTNET_RUNTIME\" \"bin/Release/${DOTNET_VERSION}/publish/Server Strategico.dll\"; exec bash"

echo ""
echo "[OK] Server avviato."
echo "     Rientra nella sessione : screen -r $SCREEN_NAME"
echo "     Lista sessioni attive  : screen -ls"