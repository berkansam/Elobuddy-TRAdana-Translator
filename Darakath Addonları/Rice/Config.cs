// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace Rice
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;

    public static class Config
    {
        public static class Modes
        {
            public static class AutoStack
            {
                private static readonly Slider _AutoStackMana;

                private static readonly Slider _MaxStacks;

                private static readonly Slider _StackTimer;

                private static readonly KeyBind _AutoStackQ;

                public static int AutoStackMana
                {
                    get
                    {
                        return _AutoStackMana.CurrentValue;
                    }
                }

                public static bool AutoStackQ
                {
                    get
                    {
                        return _AutoStackQ.CurrentValue;
                    }
                }

                public static int MaxStacks
                {
                    get
                    {
                        return _MaxStacks.CurrentValue;
                    }
                }

                public static int StackTimer
                {
                    get
                    {
                        return _StackTimer.CurrentValue;
                    }
                }

                static AutoStack()
                {
                    MiscMenu.AddGroupLabel("Otomatik Yük");
                    _AutoStackQ = MiscMenu.Add(
                        "AutoStackQ",
                        new KeyBind("Pasif kasma tuşu", false, KeyBind.BindTypes.PressToggle, 'Z'));

                    _AutoStackQ.OnValueChange += delegate(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
                        {
                            Program.StackingStatus.TextValue = args.NewValue
                                                                   ? "Passive Stacking On"
                                                                   : "Passive Stacking Off";

                            Program.StackingStatus.Color = args.NewValue ? Color.LimeGreen : Color.Red;
                        };

                    _AutoStackMana = MiscMenu.Add("AutoStackMana", new Slider("En az mana yüzde", 30));

                    _MaxStacks = MiscMenu.Add("MaxStacks", new Slider("Şu Kadar Yük Tut", 3, 1, 4));

                    _StackTimer = MiscMenu.Add("StackTimer", new Slider("Her Stack <Saniye>", 5, 1, 10));
                }

                public static void Initialize()
                {
                }
            }

            public static class Humanizer
            {
                private static readonly Slider _MinDelay;

                private static readonly Slider _MaxDelay;

                private static readonly CheckBox _Humanize;

                public static int MinDelay
                {
                    get
                    {
                        return _MinDelay.CurrentValue;
                    }
                }

                public static int MaxDelay
                {
                    get
                    {
                        return _MaxDelay.CurrentValue;
                    }
                }

                public static bool Humanize
                {
                    get
                    {
                        return _Humanize.CurrentValue;
                    }
                }

                static Humanizer()
                {
                    MiscMenu.AddGroupLabel("İnsancıl Ayar");
                    _MinDelay = MiscMenu.Add("minDelay", new Slider(""En az Gecikme", 10, 0, 200));
                    _MaxDelay = MiscMenu.Add("maxDelay", new Slider("En fazla Gecikme", 75, 0, 250));
                    _Humanize = MiscMenu.Add("humanize", new CheckBox("İnsancıl", false));
                }

                public static void Initialize()
                {
                }
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;

                private static readonly CheckBox _useW;

                private static readonly CheckBox _useE;

                private static readonly CheckBox _useR;

                private static readonly CheckBox _blockAA;

                private static readonly Slider _minMana;

                public static int Mana
                {
                    get
                    {
                        return _minMana.CurrentValue;
                    }
                }

                public static bool UseQ
                {
                    get
                    {
                        return _useQ.CurrentValue;
                    }
                }

                public static bool UseW
                {
                    get
                    {
                        return _useW.CurrentValue;
                    }
                }

                public static bool UseE
                {
                    get
                    {
                        return _useE.CurrentValue;
                    }
                }

                public static bool UseR
                {
                    get
                    {
                        return _useR.CurrentValue;
                    }
                }

                public static bool blockAA
                {
                    get
                    {
                        return _blockAA.CurrentValue;
                    }
                }

                static Combo()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("Combo");
                    _useQ = ModesMenu.Add("comboQ", new CheckBox("Kullan Q"));
                    _useW = ModesMenu.Add("comboW", new CheckBox("Kullan W"));
                    _useE = ModesMenu.Add("comboE", new CheckBox("Kullan E"));
                    _useR = ModesMenu.Add("comboR", new CheckBox("Kullan R"));
                    _blockAA = ModesMenu.Add("blockAA", new CheckBox("AA Blokla"));
                    _minMana = ModesMenu.Add("comboMana", new Slider("en az manam %"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Draw
            {
                private static readonly CheckBox _drawHealth;

                private static readonly CheckBox _drawQ;

                private static readonly CheckBox _drawW;

                private static readonly CheckBox _drawE;

                private static readonly CheckBox _drawR;

                private static readonly CheckBox _drawReady;

                private static readonly CheckBox _drawStackStatus;

                public static bool DrawHealth
                {
                    get
                    {
                        return _drawHealth.CurrentValue;
                    }
                }

                public static bool DrawQ
                {
                    get
                    {
                        return _drawQ.CurrentValue;
                    }
                }

                public static bool DrawW
                {
                    get
                    {
                        return _drawW.CurrentValue;
                    }
                }

                public static bool DrawE
                {
                    get
                    {
                        return _drawE.CurrentValue;
                    }
                }

                public static bool DrawR
                {
                    get
                    {
                        return _drawR.CurrentValue;
                    }
                }

                public static bool DrawReady
                {
                    get
                    {
                        return _drawReady.CurrentValue;
                    }
                }

                public static bool DrawStackStatus
                {
                    get
                    {
                        return _drawStackStatus.CurrentValue;
                    }
                }

                public static Color colorHealth
                {
                    get
                    {
                        return DrawMenu.GetColor("colorHealth");
                    }
                }

                public static Color colorQ
                {
                    get
                    {
                        return DrawMenu.GetColor("colorQ");
                    }
                }

                public static Color colorW
                {
                    get
                    {
                        return DrawMenu.GetColor("colorW");
                    }
                }

                public static Color colorE
                {
                    get
                    {
                        return DrawMenu.GetColor("colorE");
                    }
                }

                public static Color colorR
                {
                    get
                    {
                        return DrawMenu.GetColor("colorR");
                    }
                }

                public static float _widthQ
                {
                    get
                    {
                        return DrawMenu.GetWidth("widthQ");
                    }
                }

                public static float _widthW
                {
                    get
                    {
                        return DrawMenu.GetWidth("widthW");
                    }
                }

                public static float _widthE
                {
                    get
                    {
                        return DrawMenu.GetWidth("widthE");
                    }
                }

                public static float _widthR
                {
                    get
                    {
                        return DrawMenu.GetWidth("widthR");
                    }
                }

                static Draw()
                {
                    DrawMenu.AddGroupLabel("Göstergeler");
                    _drawReady = DrawMenu.Add("drawReady", new CheckBox("Sadece Büyüler Hazırsa Göster.", false));
                    _drawHealth = DrawMenu.Add("drawHealth", new CheckBox("Can barında hasarı göster"));
                    _drawStackStatus = DrawMenu.Add("drawStackStatus", new CheckBox("stack durumunu yaz"));
                    DrawMenu.AddColorItem("colorHealth");
                    DrawMenu.AddSeparator();
                    //Q
                    _drawQ = DrawMenu.Add("drawQ", new CheckBox("Göster Q"));
                    DrawMenu.AddColorItem("colorQ");
                    DrawMenu.AddWidthItem("widthQ");
                    //W
                    _drawW = DrawMenu.Add("drawW", new CheckBox("Göster W"));
                    DrawMenu.AddColorItem("colorW");
                    DrawMenu.AddWidthItem("widthW");
                    //E
                    _drawE = DrawMenu.Add("drawE", new CheckBox("Göster E"));
                    DrawMenu.AddColorItem("colorE");
                    DrawMenu.AddWidthItem("widthE");
                    //R
                    _drawR = DrawMenu.Add("drawR", new CheckBox("Göster R"));
                    DrawMenu.AddColorItem("colorR");
                    DrawMenu.AddWidthItem("widthR");
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;

                private static readonly Slider _minMana;

                public static int Mana
                {
                    get
                    {
                        return _minMana.CurrentValue;
                    }
                }

                public static bool UseQ
                {
                    get
                    {
                        return _useQ.CurrentValue;
                    }
                }

                static Harass()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("Dürtme");
                    _useQ = ModesMenu.Add("harassQ", new CheckBox("Kullan Q"));
                    _minMana = ModesMenu.Add("harassMana", new Slider("en az  Mana"));
                }

                public static void Initialize()
                {
                }
            }

            public static class JungleClear
            {
                private static readonly CheckBox _useQ;

                private static readonly CheckBox _useW;

                private static readonly CheckBox _useE;

                private static readonly CheckBox _useR;

                private static readonly Slider _minMana;

                public static int Mana
                {
                    get
                    {
                        return _minMana.CurrentValue;
                    }
                }

                public static bool UseQ
                {
                    get
                    {
                        return _useQ.CurrentValue;
                    }
                }

                public static bool UseW
                {
                    get
                    {
                        return _useW.CurrentValue;
                    }
                }

                public static bool UseE
                {
                    get
                    {
                        return _useE.CurrentValue;
                    }
                }

                public static bool UseR
                {
                    get
                    {
                        return _useR.CurrentValue;
                    }
                }

                static JungleClear()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("JungleClear");
                    _useQ = ModesMenu.Add("jungleQ", new CheckBox("Kullan Q"));
                    _useW = ModesMenu.Add("jungleW", new CheckBox("Kullan W"));
                    _useE = ModesMenu.Add("jungleE", new CheckBox("Kullan E"));
                    _useR = ModesMenu.Add("jungleR", new CheckBox("Kullan R"));
                    _minMana = ModesMenu.Add("jungleMana", new Slider("en az Mana"));
                }

                public static void Initialize()
                {
                }
            }

            public static class KillSteal
            {
                private static readonly CheckBox _useQ;

                private static readonly CheckBox _useW;

                private static readonly CheckBox _useE;

                public static bool UseQ
                {
                    get
                    {
                        return _useQ.CurrentValue;
                    }
                }

                public static bool UseW
                {
                    get
                    {
                        return _useW.CurrentValue;
                    }
                }

                public static bool UseE
                {
                    get
                    {
                        return _useE.CurrentValue;
                    }
                }

                static KillSteal()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("KillSteal");
                    _useQ = ModesMenu.Add("killQ", new CheckBox("Kullan Q"));
                    _useW = ModesMenu.Add("killW", new CheckBox("Kullan W"));
                    _useE = ModesMenu.Add("killE", new CheckBox("Kullan E"));
                }

                public static void Initialize()
                {
                }
            }

            public static class LaneClear
            {
                private static readonly CheckBox _useQ;

                private static readonly CheckBox _useW;

                private static readonly CheckBox _useE;

                private static readonly CheckBox _useR;

                private static readonly Slider _minMana;

                public static int Mana
                {
                    get
                    {
                        return _minMana.CurrentValue;
                    }
                }

                public static bool UseQ
                {
                    get
                    {
                        return _useQ.CurrentValue;
                    }
                }

                public static bool UseW
                {
                    get
                    {
                        return _useW.CurrentValue;
                    }
                }

                public static bool UseE
                {
                    get
                    {
                        return _useE.CurrentValue;
                    }
                }

                public static bool UseR
                {
                    get
                    {
                        return _useR.CurrentValue;
                    }
                }

                static LaneClear()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("LaneClear");
                    _useQ = ModesMenu.Add("laneQ", new CheckBox("Kullan Q"));
                    _useW = ModesMenu.Add("laneW", new CheckBox("Kullan W"));
                    _useE = ModesMenu.Add("laneE", new CheckBox("Kullan E"));
                    _useR = ModesMenu.Add("laneR", new CheckBox("Kullan R"));
                    _minMana = ModesMenu.Add("laneMana", new Slider("en az Mana"));
                }

                public static void Initialize()
                {
                }
            }

            public static class LastHit
            {
                private static readonly CheckBox _useQ;

                private static readonly Slider _minMana;

                public static int Mana
                {
                    get
                    {
                        return _minMana.CurrentValue;
                    }
                }

                public static bool UseQ
                {
                    get
                    {
                        return _useQ.CurrentValue;
                    }
                }

                static LastHit()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("LastHit");
                    _useQ = ModesMenu.Add("lastQ", new CheckBox("Kullan Q"));
                    _minMana = ModesMenu.Add("lastMana", new Slider("en az Mana"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Misc
            {
                private static readonly Slider _QCollision;

                private static readonly CheckBox _ChangeNames;

                private static readonly CheckBox _AutoWInterruptible;

                private static readonly CheckBox _AutoWGapCloser;

                public static List<string> EnemyNames, AllyNames;

                public static int QCollision
                {
                    get
                    {
                        return _QCollision.CurrentValue;
                    }
                }

                public static bool ChangeNames
                {
                    get
                    {
                        return _ChangeNames.CurrentValue;
                    }
                }

                public static bool AutoWInterruptible
                {
                    get
                    {
                        return _AutoWInterruptible.CurrentValue;
                    }
                }

                public static bool AutoWGapCloser
                {
                    get
                    {
                        return _AutoWGapCloser.CurrentValue;
                    }
                }

                static Misc()
                {
                    MiscMenu.AddGroupLabel("Ek Özellikler");
                    _QCollision = MiscMenu.Add("QCollision", new Slider("Q Çarpışmada: Her Zaman Düşmana Kullan", 0, 0, 1));
                    _ChangeNames = MiscMenu.Add("ChangeNames", new CheckBox("Hero Adını Değiştir (Deneysel)", false));
                    _AutoWGapCloser = MiscMenu.Add("AutoWGapCloser", new CheckBox("Gapcloser Otomatik Yerleşme"));
                    _AutoWInterruptible = MiscMenu.Add("AutoWInterruptible", new CheckBox("Interruptible Otomatik Yerleşme"));

                    _QCollision.OnValueChange += delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
                        {
                            switch (args.NewValue)
                            {
                                case 0:
                                    _QCollision.DisplayName = "QCollision: Always Q At Enemy";
                                    break;
                                case 1:
                                    _QCollision.DisplayName = "QCollision: Only Q If No Collision";
                                    break;
                            }
                        };
                    _ChangeNames.OnValueChange += delegate(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
                        {
                            if (args.NewValue == false)
                            {
                                foreach (var enemy in EntityManager.Heroes.Enemies)
                                {
                                    enemy.Name =
                                        EnemyNames[
                                            EntityManager.Heroes.Enemies.OrderBy(x => x.NetworkId)
                                                .ToList()
                                                .IndexOf(enemy)];
                                }
                                foreach (var ally in EntityManager.Heroes.Allies)
                                {
                                    ally.Name =
                                        AllyNames[
                                            EntityManager.Heroes.Enemies.OrderBy(x => x.NetworkId)
                                                .ToList()
                                                .IndexOf(ally)];
                                }
                            }
                            else
                            {
                                foreach (var enemy in EntityManager.Heroes.Enemies)
                                {
                                    enemy.Name = "Kombo Yap Lütfen";
                                }
                                foreach (var ally in EntityManager.Heroes.Allies)
                                {
                                    ally.Name = "KS yapma Lütfen";
                                }
                                Player.Instance.Name = "Best Rice EB";
                            }
                        };
                }

                public static void Initialize()
                {
                }
            }

            public static class TearStack
            {
                private static readonly Slider _AutoStackMana;

                private static readonly Slider _MaxStacks;

                private static readonly Slider _StackTimer;

                private static readonly CheckBox _OnlyFountain;

                private static readonly KeyBind _AutoStackQ;

                public static int AutoStackMana
                {
                    get
                    {
                        return _AutoStackMana.CurrentValue;
                    }
                }

                public static bool AutoStackQ
                {
                    get
                    {
                        return _AutoStackQ.CurrentValue;
                    }
                }

                public static bool OnlyFountain
                {
                    get
                    {
                        return _OnlyFountain.CurrentValue;
                    }
                }

                public static int MaxStacks
                {
                    get
                    {
                        return _MaxStacks.CurrentValue;
                    }
                }

                public static int StackTimer
                {
                    get
                    {
                        return _StackTimer.CurrentValue;
                    }
                }

                static TearStack()
                {
                    MiscMenu.AddGroupLabel("Sürekli Yük");
                    _AutoStackQ = MiscMenu.Add(
                        "AutoTear",
                        new KeyBind("Otomatik Yük Tuşu", false, KeyBind.BindTypes.PressToggle, 'T'));

                    _AutoStackMana = MiscMenu.Add("AutoTearMana", new Slider("en az mana %", 30));

                    _MaxStacks = MiscMenu.Add("MaxStacksTear", new Slider("Yük Tut", 3, 1, 4));

                    _StackTimer = MiscMenu.Add("TearStackTimer", new Slider("Her Yük  <saniye>", 5, 1, 10));

                    _OnlyFountain = MiscMenu.Add("OnlyFountain", new CheckBox("Sadece Yük Yap", false));
                }

                public static void Initialize()
                {
                }
            }

            private static readonly Menu ModesMenu, DrawMenu, MiscMenu;

            static Modes()
            {
                ModesMenu = Menu.AddSubMenu("Modes");

                Combo.Initialize();
                Menu.AddSeparator();
                Harass.Initialize();
                Menu.AddSeparator();
                LaneClear.Initialize();
                Menu.AddSeparator();
                LastHit.Initialize();

                MiscMenu = Menu.AddSubMenu("Misc");
                Misc.Initialize();
                Menu.AddSeparator();
                AutoStack.Initialize();
                Menu.AddSeparator();
                Humanizer.Initialize();
                //Menu.AddSeparator();
                //TearStack.Initialize();

                DrawMenu = Menu.AddSubMenu("Draw");
                Draw.Initialize();
            }

            public static void Initialize()
            {
            }
        }

        private const string MenuName = "Rice";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Rice");
            Menu.AddLabel("Tamamen Darakath tarafından yapılmıştır", 50);
            Menu.AddLabel("Desteğiniz için teşekkürler.", 50);
            Menu.AddLabel("Çeviri TRAdana");

            // Initialize the modes
            Modes.Initialize();
        }

        public static void Initialize()
        {
        }
    }
}