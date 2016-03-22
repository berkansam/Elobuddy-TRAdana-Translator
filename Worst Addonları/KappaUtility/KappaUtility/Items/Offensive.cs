﻿namespace KappaUtility.Items
{
    using System;

    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;

    internal class Offensive
    {
        public static readonly Item Hydra = new Item(ItemId.Ravenous_Hydra_Melee_Only, 250f);

        public static readonly Item Titanic = new Item(ItemId.Titanic_Hydra, Player.Instance.GetAutoAttackRange());

        public static readonly Item Timat = new Item(ItemId.Tiamat_Melee_Only, 250f);

        public static readonly Item Cutlass = new Item((int)ItemId.Bilgewater_Cutlass, 550);

        public static readonly Item Botrk = new Item((int)ItemId.Blade_of_the_Ruined_King, 550);

        public static readonly Item Youmuu = new Item((int)ItemId.Youmuus_Ghostblade);

        protected static readonly Item Gunblade = new Item(ItemId.Hextech_Gunblade, 700f);

        public static Menu OffMenu { get; private set; }

        internal static void OnLoad()
        {
            OffMenu = Load.UtliMenu.AddSubMenu("Offense Items");
            OffMenu.AddGroupLabel("Saldırgan Ayarları");
            OffMenu.Add("Hydra", new CheckBox("Kullan Hydra / Timat / Titanic", false));
            OffMenu.Add("useGhostblade", new CheckBox("Kullan Youmuu", false));
            OffMenu.Add("UseBOTRK", new CheckBox("Kullan Mahvolmuş Kılıç", false));
            OffMenu.Add("UseBilge", new CheckBox("Kullan Bilgewater Palası", false));
            OffMenu.Add("UseGunblade", new CheckBox("Kullan Hextech Kılıcı", false));
            OffMenu.AddSeparator();
            OffMenu.Add("eL", new Slider("Kullanmak için düşmanın canı", 65, 0, 100));
            OffMenu.Add("oL", new Slider("benim canım", 65, 0, 100));

            Orbwalker.OnPostAttack += Orbwalker_OnPostAttack;
        }

        private static void Orbwalker_OnPostAttack(AttackableUnit target, EventArgs args)
        {
            if (!target.IsEnemy)
            {
                return;
            }

            var useHydra = OffMenu["Hydra"].Cast<CheckBox>().CurrentValue;
            var flags = Orbwalker.ActiveModesFlags;
            if (flags.HasFlag(Orbwalker.ActiveModes.Combo) && useHydra)
            {
                if (Hydra.Cast())
                {
                    Orbwalker.ResetAutoAttack();
                }

                if (Timat.Cast())
                {
                    Orbwalker.ResetAutoAttack();
                }

                if (Titanic.Cast())
                {
                    Orbwalker.ResetAutoAttack();
                }
            }
        }

        internal static void Items()
        {
            var target = TargetSelector.GetTarget(500, DamageType.Physical);
            if (target == null || !target.IsValidTarget())
            {
                return;
            }

            if (Gunblade.IsReady() && Gunblade.IsOwned(Player.Instance) && target.IsValidTarget(Gunblade.Range)
                && target.HealthPercent <= OffMenu["eL"].Cast<Slider>().CurrentValue
                && OffMenu["UseGunblade"].Cast<CheckBox>().CurrentValue)
            {
                Gunblade.Cast(target);
            }

            if (Gunblade.IsReady() && Gunblade.IsOwned(Player.Instance) && target.IsValidTarget(Gunblade.Range)
                && Player.Instance.HealthPercent <= OffMenu["oL"].Cast<Slider>().CurrentValue
                && OffMenu["UseGunblade"].Cast<CheckBox>().CurrentValue)
            {
                Gunblade.Cast(target);
            }

            if (Botrk.IsReady() && Botrk.IsOwned(Player.Instance) && target.IsValidTarget(Botrk.Range)
                && target.HealthPercent <= OffMenu["eL"].Cast<Slider>().CurrentValue
                && OffMenu["UseBOTRK"].Cast<CheckBox>().CurrentValue)
            {
                Botrk.Cast(target);
            }

            if (Botrk.IsReady() && Botrk.IsOwned(Player.Instance) && target.IsValidTarget(Botrk.Range)
                && Player.Instance.HealthPercent <= OffMenu["oL"].Cast<Slider>().CurrentValue
                && OffMenu["UseBOTRK"].Cast<CheckBox>().CurrentValue)
            {
                Botrk.Cast(target);
            }

            if (Cutlass.IsReady() && Cutlass.IsOwned(Player.Instance) && target.IsValidTarget(Cutlass.Range)
                && target.HealthPercent <= OffMenu["eL"].Cast<Slider>().CurrentValue
                && OffMenu["UseBilge"].Cast<CheckBox>().CurrentValue)
            {
                Cutlass.Cast(target);
            }

            if (Cutlass.IsReady() && Cutlass.IsOwned(Player.Instance) && target.IsValidTarget(Cutlass.Range)
                && Player.Instance.HealthPercent <= OffMenu["oL"].Cast<Slider>().CurrentValue
                && OffMenu["UseBilge"].Cast<CheckBox>().CurrentValue)
            {
                Cutlass.Cast(target);
            }

            if (Youmuu.IsReady() && Youmuu.IsOwned(Player.Instance) && target.IsValidTarget(500)
                && OffMenu["useGhostblade"].Cast<CheckBox>().CurrentValue)
            {
                Youmuu.Cast();
            }
        }
    }
}