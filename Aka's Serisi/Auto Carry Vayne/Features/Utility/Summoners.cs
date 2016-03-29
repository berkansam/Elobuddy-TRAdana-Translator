using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;

namespace Auto_Carry_Vayne.Features.Utility
{
    class Summoners
    {
        #region Heal
        public static void Heal()
        {
            if (Manager.MenuManager.Heal &&
                Variables._Player.CountEnemiesInRange(800) >= 1 &&
                Variables._Player.HealthPercent <= Manager.MenuManager.HealHp)
            {
                Manager.SpellManager.Heal.Cast();
            }
            foreach (
                var ally in EntityManager.Heroes.Allies.Where(a => !a.IsDead))
            {
                if (Manager.MenuManager.HealAlly && ally.CountEnemiesInRange(800) >= 1 &&
                    Variables._Player.Position.Distance(ally) < 600 &&
                    ally.HealthPercent <= Manager.MenuManager.HealAllyHp)
                {
                    Manager.SpellManager.Heal.Cast();
                }
            }
            foreach (
                var ally in EntityManager.Heroes.Allies.Where(a => !a.IsDead))
            {
                if (Manager.MenuManager.HealAlly &&
                    Variables._Player.Position.Distance(ally) < 600 && ally.HasBuff("summonerdot") &&
                    ally.HealthPercent <= Manager.MenuManager.HealAllyHp)
                {
                    Manager.SpellManager.Heal.Cast();
                }
            }

            if (Manager.MenuManager.Heal && Variables._Player.HasBuff("summonerdot") &&
                Variables._Player.HealthPercent <= Manager.MenuManager.HealHp)
            {
                Manager.SpellManager.Heal.Cast();
            }
        }
        #endregion Heal
    }
}
