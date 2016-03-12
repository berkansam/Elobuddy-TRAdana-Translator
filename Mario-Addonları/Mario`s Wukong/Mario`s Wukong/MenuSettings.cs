using System.Collections.Generic;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Notifications;

namespace Mario_sTemplate
{
    internal class MenuSettings
    {
        public static readonly string MenuName = "Mario`s Wukong";
        #region Variables
        public static Menu ComboMenu, HarassMenu, LaneClearMenu, JungleClearMenu, LastHitMenu, DrawingsMenu, SettingsMenu;
        #endregion Variables

        public static void LoadMenu()
        {
            var startMenu = MainMenu.AddMenu(MenuName, MenuName.ToLower());

            var notStart = new SimpleNotification("Mario`s Wukong Yuklendi", "Mario`s Wukong Basariyla Yuklendi.");
            Notifications.Show(notStart, 2500);

            #region Combo
            ComboMenu = startMenu.AddSubMenu(":-Combo Menu-:");
            ComboMenu.AddGroupLabel("-:Combo Ayarları:-");
            var list = new List<string> {"Agressive", "Safe"};
            var comboBox = ComboMenu.Add("comboBoxComboMode", new ComboBox("Kombo Tipi:", list));
            var key = ComboMenu.Add("keyBindModeCombo",
                new KeyBind("Değiştirme modu tuşu", true, KeyBind.BindTypes.PressToggle, 'Z'));
            key.OnValueChange += delegate
            {
                comboBox.SelectedIndex = comboBox.SelectedIndex == 1 ? 0 : 1;
               
                var notModeChange = new SimpleNotification("Combo Mode Change", "Combo Mode changed to " + comboBox.SelectedText);
                Notifications.Show(notModeChange, 1000);
            };
            //
            ComboMenu.AddSeparator(5);
            ComboMenu.AddGroupLabel("-:Combo Büyüleri:-");
            ComboMenu.Add("qCombo", new CheckBox("• Kullan Q."));
            ComboMenu.Add("eCombo", new CheckBox("• Kullan E."));
            ComboMenu.Add("rCombo", new CheckBox("• Kullan R."));
            ComboMenu.Add("rComboCount", new Slider("R yi şu kadar düşman varsa kullan", 2, 0, 5));
            #endregion Combo

            #region Harass
            HarassMenu = startMenu.AddSubMenu(":-Harass Menu-:");
            HarassMenu.AddGroupLabel("-:Dürtme Büyüleri:-");
            HarassMenu.Add("qHarass", new CheckBox("• Kullan Q."));
            HarassMenu.Add("eHarass", new CheckBox("• Kullan E."));
            HarassMenu.AddGroupLabel("-:Dürtme Ayarları:-");
            HarassMenu.Add("manaHarass", new Slider("Büyüler için gereken mana", 30));
            HarassMenu.AddGroupLabel("-:OtomatikDürtme Büyüleri:-");
            HarassMenu.Add("qAutoHarass", new CheckBox("• Kullan Q."));
            HarassMenu.Add("eAutoHarass", new CheckBox("• Kullan E."));
            HarassMenu.AddGroupLabel("-:OtomatikDürtme Ayarları:-");
            var keyAutoHarass = HarassMenu.Add("keyAutoHarass",
                new KeyBind("Otomatik dürtme için tuş", false, KeyBind.BindTypes.PressToggle, 'T'));
            keyAutoHarass.OnValueChange += delegate
            {
                var notHarassOn = new SimpleNotification("AutoHarass Mode Change", "AutoHarass is now On. ");
                var notHarassOff = new SimpleNotification("AutoHarass Mode Change", "AutoHarass is now Off. ");

                Notifications.Show(keyAutoHarass.CurrentValue ? notHarassOn : notHarassOff, 1000);
            };
            HarassMenu.Add("manaAutoHarass", new Slider("Büyüler için gereken mana", 45));
            #endregion Harass

            #region LaneClear
            LaneClearMenu = startMenu.AddSubMenu(":-LaneClear Menu-:");
            LaneClearMenu.AddGroupLabel("-:Lanetemizleme Büyüleri:-");
            LaneClearMenu.Add("qLane", new CheckBox("• Kullan Q."));
            LaneClearMenu.Add("wLane", new CheckBox("• Kullan W."));
            LaneClearMenu.Add("eLane", new CheckBox("• Kullan E."));
            LaneClearMenu.Add("rLane", new CheckBox("• Kullan R."));
            LaneClearMenu.AddGroupLabel("-:Lanetemizleme Ayarları:-");
            LaneClearMenu.Add("manaLane", new Slider("Büyüler için gereken mana", 30));
            #endregion LaneClear

            #region JungleClear
            JungleClearMenu = startMenu.AddSubMenu(":-JungleClear Menu-:");
            JungleClearMenu.AddGroupLabel("-:Ormantemizleme Büyüleri:-");
            JungleClearMenu.Add("qJungle", new CheckBox("• Kullan Q."));
            JungleClearMenu.Add("wJungle", new CheckBox("• Kullan W."));
            JungleClearMenu.Add("eJungle", new CheckBox("• Kullan E."));
            JungleClearMenu.Add("rJungle", new CheckBox("• Kullan R."));
            JungleClearMenu.AddGroupLabel("-:Ormantemizleme Ayarları:-");
            JungleClearMenu.Add("manaJungle", new Slider("Büyüler için gereken mana", 30));
            #endregion JungleClear

            #region Lasthit
            LastHitMenu = startMenu.AddSubMenu(":-LastHit Menu-:");
            LastHitMenu.AddGroupLabel("-:Sonvuruş Büyüleri:-");
            LastHitMenu.Add("qLast", new CheckBox("• Kullan Q."));
            LastHitMenu.Add("wLast", new CheckBox("• Kullan W."));
            LastHitMenu.Add("eLast", new CheckBox("• Kullan E."));
            LastHitMenu.Add("rLast", new CheckBox("• Kullan R."));
            LastHitMenu.AddGroupLabel("-:Sonvuruş Ayarları:-");
            LastHitMenu.Add("manaLast", new Slider("Büyüler için gereken mana", 30));
            #endregion Lasthit

            #region Settings
            SettingsMenu = startMenu.AddSubMenu(":-Settings Menu-:");
            SettingsMenu.AddGroupLabel("-:Otomatik R:-");
            SettingsMenu.Add("rAutoCount", new Slider("Otomatik R kullanmak için düşman say(0 = Kapalı)", 3, 0, 5));
            SettingsMenu.AddGroupLabel("-:Interrupt/Gapcloser:-");
            SettingsMenu.Add("spellInterrupt", new CheckBox("•interruptables Büyüleri."));
            SettingsMenu.Add("spellGapcloser", new CheckBox("•gapcloser."));
            SettingsMenu.AddGroupLabel("-:Ayarlar:-");
            LastHitMenu.Add("manaSettings", new Slider("Büyüler için gereken mana", 30));
            #endregion Settings

            #region Drawings
            DrawingsMenu = startMenu.AddSubMenu(":-Drawings Menu-:");
            DrawingsMenu.Add("readyDraw", new CheckBox("• Büyüler hazırsa göster"));
            DrawingsMenu.Add("damageDraw", new CheckBox("• Hasartespitçisi göster"));
            DrawingsMenu.Add("perDraw", new CheckBox("• Hasar Tespitçisi yüzde olarak"));
            DrawingsMenu.Add("statDraw", new CheckBox("• Hasar tespitçisi istatistikleri"));
            DrawingsMenu.AddGroupLabel("-:Büyüler:-");
            DrawingsMenu.Add("qDraw", new CheckBox("• Göster Q."));
            DrawingsMenu.Add("eDraw", new CheckBox("• Göster E."));
            DrawingsMenu.Add("rDraw", new CheckBox("• Göster R."));
            #endregion Drawings
        }
    }
}
