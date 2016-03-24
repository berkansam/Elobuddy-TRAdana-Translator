using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace LelBlanc
{
    internal class Config
    {
        /// <summary>
        /// Contains all the Menu's
        /// </summary>
        public static Menu ConfigMenu,
            ComboMenu,
            HarassMenu,
            LaneClearMenu,
            JungleClearMenu,
            KillStealMenu,
            DrawingMenu,
            MiscMenu;

        /// <summary>
        /// Contains Different Modes
        /// </summary>
        private static readonly string[] LogicModes = {"Burst Logic", "Two Chain(TM) Logic"};

        /// <summary>
        /// Creates the Menu
        /// </summary>
        public static void Initialize()
        {
            ConfigMenu = MainMenu.AddMenu("LelBlanc", "LelBlanc");
            ConfigMenu.AddGroupLabel("This addon is made by KarmaPanda and should not be redistributed in any way.");
            ConfigMenu.AddGroupLabel(
                "Any unauthorized redistribution without credits will result in severe consequences.");
            ConfigMenu.AddGroupLabel("Thank you for using this addon and have a fun time!");

            ComboMenu = ConfigMenu.AddSubMenu("Combo Menu", "cMenu");
            ComboMenu.AddLabel("Büyü Ayarları");
            ComboMenu.Add("useQ", new CheckBox("Kullan Q"));
            ComboMenu.Add("useW", new CheckBox("Kullan W"));
            ComboMenu.Add("useReturn", new CheckBox("Kullan W Geri dönme"));
            ComboMenu.Add("useE", new CheckBox("Kullan E"));
            ComboMenu.AddLabel("R Ayarları");
            ComboMenu.Add("useQR", new CheckBox("Kullan QR"));
            ComboMenu.Add("useWR", new CheckBox("Kullan WR", false));
            ComboMenu.Add("useReturn2", new CheckBox("Kullan WR Geri dönme", false));
            ComboMenu.Add("useER", new CheckBox("Kullan ER", false));
            ComboMenu.AddLabel("Ek Ayarlar");
            ComboMenu.Add("mode", new ComboBox("Kombo Modu", 0, LogicModes));

            HarassMenu = ConfigMenu.AddSubMenu("Harass Menu", "hMenu");
            HarassMenu.AddLabel("Büyü Ayarları");
            HarassMenu.Add("useQ", new CheckBox("Kullan Q"));
            HarassMenu.Add("useW", new CheckBox("Kullan W"));
            HarassMenu.Add("useReturn", new CheckBox("Kullan W Geri dönme"));
            HarassMenu.Add("useE", new CheckBox("Kullan E"));
            HarassMenu.AddLabel("R Ayarları");
            HarassMenu.Add("useQR", new CheckBox("Kullan QR"));
            HarassMenu.Add("useWR", new CheckBox("Kullan WR", false));
            HarassMenu.Add("useReturn2", new CheckBox("Kullan WR Geri dönme"));
            HarassMenu.Add("useER", new CheckBox("Kullan ER", false));
            HarassMenu.AddLabel("Ek Ayarlar");
            HarassMenu.Add("mode", new ComboBox("Dürtme Modu", 1, LogicModes));

            LaneClearMenu = ConfigMenu.AddSubMenu("Laneclear Menu", "lcMenu");
            LaneClearMenu.AddLabel("Büyü Ayarları");
            LaneClearMenu.Add("useQ", new CheckBox("Kullan Q", false));
            LaneClearMenu.Add("useW", new CheckBox("Kullan W"));
            LaneClearMenu.Add("sliderW", new Slider("Kullan W Şu kadar minyonu öldürecekse", 3, 1, 5));
            LaneClearMenu.AddLabel("R Ayarları");
            LaneClearMenu.Add("useQR", new CheckBox("Kullan QR", false));
            LaneClearMenu.Add("useWR", new CheckBox("Kullan WR"));
            LaneClearMenu.Add("sliderWR", new Slider("Use WR Şu kadar minyonu öldürecekse", 5, 1, 5));

            JungleClearMenu = ConfigMenu.AddSubMenu("Jungleclear Menu", "jcMenu");
            JungleClearMenu.AddLabel("Büyü Ayarları");
            JungleClearMenu.Add("useQ", new CheckBox("Kullan Q"));
            JungleClearMenu.Add("useW", new CheckBox("Kullan W"));
            JungleClearMenu.Add("useE", new CheckBox("Kullan E"));
            JungleClearMenu.Add("sliderW", new Slider("Kullan W Eğer şu kadar minyona vurcaksa", 3, 1, 5));
            JungleClearMenu.AddLabel("R Ayarları");
            JungleClearMenu.Add("useQR", new CheckBox("Kullan QR"));
            JungleClearMenu.Add("useWR", new CheckBox("Kullan WR"));
            JungleClearMenu.Add("useER", new CheckBox("Kullan ER"));
            JungleClearMenu.Add("sliderWR", new Slider("Kullan WR Şu kadar minyona çarpacaksa", 5, 1, 5));

            KillStealMenu = ConfigMenu.AddSubMenu("Killsteal Menu", "ksMenu");
            KillStealMenu.AddLabel("Büyü Ayarları");
            KillStealMenu.Add("useQ", new CheckBox("Kullan Q"));
            KillStealMenu.Add("useW", new CheckBox("Kullan W"));
            KillStealMenu.Add("useReturn", new CheckBox("Kullan W Geri dönme"));
            KillStealMenu.Add("useE", new CheckBox("Kullan E"));
            KillStealMenu.AddLabel("R Ayarları");
            KillStealMenu.Add("useQR", new CheckBox("Kullan QR"));
            KillStealMenu.Add("useWR", new CheckBox("Kullan WR"));
            KillStealMenu.Add("useReturn2", new CheckBox("Kullan WR Geri dönme"));
            KillStealMenu.Add("useER", new CheckBox("Kullan ER"));
            KillStealMenu.AddLabel("ek Ayarlar");
            KillStealMenu.Add("useIgnite", new CheckBox("Tutuştur kullan"));
            KillStealMenu.Add("toggle", new CheckBox("Kill çalma aktif"));

            DrawingMenu = ConfigMenu.AddSubMenu("Drawing Menu", "dMenu");
            DrawingMenu.AddLabel("Gösterge");
            DrawingMenu.Add("drawQ", new CheckBox("Göster Q Menzili", false));
            DrawingMenu.Add("drawW", new CheckBox("Göster W Menzili", false));
            DrawingMenu.Add("drawE", new CheckBox("Göster E Menzili", false));
            DrawingMenu.AddLabel("Hasar Tespitçisi");
            DrawingMenu.Add("draw.Damage", new CheckBox("Göster Hasarı"));
            DrawingMenu.Add("draw.Q", new CheckBox("Hesapla Q Hasarı"));
            DrawingMenu.Add("draw.W", new CheckBox("Hesapla W Hasarı"));
            DrawingMenu.Add("draw.E", new CheckBox("Hesapla E Hasarı"));
            DrawingMenu.Add("draw.R", new CheckBox("Hesapla R Hasarı"));
            DrawingMenu.Add("draw.Ignite", new CheckBox("Hesapla Tutuştur Hasarı"));

            MiscMenu = ConfigMenu.AddSubMenu("Misc Menu", "mMenu");
            MiscMenu.AddLabel("Ek");
            MiscMenu.Add("pet", new CheckBox("Otomatik klon oynatma"));
        }
    }
}