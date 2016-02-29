using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace _300_Pantheon.Assistants
{
    public static class MenuDesigner
    {
        public const string MenuName = "300 Pantheon";

        public static readonly Menu PantheonUi, ComboUi, HarassUi, ClearUi, KsUi, MiscUi;

        static MenuDesigner()
        {
            // 300 Pantheon :: Main Menu
            PantheonUi = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            PantheonUi.AddGroupLabel("THIS IS SCRIPTED !!!");
            PantheonUi.AddSeparator();
            PantheonUi.AddLabel("Geliştirici    :   Enelx");
            PantheonUi.AddLabel("Versiyon          :   2.0.0.0");
            PantheonUi.AddLabel("Çevirmen TRAdana");

            // 300 Pantheon :: Combo Menu
            ComboUi = PantheonUi.AddSubMenu("Kombo");
            ComboUi.AddGroupLabel("Kombo :: Büyüleri");
            ComboUi.Add("ComboQ", new CheckBox("Kullan Q"));
            ComboUi.Add("ComboW", new CheckBox("Kullan W"));
            ComboUi.Add("ComboE", new CheckBox("Kullan E"));

            // 300 Pantheon :: Harass Menu
            HarassUi = PantheonUi.AddSubMenu("Dürtme");
            HarassUi.AddGroupLabel("Dürtme :: Büyüleri");
            HarassUi.Add("HarassQ", new CheckBox("Kullan Q"));
            HarassUi.AddSeparator();
            HarassUi.AddGroupLabel("Dürtme :: Ayarları");
            HarassUi.Add("ToggleHarass",
                new KeyBind("Dürtme Tuşu", false, KeyBind.BindTypes.PressToggle, "T".ToCharArray()[0]));
            HarassUi.AddSeparator();
            HarassUi.Add("HarassMana", new Slider("En az mana %", 40));

            // 300 Pantheon :: Clear Menu
            ClearUi = PantheonUi.AddSubMenu("Clear");
            ClearUi.AddGroupLabel("SonVuruş :: Spells");
            ClearUi.Add("LastQ", new CheckBox("Kullan Q"));
            ClearUi.AddSeparator();
            ClearUi.AddGroupLabel("LaneTemizleme :: Büyüleri");
            ClearUi.Add("ClearQ", new CheckBox("Kullan Q"));
            ClearUi.Add("ClearW", new CheckBox("Kullan W"));
            ClearUi.Add("ClearE", new CheckBox("Kullan E"));
            ClearUi.AddSeparator();
            ClearUi.Add("ClearMana", new Slider("en az mana %", 50));
            ClearUi.AddSeparator();
            ClearUi.AddGroupLabel("OrmanTemizleme :: Büyüleri");
            ClearUi.Add("JungleQ", new CheckBox("Kullan Q"));
            ClearUi.Add("JungleW", new CheckBox("Kullan W"));
            ClearUi.Add("JungleE", new CheckBox("Kullan E"));

            // 300 Pantheon :: Killsteal Menu
            KsUi = PantheonUi.AddSubMenu("Killsteal");
            KsUi.AddGroupLabel("Killsteal :: Büyüleri");
            KsUi.Add("KsQ", new CheckBox("Kullan Q"));
            KsUi.Add("KsW", new CheckBox("Kullan W"));

            // 300 Pantheon :: Misc Menu
            MiscUi = PantheonUi.AddSubMenu("Ek");
            MiscUi.AddGroupLabel("Ek :: Ayarlar");
            MiscUi.Add("InterW", new CheckBox("İnterrupt için W"));
            MiscUi.Add("GapW", new CheckBox("AntiGapcloser için W"));
            MiscUi.AddSeparator();
            MiscUi.AddGroupLabel("Ek :: İtemler");
            MiscUi.Add("UseItems", new CheckBox("Agresif İtemleri Kullan"));
            MiscUi.AddSeparator();
            MiscUi.AddGroupLabel("Ek :: Göster");
            MiscUi.Add("DrawSpells", new CheckBox("Göster Q W E"));
        }

        public static void Initialize()
        {
        }
    }
}