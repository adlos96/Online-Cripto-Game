
namespace Server_Strategico.ServerData.Moduli.Battaglie
{
    public class Battaglia
    {
        public class UnitGroup
        {
            public int[] Guerrieri { get; set; }
            public int[] Lancieri { get; set; }
            public int[] Arcieri { get; set; }
            public int[] Catapulte { get; set; }

            public UnitGroup()
            {
                Guerrieri = new int[5];
                Lancieri = new int[5];
                Arcieri = new int[5];
                Catapulte = new int[5];
            }
            public UnitGroup Clone()
            {
                return new UnitGroup
                {
                    Guerrieri = (int[])Guerrieri.Clone(),
                    Lancieri = (int[])Lancieri.Clone(),
                    Arcieri = (int[])Arcieri.Clone(),
                    Catapulte = (int[])Catapulte.Clone()
                };
            }
            public int TotalUnits()
            {
                return Guerrieri.Sum() + Lancieri.Sum() + Arcieri.Sum() + Catapulte.Sum();
            }
            public int CountUnitTypes()
            {
                int count = 0;
                if (Guerrieri.Any(g => g > 0)) count++;
                if (Lancieri.Any(l => l > 0)) count++;
                if (Arcieri.Any(a => a > 0)) count++;
                if (Catapulte.Any(c => c > 0)) count++;
                return Math.Max(count, 1); // Evita divisione per zero
            }
            //Sottrae le unità di un gruppo da questo gruppo, restituendo un nuovo gruppo con i risultati (non modifica il gruppo originale)
            public UnitGroup Subtract(UnitGroup group)
            {
                return new UnitGroup
                {
                    Guerrieri = Guerrieri.Zip(group.Guerrieri, (a, b) => Math.Max(0, a - b)).ToArray(),
                    Lancieri = Lancieri.Zip(group.Lancieri, (a, b) => Math.Max(0, a - b)).ToArray(),
                    Arcieri = Arcieri.Zip(group.Arcieri, (a, b) => Math.Max(0, a - b)).ToArray(),
                    Catapulte = Catapulte.Zip(group.Catapulte, (a, b) => Math.Max(0, a - b)).ToArray()
                };
            }
        }
        public class Report
        {
            public string Tipo { get; set; } = "";
            public string Data { get; set; } = "";
            public bool Aperto { get; set; } = false;
            public RisultatoBattaglia Battaglia { get; set; }
            public RisultatoSpionaggio Spionaggio { get; set; }
        }

        public class RisultatoBattaglia
        {
            public string Tipo_Battaglia { get; set; } = "";
            public string Nome_Attaccante { get; set; } = "";
            public string Nome_Difensore { get; set; } = "";
            public double Forza_Attaccante { get; set; }
            public double Forza_Attaccante_Finale { get; set; }
            public double Forza_Difensore { get; set; }
            public double Forza_Difensore_Finale { get; set; }
            public bool Vittoria_Attaccante { get; set; }
            public int Xp_Attaccante { get; set; } = 0;
            public int Xp_Difensore { get; set; } = 0;
            public List<RisultatoFase> Fasi { get; set; } = new List<RisultatoFase>();
            public BonusRicerca Bonus_Ricerca_Difesa { get; set; } = new BonusRicerca();
            public BonusRicerca Bonus_Ricerca_Attacco { get; set; } = new BonusRicerca();
            public RisorseRaccolte Risorse_Raccolte { get; set; } = new RisorseRaccolte();
        }
        public class RisultatoFase
        {
            public bool Vittoria_Attaccante { get; set; }
            public bool Unità_Presenti_Difensore { get; set; }
            public bool Struttura_Crollata { get; set; }
            public int Xp_Attaccante { get; set; } = 0;
            public int Xp_Difensore { get; set; } = 0;
            public Villaggio Struttura { get; set; } = new Villaggio();
            public BattagliaDistanza Fase_Distanza { get; set; } = new BattagliaDistanza();
            public Unità Attaccante { get; set; } = new Unità();
            public Unità Difensore { get; set; } = new Unità();
            public UnitGroup Perdite_Crollo { get; set; } = new UnitGroup();
        }
        public class Unità
        {
            public UnitGroup Schierati { get; set; } = new UnitGroup();
            public UnitGroup Sopravvisuti { get; set; } = new UnitGroup();
            public UnitGroup Perdite { get; set; } = new UnitGroup();
        }
        public class Villaggio
        {
            public string Nome { get; set; } = "";
            public int Salute { get; set; }
            public int Difesa { get; set; }
            public int SaluteMax { get; set; }
            public int DifesaMax { get; set; }
            public int Guarnigione { get; set; }
        }
        public class BonusRicerca
        {
            public string Bonus_Salute_Unità { get; set; } = "0";
            public string Bonus_Difesa_Unità { get; set; } = "0";
            public string Bonus_Attacco_Unità { get; set; } = "0";
            public string Bonus_Salute_Strutture { get; set; } = "0";
            public string Bonus_Difesa_Strutture { get; set; } = "0";
            public string Bonus_Guarnigione_Strutture { get; set; } = "0";
        }
        public class BattagliaDistanza
        {
            public bool Attaccante_Poche_Frecce { get; set; }
            public bool Difensore_Poche_Frecce { get; set; }
            public UnitGroup Attaccante_Schierati { get; set; } = new UnitGroup();
            public UnitGroup Attaccante_Sopravvisuti { get; set; } = new UnitGroup();
            public UnitGroup Attaccante_Morti { get; set; } = new UnitGroup();
            public UnitGroup Difensore_Schierati { get; set; } = new UnitGroup();
            public UnitGroup Difensore_Sopravvisuti { get; set; } = new UnitGroup();
            public UnitGroup Difensore_Morti { get; set; } = new UnitGroup();
            public int Attaccante_XP { get; set; } = 0;
            public int Attaccante_Danno_Guerrieri { get; set; }
            public int Attaccante_Danno_Lancieri { get; set; }
            public int Attaccante_Frecce_Usate { get; set; }
            public int Attaccante_Frecce_Necessarie { get; set; }
            public int Difensore_XP { get; set; } = 0;
            public int Difensore_Danno_Guerrieri { get; set; }
            public int Difensore_Danno_Lancieri { get; set; }
            public int Difensore_Frecce_Usate { get; set; }
            public int Difensore_Frecce_Necessarie { get; set; }
            public bool Difensore_Unità_Presenti { get; set; }
        }
        public class RangedBattleResult
        {
            public UnitGroup Attaccante_Sopravvisuti { get; set; }
            public UnitGroup Difensore_Sopravvisuti { get; set; }

            public int[] DefenderKills_Guerrieri { get; set; }  // Array di 5 elementi
            public int[] DefenderKills_Lancieri { get; set; }    // Array di 5 elementi
            public int[] AttackerKills_Guerrieri { get; set; }    // Array di 5 elementi
            public int[] AttackerKills_Lancieri { get; set; }     // Array di 5 elementi

            public int Attaccante_Danno_Guerrieri { get; set; }
            public int Attaccante_Danno_Lancieri { get; set; }
            public int Difensore_Danno_Guerrieri { get; set; }
            public int Difensore_Danno_Lancieri { get; set; }

            public int Attaccante_Frecce_Usate { get; set; }
            public int Attaccante_Frecce_Necessarie { get; set; }
            public bool Attaccante_Poche_Frecce { get; set; }
            public int Attaccante_XP { get; set; }
            public int Difensore_Frecce_Usate { get; set; }
            public int Difensore_Frecce_Necessarie { get; set; }
            public int Difensore_XP { get; set; }
            public bool Difensore_Poche_Frecce { get; set; }
            public bool Difensore_Unità_Presenti { get; set; }

            public RangedBattleResult()
            {
                AttackerKills_Guerrieri = new int[5];
                AttackerKills_Lancieri = new int[5];
                DefenderKills_Guerrieri = new int[5];
                DefenderKills_Lancieri = new int[5];
            }

            // Helper per ottenere totali (utile per log)
            public int GetTotalPlayerKills() => AttackerKills_Guerrieri.Sum() + AttackerKills_Lancieri.Sum();
            public int GetTotalEnemyKills() => DefenderKills_Guerrieri.Sum() + DefenderKills_Lancieri.Sum();
        }
        public class RisorseRaccolte
        {
            public int Cibo { get; set; }
            public int Legno { get; set; }
            public int Pietra { get; set; }
            public int Ferro { get; set; }
            public int Oro { get; set; }
            public int Esperienza { get; set; }
            public int Diamanti_Blu { get; set; }
            public int Diamanti_Viola { get; set; }
            public int Capacità_Carico { get; set; }
            public int Capacità_Carico_Usata { get; set; }
        }

        //Spionaggio
        public class RisultatoSpionaggio
        {
            public string Tipo_Battaglia { get; set; } = "";
            public bool Spionaggio_Riuscito { get; set; }
            public int Forza_Spionaggio { get; set; }
            public int Stadio { get; set; }
            public DatiGiocatore Giocatore { get; set; } = new DatiGiocatore();
            public RisorseCivili Risorse_Civili { get; set; } = new RisorseCivili();
            public RisorseMilitari Risorse_Militari { get; set; } = new RisorseMilitari();
            public RisorseSpeciali Risorse_Speciali { get; set; } = new RisorseSpeciali();
            public Edifici_Civili Strutture_Civili { get; set; } = new Edifici_Civili();
            public Workshop Workshop { get; set; } = new Workshop();
            public Caserme Caserme { get; set; } = new Caserme();
            public StatsUnità Stats_Unità { get; set; } = new StatsUnità();
            public List<SpionaggioFase> Fasi { get; set; } = new List<SpionaggioFase>();
            public RicercaCivile Ricerca_Civile { get; set; } = new RicercaCivile();
            public RicercaMilitare Ricerca_Militare { get; set; } = new RicercaMilitare();
            public Bonus Bonus { get; set; } = new Bonus();
        }
        public class RisorseCivili
        {
            public int Cibo { get; set; }
            public int Legno { get; set; }
            public int Pietra { get; set; }
            public int Ferro { get; set; }
            public int Oro { get; set; }
            public int Popolazione { get; set; }
        }
        public class RisorseMilitari
        {
            public int Spade { get; set; }
            public int Lance { get; set; }
            public int Archi { get; set; }
            public int Scudi { get; set; }
            public int Armature { get; set; }
            public int Frecce { get; set; }
        }
        public class RisorseSpeciali
        {
            public int Diamanti_Blu { get; set; }
            public int Diamanti_Viola { get; set; }
        }
        public class DatiGiocatore
        {
            public string Nome { get; set; } = "";
            public int Forza_Attaccante { get; set; }
            public int Livello { get; set; }
            public int Esperienza { get; set; }
        }
        public class Edifici_Civili
        {
            public TripleValue Fattoria { get; set; } = new TripleValue();
            public TripleValue Segheria { get; set; } = new TripleValue();
            public TripleValue Cava { get; set; } = new TripleValue();
            public TripleValue Miniera_Ferrro { get; set; } = new TripleValue();
            public TripleValue Miniera_Oro { get; set; } = new TripleValue();
            public TripleValue Abitazioni { get; set; } = new TripleValue();
        }
        public class Workshop
        {
            public TripleValue Spade { get; set; } = new TripleValue();
            public TripleValue Lance { get; set; } = new TripleValue();
            public TripleValue Archi { get; set; } = new TripleValue();
            public TripleValue Scudi { get; set; } = new TripleValue();
            public TripleValue Armature { get; set; } = new TripleValue();
            public TripleValue Frecce { get; set; } = new TripleValue();
        }
        public class Caserme
        {
            public TripleValue Guerrieri { get; set; } = new TripleValue();
            public TripleValue Lanceri { get; set; } = new TripleValue();
            public TripleValue Arcieri { get; set; } = new TripleValue();
            public TripleValue Catapulte { get; set; } = new TripleValue();
        }
        public class TripleValue
        {
            public int Min { get; set; }
            public int Reale { get; set; }
            public int Max { get; set; }
        }
        public class SpionaggioFase
        {
            public SpionaggioVillaggio Struttura { get; set; } = new SpionaggioVillaggio();
            public TripleValue[] Guerrieri { get; set; } = Enumerable.Range(0, 5).Select(_ => new TripleValue()).ToArray();
            public TripleValue[] Lanceri { get; set; } = Enumerable.Range(0, 5).Select(_ => new TripleValue()).ToArray();
            public TripleValue[] Arcieri { get; set; } = Enumerable.Range(0, 5).Select(_ => new TripleValue()).ToArray();
            public TripleValue[] Catapulte { get; set; } = Enumerable.Range(0, 5).Select(_ => new TripleValue()).ToArray();
        }
        public class TipiStatistiche
        {
            public double Salute { get; set; } = new double();
            public double Difesa { get; set; } = new double();
            public double Attacco { get; set; } = new double();
            public double Livello { get; set; } = new double();

        }
        public class StatsUnità
        {
            public TipiStatistiche[] Guerrieri { get; set; } = Enumerable.Range(0, 5).Select(_ => new TipiStatistiche()).ToArray();
            public TipiStatistiche[] Lanceri { get; set; } = Enumerable.Range(0, 5).Select(_ => new TipiStatistiche()).ToArray();
            public TipiStatistiche[] Arcieri { get; set; } = Enumerable.Range(0, 5).Select(_ => new TipiStatistiche()).ToArray();
            public TipiStatistiche[] Catapulte { get; set; } = Enumerable.Range(0, 5).Select(_ => new TipiStatistiche()).ToArray();
        }
        public class SpionaggioVillaggio
        {
            public string Nome { get; set; } = "";
            public int Salute { get; set; }
            public int SaluteMin { get; set; }
            public int SaluteMax { get; set; }
            public int Difesa { get; set; }
            public int DifesaMin { get; set; }
            public int DifesaMax { get; set; }
            public int Ricerca_Salute { get; set; }
            public int Ricerca_Difesa { get; set; }
            public int Ricerca_Guarnigione { get; set; }
            public int Ricerca_Livello { get; set; }
            public TripleValue Guarnigione { get; set; } = new TripleValue();
            public int Guarnigione_Max { get; set; }
        }
        public class RicercaCivile
        {
            public int Produzione { get; set; }
            public int Costruzione { get; set; }
            public int Addestramento { get; set; }
            public int Popolazione { get; set; }
            public int Trasporto { get; set; }
            public int Riparazione { get; set; }
            public int Spionaggio { get; set; }
            public int Contro_Spionaggio { get; set; }

        }
        public class RicercaMilitare
        {
            public TipiStatistiche Guerrieri { get; set; } = new TipiStatistiche();
            public TipiStatistiche Lanceri { get; set; } = new TipiStatistiche();
            public TipiStatistiche Arcieri { get; set; } = new TipiStatistiche();
            public TipiStatistiche Catapulte { get; set; } = new TipiStatistiche();

        }
        public class Bonus
        {
            public TipiStatistiche Guerrieri { get; set; } = new TipiStatistiche();
            public TipiStatistiche Lanceri { get; set; } = new TipiStatistiche();
            public TipiStatistiche Arceri { get; set; } = new TipiStatistiche();
            public TipiStatistiche Catapulte { get; set; } = new TipiStatistiche();

            public double Salute_Strutture { get; set; }
            public double Difesa_Strutture { get; set; }
            public double Guarnigione_Strutture { get; set; }

            public double Produzione_Risorse { get; set; }
            public double Costruzione { get; set; }
            public double Addestramento { get; set; }
            public double Capacità_Trasporto { get; set; }
            public double Ricerca { get; set; }
            public double Riparazione { get; set; }
            public double Spionaggio { get; set; }
            public double Contro_Spionaggio { get; set; }
        }
    }
}
