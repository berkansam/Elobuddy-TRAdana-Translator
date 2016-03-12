using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace Mario_sGangplank.Logics
{
    internal class FarmLogics : Helpers
    {
        #region LastHit

        public static void lastQ()
        {
            var minionQ = GetLastMinion(SpellSlot.Q);
            if (IsNotNull(minionQ) && !minionQ.IsInRange(Player.Instance, Player.Instance.GetAutoAttackRange()))
            {
                Q.Cast(minionQ);
            }
        }

        #endregion LastHit

        #region LaneClear

        public static void laneLastQ()
        {
            var minionQ = GetLastMinion(SpellSlot.Q);
            var barrels = Barrrels.GetBarrels().Where(b => b.IsValidTarget(Q.Range));
            if (!barrels.Any())
            {
                if (IsNotNull(minionQ))
                {
                    Q.Cast(minionQ);
                }
            }
        }

        public static void laneE(int count, int keepE)
        {
            var minions = EntityManager.MinionsAndMonsters.GetLaneMinions().Where(m => m.IsValidTarget(E.Range)).ToArray();
            if(minions.Length == 0)return;
            var pos = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(minions, E.Width, (int) E.Range);
            var barrel = Barrrels.GetBarrels().FirstOrDefault(b => b.Distance(pos.CastPosition) <= 350);

            if (pos.HitNumber >= count && barrel == null && Player.GetSpell(SpellSlot.E).Ammo > keepE)
            {
                E.Cast(pos.CastPosition);
            }

        }

        public static void laneQBarrel(int count)
        {
            var barrel = Barrrels.GetKillBarrelClosest();
            if (IsNotNull(barrel) && Q.IsReady() && barrel.IsValidTarget(Q.Range))
            {
                var minion = EntityManager.MinionsAndMonsters.GetLaneMinions().Count(m => m.IsInRange(barrel, 350) && m.IsEnemy);
                if (minion >= count)
                {
                    Q.Cast(barrel);
                }
            }
        }

        #endregion LaneClear

        #region JungleClear
        public static void jungleLastQ()
        {
            var minionQ = GetJungleMinion(Q.Range);
            var barrels = Barrrels.GetBarrels();
            if (!barrels.Any())
            {
                if (IsNotNull(minionQ))
                {
                    Q.Cast(minionQ);
                }
            }
        }

        public static void jungleE(int count)
        {
            var minions = EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(m => m.IsValidTarget(E.Range)).ToArray();
            if (minions.Length == 0) return;
            var pos = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(minions, E.Width, (int)E.Range);
            var barrel = Barrrels.GetBarrels().FirstOrDefault(b => b.Distance(pos.CastPosition) <= 380);

            if (pos.HitNumber >= count && barrel == null)
            {
                E.Cast(pos.CastPosition);
            }

        }

        public static void jungleQBarrel(int count)
        {
            var barrel = Barrrels.GetKillBarrelClosest();
            if (IsNotNull(barrel) && Q.IsReady() && barrel.IsValidTarget(Q.Range))
            {
                var minion = EntityManager.MinionsAndMonsters.GetJungleMonsters().Count(m => m.IsInRange(barrel, 380) && m.IsEnemy);
                if (minion >= count)
                {
                    Q.Cast(barrel);
                }
            }
        }
        #endregion JungleClear
    }
}
