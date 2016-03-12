using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Menu;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace Aka_s_Vayne_reworked
{
    class MenuManager
    {
        public static Menu VMenu,
            Qsettings,
            ComboMenu,
            CondemnMenu,
            HarassMenu,
            FleeMenu,
            LaneClearMenu,
            JungleClearMenu,
            MiscMenu,
            ItemMenu,
            DrawingMenu;

        public static void Load()
        {
            Mainmenu();
            Combomenu();
            QSettings();
            Condemnmenu();
            Harassmenu();
            Fleemenu();
            LaneClearmenu();
            JungleClearmenu();
            Miscmenu();
            Itemmenu();
            Drawingmenu();
        }

        public static void Mainmenu()
        {
            VMenu = MainMenu.AddMenu("Aka´s Vayne", "akavayne");
            VMenu.AddGroupLabel("Benim Vayne Addonuma Hoşgeldin :)");
            VMenu.AddGroupLabel("Yapım Aka *-*");
            VMenu.AddGroupLabel("Çeviri-TRAdana");
        }

        public static void Combomenu()
        {
            ComboMenu = VMenu.AddSubMenu("Combo", "Combo");
            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.AddGroupLabel("Q Mode");
            ComboMenu.AddGroupLabel("Akıllı Mod devredışı(smart), eski veya yeni kullanın");
            var qmode = ComboMenu.Add("Qmode", new ComboBox("Q Mode", 1, "Mouse", "Akıllıca", "Kaçmaya Odaklı", "Eski", "Yeni"));
            qmode.OnValueChange += delegate
            {
                if (qmode.CurrentValue == 1)
                {
                    Qsettings["UseSafeQ"].IsVisible = true;
                    Qsettings["UseQE"].IsVisible = true;
                    Qsettings["QE"].IsVisible = true;
                    Qsettings["UseQspam"].IsVisible = true;
                    Qsettings["QNmode"].IsVisible = false;
                    Qsettings["QNenemies"].IsVisible = false;
                    Qsettings["QNWall"].IsVisible = false;
                    Qsettings["QNTurret"].IsVisible = false;
                }
                if (qmode.CurrentValue == 3)
                {
                    Qsettings["UseSafeQ"].IsVisible = false;
                    Qsettings["UseQE"].IsVisible = false;
                    Qsettings["QE"].IsVisible = false;
                    Qsettings["UseQspam"].IsVisible = false;
                    Qsettings["QNmode"].IsVisible = false;
                    Qsettings["QNenemies"].IsVisible = false;
                    Qsettings["QNWall"].IsVisible = false;
                    Qsettings["QNTurret"].IsVisible = false;
                }
                if (qmode.CurrentValue == 2)
                {
                    Qsettings["UseSafeQ"].IsVisible = false;
                    Qsettings["UseQE"].IsVisible = false;
                    Qsettings["QE"].IsVisible = false;
                    Qsettings["UseQspam"].IsVisible = false;
                    Qsettings["QNmode"].IsVisible = false;
                    Qsettings["QNenemies"].IsVisible = false;
                    Qsettings["QNWall"].IsVisible = false;
                    Qsettings["QNTurret"].IsVisible = false;
                }
                if (qmode.CurrentValue == 0)
                {
                    Qsettings["UseSafeQ"].IsVisible = false;
                    Qsettings["UseQE"].IsVisible = false;
                    Qsettings["QE"].IsVisible = false;
                    Qsettings["UseQspam"].IsVisible = false;
                    Qsettings["QNmode"].IsVisible = false;
                    Qsettings["QNenemies"].IsVisible = false;
                    Qsettings["QNWall"].IsVisible = false;
                    Qsettings["QNTurret"].IsVisible = false;
                }
                if (qmode.CurrentValue == 4)
                {
                    Qsettings["UseSafeQ"].IsVisible = false;
                    Qsettings["UseQE"].IsVisible = false;
                    Qsettings["QE"].IsVisible = false;
                    Qsettings["UseQspam"].IsVisible = false;
                    Qsettings["QNmode"].IsVisible = true;
                    Qsettings["QNenemies"].IsVisible = true;
                    Qsettings["QNWall"].IsVisible = true;
                    Qsettings["QNTurret"].IsVisible = true;
                }
            };
            ComboMenu.Add("Qmode2", new ComboBox("Akıllı Mod", 0, "Agresif", "Defansif"));
            ComboMenu.Add("UseQwhen", new ComboBox("Use Q", 0, "Ataktan Sonra", "Ataktan Önce", "Asla"));
            ComboMenu.AddGroupLabel("AA Sıfırlama");
            ComboMenu.AddLabel("AA sıfırlama tikini kaldırırsan yapman gerekir [F5]");
            ComboMenu.Add("AAReset", new CheckBox("Benim AA Sıfırlamamı Kullan"));
            ComboMenu.AddLabel("İster Bu AA Cancel Kullanırsın, veya devredışı bırakırsın.");
            ComboMenu.Add("AACancel", new Slider("AA İptali", 0, 0, 20));
            ComboMenu.AddGroupLabel("W Ayarları");
            ComboMenu.Add("focusw", new CheckBox("Odaklan W", false));
            ComboMenu.AddGroupLabel("E Ayarları");
            ComboMenu.Add("Ekill", new CheckBox("Hedef Ölecekse E Kullan?"));
            ComboMenu.Add("comboUseE", new CheckBox("Kullan E"));
            ComboMenu.AddGroupLabel("R Ayarları");
            ComboMenu.Add("comboUseR", new CheckBox("Kullan R", false));
            ComboMenu.Add("comboRSlider", new Slider("Eğer şu kadar düşman varsa R Kullan", 2, 1, 5));
            ComboMenu.Add("RnoAA", new CheckBox("Görünmezken Bu durumu bozma", false));
            ComboMenu.Add("RnoAAs", new Slider("Gizliliği bozma taaaki >= .. düşman menzile girene kadar", 2, 0, 5));
        }

        public static void QSettings()
        {
            Qsettings = VMenu.AddSubMenu("Q Ayarları", "Q Settings");
            Qsettings.AddGroupLabel("Q Ayarları");
            Qsettings.AddLabel("Bursmodunda Daha Hızlı AA reset kullandırılabilir");
            Qsettings.Add("Mirin", new CheckBox("Burstmode"));
            //smart
            Qsettings.Add("UseSafeQ", new CheckBox("Koruyucu Q?", false)).IsVisible = true;
            Qsettings.Add("UseQE", new CheckBox("Düşmanın içine Q at-ma?", false)).IsVisible = true;
            Qsettings.Add("QE", new CheckBox("Dene QE?", false)).IsVisible = true;
            Qsettings.Add("UseQspam", new CheckBox("Kontrolü Yoksay", false)).IsVisible = true;
            //new
            Qsettings.Add("QNmode", new ComboBox("Yeni Mod", 1, "Yana", "Koruyucu Pozisyona")).IsVisible = false;
            Qsettings.Add("QNenemies", new Slider("x düşmanı Q ile Blokla", 3, 5, 0)).IsVisible = false;
            Qsettings.Add("QNWall", new CheckBox("Duvarda Blokla Q", true)).IsVisible = false;
            Qsettings.Add("QNTurret", new CheckBox("Kule Altında BLokla Q", true)).IsVisible = false;

        }

        public static void Condemnmenu()
        {
            CondemnMenu = VMenu.AddSubMenu("Condemn", "Condemn");
            CondemnMenu.AddGroupLabel("Condemn");
            CondemnMenu.Add("Condemnmode", new ComboBox("Condemn Mode", 3, "En iyi", "Yeni", "Nişancı", "Shine", "Aka"));
            CondemnMenu.Add("UseEauto", new CheckBox("Otomatik E Kullan??"));
            CondemnMenu.Add("UseEc", new CheckBox("Sadece Sabitlicekse Kullan?", false));
            CondemnMenu.Add("condemnPercent", new Slider("Condemn isabet şansı %", 33, 1));
            CondemnMenu.Add("pushDistance", new Slider("Condemn vurma mesafesi", 420, 350, 470));
            CondemnMenu.Add("noeaa", new Slider("E kullanma, Eğer Hedef x Kadar vuruşla ölecekse", 0, 0, 4));
            CondemnMenu.Add("trinket", new CheckBox("Trinket Kullan(Totem,Arayıcı Mercek)"));
            CondemnMenu.AddGroupLabel("Mechanics");
            CondemnMenu.Add("flashe", new KeyBind("Flash Condemn!", false, KeyBind.BindTypes.HoldActive, 'Y'));
            CondemnMenu.Add("insece", new KeyBind("Flash Insec!", false, KeyBind.BindTypes.HoldActive, 'Z'));
            CondemnMenu.Add("insecmodes", new ComboBox("Insec Mode", 0, "Dostlara", "Kuleye", "Fareye(mouse)"));
        }

        public static void Harassmenu()
        {
            HarassMenu = VMenu.AddSubMenu("Dürtme", "Harass");
            HarassMenu.AddGroupLabel("Dürtme");
            HarassMenu.AddLabel("Benim Tercihim Ayarlardan sadece 1i, Ben tercih ediyorum Sadece Q");
            HarassMenu.Add("UseQHarass", new CheckBox("Kullan Q(Eğer 2 W yükü varsa)"));
            HarassMenu.Add("UseEHarass", new CheckBox("Kullan E(Eğer 2 W yükü Varsa)", false));
            HarassMenu.Add("UseCHarass", new CheckBox("Kullan Kombo: AA -> Q+AA -> E(Çalışıyor Muhtemelen)", false));
            HarassMenu.Add("ManaHarass", new Slider("En fazla mana kullanımı ({0}%)", 40));
        }

        public static void Fleemenu()
        {
            FleeMenu = VMenu.AddSubMenu("Flee", "Flee");
            FleeMenu.AddGroupLabel("Flee");
            FleeMenu.Add("FleeUseQ", new CheckBox("Kullan Q"));
            FleeMenu.Add("FleeUseE", new CheckBox("Kullan E"));
        }

        public static void LaneClearmenu()
        {
            LaneClearMenu = VMenu.AddSubMenu("LaneTemizleme", "LaneClear");
            LaneClearMenu.AddGroupLabel("LaneTemizleme");
            LaneClearMenu.Add("LCQ", new CheckBox("Kullan Q"));
            LaneClearMenu.Add("LCQMana", new Slider("En Fazla Mana Kullanımı ({0}%)", 40));

        }

        public static void JungleClearmenu()
        {
            JungleClearMenu = VMenu.AddSubMenu("OrmanTemizleme", "JungleClear");
            JungleClearMenu.AddGroupLabel("OrmanTemizleme");
            JungleClearMenu.Add("JCQ", new CheckBox("Kullan Q"));
            JungleClearMenu.Add("JCE", new CheckBox("Kullan E"));
        }

        public static void Miscmenu()
        {
            MiscMenu = VMenu.AddSubMenu("Ek", "Misc");
            MiscMenu.AddGroupLabel("Ek");
            MiscMenu.AddLabel("Burası Fluxy ye aittir :");
            MiscMenu.Add("GapcloseE", new CheckBox("Gapclose E"));
            MiscMenu.Add("AntiRengar", new CheckBox("Anti Rengar"));
            MiscMenu.Add("AntiPanth", new CheckBox("Anti Pantheon"));
            MiscMenu.Add("fpsdrop", new CheckBox("Anti Fps Drop", false));
            MiscMenu.Add("InterruptE", new CheckBox("Interrupt Büyüleri için E?"));
            MiscMenu.Add("LowLifeE", new CheckBox("Düşük Can E Kullan", false));
            MiscMenu.Add("dangerLevel", new ComboBox("Interrupt E Tehlike seviyesi ", 2, "Düşük", "Orta", "Yüksek"));
            MiscMenu.AddGroupLabel("Utility");
            MiscMenu.Add("skinhack", new CheckBox("Skin Değiştirici Aktif"));
            MiscMenu.Add("skinId", new ComboBox("Skin Numarası", 0, "Default", "Vindicator", "Aristocrat", "Dragonslayer", "Heartseeker", "SKT T1", "Arclight", "Vayne Chroma Green", "Vayne Chroma Red", "Vayne Chroma Grey"));
            MiscMenu.Add("autolvl", new CheckBox("Otomatik Level Yükseltme"));
            MiscMenu.Add("autolvls", new ComboBox("Level Mode", 0, "En Fazla W", "En Fazla Q(benim stilim)"));
            MiscMenu.Add("autobuy", new CheckBox("Başlangıçta Otomatik Eşya Al"));
            MiscMenu.Add("autobuyt", new CheckBox("Başlangıçta trinket(totem)otomatik al", false));
            switch (MiscMenu["autolvls"].Cast<ComboBox>().CurrentValue)
            {
                case 0:
                    Variables.AbilitySequence = new[] { 1, 3, 2, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    break;
                case 1:
                    Variables.AbilitySequence = new[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
            }
        }

        public static void Itemmenu()
        {
            ItemMenu = VMenu.AddSubMenu("Activator", "Activator");
            ItemMenu.AddGroupLabel("Items");
            ItemMenu.AddLabel("Bana Sorabilrisin Eğer senin daha fazlaya ihtiyacın varsa");
            ItemMenu.Add("botrk", new CheckBox("Kullan MahvolmuşKılıç"));
            ItemMenu.Add("you", new CheckBox("Kullan Yoummmus"));
            ItemMenu.Add("yous", new Slider("Eğer Mesafee >", 1000, 0, 1500));
            ItemMenu.Add("autopotion", new CheckBox("Otomatik Can İksiri"));
            ItemMenu.Add("autopotionhp", new Slider("Can İksiri için canım şu veya daha az =>", 60));
            ItemMenu.AddGroupLabel("Sihirdar");
            ItemMenu.AddLabel("Bana Sorabilrisin Eğer senin daha fazlaya ihtiyacın varsa");
            ItemMenu.Add("heal", new CheckBox("Can"));
            ItemMenu.Add("hp", new Slider("Canım Şundan az olunca otomatik Kullan <=", 20, 0, 100));
            ItemMenu.Add("healally", new CheckBox("Can Dostlara"));
            ItemMenu.Add("hpally", new Slider("Eğer dostların Canı şundan azsa <=", 20, 0, 100));
            ItemMenu.AddGroupLabel("Qss");
            ItemMenu.Add("qss", new CheckBox("Kullan Qss"));
            ItemMenu.Add("delay", new Slider("Gecikme", 1000, 0, 2000));
            ItemMenu.Add("Blind",
                new CheckBox("Kör", false));
            ItemMenu.Add("Charm",
                new CheckBox("Çekme"));
            ItemMenu.Add("Fear",
                new CheckBox("Korku"));
            ItemMenu.Add("Polymorph",
                new CheckBox("Polymorph"));
            ItemMenu.Add("Stun",
                new CheckBox("Sabit"));
            ItemMenu.Add("Snare",
                new CheckBox("Yavaşlama"));
            ItemMenu.Add("Silence",
                new CheckBox("Sessiz", false));
            ItemMenu.Add("Taunt",
                new CheckBox("Dalga Geçme"));
            ItemMenu.Add("Suppression",
                new CheckBox("Önleme"));

        }
      
        public static void Drawingmenu()
        {
            DrawingMenu = VMenu.AddSubMenu("Göstergeler", "Drawings");
            DrawingMenu.AddGroupLabel("Göstergeler");
            DrawingMenu.Add("DrawQ", new CheckBox("Göster Q", false));
            DrawingMenu.Add("DrawE", new CheckBox("Göster E", false));
            DrawingMenu.Add("DrawOnlyReady", new CheckBox("Büyüler Eğer Hazırsa Göster"));
        }
    }
}
