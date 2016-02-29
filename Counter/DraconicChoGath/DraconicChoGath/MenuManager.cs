using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace DraconicChoGath
{
    internal class MenuManager
    {
        // Create Main Segments
        public static Menu DraconicChoGathMenu, ComboMenu, HarassMenu, JungleMenu, LaneClearMenu, LastHitMenu, KillStealMenu, DrawingMenu, SettingMenu;

        public static void Initialize()
        {
            // Addon Menu
            DraconicChoGathMenu = MainMenu.AddMenu("DraconicChoGath", "DraconicChoGath");
            DraconicChoGathMenu.AddGroupLabel("Draconic Cho'Gath");
            DraconicChoGathMenu.AddLabel("Çeviri TRAdana");

            // Combo Menu
            ComboMenu = DraconicChoGathMenu.AddSubMenu("Kombo Ayarları", "ComboFeatures");
            ComboMenu.AddGroupLabel("Kombo Ayarları");
            ComboMenu.AddLabel("Büyüler");
            ComboMenu.Add("Qcombo", new CheckBox("Kullan Q"));
            ComboMenu.Add("Wcombo", new CheckBox("Kullan W"));
            ComboMenu.Add("Rcombo", new CheckBox("Kullan R", false));

            // Harass Menu
            HarassMenu = DraconicChoGathMenu.AddSubMenu("Dürtme Ayarları", "HarassFeatures");
            HarassMenu.AddGroupLabel("Dürtme Ayarları");
            HarassMenu.AddLabel("Büyüler");
            HarassMenu.Add("Qharass", new CheckBox("Kullan Q"));
            HarassMenu.Add("Wharass", new CheckBox("Kullan W"));

            // Jungle Menu
            JungleMenu = DraconicChoGathMenu.AddSubMenu("OrmanTemizleme", "JungleFeatures");
            JungleMenu.AddGroupLabel("OrmanTemizleme Ayarları");
            JungleMenu.AddLabel("Büyüler");
            JungleMenu.Add("Qjungle", new CheckBox("Kullan Q"));
            JungleMenu.Add("Wjungle", new CheckBox("Kullan W", false));
            JungleMenu.Add("Rjungle", new CheckBox("Kullan R"));
            JungleMenu.Add("Fjungle", new CheckBox("Kullan Flash + R ile çal"));

            // LaneClear Menu
            LaneClearMenu = DraconicChoGathMenu.AddSubMenu("LaneTemizleme", "LaneClearFeatures");
            LaneClearMenu.AddGroupLabel("LaneTemizleme Ayarları");
            LaneClearMenu.AddLabel("Büyüler:");
            LaneClearMenu.Add("Qlanec", new CheckBox("Kullan Q", false));
            LaneClearMenu.Add("Wlanec", new CheckBox("Kullan W", false));

            // LastHit Menu
            LastHitMenu = DraconicChoGathMenu.AddSubMenu("SonVuruş", "LastHitFeatures");
            LastHitMenu.AddGroupLabel("SonVuruş Ayarları");
            LastHitMenu.AddLabel("Büyüler:");
            LastHitMenu.Add("Qlasthit", new CheckBox("Kullan Q", false));
            LastHitMenu.Add("Wlasthit", new CheckBox("Kullan W", false));
            LastHitMenu.Add("Rlasthit", new CheckBox("Kullan R"));

            // Kill Steal Menu
            KillStealMenu = DraconicChoGathMenu.AddSubMenu("KS Ayarları", "KSFeatures");
            KillStealMenu.AddGroupLabel("Kill Çalma Ayarları");
            KillStealMenu.Add("Uks", new CheckBox("KS Modu"));
            KillStealMenu.AddSeparator(1);
            KillStealMenu.AddLabel("Büyüler");
            KillStealMenu.Add("Qks", new CheckBox("Q Kullan"));
            KillStealMenu.Add("Wks", new CheckBox("W Kullan"));
            KillStealMenu.Add("Rks", new CheckBox("R Kullan"));
            KillStealMenu.Add("Fks", new CheckBox("Kullan Flash + R KS için"));
            
            // Drawing Menu
            DrawingMenu = DraconicChoGathMenu.AddSubMenu("Drawing Ayarları", "DrawingFeatures");
            DrawingMenu.AddGroupLabel("Drawing Ayarları");
            DrawingMenu.Add("Udraw", new CheckBox("Draw Mode"));
            DrawingMenu.AddSeparator(1);
            DrawingMenu.AddLabel("Büyüler");
            DrawingMenu.Add("Qdraw", new CheckBox("Göster Q"));
            DrawingMenu.Add("Wdraw", new CheckBox("Göster W"));
            DrawingMenu.Add("Rdraw", new CheckBox("Göster R"));
            DrawingMenu.Add("Fdraw", new CheckBox("Göster Flash + R"));
            DrawingMenu.AddSeparator(1);
            DrawingMenu.AddLabel("Skin Değiştirici");
            DrawingMenu.Add("Udesign", new CheckBox("Göster Skin"));
            DrawingMenu.Add("Sdesign", new Slider("Skin Numarası: ", 0, 0, 6));

            // Setting Menu
            SettingMenu = DraconicChoGathMenu.AddSubMenu("Ayarlar", "Settings");
            SettingMenu.AddGroupLabel("Ayarlar");
            SettingMenu.AddLabel("Otomatik Level Yükseltme");
            SettingMenu.Add("Ulevel", new CheckBox("Otomatik Level Yükseltme"));
            SettingMenu.AddSeparator(1);
            SettingMenu.AddLabel("Otomatik Yük");
            SettingMenu.Add("Ustack", new CheckBox("Stack Mode"));
            SettingMenu.AddSeparator(1);
            SettingMenu.AddLabel("Interrupter");
            SettingMenu.Add("Uinterrupt", new CheckBox("Interrupt Mode"));
            SettingMenu.Add("Qinterrupt", new CheckBox("Use Q to interrupt"));
            SettingMenu.AddLabel("Gap Closer");
            SettingMenu.Add("Ugapc", new CheckBox("Gap Closer Mode"));
            SettingMenu.Add("Qgapc", new CheckBox("Use Q to gapclose"));
        }

        // Assign Global Checks+
        public static bool ComboUseQ { get { return ComboMenu["Qcombo"].Cast<CheckBox>().CurrentValue; } }
        public static bool ComboUseW { get { return ComboMenu["Wcombo"].Cast<CheckBox>().CurrentValue; } }
        public static bool ComboUseR { get { return ComboMenu["Rcombo"].Cast<CheckBox>().CurrentValue; } }
        
        public static bool HarassUseQ { get { return HarassMenu["Qharass"].Cast<CheckBox>().CurrentValue; } }
        public static bool HarassUseW { get { return HarassMenu["Wharass"].Cast<CheckBox>().CurrentValue; } }

        public static bool JungleUseQ { get { return JungleMenu["Qjungle"].Cast<CheckBox>().CurrentValue; } }
        public static bool JungleUseW { get { return JungleMenu["Wjungle"].Cast<CheckBox>().CurrentValue; } }
        public static bool JungleUseR { get { return JungleMenu["Rjungle"].Cast<CheckBox>().CurrentValue; } }
        public static bool JungleUseSteal { get { return JungleMenu["Fjungle"].Cast<CheckBox>().CurrentValue; } }

        public static bool LaneClearUseQ { get { return LaneClearMenu["Qlanec"].Cast<CheckBox>().CurrentValue; } }
        public static bool LaneClearUseW { get { return LaneClearMenu["Wlanec"].Cast<CheckBox>().CurrentValue; } }

        public static bool LastHitUseQ { get { return LastHitMenu["Qlasthit"].Cast<CheckBox>().CurrentValue; } }
        public static bool LastHitUseW { get { return LastHitMenu["Wlasthit"].Cast<CheckBox>().CurrentValue; } }
        public static bool LastHitUseR { get { return LastHitMenu["Rlasthit"].Cast<CheckBox>().CurrentValue; } }

        public static bool KsMode { get { return KillStealMenu["Uks"].Cast<CheckBox>().CurrentValue; } }
        public static bool KsUseQ { get { return KillStealMenu["Qks"].Cast<CheckBox>().CurrentValue; } }
        public static bool KsUseW { get { return KillStealMenu["Wks"].Cast<CheckBox>().CurrentValue; } }
        public static bool KsUseR { get { return KillStealMenu["Rks"].Cast<CheckBox>().CurrentValue; } }
        public static bool KsUseFlashR { get { return KillStealMenu["Fks"].Cast<CheckBox>().CurrentValue; } }

        public static bool DrawMode { get { return DrawingMenu["Udraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DrawQ { get { return DrawingMenu["Qdraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DrawW { get { return DrawingMenu["Wdraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DrawR { get { return DrawingMenu["Rdraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DrawFlashR { get { return DrawingMenu["Fdraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DesignerMode { get { return DrawingMenu["Udesign"].Cast<CheckBox>().CurrentValue; } }
        public static int DesignerSkin { get { return DrawingMenu["Sdesign"].Cast<Slider>().CurrentValue; } }

        public static bool LevelerMode { get { return SettingMenu["Ulevel"].Cast<CheckBox>().CurrentValue; } }
        public static bool StackMode { get { return SettingMenu["Ustack"].Cast<CheckBox>().CurrentValue; } }
        
        public static bool InterrupterMode { get { return SettingMenu["Uinterrupt"].Cast<CheckBox>().CurrentValue; } }
        public static bool InterrupterUseQ { get { return SettingMenu["Qinterrupt"].Cast<CheckBox>().CurrentValue; } }
        public static bool GapCloserMode { get { return SettingMenu["Ugapc"].Cast<CheckBox>().CurrentValue; } }
        public static bool GapCloserUseQ { get { return SettingMenu["Qgapc"].Cast<CheckBox>().CurrentValue; } }
    }
}