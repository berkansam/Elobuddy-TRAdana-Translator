
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkaYasuo;
using EloBuddy.SDK.Menu;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace AkaYasuo
{
    internal class MenuManager
    {
        public static Menu YMenu,
            ComboMenu,
            HarassMenu,
            LaneClearMenu,
            LastHitMenu,
            JungleClearMenu,
            MiscMenu,
            FleeMenu,
            KillStealMenu,
            DrawingMenu,
            DogeMenu,
            ItemMenu;

        public static string[] gapcloser;
        public static string[] interrupt;
        public static string[] notarget;
        public static Dictionary<string, Menu> SubMenu = new Dictionary<string, Menu>() { };

        public static void Load()
        {
            Mainmenu();
            Combomenu();
            Harassmenu();
            Fleemenu();
            LaneClearmenu();
            LastHitmenu();
            JungleClearmenu();
            Miscmenu();
            KillStealmenu();
            Drawingmenu();
            Dogemenu();
            Itemmenu();
        }

        public static void Mainmenu()
        {
            YMenu = MainMenu.AddMenu("Aka´s Yasuo", "akasyasuo");
            YMenu.AddGroupLabel("Aka's Yasuoya Hoşgeldin Umarım Eğlenirsin! :)");
        }

        public static void Combomenu()
        {
            ComboMenu = YMenu.AddSubMenu("Kombo", "Combo");
            ComboMenu.AddGroupLabel("Kombo");
            ComboMenu.Add("Q", new CheckBox("Kullan Q"));
            ComboMenu.Add("EC", new CheckBox("Kullan E"));
            ComboMenu.Add("EQ", new CheckBox("Kullan EQ"));
            ComboMenu.Add("EGap", new CheckBox("Kullan E Gapcloser"));
            ComboMenu.Add("EGaps", new Slider("Kullan E-GapCloser düşman olmadında", 300, 1, 475));
            ComboMenu.Add("EGapTower", new CheckBox("Gapclose Kule?", false));
            ComboMenu.Add("StackQ", new CheckBox("Gapcloser için Q kullan "));
            ComboMenu.Add("R", new CheckBox("Kullan R"));
            ComboMenu.Add("Ignite", new CheckBox("Kullan Tutuştur"));
            ComboMenu.AddGroupLabel("R Kombo Ayarları");
            foreach (var hero in EntityManager.Heroes.Enemies.Where(x => x.IsEnemy))
            {
                ComboMenu.Add(hero.ChampionName, new CheckBox("R Kullan Eğer Hedefe " + hero.ChampionName));
            }
            ComboMenu.AddSeparator();
            ComboMenu.Add("R4", new CheckBox("Anında R Kullan >= 1 Dostlar Menzildeyken"));
            ComboMenu.Add("R2", new Slider("Düşman Hpsi Şundan azken <=", 50, 0, 101));
            ComboMenu.Add("R3", new Slider("Şu Kadar Düşmana Vuracaksa", 2, 0, 5));
            ComboMenu.AddGroupLabel("Otomatik R Ayarları");
            ComboMenu.Add("AutoR", new CheckBox("Otomatik R Kullan"));
            ComboMenu.Add("AutoR2", new Slider("Şu kadar Düşmana Vuracaksa", 3, 0, 5));
            ComboMenu.Add("AutoR2HP", new Slider("ve benim canım >=", 101, 0, 101));
            ComboMenu.Add("AutoR2Enemies", new Slider("ve şu kadar düşman menzildeyse <=", 2, 0, 5));
        }

        public static void Harassmenu()
        {
            HarassMenu = YMenu.AddSubMenu("Dürtme", "Harass");
            HarassMenu.AddGroupLabel("Otomatik Dürtme");
            HarassMenu.Add("AutoQ", new KeyBind("Otomatik Q Tuşu", true, KeyBind.BindTypes.PressToggle, 'T'));
            HarassMenu.Add("AutoQ3", new CheckBox("Otomatik Q3"));
            HarassMenu.Add("QTower", new CheckBox("Otomatik Q KuleAltında"));
            HarassMenu.AddGroupLabel("Dürtme");
            HarassMenu.Add("Q", new CheckBox("Kullan Q"));
            HarassMenu.Add("Q3", new CheckBox("Kullan Q3"));
            HarassMenu.Add("QLastHit", new CheckBox("Q Son Vuruş?"));
        }

        public static void Fleemenu()
        {
            FleeMenu = YMenu.AddSubMenu("Flee", "Flee");
            FleeMenu.AddGroupLabel("Flee(kaçma)");
            FleeMenu.Add("EscQ", new CheckBox("Q Yük Yap"));
            FleeMenu.Add("EscE", new CheckBox("E Kullan"));
            FleeMenu.Add("WJ", new KeyBind("Flee modunda duvarlardan geçme tuşu", false, KeyBind.BindTypes.HoldActive, 'G'));
        }

        public static void LaneClearmenu()
        {
            LaneClearMenu = YMenu.AddSubMenu("LaneTemizleme", "LaneClear");
            LaneClearMenu.AddGroupLabel("LaneTemizleme");
            LaneClearMenu.Add("Q", new CheckBox("Kullan Q"));
            LaneClearMenu.Add("Q3", new CheckBox("Kullan Q3"));
            LaneClearMenu.Add("E", new CheckBox("Kullan E"));
            LaneClearMenu.Add("Items", new CheckBox("İtemleri Kullan "));
        }

        public static void JungleClearmenu()
        {
            JungleClearMenu = YMenu.AddSubMenu("OrmanTemizleyici", "JungleClear");
            JungleClearMenu.AddGroupLabel("OrmanTemizleyici");
            JungleClearMenu.Add("Q", new CheckBox("Kullan Q"));
            JungleClearMenu.Add("E", new CheckBox("Kullan E"));
            JungleClearMenu.Add("Items", new CheckBox("İtemleri Kullan"));
        }

        public static void LastHitmenu()
        {
            LastHitMenu = YMenu.AddSubMenu("SonVuruş", "LastHit");
            LastHitMenu.AddGroupLabel("SonVuruş");
            LastHitMenu.Add("Q", new CheckBox("Kullan Q"));
            LastHitMenu.Add("Q3", new CheckBox("Kullan Q3"));
            LastHitMenu.Add("E", new CheckBox("Kullan E"));
        }

        public static void KillStealmenu()
        {
            KillStealMenu = YMenu.AddSubMenu("Kill Çalma", "KillSteal");
            KillStealMenu.AddGroupLabel("Kill Çalma");
            KillStealMenu.Add("KsQ", new CheckBox("Kullan Q"));
            KillStealMenu.Add("KsE", new CheckBox("Kullan E"));
            KillStealMenu.Add("KsIgnite", new CheckBox("Tutuştur Kullan"));
        }

        public static void Miscmenu()
        {
            MiscMenu = YMenu.AddSubMenu("Ek", "Misc");
            MiscMenu.AddGroupLabel("Ek Ayarlar");
            MiscMenu.Add("StackQ", new CheckBox("Q Yük Kas"));
            MiscMenu.Add("InterruptQ", new CheckBox("Interrupt için Q3 Kullan "));
            MiscMenu.Add("noEturret", new CheckBox("Taretlerden Atla-ma"));
            MiscMenu.AddSeparator();
            MiscMenu.AddLabel("1: Q 2: E");
            MiscMenu.Add("autolvl", new CheckBox("Otomatik Level Aktif"));
            MiscMenu.Add("autolvls", new Slider("Level Modu", 1, 1, 2));
            switch (MiscMenu["autolvls"].Cast<Slider>().CurrentValue)
            {
                case 1:
                    Variables.abilitySequence = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case 2:
                    Variables.abilitySequence = new[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                    break;
            }
            var skin = MiscMenu.Add("sID", new Slider("Skin", 0, 0, 2));
            var sID = new[] { "Classic", "High-Noon Yasuo", "Project Yasuo" };
            skin.DisplayName = sID[skin.CurrentValue];

            skin.OnValueChange +=
                delegate (ValueBase<int> sender, ValueBase<int>.ValueChangeArgs changeArgs)
                {
                    sender.DisplayName = sID[changeArgs.NewValue];
                };
        }

        public static void Drawingmenu()
        {
            DrawingMenu = YMenu.AddSubMenu("Göstergeler", "Drawing");
            DrawingMenu.AddGroupLabel("Göstergeler");
            DrawingMenu.Add("DrawQ", new CheckBox("Göster Q Menzili"));
            DrawingMenu.Add("DrawQ3", new CheckBox("Göster Q3 Menzili"));
            DrawingMenu.Add("DrawE", new CheckBox("Göster E Menzili"));
            DrawingMenu.Add("DrawR", new CheckBox("Göster R Menzili"));
            DrawingMenu.Add("DrawSpots", new CheckBox("Göster Duvardan Atlama Noktaları"));
        }

        public static void Dogemenu()
        {
            if (EntityManager.Heroes.Enemies.Any())
            {
                EvadeManager.EvadeSkillshot.Init();
                EvadeManager.EvadeTarget.Init();
            }
        }

        public static void Itemmenu()
        {
            ItemMenu = YMenu.AddSubMenu("İtemler", "QSS");
            ItemMenu.AddGroupLabel("Agresif İtemler");
            ItemMenu.Add("Items", new CheckBox("İtemleri Kullan"));
            ItemMenu.Add("myhp", new Slider("Benim Canım Şundan Azken Mahvolmuş Kılıç Kullan <=", 70, 0, 101));
            ItemMenu.AddGroupLabel("Qss");
            ItemMenu.Add("use", new KeyBind("Kullan QSS/Mercurial", true, KeyBind.BindTypes.PressToggle, 'K'));
            ItemMenu.Add("delay", new Slider("Aktivasyon Gecikmesi", 1000, 0, 2000));
            ItemMenu.Add("Blind",
                new CheckBox("Kör", false));
            ItemMenu.Add("Charm",
                new CheckBox("Charm"));
            ItemMenu.Add("Fear",
                new CheckBox("Korku"));
            ItemMenu.Add("Polymorph",
                new CheckBox("Polymorph"));
            ItemMenu.Add("Stun",
                new CheckBox("Sabitleme"));
            ItemMenu.Add("Snare",
                new CheckBox("Yavaşlatma"));
            ItemMenu.Add("Silence",
                new CheckBox("Sessiz", false));
            ItemMenu.Add("Taunt",
                new CheckBox("Dalga Geçer"));
            ItemMenu.Add("Suppression",
                new CheckBox("Önleme"));
        }
    }
}

