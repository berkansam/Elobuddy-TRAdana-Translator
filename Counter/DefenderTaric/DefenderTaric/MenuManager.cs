using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace DefenderTaric
{
    internal class MenuManager
    {
        // Create Main Segments
        public static Menu DefenderTaricMenu, ComboMenu, HarassMenu, HealingMenu, DrawingMenu, SettingMenu;

        public static void Initialize()
        {
            // Addon Menu
            DefenderTaricMenu = MainMenu.AddMenu("Defender Taric", "DefenderTaric");
            DefenderTaricMenu.AddGroupLabel("Defender Taric");
            DefenderTaricMenu.AddLabel("Gems? Gems are truly outrageous. They are truly, truly, truly outrageous.");
            DefenderTaricMenu.AddLabel("Çeviri TRAdana");
            DefenderTaricMenu.Add("Ueaster", new CheckBox("Easter Egg"));

            // Combo Menu
            ComboMenu = DefenderTaricMenu.AddSubMenu("Kombo Ayarları", "ComboFeatures");
            ComboMenu.AddGroupLabel("Kombo Ayarları");
            ComboMenu.Add("Ucombo", new Slider("EWR Kombo", 1, 1, 2));
            ComboMenu.AddSeparator(1);
            ComboMenu.AddLabel("Büyüler:");
            ComboMenu.Add("Wcombo", new CheckBox("Kullan W"));
            ComboMenu.Add("Ecombo", new CheckBox("Kullan E"));
            ComboMenu.Add("Rcombo", new CheckBox("Kullan R"));
            ComboMenu.AddSeparator(1);
            ComboMenu.AddLabel("AA için büyü");
            ComboMenu.Add("Uweave", new CheckBox("SpellWeaving"));
            ComboMenu.Add("Qweave", new CheckBox("Use Q for Spellweaving", false));
            ComboMenu.AddSeparator(1);
            ComboMenu.Add("Lcombo", new Slider("Eğer canım şu kadarsa W kapat", 25));

            // Harass Menu
            HarassMenu = DefenderTaricMenu.AddSubMenu("Dürtme Ayarları", "HarassFeatures");
            HarassMenu.AddGroupLabel("Dürtme Ayarları");
            HarassMenu.AddLabel("Büyüler:");
            HarassMenu.Add("Wharass", new CheckBox("Kullan W"));
            HarassMenu.Add("Eharass", new CheckBox("Kullan E"));
            HarassMenu.AddSeparator(1);
            HarassMenu.Add("Mharass", new Slider("Kullanmak için gereken mana miktarı yüzde", 25));

            // Healing Menu
            HealingMenu = DefenderTaricMenu.AddSubMenu("Healing Ayarları", "HealingFeatures");
            HealingMenu.AddGroupLabel("CanBasma Ayarları");
            HealingMenu.Add("Uheal", new CheckBox("Q Kullan"));
            HealingMenu.AddSeparator(1);
            HealingMenu.AddLabel("Q ile otomatik can");
            HealingMenu.Add("Qhealally", new Slider("Dostların canı şundan azsa", 35));
            HealingMenu.Add("Qhealtaric", new Slider("Benim canım şundan azsa", 20));
            HealingMenu.AddSeparator(1);
            HealingMenu.AddLabel("Can basma mana yardımcısı");
            HealingMenu.Add("Mheal", new Slider("Kullanmak için gereken mana miktarı yüzde", 1));

            // Drawing Menu
            DrawingMenu = DefenderTaricMenu.AddSubMenu("Gösterge Ayarları", "DrawingFeatures");
            DrawingMenu.AddGroupLabel("Gösterge Ayarları");
            DrawingMenu.Add("Udraw", new CheckBox("Göster Mode"));
            DrawingMenu.AddSeparator(1);
            DrawingMenu.AddLabel("Büyüler:");
            DrawingMenu.Add("Qdraw", new CheckBox("Göster Q"));
            DrawingMenu.Add("Edraw", new CheckBox("Göster E"));
            DrawingMenu.Add("WRdraw", new CheckBox("Göster W & R"));
            DrawingMenu.AddSeparator(1);
            DrawingMenu.AddLabel("Skin Değiştirici");
            DrawingMenu.Add("Udesign", new CheckBox("Göster Skin Görünümü"));
            DrawingMenu.Add("Sdesign", new Slider("Skin Numarası: ", 0, 0, 3));

            // Setting Menu
            SettingMenu = DefenderTaricMenu.AddSubMenu("Ayarlar Modu", "SettingMode");
            SettingMenu.AddGroupLabel("Ayarlar Modu");
            SettingMenu.AddLabel("Otomatik Level Yükseltme");
            SettingMenu.Add("Ulevel", new CheckBox("Otomatik Level Yükseltme"));
            SettingMenu.AddSeparator(1);
            SettingMenu.AddLabel("Interrupter");
            SettingMenu.Add("Uinterrupt", new CheckBox("Interrupt Modu"));
            SettingMenu.Add("Einterrupt", new CheckBox("interruptda E Kullan"));
            SettingMenu.AddLabel("Gap Closer");
            SettingMenu.Add("Ugapc", new CheckBox("Gap Closer Modu"));
            SettingMenu.Add("Egapc", new CheckBox("Gapcloser için E kullan"));
        }

        // Assign Global Checks
        public static bool EasterEgg { get { return DefenderTaricMenu["Ueaster"].Cast<CheckBox>().CurrentValue; } }

        public static int ComboMode { get { return ComboMenu["Ucombo"].Cast<Slider>().CurrentValue; } }
        public static bool ComboUseW { get { return ComboMenu["Wcombo"].Cast<CheckBox>().CurrentValue; } }
        public static bool ComboUseE { get { return ComboMenu["Ecombo"].Cast<CheckBox>().CurrentValue; } }
        public static bool ComboUseR { get { return ComboMenu["Rcombo"].Cast<CheckBox>().CurrentValue; } }
        public static int ComboWLimit { get { return ComboMenu["Lcombo"].Cast<Slider>().CurrentValue; } }
        public static bool SpellWeave { get { return ComboMenu["Uweave"].Cast<CheckBox>().CurrentValue; } }
        public static bool SpellWeaveUseQ { get { return ComboMenu["Qweave"].Cast<CheckBox>().CurrentValue; } }

        public static bool HarassUseW { get { return HarassMenu["Wharass"].Cast<CheckBox>().CurrentValue; } }
        public static bool HarassUseE { get { return HarassMenu["Eharass"].Cast<CheckBox>().CurrentValue; } }
        public static int HarassMana { get { return HarassMenu["Mharass"].Cast<Slider>().CurrentValue; } }

        public static bool HealingMode { get { return HealingMenu["Uheal"].Cast<CheckBox>().CurrentValue; } }
        public static int HealingAlly { get { return HealingMenu["Qhealally"].Cast<Slider>().CurrentValue; } }
        public static int HealingSelf { get { return HealingMenu["Qhealtaric"].Cast<Slider>().CurrentValue; } }
        public static int HealingMana { get { return HealingMenu["Mheal"].Cast<Slider>().CurrentValue; } }

        public static bool DrawerMode { get { return DrawingMenu["Udraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DrawQ { get { return DrawingMenu["Qdraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DrawE { get { return DrawingMenu["Edraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DrawWr { get { return DrawingMenu["WRdraw"].Cast<CheckBox>().CurrentValue; } }
        public static bool DesignerMode { get { return DrawingMenu["Udesign"].Cast<CheckBox>().CurrentValue; } }
        public static int DesignerSkin { get { return DrawingMenu["Sdesign"].Cast<Slider>().CurrentValue; } }

        public static bool LevelerMode { get { return SettingMenu["Ulevel"].Cast<CheckBox>().CurrentValue; } }

        public static bool InterrupterMode { get { return SettingMenu["Uinterrupt"].Cast<CheckBox>().CurrentValue; } }
        public static bool InterrupterUseE { get { return SettingMenu["Einterrupt"].Cast<CheckBox>().CurrentValue; } }
        public static bool GapCloserMode { get { return SettingMenu["Ugapc"].Cast<CheckBox>().CurrentValue; } }
        public static bool GapCloserUseE { get { return SettingMenu["Egapc"].Cast<CheckBox>().CurrentValue; } }
    }
}
