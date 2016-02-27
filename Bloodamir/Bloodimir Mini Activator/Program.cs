using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Bloodimir_Mini_Activator
{
    internal static class Program

    {
        public static Menu ConfigMenu;
        private static readonly AIHeroClient Player = ObjectManager.Player;
        public static Item Zhonia;
        public static Item Seraph;
        public static Item Talisman;
        public static Item Randuin;
        public static Item Queen;
        public static Item Mercur;
        public static Item Sash;
        public static Item Mikael;
        public static Item Face;
        public static Item Locket;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            Zhonia = new Item((int)ItemId.Zhonyas_Hourglass);
            Talisman = new Item((int)ItemId.Talisman_of_Ascension);
            Randuin = new Item((int)ItemId.Randuins_Omen);
            Seraph = new Item((int)ItemId.Seraphs_Embrace);
            Queen = new Item((int)ItemId.Frost_Queens_Claim);
            Sash = new Item((int)ItemId.Quicksilver_Sash);
            Mercur = new Item((int)ItemId.Mercurial_Scimitar);
            Mikael = new Item((int)ItemId.Mikaels_Crucible);
            Face = new Item((int)ItemId.Face_of_the_Mountain);
            Locket = new Item((int)ItemId.Locket_of_the_Iron_Solari);

            ConfigMenu = MainMenu.AddMenu("Ayar Menu", "configmenu");
            ConfigMenu.AddLabel("Bloodimir Mini Activator 1.0.1.0 çeviri tradana");
            ConfigMenu.Add("zhonya", new CheckBox("Kullan Zhonya"));
            ConfigMenu.Add("zhealth", new Slider("Canım Şundan Azsa Kullan %", 25));
            ConfigMenu.Add("seraph", new CheckBox("Kullan Seraph"));
            ConfigMenu.Add("shealth", new Slider("Canım Şundan azsa Kullan %", 35));
            ConfigMenu.Add("smana", new Slider("Otomaik Seraph için manam şu kadar %", 40));
            ConfigMenu.AddSeparator();
            ConfigMenu.Add("mikael", new CheckBox("Kullan Mikail'in Kazanı"));
            ConfigMenu.Add("mhealth", new Slider("Kullan Canım Şu Kadarken %", 70));
            ConfigMenu.Add("face", new CheckBox("Kullan Dağın Sureti"));
            ConfigMenu.Add("fhealth", new Slider("Otomatik Kullan Canım şundan azken %", 40));
            ConfigMenu.AddSeparator();
            ConfigMenu.Add("locket", new CheckBox("Kullan Demir Solari'nin Broşu"));
            ConfigMenu.Add("randuin", new CheckBox("Kullan Randuin Alameti"));
            ConfigMenu.Add("talisman", new CheckBox("Kullan Talisman"));
            ConfigMenu.Add("queen", new CheckBox("Kullan Buz Kraliçesinin Hakkı"));
            ConfigMenu.Add("qss", new CheckBox("Kullan QSS - Civa Yatağan"));
            ConfigMenu.AddSeparator();
            ConfigMenu.Add("qsscdelay", new Slider("QSS Gecikme to CC's", 250, 0, 2000));
            ConfigMenu.Add("qssduelay", new Slider("QSS Gecikme to Hasar/Ultiler", 750, 0 , 2000));
            ConfigMenu.Add("ZedR", new CheckBox("QSS Zed R"));
            ConfigMenu.Add("VladR", new CheckBox("QSS Vlad R"));
            ConfigMenu.Add("FizzR", new CheckBox("QSS Fizz R"));
            ConfigMenu.Add("MordeR", new CheckBox("QSS Morde R"));

            Obj_AI_Base.OnBuffGain += OnBuffGain;
            Game.OnUpdate += OnUpdate;
        }
        private static bool Immobile(Obj_AI_Base unit)
        {
            return unit.HasBuffOfType(BuffType.Charm) || unit.HasBuffOfType(BuffType.Stun) ||
                   unit.HasBuffOfType(BuffType.Knockup) || unit.HasBuffOfType(BuffType.Snare) ||
                   unit.HasBuffOfType(BuffType.Taunt) || unit.HasBuffOfType(BuffType.Suppression) ||
                   unit.HasBuffOfType(BuffType.Polymorph);
        }
        public static bool ZedR { get { return ConfigMenu["ZedR"].Cast<CheckBox>().CurrentValue; } }
        public static bool VladR { get { return ConfigMenu["VladR"].Cast<CheckBox>().CurrentValue; } }
        public static bool FizzR { get { return ConfigMenu["FizzR"].Cast<CheckBox>().CurrentValue; } }
        public static bool MordeR { get { return ConfigMenu["MordeR"].Cast<CheckBox>().CurrentValue; } }
        private static void NoCc()
        {
            if (Immobile(Player) && ConfigMenu["qss"].Cast<CheckBox>().CurrentValue)
            {
                if (Sash.IsOwned() && Sash.IsReady())
                {
                    Core.DelayAction(() => Sash.Cast(), ConfigMenu["qsscdelay"].Cast<Slider>().CurrentValue);
                }
                else if (Immobile(Player))
                {
                    if (!Sash.IsOwned() || !Sash.IsReady() && Mercur.IsOwned() && Mercur.IsReady())
                    {
                        Core.DelayAction(() => Mercur.Cast(), ConfigMenu["qsscdelay"].Cast<Slider>().CurrentValue);
                    }
                }
            }
        }
        private static void NoUlt()
        {
            if (ConfigMenu["qss"].Cast<CheckBox>().CurrentValue)
            {
                if (Sash.IsOwned() && Sash.IsReady())
                {
                    Core.DelayAction(() => Sash.Cast(), ConfigMenu["qssudelay"].Cast<Slider>().CurrentValue);
                }
                if (!Sash.IsOwned() || !Sash.IsReady() && Mercur.IsOwned() && Mercur.IsReady())
                
                    {
                        Core.DelayAction(() => Sash.Cast(), ConfigMenu["qssudelay"].Cast<Slider>().CurrentValue);
                    }
                else if (!Sash.IsOwned() && !Mercur.IsOwned() && Zhonia.IsOwned() && Zhonia.IsReady())
                {
                    Zhonia.Cast();
                }
            }   
        }
        private static void Zhonya()
        {
            var zhoniaon = ConfigMenu["zhonya"].Cast<CheckBox>().CurrentValue;
            var zhealth = ConfigMenu["zhealth"].Cast<Slider>().CurrentValue;

            if (!zhoniaon || !Zhonia.IsReady() || !Zhonia.IsOwned()) return;
            if (Player.HealthPercent <= zhealth)
            {
                Zhonia.Cast();
            }
        }

        private static void RanduinU()
        {
            if (Randuin.IsReady() && Randuin.IsOwned())
            {
                var randuin = ConfigMenu["randuin"].Cast<CheckBox>().CurrentValue;
                if (randuin && Player.HealthPercent <= 15 && Player.CountEnemiesInRange(Randuin.Range) >= 1 ||
                    Player.CountEnemiesInRange(Randuin.Range) >= 2)
                {
                    Randuin.Cast();
                }
            }
        }

        private static void MikaelU()
        {
            if (ConfigMenu["mikael"].Cast<CheckBox>().CurrentValue)
            { 
            var ally = EntityManager.Heroes.Allies.FirstOrDefault(a => a.HealthPercent > ConfigMenu["zhealth"].Cast<Slider>().CurrentValue && Immobile(a));
            if (Mikael.IsOwned() && Mikael.IsReady())
            {
                Mikael.Cast(ally);
            }
            }
        }
        private static void MountainU()
        {
            if (ConfigMenu["face"].Cast<CheckBox>().CurrentValue)
            { 
            var ally = EntityManager.Heroes.Allies.FirstOrDefault(a => a.HealthPercent > ConfigMenu["fhealth"].Cast<Slider>().CurrentValue);
            if (Face.IsOwned() && Face.IsReady())
            {
                Face.Cast(ally);
            }
            }
        }

        private static void AscensionU()
        {
            if (Talisman.IsReady() && Talisman.IsOwned())
            {
                var ascension = ConfigMenu["talisman"].Cast<CheckBox>().CurrentValue;
                if (ascension && Player.HealthPercent <= 15 && Player.CountEnemiesInRange(800) >= 1 ||
                    Player.CountEnemiesInRange(900) >= 3)
                {
                    Talisman.Cast();
                }
            }
        }

        private static void FrostQueen()
        {
            var frostqueen = ConfigMenu["queen"].Cast<CheckBox>().CurrentValue;
            if (frostqueen && Queen.IsReady() && Queen.IsOwned() && Player.CountEnemiesInRange(1500) >= 2 ||
                Player.CountEnemiesInRange(800) >= 3)
            {
                Queen.Cast();
            }
        }

        private static void LocketSolari()
        {
            var solari = ConfigMenu["locket"].Cast<CheckBox>().CurrentValue;
            if (solari && Locket.IsOwned() && Locket.IsReady() && Player.CountAlliesInRange(500) >= 2 &&
                Player.CountEnemiesInRange(890) >= 2)
            {
                Locket.Cast();

            }
        }
        private static void SeraphsEmbrace()
        {
            if (Seraph.IsReady() && Seraph.IsOwned())
            {
                var embrace = ConfigMenu["seraph"].Cast<CheckBox>().CurrentValue;
                var shealth = ConfigMenu["shealth"].Cast<Slider>().CurrentValue;
                var smana = ConfigMenu["smana"].Cast<Slider>().CurrentValue;
                if (!embrace || !Zhonia.IsReady() || !Zhonia.IsOwned()) return;
                if (Player.HealthPercent <= shealth && Player.ManaPercent >= smana)
                {
                    Seraph.Cast();
                }
            }
        }

        private static void OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            var buffName = args.Buff.Name.ToLower();
            if (buffName == "zedrdeathmark" && ZedR)
            {
                NoUlt();
            }
            if (buffName == "vladimirhemoplague" && VladR)
            {
                NoUlt();
            }
            if (buffName == "fizzmarinerdoom" && FizzR)
            {
                NoUlt();
            }
            if (buffName == "mordekaiserchildrenofthegrave" && MordeR)
            {
                NoUlt();
            }
        }
        private static void OnUpdate(EventArgs args)
        {
            Zhonya();
            AscensionU();
            RanduinU();
            FrostQueen();
            SeraphsEmbrace();
            NoCc();
            MikaelU();
            MountainU();
            LocketSolari();
        }
    }
}
