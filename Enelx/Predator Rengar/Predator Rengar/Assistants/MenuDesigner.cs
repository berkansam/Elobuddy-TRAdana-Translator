using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Predator_Rengar
{
    public static class MenuDesigner
    {
        public const string MenuName = "Predator Rengar";

        public static readonly Menu RengarUI, ComboUI, ClearUI, KsUI, MiscUI;

        static MenuDesigner()
        {
            // Predator Rengar :: Main Menu
            RengarUI = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            RengarUI.AddGroupLabel("Bu gece bizim avımız!");
            RengarUI.AddSeparator();
            RengarUI.AddLabel("Geliştirici    :   Enelx");
            RengarUI.AddLabel("Versiyon          :   1.1.1.0");
            RengarUI.AddLabel("Çevirmen TRAdana");

            // Predator Rengar :: Combo Menu
            ComboUI = RengarUI.AddSubMenu("Kombo");
            ComboUI.AddGroupLabel("Kombo :: Büyüleri");
            ComboUI.Add("ComboQ", new CheckBox("Kullan Q"));
            ComboUI.Add("ComboW", new CheckBox("Kullan W"));
            ComboUI.Add("ComboE", new CheckBox("Kullan E"));
            ComboUI.AddSeparator();
            ComboUI.AddGroupLabel("Kombo :: Modu");
            ComboUI.Add("OneShot", new KeyBind("Tek Atma Aktif", false, KeyBind.BindTypes.PressToggle, "T".ToCharArray()[0]));

            // Predator Rengar :: Clear Menu
            ClearUI = RengarUI.AddSubMenu("Lane");
            ClearUI.AddGroupLabel("LaneTemizleme :: Büyüleri");
            ClearUI.Add("ClearQ", new CheckBox("Kullan Q"));
            ClearUI.Add("ClearW", new CheckBox("Kullan W"));
            ClearUI.Add("ClearE", new CheckBox("Kullan E"));
            ClearUI.AddSeparator();
            ClearUI.AddGroupLabel("LaneTemizleme :: Vahşet Yük");
            ClearUI.Add("SaveFerocity", new CheckBox("Yükü Sakla"));

            // Predator Rengar :: Killsteal Menu

            KsUI = RengarUI.AddSubMenu("Kill Çalma");
            KsUI.AddGroupLabel("Kill Çalma :: Büyüleri");
            KsUI.Add("KsW", new CheckBox("Kullan W"));
            KsUI.Add("KsE", new CheckBox("Kullan E"));

            // Predator Rengar :: Misc Menu
            MiscUI = RengarUI.AddSubMenu("Ek");
            MiscUI.AddGroupLabel("Ek :: Ayarları");
            MiscUI.Add("AutoHeal", new Slider("Otomatik Heal %", 20, 0, 100));
            MiscUI.AddSeparator();
            MiscUI.Add("InterE", new CheckBox("İnterrupt için E"));
            MiscUI.AddSeparator();
            MiscUI.AddGroupLabel("Misc :: Items");
            MiscUI.Add("UseTiamat", new CheckBox("Kullan Tiamat"));
            MiscUI.Add("UseHydra", new CheckBox("Kullan Hydra"));
            MiscUI.Add("UseTitanic", new CheckBox("Kullan Titanic"));
            MiscUI.Add("UseYoumuus", new CheckBox("Kullan Youmuu"));
            MiscUI.AddSeparator();
            MiscUI.AddGroupLabel("Ek :: Göster");
            MiscUI.Add("DrawCombo", new CheckBox("Göster Kombo Modu"));
        }

        public static void Initialize()
        {
        }
    }
}
