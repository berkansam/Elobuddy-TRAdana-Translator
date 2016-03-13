using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

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
                EBigMinion();
                EBaronDragon();
            }
        }

        private void EBigMinion()
        {
            if (Config.Modes.Misc.EBigMinion)
            {
                if (EntityManager.MinionsAndMonsters.EnemyMinions.Any(c => Player.Instance.Position.IsInRange(c.Position, SpellManager.E.Range)
                && (c.BaseSkinName.Contains("Siege") || c.BaseSkinName.Contains("Super"))
                && c.Health < DamageHelper.GetEDamage(c)))
                {
                    E.Cast();
                }
            }
        }

        private void EBaronDragon()
        {
            if (Config.Modes.Misc.EBaronDragon)
            {
                if (EntityManager.MinionsAndMonsters.Monsters.Any(c => Player.Instance.Position.IsInRange(c.Position, SpellManager.E.Range)
                && (c.BaseSkinName.Contains("Dragon") || c.BaseSkinName.Contains("Baron") || c.BaseSkinName.Contains("Herald"))
                && c.Health < DamageHelper.GetEDamage(c)))
                {
                    E.Cast();
                }
            }
        }

    }
}
