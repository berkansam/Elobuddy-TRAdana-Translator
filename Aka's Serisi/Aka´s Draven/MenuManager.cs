
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Menu;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace AkaDraven
{
    internal class MenuManager
    {
        public static Menu YMenu,
            ComboMenu,
            AxeMenu,
            HarassMenu,
            LaneClearMenu,
            MiscMenu,
            FleeMenu,
            KillStealMenu,
            DrawingMenu,
            ItemMenu;

        public static void Load()
        {
            Mainmenu();
            Axemenu();
            Combomenu();
            Harassmenu();
            Fleemenu();
            LaneClearmenu();
            Miscmenu();
            KillStealmenu();
            Drawingmenu();
            Itemmenu();
        }

        public static void Mainmenu()
        {
            YMenu = MainMenu.AddMenu("Aka´nın Draveni", "akasdraven");
            YMenu.AddGroupLabel("Dravene hoşgeldin iyi eğlenceler! :)");
        }

        public static void Combomenu()
        {
            ComboMenu = YMenu.AddSubMenu("Kombo", "Combo");
            ComboMenu.AddGroupLabel("Kombo");
            ComboMenu.Add("Q", new CheckBox("Kullan Q"));
            ComboMenu.Add("W", new CheckBox("Kullan W"));
            ComboMenu.Add("E", new CheckBox("Kullan E"));
            ComboMenu.Add("R", new CheckBox("Kullan R"));
        }

        public static void Axemenu()
        {
            AxeMenu = YMenu.AddSubMenu("Axe Ayarları", "Axesettings");
            AxeMenu.AddGroupLabel("Axe Ayarları");
            AxeMenu.AddLabel("1: Kombo 2: Hiç 3: Her Zaman");
            AxeMenu.Add("Qmode", new Slider("Balta Tutma Modu:", 3, 1, 3));
            AxeMenu.Add("Qrange", new Slider("Balta Tutma Menzili:", 800, 120, 1500));
            AxeMenu.Add("Qmax", new Slider("En Fazla Balta:", 2, 1, 3));
            AxeMenu.Add("WforQ", new CheckBox("Baltaya Uzak Olursa W kullan"));
            AxeMenu.Add("Qunderturret", new CheckBox("Kule Altında Yakalama"));
        }

        public static void Harassmenu()
        {
            HarassMenu = YMenu.AddSubMenu("Dürtme", "Harass");
            HarassMenu.Add("E", new CheckBox("Kullan E"));
            HarassMenu.Add("AutoE", new KeyBind("Otomatik Dürtme Tuşu", true, KeyBind.BindTypes.PressToggle, 'G'));
        }

        public static void Fleemenu()
        {
            FleeMenu = YMenu.AddSubMenu("Flee", "Flee");
            FleeMenu.Add("E", new CheckBox("Kullan E"));
            FleeMenu.Add("W", new CheckBox("Kullan W"));
        }

        public static void LaneClearmenu()
        {
            LaneClearMenu = YMenu.AddSubMenu("LaneClear", "LaneClear");
            LaneClearMenu.Add("Q", new CheckBox("Kullan Q"));
            LaneClearMenu.Add("W", new CheckBox("Kullan W"));
            LaneClearMenu.Add("Mana", new Slider("En az mana", 50));
        }

        public static void KillStealmenu()
        {
            KillStealMenu = YMenu.AddSubMenu("KillÇalma", "KillSteal");
            KillStealMenu.Add("KsE", new CheckBox("Kullan E"));
            KillStealMenu.Add("KsIgnite", new CheckBox("Kullan Tutuştur"));
        }

        public static void Miscmenu()
        {
            MiscMenu = YMenu.AddSubMenu("Ek", "Misc");
            MiscMenu.Add("UseEInterrupt", new CheckBox("Interrupt için E "));
            MiscMenu.Add("UseWInstant", new CheckBox("Anında W(Eğer Hazırsa)", false));
            MiscMenu.Add("UseWSlow", new CheckBox("Eğer Yavaşlamışsa W Kullan"));
            MiscMenu.Add("WMana", new Slider("W için mana", 50));
            MiscMenu.Add("autolvl", new CheckBox("Aktif Otomatik level"));
        }

        public static void Drawingmenu()
        {
            DrawingMenu = YMenu.AddSubMenu("Göstergeler", "Drawing");
            DrawingMenu.Add("DrawE", new CheckBox("Göster E"));
            DrawingMenu.Add("DrawAxe", new CheckBox("Göster Axe"));
            DrawingMenu.Add("DrawAxeRange", new CheckBox("Balta Tutma Menzili"));
        }

        public static void Itemmenu()
        {
            ItemMenu = YMenu.AddSubMenu("İtemler", "QSS");
            ItemMenu.Add("Items", new CheckBox("İtemleri Kullan"));
            ItemMenu.Add("myhp", new Slider("Mahvolmuş Kılıç Kullan <=", 70, 0, 101));
            ItemMenu.AddSeparator();
            ItemMenu.Add("use", new KeyBind("Kullan QSS", true, KeyBind.BindTypes.PressToggle, 'K'));
            ItemMenu.Add("delay", new Slider("Aktive Gecikmesi", 1000, 0, 2000));
            ItemMenu.Add("Blind",
                new CheckBox("Blind", false));
            ItemMenu.Add("Charm",
                new CheckBox("Charm"));
            ItemMenu.Add("Fear",
                new CheckBox("Fear"));
            ItemMenu.Add("Polymorph",
                new CheckBox("Polymorph"));
            ItemMenu.Add("Stun",
                new CheckBox("Stun"));
            ItemMenu.Add("Snare",
                new CheckBox("Snare"));
            ItemMenu.Add("Silence",
                new CheckBox("Silence", false));
            ItemMenu.Add("Taunt",
                new CheckBox("Taunt"));
            ItemMenu.Add("Suppression",
                new CheckBox("Suppression"));
        }
    }
}
