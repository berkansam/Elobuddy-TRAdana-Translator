using EloBuddy;
using EloBuddy.SDK;

using Settings = KA_Kayle.Config.Modes.Combo;

namespace KA_Kayle.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            if (target == null || target.IsZombie || target.HasUndyingBuff()) return;

            if (Q.IsReady() && target.IsValidTarget(Q.Range) && Settings.UseQ)
            {
                Q.Cast(target);
            }

            if (E.IsReady() && target.IsValidTarget(E.Range) && Settings.UseE)
            {
                E.Cast();
            }

            if (E.IsReady() && W.IsReady() && !target.IsValidTarget(E.Range) && Settings.UseE)
            {
                W.Cast(Player.Instance);
            }
        }
    }
}
