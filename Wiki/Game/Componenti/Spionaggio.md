# Spionaggio

Lo spionaggio è uno strumento fondamentale per conoscere le reali capacità del tuo avversario. Inviando una spia in territorio nemico, puoi ottenere informazioni preziose sulle sue risorse, sulle sue difese e sulla potenza del suo esercito.

Questa guida spiega come funziona lo spionaggio, cosa puoi scoprire e come migliorare le tue capacità di intelligence.

---

## Come Funziona lo Spionaggio

L'efficacia di una missione di spionaggio dipende da due fattori principali:

1. **La Forza dello Spionaggio**: determinata dalla differenza tra la tua ricerca **Spionaggio** e la ricerca **Contro-Spionaggio** del difensore.
2. **La Precisione**: quanto sono accurati i dettagli del rapporto ricevuto.

### Forza dello Spionaggio

La forza dello spionaggio si calcola così:

Forza = Spionaggio_Attaccante - Contro-Spionaggio_Difensore


- Se la forza è **≤ 0**, la missione fallisce e non ottieni alcuna informazione.
- Più alta è la forza, più **dettagli** potrai scoprire (fino a 6 livelli di dettaglio).
- La forza massima è teoricamente illimitata, ma per ottenere informazioni perfette è sufficiente raggiungere 20 punti di differenza.

### Precisione

La precisione determina l'accuratezza delle informazioni ricevute:

Precisione = Forza × 45 (cap massimo 1000)


| Forza | Precisione | Effetto |
| :---: | :---: | :--- |
| 1 | 45 | Informazioni molto approssimative (range ampio) |
| 5 | 225 | Informazioni parzialmente accurate |
| 10 | 450 | Informazioni moderatamente accurate |
| 15 | 675 | Informazioni abbastanza accurate |
| 20+ | 900+ | **Informazioni esatte** (valori reali) |

Con **precisione ≥ 900** (cioè forza ≥ 20), il rapporto mostrerà i **valori esatti** del nemico. Con precisione inferiore, riceverai una **stima** compresa tra un valore minimo e un valore massimo.

#### Come viene calcolata la stima

Il gioco utilizza un sistema di **range** per determinare la stima:

- L'errore massimo dipende dalla precisione: `ErroreMax = 1 - (Precisione / 1000)`
- L'errore minimo è la metà dell'errore massimo: `ErroreMin = ErroreMax × 0.5`
- Viene generato un valore casuale tra ErroreMin ed ErroreMax
- Il range finale è: `[ValoreReale × (1 - Errore), ValoreReale × (1 + Errore)]`

**Esempio**: Se il nemico ha 100 Guerrieri e la tua precisione è 450:
- ErroreMax = 1 - 0.45 = 0.55 (55%)
- ErroreMin = 0.55 × 0.5 = 0.275 (27.5%)
- L'errore casuale sarà tra 27.5% e 55%
- Riceverai una stima tra circa 45 e 155 Guerrieri

---

## Livelli di Spionaggio

La forza dello spionaggio determina **quali informazioni** puoi ottenere. Più è alta, più dettagliato sarà il rapporto.

| Livello | Forza Richiesta | Cosa Rivedi |
| :---: | :---: | :--- |
| **0** | ≤ 0 | ❌ **Missione fallita** – Nessuna informazione. |
| **1** | 1 – 2 | **Risorse** – Civili (Cibo, Legno, Pietra, Ferro, Oro, Popolazione) e Militari (Spade, Lance, Archi, Scudi, Armature, Frecce). |
| **2** | 3 – 4 | **Truppe** – Numero di unità schierate in ogni struttura difensiva (Ingresso, Mura, Cancello, Torri, Centro, Castello). |
| **3** | 5 – 6 | **Villaggio e Difese** – Salute, Difesa e Guarnigione di Mura, Cancello, Torri e Castello. |
| **4** | 7 – 9 | **Edifici** – Numero di edifici civili (Fattorie, Segherie, Cave, Miniere, Abitazioni) e militari (Workshop, Caserme). |
| **5** | 10 – 12 | **Ricerche** – Livello di tutte le ricerche civili e militari del nemico. |
| **6** | 13+ | **Bonus e Statistiche** – Tutti i bonus attivi e le statistiche complete delle unità (Salute, Attacco, Difesa per ogni tier). |

### Dettaglio per Livello

#### Livello 1 – Risorse
Scopri le quantità esatte di:
- **Risorse Civili**: Cibo, Legno, Pietra, Ferro, Oro, Popolazione.
- **Risorse Militari**: Spade, Lance, Archi, Scudi, Armature, Frecce.

#### Livello 2 – Truppe
Per ogni struttura difensiva (Ingresso, Mura, Cancello, Torri, Centro, Castello), scopri il numero di:
- Guerrieri (Tier I–V)
- Lanceri (Tier I–V)
- Arcieri (Tier I–V)
- Catapulte (Tier I–V)

**Nota**: Le truppe per ogni struttura vengono caricate solo se hai raggiunto il livello 2 di spionaggio.

#### Livello 3 – Villaggio e Difese
Per ogni struttura (Mura, Cancello, Torri, Castello), scopri:
- **Salute** (attuale e massima)
- **Difesa** (attuale e massima)
- **Guarnigione** (attuale e massima)
- Livello delle ricerche associate (Salute, Difesa, Guarnigione, Livello)

Viene rivelato anche il numero totale di **truppe presenti nel villaggio** (somma di tutte le unità).

#### Livello 4 – Edifici
Scopri il numero di:
- **Edifici Civili**: Fattorie, Segherie, Cave, Miniere di Ferro, Miniere d'Oro, Abitazioni.
- **Edifici Militari**: Workshop (Spade, Lance, Archi, Scudi, Armature, Frecce).
- **Caserme**: Guerrieri, Lanceri, Arcieri, Catapulte.

Le informazioni sugli edifici sono **stimate** in base alla precisione (usano il sistema di range).

#### Livello 5 – Ricerche
Scopri il livello di tutte le ricerche del nemico:
- **Ricerche Civili**: Produzione, Costruzione, Addestramento, Popolazione, Trasporto, Riparazione, Spionaggio, Contro-Spionaggio.
- **Ricerche Militari**: per ogni unità (Guerriero, Lanciere, Arciere, Catapulta) – Salute, Attacco, Difesa, Livello.

#### Livello 6 – Bonus e Statistiche
Scopri:
- **Bonus attivi**: su Salute, Attacco, Difesa di ogni unità; su Salute e Difesa delle strutture; su Produzione, Costruzione, Addestramento, Trasporto, Ricerca, Spionaggio, Contro-Spionaggio.
- **Statistiche complete** delle unità: per ogni tier (I–V) e per ogni tipo di unità, i valori di Salute, Attacco e Difesa.

---

## Contro-Spionaggio

Il Contro-Spionaggio è la tua difesa contro le spie nemiche. Ogni punto di ricerca in **Contro-Spionaggio** riduce la forza dello spionaggio dell'attaccante.

### Come Funziona

Quando un giocatore ti spia, la sua forza viene calcolata come:

Forza = Spionaggio_Attaccante - Contro-Spionaggio_Difensore


| Contro-Spionaggio | Effetto sul Nemico |
| :---: | :--- |
| **0** | Nessuna protezione. Il nemico usa tutta la sua forza di spionaggio. |
| **5** | Riduce la forza del nemico di 5 punti. |
| **10** | Riduce la forza del nemico di 10 punti (potrebbe abbassare il livello di dettaglio). |
| **≥ Spionaggio_Nemico** | Annulla completamente lo spionaggio (forza ≤ 0, missione fallita). |

### Perché Investire nel Contro-Spionaggio?

- **Protegge le tue risorse**: Il nemico non saprà esattamente quanto hai.
- **Nasconde le tue difese**: Mura, Cancello, Torri e Castello rimarranno un mistero.
- **Rende l'attacco più rischioso**: Un nemico che non sa cosa lo aspetta potrebbe attaccare con l'esercito sbagliato.
- **Contrasta gli spioni esperti**: Anche con Spionaggio 20, se hai Contro-Spionaggio 20, la missione fallisce.

### Suggerimenti

- **Bilancia Spionaggio e Contro-Spionaggio**: Avere entrambi alti ti rende un bersaglio difficile da spiare, ma ti permette anche di spiare gli altri.
- **Non trascurare il Contro-Spionaggio**: Anche pochi punti possono fare la differenza tra un rapporto dettagliato e uno vago.

---

## Il Rapporto di Spionaggio

Dopo una missione riuscita, riceverai un **rapporto dettagliato** che include:

- **Data e ora** della missione.
- **Nome del giocatore** bersaglio, suo livello ed esperienza.
- **Forza dello spionaggio** utilizzata.
- **Livello di spionaggio** raggiunto (0-6).
- **Tutte le informazioni** sbloccate in base al livello.

### Cosa contiene il rapporto

| Sezione | Contenuto |
| :--- | :--- |
| **Risorse** | Quantità di risorse civili e militari (esatte). |
| **Truppe** | Numero di unità per struttura difensiva (stimate o esatte). |
| **Villaggio** | Salute, difesa e guarnigione di Mura, Cancello, Torri, Castello. |
| **Edifici** | Numero di edifici civili e militari (stimate o esatte). |
| **Ricerche** | Livello di tutte le ricerche (esatte). |
| **Bonus** | Bonus attivi su unità, strutture e produzione. |
| **Statistiche** | Statistiche complete di tutte le unità (esatte). |

Se la missione fallisce (forza ≤ 0), il rapporto indicherà semplicemente che lo spionaggio non è riuscito.

---

## Migliorare lo Spionaggio

Per ottenere informazioni sempre più precise e dettagliate, devi investire nella ricerca **Spionaggio**.

| Ricerca | Effetto |
| :--- | :--- |
| **Spionaggio** | Aumenta la forza dello spionaggio, migliorando precisione e livello delle informazioni. |
| **Contro-Spionaggio** | Riduce la forza dello spionaggio nemico, proteggendo le tue informazioni. |

### Strategie

- **Attaccante**: Investi in **Spionaggio** per conoscere a fondo il nemico prima di attaccare. Una forza ≥ 20 ti garantisce informazioni esatte.
- **Difensore**: Investi in **Contro-Spionaggio** per proteggere i tuoi segreti e ridurre l'efficacia delle spie nemiche.
- **Equilibrio**: Avere entrambi alti ti rende un avversario temibile, sia in attacco che in difesa.

---

## Esempio Pratico

| Scenario | Spionaggio Attaccante | Contro-Spionaggio Difensore | Forza | Precisione | Livello | Risultato |
| :--- | :---: | :---: | :---: | :---: | :---: | :--- |
| **Principiante** | 2 | 0 | 2 | 90 | 1 | Vede solo le risorse. |
| **Intermedio** | 8 | 2 | 6 | 270 | 3 | Vede risorse, truppe e difese. |
| **Avanzato** | 15 | 3 | 12 | 540 | 5 | Vede tutto tranne i bonus. |
| **Elite** | 25 | 5 | 20 | 900 | 6 | Vede **tutto** con precisione esatta. |
| **Contro-Spionaggio** | 20 | 20 | 0 | 0 | 0 | ❌ Missione fallita! |

---

**Ricorda**: una buona intelligence è la chiave per vittorie decisive e per evitare attacchi costosi. Investi nello spionaggio e nel contro-spionaggio per tenere sempre d'occhio i tuoi avversari e proteggere i tuoi segreti!

---

## Vedi Anche

- [Ricerca](https://github.com/adlos96/Warrior-and-Wealth/blob/main/Wiki/Game/Componenti/Ricerca.md) – per potenziare Spionaggio e Contro-Spionaggio.
- [Battaglie PVP](https://github.com/adlos96/Warrior-and-Wealth/blob/main/Wiki/Game/Battaglie/PVP.md) – per usare le informazioni raccolte negli attacchi.
- [Difesa](https://github.com/adlos96/Warrior-and-Wealth/blob/main/Wiki/Game/Battaglie/Difesa.md) – per proteggere il tuo villaggio.
- [Edifici](https://github.com/adlos96/Warrior-and-Wealth/blob/main/Wiki/Game/Componenti/Edifici.md) – per costruire e potenziare le strutture.

[⬅️ Torna al Menu Principale](https://github.com/adlos96/Warrior-and-Wealth/blob/main/README.md)