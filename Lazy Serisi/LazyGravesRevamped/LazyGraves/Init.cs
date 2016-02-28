using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace LazyGraves
{
    internal class Init
    {
        public static Menu Menu, ComboMenu, HarassMenu, FarmMenu, MiscMenu, DrawMenu;

        public static void LoadMenu()
        {
            Bootstrap.Init(null);

            Menu = MainMenu.AddMenu("Lazy Graves", "LazyGraves");
            Menu.AddGroupLabel("Lazy Graves");
            Menu.AddLabel("by DamnedNooB");
            Menu.AddLabel("ceviri tradana");
            Menu.AddSeparator();

            //-------------------------------------------------------------------------------------------------------------------
            /*
            *       _____                _             __  __                  
            *      / ____|              | |           |  \/  |                 
            *     | |     ___  _ __ ___ | |__   ___   | \  / | ___ _ __  _   _ 
            *     | |    / _ \| '_ ` _ \| '_ \ / _ \  | |\/| |/ _ \ '_ \| | | |
            *     | |___| (_) | | | | | | |_) | (_) | | |  | |  __/ | | | |_| |
            *      \_____\___/|_| |_| |_|_.__/ \___/  |_|  |_|\___|_| |_|\__,_|
            *                                                                  
            *                                                                  
            */

            ComboMenu = Menu.AddSubMenu("Kombo", "Combo");
            ComboMenu.AddGroupLabel("Kombo Ayarları");
            ComboMenu.AddLabel("Q - Yolun Sonu");
            ComboMenu.Add("useQ", new CheckBox("Q mantıklı kullan"));
            ComboMenu.AddSeparator();

            ComboMenu.AddLabel("W - Sis Perdesi");
            ComboMenu.Add("useW", new CheckBox("W mantıklı Kullan"));
            ComboMenu.AddSeparator();

            ComboMenu.AddLabel("E - Hızlı Tetik");
            ComboMenu.Add("useE", new CheckBox("E mantıklı kullan"));
            ComboMenu.Add("useEreload", new CheckBox("Yeniden Doldurmada Kullan"));
            ComboMenu.AddSeparator();

            ComboMenu.AddLabel("Parça Tesiri");
            ComboMenu.Add("useQR", new CheckBox("Hızlı Q-R Kombosu Kullan"));
            ComboMenu.Add("useR", new CheckBox("Kill almak için Kullan"));
            ComboMenu.AddSeparator();

            ComboMenu.AddLabel("Ek");
            ComboMenu.Add("disableAA", new CheckBox("Çarpışmada AA kullanma"));

            //-------------------------------------------------------------------------------------------------------------------
            /*
            *      _    _                           __  __                  
            *     | |  | |                         |  \/  |                 
            *     | |__| | __ _ _ __ __ _ ___ ___  | \  / | ___ _ __  _   _ 
            *     |  __  |/ _` | '__/ _` / __/ __| | |\/| |/ _ \ '_ \| | | |
            *     | |  | | (_| | | | (_| \__ \__ \ | |  | |  __/ | | | |_| |
            *     |_|  |_|\__,_|_|  \__,_|___/___/ |_|  |_|\___|_| |_|\__,_|
            *                                                               
            *                                                               
            */

            HarassMenu = Menu.AddSubMenu("Dürtme", "Harass");
            HarassMenu.AddGroupLabel("Dürtme Ayarları");

            HarassMenu.AddLabel("Q - Yolun Sonu");
            HarassMenu.Add("useQ", new CheckBox("Dürtmede Kullan"));
            HarassMenu.Add("qMana", new Slider("En az mana: ", 20, 1));
            HarassMenu.AddSeparator();

            HarassMenu.AddLabel("W - Sis Perdesi");
            HarassMenu.Add("useW", new CheckBox("AA da Kullanma - Menzili"));
            HarassMenu.Add("wMana", new Slider("En az mana: ", 20, 1));
            HarassMenu.AddSeparator();

            //HarassMenu.AddLabel("E - True Grit");
            //HarassMenu.Add("useEreload", new CheckBox("Use to reload"));
            //HarassMenu.Add("eMana", new Slider("Min Mana to use: ", 20, 1));
            HarassMenu.AddSeparator();


            //-------------------------------------------------------------------------------------------------------------------
            /*
            *      ______                     __  __                  
            *     |  ____|                   |  \/  |                 
            *     | |__ __ _ _ __ _ __ ___   | \  / | ___ _ __  _   _ 
            *     |  __/ _` | '__| '_ ` _ \  | |\/| |/ _ \ '_ \| | | |
            *     | | | (_| | |  | | | | | | | |  | |  __/ | | | |_| |
            *     |_|  \__,_|_|  |_| |_| |_| |_|  |_|\___|_| |_|\__,_|
            *                                                         
            *                                                         
            */

            FarmMenu = Menu.AddSubMenu("Farm", "Farm");
            FarmMenu.AddGroupLabel("Farm Ayarları");

            FarmMenu.AddLabel("Q - Yolun Sonu");
            FarmMenu.Add("useQlane", new CheckBox("LaneTemizlerken Kullan"));
            FarmMenu.Add("qManaLane", new Slider("Laneclear için gereken mana: ", 20, 1));
            FarmMenu.Add("qMinionsLane", new Slider("laneclear için gereken minyon: ", 3, 1, 6));
            FarmMenu.AddSeparator();

            FarmMenu.Add("useQjungle", new CheckBox("Orman Temizlemede Kullan"));
            FarmMenu.AddSeparator();
            
            FarmMenu.AddLabel("E - Hızlı Tetik");
            FarmMenu.Add("useEreload", new CheckBox("LaneTemizlemede Kullan"));
            FarmMenu.Add("eManaLane", new Slider("LaneTemizleme için gereken mana: ", 20));
            FarmMenu.AddSeparator();
            /* 
            FarmMenu.Add("useEjungle", new CheckBox("Use in JungleClear"));
            FarmMenu.AddSeparator();
            */

            //-------------------------------------------------------------------------------------------------------------------
            /*
            *      __  __ _            __  __                  
            *     |  \/  (_)          |  \/  |                 
            *     | \  / |_ ___  ___  | \  / | ___ _ __  _   _ 
            *     | |\/| | / __|/ __| | |\/| |/ _ \ '_ \| | | |
            *     | |  | | \__ \ (__  | |  | |  __/ | | | |_| |
            *     |_|  |_|_|___/\___| |_|  |_|\___|_| |_|\__,_|
            *                                                  
            *                                                  
            */

            MiscMenu = Menu.AddSubMenu("Ek", "Misc");
            MiscMenu.AddGroupLabel("Ek Ayarları");
            MiscMenu.AddLabel("Anti Gapcloser Ayarları");

            MiscMenu.Add("gapcloserW", new CheckBox("W sis perdesini gapcloser için kullan"));
            MiscMenu.Add("gapcloserE", new CheckBox("E hızlı testik tekrar gapclose için kullan"));
            MiscMenu.AddSeparator();


            //-------------------------------------------------------------------------------------------------------------------
            /*
            *      _____                       __  __                  
            *     |  __ \                     |  \/  |                 
            *     | |  | |_ __ __ ___      __ | \  / | ___ _ __  _   _ 
            *     | |  | | '__/ _` \ \ /\ / / | |\/| |/ _ \ '_ \| | | |
            *     | |__| | | | (_| |\ V  V /  | |  | |  __/ | | | |_| |
            *     |_____/|_|  \__,_| \_/\_/   |_|  |_|\___|_| |_|\__,_|
            *                                                          
            *                                                          
            */
            /*
                        DrawMenu = Menu.AddSubMenu("Draw", "Draw");
                        DrawMenu.AddGroupLabel("Draw Settings");
                        DrawMenu.AddLabel("Spell Ranges");
                        DrawMenu.Add("drawQ", new CheckBox("Draw Q Range"));
                        DrawMenu.Add("drawQextended", new CheckBox("Draw Extended Q Range"));
                        DrawMenu.Add("drawW", new CheckBox("Draw W Range"));
                        DrawMenu.Add("drawE", new CheckBox("Draw E Range"));
                        DrawMenu.Add("drawR", new CheckBox("Draw R Range"));
            */
            //-------------------------------------------------------------------------------------------------------------------
            /*
            *      ______               _       
            *     |  ____|             | |      
            *     | |____   _____ _ __ | |_ ___ 
            *     |  __\ \ / / _ \ '_ \| __/ __|
            *     | |___\ V /  __/ | | | |_\__ \
            *     |______\_/ \___|_| |_|\__|___/
            *                                   
            *                                   
            */

            Game.OnUpdate += Events.OnUpdate;
            Orbwalker.OnPostAttack += Events.OnPostAttack;
            Orbwalker.OnPreAttack += Events.OnPreAttack;
            Obj_AI_Base.OnSpellCast += Events.OnSpellCast;
            Gapcloser.OnGapcloser += Events.OnGapCloser;
        }
    }
}