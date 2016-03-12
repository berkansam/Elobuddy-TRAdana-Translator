using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
// ReSharper disable SwitchStatementMissingSomeCases

namespace Mario_sGangplank
{
    internal class Helpers : Spells
    {
        #region
        //Spaghetti code because EB doesnt support C# 6 :(
        //EventsManager
        protected static bool CanPostAttack
        {
            get { return EventsManager.CanPostAttack; }
        }

        protected static bool CanPreAttack
        {
            get { return EventsManager.CanPreAttack; }
        }

        public static bool IsNotNull(Obj_AI_Base target)
        {
            return target != null;
        }
        #endregion

        #region GettingEnemies

        public static Obj_AI_Base GetLastMinion(SpellSlot slot)
        {
            var spell = SpellList.FirstOrDefault(s => s.Slot == slot);
            if (spell == null)return null;

            var minion =
                EntityManager.MinionsAndMonsters.GetLaneMinions()
                    .OrderBy(m => m.Health)
                    .FirstOrDefault(
                        mi =>
                            mi.IsValidTarget(spell.Range) &&
                            Prediction.Health.GetPrediction(mi, spell.CastDelay) <= GetDamage(slot, mi) &&
                            Prediction.Health.GetPrediction(mi, spell.CastDelay) > 20);

            return minion;
        }

        public static Obj_AI_Base GetLaneMinion(uint Range)
        {
            var minion =
                EntityManager.MinionsAndMonsters.GetLaneMinions()
                    .OrderBy(m => m.Health)
                    .FirstOrDefault(
                        mi =>
                            mi.IsValidTarget(Range));

            return minion;
        }

        public static Obj_AI_Base GetJungleMinion(uint range)
        {
            var minion =
                EntityManager.MinionsAndMonsters.GetJungleMonsters()
                    .OrderBy(m => m.Health)
                    .FirstOrDefault(mi => mi.IsValidTarget(range));

            return minion;
        }

        public static Obj_AI_Base GetJungleMinionToKS(SpellSlot slot)
        {
            var spell = SpellList.FirstOrDefault(s => s.Slot == slot);
            if (spell == null) return null;

            var minion =
                EntityManager.MinionsAndMonsters.GetJungleMonsters()
                    .OrderBy(m => m.Health)
                    .FirstOrDefault(
                        mi =>
                            mi.IsValidTarget(spell.Range) &&
                            Prediction.Health.GetPrediction(mi, spell.CastDelay) <= GetDamage(slot, mi) &&
                            Prediction.Health.GetPrediction(mi, spell.CastDelay) > 20);

            return minion;
        }
        #endregion GettingEnemies

        #region KS

        public static AIHeroClient GetBestKSHero(SpellSlot slot)
        {
            var spell = SpellList.FirstOrDefault(s => s.Slot == slot);
            if (spell == null) return null;

            var hero =
                EntityManager.Heroes.Enemies.OrderBy(x => x.Health).ThenBy(TargetSelector.GetPriority)
                    .FirstOrDefault(
                        e =>
                            e.IsValidTarget(spell.Range) &&
                            Prediction.Health.GetPrediction(e, spell.CastDelay) + e.TotalShield() <= GetRKSDamage(e) &&
                            Prediction.Health.GetPrediction(e, spell.CastDelay) > 20);

            return hero;
        }

        public static List<SpellSlot> GetBestSpellsToKS(Obj_AI_Base target, List<Spell.SpellBase> availableSpells)
        {
            var dmg = 0f;
            var delay = 0;
            var spells = new List<SpellSlot>();

            foreach (var spell in from spell in availableSpells let spellInfo = Player.GetSpell(spell.Slot) where spellInfo.IsReady select spell)
            {
                dmg += GetDamage(spell.Slot, target);
                delay += spell.CastDelay;
                spells.Add(spell.Slot);
                if (Prediction.Health.GetPrediction(target, delay) <= dmg) return spells;
            }

            return null;
        }
        #endregion KS

        #region GettingMenuValues

        public enum MenuTypes
        {
            Combo,Harass,LaneClear,JungleClear,LastHit, Settings, Drawings
        }


        public static bool GetCheckBoxValue(MenuTypes type, string checkName)
        {
            var value = false;
            try
            {
                switch (type)
                {
                    case MenuTypes.Combo:
                        value = MenuSettings.ComboMenu.Get<CheckBox>(checkName).CurrentValue;
                        break;
                    case MenuTypes.Harass:
                        value = MenuSettings.HarassMenu.Get<CheckBox>(checkName).CurrentValue;
                        break;
                    case MenuTypes.LaneClear:
                        value = MenuSettings.LaneClearMenu.Get<CheckBox>(checkName).CurrentValue;
                        break;
                    case MenuTypes.JungleClear:
                        value = MenuSettings.JungleClearMenu.Get<CheckBox>(checkName).CurrentValue;
                        break;
                    case MenuTypes.LastHit:
                        value = MenuSettings.LastHitMenu.Get<CheckBox>(checkName).CurrentValue;
                        break;
                    case MenuTypes.Settings:
                        value = MenuSettings.SettingsMenu.Get<CheckBox>(checkName).CurrentValue;
                        break;
                    case MenuTypes.Drawings:
                        value = MenuSettings.DrawingsMenu.Get<CheckBox>(checkName).CurrentValue;
                        break;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("============================= ERROR =============================");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Menu name is wrong please fix it = " + checkName);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("============================= ERROR =============================");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            return value;
        }

        public static int GetSliderValue(MenuTypes type, string sliderName)
        {
            var value = 0;
            try
            {
                switch (type)
                {
                    case MenuTypes.Combo:
                        value = MenuSettings.ComboMenu.Get<Slider>(sliderName).CurrentValue;
                        break;
                    case MenuTypes.Harass:
                        value = MenuSettings.HarassMenu.Get<Slider>(sliderName).CurrentValue;
                        break;
                    case MenuTypes.LaneClear:
                        value = MenuSettings.LaneClearMenu.Get<Slider>(sliderName).CurrentValue;
                        break;
                    case MenuTypes.JungleClear:
                        value = MenuSettings.JungleClearMenu.Get<Slider>(sliderName).CurrentValue;
                        break;
                    case MenuTypes.LastHit:
                        value = MenuSettings.LastHitMenu.Get<Slider>(sliderName).CurrentValue;
                        break;
                    case MenuTypes.Settings:
                        value = MenuSettings.SettingsMenu.Get<Slider>(sliderName).CurrentValue;
                        break;
                    case MenuTypes.Drawings:
                        value = MenuSettings.DrawingsMenu.Get<Slider>(sliderName).CurrentValue;
                        break;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("============================= ERROR =============================");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Menu name is wrong please fix it = " + sliderName);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("============================= ERROR =============================");
                Console.ForegroundColor = ConsoleColor.Gray;
            }


            return value;
        }

        public static bool GetKeyBindValue(MenuTypes type, string keyName)
        {
            var value = false;
            try
            {
                switch (type)
                {
                    case MenuTypes.Combo:
                        value = MenuSettings.ComboMenu.Get<KeyBind>(keyName).CurrentValue;
                        break;
                    case MenuTypes.Harass:
                        value = MenuSettings.HarassMenu.Get<KeyBind>(keyName).CurrentValue;
                        break;
                    case MenuTypes.LaneClear:
                        value = MenuSettings.LaneClearMenu.Get<KeyBind>(keyName).CurrentValue;
                        break;
                    case MenuTypes.JungleClear:
                        value = MenuSettings.JungleClearMenu.Get<KeyBind>(keyName).CurrentValue;
                        break;
                    case MenuTypes.LastHit:
                        value = MenuSettings.LastHitMenu.Get<KeyBind>(keyName).CurrentValue;
                        break;
                    case MenuTypes.Settings:
                        value = MenuSettings.SettingsMenu.Get<KeyBind>(keyName).CurrentValue;
                        break;
                    case MenuTypes.Drawings:
                        value = MenuSettings.DrawingsMenu.Get<KeyBind>(keyName).CurrentValue;
                        break;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("============================= ERROR =============================");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Menu name is wrong please fix it = " + keyName);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("============================= ERROR =============================");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            return value;
        }

        public static int GetComboBoxValue(MenuTypes type, string comboBoxName)
        {
            var value = 0;
            try
            {
                switch (type)
                {
                    case MenuTypes.Combo:
                        value = MenuSettings.ComboMenu.Get<ComboBox>(comboBoxName).SelectedIndex;
                        break;
                    case MenuTypes.Harass:
                        value = MenuSettings.HarassMenu.Get<ComboBox>(comboBoxName).CurrentValue;
                        break;
                    case MenuTypes.LaneClear:
                        value = MenuSettings.LaneClearMenu.Get<ComboBox>(comboBoxName).CurrentValue;
                        break;
                    case MenuTypes.JungleClear:
                        value = MenuSettings.JungleClearMenu.Get<ComboBox>(comboBoxName).CurrentValue;
                        break;
                    case MenuTypes.LastHit:
                        value = MenuSettings.LastHitMenu.Get<ComboBox>(comboBoxName).CurrentValue;
                        break;
                    case MenuTypes.Settings:
                        value = MenuSettings.SettingsMenu.Get<ComboBox>(comboBoxName).CurrentValue;
                        break;
                    case MenuTypes.Drawings:
                        value = MenuSettings.DrawingsMenu.Get<ComboBox>(comboBoxName).CurrentValue;
                        break;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("============================= ERROR =============================");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Menu name is wrong please fix it = " + comboBoxName );
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("============================= ERROR =============================");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            return value;
        }

        #endregion GettingMenuValues
    }
}
