using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace MissFortune
{
    // I can't really help you with my layout of a good config class
    // since everyone does it the way they like it most, go checkout my
    // config classes I make on my GitHub if you wanna take over the
    // complex way that I use
    public static class Config
    {
        private const string MenuName = "MissFortune";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to MissFortune by TopGunner");

            // Initialize the modes
            Modes.Initialize();
        }

        public static void Initialize()
        {
        }


        public static class Misc
        {

            private static readonly Menu Menu;
            public static readonly CheckBox _drawQ;
            public static readonly CheckBox _drawW;
            public static readonly CheckBox _drawE;
            public static readonly CheckBox _drawR;
            public static readonly CheckBox _drawCombo;
            private static readonly CheckBox _ksQ;
            private static readonly CheckBox _unkillableQ;
            private static readonly CheckBox _useLoveTaps;
            private static readonly CheckBox _useHeal;
            private static readonly CheckBox _useQSS;
            private static readonly CheckBox _autoBuyStartingItems;
            private static readonly CheckBox _autolevelskills;
            private static readonly Slider _skinId;
            public static readonly CheckBox _useSkinHack;
            private static readonly CheckBox[] _useHealOn = { new CheckBox("", false), new CheckBox("", false), new CheckBox("", false), new CheckBox("", false), new CheckBox("", false) };
            private static readonly CheckBox[] _useQOn = { new CheckBox(""), new CheckBox(""), new CheckBox(""), new CheckBox(""), new CheckBox("") };

            public static bool UseQOnI(int i)
            {
                return _useQOn[i].CurrentValue;
            }
            public static bool useHealOnI(int i)
            {
                return _useHealOn[i].CurrentValue;
            }
            public static bool ksQ
            {
                get { return _ksQ.CurrentValue; }
            }
            public static bool useQFarm
            {
                get { return _unkillableQ.CurrentValue; }
            }
            public static bool useLoveTaps
            {
                get { return _useLoveTaps.CurrentValue; }
            }
            public static bool useHeal
            {
                get { return _useHeal.CurrentValue; }
            }
            public static bool useQSS
            {
                get { return _useQSS.CurrentValue; }
            }
            public static bool autoBuyStartingItems
            {
                get { return _autoBuyStartingItems.CurrentValue; }
            }
            public static bool autolevelskills
            {
                get { return _autolevelskills.CurrentValue; }
            }
            public static int skinId
            {
                get { return _skinId.CurrentValue; }
            }
            public static bool UseSkinHack
            {
                get { return _useSkinHack.CurrentValue; }
            }
            public static bool drawComboDmg
            {
                get { return _drawCombo.CurrentValue; }
            }


            static Misc()
            {
                // Initialize the menu values
                Menu = Config.Menu.AddSubMenu("Misc");
                _drawQ = Menu.Add("drawQ", new CheckBox("Göster Q"));
                _drawW = Menu.Add("drawW", new CheckBox("Göster W"));
                _drawE = Menu.Add("drawE", new CheckBox("Göster E"));
                _drawR = Menu.Add("drawR", new CheckBox("Göster R"));
                Menu.AddSeparator();
                _useLoveTaps = Menu.Add("LoveTaps", new CheckBox("Kullan Love Taps"));
                Menu.AddSeparator();
                _ksQ = Menu.Add("ksQ", new CheckBox("Ksde akıllıca Q Kullan"));
                _unkillableQ = Menu.Add("unkillableQ", new CheckBox("Q kullan öldürelemeyecek minyonlarda"));
                Menu.AddSeparator();
                _useHeal = Menu.Add("useHeal", new CheckBox("Akıllı can bas"));
                _useQSS = Menu.Add("useQSS", new CheckBox("Kullan QSS"));
                Menu.AddSeparator();
                for (int i = 0; i < EntityManager.Heroes.Allies.Count; i++)
                {
                    _useHealOn[i] = Menu.Add("useHeal" + i, new CheckBox("Can Kullan " + EntityManager.Heroes.Allies[i].ChampionName));
                }
                Menu.AddSeparator();
                for (int i = 0; i < EntityManager.Heroes.Enemies.Count; i++)
                {
                    _useQOn[i] = Menu.Add("useQ" + i, new CheckBox("Q Kullan" + EntityManager.Heroes.Enemies[i].ChampionName));
                }
                Menu.AddSeparator();
                _autolevelskills = Menu.Add("autolevelskills", new CheckBox("Otomatik level yükseltme"));
                _autoBuyStartingItems = Menu.Add("autoBuyStartingItems", new CheckBox("Başlangıçta item alma"));
                Menu.AddSeparator();
                _useSkinHack = Menu.Add("useSkinHack", new CheckBox("Skin değiştiric kullan"));
                _skinId = Menu.Add("skinId", new Slider("Skin Numarası", 7, 1, 9));
            }

            public static void Initialize()
            {
            }

        }

        public static class Modes
        {
            private static readonly Menu Menu;

            static Modes()
            {
                // Initialize the menu
                Menu = Config.Menu.AddSubMenu("Modes");

                // Initialize all modes
                // Combo
                Combo.Initialize();
                Menu.AddSeparator();

                // Harass
                Harass.Initialize();
                LaneClear.Initialize();
                JungleClear.Initialize();
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
                private static readonly CheckBox _useQChampsOnly;
                private static readonly CheckBox _useRHotkey;
                private static readonly CheckBox _useBOTRK;
                private static readonly CheckBox _useYOUMOUS;
                private static readonly CheckBox _saveRforStunned;
                private static readonly CheckBox _alwaysROnStunned;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool useQChampsOnly
                {
                    get { return _useQChampsOnly.CurrentValue; }
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
                public static bool UseRHotkey
                {
                    get { return _useRHotkey.CurrentValue; }
                }
                public static bool useBOTRK
                {
                    get { return _useBOTRK.CurrentValue; }
                }
                public static bool useYOUMOUS
                {
                    get { return _useYOUMOUS.CurrentValue; }
                }
                public static int ROnEnemies
                {
                    get { return Menu["comboROnEnemies"].Cast<Slider>().CurrentValue; }
                }
                public static int REnemiesMaxHP
                {
                    get { return Menu["REnemiesMaxHP"].Cast<Slider>().CurrentValue; }
                }
                public static bool saveRforStunned
                {
                    get { return _saveRforStunned.CurrentValue; }
                }
                public static bool alwaysROnStunned
                {
                    get { return _alwaysROnStunned.CurrentValue; }
                }
                public static bool RPressed
                {
                    get { return Menu["RHotkey"].Cast<KeyBind>().CurrentValue; }
                }
                public static void resetRKey()
                {
                    Menu["RHotkey"].Cast<KeyBind>().CurrentValue = false;
                }

                static Combo()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("Combo");
                    _useQ = Menu.Add("comboUseQ", new CheckBox("Kullan Q"));
                    _useQChampsOnly = Menu.Add("comboUseQChampsOnly", new CheckBox("Q sadece hedefe kullan"));
                    _useW = Menu.Add("comboUseW", new CheckBox("Kullan Akıllı W"));
                    _useE = Menu.Add("comboUseE", new CheckBox("Kullan E"));
                    _useR = Menu.Add("comboUseR", new CheckBox("Kullan R"));
                    _useRHotkey = Menu.Add("comboUseRHotkey", new CheckBox("R kullanma tuşu", false));
                    Menu.Add("RHotkey", new KeyBind("Tickini kaldırmayın!", false, KeyBind.BindTypes.HoldActive, 'R'));
                    Menu.Add("comboROnEnemies", new Slider("R için en az düşman", 2, 1, 5));
                    Menu.Add("REnemiesMaxHP", new Slider("1 düşmana ulti için hedefin canı  ({0}%)HP", 100, 1, 100));
                    Menu.AddSeparator();
                    _saveRforStunned = Menu.Add("saveRforStunned", new CheckBox("R yi sadece hedef sabitse kullan"));
                    Menu.AddSeparator();
                    _alwaysROnStunned = Menu.Add("alwaysROnStunned", new CheckBox("Ryi tek hedefe ise her zaman sabitse kullan"));
                    Menu.AddSeparator();
                    _useBOTRK = Menu.Add("useBotrk", new CheckBox("Mahvolmuş Kullan"));
                    _useYOUMOUS = Menu.Add("useYoumous", new CheckBox("Kullan Youmous"));
                }


                public static void Initialize()
                {
                }

            }

            public static class Harass
            {
                public static bool UseQ
                {
                    get { return Menu["harassUseQ"].Cast<CheckBox>().CurrentValue; }
                }
                public static bool useQMinionKillOnly
                {
                    get { return Menu["harassUseQKillingBlowOnly"].Cast<CheckBox>().CurrentValue; }
                }
                public static bool UseW
                {
                    get { return Menu["harassUseW"].Cast<CheckBox>().CurrentValue; }
                }
                public static bool UseE
                {
                    get { return Menu["harassUseE"].Cast<CheckBox>().CurrentValue; }
                }
                public static bool UseR
                {
                    get { return Menu["harassUseR"].Cast<CheckBox>().CurrentValue; }
                }
                public static int Mana
                {
                    get { return Menu["harassMana"].Cast<Slider>().CurrentValue; }
                }

                static Harass()
                {
                    // Here is another option on how to use the menu, but I prefer the
                    // way that I used in the combo class
                    Menu.AddGroupLabel("Harass");
                    Menu.Add("harassUseQ", new CheckBox("Kullan Q"));
                    Menu.Add("harassUseQKillingBlowOnly", new CheckBox("Q kullan sadece hedefe çarpması için", false));
                    Menu.Add("harassUseW", new CheckBox("Kullan Akıllı W"));
                    Menu.Add("harassUseE", new CheckBox("Kullan E", false)); // Default false

                    // Adding a slider, we have a little more options with them, using {0} {1} and {2}
                    // in the display name will replace it with 0=current 1=min and 2=max value
                    Menu.Add("harassMana", new Slider("Gereken mana ({0}%)", 40));
                }
                public static void Initialize()
                {
                }

            }

            public static class LaneClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useQHarass;
                private static readonly CheckBox _useW;
                private static readonly Slider _mana;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseQHarass
                {
                    get { return _useQHarass.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static int mana
                {
                    get { return _mana.CurrentValue; }
                }

                static LaneClear()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("Lane Clear");
                    _useQ = Menu.Add("clearUseQ", new CheckBox("Kullan Q"));
                    _useQHarass = Menu.Add("clearUseQHarass", new CheckBox("Şampiyona çarpacaksa Q kullan"));
                    _useW = Menu.Add("clearUseW", new CheckBox("Kullan W"));
                    _mana = Menu.Add("clearMana", new Slider("Gereken mana ({0}%)", 40));
                }

                public static void Initialize()
                {
                }
            }
            public static class JungleClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly Slider _mana;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static int mana
                {
                    get { return _mana.CurrentValue; }
                }

                static JungleClear()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("Jungle Clear");
                    _useQ = Menu.Add("jglUseQ", new CheckBox("Kullan Q"));
                    _useW = Menu.Add("jglUseW", new CheckBox("Kullan W"));
                    _mana = Menu.Add("jglMana", new Slider("Gereken mana ({0}%)", 40));
                }

                public static void Initialize()
                {
                }
            }
        }
    }
}
