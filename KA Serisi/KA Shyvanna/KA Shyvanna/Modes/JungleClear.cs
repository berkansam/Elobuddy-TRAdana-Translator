using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using KA_Shyvanna;
using Settings = KA_Shyvanna.Config.Modes.LaneClear;

namespace KA_Shyvanna
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            var jgminion =
                EntityManager.MinionsAndMonsters.GetJungleMonsters()
                    .OrderByDescending(j => j.Health)
                    .FirstOrDefault(j => j.IsValidTarget(E.Range));

            if (jgminion == null)return;

            if (Q.IsReady() && jgminion.IsValidTarget(Q.Range) && Settings.UseQ && EventsManager.CanQ)
            {
                Q.Cast();
            }

            if (W.IsReady() && Settings.UseW && jgminion.IsValidTarget(W.Range))
            {
                W.Cast();
            }

            if (E.IsReady() && jgminion.IsValidTarget(E.Range) && Settings.UseE)
            {
                E.Cast(E.GetPrediction(jgminion).CastPosition);
            }
        }
    }
}
