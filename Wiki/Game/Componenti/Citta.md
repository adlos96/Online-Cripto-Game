# Città

[#città](#città)

La **Città** (o **Cittadella**) è il cuore difensivo del tuo Villaggio: qui risiedono le strutture fortificate e la guarnigione che proteggono le risorse e la sopravvivenza del tuo popolo. A differenza degli edifici economici del Villaggio — dedicati alla produzione e alla crescita — la Città è interamente votata alla difesa. Da questa sezione puoi osservare la disposizione degli strati difensivi, gestire le riparazioni e organizzare le guarnigioni struttura per struttura.

Per violare le difese della Città di un altro giocatore, un attaccante deve attraversare in sequenza tutti gli strati che la compongono, fino a raggiungere l'ultimo: solo così potrà saccheggiarne le risorse. Questa pagina descrive la **composizione strutturale** della Città; per le meccaniche di combattimento vere e proprie (fasi di battaglia, calcolo dei danni, saccheggio), consulta la [Guida alla Difesa](https://github.com/adlos96/Warrior-and-Wealth/blob/main/Wiki/Game/Battaglie/Difesa.md).

> ⚠️ Quando una struttura viene danneggiata è possibile avviare le riparazioni. Queste richiedono risorse e tempo; se non ci sono risorse sufficienti la riparazione non può essere avviata o interrotta se già attiva.

| **Edificio**        | **Statistiche**             | **Strato** | **Descrizione**                                                                                                                                            |
| ------------------- | --------------------------- | ---------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Ingresso**        | Guarnigione                 | 1          | Punto d'accesso iniziale; ottiene bonus se **Mura** e **Cancello** sono presidiati da un numero sufficiente di uomini.                                     |
| **Mura**            | Salute, Difesa, Guarnigione | 2          | Prima linea difensiva; assorbe parte dei danni diretti alle strutture interne.                                                                             |
| **Cancello**        | Salute, Difesa, Guarnigione | 3          | Ostacolo che rallenta l'avanzata; può essere presidiato per aumentare la resistenza.                                                                       |
| **Torri**           | Salute, Difesa, Guarnigione | 4          | Linea difensiva aggiuntiva; cambia le statistiche dell'edificio (Salute, Difesa, Guarnigione).                                                             |
| **Centro**          | Guarnigione                 | 5          | Postazione difensiva avanzata di fronte al castello; dispone solo di guarnigione.                                                                          |
| **Castello**        | Salute, Difesa, Guarnigione | 6          | Cuore della difesa; elevata resistenza e capacità di presidio.                                                                                             |
| **Attacco Diretto (o Giocatore)** | Guarnigione                     | 7          | Ultimo baluardo: raggiungendo questo strato e sconfiggendo la guarnigione, l'attaccante può saccheggiare risorse e diamanti; **Difensore:** in caso di sconfitta rischia la perdita di risorse preziose. |

## Caratteristiche delle strutture

[#caratteristiche-delle-strutture](#caratteristiche-delle-strutture)

Per ogni struttura sono rilevanti tre valori principali: **Salute**, **Difesa** e **Guarnigione**. Ingresso e Centro dispongono solo di guarnigione.

| **Voce**        | **Descrizione**                                                                                                                                             |
| --------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Salute**      | Indica lo stato strutturale. Se la Salute raggiunge zero la struttura crolla ed eventuale guarnigione viene eliminata.                                      |
| **Difesa**      | Riduce i danni subiti: assorbe parte della forza d'attacco prima che venga sottratta alla Salute.                                                            |
| **Guarnigione** | Numero di unità che possono presidiare la struttura; la sua presenza aumenta l'efficacia difensiva e riduce i danni ricevuti dalla struttura.                |

> ⚠️ Una struttura senza guarnigione è comunque in grado di assorbire gli attacchi, ma subirà danni maggiori rispetto a una struttura presidiata. La struttura deve essere danneggiata per far si che le unità la possano attraversare.

## Riparazioni

[#riparazioni](#riparazioni)

Durante un assedio, quando la struttura viene danneggiata, le riparazioni sono fondamentali per il suo mantenimento. La salute e la difesa della struttura non si ripristinano da sole, servono delle squadre di riparatori, tempo e molte risorse per questa opera.

- **Avvio:** quando una o più strutture sono danneggiate puoi avviare una riparazione specifica o generale. in base alle strutture coinvolote le risorse richieste posso variare, ogni punto ripristinato preleva risorse dai magazzini del villaggio, questa operazione richiede del tempo, in base all valore da ripristinare.
- **Interruzione:** se durante le riparazioni le risorse si esauriscono, i lavori si fermano all'instante, senza il raggiongimento del valore massimo.
- **Costo e durata:** dipendono dal livello della struttura, dall'entità del danno subito e dalla ricerca **Riparazione**.

> Per capire come viene calcolato il danno subito da ciascuna struttura durante un attacco, consulta la [Guida alla Difesa](https://github.com/adlos96/Warrior-and-Wealth/blob/main/Wiki/Game/Battaglie/Difesa.md).

## Strato 7: Attacco Diretto

[#strato-7-attacco-diretto](#strato-7-attacco-diretto)

L'ultimo strato non possiede Salute o Difesa proprie: la sua tenuta dipende dalla totalità dell'esercito schierato dal giocatore. Solo raggiungendo questo strato l'attaccante può saccheggiare risorse e diamanti; per le regole complete di combattimento e saccheggio applicate a questo strato, consulta la [Guida alla Difesa](https://github.com/adlos96/Warrior-and-Wealth/blob/main/Wiki/Game/Battaglie/Difesa.md) e la [Guida al PVP](https://github.com/adlos96/Warrior-and-Wealth/blob/main/Wiki/Game/Battaglie/PVP.md).

---

[⬅️ Torna al Menu Principale](https://github.com/adlos96/Warrior-and-Wealth/blob/main/README.md) | [📅 Vai alla Guida Quest](https://github.com/adlos96/Warrior-and-Wealth/blob/main/Wiki/Game/Battaglie/Quest.md)