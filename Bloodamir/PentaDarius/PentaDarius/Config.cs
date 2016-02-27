using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace PentaDarius
{
    public class Config
    {
        public static Menu Menu, DrawMenu, DebugMenu, SpellMenu, OrbMenu;
        private static readonly string[] SkinName =
        {
            "Classic", "Lord Darius", "Bioforge Darius", "Woad King Darius", "Dunkmaster Darius", "Academy Darius"
        };

        static Config()
        {
            Menu = MainMenu.AddMenu("Penta Darius", "Penta Darius");
            Menu.AddGroupLabel("Penta Darius Bloodamir çeviri tradana");
            Menu.Add("damageIndicator", new CheckBox("Hasar Planlama", true));
            Menu.Add("drawing", new CheckBox("Aktif Gösterge", true));
            Menu.Add("useItem", new CheckBox("Item Kullan ", true));
            Menu.AddSeparator();
            Menu.Add("skin", new CheckBox("Skin Değiştirici", false));
            Menu.Add("sID", new ComboBox("Skin Numarası: ", 0, SkinName));
            Menu.AddSeparator();
            Menu.Add("debug", new CheckBox("Ayar Sıfırlama  (Gerekir F5)", false));
            Menu.AddSeparator();
            Menu.AddGroupLabel("Credit");
            Menu.AddLabel("Addon - by Tychus");
            Menu.AddLabel("DamageIndicator - by Fluxy");
            Menu.AddLabel("Translate Turkish-by TRAdana");

            #region Orbwalk Menu
            OrbMenu = Menu.AddSubMenu("Orbwalk mode");
            OrbMenu.AddGroupLabel("Kombo");
            OrbMenu.Add("useQcombo", new CheckBox("Q Kullan", true));
            OrbMenu.Add("useWcombo", new CheckBox("W Kullan", true));
            OrbMenu.Add("useEcombo", new CheckBox("E Kullan", true));
            OrbMenu.Add("useRcombo", new CheckBox("R Kullan", true));
            OrbMenu.AddSeparator();

            OrbMenu.AddGroupLabel("Dürtme");
            OrbMenu.Add("useQharass", new CheckBox("Q Kullan", false));
            OrbMenu.Add("useWharass", new CheckBox("W Kullan", true));
            OrbMenu.Add("useEharass", new CheckBox("E Kullan", false));
            OrbMenu.Add("useRharass", new CheckBox("R Kullan", false));
            OrbMenu.AddSeparator();
            
            OrbMenu.AddGroupLabel("Lane Temizleme");
            OrbMenu.Add("useQlaneclear", new CheckBox("Q Kullan", false));
            OrbMenu.Add("useWlaneclear", new CheckBox("W Kullan", true));
            OrbMenu.Add("laneMana", new Slider("En az mana %", 40, 0, 100));
            OrbMenu.AddSeparator();

            OrbMenu.AddGroupLabel("Jungle Temizleme");
            OrbMenu.Add("useQjungleclear", new CheckBox("Q Kullan", false));
            OrbMenu.Add("useWjungleclear", new CheckBox("W Kullan", true));
            OrbMenu.Add("jungleMana", new Slider("En az Mana %", 40, 0, 100));
            OrbMenu.AddSeparator();
            #endregion

            #region Spell Menu
            SpellMenu = Menu.AddSubMenu("Büyü Ayarları");
            //SpellMenu.AddGroupLabel("Q");
            //SpellMenu.Add("moveAssist", new CheckBox("Movement assist", true));
            SpellMenu.AddGroupLabel("W");
            SpellMenu.Add("aaReset", new CheckBox("AA Sıfırlama", true));
            SpellMenu.Add("towerW", new CheckBox("Kulede", true));
            SpellMenu.AddGroupLabel("E");
            //SpellMenu.Add("flashE", new KeyBind("Flash E", false, KeyBind.BindTypes.HoldActive, 'T'));
            SpellMenu.Add("dashE", new CheckBox("Vur E", true));
            SpellMenu.Add("interruptE", new CheckBox("Interrupt E", true));
            SpellMenu.Add("towerE", new CheckBox("Kule E", true));
            SpellMenu.Add("minErange", new Slider("En az E Menzili", 450, 0, 550));
            SpellMenu.AddGroupLabel("R");
            //SpellMenu.Add("flashR", new KeyBind("Flash R", false, KeyBind.BindTypes.HoldActive, 'G'));
            SpellMenu.Add("autoR", new KeyBind("Otomatik R Aktif", true, KeyBind.BindTypes.PressToggle, 'U'));
            SpellMenu.Add("saveRMana", new CheckBox("R için Mana Sakla", true));
            SpellMenu.Add("unneR", new CheckBox("Eğer Gereksizse Kullanma", true));
            //SpellMenu.Add("dieR", new CheckBox("Use R before die", true));
            SpellMenu.AddGroupLabel("Ignite");
            SpellMenu.Add("useIgnite", new CheckBox("Tutuştur Kullan", true));
            SpellMenu.Add("1tick", new CheckBox("Hızlı Kill", true));
            SpellMenu.Add("igniteTick", new Slider("Tutuştur hasarı 1-5 saniye arası", 4, 1, 5));    // 다이나믹 슬라이더
            #endregion
            
            #region Draw Menu
            DrawMenu = Menu.AddSubMenu("Gösterge");
            DrawMenu.AddGroupLabel("Büyü Menzili");
            DrawMenu.Add("drawQ", new CheckBox("Göster Q", true));
            DrawMenu.Add("drawE", new CheckBox("Göster E", true));
            DrawMenu.Add("drawR", new CheckBox("Göster R", false));
            DrawMenu.AddSeparator();
            //DrawMenu.Add("drawFlashE", new CheckBox("Draw Flash E", false));
            //DrawMenu.Add("drawFlashR", new CheckBox("Draw Flash R", false));
            //DrawMenu.AddSeparator();
            DrawMenu.Add("drawText", new CheckBox("Yazı Göster", true));
            DrawMenu.AddSeparator();
            DrawMenu.AddLabel("   E rengini Göster");
            DrawMenu.AddLabel("Turuncu : Kitli Hedef");
            DrawMenu.AddLabel("Kırmızı : Yakalayabilir ");
            #endregion

            if (Menu["debug"].Cast<CheckBox>().CurrentValue)
            {
                #region Debug Menu
                DebugMenu = Menu.AddSubMenu("Sıfırlama", "Debug");
                DebugMenu.AddGroupLabel("Gösterge");
                DebugMenu.Add("ePosPred", new CheckBox("Prediction E pozisyonu", false));
                DebugMenu.AddSeparator();
                DebugMenu.AddGroupLabel("HUD");
                DebugMenu.Add("hud", new CheckBox("Göster hud", false));
                DebugMenu.AddSeparator();
                DebugMenu.Add("hudGeneral", new CheckBox("Genel Özellikler", true));
                DebugMenu.Add("hudHealth", new CheckBox("Can Özellikler", false));
                DebugMenu.Add("hudPrediction", new CheckBox("Prediction Özellikler", false));
                DebugMenu.Add("hudDamage", new CheckBox("Hasar Özellikler", false));
                DebugMenu.Add("hudUltimateOutPut", new CheckBox("Ulti Özellikler", false));
                DebugMenu.Add("hudTarget", new CheckBox("Hedef Özellikler", false));
                #endregion
            }
        }

        public static void Initialize()
        {

        }
    }
}
