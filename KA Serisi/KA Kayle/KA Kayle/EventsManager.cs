﻿using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;

using Settings = KA_Kayle.Config.Modes.Misc;

namespace KA_Kayle
{
    internal static class EventsManager
    {
        public static void Initialize()
        {
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (!sender.IsEnemy) return;

            if (sender.IsValidTarget(SpellManager.Q.Range) && Player.Instance.ManaPercent > Settings.MiscMana)
            {
                SpellManager.Q.Cast(sender);
            }
        }
    }
}
