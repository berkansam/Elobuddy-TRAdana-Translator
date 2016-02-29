using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Kassawin
{
    internal class MenuX
    {
        public static Menu Kassawin, Combo, Harass, LaneClear, JungleClear, Misc;
        public static Slider SkinSelect;

        public static void GetMenu()
        {
            Kassawin = MainMenu.AddMenu("KassaWIN", "KassaWIN");
            Kassawin.AddGroupLabel("Free Win with Kassadin");
            Kassawin.AddSeparator();
            Kassawin.AddLabel("Made by Kk2");
            Kassawin.AddLabel("Çeviri-TRAdana");

            Combo = Kassawin.AddSubMenu("Kombo", "Combo");
            Combo.AddGroupLabel("Kombo Ayarları");
            Combo.AddSeparator();
            Combo.Add("ComboQ", new CheckBox("Komboda Q Kullan"));
            Combo.Add("ComboW", new CheckBox("Komboda W Kullan"));
            Combo.Add("ComboE", new CheckBox("Komboda E Kullan"));
            Combo.Add("ComboR", new CheckBox("Komboda R Kullan"));
            Combo.Add("IgniteToKill", new CheckBox("Hedef Ölecekse Tutuştur Kullan"));
            Combo.Add("sliderR", new Slider("R çevresinde en fazla şampiyon sayısı", 3, 1, 5));

            Harass = Kassawin.AddSubMenu("Dürtme", "Harass");
            Harass.AddGroupLabel("Dürtme Ayarları");
            Harass.AddSeparator();
            Harass.Add("HarassQ", new CheckBox("Dürtmede Q Kullan"));
            Harass.Add("HarassW", new CheckBox("Dürtmede W Kullan"));
            Harass.Add("HarassE", new CheckBox("Dürtmede R Kullan"));
            Harass.Add("manaPCTH", new Slider("Dürtme için gereken Mana", 20));

            LaneClear = Kassawin.AddSubMenu("LaneTemizleme", "LaneClear");
            LaneClear.AddGroupLabel("LaneTemizleme Ayarları");
            LaneClear.AddSeparator();
            LaneClear.Add("LaneQ", new CheckBox("LaneTemizlemede Q Kullan"));
            LaneClear.Add("LaneW", new CheckBox("LaneTemizleme W Kullan"));
            LaneClear.Add("LaneE", new CheckBox("LaneTemizleme E Kullan"));
            LaneClear.Add("manaPCTL", new Slider("LaneTemizleyici için gereken Mana", 20));

            JungleClear = Kassawin.AddSubMenu("Orman", "Jungle");
            JungleClear.AddGroupLabel("OrmanTemizleyici Ayarları");
            JungleClear.AddSeparator();
            JungleClear.Add("JungleQ", new CheckBox("Ormanda Q Kullan"));
            JungleClear.Add("JungleW", new CheckBox("Ormanda W Kullan"));
            JungleClear.Add("JungleE", new CheckBox("Ormanda E Kullan"));
            JungleClear.Add("manaPCTJ", new Slider("Orman Temizleyicisi için gereken Mana", 20));

            Misc = Kassawin.AddSubMenu("Ek", "Misc");
            Misc.AddGroupLabel("Ek Ayarlar");
            Misc.AddSeparator();
            Misc.Add("usePot", new CheckBox("Kullan İksir"));
            Misc.AddSeparator();
            Misc.Add("drawQ", new CheckBox("Göster Q Menzili"));
            Misc.Add("drawW", new CheckBox("Göster W Menzili"));
            Misc.Add("drawE", new CheckBox("Göster E Menzili"));
            Misc.Add("drawR", new CheckBox("Göster R Menzili"));
            Misc.AddSeparator();
            SkinSelect = Misc.Add("skinSelect", new Slider("Skin Değiştirici [Numarası]", 0, 0, 6));
        }
    }
}