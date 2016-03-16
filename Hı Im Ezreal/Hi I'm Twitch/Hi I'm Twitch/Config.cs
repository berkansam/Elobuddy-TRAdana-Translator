using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace AddonTemplate
{
    public static class Config
    {
        private const string MenuName = "Hi I'm Twitch";
        public static Item Youmuu = new Item(ItemId.Youmuus_Ghostblade);
        public static Item Botrk = new Item(ItemId.Blade_of_the_Ruined_King);
        public static Item Cutlass = new Item(ItemId.Bilgewater_Cutlass);

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
            private static readonly Menu MenuKillSteal;
            private static readonly Menu MenuClear;

            static Modes()
            {
                MenuCombo = Config.Menu.AddSubMenu("Combo");
                MenuHarass = Config.Menu.AddSubMenu("Harass");
                MenuDraw = Config.Menu.AddSubMenu("Visual");
                MenuKillSteal = Config.Menu.AddSubMenu("Contaminate usage");
                MenuClear = Config.Menu.AddSubMenu("Clear");

                Combo.Initialize();
                Harass.Initialize();
                Draw.Initialize();
                KillSteal.Initialize();
                Clear.Initialize();
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
                private static readonly Slider _minHPBotrk;
                private static readonly Slider _enemyMinHPBotrk;
                private static readonly CheckBox _useYoumuu;
                private static readonly CheckBox _useBotrk;


                public static bool UseYoumuu
                {
                    get { return _useYoumuu.CurrentValue; }
                }
                public static bool useBotrk
                {
                    get { return _useBotrk.CurrentValue; }
                }
                public static int MinHPBotrk
                {
                    get { return _minHPBotrk.CurrentValue; }
                }
                public static int EnemyMinHPBotrk
                {
                    get { return _enemyMinHPBotrk.CurrentValue; }
                }


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
                    MenuCombo.AddGroupLabel("Combo");
                    _useQ = MenuCombo.Add("comboUseQ", new CheckBox("Kullan Q"));
                    _useW = MenuCombo.Add("comboUseW", new CheckBox("Kullan W"));
                    _useE = MenuCombo.Add("comboUseE", new CheckBox("Kullan E"));
                    _useR = MenuCombo.Add("comboUseR", new CheckBox("Kullan R", false));
                    _useYoumuu = MenuCombo.Add("useYoumuu", new CheckBox("Yuumo kullan"));
                    _useBotrk = MenuCombo.Add("useBotrk", new CheckBox("Mahvolmuş Kılıcı Kullan"));
                    _minHPBotrk = MenuCombo.Add("minHPBotrk", new Slider("Canım ({0}%)", 80));
                    _enemyMinHPBotrk = MenuCombo.Add("enemyMinHPBotrk", new Slider("Mhavolmuş için düşmanın canı ({0}%)", 80));
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
                    MenuDraw.AddSeparator();
                    MenuDraw.AddLabel("HP Bar");
                    _dmgIndicator = MenuDraw.Add("damageIndicator", new CheckBox("Hasar Tespiti"));
                    MenuDraw.AddSeparator();
                    MenuDraw.AddLabel("Stealth");
                    _sleathDistance = MenuDraw.Add("stealthdistance", new CheckBox("Gizlilik mesafesi"));
                    _miniMapSleathDistance = MenuDraw.Add("minimapstealthdistance", new CheckBox("Haritada gizlilik mesafesi"));
                }

                public static void Initialize()
                {
                }
            }

            public static class KillSteal
            {
                private static readonly CheckBox _eChamp;
                private static readonly CheckBox _eFullStacks;
                private static readonly Slider _eNumberStacks;
                private static readonly CheckBox _eOutOfRange;
                private static readonly Slider _eOutOfRangeStacks;
                private static readonly CheckBox _eDying;


                public static bool KsE
                {
                    get { return _eChamp.CurrentValue; }
                }
                public static bool EFullStacks
                {
                    get { return _eFullStacks.CurrentValue; }
                }
                public static bool EOutOfRange
                {
                    get { return _eOutOfRange.CurrentValue; }
                }
                public static bool EDying
                {
                    get { return _eDying.CurrentValue; }
                }
                public static int ENumberstacks
                {
                    get { return _eNumberStacks.CurrentValue; }
                }

                public static int EOutOfRangeStacks
                {
                    get { return _eOutOfRangeStacks.CurrentValue; }
                }


                static KillSteal()
                {
                    MenuKillSteal.AddGroupLabel("Contaminate usage");
                    _eChamp = MenuKillSteal.Add("echamp", new CheckBox("E yi korumak için / KS şampiyonlara"));
                    MenuKillSteal.AddSeparator();
                    _eFullStacks = MenuKillSteal.Add("efullstacks", new CheckBox("Eğer hedefte x stack varsa E kullan", false));
                    _eNumberStacks = MenuKillSteal.Add("enumberstacks", new Slider("Otomatik E için stacks ({0})", 6, 1, 6));
                    MenuKillSteal.AddSeparator();
                    _eOutOfRange = MenuKillSteal.Add("eoutofrange", new CheckBox("Eğer düşman menzilden çıkarsa E kullan", false));
                    _eOutOfRangeStacks = MenuKillSteal.Add("eoutofrangestacks", new Slider("Menzil dışında e çekmek için kaç Stack olsun ({0})", 6, 1, 6));
                    MenuKillSteal.AddSeparator();
                    _eDying = MenuKillSteal.Add("edying", new CheckBox("Eğer ölecekse E kullan"));
                }

                public static void Initialize()
                {
                }
        }

        public static class Clear
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



            static Clear()
            {
                MenuClear.AddGroupLabel("Clear");
                _eBaronDragon = MenuClear.Add("ebarondragon", new CheckBox("Barona Ejdere E"));
                _eBigMinion = MenuClear.Add("ebigminion", new CheckBox("Büyük minyonlara E kullan"));
            }

            public static void Initialize()
            {
            }
        }
    }
}
}
