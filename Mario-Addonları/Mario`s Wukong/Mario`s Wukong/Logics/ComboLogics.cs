using EloBuddy;
using EloBuddy.SDK;

namespace Mario_sTemplate.Logics
{
    internal class ComboLogics : Helpers
    {
        #region Agressive
        public static void castQ(Obj_AI_Base target)
        {
            if (target.IsValidTarget(Q.Range) && Q.IsReady() && CanPostAttack)
            {
                Q.Cast();
            }
        }

        public static void castE(Obj_AI_Base target)
        {
            if (target.IsValidTarget(E.Range) && E.IsReady())
            {
                E.Cast(target);
            }
        }

        public static void castR(Obj_AI_Base target, int MinEnemies)
        {
            var toggleState = Player.Instance.Spellbook.GetSpell(SpellSlot.R).ToggleState;
            if (target.IsValidTarget(R.Range) && R.IsReady() &&
                Player.Instance.CountEnemiesInRange(R.Range) >= MinEnemies && toggleState == 0)
            {
                R.Cast();
            }
        }
        #endregion Agressive

        #region Safe
        public static void castSafeE(Obj_AI_Base target)
        {
            if (target.IsValidTarget(E.Range) && E.IsReady() && target.CountEnemiesInRange(800) <= 2)
            {
                E.Cast(target);
            }
        }
        #endregion Safe
    }
}
