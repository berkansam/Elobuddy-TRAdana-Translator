using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace KiteMachineKogMaw
{
    internal class MenuManager
    {
        // Create Main Segments
        public static Menu KiteMachineKogMawMenu, ComboMenu, HarassMenu, JungleMenu, LaneClearMenu, LastHitMenu, KillStealMenu, DrawingMenu, SettingMenu, ItemMenu;

        public static void Initialize()
        {
            // Addon Menu
            KiteMachineKogMawMenu = MainMenu.AddMenu("BallistaKogMaw", "BallistaKogMaw");
            KiteMachineKogMawMenu.AddGroupLabel("Ballista Kog'Maw-Ceviri TRAdana");

            // Combo Menu
            ComboMenu = KiteMachineKogMawMenu.AddSubMenu("Combo Ayarları", "ComboFeatures");
            ComboMenu.AddGroupLabel("Combo Ayarları");
            ComboMenu.AddLabel("Independent boxes for Spells:");
            ComboMenu.Add("Qcombo", new CheckBox("Q Kullan"));
            ComboMenu.Add("Wcombo", new CheckBox("W Kullan"));
            ComboMenu.Add("Ecombo", new CheckBox("E Kullan"));
            ComboMenu.Add("Rcombo", new CheckBox("R Kullan"));
            ComboMenu.Add("Scombo", new Slider("en fazla R yükü", 1, 1, 10));
            ComboMenu.AddSeparator(1);
            ComboMenu.Add("Mcombo", new Slider("Mana limitleyici mana %", 25));

            // Harass Menu
            HarassMenu = KiteMachineKogMawMenu.AddSubMenu("Dürtme Ayarları", "HarassFeatures");
            HarassMenu.AddGroupLabel("Dürtme Ayarları");
            HarassMenu.AddLabel("Independent boxes for Spells:");
            HarassMenu.Add("Qharass", new CheckBox("Q Kullan"));
            HarassMenu.Add("Wharass", new CheckBox("W Kullan", false));
            HarassMenu.Add("Eharass", new CheckBox("E Kullan", false));
            HarassMenu.Add("Rharass", new CheckBox("R Kullan", false));
            HarassMenu.Add("Sharass", new Slider("en fazla R yükü", 1, 1, 10));
            HarassMenu.AddSeparator(1);
            HarassMenu.Add("Mharass", new Slider("Mana Limitleyici en az mana %", 25));

            // Jungle Menu
            JungleMenu = KiteMachineKogMawMenu.AddSubMenu("Orman Ayarları", "JungleFeatures");
            JungleMenu.AddGroupLabel("Orman Ayarları");
            JungleMenu.AddLabel("Independent boxes for Spells:");
            JungleMenu.Add("Qjungle", new CheckBox("Q Kullan"));
            JungleMenu.Add("Wjungle", new CheckBox("W Kullan"));
            JungleMenu.Add("Rjungle", new CheckBox("R Kullan", false));
            JungleMenu.Add("Sjungle", new Slider("En fazla R yükü", 1, 1, 10));
            JungleMenu.AddSeparator(1);
            JungleMenu.Add("Mjungle", new Slider("Mana Limitleyici en az mana %", 25));

            // LaneClear Menu
            LaneClearMenu = KiteMachineKogMawMenu.AddSubMenu("Lane Clear Ayarları", "LaneClearFeatures");
            LaneClearMenu.AddGroupLabel("Lane Clear Ayarları");
            LaneClearMenu.AddLabel("Independent boxes for Spells:");
            LaneClearMenu.Add("Qlanec", new CheckBox("Q Kullan", false));
            LaneClearMenu.Add("Wlanec", new CheckBox("W Kullan", false));
            LaneClearMenu.Add("Rlanec", new CheckBox("R Kullan", false));
            LaneClearMenu.Add("Slanec", new Slider("en fazla R yükü", 1, 1, 10));
            LaneClearMenu.AddSeparator(1);
            LaneClearMenu.Add("Mlanec", new Slider("Mana Limitleyici en az mana %", 25));

            // LastHit Menu
            LastHitMenu = KiteMachineKogMawMenu.AddSubMenu("Last Hit Ayarları", "LastHitFeatures");
            LastHitMenu.AddGroupLabel("Last Hit Ayarları");
            LastHitMenu.AddLabel("Independent boxes for Spells:");
            LastHitMenu.Add("Qlasthit", new CheckBox("Q Kullan"));
            LastHitMenu.Add("Rlasthit", new CheckBox("R Kullan", false));
            LastHitMenu.Add("Slasthit", new Slider("En fazla R yükü", 1, 1, 10));
            LastHitMenu.AddSeparator(1);
            LastHitMenu.Add("Mlasthit", new Slider("Mana Limitleyici en az mana %", 25));

            // Kill Steal Menu
            KillStealMenu = KiteMachineKogMawMenu.AddSubMenu("KS Ayarları", "KSFeatures");
            KillStealMenu.AddGroupLabel("Kill Çalma Ayarları");
            KillStealMenu.Add("Uks", new CheckBox("KS Modu"));
            KillStealMenu.AddSeparator(1);
            KillStealMenu.AddLabel("Independent boxes for Spells:");
            KillStealMenu.Add("Qks", new CheckBox("KS'de Q kullan"));
            KillStealMenu.Add("Rks", new CheckBox("KS'de R kullan"));

            // Drawing Menu
            DrawingMenu = KiteMachineKogMawMenu.AddSubMenu("Gösterge Ayarları", "DrawingFeatures");
            DrawingMenu.AddGroupLabel("Gösterge Ayarları");
            DrawingMenu.Add("Udrawer", new CheckBox("Use Drawer"));
            DrawingMenu.AddSeparator(1);
            DrawingMenu.AddLabel("Independent boxes for Spells:");
            DrawingMenu.Add("Qdraw", new CheckBox("Göster Q"));
            DrawingMenu.Add("Wdraw", new CheckBox("Göster W"));
            DrawingMenu.Add("Edraw", new CheckBox("Göster E"));
            DrawingMenu.Add("Rdraw", new CheckBox("Göster R"));
            DrawingMenu.AddSeparator(1);
            DrawingMenu.AddLabel("Skin Tasarımcısı");
            DrawingMenu.Add("Udesigner", new CheckBox("Tasarımcı Kullan"));
            DrawingMenu.Add("Sdesign", new Slider("Skin Tasarımı: ", 7, 0, 8));

            // Setting Menu
            SettingMenu = KiteMachineKogMawMenu.AddSubMenu("Ayarlar", "Settings");
            SettingMenu.AddGroupLabel("Ayarlar");
            SettingMenu.AddLabel("Otomatik Level");
            SettingMenu.Add("Uleveler", new CheckBox("Kullan"));
            SettingMenu.AddSeparator(1);
            SettingMenu.AddLabel("Otomatik Gözyaşı Biriktir");
            SettingMenu.Add("Ustacker", new CheckBox("Biriktirici Kullan"));
            SettingMenu.AddSeparator(1);
            SettingMenu.AddLabel("Otomatik Pasif - Ölü Takipçi");
            SettingMenu.Add("Ufollower", new CheckBox("Takipçi Kullan"));
            SettingMenu.AddSeparator(1);
            SettingMenu.AddLabel("Gap Closer");
            SettingMenu.Add("Ugapc", new CheckBox("Gapcloser Kullan"));
            SettingMenu.Add("Egapc", new CheckBox("Gapclose için E Kullan"));
            SettingMenu.AddSeparator(1);

            //Item Menu

           




        }


        // Assign Global Checks+
        public static bool ComboUseQ { get { return ComboMenu["Qcombo"].Cast<CheckBox>().CurrentValue; } }
        public static bool ComboUseW { get { return ComboMenu["Wcombo"].Cast<CheckBox>().CurrentValue; } }
        public static bool ComboUseE { get { return ComboMenu["Ecombo"].Cast<CheckBox>().CurrentValue; } }
        public static bool ComboUseR { get { return ComboMenu["Rcombo"].Cast<CheckBox>().CurrentValue; } }
        public static int ComboStacks { get { return ComboMenu["Scombo"].Cast<Slider>().CurrentValue; } }
        public static int ComboMana { get { return ComboMenu["Mcombo"].Cast<Slider>().CurrentValue; } }

        public static bool HarassUseQ { get { return HarassMenu["Qharass"].Cast<CheckBox>().CurrentValue; } }
        public static bool HarassUseW { get { return HarassMenu["Wharass"].Cast<CheckBox>().CurrentValue; } }
        public static bool HarassUseE { get { return HarassMenu["Eharass"].Cast<CheckBox>().CurrentValue; } }
        public static bool HarassUseR { get { return HarassMenu["Rharass"].Cast<CheckBox>().CurrentValue; } }
        public static int HarassStacks { get { return HarassMenu["Sharass"].Cast<Slider>().CurrentValue; } }
        public static int HarassMana { get { return HarassMenu["Mharass"].Cast<Slider>().CurrentValue; } }

        public static bool JungleUseQ { get { return JungleMenu["Qjungle"].Cast<CheckBox>().CurrentValue; } }
        public static bool JungleUseW { get { return JungleMenu["Wjungle"].Cast<CheckBox>().CurrentValue; } }
        public static bool JungleUseE { get { return JungleMenu["Ejungle"].Cast<CheckBox>().CurrentValue; } }
        public static bool JungleUseR { get { return JungleMenu["Rjungle"].Cast<CheckBox>().CurrentValue; } }
        public static int JungleStacks { get { return JungleMenu["Sjungle"].Cast<Slider>().CurrentValue; } }
        public static int JungleMana { get { return JungleMenu["Mjungle"].Cast<Slider>().CurrentValue; } }

        public static bool LaneClearUseQ { get { return LaneClearMenu["Qlanec"].Cast<CheckBox>().CurrentValue; } }
        public static bool LaneClearUseW { get { return LaneClearMenu["Wlanec"].Cast<CheckBox>().CurrentValue; } }
        public static bool LaneClearUseE { get { return LaneClearMenu["Elanec"].Cast<CheckBox>().CurrentValue; } }
        public static bool LaneClearUseR { get { return LaneClearMenu["Rlanec"].Cast<CheckBox>().CurrentValue; } }
        public static int LaneClearStacks { get { return LaneClearMenu["Slanec"].Cast<Slider>().CurrentValue; } }
        public static int LaneClearMana { get { return LaneClearMenu["Mlanec"].Cast<Slider>().CurrentValue; } }

        public static bool LastHitUseQ { get { return LastHitMenu["Qlasthit"].Cast<CheckBox>().CurrentValue; } }
        public static bool LastHitUseR { get { return LastHitMenu["Rlasthit"].Cast<CheckBox>().CurrentValue; } }
        public static int LastHitStacks { get { return LastHitMenu["Slasthit"].Cast<Slider>().CurrentValue; } }
        public static int LastHitMana { get { return LastHitMenu["Mlasthit"].Cast<Slider>().CurrentValue; } }

        public static bool KsMode { get { return KillStealMenu["Uks"].Cast<CheckBox>().CurrentValue; } }
        public static bool KsUseQ { get { return KillStealMenu["Qks"].Cast<CheckBox>().CurrentValue; } }
        public static bool KsUseR { get { return KillStealMenu["Rks"].Cast<CheckBox>().CurrentValue; } }

        public static bool DrawMode { get { return DrawingMenu["Udrawer"].Cast<CheckBox>().CurrentValue; } }
        public static bool DrawQ { get { return DrawingMenu["Qdraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DrawW { get { return DrawingMenu["Wdraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DrawE { get { return DrawingMenu["Edraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DrawR { get { return DrawingMenu["Rdraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DesignerMode { get { return DrawingMenu["Udesigner"].Cast<CheckBox>().CurrentValue; } }
        public static int DesignerSkin { get { return DrawingMenu["Sdesign"].Cast<Slider>().CurrentValue; } }

        public static bool LevelerMode { get { return SettingMenu["Uleveler"].Cast<CheckBox>().CurrentValue; } }
        public static bool StackerMode { get { return SettingMenu["Ustacker"].Cast<CheckBox>().CurrentValue; } }
        public static bool FollowerMode { get { return SettingMenu["Ufollower"].Cast<CheckBox>().CurrentValue; } }

        public static bool GapCloserMode { get { return SettingMenu["Ugapc"].Cast<CheckBox>().CurrentValue; } }
        public static bool GapCloserUseE { get { return SettingMenu["Egapc"].Cast<CheckBox>().CurrentValue; } }

        //Add ItemManager
    
    }
}
