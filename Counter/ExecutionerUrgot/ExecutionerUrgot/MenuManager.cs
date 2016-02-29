using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace ExecutionerUrgot
{
    internal class MenuManager
    {
        // Create Main Segments
        public static Menu ExecutionerUrgotMenu,
            ComboMenu,
            HarassMenu,
            JungleMenu,
            LaneClearMenu,
            LastHitMenu,
            KillStealMenu,
            DrawingMenu,
            SettingMenu;

        public static void Initialize()
        {
            // Addon Menu
            ExecutionerUrgotMenu = MainMenu.AddMenu("ExecutionerUrgot", "ExecutionerUrgot");
            ExecutionerUrgotMenu.AddGroupLabel("Executioner Urgot");
            ExecutionerUrgotMenu.AddLabel("Çeviri TRAdana");

            // Combo Menu
            ComboMenu = ExecutionerUrgotMenu.AddSubMenu("Kombo Ayarları", "ComboFeatures");
            ComboMenu.AddGroupLabel("Kombo Ayarları");
            ComboMenu.AddLabel("Büyüler");
            ComboMenu.Add("Qcombo", new CheckBox("Kullan Q"));
            ComboMenu.Add("Wcombo", new CheckBox("Kullan W Yavaşlatmak için"));
            ComboMenu.Add("Ecombo", new CheckBox("Kullan E"));
            ComboMenu.AddLabel("Aşadağıdaki bölümü ayarlayın");
            ComboMenu.Add("Rcombo", new Slider("Kullan R -şu kadar hedef içinde", 3, 0, 5));

            // Harass Menu
            HarassMenu = ExecutionerUrgotMenu.AddSubMenu("Dürtme ", "HarassFeatures");
            HarassMenu.AddGroupLabel("Dürtme Ayarları");
            HarassMenu.AddLabel("Büyüler:");
            HarassMenu.Add("Qharass", new CheckBox("Kullan Q"));
            HarassMenu.Add("Eharass", new CheckBox("Kullan E"));
            HarassMenu.AddSeparator(1);
            HarassMenu.Add("Mharass", new Slider("Dürtme için en az mana %", 25));

            // Jungle Menu
            JungleMenu = ExecutionerUrgotMenu.AddSubMenu("OrmanTemizleme", "JungleFeatures");
            JungleMenu.AddGroupLabel("OrmanTemizleme Ayarları");
            JungleMenu.AddLabel("Büyüler:");
            JungleMenu.Add("Qjungle", new CheckBox("Kullan Q"));
            JungleMenu.Add("Ejungle", new CheckBox("Kullan E"));
            JungleMenu.AddSeparator(1);
            JungleMenu.Add("Mjungle", new Slider("Orman temizleme için gereken mana %", 25));

            // LaneClear Menu
            LaneClearMenu = ExecutionerUrgotMenu.AddSubMenu("LaneTemizleme ", "LaneClearFeatures");
            LaneClearMenu.AddGroupLabel("LaneTemizleme Ayarları");
            LaneClearMenu.AddLabel("Büyüler:");
            LaneClearMenu.Add("Qlanec", new CheckBox("Kullan Q"));
            LaneClearMenu.Add("Elanec", new CheckBox("Kullan E", false));
            LaneClearMenu.AddSeparator(1);
            LaneClearMenu.Add("Mlanec", new Slider("LaneTemizleme için en az mana %", 25));

            // LastHit Menu
            LastHitMenu = ExecutionerUrgotMenu.AddSubMenu("SonVuruş Ayarları", "LastHitFeatures");
            LastHitMenu.AddGroupLabel("SonVuruş Ayarları");
            LastHitMenu.AddLabel("Büyüler");
            LastHitMenu.Add("Qlasthit", new CheckBox("Kullan Q"));
            LastHitMenu.AddSeparator(1);
            LastHitMenu.Add("Mlasthit", new Slider("son vuruş için gereken mana", 25));

            // Kill Steal Menu
            KillStealMenu = ExecutionerUrgotMenu.AddSubMenu("Kill Çalma", "KSFeatures");
            KillStealMenu.AddGroupLabel("Kill Çalma Ayarları");
            KillStealMenu.Add("Uks", new CheckBox("KS Modu"));
            KillStealMenu.AddSeparator(1);
            KillStealMenu.AddLabel("Büyüler:");
            KillStealMenu.Add("Qks", new CheckBox("Q Kullan"));
            KillStealMenu.Add("Eks", new CheckBox("E Kullan", false));

            // Drawing Menu
            DrawingMenu = ExecutionerUrgotMenu.AddSubMenu("Gösterge", "DrawingFeatures");
            DrawingMenu.AddGroupLabel("Gösterge Ayarları");
            DrawingMenu.Add("Udrawer", new CheckBox("Gösterge Kullan"));
            DrawingMenu.AddSeparator(1);
            DrawingMenu.AddLabel("Büyüler");
            DrawingMenu.Add("Qdraw", new CheckBox("Göster Q"));
            DrawingMenu.Add("Edraw", new CheckBox("Göster E"));
            DrawingMenu.Add("Rdraw", new CheckBox("Göster R"));
            DrawingMenu.AddSeparator(1);
            DrawingMenu.AddLabel("Skin Değiştirici");
            DrawingMenu.Add("Udesigner", new CheckBox("Kullan"));
            DrawingMenu.Add("Sdesign", new Slider("Skin Numarası: ", 2, 0, 3));

            // Setting Menu
            SettingMenu = ExecutionerUrgotMenu.AddSubMenu("Ayarları", "Settings");
            SettingMenu.AddGroupLabel("Ayarları");
            SettingMenu.AddLabel("Otomatik Level Yükseltme");
            SettingMenu.Add("Uleveler", new CheckBox("Kullan"));
            SettingMenu.AddSeparator(1);
            SettingMenu.AddLabel("Otomatik Yük Kasma");
            SettingMenu.Add("Ustacker", new CheckBox("Kullan"));
            SettingMenu.AddSeparator(1);
            SettingMenu.AddLabel("Otomatik R kullan düşmanı dost kuleye çekecekse");
            SettingMenu.Add("Ugrabber", new CheckBox("Çekmeyi Kullan"));
            SettingMenu.AddSeparator(1);
            SettingMenu.AddLabel("Interrupter");
            SettingMenu.Add("Uinterrupt", new CheckBox("İnterrupt Kullan", false));
            SettingMenu.Add("Rinterrupt", new CheckBox("İnterrupt için R", false));
            SettingMenu.AddLabel("Gap Closer");
            SettingMenu.Add("Ugapc", new CheckBox("Kullan Gapcloser", false));
            SettingMenu.Add("Rgapc", new CheckBox("Gapcloser için  R", false));
        }

        // Assign Global Checks+
        public static bool ComboUseQ { get { return ComboMenu["Qcombo"].Cast<CheckBox>().CurrentValue; } }
        public static bool ComboUseW { get { return ComboMenu["Wcombo"].Cast<CheckBox>().CurrentValue; } }
        public static bool ComboUseE { get { return ComboMenu["Ecombo"].Cast<CheckBox>().CurrentValue; } }
        public static int ComboUseR { get { return ComboMenu["Rcombo"].Cast<Slider>().CurrentValue; } }

        public static bool HarassUseQ { get { return HarassMenu["Qharass"].Cast<CheckBox>().CurrentValue; } }
        public static bool HarassUseE { get { return HarassMenu["Eharass"].Cast<CheckBox>().CurrentValue; } }
        public static int HarassMana { get { return HarassMenu["Mharass"].Cast<Slider>().CurrentValue; } }

        public static bool JungleUseQ { get { return JungleMenu["Qjungle"].Cast<CheckBox>().CurrentValue; } }
        public static bool JungleUseE { get { return JungleMenu["Ejungle"].Cast<CheckBox>().CurrentValue; } }
        public static int JungleMana { get { return JungleMenu["Mjungle"].Cast<Slider>().CurrentValue; } }

        public static bool LaneClearUseQ { get { return LaneClearMenu["Qlanec"].Cast<CheckBox>().CurrentValue; } }
        public static bool LaneClearUseE { get { return LaneClearMenu["Elanec"].Cast<CheckBox>().CurrentValue; } }
        public static int LaneClearMana { get { return LaneClearMenu["Mlanec"].Cast<Slider>().CurrentValue; } }

        public static bool LastHitUseQ { get { return LastHitMenu["Qlasthit"].Cast<CheckBox>().CurrentValue; } }
        public static int LastHitMana { get { return LastHitMenu["Mlasthit"].Cast<Slider>().CurrentValue; } }

        public static bool KsMode { get { return KillStealMenu["Uks"].Cast<CheckBox>().CurrentValue; } }
        public static bool KsUseQ { get { return KillStealMenu["Qks"].Cast<CheckBox>().CurrentValue; } }
        public static bool KsUseE { get { return KillStealMenu["Eks"].Cast<CheckBox>().CurrentValue; } }

        public static bool DrawerMode { get { return DrawingMenu["Udrawer"].Cast<CheckBox>().CurrentValue; } }
        public static bool DrawQ { get { return DrawingMenu["Qdraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DrawE { get { return DrawingMenu["Edraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DrawR { get { return DrawingMenu["Rdraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DesignerMode { get { return DrawingMenu["Udesigner"].Cast<CheckBox>().CurrentValue; } }
        public static int DesignerSkin { get { return DrawingMenu["Sdesign"].Cast<Slider>().CurrentValue; } }

        public static bool LevelerMode { get { return SettingMenu["Uleveler"].Cast<CheckBox>().CurrentValue; } }
        public static bool StackerMode { get { return SettingMenu["Ustacker"].Cast<CheckBox>().CurrentValue; } }
        public static bool GrabberMode { get { return SettingMenu["Ugrabber"].Cast<CheckBox>().CurrentValue; } }

        public static bool InterrupterMode { get { return SettingMenu["Uinterrupt"].Cast<CheckBox>().CurrentValue; } }
        public static bool InterrupterUseR { get { return SettingMenu["Rinterrupt"].Cast<CheckBox>().CurrentValue; } }
        public static bool GapCloserMode { get { return SettingMenu["Ugapc"].Cast<CheckBox>().CurrentValue; } }
        public static bool GapCloserUseR { get { return SettingMenu["Rgapc"].Cast<CheckBox>().CurrentValue; } }
    }
}
