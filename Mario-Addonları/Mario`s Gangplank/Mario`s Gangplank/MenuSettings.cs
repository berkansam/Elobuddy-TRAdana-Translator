using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Notifications;

namespace Mario_sGangplank
{
    internal class MenuSettings
    {
        public static readonly string MenuName = "Mario`s Gangplank";

        #region Variables
        public static Menu ComboMenu, HarassMenu, LaneClearMenu, JungleClearMenu, LastHitMenu, DrawingsMenu, SettingsMenu;
        #endregion Variables

        public static void LoadMenu()
        {
            var startMenu = MainMenu.AddMenu(MenuName, MenuName.ToLower());

            var notStart = new SimpleNotification("Mario`s Gangplank Yuklendi", "Mario`s Gangplank Basariyle yuklendi.");
            Notifications.Show(notStart, 2500);

            #region Combo
            ComboMenu = startMenu.AddSubMenu(":-Combo Menu-:");
            ComboMenu.AddGroupLabel("-:Combo Büyüler:-");
            ComboMenu.Add("qCombo", new CheckBox("• Kullan Q."));
            ComboMenu.Add("eCombo", new CheckBox("• Kullan E."));
            ComboMenu.AddLabel("If the target is close to you");
            ComboMenu.Add("eComboRangeClose", new Slider("How close to put the barrel(0 on the player)", 150, 50, 600));
            ComboMenu.AddLabel("If the target is far from you");
            ComboMenu.Add("eComboRangeFar", new Slider("How close to put the barrel(0 on the player)", 350, 300, 800));
            ComboMenu.Add("rCombo", new CheckBox("• Kullan R."));
            ComboMenu.Add("rComboCount", new Slider("R için en az düşamn(0 = Kapalı)", 2, 0, 5));
            #endregion Combo

            #region Harass
            HarassMenu = startMenu.AddSubMenu(":-Harass Menu-:");
            HarassMenu.AddGroupLabel("-:Dürtme Ayarları:-");
            HarassMenu.Add("qHarass", new CheckBox("• Kullan Q."));
            HarassMenu.Add("eHarass", new CheckBox("• Kullan E.", false));
            HarassMenu.AddGroupLabel("-:Dürtme Ayarları:-");
            HarassMenu.Add("manaHarass", new Slider("Dürtme için en az mana.", 30));
            HarassMenu.AddGroupLabel("-:Otomatikdürt Büyüleri:-");
            HarassMenu.Add("qAutoHarass", new CheckBox("• Kullan Q."));
            HarassMenu.Add("eAutoHarass", new CheckBox("• Kullan E."));
            HarassMenu.AddGroupLabel("-:Otomatikdürt Ayarları:-");
            var keyAutoHarass = HarassMenu.Add("keyAutoHarass",
                new KeyBind("Otomatik dürtme kapat/aç", false, KeyBind.BindTypes.PressToggle, 'T'));
            keyAutoHarass.OnValueChange += delegate
            {
                var notHarassOn = new SimpleNotification("AutoHarass Mode Change", "AutoHarass is now On. ");
                var notHarassOff = new SimpleNotification("AutoHarass Mode Change", "AutoHarass is now Off. ");

                Notifications.Show(keyAutoHarass.CurrentValue ? notHarassOn : notHarassOff, 1000);
            };
            HarassMenu.Add("manaAutoHarass", new Slider("Otomatik dürtme için gereken mana", 45));
            #endregion Harass

            #region LaneClear
            LaneClearMenu = startMenu.AddSubMenu(":-LaneClear Menu-:");
            LaneClearMenu.AddGroupLabel("-:Lanetemizleme Büyüleri:-");
            LaneClearMenu.Add("qLane", new CheckBox("• Kullan Q varile."));
            LaneClearMenu.Add("qLaneLast", new CheckBox("• Kullan Q son vuruşta"));
            LaneClearMenu.Add("eLane", new CheckBox("• Kullan E."));
            LaneClearMenu.Add("eKeep", new Slider("• şu kadar varil", 1, 0, 4));
            LaneClearMenu.AddGroupLabel("-:Lanetemizleme Ayarları:-");
            LaneClearMenu.Add("qLaneCount", new Slider("Varil Q için gereken minyon", 2,0,6));
            LaneClearMenu.Add("eLaneCount", new Slider("Varil yerleştirmek için gereken minyon.", 3,0,6));
            LaneClearMenu.Add("manaLane", new Slider("Lanetemizleme için gereken en az mana", 30));
            #endregion LaneClear

            #region JungleClear
            JungleClearMenu = startMenu.AddSubMenu(":-JungleClear Menu-:");
            JungleClearMenu.AddGroupLabel("-:Ormantemizleme Büyüleri:-");
            JungleClearMenu.Add("qJungle", new CheckBox("• Kullan Q Barrel."));
            JungleClearMenu.Add("qJungleLast", new CheckBox("• Use Q to kill the minion."));
            JungleClearMenu.Add("eJungle", new CheckBox("• Kullan E."));
            JungleClearMenu.AddGroupLabel("-:Ormantemizleme Ayarları:-");
            JungleClearMenu.Add("manaJungle", new Slider("Orman temizleme için gereken en az mana", 30));
            #endregion JungleClear

            #region Lasthit
            LastHitMenu = startMenu.AddSubMenu(":-LastHit Menu-:");
            LastHitMenu.AddGroupLabel("-:Sonvuruş Büyüleri:-");
            LastHitMenu.Add("qLast", new CheckBox("• Kullan Q."));
            LastHitMenu.AddGroupLabel("-:Sonvuruş Ayarları:-");
            LastHitMenu.Add("manaLast", new Slider("Sonvuruş için gereken en az mana", 30));
            #endregion Lasthit

            #region Settings
            SettingsMenu = startMenu.AddSubMenu(":-Settings Menu-:");
            SettingsMenu.AddGroupLabel("-:Q KS AAyarları:-");
            SettingsMenu.Add("qKS", new CheckBox("• Q Kullan"));
            SettingsMenu.AddGroupLabel("-:R Ayarları:-");
            SettingsMenu.Add("rKS", new CheckBox("• R Kullan"));
            SettingsMenu.Add("rKSOverkill", new Slider("R KS hızlıkil, Ulti için gereken hedefin canı [{0}]", 150, 50, 400));
            SettingsMenu.Add("rToSaveAlly", new CheckBox("• Ryi dostlarını korumak için kullan"));
            SettingsMenu.Add("rToSaveAllyPercent", new Slider("• Dostları korumak için dostların klan canı ({0}).", 15));
            SettingsMenu.AddGroupLabel("-:W Ayarları:-"); 
            SettingsMenu.Add("wUsePercent", new Slider("• Canım şundan azsa  W Kullan({0}).", 20));
            SettingsMenu.AddSeparator(1);
            SettingsMenu.Add("wBuffStun", new CheckBox("• Sabitleme"));
            SettingsMenu.Add("wBuffSlow", new CheckBox("• Yavaşlatma", false));
            SettingsMenu.Add("wBuffBlind", new CheckBox("• Blind"));
            SettingsMenu.Add("wBuffSupression", new CheckBox("• Supression"));
            SettingsMenu.Add("wBuffSnare", new CheckBox("• Snare"));
            SettingsMenu.Add("wBuffTaunt", new CheckBox("• Taunt"));
            SettingsMenu.AddGroupLabel("-:Ayarlar:-");
            LastHitMenu.Add("manaSettings", new Slider("Büyüler için gereken mana", 30));
            #endregion Settings

            #region Drawings
            DrawingsMenu = startMenu.AddSubMenu(":-Drawings Menu-:");
            DrawingsMenu.Add("readyDraw", new CheckBox("• Büyüler hazırsa göster"));
            DrawingsMenu.Add("damageDraw", new CheckBox("•Hasartespitçisi göster"));
            DrawingsMenu.Add("perDraw", new CheckBox("• hasar tespitçisi yüzdeyle göster"));
            DrawingsMenu.Add("statDraw", new CheckBox("• hasartespitçisi istatistiklerini göster"));
            DrawingsMenu.AddGroupLabel("-:Büyüler:-");
            DrawingsMenu.Add("qDraw", new CheckBox("• göster Q."));
            DrawingsMenu.Add("wDraw", new CheckBox("• göster W."));
            DrawingsMenu.Add("eDraw", new CheckBox("• göster E."));
            DrawingsMenu.Add("barrelDraw", new CheckBox("• göster Varil."));
            #endregion Drawings
        }
    }
}
