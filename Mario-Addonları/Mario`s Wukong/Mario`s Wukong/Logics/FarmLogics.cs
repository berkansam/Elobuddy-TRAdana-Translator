using EloBuddy;

namespace Mario_sTemplate.Logics
{
    internal class FarmLogics : Helpers
    {
        #region LastHit

        public static void lastQ()
        {
            var minionQ = GetLastMinion(SpellSlot.Q);
            if (IsNotNull(minionQ))
            {
                Q.Cast();
            }
        }

        public static void lastE()
        {
            var minionE = GetLastMinion(SpellSlot.E);
            if (IsNotNull(minionE))
            {
                E.Cast(minionE);
            }
        }

        #endregion LastHit

        #region LaneClear

        public static void laneQ()
        {
            var minionQ = GetLaneMinion(Q.Range);
            if (IsNotNull(minionQ))
            {
                Q.Cast();
            }
        }

        public static void laneE()
        {
            var minionE = GetLaneMinion(E.Range);
            if (IsNotNull(minionE))
            {
                E.Cast(minionE);
            }
        }

        #endregion LaneClear

        #region JungleClear

        public static void jungleQ()
        {
            var minionJungle = GetJungleMinion(Q.Range);
            if (IsNotNull(minionJungle))
            {
                Q.Cast();
            }
        }

        public static void jungleE()
        {
            var minionJungle = GetJungleMinion(E.Range);
            if (IsNotNull(minionJungle))
            {
                E.Cast(minionJungle);
            }
        }
        #endregion
    }
}
