namespace KappaUtility.Items
{
    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;

    internal class Potions
    {
        public static Menu PotMenu { get; private set; }

        public static readonly Item Corrupting = new Item(ItemId.Corrupting_Potion);

        public static readonly Item Health = new Item(ItemId.Health_Potion);

        public static readonly Item Hunters = new Item(ItemId.Hunters_Potion);

        public static readonly Item Refillable = new Item((int)ItemId.Refillable_Potion);

        public static readonly Item Biscuit = new Item((int)ItemId.Total_Biscuit_of_Rejuvenation);

        internal static void OnLoad()
        {
            PotMenu = Load.UtliMenu.AddSubMenu("Potions");
            PotMenu.AddGroupLabel("İksir Ayarları");
            PotMenu.Add("CP", new CheckBox("Musibet iksiri", false));
            PotMenu.Add("CPH", new Slider("gereken can %", 65, 0, 100));
            PotMenu.Add("HP", new CheckBox("Can İksiri", false));
            PotMenu.Add("HPH", new Slider("gereken can %", 45, 0, 100));
            PotMenu.Add("HPS", new CheckBox("Avcı İksiri", false));
            PotMenu.Add("HPSH", new Slider("gereken can %", 75, 0, 100));
            PotMenu.Add("RP", new CheckBox("Yeniden Doldurulabilir İksir", false));
            PotMenu.Add("RPH", new Slider("gereken can %", 50, 0, 100));
            PotMenu.Add("BP", new CheckBox("Bisküvi", false));
            PotMenu.Add("BPH", new Slider("gereken can %", 40, 0, 100));
        }
    }
}