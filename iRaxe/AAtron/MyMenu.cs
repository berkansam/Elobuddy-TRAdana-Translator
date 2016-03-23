using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace AAtron
{
    static class AatroxMenu
    {
        private static Menu MyMenu;
        public static Menu MyCombo;
        private static Menu MyDraw;
        public static Menu MyHarrass, MyActivator, MySpells, MyFarm, MyOtherFunctions;

        public static void loadMenu()
        {
            MyAatroxPage();
            MyDrawPage();
            MyComboPage();
            MyFarmPage();
            MyHarrassPage();
            MyActivatorPage();
            MyOtherFunctionsPage();
        }

        private static void MyAatroxPage()
        {
            MyMenu = MainMenu.AddMenu("AAtron", "main");
            MyMenu.AddGroupLabel("About this script:");
            MyMenu.AddLabel(" AAtron - " + Program.version);
            MyMenu.AddLabel(" Made by -iRaxe");
            MyMenu.AddSeparator();
            MyMenu.AddGroupLabel("Tuþlar");
            MyMenu.AddLabel(" - Kombo için space tuþuna bas");
        }

        private static void MyDrawPage()
        {
            MyDraw = MyMenu.AddSubMenu("Draw  settings", "Draw");
            MyDraw.AddGroupLabel("Göster Ayarlarý:");
            MyDraw.Add("nodraw", 
                new CheckBox("Hiçbiþey gösterme", false));
            MyDraw.Add("onlyReady", 
                new CheckBox("Sadece hazýr olaný göster", true));
            MyDraw.AddSeparator();
            MyDraw.Add("draw.Q", 
                new CheckBox("Q ile zýplama mesafesini göster", true));
            MyDraw.Add("draw.W", 
                new CheckBox("W büyüsünü göster", true));
            MyDraw.Add("draw.E", 
                new CheckBox("E büyüsünü göster", true));
            MyDraw.Add("draw.R", 
                new CheckBox("R büyüsünü göster", true));
            MyDraw.AddSeparator();
            MyDraw.AddGroupLabel("Pro Tips");
            MyDraw.AddLabel(" - Uncheck the boxes if you wish to dont see a specific draw");
        }

        private static void MyComboPage()
        {
            MyCombo = MyMenu.AddSubMenu("Combo settings", "Combo");
            MyCombo.AddGroupLabel("Combo settings:");
            MyCombo.Add("combo.Q", 
                new CheckBox("Q Kullan"));
            MyCombo.Add("combo.W",
                new CheckBox("W Kullan"));
            MyCombo.Add("combo.minw",
                new Slider("W can özelliði kullanmak için benim caným", 50, 0, 100));
            MyCombo.Add("combo.maxw", 
                new Slider("Caným þundan fazla ise can özelliðini kullanma W'de", 80, 0, 100));
            MyCombo.Add("combo.E", 
                new CheckBox("E Kullan"));
            MyCombo.Add("combo.R",
                new CheckBox("R Kullan"));
            MyCombo.Add("combo.REnemies", 
                new Slider("R için en az düþman >=", 2, 1, 5));
            MyCombo.AddSeparator();
            MyCombo.AddGroupLabel("Pro Tips");
            MyCombo.AddLabel(" -Uncheck the boxes if you wish to dont use a specific spell while you are pressing the Combo Key");
        }

        private static void MyFarmPage()
        {
            MyFarm = MyMenu.AddSubMenu("Lane Clear Settings", "laneclear");
            MyFarm.AddGroupLabel("LaneTemizleme Ayarlarý:");
            MyFarm.Add("lc.Q",
                new CheckBox("Q Kullan", false));
            MyFarm.Add("lc.MinionsQ", 
                new Slider("Minyonlara Q için gereken minyon ", 3, 0, 10));
            MyFarm.Add("lc.E", 
                new CheckBox("E Kullan", false));
            MyFarm.Add("lc.MinionsE",
                new Slider("E için gereken minyon ", 3, 0, 10));
            MyFarm.AddSeparator();
            MyFarm.AddGroupLabel("OrmanTemizleme Ayarlarý");
            MyFarm.Add("jungle.Q", 
                new CheckBox("Q Kullan"));
            MyFarm.Add("jungle.E",
                new CheckBox("E Kullan"));
            MyFarm.Add("jungle.W",
                new CheckBox("W Kullan"));
            MyFarm.Add("jungle.minw",
                new Slider("Can çekmek için þundan az", 50, 0, 100));
            MyFarm.Add("jungle.maxw",
                new Slider("Þu kadar caným varken can çekme", 80, 0, 100));
            MyFarm.AddSeparator();
            MyFarm.AddGroupLabel("Pro Tips");
            MyFarm.AddLabel(" -Uncheck the boxes if you wish to dont use a specific spell while you are pressing the Jungle/LaneClear Key");
        }

        private static void MyHarrassPage()
        {
            MyHarrass = MyMenu.AddSubMenu("Harrass/Blades of Torment Settings", "harrass");
            MyHarrass.AddGroupLabel("Dürtme Ayarlarý:");
            MyHarrass.AddSeparator();
            MyHarrass.Add("harrass.E", 
                new CheckBox("E Kullan"));
            MyHarrass.AddGroupLabel("E Ayarlarý:");
            MyHarrass.Add("interrupt.q",
                new CheckBox("Interrupt için Q"));
            MyHarrass.Add("gapcloser.e", 
                new CheckBox("Gapclose için E"));
            MyHarrass.AddGroupLabel("Pro Tips");
            MyHarrass.AddLabel(" -Remember to play safe and don't be a teemo");
        }

        private static void MyActivatorPage()
        {
            MyActivator = MyMenu.AddSubMenu("Activator Settings", "Items");
            MyActivator.AddGroupLabel("Otomatik QSS:");
            MyActivator.Add("Blind",
                new CheckBox("Kör", false));
            MyActivator.Add("Charm",
                new CheckBox("Çekicilik(ahri)"));
            MyActivator.Add("Fear",
                new CheckBox("Korku"));
            MyActivator.Add("Polymorph",
                new CheckBox("Polymorph"));
            MyActivator.Add("Stun",
                new CheckBox("Sabitleme"));
            MyActivator.Add("Snare",
                new CheckBox("Tuzaða düþme"));
            MyActivator.Add("Silence",
                new CheckBox("Sessiz", false));
            MyActivator.Add("Taunt",
                new CheckBox("Alay Etme"));
            MyActivator.Add("Suppression",
                new CheckBox("Suppression"));
            MyActivator.AddGroupLabel("Ultiler");
            MyActivator.Add("ZedUlt",
                new CheckBox("Zed Ult"));
            MyActivator.Add("VladUlt",
                new CheckBox("Vlad Ult"));
            MyActivator.Add("FizzUlt",
                new CheckBox("Fizz Ult"));
            MyActivator.Add("MordUlt",
                new CheckBox("Mordekaiser Ult"));
            MyActivator.Add("PoppyUlt",
                new CheckBox("Poppy Ult"));
            MyActivator.AddGroupLabel("Items Kullanýmý:");
            MyActivator.AddSeparator();
            MyActivator.Add("items.sliderHP",
                new Slider("Ýtemleri kullanmak için caným þundan az {0}(%)", 30, 1, 100));
            MyActivator.Add("items.enemiesinrange",
                new Slider("Ýtem kullanmak için menzildeki düþman sayýsý", 3, 1, 5));
            MyActivator.AddSeparator();
            MyActivator.AddLabel("Activator itemleri:");
            MyActivator.Add("randuin",
                new CheckBox("Kullan Randuin"));
            MyActivator.Add("glory",
                new CheckBox("Kullan Righteous Glory"));
            MyActivator.Add("bilgewater", 
                new CheckBox("Kullan Bilgewater Palasý"));
            MyActivator.Add("botrk",
                new CheckBox("Kullan Mahvolmuþ Kýlýç"));
            MyActivator.Add("youmus", 
                new CheckBox("Kullan Youmus"));
            MyActivator.Add("hydra",
                new CheckBox("Kullan Hydra"));
            MyActivator.Add("tiamat",
                new CheckBox("Kullan Tiamat"));
            MySpells = MyMenu.AddSubMenu("Spells Settings");
            MySpells.AddGroupLabel("Çarp Ayarlarý");
            MySpells.AddSeparator();
            MySpells.Add("SRU_Red",
                new CheckBox("Çarp Kýrmýzý"));
            MySpells.Add("SRU_Blue", 
                new CheckBox("Çarp MAvi"));
            MySpells.Add("SRU_Dragon", 
                new CheckBox("Çarp Ejder"));
            MySpells.Add("SRU_Baron",
                new CheckBox("Çarp Baron"));
            MySpells.Add("SRU_Gromp",
                new CheckBox("Çarp Kurbaða"));
            MySpells.Add("SRU_Murkwolf", 
                new CheckBox("Çarp Kurtlar"));
            MySpells.Add("SRU_Razorbeak",
                new CheckBox("Çarp Sivrigagalar"));
            MySpells.Add("SRU_Krug", 
                new CheckBox("Çarp Golem"));
            MySpells.Add("Sru_Crab", 
                new CheckBox("Çarp Yampiri yengeç"));
            MySpells.AddSeparator();
            MySpells.AddGroupLabel("Can Ayarlarý:");
            MySpells.Add("spells.Heal.Hp", 
                new Slider("Can kullanmak için caným þundan az {0}(%)", 30, 1, 100));
            MySpells.AddGroupLabel("Tutuþtur Ayarlarý:");
            MySpells.Add("spell.Ignite.Use", 
                new CheckBox("Kill çalmada tutuþtur kullan"));
            MySpells.Add("spells.Ignite.Focus",
                new Slider("Hedefin caný þundan azsa tutuþtur kullan {0}(%)", 10, 1, 100));
            MySpells.Add("spells.Ignite.Kill",
                new CheckBox("hedef ölecekse tutuþtur kullan"));
        }

        private static void MyOtherFunctionsPage()
        {
            MyOtherFunctions = MyMenu.AddSubMenu("Misc Menu", "othermenu");
            MyOtherFunctions.AddGroupLabel("Level Artýmý Ayarý");
            MyOtherFunctions.Add("lvlup", 
                new CheckBox("Otomatik Level Artýmý:", false));;
            MyOtherFunctions.AddSeparator();
            MyOtherFunctions.AddGroupLabel("Skin Ayarlarý");
            MyOtherFunctions.Add("skin.Id", 
                new Slider("Skin Deðiþtrici", 3, 1, 4));
        }

        public static int skinId()
        {
            return MyOtherFunctions["skin.Id"].Cast<Slider>().CurrentValue;
        }
        public static float spellsHealignite()
        {
            return MySpells["spells.Ignite.Focus"].Cast<Slider>().CurrentValue;
        }
        public static float checkenemies()
        {
            return MySpells["items.enemiesinrange"].Cast<Slider>().CurrentValue;
        }
        public static float checkhp()
        {
            return MySpells["items.sliderHP"].Cast<Slider>().CurrentValue;
        }
        public static float spellsHealhp()
        {
            return MySpells["spells.Heal.Hp"].Cast<Slider>().CurrentValue;
        }
        public static bool nodraw()
        {
            return MyDraw["nodraw"].Cast<CheckBox>().CurrentValue;
        }
        public static bool onlyReady()
        {
            return MyDraw["onlyReady"].Cast<CheckBox>().CurrentValue;
        }
        public static bool drawingsQ()
        {
            return MyDraw["draw.Q"].Cast<CheckBox>().CurrentValue;
        }
        public static bool drawingsW()
        {
            return MyDraw["draw.W"].Cast<CheckBox>().CurrentValue;
        }
        public static bool drawingsE()
        {
            return MyDraw["draw.E"].Cast<CheckBox>().CurrentValue;
        }
        public static bool drawingsR()
        {
            return MyDraw["draw.R"].Cast<CheckBox>().CurrentValue;
        }
        public static bool SpellsPotionsCheck()
        {
            return MyActivator["spells.Potions.Check"].Cast<CheckBox>().CurrentValue;
        }
        public static bool tiamat()
        {
            return MyActivator["tiamat"].Cast<CheckBox>().CurrentValue;
        }
        public static bool hydra()
        {
            return MyActivator["hydra"].Cast<CheckBox>().CurrentValue;
        }
        public static float SpellsPotionsHP()
        {
            return MyActivator["spells.Potions.HP"].Cast<Slider>().CurrentValue;
        }
        public static float SpellsPotionsM()
        {
            return MyActivator["spells.Potions.Mana"].Cast<Slider>().CurrentValue;
        }
        public static float spellsBarrierHP()
        {
            return MyActivator["spells.Barrier.Hp"].Cast<Slider>().CurrentValue;
        }

        public static bool Blind()
        {
            return MyActivator["Blind"].Cast<CheckBox>().CurrentValue;
        }

        public static bool Charm()
        {
            return MyActivator["Charm"].Cast<CheckBox>().CurrentValue;
        }

        public static bool Fear()
        {
            return MyActivator["Fear"].Cast<CheckBox>().CurrentValue;
        }

        public static bool Polymorph()
        {
            return MyActivator["Polymorph"].Cast<CheckBox>().CurrentValue;
        }

        public static bool Stun()
        {
            return MyActivator["Stun"].Cast<CheckBox>().CurrentValue;
        }

        public static bool Snare()
        {
            return MyActivator["Snare"].Cast<CheckBox>().CurrentValue;
        }

        public static bool Silence()
        {
            return MyActivator["Silence"].Cast<CheckBox>().CurrentValue;
        }

        public static bool Taunt()
        {
            return MyActivator["Taunt"].Cast<CheckBox>().CurrentValue;
        }
        public static bool Suppression()
        {
            return MyActivator["Suppression"].Cast<CheckBox>().CurrentValue;
        }

        public static bool ZedUlt()
        {
            return MyActivator["ZedUlt"].Cast<CheckBox>().CurrentValue;
        }

        public static bool VladUlt()
        {
            return MyActivator["VladUlt"].Cast<CheckBox>().CurrentValue;
        }

        public static bool FizzUlt()
        {
            return MyActivator["FizzUlt"].Cast<CheckBox>().CurrentValue;
        }

        public static bool MordUlt()
        {
            return MyActivator["MordUlt"].Cast<CheckBox>().CurrentValue;
        }

        public static bool PoppyUlt()
        {
            return MyActivator["PoppyUlt"].Cast<CheckBox>().CurrentValue;
        }
    }
}
