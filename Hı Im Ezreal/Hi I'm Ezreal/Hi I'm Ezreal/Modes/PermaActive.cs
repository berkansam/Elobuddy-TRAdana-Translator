using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using Settings = AddonTemplate.Config.Modes.KillSteal;

namespace AddonTemplate.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                return;
            if (!Player.Instance.IsRecalling())
            {
                KsChamp();
                AutoCCed();
                //  KsBuff();
                QIfUnkillable();
                AutoHarass();
                StackTear();
            }
        }

        public static void StackTear()
        {
            if (Config.Modes.Misc.AutoTear)
            {
                if (Player.Instance.IsInShopRange())
                {
                    if (Config.Tear.IsOwned() || Config.Manamume.IsOwned())
                    {
                        Q.Cast(Game.CursorPos);
                        W.Cast(Game.CursorPos);
                    }
                }
            }
        }

        public static Obj_AI_Turret IsUnderTurret()
        {
            return (EntityManager.Turrets.Enemies.OrderBy(x => x.Distance(Player.Instance.Position) <= 750 && !x.IsAlly && !x.IsDead).FirstOrDefault());
        }
        
        private static void AutoHarass()
        {
            if (!Player.Instance.CanAttack || EntityManager.Turrets.Enemies.Any(turret => turret.IsInRange(Player.Instance.Position, 775)))
            {
                return;
            }

            if (Config.Modes.Harass.ToggleQ && Player.Instance.ManaPercent > Config.Modes.Harass.ManaQ && Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Q.Range - 50, DamageType.Physical);
                if (target != null)
                {
                    var predQ = Q.GetPrediction(target);
                    if (Config.Modes.MenuHarass[target.ChampionName + "harass"].Cast<CheckBox>().CurrentValue &&
                        predQ.HitChance >= SpellManager.PredQ())
                    {
                        if (Config.Modes.Harass.DelayAutoHarass)
                            Core.DelayAction(() => Q.Cast(predQ.CastPosition), 500);
                        else
                            Q.Cast(predQ.CastPosition);
                    }
                }
            }
            if (Config.Modes.Harass.ToggleW && Player.Instance.ManaPercent > Config.Modes.Harass.ManaW && W.IsReady())
            {
                var target = TargetSelector.GetTarget(W.Range - 50, DamageType.Physical);
                if (target != null)
                {
                    var predW = W.GetPrediction(target);
                    if (target != null &&
                        Config.Modes.MenuHarass[target.ChampionName + "harass"].Cast<CheckBox>().CurrentValue &&
                        predW.HitChance >= SpellManager.PredW())
                    {
                        if (Config.Modes.Harass.DelayAutoHarass)
                            Core.DelayAction(() => W.Cast(predW.CastPosition), 500);
                        else
                            W.Cast(predW.CastPosition);
                    }
                }
            }
        }

        private static void QIfUnkillable()
        {
            if (Player.Instance.ManaPercent > Config.Modes.Clear.ManaQ && !Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                foreach (
                    var minions in
                        EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                            Player.Instance.ServerPosition, SpellManager.Q.Range))
                {
                    if (Prediction.Health.GetPrediction(minions, (int)(Player.Instance.AttackDelay * 1000)) <= 0 &&
                        (!Orbwalker.CanAutoAttack || !Player.Instance.IsInAutoAttackRange(minions)))
                    {
                        if (Config.Modes.Misc.UseQOnUnkillable &&
                            Player.Instance.GetSpellDamage(minions, SpellSlot.Q) >= minions.Health &&
                            (Orbwalker.LastTarget == null || Orbwalker.LastTarget.NetworkId != minions.NetworkId))
                            Q.Cast(Q.GetPrediction(minions).CastPosition);
                    }
                }
            }
        }
        public static void KsChamp()
        {

            foreach (var enemy in EntityManager.Heroes.Enemies.Where(enemy => enemy.IsValidTarget(Q.Range)))
            {
                if (Settings.KsQ && Q.IsReady() && enemy.IsKillable(SpellSlot.Q))
                {
                    Q.Cast(Q.GetPrediction(enemy).CastPosition);
                }
            }
            foreach (var enemy in EntityManager.Heroes.Enemies.Where(enemy => enemy.IsValidTarget(W.Range)))
            {
                if (Settings.KsW && W.IsReady() && enemy.IsKillable(SpellSlot.W))
                {
                    W.Cast(W.GetPrediction(enemy).CastPosition);
                }
            }
            foreach (var enemy in EntityManager.Heroes.Enemies.Where(enemy => enemy.Distance(Player.Instance) > Settings.MinRRange && enemy.Distance(Player.Instance) < Settings.MaxRRange && enemy.IsValidTarget()))
            {
                if (Settings.KsR && Player.Instance.Position.CountAlliesInRange(2000) <= 3 && enemy.IsKillable(SpellSlot.R))
                {
                    R.Cast(R.GetPrediction(enemy).CastPosition);
                }
            }
        }

        public static void AutoCCed()
        {

            var turret = EntityManager.Turrets.Enemies.FirstOrDefault(x => x.Distance(Player.Instance.Position) <= 775 && !x.IsAlly && !x.IsDead);
            if (turret == null)
            {
                string[] hardCc =
                {
                    "Charm", "Fear", "Flee", "Knockup", "Polymorph", "Sleep", "Slow", "Snare", "Stun", "Suppression", "Taunt"
                };
                foreach (var enemy in EntityManager.Heroes.Enemies)
                {
                    foreach (
                        var debuff in
                            hardCc.Where(debuff => enemy.HasBuffOfType((BuffType)Enum.Parse(typeof(BuffType), debuff)))
                        )
                    {
                        if (Config.Modes.Misc.CcQ && Q.IsReady())
                        {
                            Q.Cast(enemy);
                        }
                        if (Config.Modes.Misc.CcW && W.IsReady())
                        {
                            W.Cast(enemy);
                        }
                    }
                }
            }

        }
        /*
                public static void KsBuff()
                {
                    var monstersBuff = new List<String>();
                    if (Config.Modes.KillSteal.DragonSteal)
                        monstersBuff.Add("SRU_Dragon");
                    if (Config.Modes.KillSteal.BaronSteal)
                        monstersBuff.Add("SRU_Baron");

                    List<Obj_AI_Minion> jungleBuffs = EntityManager.MinionsAndMonsters.GetJungleMonsters(null, Int32.MaxValue, true).ToList();
                    if (MyHero.Team.ToString().Equals("Order"))
                    {
                        if (Config.Modes.KillSteal.RedSteal)
                            monstersBuff.Add("SRU_Red10.1.1");
                        if (Config.Modes.KillSteal.BlueSteal)
                            monstersBuff.Add("SRU_Blue7.1.1");
                    }
                    else
                    {
                        if (Config.Modes.KillSteal.BlueSteal)
                            monstersBuff.Add("SRU_Blue1.1.1");
                        if (Config.Modes.KillSteal.RedSteal)
                            monstersBuff.Add("SRU_Red4.1.1");
                    }

                    foreach (Obj_AI_Minion mob in jungleBuffs)
                    {
                        foreach (string name in monstersBuff)
                        {
                            if (Regex.IsMatch(mob.Name, name + "[0-9.]*$"))
                            {
                                var timeQ = (distance / SpellManager.Q.Speed + SpellManager.Q.CastDelay) * 1000;
                                var timeR = 1000 * (int)(Extensions.Distance(Player.Instance, mob) / SpellManager.R.Speed) + SpellManager.R.CastDelay;
                                var predHealth = Prediction.Health.GetPrediction(mob, timeR);

                                Chat.Print("TimeR : " + timeR + " Pred : " + predHealth);

                               if (Prediction.Health.GetPrediction(mob, (int)timeQ) <= MyHero.GetSpellDamage(mob, SpellSlot.Q)
                                    && Prediction.Health.GetPrediction(mob, (int)timeQ) >= 0)
                                {
                                    Q.Cast(mob);
                                }
                                if (predHealth <= MyHero.GetSpellDamage(mob, SpellSlot.R) - ((myHero.GetSpellDamage(mob, SpellSlot.R) / 100) * 30)) && predHealth >= 0)
                                {
                                    R.Cast(mob);
                                }
                            }
                        }
                    }
                }
                */
    }

}

