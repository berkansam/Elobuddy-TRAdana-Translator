using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using Color = System.Drawing.Color;

// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace KA_Akali
{
    public static class Config
    {
        private static readonly string MenuName = "KA " + Player.Instance.ChampionName;

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("KA " + Player.Instance.ChampionName);
            Modes.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu SpellsMenu, FarmMenu, MiscMenu, DrawMenu;

            static Modes()
            {
                SpellsMenu = Menu.AddSubMenu("::Büyü Menüleri::");
                Combo.Initialize();
                Harass.Initialize();

                FarmMenu = Menu.AddSubMenu("::FarmMenu::");
                LaneClear.Initialize();
                LastHit.Initialize();

                MiscMenu = Menu.AddSubMenu("::Ek::");
                Misc.Initialize();

                DrawMenu = Menu.AddSubMenu("::Göstergeler::");
                Draw.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useR;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                static Combo()
                {
                    // Initialize the menu values
                    SpellsMenu.AddGroupLabel("Kombo Büyüleri:");
                    _useQ = SpellsMenu.Add("comboQ", new CheckBox("Q Kullan ?"));
                    _useW = SpellsMenu.Add("comboW", new CheckBox("W Kullan ?"));
                    _useE = SpellsMenu.Add("comboE", new CheckBox("E Kullan ?"));
                    _useR = SpellsMenu.Add("comboR", new CheckBox("R Kullan ?"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }


                static Harass()
                {
                    SpellsMenu.AddGroupLabel("Dürtme Büyüleri:");
                    _useQ = SpellsMenu.Add("harassQ", new CheckBox("Q Kullan ?"));
                    _useE = SpellsMenu.Add("harassE", new CheckBox("E Kullan ?"));
                }

                public static void Initialize()
                {
                }
            }

            public static class LaneClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                static LaneClear()
                {
                    FarmMenu.AddGroupLabel("Lanetemizleme Büyüleri:");
                    _useQ = FarmMenu.Add("laneclearQ", new CheckBox("Q Kullan ?"));
                    _useE = FarmMenu.Add("laneclearE", new CheckBox("E Kullan ?"));
                }

                public static void Initialize()
                {
                }
            }

            public static class LastHit
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                static LastHit()
                {
                    FarmMenu.AddGroupLabel("Son Vuruş Büyüleri:");
                    _useQ = FarmMenu.Add("lasthitQ", new CheckBox("Q Kullan ?"));
                    _useE = FarmMenu.Add("lasthitE", new CheckBox("E Kullan ?"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Misc
            {
                private static readonly CheckBox _antiGapCloserSpell;

                public static bool AntiGapCloser
                {
                    get { return _antiGapCloserSpell.CurrentValue; }
                }

                static Misc()
                {
                    // Initialize the menu values
                    MiscMenu.AddGroupLabel("Ek");
                    _antiGapCloserSpell = MiscMenu.Add("gapcloserX", new CheckBox("Antigapcloser Büyüsü W ?"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Draw
            {
                private static readonly CheckBox _drawReady;
                private static readonly CheckBox _drawHealth;
                private static readonly CheckBox _drawPercent;
                private static readonly CheckBox _drawQ;
                private static readonly CheckBox _drawW;
                private static readonly CheckBox _drawE;
                private static readonly CheckBox _drawR;
                //Color Config
                private static readonly ColorConfig _qColor;
                private static readonly ColorConfig _wColor;
                private static readonly ColorConfig _eColor;
                private static readonly ColorConfig _rColor;
                private static readonly ColorConfig _healthColor;

                //CheckBoxes
                public static bool DrawReady
                {
                    get { return _drawReady.CurrentValue; }
                }

                public static bool DrawHealth
                {
                    get { return _drawHealth.CurrentValue; }
                }

                public static bool DrawPercent
                {
                    get { return _drawPercent.CurrentValue; }
                }

                public static bool DrawQ
                {
                    get { return _drawQ.CurrentValue; }
                }

                public static bool DrawW
                {
                    get { return _drawW.CurrentValue; }
                }

                public static bool DrawE
                {
                    get { return _drawE.CurrentValue; }
                }

                public static bool DrawR
                {
                    get { return _drawR.CurrentValue; }
                }
                //Colors
                public static Color HealthColor
                {
                    get { return _healthColor.GetSystemColor(); }
                }

                public static SharpDX.Color QColor
                {
                    get { return _qColor.GetSharpColor(); }
                }

                public static SharpDX.Color WColor
                {
                    get { return _wColor.GetSharpColor(); }
                }

                public static SharpDX.Color EColor
                {
                    get { return _eColor.GetSharpColor(); }
                }
                public static SharpDX.Color RColor
                {
                    get { return _rColor.GetSharpColor(); }
                }

                static Draw()
                {
                    DrawMenu.AddGroupLabel("Göstergeler :");
                    _drawReady = DrawMenu.Add("drawOnlyWhenReady", new CheckBox("Büyüler Hazırsa Göster ?"));
                    _drawHealth = DrawMenu.Add("damageIndicatorDraw", new CheckBox("Hasar tespitini göster ?"));
                    _drawPercent = DrawMenu.Add("percentageIndicatorDraw", new CheckBox("Hasarı Yüzde olarak göster ?"));
                    DrawMenu.AddSeparator(1);
                    _drawQ = DrawMenu.Add("qDraw", new CheckBox("Göster Q  Menzili ?"));
                    _drawW = DrawMenu.Add("wDraw", new CheckBox("Göster W  Menzili ?"));
                    _drawE = DrawMenu.Add("eDraw", new CheckBox("Göster E  Menzili ?"));
                    _drawR = DrawMenu.Add("rDraw", new CheckBox("Göster R  Menzili ?"));

                    _healthColor = new ColorConfig(DrawMenu, "healthColorConfig", Color.Orange, "Renkli HASAR TESPİTİ:");
                    _qColor = new ColorConfig(DrawMenu, "qColorConfig", Color.Blue, "Renk Q:");
                    _wColor = new ColorConfig(DrawMenu, "wColorConfig", Color.Red, "Renk W:");
                    _eColor = new ColorConfig(DrawMenu, "eColorConfig", Color.DeepPink, "Renk E:");
                    _rColor = new ColorConfig(DrawMenu, "rColorConfig", Color.Yellow, "Renk R:");
                }

                public static void Initialize()
                {
                }
            }
        }
    }
}