using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TimerBuddy
{
    internal class Config
    {
        public static Menu Menu, SpellMenu, SummonerMenu, TrapMenu, ItemMenu, WardMenu, SC2Menu, MiscMenu, DebugMenu;
        public static List<string> MenuChecker = new List<string>();
        
        static Config()
        {
            try
            {
                var hero = EntityManager.Heroes.AllHeroes;
                var heroName = hero.Select(h => h.BaseSkinName).ToArray();
                var summonerList = Program.SpellDB.Where(i => i.SpellType == SpellType.SummonerSpell).ToList();
                var itemList = Program.SpellDB.Where(i => i.SpellType == SpellType.Item).ToList();
                var wardList = Program.SpellDB.Where(i => i.SpellType == SpellType.Ward).ToList();
                var trapList = Program.SpellDB.Where(t => heroName.Contains(t.ChampionName) && t.SpellType == SpellType.Trap).ToList();
                var spellList = Program.SpellDB.Where(s => heroName.Contains(s.ChampionName) && s.SpellType == SpellType.Spell).ToList();

                #region Main Menu
                Menu = MainMenu.AddMenu("TimerBuddy", "TimerBuddy", "TimerBuddy - Life is all about Timing!");
                Menu.AddGroupLabel("TimerBuddy'e hoşgeldin xD");
                Menu.AddLabel(string.Format("{0} veriler yuklendi", summonerList.Count + itemList.Count + trapList.Count + spellList.Count + wardList.Count));
                Menu.AddGroupLabel("General Settings");
                Menu.AddImportanceItem("minImportance", "Minimum Importance Level to draw: ");

                //Menu.AddGroupLabel("Spell Timer");
                Menu.AddCheckBox("sTimer", "Büyü Zamanlayıcı", true);
                Menu.AddCheckBox("ssTimer", "Şampiyon Büyüleri Zamanlayıcı", true);
                Menu.AddCheckBox("itemTimer", "İtem Zamanlayıcı", true);
                Menu.AddBlank("mainBlank");
                Menu.AddCheckBox("trapTimer", "Tuzak Zamanlayıcı", true);
                Menu.AddCheckBox("wardTimer", "Totem Zamanlayıcı", true);
                Menu.AddSeparator();

                Menu.AddGroupLabel("Ek Özellikler");
                Menu.AddCheckBox("blinkTracker", "Blink Tracker", true);
                Menu.AddCheckBox("cloneTracker", "Klon Gösterici (WIP)", false);
                Menu.AddSeparator();

                Menu.AddGroupLabel("Geliştiriciler");
                Menu.AddLabel("Tychus   - Addon Developer");
                Menu.AddLabel("Hellsing - Dev-a-lot");
                Menu.AddLabel("and Developing forum buddies");
                Menu.AddLabel("Translate Turkish- Türkçeye Çeviri TRAdana");
                #endregion

                #region SC2Menu
                SC2Menu = Menu.AddSubMenu("Zaman Gösterge Listesi");
                SC2Menu.AddGroupLabel("Ejder, Baron Doğma Zamanı");
                SC2Menu.AddCheckBox("jungleEnable", "Aktif", true);
                SC2Menu.AddBlank("blank");
                SC2Menu.AddCheckBox("jungle", "Orman için 10 saniye önce haber ver", true);
                SC2Menu.AddCheckBox("jungle1min", "1 dakika önce alarm ver", true);
                SC2Menu.AddSeparator();
                SC2Menu.AddGroupLabel("Büyülerin Bekleme Süresini haber ver");
                SC2Menu.AddCheckBox("ult", "Yakındaki Heroların Ulti Süresini haber ver", true);
                SC2Menu.AddCheckBox("globalUlt", "Genel Ulti", true);
                SC2Menu.AddCheckBox("ss", "Şampiyon Büyüleri haber ver (Sadece Şampiyon)", true);
                SC2Menu.AddSeparator();
                SC2Menu.AddGroupLabel("Genel Alarmlar");
                foreach (var database in SC2TimerDatabase.Database.Where(d => heroName.Contains(d.ChampionName) && d.SC2Type == SC2Type.Spell))
                    SC2Menu.AddCheckBox("sc2global" + database.ChampionName, database.ChampionName + " " + database.Slot.ToString(), database.Global);
                SC2Menu.AddSeparator();
                SC2Menu.AddGroupLabel("Ek Özellikler");
                //SC2Menu.AddSlider("duration", "Notifications duration time", 10, 2, 20);
                SC2Menu.AddSlider("maxSlot", "En fazla Bildirim Sayısı", 5, 2, 8);
                #endregion

                #region SpellMenu
                if (spellList.Count > 0)
                {
                    SpellMenu = Menu.AddSubMenu("Spell List");
                    foreach (var s in spellList)
                    {
                        if (MenuChecker.Contains(s.MenuCode))
                            continue;

                        MenuChecker.Add(s.MenuCode);

                        SpellMenu.AddGroupLabel(s.MenuCode);
                        SpellMenu.AddCheckBox(s.MenuCode + "draw", "Göster", true);
                        SpellMenu.AddCheckBox(s.MenuCode + "onlyme", "Şampiyon Adını Göster " + s.ChampionName, s.OnlyMe);
                        SpellMenu.AddImportanceItem(s.MenuCode + "importance", "Önem seviyesini göster: ", s.Importance.ToInt());
                        SpellMenu.AddDrawTypeItem(s.MenuCode + "drawtype", "Gösterge Stili: ", s.DrawType.ToInt());
                        SpellMenu.AddColorItem(s.MenuCode + "color");
                        SpellMenu.AddSeparator();
                    }
                }
                #endregion

                #region SummonerMenu
                if (summonerList.Count > 0)
                {
                    SummonerMenu = Menu.AddSubMenu("SummonerSpell List");
                    foreach (var t in summonerList)
                    {
                        if (MenuChecker.Contains(t.MenuCode))
                            continue;

                        MenuChecker.Add(t.MenuCode);

                        SummonerMenu.AddGroupLabel(t.MenuCode);
                        SummonerMenu.Add(t.MenuCode + "draw", new CheckBox("Göster"));
                        SummonerMenu.AddImportanceItem(t.MenuCode + "importance", "Önem Seviyesi: ", t.Importance.ToInt());
                        SummonerMenu.AddDrawTypeItem(t.MenuCode + "drawtype", "Gösterge Stili: ", t.DrawType.ToInt());
                        SummonerMenu.AddColorItem(t.MenuCode + "color");
                        SummonerMenu.AddSeparator();
                    }
                }
                #endregion

                #region ItemMenu
                if (itemList.Count > 0)
                {
                    ItemMenu = Menu.AddSubMenu("Item List");
                    foreach (var i in itemList)
                    {
                        ItemMenu.AddGroupLabel(i.MenuCode);
                        ItemMenu.AddCheckBox(i.MenuCode + "draw", "Göster", true);
                        ItemMenu.AddBlank(i.MenuCode + "blank");
                        ItemMenu.AddCheckBox(i.MenuCode + "ally", "Dost İtemleri Göster", true);
                        ItemMenu.AddCheckBox(i.MenuCode + "enemy", "Düşman İtemleri Göster", true);
                        ItemMenu.AddImportanceItem(i.MenuCode + "importance", "Önem Seviyesi: ", i.Importance.ToInt());
                        ItemMenu.AddDrawTypeItem(i.MenuCode + "drawtype", "Gösterge Stili: ", i.DrawType.ToInt());
                        ItemMenu.AddColorItem(i.MenuCode + "color");
                        ItemMenu.AddSeparator();
                    }
                }
                #endregion

                #region TrapMenu
                if (trapList.Count > 0)
                {
                    TrapMenu = Menu.AddSubMenu("Trap List");

                    foreach (var t in trapList)
                    {
                        TrapMenu.AddGroupLabel(t.MenuCode);
                        TrapMenu.AddCheckBox(t.MenuCode + "draw", "Göster", true);
                        TrapMenu.AddCheckBox(t.MenuCode + "ally", "Dost Tuzakları Göster", true);
                        TrapMenu.AddCheckBox(t.MenuCode + "drawCircle", "Daire Göster", true);
                        TrapMenu.AddCheckBox(t.MenuCode + "enemy", "Tuzakları Göster", true);
                        TrapMenu.AddColorItem(t.MenuCode + "color", 0);
                        TrapMenu.AddSeparator();
                    }
                    TrapMenu.AddGroupLabel("Ek");
                    TrapMenu.AddCheckBox("circleOnlyEnemy", "Daireyi Sadece Düşman İçin Çiz", true);
                }
                #endregion

                #region WardMenu
                if (wardList.Count > 0)
                {
                    WardMenu = Menu.AddSubMenu("Ward List");
                    foreach (var w in wardList)
                    {
                        WardMenu.AddGroupLabel(w.MenuCode);
                        WardMenu.AddCheckBox(w.MenuCode + "draw", "Göster", true);
                        WardMenu.AddCheckBox(w.MenuCode + "ally", "Dost Totemler", true);
                        WardMenu.AddCheckBox(w.MenuCode + "drawCircle", "Daire Göster", true);
                        WardMenu.AddCheckBox(w.MenuCode + "enemy", "Düşman Totemler", true);
                        WardMenu.AddColorItem(w.MenuCode + "color", w.Color.ToInt());
                        WardMenu.AddSeparator();
                    }
                }
                #endregion

                #region MiscMenu
                MiscMenu = Menu.AddSubMenu("Ek Ayarlar");
                MiscMenu.AddGroupLabel("Gösterilen");
                MiscMenu.AddCheckBox("error", "Hata Kodu Göster", true);
                MiscMenu.AddLabel("Eğer bir hata alırsan hata kodunu bize gönder");
                MiscMenu.AddSeparator();
                MiscMenu.AddGroupLabel("Blink Tracker");
                MiscMenu.AddCheckBox("blinkAlly", "Draw Ally", false);
                MiscMenu.AddCheckBox("blinkEnemy", "Draw Enemy", true);
                #endregion
                /*
                DebugMenu = Menu.AddSubMenu("Debug");
                DebugMenu.Add("s1", new Slider("Slider 1", 0, 0, 200));
                DebugMenu.Add("s2", new Slider("Slider 2", 0, 0, 200));
                DebugMenu.Add("s3", new Slider("Slider 3", 0, 0, 200));
                DebugMenu.Add("s4", new Slider("Slider 4", 0, 0, 200));
                DebugMenu.Add("s5", new Slider("Slider 5", 0, 0, 200));
                DebugMenu.Add("c1", new CheckBox("CheckBox 1"));
                DebugMenu.Add("c2", new CheckBox("CheckBox 2"));
                DebugMenu.Add("c3", new CheckBox("CheckBox 3"));*/

                hero.Clear();
                summonerList.Clear();
                spellList.Clear();
                itemList.Clear();
                wardList.Clear();
                trapList.Clear();
                MenuChecker.Clear();
            }
            catch (Exception e)
            {
                e.ErrorMessage("MENU");
            }
        }
        
        public static void Initialize()
        {
            try
            {

            }
            catch (Exception e)
            {
                e.ErrorMessage("CONFIG_INIT");
            }
        }
    }
}
