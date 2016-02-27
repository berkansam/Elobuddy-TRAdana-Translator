﻿using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

using Settings = KA_Kayle.Config.Modes.LaneClear;

namespace KA_Kayle.Modes
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
                    .FirstOrDefault(m => m.IsValidTarget(Q.Range));

            if (minion == null) return;

            if (Player.Instance.ManaPercent < Settings.LaneMana) return;

            if (Q.IsReady() && minion.IsValidTarget(Q.Range) && Settings.UseQ)
            {
                Q.Cast(minion);
            }

            if (E.IsReady() && minion.IsValidTarget(E.Range) && Settings.UseE)
            {
                E.Cast();
            }
        }
    }
}
