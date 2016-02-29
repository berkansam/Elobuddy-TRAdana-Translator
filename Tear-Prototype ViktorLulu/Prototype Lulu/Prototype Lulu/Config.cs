using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Prototype_Lulu
{
    static class Config
    {
        public static void Initialize() { }
        public static Menu LuluAutoShieldMenu, LuluAutoRMenu,LuluLaneclearMenu;
        private static Menu LuluMainMenu, LuluComboMenu, LuluHarrasMenu, LuluProtectorMenu, LuluMiscMenu, LuluDrawingsMenu;

        static Config()
        {
            LuluMainMenu = MainMenu.AddMenu("Prototype Lulu", "Prototype Lulu");

            LuluComboMenu = LuluMainMenu.AddSubMenu("Kombo", "Combo");
            LuluComboMenu.AddLabel("[Kombo Ayarları]");
            LuluComboMenu.Add("UseQ", new CheckBox("Kullan Q"));
            LuluComboMenu.Add("UseW", new CheckBox("Kullan W", false));
            LuluComboMenu.Add("UseE", new CheckBox("Kullan E"));
            LuluComboMenu.Add("UseR", new CheckBox("Kullan R", false));
            LuluComboMenu.Add("UseIgnite", new CheckBox("Kullan Tutuştur"));
            LuluComboMenu.Add("HealthR", new Slider("Ulti Kullan Canın şundan az ise (%):", 10, 1, 100));
            LuluComboMenu.AddSeparator(15);
            LuluComboMenu.Add("KillSteal", new CheckBox("Aktif KillÇalma"));
            LuluComboMenu.Add("KillStealQ", new CheckBox("Q ile KillÇalma "));
            LuluComboMenu.Add("KillStealE", new CheckBox("E ile KillÇalma"));
            LuluComboMenu.Add("KillStealQE", new CheckBox("E-Q Kombosu ile KillÇalma (Gelişmiş Q)"));

            LuluLaneclearMenu = LuluMainMenu.AddSubMenu("Laneclear", "Laneclear");
            LuluLaneclearMenu.AddLabel("[LaneClear Ayarları]");
            LuluLaneclearMenu.Add("LaneClearQ", new CheckBox("Lanetemizlemede Q kullan"));
            LuluLaneclearMenu.Add("LaneclearMinions", new Slider("Q için gerekli minyon sayısı", 3, 1, 15));
            LuluLaneclearMenu.Add("LaneClearMana", new Slider("Lanetemizlemede Q için gereken mana (%)", 40, 1, 100));

            LuluHarrasMenu = LuluMainMenu.AddSubMenu("Dürtme", "Harras");
            LuluHarrasMenu.AddLabel("[Dürtme Ayarları]");
            LuluHarrasMenu.Add("HarrasQ", new CheckBox("Kullan Q"));
            LuluHarrasMenu.Add("HarrasE", new CheckBox("Kullan E"));
            LuluHarrasMenu.Add("HarrasQE", new CheckBox("Kullan E-Q (Gelişmiş Q)"));
            LuluHarrasMenu.Add("HarrasManaSlider", new Slider("Dürtme için gereken mana (%):", 40, 1, 100));

            LuluProtectorMenu = LuluMainMenu.AddSubMenu("Koruyucu", "Protector");
            LuluProtectorMenu.AddLabel("[Koruyucu Ayarlar]");
            LuluProtectorMenu.AddLabel("(Bu özellik Takım Savaşında Dostlarını Önceliklerine Göre Koruyacak)");
            LuluProtectorMenu.AddLabel("(Ayrıca Dostların Menzildeyken Kombo Modunda E Kullanacak)");
            LuluProtectorMenu.Add("SupportMode", new CheckBox("Destek Modu Lulu Aktif"));
            LuluProtectorMenu.AddLabel("[Gapcloser Ayarları]");
            LuluProtectorMenu.Add("GapClose", new CheckBox("Önlemek GapClosers (W)"));
            LuluProtectorMenu.Add("GapCloseAllies", new CheckBox("Önlemek Gapclosers Dostlarda (W)"));
            LuluProtectorMenu.Add("Interrupt", new CheckBox("Otomatik Interrupt Büyüsü (W)"));
            LuluProtectorMenu.Add("Poison", new CheckBox("Otomatik Koruma Zehir Büyülerinden (E)"));

            LuluAutoShieldMenu = LuluMainMenu.AddSubMenu("Otomatik Kalkan", "Auto Shield");
            LuluAutoShieldMenu.AddLabel("[Otomatik Kalkan Ayarları]");
            LuluAutoShieldMenu.Add("AShield", new CheckBox("Aktif Otomatik Kalkan", true));
            LuluAutoShieldMenu.Add("AShieldMana", new Slider("E için gereken mana", 50, 1, 100));
            LuluAutoShieldMenu.AddLabel("[Korumaya Karşı Büyüler]");
            foreach (var enemy in EntityManager.Heroes.Enemies)
            {
                if (SpellProtectDB.AvoidSpells.ContainsKey(enemy.ChampionName))
                {
                    LuluAutoShieldMenu.AddLabel(enemy.ChampionName);
                    foreach (var xd in SpellProtectDB.AvoidSpells[enemy.ChampionName])
                    {
                        LuluAutoShieldMenu.Add(xd, new CheckBox(xd, false));
                    }
                }
            }

            LuluAutoRMenu = LuluMainMenu.AddSubMenu("Otomatik R", "Auto R");
            LuluAutoRMenu.AddLabel("[Otomatik Ulti(R) Ayarları]");
            LuluAutoRMenu.Add("AutoRLuLu", new CheckBox("Otomatik yakala ultiyle lulu'yu"));
            LuluAutoRMenu.Add("AutoRSliderLulu", new Slider("Canım Şundan Azsa R kullan (%):", 20, 1, 100));
            foreach (var protectTarget in EntityManager.Heroes.Allies.Where(x => !x.IsMe))
            {
                LuluAutoRMenu.Add(protectTarget.ChampionName + "CB", new CheckBox("Protect " + protectTarget.ChampionName));
                LuluAutoRMenu.Add(protectTarget.ChampionName + "SL", new Slider(protectTarget.ChampionName + " Minimum HP to cast Ulti(R) (%):", 20, 1, 100));
            }



            LuluMiscMenu = LuluMainMenu.AddSubMenu("Ek", "Misc");
            LuluMiscMenu.AddGroupLabel("[Ek Ayarlar]");
            /*
            LuluMiscMenu.AddLabel("[Skin Selector]");
            LuluMiscMenu.Add("SkinSelector", new CheckBox("Enable Skin Selector"));
            LuluMiscMenu.Add("SkinID", new Slider("Skin ID:", 1, 1, 5));
            */
            LuluMiscMenu.AddLabel("[İsabet Oranı]");
            LuluMiscMenu.Add("İsabet Oranı", new Slider("İsabet Oranı:", 3, 1, 3));

            LuluDrawingsMenu = LuluMainMenu.AddSubMenu("Göstergeler", "Drawings");
            LuluDrawingsMenu.AddGroupLabel("[Gösterge Ayarları");
            LuluDrawingsMenu.AddLabel("[Göster Menzil Ayarları]");
            LuluDrawingsMenu.Add("DisableAll", new CheckBox("Tüm göstergeleri kapat", false));
            LuluDrawingsMenu.Add("DrawQ", new CheckBox("Göster Q"));
            LuluDrawingsMenu.Add("DrawW", new CheckBox("Göster W", false));
            LuluDrawingsMenu.Add("DrawE", new CheckBox("Göster E", false));
            LuluDrawingsMenu.Add("DrawR", new CheckBox("Göster R", false));
        }

        //Skin Selector Config
        /*
        public static Slider _SkinSelector
        {
            get { return LuluMiscMenu["SkinID"].Cast<Slider>(); }
        }
        */

        //E protect
        public static bool _AShield {  get { return LuluAutoShieldMenu["AShield"].Cast<CheckBox>().CurrentValue; } }
        public static int _AShieldMana {  get { return LuluAutoShieldMenu["AShieldMana"].Cast<Slider>().CurrentValue; } }


        //Ulti for allies
        public static bool _AutoR(string champName)
        {
            return LuluAutoRMenu[champName + "CB"].Cast<CheckBox>().CurrentValue;
        }

        public static int _AutoRHp(string champName)
        {
            return LuluAutoRMenu[champName + "SL"].Cast<Slider>().CurrentValue;
        }
        //Ulti for Lulu
        public static bool _AutoRLulu { get { return LuluAutoRMenu["AutoRLuLu"].Cast<CheckBox>().CurrentValue; } }
        public static int _AutoRLuluHp {  get { return LuluAutoRMenu["AutoRSliderLulu"].Cast<Slider>().CurrentValue; } }





        //--------------- Menu Checkboxes -----------------//
        public static bool ReturnBoolMenu(string category, string unqIdentifier)
        {
            //Console.WriteLine("Returned Menu Name: {0} with identifier: {1}",name.DisplayName,unqIdentifier);
            switch (category)
            {
                case "Combo":
                    return LuluComboMenu[unqIdentifier].Cast<CheckBox>().CurrentValue;

                case "Harras":
                    return LuluHarrasMenu[unqIdentifier].Cast<CheckBox>().CurrentValue;

                case "Protector":
                    return LuluProtectorMenu[unqIdentifier].Cast<CheckBox>().CurrentValue;

                case "Drawings":
                    return LuluDrawingsMenu[unqIdentifier].Cast<CheckBox>().CurrentValue;

                case "Misc":
                    return LuluMiscMenu[unqIdentifier].Cast<CheckBox>().CurrentValue;
            }
            return false;
        }

        //--------------- Menu Slider -----------------//
        public static int ReturnIntMenu(string category, string unqIdentifier)
        {
            switch (category)
            {
                case "Combo":
                    return LuluComboMenu[unqIdentifier].Cast<Slider>().CurrentValue;

                case "Harras":
                    return LuluHarrasMenu[unqIdentifier].Cast<Slider>().CurrentValue;

                case "Protector":
                    return LuluProtectorMenu[unqIdentifier].Cast<Slider>().CurrentValue;

                case "Drawings":
                    return LuluDrawingsMenu[unqIdentifier].Cast<Slider>().CurrentValue;

                case "Misc":
                    return LuluMiscMenu[unqIdentifier].Cast<Slider>().CurrentValue;
            }
            return 0;
        }


    }
}
