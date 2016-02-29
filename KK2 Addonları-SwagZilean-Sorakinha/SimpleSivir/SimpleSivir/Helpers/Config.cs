﻿using System.CodeDom;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace SimpleSivir.Helpers
{
    internal class Config
    {
        private const string MenuName = "Sivir";
        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("SimpleSivir Rework");
            Menu.AddSeparator();
            Menu.AddLabel("Yapan Kk2");
            Menu.AddLabel("Yardım Etti MrArticuno!");
            Menu.AddLabel("Çeviri-TRAdana");
            Combo.Initialize();
            Harass.Initialize();
            LaneClear.Initialize();
            JungleClear.Initialize();
            Misc.Initialize();
            Drawings.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Combo
        {
            private static readonly Menu Menu;
            private static readonly CheckBox _useQ;
            private static readonly CheckBox _useW;
            private static readonly CheckBox _useR;

            static Combo()
            {
                Menu = Config.Menu.AddSubMenu("Kombo");
                _useQ = Menu.Add("useQCombo", new CheckBox("Kullan Q"));
                _useW = Menu.Add("useWCombo", new CheckBox("Kullan W"));
                _useR = Menu.Add("useRCombo", new CheckBox("Kullan R"));
            }

            public static bool UseQ
            {
                get { return _useQ.CurrentValue; }
            }

            public static bool UseW
            {
                get { return _useW.CurrentValue; }
            }

            public static bool UseR
            {
                get { return _useR.CurrentValue; }
            }

            public static void Initialize()
            {
            }
        }

        public static class Harass
        {
            private static readonly Menu Menu;
            private static readonly CheckBox _useQ;
            private static readonly CheckBox _useW;
            private static readonly Slider _minMana;

            static Harass()
            {
                Menu = Config.Menu.AddSubMenu("Dürtme");
                _useQ = Menu.Add("useQHarass", new CheckBox("Kullan Q"));
                _useW = Menu.Add("useWHarass", new CheckBox("Kullan W"));
                _minMana = Menu.Add("minManaHarass", new Slider("Dürtme için en az mana % ", 40));
            }

            public static bool UseQ
            {
                get { return _useQ.CurrentValue; }
            }

            public static bool UseW
            {
                get { return _useW.CurrentValue; }
            }

            public static int MinMana
            {
                get { return _minMana.CurrentValue; }
            }

            public static void Initialize()
            {
            }
        }

        public static class LaneClear
        {
            private static readonly Menu Menu;
            private static readonly CheckBox _useQ;
            private static readonly CheckBox _useW;
            private static readonly Slider _minMana;
            private static readonly Slider _minMinion;

            static LaneClear()
            {
                Menu = Config.Menu.AddSubMenu("LaneTemizleme");
                _useQ = Menu.Add("useQLane", new CheckBox("Kullan Q"));
                _useW = Menu.Add("useWLane", new CheckBox("Kullan W"));
                _minMana = Menu.Add("minManaLane", new Slider("Mana %>", 40));
                _minMinion = Menu.Add("minMinionsL", new Slider("En az yakalanacak minyonlar", 3, 1, 5));
            }

            public static int MinMinions
            {
                get { return _minMinion.CurrentValue; }
            }
            public static bool UseQ
            {
                get { return _useQ.CurrentValue; }
            }

            public static bool UseW
            {
                get { return _useW.CurrentValue; }
            }

            public static int MinMana
            {
                get { return _minMana.CurrentValue; }
            }

            public static void Initialize()
            {
            }
        }

        public static class JungleClear
        {
            private static readonly Menu Menu;
            private static readonly CheckBox _useQ;
            private static readonly CheckBox _useW;
            private static readonly Slider _minMana;

            static JungleClear()
            {
                Menu = Config.Menu.AddSubMenu("Orman Temizleme");
                _useQ = Menu.Add("useQJungle", new CheckBox("Kullan Q"));
                _useW = Menu.Add("useWJungle", new CheckBox("Kullan W"));
                _minMana = Menu.Add("minManaJungle", new Slider("Mana %>", 40));
               
            }

            public static bool UseQ
            {
                get { return _useQ.CurrentValue; }
            }

            public static bool UseW
            {
                get { return _useW.CurrentValue; }
            }

            public static int MinMana
            {
                get { return _minMana.CurrentValue; }
            }

            public static void Initialize()
            {
            }
        }

        public static class Misc
        {
            private static readonly Menu Menu;
            private static readonly CheckBox _useE;
            private static readonly CheckBox _autoQ;
            private static readonly Slider _skinHax;

            static Misc()
            {
                Menu = Config.Menu.AddSubMenu("Ek");
                _useE = Menu.Add("useEOP", new CheckBox("Otomatik E Kullan"));
                _autoQ = Menu.Add("autoQop", new CheckBox("Immobile/Dashing Hedeflere Otomatik Q Kullan"));
                Menu.AddSeparator();
                _skinHax = Menu.Add("skinhax", new Slider("İstediğin Görünümü Seç [Numarası]", 0, 0, 8));
                _skinHax.OnValueChange += delegate { ObjectManager.Player.SetSkinId(_skinHax.CurrentValue); };
                ObjectManager.Player.SetSkinId(_skinHax.CurrentValue);
            }

            public static bool UseE
            {
                get { return _useE.CurrentValue; }
            }

            public static bool AutoQ
            {
                get { return _autoQ.CurrentValue; }
            }

            public static int SkinHax
            {
                get { return _skinHax.CurrentValue; }
            }

            public static void Initialize()
            {
            }
        }

        public static class Drawings
        {
            private static readonly Menu Menu;
            private static readonly CheckBox _drawQ;
            private static readonly CheckBox _drawRTimer;
            private static readonly CheckBox _healthbar;
            private static readonly CheckBox _percent;
            private static readonly CheckBox _minionmark;

            static Drawings()
            {
                Menu = Config.Menu.AddSubMenu("Göstergeler");
                _drawQ = Menu.Add("drawQ", new CheckBox("Göster Q Range"));
                _drawRTimer = Menu.Add("drawRTimer", new CheckBox("Göster R Zamanlayıcı"));
                _minionmark = Menu.Add("minionMark", new CheckBox("Göster Son Vuruş Yardımcısı"));
                Menu.AddGroupLabel("Hasar Tespitçisi");
                _healthbar = Menu.Add("healthbar", new CheckBox("Canbarı Kaplama"));
                _percent = Menu.Add("percent", new CheckBox("Hasar Yüzde olarak bilgisi"));
            }

            public static bool DrawQ
            {
                get { return _drawQ.CurrentValue; }
            }

            public static bool DrawR
            {
                get { return _drawRTimer.CurrentValue; }
            }

            public static bool IndicatorHealthbar
            {
                get { return _healthbar.CurrentValue; }
            }

            public static bool IndicatorPercent
            {
                get { return _percent.CurrentValue; }
            }

            public static bool MinionMark
            {
                get { return _minionmark.CurrentValue; }
            }
            public static void Initialize()
            {
            }
        }
    }
}