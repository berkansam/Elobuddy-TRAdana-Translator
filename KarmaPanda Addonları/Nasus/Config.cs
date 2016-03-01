﻿using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Nasus
{
    internal class Config
    {
        /// <summary>
        /// Initializes the Menu
        /// </summary>
        public static Menu ConfigMenu, FarmMenu, ComboMenu, DrawingMenu;

        /// <summary>
        /// Creates the Menu
        /// </summary>
        public static void Initialize()
        {
            // Main Menu
            ConfigMenu = MainMenu.AddMenu("KA Nasus", "ConfigMenu");
            ConfigMenu.AddGroupLabel("Bu Addon KarmaPandaya aittir.");
            ConfigMenu.AddGroupLabel(
                "Any unauthorized redistribution without credits will result in severe consequences.");
            ConfigMenu.AddGroupLabel("Kullandığınız için teşekkürler iyi eğlenceler dilerim!");
            ConfigMenu.AddLabel("Çeviri TRAdana");

            // Farm Menu
            FarmMenu = ConfigMenu.AddSubMenu("Farm", "Farm");
            FarmMenu.AddGroupLabel("Büyü Ayarları");
            FarmMenu.AddLabel("Q Ayarları");
            FarmMenu.Add("useQ", new CheckBox("Son vuruş için Q Kullan"));
            FarmMenu.Add("disableAA", new CheckBox("Minyona düz vuruş yapma Sadece Q Kullan", false));
            FarmMenu.AddLabel("Dürtme Ayarları");
            FarmMenu.Add("useQH", new CheckBox("Q Kullan", false));
            FarmMenu.Add("useEH", new CheckBox("E Kullan", false));
            FarmMenu.Add("manaEH", new Slider("Dürtmeden önce gereken mana %", 30));
            FarmMenu.AddLabel("LaneTemizleme Ayarları");
            FarmMenu.Add("useELC", new CheckBox("E Kullan"));
            FarmMenu.Add("useELCS", new Slider("E için minyon say", 2, 1, 6));
            FarmMenu.Add("manaELC", new Slider("Lanetemizleme E için gereken mana %", 30));

            // Combo Menu
            ComboMenu = ConfigMenu.AddSubMenu("Kombo", "Combo");
            ComboMenu.AddGroupLabel("Büyü Ayarları");
            ComboMenu.Add("useQ", new CheckBox("Q Kullan"));
            ComboMenu.Add("useW", new CheckBox("W Kullan"));
            ComboMenu.Add("useE", new CheckBox("E Kullan"));
            ComboMenu.Add("useR", new CheckBox("R Kullan"));
            ComboMenu.AddGroupLabel("ManaYardımcısı");
            ComboMenu.Add("manaW", new Slider("W için gereken mana %", 25));
            ComboMenu.Add("manaE", new Slider("E için gereken mana %", 30));
            ComboMenu.AddGroupLabel("R Ayarları");
            ComboMenu.Add("hpR", new Slider("R için canım şundan az", 25));
            ComboMenu.Add("intR", new Slider("R için Şu kadar düşman", 1, 0, 5));
            ComboMenu.Add("rangeR", new Slider("R için Şu menzilde yukarıdaki kadar düşman varsa", 1200, 0, 2000));

            // Drawing Menu
            DrawingMenu = ConfigMenu.AddSubMenu("Gösterge", "Drawing");
            DrawingMenu.AddGroupLabel("Büyü Drawing Ayarları");
            DrawingMenu.Add("drawW", new CheckBox("Göster W Menzili", false));
            DrawingMenu.Add("drawE", new CheckBox("Göster E Menzili", false));
            DrawingMenu.AddLabel("HasarTespitçisi");
            DrawingMenu.Add("draw.Damage", new CheckBox("Göster Hasar"));
            DrawingMenu.Add("draw.Q", new CheckBox("Q Hasarı Hesapla"));
            DrawingMenu.Add("draw.E", new CheckBox("E Hesapla Hesapla"));
            DrawingMenu.AddLabel("HasarTespitçisi için renk");
            DrawingMenu.Add("draw_Alpha", new Slider("Alpha: ", 255, 0, 255));
            DrawingMenu.Add("draw_Red", new Slider("Kırmızı: ", 255, 0, 255));
            DrawingMenu.Add("draw_Green", new Slider("Yeşil: ", 0, 0, 255));
            DrawingMenu.Add("draw_Blue", new Slider("Mavi: ", 0, 0, 255));
        }
    }
}