using System.Linq;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace ScaryKalista
{
    public class Config
    {
        public static Menu Menu { get; private set; }
        public static Menu ComboMenu { get; private set; }
        public static Menu HarassMenu { get; private set; }
        public static Menu LaneMenu { get; private set; }
        public static Menu JungleMenu { get; private set; }
        public static Menu FleeMenu { get; private set; }
        public static Menu SentinelMenu { get; private set; }
        public static Menu MiscMenu { get; private set; }
        public static Menu DrawMenu { get; private set; }
        public static Menu BalistaMenu { get; private set; }
        public static Menu ItemMenu { get; private set; }

        public static void Initialize()
        {
            var blitzcrank = EntityManager.Heroes.Allies.Any(x => x.ChampionName == "Blitzcrank");

            //Initialize the menu
            Menu = MainMenu.AddMenu("Scary Kalista", "ScaryKalista");
            Menu.AddGroupLabel("Scary'nin Kalistasına Hoşgeldin, çevirmen TRAdana!");

            //Combo
            ComboMenu = Menu.AddSubMenu("Kombo");
            {
                ComboMenu.Add("combo.useQ", new CheckBox("Q Kullan"));
                ComboMenu.Add("combo.minManaQ", new Slider("Mininum {0}% manam olduğunda Q Kullan", 40));

                ComboMenu.Add("combo.sep1", new Separator());
                ComboMenu.Add("combo.useE", new CheckBox("E ile Öldür"));
                ComboMenu.Add("combo.gapClose", new CheckBox("Minyona vura vura ilerle"));

                ComboMenu.Add("combo.sep2", new Separator());
                ComboMenu.Add("combo.harassEnemyE", new CheckBox("Minyonları öldürerek hedefi dürt", false));
            }

            //Harass
            HarassMenu = Menu.AddSubMenu("Dürtme");
            {
                HarassMenu.Add("harass.useQ", new CheckBox("Q Kullan"));
                HarassMenu.Add("harass.minManaQ", new Slider("Mininum {0}% manam olduğunda Q Kullan", 60));

                HarassMenu.Add("harass.sep1", new Separator());
                HarassMenu.Add("harass.harassEnemyE", new CheckBox("Minyonları öldürerek hedefi dürt"));
            }

            //LaneClear
            LaneMenu = Menu.AddSubMenu("Lane Temizleme");
            {
                LaneMenu.Add("laneclear.useQ", new CheckBox("Q Kullan"));
                LaneMenu.Add("laneclear.minQ", new Slider("Mininum {0} minyon olduğunda Q Kullan", 3, 2, 10));
                LaneMenu.Add("laneclear.minManaQ", new Slider("Mininum {0}% manam olduğunda Q Kullan", 30));

                LaneMenu.Add("laneclear.sep1", new Separator());
                LaneMenu.Add("laneclear.useE", new CheckBox("E Kullan"));
                LaneMenu.Add("laneclear.minE", new Slider("Mininum {0} minyon olduğunda E çek", 3, 2, 10));
                LaneMenu.Add("laneclear.minManaE", new Slider("Mininum {0}% manam olduğunda E Kullan", 30));

                LaneMenu.Add("laneclear.sep2", new Separator());
                LaneMenu.Add("laneclear.harassEnemyE", new CheckBox("Minyonları öldürerek hedefi dürt"));
            }

            //JungleClear
            JungleMenu = Menu.AddSubMenu("Orman Temizleme");
            {
                JungleMenu.Add("jungleclear.useE", new CheckBox("Orman Kamplarında E Kullan"));
                JungleMenu.Add("jungleclear.miniE", new CheckBox("Küçük Orman Canavarlarında E Kullan", false));
            }
            
            //Flee
            FleeMenu = Menu.AddSubMenu("Kaç! (Flee) ");
            {
                FleeMenu.Add("flee.attack", new CheckBox("Minyona,Şampa vura vura kaç"));
                FleeMenu.Add("flee.useJump", new CheckBox("Kaçma Tuşu Etkinken Q kullanarak Duvardan atla"));
            }

            //Sentinel
            SentinelMenu = Menu.AddSubMenu("Gözcü (W)");
            {
                SentinelMenu.Add("sentinel.castDragon", new KeyBind("Ejdere gözcü gönder", false, KeyBind.BindTypes.HoldActive, 'U'));
                SentinelMenu.Add("sentinel.castBaron", new KeyBind("Barona gözcü gönder", false, KeyBind.BindTypes.HoldActive, 'I'));

                SentinelMenu.Add("sentinel.sep1", new Separator());
                SentinelMenu.Add("sentinel.enable", new CheckBox("Otomatik gözcü gönder", false));
                SentinelMenu.Add("sentinel.noMode", new CheckBox("Only when no modes are active"));
                SentinelMenu.Add("sentinel.alert", new CheckBox("Gözcü Hasar aldığında ping at"));
                SentinelMenu.Add("sentinel.mana", new Slider("Minimum {0}% manam olduğunda otomatik gönder", 40));

                SentinelMenu.Add("sentinel.sep2", new Separator());
                SentinelMenu.Add("sentinel.locationLabel", new Label("Şunlara gözcü gönder:"));
                (SentinelMenu.Add("sentinel.baron", new CheckBox("Baron / Rift Herald"))).OnValueChange += SentinelLocationsChanged;
                (SentinelMenu.Add("sentinel.dragon", new CheckBox("Ejder"))).OnValueChange += SentinelLocationsChanged;
                (SentinelMenu.Add("sentinel.mid", new CheckBox("Mide salla gtsin"))).OnValueChange += SentinelLocationsChanged;
                (SentinelMenu.Add("sentinel.blue", new CheckBox("Blue Güçlendirme"))).OnValueChange += SentinelLocationsChanged;
                (SentinelMenu.Add("sentinel.red", new CheckBox("Red Güçlendirme"))).OnValueChange += SentinelLocationsChanged;
                Sentinel.RecalculateOpenLocations();
            }

            //Misc
            MiscMenu = Menu.AddSubMenu("Ek");
            {
                MiscMenu.Add("misc.labelSteal", new Label("Stealing: you don't have to hold any button"));
                MiscMenu.Add("misc.killstealE", new CheckBox("E ile kill çal"));
                MiscMenu.Add("misc.junglestealE", new CheckBox("E ile orman moblarını çal"));

                MiscMenu.Add("misc.sep1", new Separator());
                MiscMenu.Add("misc.autoE", new CheckBox("Otomatik E Kullan"));
                MiscMenu.Add("misc.autoEHealth", new Slider("Canım {0}% Altında ise otomatik E Kullan, 10, 5, 25));

                MiscMenu.Add("misc.sep2", new Separator());
                MiscMenu.Add("misc.unkillableE", new CheckBox("Öldüremeyeceğim minyonlarda E Kullan"));

                MiscMenu.Add("misc.sep3", new Separator());
                MiscMenu.Add("misc.useR", new CheckBox("Ultiyle Takım Arkadaşımı Koru"));
                MiscMenu.Add("misc.healthR", new Slider("{0}% Canı olduğunda kullan", 15, 5, 25));
            }

            //Items
            ItemMenu = Menu.AddSubMenu("İtemler");
            {
                var cutlass = Items.BilgewaterCutlass;
                ItemMenu.Add("item." + cutlass.ItemInfo.Name, new CheckBox("Kullan " + cutlass.ItemInfo.Name));
                ItemMenu.Add("item." + cutlass.ItemInfo.Name + "Benim Canım", new Slider("Senin canın  {0}%", 80));
                ItemMenu.Add("item." + cutlass.ItemInfo.Name + "Rakibin Canı", new Slider("Rakibin canı şundan azsa {0}%", 80));
                ItemMenu.Add("item.sep", new Separator());

                var bork = Items.BladeOfTheRuinedKing;
                ItemMenu.Add("item." + bork.ItemInfo.Name, new CheckBox("Kullan " + bork.ItemInfo.Name));
                ItemMenu.Add("item." + bork.ItemInfo.Name + "Benim Canım", new Slider("Senin canın {0}%", 80));
                ItemMenu.Add("item." + bork.ItemInfo.Name + "Rakibin Canı", new Slider("Rakibin canı şundan azsa {0}%", 80));
            }

            //Balista
            if (blitzcrank)
            {
                BalistaMenu = Menu.AddSubMenu("Balista");
                {
                    BalistaMenu.Add("balista.comboOnly", new CheckBox("Sadece Balista kombosu içindir"));
                    BalistaMenu.Add("balista.distance", new Slider("Blitzcharkın bana olan minimum mesafesi: {0}", 400, 0, 1200));
                    BalistaMenu.Add("balista.sep", new Separator());
                    BalistaMenu.Add("balista.label", new Label("Balista Kullan:"));
                    foreach (var enemy in EntityManager.Heroes.Enemies)
                    {
                        BalistaMenu.Add("balista." + enemy.ChampionName, new CheckBox(enemy.ChampionName));
                    }
                }
            }

            //Drawings
            DrawMenu = Menu.AddSubMenu("Drawings");
            {
                DrawMenu.Add("draw.Q", new CheckBox("Draw Q range"));
                DrawMenu.Add("draw.W", new CheckBox("Draw W range", false));
                DrawMenu.Add("draw.E", new CheckBox("Draw E range"));
                DrawMenu.Add("draw.R", new CheckBox("Draw R range"));
                DrawMenu.Add("draw.enemyE", new CheckBox("Rakibin canını göster"));
                DrawMenu.Add("draw.percentage", new CheckBox("Draw E damage percentage enemy"));
                DrawMenu.Add("draw.jungleE", new CheckBox("Orman Moblarının Canını Göster"));
                DrawMenu.Add("draw.killableMinions", new CheckBox("Öldürülebilir minyonları göster"));
                DrawMenu.Add("draw.stacks", new CheckBox("Draw E stacks enemy", false));
                DrawMenu.Add("draw.jumpSpots", new CheckBox("Zıplayabileceğim yerleri göster"));
                if (blitzcrank) DrawMenu.Add("draw.balista", new CheckBox("Draw Balista range"));
            }
        }

        private static void SentinelLocationsChanged(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            Sentinel.RecalculateOpenLocations();
        }
    }
}
