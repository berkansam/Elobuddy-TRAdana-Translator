namespace KappaUtility.Items
{
    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;

    internal class Defensive
    {
        public static readonly Item Zhonyas = new Item(ItemId.Zhonyas_Hourglass);

        public static readonly Item Seraph = new Item(ItemId.Seraphs_Embrace);

        public static readonly Item FOTM = new Item(ItemId.Face_of_the_Mountain);

        public static readonly Item Solari = new Item(ItemId.Locket_of_the_Iron_Solari);

        public static readonly Item Randuin = new Item(ItemId.Randuins_Omen, 500f);

        public static Menu DefMenu { get; private set; }

        internal static void OnLoad()
        {
            DefMenu = Load.UtliMenu.AddSubMenu("Defence Items");
            DefMenu.AddGroupLabel("Savunma Ayarları");
            DefMenu.Add("Zhonyas", new CheckBox("Kullan Zhonyas", false));
            DefMenu.Add("Zhonyash", new Slider("Kullan Zhonyas için Can", 25, 0, 100));
            DefMenu.AddSeparator();
            DefMenu.Add("Seraph", new CheckBox("1000mana", false));
            DefMenu.Add("Seraphh", new Slider("1000mana itemi kullanmak için can", 45, 0, 100));
            DefMenu.AddSeparator();
            DefMenu.Add("FaceOfTheMountain", new CheckBox("Dağın sureti kullan", false));
            DefMenu.Add("FaceOfTheMountainh", new Slider("Dağın sureti için can", 50, 0, 100));
            DefMenu.AddSeparator();
            DefMenu.Add("Solari", new CheckBox("Solarinin broşu kullan", false));
            DefMenu.Add("Solarih", new Slider("Solari için can", 30, 0, 100));
            DefMenu.AddSeparator();
            DefMenu.Add("Randuin", new CheckBox("Randuin kullan", false));
            DefMenu.Add("Randuinh", new Slider("Randuin için gereken düşman", 2, 1, 5));
            DefMenu.AddSeparator();
            DefMenu.AddGroupLabel("Zhonya Tehlikeli Büyüler");
            DefMenu.Add("ZhonyasD", new CheckBox("Tehlikeli büyülerde kullanma", false));
            Zhonya.OnLoad();
        }

        internal static void Items()
        {
            if (Randuin.IsReady() && Randuin.IsOwned(Player.Instance)
                && Player.Instance.CountEnemiesInRange(Randuin.Range) >= DefMenu["Randuinh"].Cast<Slider>().CurrentValue
                && DefMenu["Randuin"].Cast<CheckBox>().CurrentValue)
            {
                Randuin.Cast();
            }
        }
    }
}