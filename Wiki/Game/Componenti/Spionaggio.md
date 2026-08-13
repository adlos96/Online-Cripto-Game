# Spionaggio

[#spionaggio](#spionaggio)

Prima di lanciare un assedio, un buon generale vuole sapere cosa lo aspetta. Lo **Spionaggio** è lo strumento che ti permette di scoprire le reali capacità di un avversario prima ancora di muovere un solo soldato: inviando una spia in territorio nemico puoi scoprire risorse, difese e potenza dell'esercito del bersaglio, informazioni preziose per decidere se, come e quando colpire.

Questa guida spiega come funziona lo spionaggio, cosa puoi scoprire a ogni livello e come proteggerti dalle spie altrui tramite il **Contro-Spionaggio**.

---

## Come Funziona lo Spionaggio

[#come-funziona-lo-spionaggio](#come-funziona-lo-spionaggio)

L'efficacia di una missione di spionaggio dipende da due fattori: la **Forza** della missione e la **Precisione** del rapporto che ne deriva.

### Forza dello Spionaggio

[#forza-dello-spionaggio](#forza-dello-spionaggio)

La forza è la differenza tra la tua ricerca **Spionaggio** e la ricerca **Contro-Spionaggio** del difensore:

```
Forza = Spionaggio (Attaccante) − Contro-Spionaggio (Difensore)
```

- Se la forza è **≤ 0**, la missione fallisce e non ottieni alcuna informazione.
- Più alta è la forza, più **livelli di dettaglio** sblocchi (fino a 6).
- Non esiste un tetto massimo alla forza, ma **20 punti di differenza** sono già sufficienti per ottenere informazioni perfette.

### Precisione

[#precisione](#precisione)

La precisione determina quanto sono affidabili i numeri che ricevi nel rapporto:

```
Precisione = Forza × 45   (cap massimo 1000)
```

| Forza | Precisione | Effetto                                        |
| :---: | :--------: | ----------------------------------------------- |
| 1     | 45         | Informazioni molto approssimative (range ampio)  |
| 5     | 225        | Informazioni parzialmente accurate               |
| 10    | 450        | Informazioni moderatamente accurate              |
| 15    | 675        | Informazioni abbastanza accurate                 |
| 20+   | 900+       | **Informazioni esatte** (valori reali)           |

Con **precisione ≥ 900** (forza ≥ 20) il rapporto mostra i valori esatti del nemico. Sotto quella soglia, ricevi una **stima** compresa tra un valore minimo e un valore massimo.

> 💡 **Consiglio:** non serve rincorrere la forza massima per ogni missione. Se ti basta sapere approssimativamente quante risorse ha un bersaglio prima di un saccheggio veloce, una forza modesta è più che sufficiente — riserva gli investimenti pesanti in Spionaggio per i bersagli che contano davvero.

### Come viene calcolata la stima

[#come-viene-calcolata-la-stima](#come-viene-calcolata-la-stima)

Quando la precisione non è massima, il gioco genera un range attorno al valore reale:

```
ErroreMax = 1 − (Precisione / 1000)
ErroreMin = ErroreMax × 0.5
Range     = [ValoreReale × (1 − Errore), ValoreReale × (1 + Errore)]
```

dove `Errore` è un valore casuale compreso tra `ErroreMin` ed `ErroreMax`.

**Esempio:** il nemico ha 100 Guerrieri e la tua precisione è 450.
- `ErroreMax = 1 − 0.45 = 0.55` (55%)
- `ErroreMin = 0.55 × 0.5 = 0.275` (27.5%)
- L'errore casuale ricade tra il 27.5% e il 55%
- Riceverai una stima tra circa **45 e 155 Guerrieri**

---

## Livelli di Spionaggio

[#livelli-di-spionaggio](#livelli-di-spionaggio)

La forza determina quali informazioni riesci a strappare al nemico: più è alta, più il rapporto si arricchisce di dettagli.

| Livello | Forza Richiesta | Cosa Rivela | Descrizione |
| :---: | :--------: | :----------- | --------------------------------------- |
| **0**   | ≤ 0              | ❌ **Missione fallita** | nessuna informazione.                                                            |
| **1**   | 1 – 2            | **Risorse** | civili e militari . |
| **2**   | 3 – 4             | **Truppe** | numero di unità schierate in ogni struttura difensiva della [Città](https://github.com/adlos96/Warrior-and-Wealth/blob/main/Wiki/Game/Componenti/Citt%C3%A0.md) (Ingresso, Mura, Cancello, Torri, Centro, Castello). |
| **3**   | 5 – 6             | **Città e Difese** | Salute, Difesa e Guarnigione di Mura, Cancello, Torri e Castello.                    |
| **4**   | 7 – 9             | **Edifici** | numero di edifici civili (Fattorie, Segherie, Cave, Miniere, Abitazioni) e militari (Workshop, Caserme). |
| **5**   | 10 – 12           | **Ricerche** | livello di tutte le ricerche civili e militari del nemico.                                 |
| **6**   | 13+               | **Bonus e Statistiche** | tutti i bonus attivi e le statistiche complete delle unità per ogni tier.        |

---

## Contro-Spionaggio

[#contro-spionaggio](#contro-spionaggio)

Il **Contro-Spionaggio** è la tua difesa contro le spie nemiche: ogni punto investito riduce direttamente la forza dello spionaggio di chi ti osserva, secondo la stessa formula vista sopra (`Forza = Spionaggio Attaccante − Contro-Spionaggio Difensore`).

| Contro-Spionaggio      | Effetto sul Nemico                                                    |
| :---------------------: | ------------------------------------------------------------------------ |
| **0**                   | Nessuna protezione: il nemico usa tutta la sua forza di spionaggio.       |
| **5**                   | Riduce la forza del nemico di 5 punti.                                    |
| **10**                  | Riduce la forza del nemico di 10 punti (può abbassare il livello di dettaglio ottenuto). |
| **≥ Spionaggio Nemico** | Annulla completamente lo spionaggio: forza ≤ 0, missione fallita.         |

### Perché Investire nel Contro-Spionaggio

[#perché-investire-nel-contro-spionaggio](#perché-investire-nel-contro-spionaggio)

- **Protegge le tue risorse:** il nemico non saprà esattamente quanto possiedi.
- **Nasconde le tue difese:** Mura, Cancello, Torri e Castello restano un mistero.
- **Rende l'attacco più rischioso per l'avversario:** chi non sa cosa lo aspetta rischia di attaccare con l'esercito sbagliato.
- **Contrasta anche gli spioni esperti:** con Spionaggio 20 e Contro-Spionaggio 20 di fronte, la missione fallisce comunque.

> 💡 **Consiglio:** Spionaggio e Contro-Spionaggio non si escludono a vicenda. Un giocatore con entrambi alti è un bersaglio difficile da spiare, ma resta perfettamente in grado di spiare gli altri — anche pochi punti di Contro-Spionaggio possono fare la differenza tra un rapporto dettagliato e uno vago.

---

## Il Rapporto di Spionaggio

[#il-rapporto-di-spionaggio](#il-rapporto-di-spionaggio)

Dopo una missione riuscita, ricevi un rapporto che include data e ora, nome/livello/esperienza del bersaglio, forza utilizzata, livello di spionaggio raggiunto (0–6) e tutte le informazioni sbloccate a quel livello.

| Sezione        | Contenuto                                                         |
| --------------- | ------------------------------------------------------------------- |
| **Risorse**     | Quantità di risorse civili e militari (esatte).                     |
| **Truppe**      | Numero di unità per struttura difensiva (stimate o esatte).          |
| **Città**       | Salute, Difesa e Guarnigione di Mura, Cancello, Torri, Castello.      |
| **Edifici**     | Numero di edifici civili e militari (stimate o esatte).              |
| **Ricerche**    | Livello di tutte le ricerche (esatte).                                |
| **Bonus**       | Bonus attivi su unità, strutture e produzione.                       |
| **Statistiche** | Statistiche complete di tutte le unità (esatte).                     |

Se la missione fallisce (forza ≤ 0), il rapporto si limita a segnalare l'insuccesso.

---

## Migliorare lo Spionaggio

[#migliorare-lo-spionaggio](#migliorare-lo-spionaggio)

Entrambe le discipline si potenziano tramite ricerca dedicata:

| Ricerca               | Effetto                                                                 |
| ----------------------- | -------------------------------------------------------------------------- |
| **Spionaggio**         | Aumenta la forza delle tue missioni, migliorando precisione e livello di dettaglio ottenibile. |
| **Contro-Spionaggio**  | Riduce la forza dello spionaggio nemico, proteggendo le tue informazioni.   |

**Strategie:**
- **Attaccante:** investi in Spionaggio prima di ogni campagna importante — una forza ≥ 20 garantisce informazioni esatte su cui basare il piano d'assedio.
- **Difensore:** investi in Contro-Spionaggio per proteggere i tuoi segreti e ridurre l'efficacia delle spie nemiche.
- **Equilibrio:** avere entrambi alti ti rende un avversario temibile sia in attacco che in difesa.

---

## Esempio Pratico

[#esempio-pratico](#esempio-pratico)

| Scenario              | Spionaggio | Contro-Spionaggio | Forza | Precisione | Livello | Risultato                          |
| ----------------------- | :----------: | :------------------: | :-----: | :----------: | :-------: | ------------------------------------- |
| **Principiante**       | 2            | 0                    | 2      | 90           | 1        | Vede solo le risorse.                  |
| **Intermedio**         | 8            | 2                    | 6      | 270          | 3        | Vede risorse, truppe e difese.         |
| **Avanzato**           | 15           | 3                    | 12     | 540          | 5        | Vede tutto tranne i bonus.             |
| **Elite**              | 25           | 5                    | 20     | 900          | 6        | Vede **tutto**, con precisione esatta. |
| **Contro-Spionaggio pari** | 20        | 20                   | 0      | 0            | 0        | ❌ Missione fallita!                    |


Una buona intelligence è la differenza tra un assedio ben pianificato e un attacco alla cieca. Investi in Spionaggio e Contro-Spionaggio per tenere sempre d'occhio i tuoi avversari e proteggere i tuoi segreti.

---

[⬅️ Torna al Menu Principale](https://github.com/adlos96/Warrior-and-Wealth/blob/main/README.md)