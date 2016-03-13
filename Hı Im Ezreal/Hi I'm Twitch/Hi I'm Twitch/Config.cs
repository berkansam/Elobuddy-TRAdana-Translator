using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace AddonTemplate
{
    public static class Config
    {
        private const string MenuName = "AddonTemplate";

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Hi I'm Twitch addonuna hoşgeldin!");
            Menu.AddLabel("Tarafından GinjiBan");
            Menu.AddLabel("Ceviri-tradana");

            Modes.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu MenuCombo;
            private static readonly Menu MenuHarass;
            private static readonly Menu MenuDraw;

            static Modes()
            {
                MenuCombo = Config.Menu.AddSubMenu("Combo");
                MenuHarass = Config.Menu.AddSubMenu("Harass");
                MenuDraw = Config.Menu.AddSubMenu("Visual");

                Combo.Initialize();
                Harass.Initialize();
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
                    MenuCombo.AddGroupLabel("Kombo");
                    _useQ = MenuCombo.Add("comboUseQ", new CheckBox("Kullan Q"));
                    _useW = MenuCombo.Add("comboUseW", new CheckBox("Kullan W"));
                    _useE = MenuCombo.Add("comboUseE", new CheckBox("Kullan E"));
                    _useR = MenuCombo.Add("comboUseR", new CheckBox("Kullan R", false));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
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

                static Harass()
                {
                    MenuHarass.AddGroupLabel("Dürtme");
                    _useQ = MenuHarass.Add("harassUseQ", new CheckBox("Kullan Q"));
                    _useW = MenuHarass.Add("harassUseW", new CheckBox("Kullan W"));
                    _useE = MenuHarass.Add("harassUseE", new CheckBox("Kullan E"));
                    _useR = MenuHarass.Add("harassUseR", new CheckBox("Kullan R", false)); // Default false
                }

                public static void Initialize()
                {
                }
            }
            public static class Draw
            {
                private static readonly CheckBox _dmgIndicator;
                private static readonly CheckBox _sleathDistance;
                private static readonly CheckBox _miniMapSleathDistance;

                public static bool DamageIndicator
                {
                    get { return _dmgIndicator.CurrentValue; }
                }
                public static bool StealthDistance
                {
                    get { return _sleathDistance.CurrentValue; }
                }
                public static bool MinimapStealthDistance
                {
                    get { return _miniMapSleathDistance.CurrentValue; }
                }

                static Draw()
                {
                    MenuDraw.AddGroupLabel("Görsel");
                    _dmgIndicator = MenuDraw.Add("damageIndicator", new CheckBox("Hasar Tespiti"));
                    _sleathDistance = MenuDraw.Add("stealthdistance", new CheckBox("Gizlilik mesafesi"));
                    _miniMapSleathDistance = MenuDraw.Add("minimapstealthdistance", new CheckBox("Haritada gizlilik mesafesi"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Misc
            {
                private static readonly CheckBox _eBigMinion;
                private static readonly CheckBox _eBaronDragon;

                public static bool EBigMinion
                {
                    get { return _eBigMinion.CurrentValue; }
                }

                public static bool EBaronDragon
                {
                    get { return _eBaronDragon.CurrentValue; }
                }

                static Misc()
                {
                    MenuDraw.AddGroupLabel("Ek");
                    _eBigMinion = MenuDraw.Add("ebigminion", new CheckBox("Büyük minyona E"));
                    _eBaronDragon = MenuDraw.Add("ebarondragon", new CheckBox("Barona Ejdere E"));

                }

                public static void Initialize()
                {
                }
            }
        }
    }
}
