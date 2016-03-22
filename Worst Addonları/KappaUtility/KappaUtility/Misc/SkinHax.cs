namespace KappaUtility.Misc
{
    using EloBuddy;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;

    internal class SkinHax
    {
        public static Menu SkinMenu { get; private set; }

        internal static void OnLoad()
        {
            SkinMenu = Load.UtliMenu.AddSubMenu("Skin Hax");
            SkinMenu.AddGroupLabel("Skin Ayarları");
            SkinMenu.Add(Player.Instance.ChampionName + "skin", new CheckBox("Aktif", false));
            var setskin = SkinMenu.Add(Player.Instance.ChampionName + "skins", new Slider("Seç Skin", 0, 0, 15));
            setskin.OnValueChange += delegate { Hax(); };

            SkinMenu.AddLabel("Kendine göre skin seçebilirsin.");
        }

        public static void Hax()
        {
            if (SkinMenu[Player.Instance.ChampionName + "skin"].Cast<CheckBox>().CurrentValue
                && Player.Instance.SkinId
                != SkinMenu[Player.Instance.ChampionName + "skins"].Cast<Slider>().CurrentValue)
            {
                Player.Instance.SetSkinId(SkinMenu[Player.Instance.ChampionName + "skins"].Cast<Slider>().CurrentValue);
            }
        }
    }
}