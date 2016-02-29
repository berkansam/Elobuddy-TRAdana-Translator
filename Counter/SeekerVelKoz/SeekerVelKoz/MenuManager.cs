using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace SeekerVelKoz
{
    internal class MenuManager
    {
        // Create Main Segments
        public static Menu SeekerVelKozMenu, ComboMenu, HarassMenu, JungleMenu, LaneClearMenu, LastHitMenu, KillStealMenu, DrawingMenu, SettingMenu;

        public static void Initialize()
        {
            // Addon Menu
            SeekerVelKozMenu = MainMenu.AddMenu("Seeker Vel'Koz", "SeekerVelKoz");
            SeekerVelKozMenu.AddGroupLabel("Seeker Vel'Koz");
            SeekerVelKozMenu.AddLabel("Çevirmen TRAdana");

            // Combo Menu
            ComboMenu = SeekerVelKozMenu.AddSubMenu("Kombo", "ComboFeatures");
            ComboMenu.AddGroupLabel("Kombo Ayarları");
            ComboMenu.AddLabel("Büyüler");
            ComboMenu.Add("Qcombo", new CheckBox("Kullan Q"));
            ComboMenu.Add("Wcombo", new CheckBox("Kullan W"));
            ComboMenu.Add("Ecombo", new CheckBox("Kullan E"));
            ComboMenu.Add("Rcombo", new CheckBox("Kullan R"));
            ComboMenu.AddSeparator(1);
            ComboMenu.Add("Wlimit", new CheckBox("Sadece W ile vurup dön", false));
            ComboMenu.Add("Elimit", new CheckBox("Sadece E ile yavaşlat", false));
            ComboMenu.Add("Rcool", new CheckBox("Sadece R eğer büyüler Bekleme süresindeyse"));
            ComboMenu.Add("Rlimit", new Slider("R Kullan eğer menzilde şu kadar hedef varsa >=", 4, 1, 5));

            // Harass Menu
            HarassMenu = SeekerVelKozMenu.AddSubMenu("Dürtme", "HarassFeatures");
            HarassMenu.AddGroupLabel("Dürtme Ayarları");
            HarassMenu.AddLabel("Büyüler");
            HarassMenu.Add("Qharass", new CheckBox("Kullan Q"));
            HarassMenu.Add("Eharass", new CheckBox("Kullan E", false));
            HarassMenu.AddSeparator(1);
            HarassMenu.Add("Mharass", new Slider("Dürtme büyüleri için gereken mana %", 25));

            // Jungle Menu
            JungleMenu = SeekerVelKozMenu.AddSubMenu("OrmanTemizleme", "JungleFeatures");
            JungleMenu.AddGroupLabel("OrmanTemizleme Ayarları");
            JungleMenu.AddLabel("Büyüler");
            JungleMenu.Add("Qjungle", new CheckBox("Kullan Q"));
            JungleMenu.Add("Wjungle", new CheckBox("Kullan W"));
            JungleMenu.Add("Ejungle", new CheckBox("Kullan E"));
            JungleMenu.AddSeparator(1);
            JungleMenu.Add("Mjungle", new Slider("Orman Temizleme büyüleri için gereken mana %", 25));

            // LaneClear Menu
            LaneClearMenu = SeekerVelKozMenu.AddSubMenu("LaneTemizleme", "LaneClearFeatures");
            LaneClearMenu.AddGroupLabel("LaneTemizleme Ayarları");
            LaneClearMenu.AddLabel("Büyüler");
            LaneClearMenu.Add("Qlanec", new CheckBox("Kullan Q", false));
            LaneClearMenu.Add("Wlanec", new CheckBox("Kullan W", false));
            LaneClearMenu.Add("Elanec", new CheckBox("Kullan E", false));
            LaneClearMenu.AddSeparator(1);
            LaneClearMenu.Add("Mlanec", new Slider("LaneTemizleme büyüleri için gereken mana %", 25));

            // LastHit Menu
            LastHitMenu = SeekerVelKozMenu.AddSubMenu("SonVuruş", "LastHitFeatures");
            LastHitMenu.AddGroupLabel("SonVuruş Ayarları");
            LastHitMenu.AddLabel("Büyüler");
            LastHitMenu.Add("Qlasthit", new CheckBox("Kullan Q", false));
            LastHitMenu.Add("Wlasthit", new CheckBox("Kullan W", false));
            LastHitMenu.Add("Elasthit", new CheckBox("Kullan E", false));
            LastHitMenu.AddSeparator(1);
            LastHitMenu.Add("Mlasthit", new Slider("SonVuruş büyüleri için gereken mana %", 25));

            // Kill Steal Menu
            KillStealMenu = SeekerVelKozMenu.AddSubMenu("Kill Çalma", "KSFeatures");
            KillStealMenu.AddGroupLabel("Kill Çalma Ayarları");
            KillStealMenu.Add("Uks", new CheckBox("KS Mode"));
            KillStealMenu.AddSeparator(1);
            KillStealMenu.AddLabel("Büyüler");
            KillStealMenu.Add("Qks", new CheckBox("Q Kullan"));
            KillStealMenu.Add("Wks", new CheckBox("W Kullan", false));
            KillStealMenu.Add("Eks", new CheckBox("E Kullan", false));
            KillStealMenu.Add("Rks", new CheckBox("R Kullan"));
            KillStealMenu.Add("Kslimit", new Slider("R için gereken düşman >=", 2, 1, 5));

            // Drawing Menu
            DrawingMenu = SeekerVelKozMenu.AddSubMenu("Göstergeler", "DrawingFeatures");
            DrawingMenu.AddGroupLabel("Gösterge Ayarları");
            DrawingMenu.Add("Udraw", new CheckBox("Gösterge modu"));
            DrawingMenu.AddSeparator(1);
            DrawingMenu.AddLabel("Büyüler");
            DrawingMenu.Add("QWdraw", new CheckBox("Göster Q & W"));
            DrawingMenu.Add("Edraw", new CheckBox("Göster E"));
            DrawingMenu.Add("Rdraw", new CheckBox("Göster R"));
            DrawingMenu.AddSeparator(1);
            DrawingMenu.AddLabel("Skin Değiştirici");
            DrawingMenu.Add("Udesign", new CheckBox("Değiştirici"));
            DrawingMenu.Add("Sdesign", new Slider("Skin Numarası: ", 0, 0, 2));

            // Setting Menu
            SettingMenu = SeekerVelKozMenu.AddSubMenu("Ayarlar", "Settings");
            SettingMenu.AddGroupLabel("Ayarlar");
            SettingMenu.AddLabel("Otomatik Level Yükseltme");
            SettingMenu.Add("Ulevel", new CheckBox("Otomatik Level Yükseltme"));
            SettingMenu.AddSeparator(1);
            SettingMenu.AddLabel("Ultimate Takipçisi");
            SettingMenu.Add("Uultimate", new CheckBox("Ulti Takipçi Kullan"));
            SettingMenu.AddLabel("Otomatik Yük Kasma");
            SettingMenu.Add("Ustack", new CheckBox("Yük Modu"));
            SettingMenu.AddLabel("Interrupter");
            SettingMenu.Add("Uinterrupt", new CheckBox("Interrupt Mode"));
            SettingMenu.Add("Einterrupt", new CheckBox("İnterrupt için E"));
            SettingMenu.AddSeparator(1);
            SettingMenu.AddLabel("Gap Closer");
            SettingMenu.Add("Ugapc", new CheckBox("Gap Closer Modu"));
            SettingMenu.Add("Egapc", new CheckBox("Gapcloser için E"));
        }

        // Assign Global Checks+
        public static bool ComboUseQ { get { return ComboMenu["Qcombo"].Cast<CheckBox>().CurrentValue; } }
        public static bool ComboUseW { get { return ComboMenu["Wcombo"].Cast<CheckBox>().CurrentValue; } }
        public static bool ComboUseE { get { return ComboMenu["Ecombo"].Cast<CheckBox>().CurrentValue; } }
        public static bool ComboUseR { get { return ComboMenu["Rcombo"].Cast<CheckBox>().CurrentValue; } }
        public static bool ComboWKnock { get { return ComboMenu["Wlimit"].Cast<CheckBox>().CurrentValue; } }
        public static bool ComboESlow { get { return ComboMenu["Elimit"].Cast<CheckBox>().CurrentValue; } }
        public static bool ComboRCooldown { get { return ComboMenu["Rcool"].Cast<CheckBox>().CurrentValue; } }
        public static int ComboRLimiter { get { return ComboMenu["Rlimit"].Cast<Slider>().CurrentValue; } }

        public static bool HarassUseQ { get { return HarassMenu["Qharass"].Cast<CheckBox>().CurrentValue; } }
        public static bool HarassUseE { get { return HarassMenu["Eharass"].Cast<CheckBox>().CurrentValue; } }
        public static int HarassMana { get { return HarassMenu["Mharass"].Cast<Slider>().CurrentValue; } }

        public static bool JungleUseQ { get { return JungleMenu["Qjungle"].Cast<CheckBox>().CurrentValue; } }
        public static bool JungleUseW { get { return JungleMenu["Wjungle"].Cast<CheckBox>().CurrentValue; } }
        public static bool JungleUseE { get { return JungleMenu["Ejungle"].Cast<CheckBox>().CurrentValue; } }
        public static int JungleMana { get { return JungleMenu["Mjungle"].Cast<Slider>().CurrentValue; } }

        public static bool LaneClearUseQ { get { return LaneClearMenu["Qlanec"].Cast<CheckBox>().CurrentValue; } }
        public static bool LaneClearUseW { get { return LaneClearMenu["Wlanec"].Cast<CheckBox>().CurrentValue; } }
        public static bool LaneClearUseE { get { return LaneClearMenu["Elanec"].Cast<CheckBox>().CurrentValue; } }
        public static int LaneClearMana { get { return LaneClearMenu["Mlanec"].Cast<Slider>().CurrentValue; } }

        public static bool LastHitUseQ { get { return LastHitMenu["Qlasthit"].Cast<CheckBox>().CurrentValue; } }
        public static bool LastHitUseW { get { return LastHitMenu["Wlasthit"].Cast<CheckBox>().CurrentValue; } }
        public static bool LastHitUseE { get { return LastHitMenu["Elasthit"].Cast<CheckBox>().CurrentValue; } }
        public static int LastHitMana { get { return LastHitMenu["Mlasthit"].Cast<Slider>().CurrentValue; } }

        public static bool KsMode { get { return KillStealMenu["Uks"].Cast<CheckBox>().CurrentValue; } }
        public static bool KsUseQ { get { return KillStealMenu["Qks"].Cast<CheckBox>().CurrentValue; } }
        public static bool KsUseW { get { return KillStealMenu["Wks"].Cast<CheckBox>().CurrentValue; } }
        public static bool KsUseE { get { return KillStealMenu["Eks"].Cast<CheckBox>().CurrentValue; } }
        public static bool KsUseR { get { return KillStealMenu["Rks"].Cast<CheckBox>().CurrentValue; } }
        public static int KsUltLimiter { get { return KillStealMenu["Kslimit"].Cast<Slider>().CurrentValue; } }

        public static bool DrawerMode { get { return DrawingMenu["Udraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DrawQw { get { return DrawingMenu["QWdraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DrawE { get { return DrawingMenu["Edraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DrawR { get { return DrawingMenu["Rdraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DesignerMode { get { return DrawingMenu["Udesign"].Cast<CheckBox>().CurrentValue; } }
        public static int DesignerSkin { get { return DrawingMenu["Sdesign"].Cast<Slider>().CurrentValue; } }

        public static bool LevelerMode { get { return SettingMenu["Ulevel"].Cast<CheckBox>().CurrentValue; } }
        public static bool StackMode { get { return SettingMenu["Ustack"].Cast<CheckBox>().CurrentValue; } }
        public static bool UltimateFollower { get { return SettingMenu["Uultimate"].Cast<CheckBox>().CurrentValue; } }

        public static bool InterrupterMode { get { return SettingMenu["Uinterrupt"].Cast<CheckBox>().CurrentValue; } }
        public static bool InterrupterUseE { get { return SettingMenu["Einterrupt"].Cast<CheckBox>().CurrentValue; } }
        public static bool GapCloserMode { get { return SettingMenu["Ugapc"].Cast<CheckBox>().CurrentValue; } }
        public static bool GapCloserUseE { get { return SettingMenu["Egapc"].Cast<CheckBox>().CurrentValue; } }
    }
}
