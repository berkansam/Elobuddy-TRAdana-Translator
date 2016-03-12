using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Mario_sTemplate.Logics;

namespace Mario_sTemplate
{
    internal class ModeManager : Helpers
    {
        internal class Modes : Helpers
        {
            public static void Active()
            {
                var target = TargetSelector.GetTarget(highestRange, dmgType);
                if (target == null || target.IsZombie || target.HasUndyingBuff()) return;

                if (GetCheckBoxValue(MenuTypes.Harass, "qAutoHarass") &&
                    GetSliderValue(MenuTypes.Harass, "manaAutoHarass") < Player.Instance.ManaPercent)
                {
                    ComboLogics.castQ(target);
                }

                if (GetCheckBoxValue(MenuTypes.Harass, "eAutoHarass") &&
                    GetSliderValue(MenuTypes.Harass, "manaAutoHarass") < Player.Instance.ManaPercent)
                {
                    if (!target.IsUnderHisturret())
                    {
                        ComboLogics.castE(target);
                    }
                }
                var minEne = GetSliderValue(MenuTypes.Combo, "rAutoCount");
                if (minEne > 0)
                {
                    ComboLogics.castR(target, minEne);
                }
            }

            public static void Combo()
            {
                var target = TargetSelector.GetTarget(highestRange, dmgType);

                if (target == null || target.IsZombie || target.HasUndyingBuff()) return;
                //Agressive
                if (GetComboBoxValue(MenuTypes.Combo, "comboBoxComboMode") == 0)
                {
                    if (GetCheckBoxValue(MenuTypes.Combo, "qCombo"))
                    {
                        ComboLogics.castQ(target);
                    }

                    if (GetCheckBoxValue(MenuTypes.Combo, "eCombo"))
                    {
                        ComboLogics.castE(target);
                    }

                    if (GetCheckBoxValue(MenuTypes.Combo, "rCombo"))
                    {
                        var minEne = GetSliderValue(MenuTypes.Combo, "rComboCount");

                        ComboLogics.castR(target, minEne);
                    }
                }
                //Safe
                else
                {
                    if (GetCheckBoxValue(MenuTypes.Combo, "qCombo"))
                    {
                        ComboLogics.castQ(target);
                    }

                    if (GetCheckBoxValue(MenuTypes.Combo, "eCombo"))
                    {
                        ComboLogics.castSafeE(target);
                    }

                    if (GetCheckBoxValue(MenuTypes.Combo, "rCombo"))
                    {
                        var minEne = GetSliderValue(MenuTypes.Combo, "rComboCount");

                        ComboLogics.castR(target, minEne);
                    }
                }
            }

            public static void Harass()
            {
                var target = TargetSelector.GetTarget(highestRange, dmgType);

                if (target == null || target.IsZombie || target.HasUndyingBuff()) return;
                if (GetCheckBoxValue(MenuTypes.Harass, "qHarass") &&
                    GetSliderValue(MenuTypes.Harass, "manaHarass") < Player.Instance.ManaPercent)
                {
                    ComboLogics.castQ(target);
                }

                if (GetCheckBoxValue(MenuTypes.Harass, "eHarass") &&
                    GetSliderValue(MenuTypes.Harass, "manaHarass") < Player.Instance.ManaPercent)
                {
                    ComboLogics.castE(target);
                }
            }

            public static void Flee()
            {
            }

            public static void LaneClear()
            {
                if (GetCheckBoxValue(MenuTypes.LaneClear, "qLane"))
                {
                    FarmLogics.laneQ();
                }

                if (GetCheckBoxValue(MenuTypes.LaneClear, "eLane"))
                {
                    FarmLogics.laneE();
                }
            }

            public static void LastHit()
            {
                if (GetCheckBoxValue(MenuTypes.LastHit, "qLast"))
                {
                    FarmLogics.lastQ();
                }

                if (GetCheckBoxValue(MenuTypes.LastHit, "eLast"))
                {
                    FarmLogics.lastE();
                }
            }

            public static void JungleClear()
            {
                if (GetCheckBoxValue(MenuTypes.JungleClear, "qJungle"))
                {
                    FarmLogics.jungleQ();
                }

                if (GetCheckBoxValue(MenuTypes.JungleClear, "eJungle"))
                {
                    FarmLogics.jungleE();
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
