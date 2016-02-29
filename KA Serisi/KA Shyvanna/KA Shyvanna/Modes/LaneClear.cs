using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using KA_Shyvanna;
using Settings = KA_Shyvanna.Config.Modes.LaneClear;

namespace KA_Shyvanna
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            var minion =
                EntityManager.MinionsAndMonsters.GetLaneMinions()
                    .OrderByDescending(m => m.Health)
                    .FirstOrDefault(m => m.IsValidTarget(E.Range));

            if (minion == null) return;

            if (Q.IsReady() && minion.IsValidTarget(Q.Range) && Settings.UseQ && EventsManager.CanQ)
            {
                Q.Cast();
            }

            if (W.IsReady() && Settings.UseW && minion.IsValidTarget(W.Range))
            {
                W.Cast();
            }

            if (E.IsReady() && minion.IsValidTarget(E.Range) && Settings.UseE)
            {
                E.Cast(E.GetPrediction(minion).CastPosition);
            }
        }
    }
}
