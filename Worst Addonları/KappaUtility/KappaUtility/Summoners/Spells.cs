﻿namespace KappaUtility.Summoners
{
    using System.Linq;

    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;
    using static EloBuddy.SDK.DamageLibrary;

    internal class Spells
    {
        public static Spell.Active Heal;

        public static Spell.Active Barrier;

        public static Spell.Targeted Ignite;

        public static Spell.Targeted Smite;

        public static Spell.Targeted Exhaust;

        public static Menu SummMenu { get; private set; }

        internal static void OnLoad()
        {
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            Obj_AI_Base.OnBasicAttack += OnBasicAttack;

            SummMenu = Load.UtliMenu.AddSubMenu("Summoner Spells");
            SummMenu.AddGroupLabel("Sihirdar Büyüleri Ayarları");

            if (Player.Spells.FirstOrDefault(o => o.SData.Name.Contains("SummonerDot")) != null)
            {
                SummMenu.AddGroupLabel("Tutuştur Ayarları");
                SummMenu.Add("ignite", new CheckBox("Ignite", false));
                SummMenu.AddGroupLabel("Tutuştur Kullanma:");
                foreach (var enemy in ObjectManager.Get<AIHeroClient>())
                {
                    CheckBox cb = new CheckBox(enemy.BaseSkinName) { CurrentValue = false };
                    if (enemy.Team != Player.Instance.Team)
                    {
                        SummMenu.Add("DontIgnite" + enemy.BaseSkinName, cb);
                    }
                }

                SummMenu.AddSeparator();
                Ignite = new Spell.Targeted(ObjectManager.Player.GetSpellSlotFromName("SummonerDot"), 600);
            }

            if (Player.Spells.FirstOrDefault(o => o.SData.Name.Contains("SummonerBarrier")) != null)
            {
                SummMenu.AddGroupLabel("Bariyer Ayarları");
                SummMenu.Add("barrier", new CheckBox("Barrier", false));
                SummMenu.Add("barrierme", new Slider("Use On My Health %", 30, 0, 100));
                SummMenu.AddSeparator();
                Barrier = new Spell.Active(ObjectManager.Player.GetSpellSlotFromName("SummonerBarrier"));
            }

            if (Player.Spells.FirstOrDefault(o => o.SData.Name.Contains("SummonerHeal")) != null)
            {
                SummMenu.AddGroupLabel("Can Ayarları");
                SummMenu.Add("Heal", new CheckBox("Can", false));
                SummMenu.Add("Healally", new Slider("Dostların canı şundan aşağıdayken %", 25, 0, 100));
                SummMenu.Add("Healme", new Slider("benim canım şundan aşağıdayken %", 30, 0, 100));
                SummMenu.AddGroupLabel("Canı kullanma:");
                foreach (var ally in ObjectManager.Get<AIHeroClient>())
                {
                    CheckBox cb = new CheckBox(ally.BaseSkinName) { CurrentValue = false };
                    if (ally.Team == Player.Instance.Team)
                    {
                        SummMenu.Add("DontHeal" + ally.BaseSkinName, cb);
                    }
                }

                SummMenu.AddSeparator();
                Heal = new Spell.Active(ObjectManager.Player.GetSpellSlotFromName("SummonerHeal"), 850);
            }

            if (Player.Spells.FirstOrDefault(o => o.SData.Name.Contains("SummonerExhaust")) != null)
            {
                SummMenu.AddGroupLabel("Koşu Ayarları");
                SummMenu.Add("Exhaust", new CheckBox("Koşu", false));
                SummMenu.Add("exhaustally", new Slider("Dostların canı şu kadarken  %", 35, 0, 100));
                SummMenu.Add("exhaustenemy", new Slider("Düşmanın canı şu kadarken %", 40, 0, 100));
                SummMenu.AddGroupLabel("Koşu kullanma:");
                foreach (var enemy in ObjectManager.Get<AIHeroClient>())
                {
                    CheckBox cb = new CheckBox(enemy.BaseSkinName) { CurrentValue = false };
                    if (enemy.Team != Player.Instance.Team)
                    {
                        SummMenu.Add("DontExhaust" + enemy.BaseSkinName, cb);
                    }
                }

                Exhaust = new Spell.Targeted(ObjectManager.Player.GetSpellSlotFromName("SummonerExhaust"), 650);
            }

            if (Player.Spells.FirstOrDefault(o => o.SData.Name.Contains("SummonerSmite")) != null)
            {
                SummMenu.AddGroupLabel("Çarp Ayarları");
                SummMenu.Add("smitemob", new CheckBox("Çarp Canavarlar", false));
                SummMenu.Add("smitecombo", new CheckBox("Çarp Kombo", false));
                SummMenu.Add("smiteks", new CheckBox("Çarp Kill Çalma", false));
                SummMenu.AddGroupLabel("Çarp Kullanma:");
                foreach (var enemy in ObjectManager.Get<AIHeroClient>())
                {
                    CheckBox cb = new CheckBox(enemy.BaseSkinName) { CurrentValue = false };
                    if (enemy.Team != Player.Instance.Team)
                    {
                        SummMenu.Add("DontSmite" + enemy.BaseSkinName, cb);
                    }
                }

                SummMenu.AddGroupLabel("Şu canavarlada çarp kullan:");
                SummMenu.Add("blue", new CheckBox(" Mavi "));
                SummMenu.Add("red", new CheckBox(" Kırmızı "));
                SummMenu.Add("baron", new CheckBox(" Baron "));
                SummMenu.Add("drake", new CheckBox(" Ejder "));
                SummMenu.Add("gromp", new CheckBox(" Kurbağa "));
                SummMenu.Add("krug", new CheckBox(" Golem "));
                SummMenu.Add("razorbeak", new CheckBox(" SivriGagalar "));
                SummMenu.Add("crab", new CheckBox(" yampiri yengeç "));
                SummMenu.Add("murkwolf", new CheckBox(" Alacakurtlar "));
                SummMenu.AddSeparator();
                Smite = new Spell.Targeted(ObjectManager.Player.GetSpellSlotFromName("SummonerSmite"), 500);
            }
        }

        public static void Cast()
        {
            var target =
                ObjectManager.Get<AIHeroClient>()
                    .FirstOrDefault(enemy => enemy.IsValid && enemy.IsEnemy && enemy.IsVisible);

            var ally = ObjectManager.Get<AIHeroClient>().FirstOrDefault(a => a.IsValid && a.IsAlly && a.IsVisible);

            if (Ignite != null)
            {
                var ignitec = SummMenu["ignite"].Cast<CheckBox>().CurrentValue && Ignite.IsReady();

                if (ignitec && target != null
                    && Player.Instance.GetSummonerSpellDamage(target, SummonerSpells.Ignite)
                    >= target.TotalShieldHealth() + (target.HPRegenRate * 4))
                {
                    if (target.IsValidTarget(Ignite.Range) && !target.IsDead
                        && !SummMenu["DontIgnite" + target.BaseSkinName].Cast<CheckBox>().CurrentValue)
                    {
                        Ignite.Cast(target);
                    }
                }
            }

            if (Exhaust != null)
            {
                var exhaustc = SummMenu["Exhaust"].Cast<CheckBox>().CurrentValue && Exhaust.IsReady();
                var Exhaustally = SummMenu["exhaustally"].Cast<Slider>().CurrentValue;
                var Exhaustenemy = SummMenu["exhaustenemy"].Cast<Slider>().CurrentValue;

                if (exhaustc && target != null
                    && (target.IsValidTarget(Exhaust.Range)
                        && !SummMenu["DontExhaust" + target.BaseSkinName].Cast<CheckBox>().CurrentValue))
                {
                    if (target.HealthPercent <= Exhaustenemy)
                    {
                        Exhaust.Cast(target);
                    }

                    if (ally != null && ally.HealthPercent <= Exhaustally)
                    {
                        Exhaust.Cast(target);
                    }
                }
            }
        }

        public static void OnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(args.Target is AIHeroClient))
            {
                return;
            }

            var caster = sender;
            var target = (AIHeroClient)args.Target;

            if ((caster is AIHeroClient || caster is Obj_AI_Turret) && caster != null && target != null)
            {
                if (target.IsAlly && !target.IsMe)
                {
                    if (Exhaust != null)
                    {
                        var exhaustc = SummMenu["Exhaust"].Cast<CheckBox>().CurrentValue && Exhaust.IsReady();
                        var Exhaustally = SummMenu["exhaustally"].Cast<Slider>().CurrentValue;
                        var Exhaustenemy = SummMenu["exhaustenemy"].Cast<Slider>().CurrentValue;

                        if (exhaustc
                            && (target.IsValidTarget(Exhaust.Range)
                                && !SummMenu["DontExhaust" + caster.BaseSkinName].Cast<CheckBox>().CurrentValue))
                        {
                            if (target.HealthPercent <= Exhaustenemy)
                            {
                                Exhaust.Cast(caster);
                            }

                            if (target.HealthPercent <= Exhaustally)
                            {
                                Exhaust.Cast(caster);
                            }
                        }
                    }

                    if (Heal != null && !SummMenu["DontHeal" + target.BaseSkinName].Cast<CheckBox>().CurrentValue)
                    {
                        var healc = SummMenu["Heal"].Cast<CheckBox>().CurrentValue && Heal.IsReady();
                        var healally = SummMenu["Healally"].Cast<Slider>().CurrentValue;
                        if (healc)
                        {
                            if (target.IsInRange(Player.Instance, Heal.Range))
                            {
                                if (target.HealthPercent <= healally)
                                {
                                    Heal.Cast();
                                }

                                if (caster.GetAutoAttackDamage(target) > target.TotalShieldHealth())
                                {
                                    Heal.Cast();
                                }
                            }
                        }
                    }
                }

                if (target.IsMe)
                {
                    if (Heal != null && !SummMenu["DontHeal" + target.BaseSkinName].Cast<CheckBox>().CurrentValue)
                    {
                        var healc = SummMenu["Heal"].Cast<CheckBox>().CurrentValue;
                        var healme = SummMenu["Healme"].Cast<Slider>().CurrentValue;
                        if (healc)
                        {
                            if (target.HealthPercent <= healme)
                            {
                                Heal.Cast();
                            }

                            if (caster.GetAutoAttackDamage(target) > target.TotalShieldHealth())
                            {
                                Heal.Cast();
                            }
                        }
                    }

                    if (Exhaust != null)
                    {
                        var exhaustc = SummMenu["Exhaust"].Cast<CheckBox>().CurrentValue && Exhaust.IsReady();
                        var Exhaustally = SummMenu["exhaustally"].Cast<Slider>().CurrentValue;
                        var Exhaustenemy = SummMenu["exhaustenemy"].Cast<Slider>().CurrentValue;
                        if (exhaustc && !SummMenu["DontExhaust" + caster.BaseSkinName].Cast<CheckBox>().CurrentValue)
                        {
                            if (target.HealthPercent <= Exhaustenemy)
                            {
                                Exhaust.Cast(caster);
                            }

                            if (target.HealthPercent <= Exhaustally)
                            {
                                Exhaust.Cast(caster);
                            }
                        }
                    }

                    if (Barrier != null)
                    {
                        var barrierc = SummMenu["barrier"].Cast<CheckBox>().CurrentValue && Barrier.IsReady();
                        var barrierme = SummMenu["barrierme"].Cast<Slider>().CurrentValue;
                        if (barrierc)
                        {
                            if (target.HealthPercent <= barrierme)
                            {
                                Barrier.Cast();
                            }

                            if (caster.GetAutoAttackDamage(target) > target.TotalShieldHealth())
                            {
                                Barrier.Cast();
                            }
                        }
                    }
                }
            }
        }

        public static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(args.Target is AIHeroClient))
            {
                return;
            }

            var caster = sender;
            var target = (AIHeroClient)args.Target;

            if ((caster is AIHeroClient || caster is Obj_AI_Turret) && caster != null && target != null)
            {
                if (target.IsAlly && !target.IsMe)
                {
                    if (Heal != null && !SummMenu["DontHeal" + target.BaseSkinName].Cast<CheckBox>().CurrentValue)
                    {
                        var healc = SummMenu["Heal"].Cast<CheckBox>().CurrentValue && Heal.IsReady();
                        var healally = SummMenu["Healally"].Cast<Slider>().CurrentValue;
                        if (healc)
                        {
                            if (target.IsInRange(Player.Instance, Heal.Range))
                            {
                                if (target.HealthPercent <= healally)
                                {
                                    Heal.Cast();
                                }

                                if (caster.BaseAttackDamage > target.TotalShieldHealth()
                                    || caster.BaseAbilityDamage > target.TotalShieldHealth())
                                {
                                    Heal.Cast();
                                }
                            }
                        }
                    }

                    if (Exhaust != null)
                    {
                        var exhaustc = SummMenu["Exhaust"].Cast<CheckBox>().CurrentValue && Exhaust.IsReady();
                        var Exhaustally = SummMenu["exhaustally"].Cast<Slider>().CurrentValue;
                        var Exhaustenemy = SummMenu["exhaustenemy"].Cast<Slider>().CurrentValue;
                        if (exhaustc && !SummMenu["DontExhaust" + caster.BaseSkinName].Cast<CheckBox>().CurrentValue)
                        {
                            if (target.HealthPercent <= Exhaustenemy)
                            {
                                Exhaust.Cast(caster);
                            }

                            if (target.HealthPercent <= Exhaustally)
                            {
                                Exhaust.Cast(caster);
                            }
                        }
                    }
                }

                if (target.IsMe)
                {
                    if (Heal != null && !SummMenu["DontHeal" + target.BaseSkinName].Cast<CheckBox>().CurrentValue)
                    {
                        var healc = SummMenu["Heal"].Cast<CheckBox>().CurrentValue && Heal.IsReady();
                        var healme = SummMenu["Healme"].Cast<Slider>().CurrentValue;
                        if (healc)
                        {
                            if (target.HealthPercent <= healme)
                            {
                                Heal.Cast();
                            }

                            if (caster.BaseAttackDamage > target.TotalShieldHealth()
                                || caster.BaseAbilityDamage > target.TotalShieldHealth())
                            {
                                Heal.Cast();
                            }
                        }

                        if (Exhaust != null)
                        {
                            var exhaustc = SummMenu["Exhaust"].Cast<CheckBox>().CurrentValue && Exhaust.IsReady();
                            var Exhaustally = SummMenu["exhaustally"].Cast<Slider>().CurrentValue;
                            var Exhaustenemy = SummMenu["exhaustenemy"].Cast<Slider>().CurrentValue;
                            if (exhaustc && !SummMenu["DontExhaust" + caster.BaseSkinName].Cast<CheckBox>().CurrentValue)
                            {
                                if (target.HealthPercent <= Exhaustenemy)
                                {
                                    Exhaust.Cast(caster);
                                }

                                if (target.HealthPercent <= Exhaustally)
                                {
                                    Exhaust.Cast(caster);
                                }
                            }
                        }
                    }

                    if (Barrier != null)
                    {
                        var barrierc = SummMenu["barrier"].Cast<CheckBox>().CurrentValue && Barrier.IsReady();
                        var barrierme = SummMenu["barrierme"].Cast<Slider>().CurrentValue;
                        if (barrierc)
                        {
                            if (target.HealthPercent <= barrierme)
                            {
                                Barrier.Cast();
                            }

                            if (caster.BaseAttackDamage > target.TotalShieldHealth()
                                || caster.BaseAbilityDamage > target.TotalShieldHealth())
                            {
                                Barrier.Cast();
                            }
                        }
                    }
                }
            }
        }
    }
}