using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Warrior_and_Wealth.GUI
{
    // ===========================
    //  ENUMERAZIONI E TIPI
    // ===========================

    public enum TerrainType { Grass, Forest, Mountain, Water }

    public enum CellEntityType
    {
        Empty,
        PlayerVillage,
        BarbarianVillage,
        BarbarianCity
    }

    // ===========================
    //  ENUM COMANDI MAPPA
    // ===========================

    public enum MapCommand
    {
        Sposta,       // Teletrasporto su cella vuota
        Attacca,      // Attacca villaggio/città nemica
        Esplora,      // Esplora una cella (scouting)
        Commercia,    // Commercia con un altro giocatore
        Spia,         // Invia spia su un altro giocatore
        Rinforza,     // Manda truppe di rinforzo a un alleato
        InfoCella,    // Mostra info dettagliate sulla cella
        Annulla
    }

    // ===========================
    //  CLASSI DATI
    // ===========================

    public class MapCell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public TerrainType Terrain { get; set; }
        public CellEntity? Entity { get; set; }
    }

    public class CellEntity
    {
        public CellEntityType Type { get; set; }
        public string Name { get; set; } = "";
        public int Level { get; set; }
        public string? PlayerId { get; set; }
        public int Points { get; set; }
        public int Population { get; set; }
        public int Resources { get; set; }
        public int TroopCount { get; set; }
        public int LootResources { get; set; }
    }

    public class ServerMapData
    {
        public List<CellEntityData> Entities { get; set; } = new();
    }

    public class CellEntityData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public CellEntityType Type { get; set; }
        public string Name { get; set; } = "";
        public int Level { get; set; }
        public string? PlayerId { get; set; }
        public int Points { get; set; }
        public int Population { get; set; }
        public int Resources { get; set; }
        public int TroopCount { get; set; }
        public int LootResources { get; set; }
    }

    // ===========================
    //  FORM MAPPA
    // ===========================

    public partial class Mappa : Form
    {
        // --- Costanti mappa ---
        private const int MAP_W = 100;
        private const int MAP_H = 100;
        private const int MIN_CELL = 10;
        private const int MAX_CELL = 80;
        private const int ZOOM_STEP = 3;
        private const int REFRESH_MS = 5000;

        // --- Stato mappa ---
        private MapCell[,] map = new MapCell[MAP_W, MAP_H];
        private int cellSize = 35;
        private Point offset = new(0, 0);

        // --- Drag ---
        private bool isDragging = false;
        private bool dragHappened = false;
        private Point lastMousePos;

        // --- ID giocatore locale ---
        private string _myPlayerId = "player_me";
        public void SetMyPlayerId(string id) => _myPlayerId = id;

        // ===========================
        //  ANIMAZIONE
        // ===========================

        // Timer dedicato alle animazioni (~30 fps)
        private System.Windows.Forms.Timer animTimer = new();
        private const int ANIM_MS = 33;

        // Pulsazione cerchio giallo: fase 0..2π ciclica
        private double _pulsePhase = 0.0;
        private const double PULSE_SPEED = 0.07;

        // Tremore barbari: offset pixel casuali per cella → chiave "x,y"
        private readonly Dictionary<string, (int dx, int dy)> _shakeOffsets = new();
        private readonly Random _shakeRng = new();

        // ===========================
        //  RISORSE GRAFICHE
        // ===========================

        private readonly Dictionary<TerrainType, Color> terrainColors = new()
        {
            { TerrainType.Grass,    Color.FromArgb(45,  150, 45)  },
            { TerrainType.Forest,   Color.FromArgb(0,   100, 0)   },
            { TerrainType.Mountain, Color.FromArgb(130, 125, 125) },
            { TerrainType.Water,    Color.FromArgb(55,   95, 200) }
        };

        private Pen gridPen = new(Color.FromArgb(60, 255, 255, 255), 1);
        private Font coordFont = new("Arial", 7);
        private Font nameFont = new("Arial", 8, FontStyle.Bold);
        private Font labelFont = new("Segoe UI", 9);

        // --- UI overlay ---
        private Label lblCoords = new();
        private Label lblZoom = new();
        private Button btnCenter = new();
        private Button btnRefresh = new();

        // --- Timer auto-refresh server ---
        private System.Windows.Forms.Timer refreshTimer = new();

        // ===========================
        //  COSTRUTTORE
        // ===========================

        public Mappa()
        {
            InitializeComponent();
            EnableDoubleBuffer();
            InitializeMap();
            BuildOverlayUI();
            SetupAutoRefresh();
            SetupAnimTimer();
        }

        // ===========================
        //  DOUBLE BUFFERING
        // ===========================

        private void EnableDoubleBuffer()
        {
            typeof(Panel).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(Panel_Mappa, true);
        }

        // ===========================
        //  TIMER ANIMAZIONE
        // ===========================

        private void SetupAnimTimer()
        {
            animTimer.Interval = ANIM_MS;
            animTimer.Tick += AnimTimer_Tick;
            animTimer.Start();
        }

        private void AnimTimer_Tick(object? sender, EventArgs e)
        {
            // 1) Avanza fase pulsazione
            _pulsePhase += PULSE_SPEED;
            if (_pulsePhase > Math.PI * 2) _pulsePhase -= Math.PI * 2;

            // 2) Aggiorna tremore barbari (solo celle visibili)
            int startX = Math.Max(0, -offset.X / cellSize);
            int startY = Math.Max(0, -offset.Y / cellSize);
            int endX = Math.Min(MAP_W, startX + Panel_Mappa.Width / cellSize + 2);
            int endY = Math.Min(MAP_H, startY + Panel_Mappa.Height / cellSize + 2);

            for (int x = startX; x < endX; x++)
                for (int y = startY; y < endY; y++)
                {
                    var ent = map[x, y].Entity;
                    if (ent is
                        {
                            Type: CellEntityType.BarbarianVillage or
                                       CellEntityType.BarbarianCity
                        })
                    {
                        int range = ent.Type == CellEntityType.BarbarianCity ? 2 : 1;
                        _shakeOffsets[$"{x},{y}"] = (
                            _shakeRng.Next(-range, range + 1),
                            _shakeRng.Next(-range, range + 1)
                        );
                    }
                }

            Panel_Mappa.Invalidate();
        }

        // ===========================
        //  GENERAZIONE MAPPA
        // ===========================

        private void InitializeMap()
        {
            var rng = new Random(42); // seed fisso = mappa sempre uguale
            for (int x = 0; x < MAP_W; x++)
                for (int y = 0; y < MAP_H; y++)
                {
                    double n = rng.NextDouble();
                    TerrainType t = n < 0.10 ? TerrainType.Water
                                  : n < 0.25 ? TerrainType.Forest
                                  : n < 0.35 ? TerrainType.Mountain
                                  : TerrainType.Grass;
                    map[x, y] = new MapCell { X = x, Y = y, Terrain = t };
                }

            GenerateTestData();
        }

        private void GenerateTestData()
        {
            var rng = new Random();

            PlaceEntity(50, 50, new CellEntity
            {
                Type = CellEntityType.PlayerVillage,
                PlayerId = _myPlayerId,
                Name = "Il Mio Villaggio",
                Level = 5,
                Points = 1250,
                Population = 480,
                Resources = 8400
            });

            string[] names = { "LordDrago", "ShadowKing", "IronFist", "NightWolf", "SilverArrow" };
            foreach (var n in names)
            {
                var (px, py) = FindFreeGrassTile(rng);
                PlaceEntity(px, py, new CellEntity
                {
                    Type = CellEntityType.PlayerVillage,
                    PlayerId = $"pid_{n.ToLower()}",
                    Name = n,
                    Level = rng.Next(1, 12),
                    Points = rng.Next(100, 5000),
                    Population = rng.Next(50, 1000),
                    Resources = rng.Next(500, 20000)
                });
            }

            for (int i = 0; i < 30; i++)
            {
                var (bx, by) = FindFreeGrassTile(rng);
                PlaceEntity(bx, by, new CellEntity
                {
                    Type = CellEntityType.BarbarianVillage,
                    Name = $"Villaggio Barbaro {i + 1}",
                    Level = rng.Next(1, 6),
                    TroopCount = rng.Next(10, 200),
                    LootResources = rng.Next(100, 2000)
                });
            }

            for (int i = 0; i < 8; i++)
            {
                var (cx, cy) = FindFreeGrassTile(rng);
                PlaceEntity(cx, cy, new CellEntity
                {
                    Type = CellEntityType.BarbarianCity,
                    Name = $"Città Barbara {i + 1}",
                    Level = rng.Next(6, 15),
                    TroopCount = rng.Next(500, 3000),
                    LootResources = rng.Next(5000, 30000)
                });
            }
        }

        private void PlaceEntity(int x, int y, CellEntity entity)
        {
            if (InBounds(x, y)) map[x, y].Entity = entity;
        }

        private (int x, int y) FindFreeGrassTile(Random rng)
        {
            int attempts = 0;
            while (true)
            {
                int x = rng.Next(MAP_W), y = rng.Next(MAP_H);
                if (map[x, y].Terrain == TerrainType.Grass && map[x, y].Entity == null)
                    return (x, y);
                if (++attempts > 5000) return (rng.Next(MAP_W), rng.Next(MAP_H));
            }
        }

        // ===========================
        //  AUTO-REFRESH SERVER
        // ===========================

        private void SetupAutoRefresh()
        {
            refreshTimer.Interval = REFRESH_MS;
            refreshTimer.Tick += (_, _) => RefreshFromServer();
            refreshTimer.Start();
        }

        private void RefreshFromServer()
        {
            // TODO: chiedi la mappa aggiornata via WatsonTcp
            // es: WatsonClient.Send("REQUEST_MAP|" + _myPlayerId);
            // La risposta chiamerà ApplyServerData(data)
        }

        public void ApplyServerData(ServerMapData data)
        {
            for (int x = 0; x < MAP_W; x++)
                for (int y = 0; y < MAP_H; y++)
                    map[x, y].Entity = null;

            foreach (var d in data.Entities)
            {
                if (!InBounds(d.X, d.Y)) continue;
                map[d.X, d.Y].Entity = new CellEntity
                {
                    Type = d.Type,
                    Name = d.Name,
                    Level = d.Level,
                    PlayerId = d.PlayerId,
                    Points = d.Points,
                    Population = d.Population,
                    Resources = d.Resources,
                    TroopCount = d.TroopCount,
                    LootResources = d.LootResources
                };
            }

            Panel_Mappa.Invalidate();
        }

        // ===========================
        //  OVERLAY UI
        // ===========================

        private void BuildOverlayUI()
        {
            lblCoords = MakeLabel("Coordinate: (-, -)", new Point(10, 10));
            lblZoom = MakeLabel($"Zoom: {cellSize}px", new Point(10, 38));

            btnCenter = MakeButton("🏠 Mio Villaggio", new Point(10, 66),
                Color.FromArgb(200, 60, 120, 180), BtnCenter_Click);

            btnRefresh = MakeButton("🔄 Aggiorna", new Point(10, 100),
                Color.FromArgb(200, 34, 130, 34), (_, _) => RefreshFromServer());

            this.Controls.AddRange(new Control[] { lblCoords, lblZoom, btnCenter, btnRefresh });
            foreach (Control c in new Control[] { lblCoords, lblZoom, btnCenter, btnRefresh })
                c.BringToFront();
        }

        private Label MakeLabel(string text, Point loc) => new()
        {
            Text = text,
            Location = loc,
            AutoSize = true,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(180, 0, 0, 0),
            Padding = new Padding(4),
            Font = labelFont
        };

        private Button MakeButton(string text, Point loc, Color bg, EventHandler handler)
        {
            var btn = new Button
            {
                Text = text,
                Location = loc,
                AutoSize = true,
                BackColor = bg,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = labelFont
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += handler;
            return btn;
        }

        // ===========================
        //  FORM LOAD
        // ===========================

        private void Mappa_Load(object sender, EventArgs e)
        {
            Panel_Mappa.Paint += Panel_Paint;
            Panel_Mappa.MouseDown += Panel_MouseDown;
            Panel_Mappa.MouseMove += Panel_MouseMove;
            Panel_Mappa.MouseUp += Panel_MouseUp;
            Panel_Mappa.MouseWheel += Panel_MouseWheel;
            Panel_Mappa.MouseClick += Panel_MouseClick;
            Panel_Mappa.BackColor = Color.Black;

            var pos = FindMyVillage();
            if (pos.HasValue) CenterOnCell(pos.Value.X, pos.Value.Y);
        }

        // ===========================
        //  DISEGNO PRINCIPALE
        // ===========================

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int startX = Math.Max(0, -offset.X / cellSize);
            int startY = Math.Max(0, -offset.Y / cellSize);
            int endX = Math.Min(MAP_W, startX + Panel_Mappa.Width / cellSize + 2);
            int endY = Math.Min(MAP_H, startY + Panel_Mappa.Height / cellSize + 2);

            for (int x = startX; x < endX; x++)
                for (int y = startY; y < endY; y++)
                    DrawCell(g, map[x, y]);

            DrawGrid(g, startX, startY, endX, endY);

            if (cellSize >= 20)
                DrawCoordinates(g, startX, startY, endX, endY);

            DrawMinimap(g);
        }

        // ===========================
        //  DISEGNO CELLE
        // ===========================

        private void DrawCell(Graphics g, MapCell cell)
        {
            int sx = cell.X * cellSize + offset.X;
            int sy = cell.Y * cellSize + offset.Y;

            using var bg = new SolidBrush(terrainColors[cell.Terrain]);
            g.FillRectangle(bg, sx + 1, sy + 1, cellSize - 1, cellSize - 1);

            if (cell.Entity == null) return;

            switch (cell.Entity.Type)
            {
                case CellEntityType.PlayerVillage:
                    DrawPlayerVillage(g, cell.Entity, sx, sy);
                    break;
                case CellEntityType.BarbarianVillage:
                    DrawBarbarianVillage(g, cell.Entity, sx, sy, cell.X, cell.Y);
                    break;
                case CellEntityType.BarbarianCity:
                    DrawBarbarianCity(g, cell.Entity, sx, sy, cell.X, cell.Y);
                    break;
            }
        }

        // --- Villaggio giocatore ---
        private void DrawPlayerVillage(Graphics g, CellEntity ent, int sx, int sy)
        {
            bool isMe = ent.PlayerId == _myPlayerId;
            int margin = cellSize / 5;
            int size = cellSize - margin * 2;

            if (cellSize >= 16)
            {
                int houseY = sy + margin + size / 3;
                int houseH = size * 2 / 3;

                Color wallColor = isMe ? Color.FromArgb(220, 180, 80) : Color.FromArgb(180, 140, 70);
                Color roofColor = isMe ? Color.FromArgb(200, 50, 50) : Color.FromArgb(100, 80, 80);

                using var wall = new SolidBrush(wallColor);
                g.FillRectangle(wall, sx + margin, houseY, size, houseH);

                Point[] roof =
                {
                    new(sx + margin,          houseY),
                    new(sx + margin + size/2, sy + margin),
                    new(sx + margin + size,   houseY)
                };
                using var roofBrush = new SolidBrush(roofColor);
                g.FillPolygon(roofBrush, roof);
            }
            else
            {
                Color c = isMe ? Color.Gold : Color.SandyBrown;
                using var b = new SolidBrush(c);
                g.FillRectangle(b, sx + 2, sy + 2, cellSize - 4, cellSize - 4);
            }

            // ── Cerchio giallo PULSANTE ──────────────────────────
            if (isMe)
            {
                // sin → 0..1..0 ciclico
                double pulse = (Math.Sin(_pulsePhase) + 1.0) / 2.0;

                float thickness = (float)(1.5 + pulse * 2.5); // 1.5 ÷ 4.0 px
                int alpha = (int)(140 + pulse * 115);   // 140 ÷ 255
                int expand = (int)(pulse * 3);           // 0 ÷ 3 px extra

                using var pen = new Pen(Color.FromArgb(alpha, Color.Yellow), thickness);
                g.DrawEllipse(pen,
                    sx + 1 - expand,
                    sy + 1 - expand,
                    cellSize - 3 + expand * 2,
                    cellSize - 3 + expand * 2);
            }

            if (cellSize >= 30)
            {
                string label = isMe ? "★ " + TruncateName(ent.Name, 7)
                                    : TruncateName(ent.Name, 8);
                DrawCenteredName(g, label, sx, sy,
                    isMe ? Color.Yellow : Color.WhiteSmoke);
            }
        }

        // --- Villaggio barbaro con tremore ---
        private void DrawBarbarianVillage(Graphics g, CellEntity ent, int sx, int sy, int cx, int cy)
        {
            var (dx, dy) = GetShake(cx, cy);
            int margin = cellSize / 5;
            int size = cellSize - margin * 2;

            using var fill = new SolidBrush(Color.FromArgb(120, 90, 50));
            g.FillEllipse(fill, sx + margin + dx, sy + margin + dy, size, size);

            using var border = new Pen(Color.FromArgb(80, 60, 30), 1.5f);
            g.DrawEllipse(border, sx + margin + dx, sy + margin + dy, size, size);

            if (cellSize >= 22)
            {
                using var xPen = new Pen(Color.FromArgb(160, 60, 30), 1.5f);
                int mid = cellSize / 2, arm = cellSize / 6;
                g.DrawLine(xPen, sx + mid - arm + dx, sy + mid - arm + dy,
                                 sx + mid + arm + dx, sy + mid + arm + dy);
                g.DrawLine(xPen, sx + mid + arm + dx, sy + mid - arm + dy,
                                 sx + mid - arm + dx, sy + mid + arm + dy);
            }

            if (cellSize >= 30)
                DrawCenteredName(g, TruncateName(ent.Name, 6), sx + dx, sy + dy, Color.LightGray);
        }

        // --- Città barbara con tremore e merlature ---
        private void DrawBarbarianCity(Graphics g, CellEntity ent, int sx, int sy, int cx, int cy)
        {
            var (dx, dy) = GetShake(cx, cy);
            int margin = 2;
            int size = cellSize - margin * 2;

            using var fill = new SolidBrush(Color.FromArgb(80, 70, 70));
            g.FillRectangle(fill, sx + margin + dx, sy + margin + dy, size, size);

            using var border = new Pen(Color.FromArgb(200, 50, 50), 2f);
            g.DrawRectangle(border, sx + margin + dx, sy + margin + dy, size, size);

            // Merlature (3 blocchi in cima)
            if (cellSize >= 24)
            {
                int mw = Math.Max(3, size / 5);
                int mh = Math.Max(2, size / 6);
                using var merlon = new SolidBrush(Color.FromArgb(100, 90, 90));
                for (int i = 0; i < 3; i++)
                {
                    int mx2 = sx + margin + dx + i * (size / 3) + 1;
                    int my2 = sy + margin + dy - mh;
                    g.FillRectangle(merlon, mx2, my2, mw, mh);
                }
            }

            // Croce rossa pulsante (fase leggermente diversa dal cerchio)
            if (cellSize >= 20)
            {
                double pulse = (Math.Sin(_pulsePhase * 1.3) + 1.0) / 2.0;
                int alpha = (int)(160 + pulse * 95);
                using var cross = new Pen(Color.FromArgb(alpha, 220, 30, 30), 2f);
                int pcx = sx + cellSize / 2 + dx;
                int pcy = sy + cellSize / 2 + dy;
                int arm = cellSize / 4;
                g.DrawLine(cross, pcx - arm, pcy, pcx + arm, pcy);
                g.DrawLine(cross, pcx, pcy - arm, pcx, pcy + arm);
            }

            if (cellSize >= 30)
                DrawCenteredName(g, TruncateName(ent.Name, 6), sx + dx, sy + dy, Color.OrangeRed);
        }

        // ===========================
        //  TREMORE: helper
        // ===========================

        private (int dx, int dy) GetShake(int cx, int cy)
        {
            string key = $"{cx},{cy}";
            return _shakeOffsets.TryGetValue(key, out var v) ? v : (0, 0);
        }

        // ===========================
        //  HELPERS DISEGNO
        // ===========================

        private void DrawCenteredName(Graphics g, string text, int sx, int sy, Color color)
        {
            var sz = g.MeasureString(text, nameFont);
            float tx = sx + (cellSize - sz.Width) / 2f;
            float ty = sy + cellSize - sz.Height - 1;
            g.DrawString(text, nameFont, Brushes.Black, tx + 1, ty + 1);
            using var brush = new SolidBrush(color);
            g.DrawString(text, nameFont, brush, tx, ty);
        }

        private static string TruncateName(string s, int maxLen)
            => s.Length > maxLen ? s[..maxLen] + "." : s;

        private void DrawGrid(Graphics g, int x0, int y0, int x1, int y1)
        {
            int mx0 = offset.X, my0 = offset.Y;
            int mx1 = MAP_W * cellSize + offset.X, my1 = MAP_H * cellSize + offset.Y;

            for (int x = x0; x <= x1; x++)
            {
                int sx = x * cellSize + offset.X;
                if (sx < mx0 || sx > mx1) continue;
                g.DrawLine(gridPen, sx, Math.Max(0, my0), sx, Math.Min(Panel_Mappa.Height, my1));
            }
            for (int y = y0; y <= y1; y++)
            {
                int sy = y * cellSize + offset.Y;
                if (sy < my0 || sy > my1) continue;
                g.DrawLine(gridPen, Math.Max(0, mx0), sy, Math.Min(Panel_Mappa.Width, mx1), sy);
            }
        }

        private void DrawCoordinates(Graphics g, int x0, int y0, int x1, int y1)
        {
            using var brush = new SolidBrush(Color.FromArgb(140, 255, 255, 255));
            for (int x = x0; x < x1; x += 10)
                g.DrawString(x.ToString(), coordFont, brush,
                    x * cellSize + offset.X + 2, 2);
            for (int y = y0; y < y1; y += 10)
                g.DrawString(y.ToString(), coordFont, brush,
                    2, y * cellSize + offset.Y + 2);
        }

        private void DrawMinimap(Graphics g)
        {
            const int mm = 150;
            int mx = Panel_Mappa.Width - mm - 10;
            int my = 10;

            using var bg = new SolidBrush(Color.FromArgb(180, 0, 0, 0));
            g.FillRectangle(bg, mx, my, mm, mm);
            g.DrawRectangle(Pens.Gray, mx, my, mm, mm);

            float sc = (float)mm / MAP_W;

            for (int x = 0; x < MAP_W; x += 2)
                for (int y = 0; y < MAP_H; y += 2)
                {
                    var ent = map[x, y].Entity;
                    if (ent == null) continue;

                    Color dot = ent.Type switch
                    {
                        CellEntityType.PlayerVillage => ent.PlayerId == _myPlayerId
                                                            ? Color.Yellow : Color.Cyan,
                        CellEntityType.BarbarianVillage => Color.SaddleBrown,
                        CellEntityType.BarbarianCity => Color.Red,
                        _ => Color.White
                    };

                    using var b = new SolidBrush(dot);
                    g.FillRectangle(b, mx + x * sc, my + y * sc, sc * 2 + 1, sc * 2 + 1);
                }

            float vx = -offset.X / cellSize * sc;
            float vy = -offset.Y / cellSize * sc;
            float vw = Panel_Mappa.Width / cellSize * sc;
            float vh = Panel_Mappa.Height / cellSize * sc;
            using var vp = new Pen(Color.Red, 1.5f);
            g.DrawRectangle(vp, mx + vx, my + vy, vw, vh);
        }

        // ===========================
        //  MOUSE
        // ===========================

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragHappened = false;
                lastMousePos = e.Location;
                Panel_Mappa.Cursor = Cursors.Hand;
            }
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int dx = e.X - lastMousePos.X;
                int dy = e.Y - lastMousePos.Y;
                if (Math.Abs(dx) > 2 || Math.Abs(dy) > 2) dragHappened = true;

                offset.X += dx;
                offset.Y += dy;
                ClampOffset();
                lastMousePos = e.Location;
                Panel_Mappa.Invalidate();
            }

            int mx = (e.X - offset.X) / cellSize;
            int my = (e.Y - offset.Y) / cellSize;
            if (InBounds(mx, my))
                lblCoords.Text = $"Coordinate: ({mx}, {my})";
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            Panel_Mappa.Cursor = Cursors.Default;
        }

        private void Panel_MouseClick(object sender, MouseEventArgs e)
        {
            if (dragHappened) return;

            int mx = (e.X - offset.X) / cellSize;
            int my = (e.Y - offset.Y) / cellSize;
            if (!InBounds(mx, my)) return;

            // Qualsiasi click (sin/des) → apre finestra comandi
            ApriFinestraComandi(map[mx, my], mx, my);
        }

        private void Panel_MouseWheel(object sender, MouseEventArgs e)
        {
            int old = cellSize;
            cellSize = Math.Clamp(cellSize + (e.Delta > 0 ? ZOOM_STEP : -ZOOM_STEP),
                                  MIN_CELL, MAX_CELL);

            float sc = (float)cellSize / old;
            offset.X = (int)((offset.X - e.X) * sc + e.X);
            offset.Y = (int)((offset.Y - e.Y) * sc + e.Y);

            ClampOffset();
            lblZoom.Text = $"Zoom: {cellSize}px";
            Panel_Mappa.Invalidate();
        }

        // ===========================
        //  FINESTRA COMANDI
        // ===========================

        private void ApriFinestraComandi(MapCell cell, int mx, int my)
        {
            var comandi = CalcolaComandi(cell);
            if (comandi.Count == 0) return;

            var win = new FinestraComandi(cell, mx, my, comandi, _myPlayerId);
            win.OnComandoScelto += (cmd) => EseguiComando(cmd, cell, mx, my);

            // Posiziona vicino al cursore, evitando di uscire dallo schermo
            Point pos = Panel_Mappa.PointToScreen(new Point(
                mx * cellSize + offset.X + cellSize,
                my * cellSize + offset.Y));

            Rectangle screen = Screen.FromControl(this).WorkingArea;
            if (pos.X + 240 > screen.Right) pos.X -= 240 + cellSize;
            if (pos.Y + 380 > screen.Bottom) pos.Y = screen.Bottom - 390;

            win.StartPosition = FormStartPosition.Manual;
            win.Location = pos;
            win.Show(this);
        }

        /// <summary>
        /// Calcola i comandi disponibili in base al contenuto della cella
        /// e alla relazione tra il giocatore locale e l'entità presente.
        /// </summary>
        private List<MapCommand> CalcolaComandi(MapCell cell)
        {
            var list = new List<MapCommand>();

            if (cell.Entity == null)
            {
                // Cella vuota: solo teletrasporto (non su acqua)
                if (cell.Terrain != TerrainType.Water)
                    list.Add(MapCommand.Sposta);
            }
            else
            {
                bool isMe = cell.Entity.PlayerId == _myPlayerId;
                bool isBarbarian = cell.Entity.Type is CellEntityType.BarbarianVillage
                                                     or CellEntityType.BarbarianCity;

                list.Add(MapCommand.InfoCella); // sempre disponibile

                if (isBarbarian)
                {
                    list.Add(MapCommand.Attacca);
                    list.Add(MapCommand.Esplora);
                }
                else if (!isMe) // altro giocatore
                {
                    list.Add(MapCommand.Attacca);
                    list.Add(MapCommand.Commercia);
                    list.Add(MapCommand.Spia);
                    list.Add(MapCommand.Rinforza);
                }
                // Se è il mio villaggio: solo InfoCella
            }

            list.Add(MapCommand.Annulla);
            return list;
        }

        // ===========================
        //  ESECUZIONE COMANDI
        // ===========================

        /// <summary>
        /// Punto centrale di esecuzione comandi.
        /// Ogni case è il punto di aggancio per il messaggio WatsonTcp al server.
        /// </summary>
        public void EseguiComando(MapCommand comando, MapCell cell, int mx, int my)
        {
            switch (comando)
            {
                case MapCommand.Sposta:
                    EseguiSposta(mx, my, cell);
                    break;

                case MapCommand.Attacca:
                    // TODO → WatsonClient.Send($"ATTACCA|{_myPlayerId}|{mx}|{my}");
                    MessageBox.Show(
                        $"Attacco inviato a ({mx},{my}) — {cell.Entity?.Name}\n\n" +
                        "TODO: WatsonTcp → server",
                        "⚔ Attacco", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case MapCommand.Esplora:
                    // TODO → WatsonClient.Send($"ESPLORA|{_myPlayerId}|{mx}|{my}");
                    MessageBox.Show(
                        $"Esplorazione di ({mx},{my}) avviata.\n\nTODO: WatsonTcp → server",
                        "🔍 Esplora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case MapCommand.Commercia:
                    // TODO → WatsonClient.Send($"COMMERCIA|{_myPlayerId}|{cell.Entity?.PlayerId}|{mx}|{my}");
                    MessageBox.Show(
                        $"Proposta commerciale a {cell.Entity?.Name}.\n\nTODO: WatsonTcp → server",
                        "💰 Commercia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case MapCommand.Spia:
                    // TODO → WatsonClient.Send($"SPIA|{_myPlayerId}|{mx}|{my}");
                    MessageBox.Show(
                        $"Spia inviata su {cell.Entity?.Name}.\n\nTODO: WatsonTcp → server",
                        "🕵 Spia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case MapCommand.Rinforza:
                    // TODO → WatsonClient.Send($"RINFORZA|{_myPlayerId}|{cell.Entity?.PlayerId}|{mx}|{my}");
                    MessageBox.Show(
                        $"Rinforzi inviati a {cell.Entity?.Name}.\n\nTODO: WatsonTcp → server",
                        "🛡 Rinforza", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case MapCommand.InfoCella:
                    if (cell.Entity != null) ShowEntityInfo(cell.Entity, mx, my);
                    break;

                case MapCommand.Annulla:
                    break; // la finestra si è già chiusa
            }
        }

        // ===========================
        //  TELETRASPORTO
        // ===========================

        private void EseguiSposta(int mx, int my, MapCell cell)
        {
            if (cell.Terrain == TerrainType.Water)
            {
                MessageBox.Show("Non puoi spostarti sull'acqua!",
                    "Posizione non valida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var myPos = FindMyVillage();
            string curr = myPos.HasValue ? $"({myPos.Value.X}, {myPos.Value.Y})" : "?";

            var res = MessageBox.Show(
                $"Sposta il tuo villaggio a ({mx}, {my})?\n\n" +
                $"Posizione attuale: {curr}\n" +
                $"⚠ Il trasferimento è PERMANENTE.\n" +
                $"(Il server verificherà costo e cooldown.)",
                "🏠 Teletrasporto",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res != DialogResult.Yes) return;

            // TODO → WatsonClient.Send($"TELEPORT|{_myPlayerId}|{mx}|{my}");
            // Simulazione locale temporanea:
            if (myPos.HasValue)
            {
                var entity = map[myPos.Value.X, myPos.Value.Y].Entity!;
                map[myPos.Value.X, myPos.Value.Y].Entity = null;
                map[mx, my].Entity = entity;
                CenterOnCell(mx, my);
            }
        }

        // ===========================
        //  INFO ENTITÀ
        // ===========================

        private void ShowEntityInfo(CellEntity ent, int mx, int my)
        {
            (string title, string body) = ent.Type switch
            {
                CellEntityType.PlayerVillage => (
                    ent.PlayerId == _myPlayerId ? "⭐ Il Tuo Villaggio" : $"🏘 {ent.Name}",
                    $"Coordinate: ({mx},{my})\nLivello: {ent.Level}\n" +
                    $"Punti: {ent.Points:N0}\nPopolazione: {ent.Population:N0}\n" +
                    $"Risorse: {ent.Resources:N0}"),

                CellEntityType.BarbarianVillage => (
                    "⚔ Villaggio Barbaro",
                    $"{ent.Name}\nCoordinate: ({mx},{my})\nLivello: {ent.Level}\n" +
                    $"Truppe: {ent.TroopCount:N0}\nBottino: {ent.LootResources:N0}"),

                CellEntityType.BarbarianCity => (
                    "🔴 Città Barbara",
                    $"{ent.Name}\nCoordinate: ({mx},{my})\nLivello: {ent.Level} ⚠\n" +
                    $"Truppe: {ent.TroopCount:N0}\nBottino: {ent.LootResources:N0}"),

                _ => ("Entità", $"Coordinate: ({mx},{my})")
            };

            MessageBox.Show(body, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ===========================
        //  BOTTONI UI
        // ===========================

        private void BtnCenter_Click(object sender, EventArgs e)
        {
            var pos = FindMyVillage();
            if (pos.HasValue) CenterOnCell(pos.Value.X, pos.Value.Y);
            else MessageBox.Show("Villaggio non trovato.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // ===========================
        //  UTILITY
        // ===========================

        private void ClampOffset()
        {
            int minX = Panel_Mappa.Width - MAP_W * cellSize;
            int minY = Panel_Mappa.Height - MAP_H * cellSize;
            offset.X = Math.Clamp(offset.X, minX, 0);
            offset.Y = Math.Clamp(offset.Y, minY, 0);
        }

        private void CenterOnCell(int x, int y)
        {
            offset.X = Panel_Mappa.Width / 2 - x * cellSize - cellSize / 2;
            offset.Y = Panel_Mappa.Height / 2 - y * cellSize - cellSize / 2;
            ClampOffset();
            Panel_Mappa.Invalidate();
        }

        public Point? FindMyVillage()
        {
            for (int x = 0; x < MAP_W; x++)
                for (int y = 0; y < MAP_H; y++)
                    if (map[x, y].Entity?.PlayerId == _myPlayerId)
                        return new Point(x, y);
            return null;
        }

        private bool InBounds(int x, int y)
            => x >= 0 && x < MAP_W && y >= 0 && y < MAP_H;
    }

    // ============================================================
    //  FORM COMANDI — finestra separata che appare al click cella
    // ============================================================

    /// <summary>
    /// Finestra popup scura con i comandi disponibili per la cella cliccata.
    /// Si chiude automaticamente quando perde il focus (Deactivate).
    /// Lancia l'evento OnComandoScelto con il comando selezionato.
    /// </summary>
    public class FinestraComandi : Form
    {
        public event Action<MapCommand>? OnComandoScelto;

        // Icona + colore hover + etichetta per ogni comando
        private static readonly Dictionary<MapCommand, (string icona, Color colore, string label)>
            _info = new()
        {
            { MapCommand.InfoCella,  ("📋", Color.FromArgb(60,  120, 200), "Informazioni")   },
            { MapCommand.Sposta,     ("🏠", Color.FromArgb(60,  150,  60), "Teletrasporto") },
            { MapCommand.Attacca,    ("⚔",  Color.FromArgb(180,  50,  50), "Attacca")       },
            { MapCommand.Esplora,    ("🔍", Color.FromArgb(150, 100,  30), "Esplora")       },
            { MapCommand.Commercia,  ("💰", Color.FromArgb(160, 130,  20), "Commercia")     },
            { MapCommand.Spia,       ("🕵", Color.FromArgb( 80,  80, 130), "Invia Spia")    },
            { MapCommand.Rinforza,   ("🛡", Color.FromArgb( 40, 100, 140), "Rinforza")      },
            { MapCommand.Annulla,    ("✖",  Color.FromArgb( 80,  80,  80), "Annulla")       },
        };

        public FinestraComandi(MapCell cell, int mx, int my,
                               List<MapCommand> comandi, string myPlayerId)
        {
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.FromArgb(22, 22, 32);
            Opacity = 0.96;
            ShowInTaskbar = false;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Padding = new Padding(10);

            // Chiusura automatica quando si clicca altrove
            Deactivate += (_, _) => Close();

            var stack = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0)
            };

            // ── Intestazione ─────────────────────────────────
            string title = cell.Entity != null
                ? $"{cell.Entity.Name}"
                : "Cella vuota";

            stack.Controls.Add(new Label
            {
                Text = $"{title}  ({mx},{my})",
                ForeColor = Color.FromArgb(210, 210, 230),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(2, 2, 2, 4)
            });

            // Sottotitolo tipo entità
            if (cell.Entity != null)
            {
                string sub = cell.Entity.Type switch
                {
                    CellEntityType.PlayerVillage =>
                        cell.Entity.PlayerId == myPlayerId
                            ? "⭐ Il tuo villaggio"
                            : $"👤 Giocatore · Lv.{cell.Entity.Level}",
                    CellEntityType.BarbarianVillage =>
                        $"⚔ Villaggio Barbaro · Lv.{cell.Entity.Level}",
                    CellEntityType.BarbarianCity =>
                        $"🔴 Città Barbara · Lv.{cell.Entity.Level}",
                    _ => ""
                };
                stack.Controls.Add(new Label
                {
                    Text = sub,
                    ForeColor = Color.FromArgb(140, 140, 170),
                    Font = new Font("Segoe UI", 8),
                    AutoSize = true,
                    Padding = new Padding(12, 0, 2, 4)
                });
            }

            // Separatore
            stack.Controls.Add(new Panel
            {
                Height = 1,
                Width = 224,
                BackColor = Color.FromArgb(55, 55, 75),
                Margin = new Padding(12, 0, 0, 4)
            });

            // ── Bottoni comandi ──────────────────────────────
            foreach (var cmd in comandi)
            {
                if (!_info.TryGetValue(cmd, out var inf)) continue;

                var btn = new Button
                {
                    Text = $"  {inf.icona}  {inf.label}",
                    Width = 224,
                    Height = 34,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(35, 35, 50),
                    ForeColor = Color.FromArgb(215, 215, 230),
                    Font = new Font("Segoe UI", 9),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Cursor = Cursors.Hand,
                    Margin = new Padding(12, 1, 0, 1), // sinistra, sopra, destra, sotto
                    Tag = cmd
                };
                btn.FlatAppearance.BorderColor = Color.FromArgb(55, 55, 75);
                btn.FlatAppearance.BorderSize = 1;

                // Hover colorato per ogni comando
                Color hoverBg = Color.FromArgb(70,
                    inf.colore.R, inf.colore.G, inf.colore.B);

                btn.MouseEnter += (_, _) =>
                {
                    btn.BackColor = hoverBg;
                    btn.ForeColor = Color.White;
                };
                btn.MouseLeave += (_, _) =>
                {
                    btn.BackColor = Color.FromArgb(35, 35, 50);
                    btn.ForeColor = Color.FromArgb(215, 215, 230);
                };

                btn.Click += (_, _) =>
                {
                    Close();
                    OnComandoScelto?.Invoke((MapCommand)btn.Tag!);
                };

                stack.Controls.Add(btn);
            }

            Controls.Add(stack);
        }
    }
}