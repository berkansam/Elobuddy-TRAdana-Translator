using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using Settings = JusticeTalon.Config.Modes;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace JusticeTalon
{
    public static class Config
    {
        private const string MenuName = "Justice For Everyone";
        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Merhaba Bu Addon Senin İçin Hazır");
            Menu.AddLabel("Lütfen Kombo Durumu Hakkında Bana Geri Dönüş Yap");
            Menu.AddLabel("forum name : aliyrlmz");
            Menu.AddLabel("Çeviri-TRAdana");
            Menu.AddLabel("Addon Güncellendiğinde PM atınız");
            Modes.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu Menu;

            static Modes()
            {
                Menu = Config.Menu.AddSubMenu("Modes");
                Menu.AddSeparator();
                Combo.Initialize();
                Menu.AddSeparator();
                LaneClear.Initialize();
                Harass.Initialize();
                JungleClear.Initialize();
                DrawDmg.Initialize();
                Skilldraws.Initialize();
                ItemUsage.Initialize();
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

                static Combo()
                {
                    Menu.AddGroupLabel("Kombo");
                    _useQ = Menu.Add("comboUseQ", new CheckBox("Kullan Q"));
                    _useW = Menu.Add("comboUseW", new CheckBox("Kullan W"));
                    _useE = Menu.Add("comboUseE", new CheckBox("Kullan E"));
                    _useR = Menu.Add("comboUseR", new CheckBox("Kullan R"));
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

                public static void Initialize()
                {
                }
            }
            public static class DrawDmg
            {
                private static readonly CheckBox _damageDraw;
                private static readonly CheckBox _perDraw;
                private static readonly CheckBox _statDraw;

                static DrawDmg()
                {
                    Menu.AddGroupLabel("Hasarı Göster");
                    _damageDraw = Menu.Add("damageDraw", new CheckBox("Hasar Göster"));
                    _perDraw = Menu.Add("perDraw", new CheckBox("perDraw"));
                    _statDraw = Menu.Add("statDraw", new CheckBox("Durumu Göster"));
                }

                public static bool damageDraw
                {
                    get { return _damageDraw.CurrentValue; }
                }

                public static bool perDraw
                {
                    get { return _perDraw.CurrentValue; }
                }

                public static bool statDraw
                {
                    get { return _statDraw.CurrentValue; }
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                static Harass()
                {
                    Menu.AddGroupLabel("Dürtme");
                    Menu.Add("harassUseW", new CheckBox("Kullan W"));
                    /*Menu.Add("harassUseQ", new CheckBox("Use Q"));*/
                    Menu.Add("harassMana", new Slider("En fazla kullanabilir mana ({0}%)", 40));
                }

                public static bool UseW
                {
                    get { return Menu["harassUseW"].Cast<CheckBox>().CurrentValue; }
                }

                /*public static bool UseQ
                {
                    get { return Menu["harassUseQ"].Cast<CheckBox>().CurrentValue; }
                }*/

                public static int ManaUsage
                {
                    get { return Menu["harassMana"].Cast<Slider>().CurrentValue; }
                }

                public static void Initialize()
                {
                }
            }

            public static class LaneClear
            {
                public const string GroupName = "LaneClear";

                /*private static readonly CheckBox _useQ;*/
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useQ;
                private static readonly Slider _hitNumW;
                private static readonly Slider _mana;

                static LaneClear()
                {
                    Menu.AddGroupLabel(GroupName);
                    _useW = Menu.Add("laneUseW", new CheckBox("Kullan W"));
                    _useQ = Menu.Add("laneUseQ", new CheckBox("Kullan Q"));

                    Menu.AddLabel("Gelişmiş Ayarlar:");
                    _hitNumW = Menu.Add("laneHitW", new Slider("Kaç minyona çarpacaksa belirt", 3, 1, 10));
                    _mana = Menu.Add("laneMana", new Slider("Manam Şundan Azsa lanetemizleme kullanma (%)", 30));
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static int HitNumberW
                {
                    get { return _hitNumW.CurrentValue; }
                }

                public static int ManaUsage
                {
                    get { return _mana.CurrentValue; }
                }

                public static void Initialize()
                {
                }
            }

            public static class JungleClear
            {
                public const string GroupName = "JungleClear";

                /* private static readonly CheckBox _useQ;*/
                private static readonly CheckBox _useW;
                private static readonly Slider _hitNumW;
                private static readonly Slider _mana;

                static JungleClear()
                {
                    Menu.AddGroupLabel(GroupName);
                    _useW = Menu.Add("jungleUseW", new CheckBox("Kullan W"));
                    _useW = Menu.Add("jungleUseQ", new CheckBox("Kullan Q"));
                    /* _useQ = Menu.Add("jungleUseQ", new CheckBox("Use Q"));*/

                    Menu.AddLabel("Gelişmiş Özellikler:");
                    _hitNumW = Menu.Add("jungleHitW", new Slider("W kaç canavara çarpacak", 3, 1, 10));
                    _mana = Menu.Add("jungleMana", new Slider("Eğer manam şundan az ise orman temizleme kullanma (%)", 30));
                }

                /*   public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }*/

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool UseQ
                {
                    get { return _useW.CurrentValue; }
                }

                public static int HitNumberW
                {
                    get { return _hitNumW.CurrentValue; }
                }

                public static int ManaUsage
                {
                    get { return _mana.CurrentValue; }
                }

                public static void Initialize()
                {
                }
            }
            public static class Skilldraws
            {
                public const string GroupName = "SkillDraws";

                /* private static readonly CheckBox _useQ;*/
                private static readonly CheckBox _drawE;

                static Skilldraws()
                {
                    Menu.AddGroupLabel("Büyü Gösterimi");
                    _drawE = Menu.Add("DrawE", new CheckBox("Göster E"));
                }

                public static bool drawE
                {
                    get { return _drawE.CurrentValue; }
                }


                public static void Initialize()
                {
                }
            }
            public static class ItemUsage
            {
                public const string GroupName = "ItemUsage";

                /* private static readonly CheckBox _useQ;*/
                private static readonly CheckBox _ItemUsage;

                static ItemUsage()
                {
                    Menu.AddGroupLabel("İtem Kullanımı");
                    _ItemUsage = Menu.Add("ItemUsage", new CheckBox("İtem Kullan"));
                }

                public static bool itemusage
                {
                    get { return _ItemUsage.CurrentValue; }
                }


                public static void Initialize()
                {
                }
            }
        }
    }
}