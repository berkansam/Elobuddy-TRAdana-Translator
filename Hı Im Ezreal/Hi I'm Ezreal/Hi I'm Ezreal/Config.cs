using System.Linq;
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
        public static Item Muramana = new Item(3042); // Muramana itemId is lacking in the Item DB
        public static Item Youmuu = new Item(ItemId.Youmuus_Ghostblade);
        public static Item Botrk = new Item(ItemId.Blade_of_the_Ruined_King);
        public static Item Cutlass = new Item(ItemId.Bilgewater_Cutlass);
        public static Item Tear = new Item(ItemId.Tear_of_the_Goddess);
        public static Item Manamume = new Item(ItemId.Manamune);

        public static float LastComboPressed = 0;

        private const string MenuName = "Hi I'm Ezreal";

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Hi I'm Ezreal addonuna Hoşgeldin!");
            Menu.AddLabel("GinjiBan Tarafından");
            Menu.AddLabel("Çeviri-tradana");

            Modes.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu MenuCombo;
            public static readonly Menu MenuHarass;
            private static readonly Menu MenuKillsteal;
            private static readonly Menu MenuMisc;
            private static readonly Menu MenuDraw;
            private static readonly Menu MenuClear;

            static Modes()
            {
                MenuCombo = Menu.AddSubMenu("Combo");
                MenuHarass = Menu.AddSubMenu("Harass");
                MenuKillsteal = Menu.AddSubMenu("KillSteal");
                MenuMisc = Menu.AddSubMenu("Global settings");
                MenuDraw = Menu.AddSubMenu("Visual");
                MenuClear = Menu.AddSubMenu("Wave Clear");

                Combo.Initialize();
                Harass.Initialize();
                KillSteal.Initialize();
                Draw.Initialize();
                Misc.Initialize();
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
                private static readonly CheckBox _useRSeveral;
                private static readonly Slider _numberR;
                private static readonly Slider _minHPBotrk;
                private static readonly Slider _enemyMinHPBotrk;
                private static readonly CheckBox _useYoumuu;
                private static readonly CheckBox _useBotrk;

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

                public static bool UseRSeveral
                {
                    get { return _useRSeveral.CurrentValue; }
                }

 

                public static bool UseYoumuu
                {
                    get { return _useYoumuu.CurrentValue; }
                }
                public static bool useBotrk
                {
                    get { return _useBotrk.CurrentValue; }
                }

              public static int NumberR
                {
                    get { return _numberR.CurrentValue; }
                }

                
                public static int MinHPBotrk
                {
                    get { return _minHPBotrk.CurrentValue; }
                }
                public static int EnemyMinHPBotrk
                {
                    get { return _enemyMinHPBotrk.CurrentValue; }
                }

                static Combo()
                {
                    MenuCombo.AddGroupLabel("Kombo");
                    _useQ = MenuCombo.Add("comboUseQ", new CheckBox("Kullan Q"));
                    _useW = MenuCombo.Add("comboUseW", new CheckBox("Kullan W"));
                    _useE = MenuCombo.Add("comboUseE", new CheckBox("Kullan E", false));
                    _useRSeveral = MenuCombo.Add("comboUseRSeveral", new CheckBox("Düşmana R Kullan"));
                    _numberR = MenuCombo.Add("combonumberR", new Slider("R için en az düşman", 3, 1, 5));
                    MenuCombo.AddSeparator();
                    _useYoumuu = MenuCombo.Add("useYoumuu", new CheckBox("Kullan Youmuu"));
                    _useBotrk = MenuCombo.Add("useBotrk", new CheckBox("Kullan Mahvolmuş Kılıcı"));
                    _minHPBotrk = MenuCombo.Add("minHPBotrk", new Slider("Mahvolmuş Kullanmak için min canım ({0}%)", 80));
                    _enemyMinHPBotrk = MenuCombo.Add("enemyMinHPBotrk", new Slider("Mahvolmuş Kullanmak için Düşmanın canı ({0}%)", 80));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _toggleQ;
                private static readonly CheckBox _toggleW;
                private static readonly CheckBox _DelayAutoHarass;
                private static readonly Slider _manaQ;
                private static readonly Slider _manaW;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool ToggleQ
                {
                    get { return _toggleQ.CurrentValue; }
                }
                public static bool ToggleW
                {
                    get { return _toggleW.CurrentValue; }
                }
                public static bool DelayAutoHarass
                {
                    get { return _DelayAutoHarass.CurrentValue; }
                }

                public static int ManaQ
                {
                    get { return _manaQ.CurrentValue; }
                }

                public static int ManaW
                {
                    get { return _manaW.CurrentValue; }
                }

                static Harass()
                {
                    MenuHarass.AddGroupLabel("Dürtme");
                    _useQ = MenuHarass.Add("harassUseQ", new CheckBox("Kullan Q"));
                    _useW = MenuHarass.Add("harassUseW", new CheckBox("Kullan W"));
                    MenuHarass.AddSeparator();
                    MenuHarass.AddGroupLabel("Otomaik Dürtme");
                    _toggleQ = MenuHarass.Add("toggleQ", new CheckBox("Q Otomatik Dürtme"));
                    _toggleW = MenuHarass.Add("toggleW", new CheckBox("W Otomatik Dürtme"));
                    MenuHarass.AddSeparator();
                    foreach (var source in ObjectManager.Get<AIHeroClient>().Where(a => a.IsEnemy))
                    {
                        MenuHarass.Add(source.ChampionName + "harass", new CheckBox("Harass " + source.ChampionName, true));
                    }
                    MenuHarass.AddSeparator();
                    MenuHarass.AddGroupLabel("Mana Yardımcısı");
                    _manaQ = MenuHarass.Add("harassManaQ", new Slider("Q için en az mana ({0}%)", 40));
                    _manaW = MenuHarass.Add("harassManaW", new Slider("W için en az mana ({0}%)", 40));
                    _DelayAutoHarass = MenuHarass.Add("DelayAutoHarass", new CheckBox("Otomatik Dürtmede Gecikme"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Draw
            {
                private static readonly CheckBox _drawQ;
                private static readonly CheckBox _drawW;
                private static readonly CheckBox _drawE;
                private static readonly CheckBox _onlyRdy;
                public static readonly CheckBox _useHax;
                public static readonly Slider _skinhax;
                public static string[] skinName = { "Classic Ezreal", "Nottingham Ezreal", "Striker Ezreal", "Frosted Ezreal", "Explorer Ezreal", "Pulsefire Ezreal", "TPA Ezreal", "Debonair Ezreal", "Ace of Spades Ezreal" };

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

                public static bool OnlyRdy
                {
                    get { return _onlyRdy.CurrentValue; }
                }
                public static bool UseHax
                {
                    get { return _useHax.CurrentValue; }
                }

                public static int SkinHax
                {
                    get { return _skinhax.CurrentValue; }
                }

                static Draw()
                {
                    MenuDraw.AddGroupLabel("Büyü Menzili");
                    _drawQ = MenuDraw.Add("drawQ", new CheckBox("Göster Q"));
                    _drawW = MenuDraw.Add("drawW", new CheckBox("Göster W"));
                    _drawE = MenuDraw.Add("drawE", new CheckBox("Göster E"));
                    _onlyRdy = MenuDraw.Add("onlyRdy", new CheckBox("R göster sadece bekleme süresinde olmadığında"));
                    MenuDraw.AddSeparator();
                    MenuDraw.AddGroupLabel("Skin Değiştirici");
                    _useHax = MenuDraw.Add("UseHax", new CheckBox("Aktif skin Değiştirici", false));
                    _skinhax = MenuDraw.Add("skinhax", new Slider("Skin Numarası", 0, skinName.Length - 1, 0));
                }

                public static void Initialize()
                {
                }
            }
            public static class KillSteal
            {
                private static readonly CheckBox _KsQ;
                private static readonly CheckBox _KsW;
                private static readonly CheckBox _KsR;
                private static readonly Slider _MaxRRange;
                private static readonly Slider _MinRRange;
                private static readonly CheckBox _RedSteal;
                private static readonly CheckBox _BlueSteal;
                private static readonly CheckBox _DragonSteal;
                private static readonly CheckBox _BaronSteal;

                public static bool KsQ
                {
                    get { return _KsQ.CurrentValue; }
                }

                public static bool KsW
                {
                    get { return _KsW.CurrentValue; }
                }

                public static bool KsR
                {
                    get { return _KsR.CurrentValue; }
                }
                public static int MaxRRange
                {
                    get { return _MaxRRange.CurrentValue; }
                }
                public static int MinRRange
                {
                    get { return _MinRRange.CurrentValue; }
                }

                public static bool RedSteal
                {
                    get { return _RedSteal.CurrentValue; }
                }

                public static bool BlueSteal
                {
                    get { return _BlueSteal.CurrentValue; }
                }

                public static bool DragonSteal
                {
                    get { return _DragonSteal.CurrentValue; }
                }

                public static bool BaronSteal
                {
                    get { return _BaronSteal.CurrentValue; }
                }

                static KillSteal()
                {
                    MenuKillsteal.AddGroupLabel("Kill Çalma");
                    _KsQ = MenuKillsteal.Add("KsQ", new CheckBox("Kullan Q"));
                    _KsW = MenuKillsteal.Add("KsW", new CheckBox("Kullan W", false));
                    _KsR = MenuKillsteal.Add("KsR", new CheckBox("Kullan R"));
                    _MinRRange = MenuKillsteal.Add("MinRRange", new Slider("en az R menzili", 350, 1, 15000));
                    _MaxRRange = MenuKillsteal.Add("RRange", new Slider("En fazla R menzili", 4000, 1, 5000));
                    MenuKillsteal.AddLabel("R 5000le sınırla (Çökmeyi göreceksin) !");
                    MenuKillsteal.AddSeparator();
                    MenuKillsteal.AddGroupLabel("Orman Çalma");
                    MenuKillsteal.AddLabel("Disabled for now !");
                    _RedSteal = MenuKillsteal.Add("RedS", new CheckBox("R kullan Düşman Kırmızısına", false));
                    _BlueSteal = MenuKillsteal.Add("BlueS", new CheckBox("R kullan Düşman Mavisine", false));
                    _DragonSteal = MenuKillsteal.Add("DragonS", new CheckBox("Ulti Kullan Ejderi Çalmak İçin", false));
                    _BaronSteal = MenuKillsteal.Add("BaronS", new CheckBox("Ulti Kullan Baron Çalmak İçin", false));
                }

                public static void Initialize()
                {
                }
            }

            public static class Clear
            {
                private static readonly CheckBox _QLastHit;
                private static readonly CheckBox _WOnAlly;
                private static readonly Slider _NumberW;
                private static readonly Slider _manaQ;
                private static readonly Slider _manaW;

                public static bool UseQLastHit
                {
                    get { return _QLastHit.CurrentValue; }
                }

                public static bool UseWOnAlly
                {
                    get { return _WOnAlly.CurrentValue; }
                }

                public static int NumberW
                {
                    get { return _NumberW.CurrentValue; }
                }

                public static int ManaQ
                {
                    get { return _manaQ.CurrentValue; }
                }

                public static int ManaW
                {
                    get { return _manaW.CurrentValue; }
                }

                static Clear()
                {
                    MenuClear.AddGroupLabel("Lane / Jungle clear");
                    _QLastHit = MenuClear.Add("QLastHit", new CheckBox("Q yu lanetemizlemede akıllıca kullan"));
                    _WOnAlly = MenuClear.Add("WAlly", new CheckBox("Wyi dostlarda kullan"));
                    MenuClear.AddSeparator();
                    _NumberW = MenuClear.Add("NumberW", new Slider("W kullanmak için gereken dost sayısı", 2, 1, 4));
                    MenuClear.AddSeparator();
                    MenuClear.AddGroupLabel("Mana Yardımcısı");
                    _manaQ = MenuClear.Add("clearManaQ", new Slider("Q için en az mana ({0}%)", 40));
                    _manaW = MenuClear.Add("clearManaW", new Slider("W için en az mana ({0}%)", 40));
                }

                public static void Initialize()
                {
                }
            }
            public static class Misc
            {
                private static readonly CheckBox _CcQ;
                private static readonly CheckBox _CcW;
                private static readonly CheckBox _UseQOnUnkillable;
                private static readonly CheckBox _EGapClos;
                private static readonly CheckBox _UseQUnderTurret;
                private static readonly CheckBox _AutoTear;
                private static readonly CheckBox _EFlee;
                private static readonly Slider _PredQ;
                private static readonly Slider _PredW;
                private static readonly Slider _PredR;
                public static KeyBind _SelfW;


                public static bool CcQ
                {
                    get { return _CcQ.CurrentValue; }
                }

                public static bool CcW
                {
                    get { return _CcW.CurrentValue; }
                }
                public static bool AutoTear
                {
                    get { return _AutoTear.CurrentValue; }
                }
                public static bool EFlee
                {
                    get { return _EFlee.CurrentValue; }
                }
                public static bool UseQOnUnkillable
                {
                    get { return _UseQOnUnkillable.CurrentValue; }
                }

                public static bool SelfW
                {
                    get { return _SelfW.CurrentValue; }
                }

                public static bool UseQUnderTurret
                {
                    get { return _UseQUnderTurret.CurrentValue; }
                }

                public static int PredQ
                {
                    get { return _PredQ.CurrentValue; }
                }

                public static int PredW
                {
                    get { return _PredW.CurrentValue; }
                }

                public static int PredR
                {
                    get { return _PredR.CurrentValue; }
                }
                public static bool EGapClos
                {
                    get { return _EGapClos.CurrentValue; }
                }

                static Misc()
                {
                    MenuMisc.AddGroupLabel("Toplu Hedef");
                    _CcQ = MenuMisc.Add("CCQ", new CheckBox("Toplu Düşmanda Q Kullan"));
                    _CcW = MenuMisc.Add("CCW", new CheckBox("Toplu Düşmanda W Kullan"));
                    MenuMisc.AddGroupLabel("Minyonları Korumaya Al Q ile");
                    _UseQOnUnkillable = MenuMisc.Add("QUnkillable", new CheckBox("Eğer minyon AA ile ölmiyecekse Q Kullan"));
                    _UseQUnderTurret = MenuMisc.Add("QUnderTurret", new CheckBox("Kule altında Eğer Minyon AA ile Ölmeyecekse Q kullan"));
                    MenuMisc.AddGroupLabel("W ve E atak hızı ayarla");
                    _SelfW = MenuMisc.Add("SelfW", new KeyBind("Güvenli W", false, KeyBind.BindTypes.HoldActive, 'J'));
                    MenuMisc.AddGroupLabel("anti gap close için W kullan");
                    _EGapClos = MenuMisc.Add("EGapClos", new CheckBox("Gap closer için E kullan"));
                    MenuMisc.AddSeparator();
                    _AutoTear = MenuMisc.Add("AutoTear", new CheckBox("Enable Auto Tear at base"));
                    _EFlee = MenuMisc.Add("EFlee", new CheckBox("Kaçmak İçin E Kullan"));
                    MenuMisc.AddGroupLabel("İsabet Oranı");
                    MenuMisc.AddLabel("İsabet Oranı : 1 = Low, 2 = Medium, 3 = High");
                    _PredQ = MenuMisc.Add("PredQ", new Slider("Q İsabet Oranı", 3, 1, 3));
                    _PredW = MenuMisc.Add("PredW", new Slider("W İsabet Oranı", 3, 1, 3));
                    _PredR = MenuMisc.Add("PredR", new Slider("R İsabet Oranı", 3, 1, 3));
                }

                public static void Initialize()
                {
                }
            }
        }
    }
}
