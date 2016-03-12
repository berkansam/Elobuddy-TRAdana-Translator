using System;
using EloBuddy;
using EloBuddy.SDK;
using Mario_sGangplank.Logics;

namespace Mario_sGangplank
{
    internal class ModeManager : Helpers
    {
        internal class Modes : Helpers
        {
            public static void Active()
            {
                ComboLogics.CastQMultipleBarrels();
                ComboLogics.CastEBetween();

                Functions.castW();

                if (GetCheckBoxValue(MenuTypes.Settings, "rToSaveAlly"))
                {
                    var value = GetSliderValue(MenuTypes.Settings, "rToSaveAllyPercent");
                    ComboLogics.castRSaveAlly(value);
                }

                var targeR = TargetSelector.GetTarget(int.MaxValue, dmgType);
                if (targeR != null && !targeR.IsZombie && !targeR.HasUndyingBuff())
                {
                    if (GetCheckBoxValue(MenuTypes.Settings, "rKS"))
                    {
                        ComboLogics.castRKS(targeR);
                    }
                }


                var target = TargetSelector.GetTarget(highestRange, dmgType);
                if (target != null && !target.IsZombie && !target.HasUndyingBuff())
                {
                    if (GetCheckBoxValue(MenuTypes.Settings, "qKS") && Prediction.Health.GetPrediction(target, Q.CastDelay) <= GetDamage(SpellSlot.Q, target))
                    {
                        ComboLogics.castQAlone(target);
                    }

                    if (GetKeyBindValue(MenuTypes.Harass, "keyAutoHarass"))
                    {
                        if (GetCheckBoxValue(MenuTypes.Harass, "qAutoHarass"))
                        {
                            ComboLogics.castQ(target);
                        }

                        if (GetCheckBoxValue(MenuTypes.Harass, "eAutoHarass"))
                        {
                            ComboLogics.castE(target);
                            ComboLogics.CastEBetween();
                            ComboLogics.CastQMultipleBarrels();
                        }
                    }
                }
            }

            public static void Combo()
            {
                
                var target = TargetSelector.GetTarget(highestRange, dmgType);
                if (target != null && !target.IsZombie && !target.HasUndyingBuff())
                {
                    if (GetCheckBoxValue(MenuTypes.Combo, "eCombo"))
                    {
                        ComboLogics.castE(target);
                        ComboLogics.CastEBetween();
                    }

                    if (GetCheckBoxValue(MenuTypes.Combo, "rCombo"))
                    {
                        var count = GetSliderValue(MenuTypes.Combo, "rComboCount");
                        ComboLogics.castR(count);
                    }

                    if (GetCheckBoxValue(MenuTypes.Combo, "qCombo"))
                    {
                        ComboLogics.castQ(target);
                        ComboLogics.CastQMultipleBarrels();
                    }
                }
            }

            public static void Harass()
            {
                var target = TargetSelector.GetTarget(highestRange, dmgType);
                if (target != null && !target.IsZombie && !target.HasUndyingBuff())
                {
                    if (GetCheckBoxValue(MenuTypes.Harass, "qHarass"))
                    {
                        ComboLogics.castQAlone(target);
                    }

                    if (GetCheckBoxValue(MenuTypes.Harass, "eHarass"))
                    {
                        ComboLogics.castE(target);
                    }
                }
            }

            public static void Flee()
            {

            }

            public static void LaneClear()
            {
                if (GetCheckBoxValue(MenuTypes.LaneClear, "qLane") && Player.Instance.ManaPercent >= GetSliderValue(MenuTypes.LaneClear, "manaLane"))
                {
                    var count = GetSliderValue(MenuTypes.LaneClear, "qLaneCount");
                    FarmLogics.laneQBarrel(count);
                }

                if (GetCheckBoxValue(MenuTypes.LaneClear, "eLane"))
                {
                    var count = GetSliderValue(MenuTypes.LaneClear, "eLaneCount");
                    var ecount = GetSliderValue(MenuTypes.LaneClear, "eKeep");
                    FarmLogics.laneE(count, ecount);
                }

                if (GetCheckBoxValue(MenuTypes.LaneClear, "qLaneLast") && Player.Instance.ManaPercent >= GetSliderValue(MenuTypes.LaneClear, "manaLane"))
                {
                    FarmLogics.laneLastQ();
                }
            }

            public static void LastHit()
            {
                if (GetCheckBoxValue(MenuTypes.LastHit, "qLast") && Player.Instance.ManaPercent >= GetSliderValue(MenuTypes.LastHit, "manaLast"))
                {
                    FarmLogics.lastQ();
                }
            }

            public static void JungleClear()
            {
                if (GetCheckBoxValue(MenuTypes.JungleClear, "qJungle"))
                {
                    FarmLogics.jungleQBarrel(2);
                }

                if (GetCheckBoxValue(MenuTypes.JungleClear, "eJungle"))
                {
                    FarmLogics.jungleE(2);
                }

                if (GetCheckBoxValue(MenuTypes.JungleClear, "qJungleLast"))
                {
                    FarmLogics.jungleLastQ();
                }
            }
        }

        public static void InitModeManager()
        {
            Game.OnTick += Game_OnTick;
        }

        private static void Game_OnTick(EventArgs args)
        {
                        try
            {
                Modes.Active();

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                {
                    Modes.Combo();
                }

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                {
                    Modes.Harass();
                }

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                {
                    Modes.Flee();
                }

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                {
                    Modes.LaneClear();
                }

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
                {
                    Modes.LastHit();
                }

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                {
                    Modes.JungleClear();
                }
            }
            catch (Exception exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("============================= ERROR IN MODE =============================");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(exp);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("============================= ERROR IN MODE =============================");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}
