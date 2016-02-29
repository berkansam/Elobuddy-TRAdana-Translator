using EloBuddy;
using EloBuddy.SDK;
using Settings = KA_Akali.Config.Modes.Combo;

namespace KA_Akali.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(R.Range, DamageType.Magical);
            if (target == null || target.IsZombie) return;

            if (Q.IsReady() && target.IsValidTarget(Q.Range) && Settings.UseQ)
            {
                Q.Cast(target);
            }

            if (R.IsReady() && target.IsValidTarget(R.Range) && Settings.UseR && (target.HasBuff("AkaliMota") || target.HealthPercent +20 <= Player.Instance.HealthPercent))
            {
                R.Cast(target);
            }

            if (E.IsReady() && target.IsValidTarget(E.Range) && Settings.UseE)
            {
                E.Cast();
            }

            if (W.IsReady() && target.IsValidTarget(Q.Range) && Settings.UseW)
            {
                if (Player.Instance.CountEnemiesInRange(Q.Range) >= 2 || Player.Instance.HealthPercent <= 18)
                {
                    W.Cast(Player.Instance);
                }
            }
        }
    }
}
