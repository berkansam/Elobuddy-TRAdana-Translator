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
            MyMenu.AddGroupLabel("Tu�lar");
            MyMenu.AddLabel(" - Kombo i�in space tu�una bas");
        }

        private static void MyDrawPage()
        {
            MyDraw = MyMenu.AddSubMenu("Draw  settings", "Draw");
            MyDraw.AddGroupLabel("G�ster Ayarlar�:");
            MyDraw.Add("nodraw", 
                new CheckBox("Hi�bi�ey g�sterme", false));
            MyDraw.Add("onlyReady", 
                new CheckBox("Sadece haz�r olan� g�ster", true));
            MyDraw.AddSeparator();
            MyDraw.Add("draw.Q", 
                new CheckBox("Q ile z�plama mesafesini g�ster", true));
            MyDraw.Add("draw.W", 
                new CheckBox("W b�y�s�n� g�ster", true));
            MyDraw.Add("draw.E", 
                new CheckBox("E b�y�s�n� g�ster", true));
            MyDraw.Add("draw.R", 
                new CheckBox("R b�y�s�n� g�ster", true));
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
                new Slider("W can �zelli�i kullanmak i�in benim can�m", 50, 0, 100));
            MyCombo.Add("combo.maxw", 
                new Slider("Can�m �undan fazla ise can �zelli�ini kullanma W'de", 80, 0, 100));
            MyCombo.Add("combo.E", 
                new CheckBox("E Kullan"));
            MyCombo.Add("combo.R",
                new CheckBox("R Kullan"));
            MyCombo.Add("combo.REnemies", 
                new Slider("R i�in en az d��man >=", 2, 1, 5));
            MyCombo.AddSeparator();
            MyCombo.AddGroupLabel("Pro Tips");
            MyCombo.AddLabel(" -Uncheck the boxes if you wish to dont use a specific spell while you are pressing the Combo Key");
        }

        private static void MyFarmPage()
        {
            MyFarm = MyMenu.AddSubMenu("Lane Clear Settings", "laneclear");
            MyFarm.AddGroupLabel("LaneTemizleme Ayarlar�:");
            MyFarm.Add("lc.Q",
                new CheckBox("Q Kullan", false));
            MyFarm.Add("lc.MinionsQ", 
                new Slider("Minyonlara Q i�in gereken minyon ", 3, 0, 10));
            MyFarm.Add("lc.E", 
                new CheckBox("E Kullan", false));
            MyFarm.Add("lc.MinionsE",
                new Slider("E i�in gereken minyon ", 3, 0, 10));
            MyFarm.AddSeparator();
            MyFarm.AddGroupLabel("OrmanTemizleme Ayarlar�");
            MyFarm.Add("jungle.Q", 
                new CheckBox("Q Kullan"));
            MyFarm.Add("jungle.E",
                new CheckBox("E Kullan"));
            MyFarm.Add("jungle.W",
                new CheckBox("W Kullan"));
            MyFarm.Add("jungle.minw",
                new Slider("Can �ekmek i�in �undan az", 50, 0, 100));
            MyFarm.Add("jungle.maxw",
                new Slider("�u kadar can�m varken can �ekme", 80, 0, 100));
            MyFarm.AddSeparator();
            MyFarm.AddGroupLabel("Pro Tips");
            MyFarm.AddLabel(" -Uncheck the boxes if you wish to dont use a specific spell while you are pressing the Jungle/LaneClear Key");
        }

        private static void MyHarrassPage()
        {
            MyHarrass = MyMenu.AddSubMenu("Harrass/Blades of Torment Settings", "harrass");
            MyHarrass.AddGroupLabel("D�rtme Ayarlar�:");
            MyHarrass.AddSeparator();
            MyHarrass.Add("harrass.E", 
                new CheckBox("E Kullan"));
            MyHarrass.AddGroupLabel("E Ayarlar�:");
            MyHarrass.Add("interrupt.q",
                new CheckBox("Interrupt i�in Q"));
            MyHarrass.Add("gapcloser.e", 
                new CheckBox("Gapclose i�in E"));
            MyHarrass.AddGroupLabel("Pro Tips");
            MyHarrass.AddLabel(" -Remember to play safe and don't be a teemo");
        }

        private static void MyActivatorPage()
        {
            MyActivator = MyMenu.AddSubMenu("Activator Settings", "Items");
            MyActivator.AddGroupLabel("Otomatik QSS:");
            MyActivator.Add("Blind",
                new CheckBox("K�r", false));
            MyActivator.Add("Charm",
                new CheckBox("�ekicilik(ahri)"));
            MyActivator.Add("Fear",
                new CheckBox("Korku"));
            MyActivator.Add("Polymorph",
                new CheckBox("Polymorph"));
            MyActivator.Add("Stun",
                new CheckBox("Sabitleme"));
            MyActivator.Add("Snare",
                new CheckBox("Tuza�a d��me"));
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
            MyActivator.AddGroupLabel("Items Kullan�m�:");
            MyActivator.AddSeparator();
            MyActivator.Add("items.sliderHP",
                new Slider("�temleri kullanmak i�in can�m �undan az {0}(%)", 30, 1, 100));
            MyActivator.Add("items.enemiesinrange",
                new Slider("�tem kullanmak i�in menzildeki d��man say�s�", 3, 1, 5));
            MyActivator.AddSeparator();
            MyActivator.AddLabel("Activator itemleri:");
            MyActivator.Add("randuin",
                new CheckBox("Kullan Randuin"));
            MyActivator.Add("glory",
                new CheckBox("Kullan Righteous Glory"));
            MyActivator.Add("bilgewater", 
                new CheckBox("Kullan Bilgewater Palas�"));
            MyActivator.Add("botrk",
                new CheckBox("Kullan Mahvolmu� K�l��"));
            MyActivator.Add("youmus", 
                new CheckBox("Kullan Youmus"));
            MyActivator.Add("hydra",
                new CheckBox("Kullan Hydra"));
            MyActivator.Add("tiamat",
                new CheckBox("Kullan Tiamat"));
            MySpells = MyMenu.AddSubMenu("Spells Settings");
            MySpells.AddGroupLabel("�arp Ayarlar�");
            MySpells.AddSeparator();
            MySpells.Add("SRU_Red",
                new CheckBox("�arp K�rm�z�"));
            MySpells.Add("SRU_Blue", 
                new CheckBox("�arp MAvi"));
            MySpells.Add("SRU_Dragon", 
                new CheckBox("�arp Ejder"));
            MySpells.Add("SRU_Baron",
                new CheckBox("�arp Baron"));
            MySpells.Add("SRU_Gromp",
                new CheckBox("�arp Kurba�a"));
            MySpells.Add("SRU_Murkwolf", 
                new CheckBox("�arp Kurtlar"));
            MySpells.Add("SRU_Razorbeak",
                new CheckBox("�arp Sivrigagalar"));
            MySpells.Add("SRU_Krug", 
                new CheckBox("�arp Golem"));
            MySpells.Add("Sru_Crab", 
                new CheckBox("�arp Yampiri yenge�"));
            MySpells.AddSeparator();
            MySpells.AddGroupLabel("Can Ayarlar�:");
            MySpells.Add("spells.Heal.Hp", 
                new Slider("Can kullanmak i�in can�m �undan az {0}(%)", 30, 1, 100));
            MySpells.AddGroupLabel("Tutu�tur Ayarlar�:");
            MySpells.Add("spell.Ignite.Use", 
                new CheckBox("Kill �almada tutu�tur kullan"));
            MySpells.Add("spells.Ignite.Focus",
                new Slider("Hedefin can� �undan azsa tutu�tur kullan {0}(%)", 10, 1, 100));
            MySpells.Add("spells.Ignite.Kill",
                new CheckBox("hedef �lecekse tutu�tur kullan"));
        }

        private static void MyOtherFunctionsPage()
        {
            MyOtherFunctions = MyMenu.AddSubMenu("Misc Menu", "othermenu");
            MyOtherFunctions.AddGroupLabel("Level Art�m� Ayar�");
            MyOtherFunctions.Add("lvlup", 
                new CheckBox("Otomatik Level Art�m�:", false));;
            MyOtherFunctions.AddSeparator();
            MyOtherFunctions.AddGroupLabel("Skin Ayarlar�");
            MyOtherFunctions.Add("skin.Id", 
                new Slider("Skin De�i�trici", 3, 1, 4));
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
