using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using Color = System.Drawing.Color;

// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace KA_DamageIndicator
{
    public static class Config
    {
        private const string MenuName = "KA Hasar Gösterme";

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("KA Hasar Gösterme");
            Modes.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu DamageIndicatorMenu;

            static Modes()
            {

                DamageIndicatorMenu = Menu.AddSubMenu("::Hasar Gösterme::");
                Draw.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class Draw
            {
                private static readonly CheckBox _drawHealth;
                private static readonly CheckBox _drawPercent;
                private static readonly CheckBox _drawStatiscs;
                //Color Config
                private static readonly ColorConfig _healthColor;

                //CheckBoxes

                public static bool DrawHealth
                {
                    get { return _drawHealth.CurrentValue; }
                }

                public static bool DrawPercent
                {
                    get { return _drawPercent.CurrentValue; }
                }

                public static bool DrawStatistics
                {
                    get { return _drawStatiscs.CurrentValue; }
                }


                //Colors
                public static Color HealthColor
                {
                    get { return _healthColor.GetSystemColor(); }
                }

                static Draw()
                {
                    DamageIndicatorMenu.AddGroupLabel("Büyü Gösterme Ayarları :");
                    _drawHealth = DamageIndicatorMenu.Add("damageIndicatorDraw", new CheckBox("Verebileceğin hasarı göster ?"));
                    _drawPercent = DamageIndicatorMenu.Add("percentageIndicatorDraw", new CheckBox("Verebileceğin Hasarı Yüzde olarka göster ?"));
                    _drawStatiscs = DamageIndicatorMenu.Add("statiscsIndicatorDraw", new CheckBox("Hasar İstatistiklerini Göster ?"));

                    _healthColor = new ColorConfig(DamageIndicatorMenu, "healthColorConfig", Color.Yellow,
                        "Color Damage Indicator:");
                }

                public static void Initialize()
                {
                }
            }
        }
    }
}