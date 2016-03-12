using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace Mario_sGangplank.Logics
{
    internal class ComboLogics : Helpers
    {
        #region Agressive

        public static void castQ(Obj_AI_Base target)
        {
            if (Player.Instance.Spellbook.GetSpell(SpellSlot.E).Ammo >= 1)
            {
                castQBarrel();
            }
            else
            {
                var barrel = Barrrels.GetKillBarrelWithEemyInside();
                if (barrel != null)
                {
                    if (barrel.IsValidTarget(Q.Range) && Q.IsReady())
                    {
                        Q.Cast(barrel);
                    }
                }

                castQAlone(target);
            }
        }

        public static void castQAlone(Obj_AI_Base target)
        {
            if (target.IsValidTarget(Q.Range) && Q.IsReady())
            {
                Q.Cast(target);
            }
        }

        public static void castQBarrel()
        {
            var barrel = Barrrels.GetKillBarrelWithEemyInside();
            if (barrel != null)
            {
                if (barrel.IsValidTarget(Q.Range) && Q.IsReady())
                {
                    Q.Cast(barrel);
                }
            }
            else
            {
                var barrelwithenemy = Barrrels.GetBarrelWithEemyInside();
                var ClosestkillBarrel = Barrrels.GetKillBarrelClosest();
                if (barrelwithenemy != null && ClosestkillBarrel != null)
                {
                    if (ClosestkillBarrel.IsInRange(barrelwithenemy, 850))
                    {
                        Q.Cast(ClosestkillBarrel);
                    }
                }
            }
        }

        public static void castE(Obj_AI_Base target)
        {
            if (!target.IsValidTarget(E.Range + 50) || !E.IsReady()) return;
            var barrelNearPlayer = Barrrels.GetBarrels().FirstOrDefault(b => b.IsInRange(Player.Instance, Q.Range + 150));
            if (barrelNearPlayer == null)
            {
                var sliderClose = GetSliderValue(MenuTypes.Combo, "eComboRangeClose");
                var sliderFar = GetSliderValue(MenuTypes.Combo, "eComboRangeFar");
                E.Cast(target.IsInRange(Player.Instance, 650) ? Player.Instance.Position.Extend(target, sliderClose).To3D() : Player.Instance.Position.Extend(target, sliderFar).To3D());
            }
            else if (barrelNearPlayer.Health <= 1 && barrelNearPlayer.Health >= 1)
            {
                var pred = E.GetPrediction(target);
                var barrel = Barrrels.GetBarrels().FirstOrDefault(b => b.Distance(pred.CastPosition) <= 380);

                if (barrel == null)
                {
                    var predpos = pred.CastPosition;
                    if (Q.IsReady() && predpos.Distance(barrelNearPlayer) <= 825)
                    {
                        E.Cast(predpos);
                        CastEBetween();
                        var killBC = Barrrels.GetKillBarrelClosest();
                        var barrelWithENemy = Barrrels.GetBarrelWithEemyInside();
                        if (killBC != null && barrelWithENemy != null && killBC.Distance(barrelWithENemy) < 825)
                        {
                            Q.Cast(killBC);
                        }
                    }
                }
            }
        }

        public static void CastEBetween()
        {
            var killBC = Barrrels.GetKillBarrelClosest();
            if (killBC != null)
            {
                var buffKillableLink = killBC.Buffs.FirstOrDefault(b => b.Name.ToLower().Contains("link"));
                var barrelWithEnemy = Barrrels.GetBarrelWithEemyInside();
                if (barrelWithEnemy != null && barrelWithEnemy.Health > 1)
                {
                    var buffWithEnemyLink = killBC.Buffs.FirstOrDefault(b => b.Name.ToLower().Contains("link"));
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    if (barrelWithEnemy != null && buffWithEnemyLink == null && buffKillableLink == null && E.IsReady())
                    {
                        var pos = killBC.Position.Extend(barrelWithEnemy, 500).To3D();
                        if (pos.Distance(barrelWithEnemy) <= 825)
                        {
                            E.Cast(pos);
                        }
                    }
                }
            }
        }

        public static void CastQMultipleBarrels()
        {
            var killBC = Barrrels.GetKillBarrelClosest();
            if (killBC != null)
            {
                var buffKillableLink = killBC.Buffs.FirstOrDefault(b => b.Name.ToLower().Contains("link"));
                var barrelWithEnemy = Barrrels.GetBarrelWithEemyInside();
                if (barrelWithEnemy != null && barrelWithEnemy.Health >= 1)
                {
                    var buffWithEnemyLink = killBC.Buffs.FirstOrDefault(b => b.Name.ToLower().Contains("link"));
                    if (buffWithEnemyLink != null && buffKillableLink != null && Q.IsReady())
                    {
                        Q.Cast(killBC);
                    }
                }
            }
        }

        public static void castR(int count)
        {
            var target = TargetSelector.GetTarget(int.MaxValue, dmgType);
            if (target == null || target.IsZombie || target.HasUndyingBuff()) return;
            if (R.IsReady() && target.CountEnemiesInRange(490) >= count)
            {
                Player.Instance.Spellbook.CastSpell(SpellSlot.R, target.Position, target.Position);
            }
        }

        public static void castRKS(Obj_AI_Base target)
        {
            if (R.IsReady() && Prediction.Health.GetPrediction(target, 350) <= GetRKSDamage(target) &&
                !Player.Instance.IsInRange(target, 900) &&
                Prediction.Health.GetPrediction(target, 350) <= GetSliderValue(MenuTypes.Settings, "rKSOverkill"))
            {
                Player.Instance.Spellbook.CastSpell(SpellSlot.R,
                    target.ServerPosition.Extend(target.Direction.To2D().Perpendicular(), 200).To3D());
            }
        }

        public static void castRSaveAlly(int allyHPPercent)
        {
            var ally = EntityManager.Heroes.Allies.FirstOrDefault(a => a.HealthPercent <= allyHPPercent && !a.IsMe);
            if (ally == null) return;
            var enemy = EntityManager.Heroes.Enemies.OrderBy(e => e.HealthPercent).FirstOrDefault(a => a.IsInRange(ally, 500));
            if (enemy == null) return;
            if (R.IsReady() && enemy.HealthPercent + 15 > ally.HealthPercent)
            {
                Player.Instance.Spellbook.CastSpell(SpellSlot.R, ally.ServerPosition.Extend(ally.Direction.To2D().Perpendicular(), 200).To3D());
            }
        }

        #endregion Agressive
    }
}
