using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Syndra
{
    public static class MenuManager
    {
        public static Menu AddonMenu;
        public static Dictionary<string, Menu> SubMenu = new Dictionary<string, Menu>();
        public static void Init(EventArgs args)
        {
            var addonName = Champion.AddonName;
            var author = Champion.Author;
            AddonMenu = MainMenu.AddMenu(addonName, addonName + " by " + author + " v1.2- ceviri tradana ");
            AddonMenu.AddLabel(addonName + " Tarafından " + author);

            SubMenu["Prediction"] = AddonMenu.AddSubMenu("İsabet", "Prediction 2.1");
            SubMenu["Prediction"].AddGroupLabel("Q Ayarları");
            SubMenu["Prediction"].Add("QCombo", new Slider("Kombo İsabet Oranı", 65, 0, 100));
            SubMenu["Prediction"].Add("QHarass", new Slider("Dürtme İsabet Oranı", 70, 0, 100));
            SubMenu["Prediction"].AddGroupLabel("W Ayarları");
            SubMenu["Prediction"].Add("WCombo", new Slider("Kombo İsabet Oranı", 65, 0, 100));
            SubMenu["Prediction"].Add("WHarass", new Slider("Dürtme İsabet Oranı", 65, 0, 100));
            SubMenu["Prediction"].AddGroupLabel("QE Ayarları");
            SubMenu["Prediction"].Add("ECombo", new Slider("Kombo İsabet Oranı", 65, 0, 100));
            SubMenu["Prediction"].Add("EHarass", new Slider("Dürtme İsabet Oranı", 70, 0, 100));


            SubMenu["Combo"] = AddonMenu.AddSubMenu("Kombo", "Combo 2");
            SubMenu["Combo"].Add("Q", new CheckBox("Q Kullan", true));
            SubMenu["Combo"].Add("W", new CheckBox("W Kullan", true));
            SubMenu["Combo"].Add("E", new CheckBox("E Kullan", true));
            SubMenu["Combo"].Add("QE", new CheckBox("QE Kullan", true));
            SubMenu["Combo"].Add("WE", new CheckBox("WE Kullan", true));
            SubMenu["Combo"].AddStringList("R", "R Kullan", new[] { "Asla", "Öldürülebilirse", "Gerekliyse", "Herzaman" }, 1);
            SubMenu["Combo"].Add("Zhonyas", new Slider("Zhonya Kullan Eğer canım şundan azsa % <=", 10, 0, 100));
            SubMenu["Combo"].Add("Cooldown", new Slider("R için büyülerin bekleme süresi gerekli", 4, 0, 10));

            SubMenu["Harass"] = AddonMenu.AddSubMenu("Dürtme", "Harass");
            SubMenu["Harass"].Add("Toggle", new KeyBind("Otomatik Dürtme Tuşu", false, KeyBind.BindTypes.PressToggle, 'K'));
            SubMenu["Harass"].Add("Q", new CheckBox("Q Kullan", true));
            SubMenu["Harass"].Add("W", new CheckBox("W Kullan", false));
            SubMenu["Harass"].Add("E", new CheckBox("E Kullan", false));
            SubMenu["Harass"].Add("QE", new CheckBox("QE Kullan", false));
            SubMenu["Harass"].Add("WE", new CheckBox("WE Kullan", false));
            SubMenu["Harass"].Add("Turret", new CheckBox("Düşman Kulesi Altında Dürtme", true));
            SubMenu["Harass"].Add("Mana", new Slider("en az mana:", 20, 0, 100));

            SubMenu["LaneClear"] = AddonMenu.AddSubMenu("LaneTemizleme", "LaneClear");
            SubMenu["LaneClear"].Add("Q", new Slider("Q kullan eğer isabet edecekse rakibe: >=", 3, 0, 10));
            SubMenu["LaneClear"].Add("W", new Slider("W Kullan eğer isabet edecekse rakibe >=", 3, 0, 10));
            SubMenu["LaneClear"].AddGroupLabel("Öldürülemez Minyonlar");
            SubMenu["LaneClear"].Add("Q2", new CheckBox("Q Kullan", true));
            SubMenu["LaneClear"].Add("Mana", new Slider("en az mana:", 50, 0, 100));
            
            SubMenu["JungleClear"] = AddonMenu.AddSubMenu("JungleTemizleme", "JungleClear");
            SubMenu["JungleClear"].Add("Q", new CheckBox("Q Kullan", true));
            SubMenu["JungleClear"].Add("W", new CheckBox("W Kullan", true));
            SubMenu["JungleClear"].Add("E", new CheckBox("E Kullan", true));
            SubMenu["JungleClear"].Add("Mana", new Slider("en az mana:", 20, 0, 100));

            SubMenu["LastHit"] = AddonMenu.AddSubMenu("Son Vuruş", "LastHit");
            SubMenu["LastHit"].AddGroupLabel("Öldürülemez Minyonlar");
            SubMenu["LastHit"].Add("Q", new CheckBox("Q Kullan", true));
            SubMenu["LastHit"].Add("Mana", new Slider("en az mana:", 50, 0, 100));
            
            SubMenu["KillSteal"] = AddonMenu.AddSubMenu("Kill Çalma", "KillSteal");
            SubMenu["KillSteal"].Add("Q", new CheckBox("Q Kullan", true));
            SubMenu["KillSteal"].Add("W", new CheckBox("W Kullan", true));
            SubMenu["KillSteal"].Add("E", new CheckBox("E Kullan", true));
            SubMenu["KillSteal"].Add("R", new CheckBox("R Kullan", false));
            SubMenu["KillSteal"].Add("Ignite", new CheckBox("Tutuştur Kullan", true));

            SubMenu["Flee"] = AddonMenu.AddSubMenu("Flee(Kaçma)", "Flee");
            SubMenu["Flee"].Add("Movement", new CheckBox("Hareketler Devredışı", true));
            SubMenu["Flee"].Add("E", new CheckBox("Kullan QE/WE Düşman Farenin yakınındaysa", true));

            SubMenu["Drawings"] = AddonMenu.AddSubMenu("Gösterge", "Drawings");
            SubMenu["Drawings"].Add("Disable", new CheckBox("Disable all drawings", false));
            SubMenu["Drawings"].AddSeparator();
            SubMenu["Drawings"].Add("Q", new CheckBox("Göster Q Menzili", true));
            SubMenu["Drawings"].Add("W", new CheckBox("Göster W Menzili", false));
            SubMenu["Drawings"].Add("QE", new CheckBox("Göster QE Menzili", true));
            SubMenu["Drawings"].Add("R", new CheckBox("Göster R Menzili", false));
            SubMenu["Drawings"].Add("DamageIndicator", new CheckBox("Göster Hasar Tespiti ", true));
            SubMenu["Drawings"].Add("Target", new CheckBox("Hedefi Daireyle Göster", true));
            SubMenu["Drawings"].Add("Killable", new CheckBox("Öldürülebilecek hedefi yaz", true));
            SubMenu["Drawings"].Add("W.Object", new CheckBox("W nesnelerini Göster", true));
            SubMenu["Drawings"].Add("Harass.Toggle", new CheckBox("Oto Dürtme Durumunu Göster", true));
            SubMenu["Drawings"].AddStringList("E.Lines", "E çizgisini Göster", new[] { "Asla", "Eğer Düşmana isabet edecekse", "Her zaman" }, 1);

            SubMenu["Misc"] = AddonMenu.AddSubMenu("Ek", "Misc");
            SubMenu["Misc"].Add("GapCloser", new CheckBox("Use QE/WE to Interrupt GapClosers", true));
            SubMenu["Misc"].Add("Interrupter", new CheckBox("Use QE/WE to Interrupt Channeling Spells", true));
            SubMenu["Misc"].Add("QE.Range", new Slider("Less QE Range", 0, 0, 650));
            SubMenu["Misc"].Add("Overkill", new Slider("Overkill % for damage prediction", 10, 0, 100));
            if (EntityManager.Heroes.Enemies.Count > 0)
            {
                SubMenu["Misc"].AddGroupLabel("Don't use R on");
                foreach (var enemy in EntityManager.Heroes.Enemies)
                {
                    SubMenu["Misc"].Add("Dont.R." + enemy.ChampionName, new CheckBox(enemy.ChampionName, false));
                }
            }

        }

        public static int GetSliderValue(this Menu m, string s)
        {
            if (m != null)
                return m[s].Cast<Slider>().CurrentValue;
            return -1;
        }
        public static bool GetCheckBoxValue(this Menu m, string s)
        {
            return m != null && m[s].Cast<CheckBox>().CurrentValue;
        }

        public static bool GetKeyBindValue(this Menu m, string s)
        {
            return m != null && m[s].Cast<KeyBind>().CurrentValue;
        }

        public static void AddStringList(this Menu m, string uniqueId, string displayName, string[] values, int defaultValue)
        {
            var mode = m.Add(uniqueId, new Slider(displayName, defaultValue, 0, values.Length - 1));
            mode.DisplayName = displayName + ": " + values[mode.CurrentValue];
            mode.OnValueChange += delegate (ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
            {
                sender.DisplayName = displayName + ": " + values[args.NewValue];
            };
        }
        public static Menu GetSubMenu(string s)
        {
            return (from t in SubMenu where t.Key.Equals(s) select t.Value).FirstOrDefault();
        }

        public static Menu MiscMenu
        {
            get { return GetSubMenu("Misc"); }
        }

        public static Menu PredictionMenu
        {
            get { return GetSubMenu("Prediction"); }
        }

        public static Menu DrawingsMenu
        {
            get { return GetSubMenu("Drawings"); }
        }
    }
}
