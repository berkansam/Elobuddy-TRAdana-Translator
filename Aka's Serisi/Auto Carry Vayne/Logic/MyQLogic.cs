using SharpDX;
using ClipperLib;
using EloBuddy;
using EloBuddy.SDK;
using System.Linq;
using System.Collections.Generic;
using System;
using Auto_Carry_Vayne.Manager;

namespace Auto_Carry_Vayne.Logic
{
    public static class MyQLogic
    {
        public static Vector3 GetQPosition()
        {
            #region Variables
            var AverageDistanceWeight = .60f;
            var enemyPositions = GetEnemyPoints();
            var ClosestDistanceWeight = .40f;
            var bestWeightedAvg = 0f;
            var positions = GetRotatedQPositions();
            var safePositions = positions.Where(pos => !enemyPositions.Contains(pos.To2D()));
            var BestPosition = Variables._Player.ServerPosition.Extend(Game.CursorPos, 300f);
            var enemiesNear =
    EntityManager.Heroes.Enemies.Where(
        m => m.IsValidTarget(Variables._Player.GetAutoAttackRange(m) + 300f + 65f));
            var highHealthEnemiesNear =
    EntityManager.Heroes.Enemies.Where(m => !m.IsMelee && m.IsValidTarget(1300f) && m.HealthPercent > 7)
        ;
            #endregion

            #region 1 Enemy
            if (Variables._Player.CountEnemiesInRange(1500f) <= 1)
            {
                //Logic for 1 enemy near
                var position = (Vector3)Variables._Player.ServerPosition.Extend(Game.CursorPos, 300f);
                return position.To2D().IsSafeEx() ? position : Vector3.Zero;
            }
            #endregion

            if (
enemiesNear.Any(
t =>
    t.Health + 15 <
    ObjectManager.Player.GetAutoAttackDamage(t) * 2 + Variables._Player.GetSpellDamage(t, SpellSlot.Q)
    && t.Distance(ObjectManager.Player) < Variables._Player.GetAutoAttackRange(t) + 80f))
            {
                var QPosition =
                    ObjectManager.Player.ServerPosition.Extend(
                        enemiesNear.OrderBy(t => t.Health).First().ServerPosition, 300f);

                if (!Variables.UnderEnemyTower(QPosition))
                {
                    return (Vector3)QPosition;
                }
            }

            #region Alone, 2 Enemies, 1 Killable

            if (enemiesNear.Count() <= 2)
            {
                if (
                    enemiesNear.Any(
                        t =>
                            t.Health + 15 <
                            Variables._Player.GetAutoAttackDamage(t) +
                            Variables._Player.GetSpellDamage(t, SpellSlot.Q)
                            && t.Distance(Variables._Player) < Variables._Player.GetAutoAttackRange(t) + 80f))
                {
                    var QPosition =
                        Variables._Player.ServerPosition.Extend(
                            highHealthEnemiesNear.OrderBy(t => t.Health).FirstOrDefault().ServerPosition, 300f);

                    if (!Variables.UnderEnemyTower(QPosition))
                    {
                        return (Vector3)QPosition;
                    }
                }
            }

            #endregion

            #region Already in an enemy's attack range.

            var closeNonMeleeEnemy =
                GetClosestEnemy((Vector3)Variables._Player.ServerPosition.Extend(Game.CursorPos, 300f));

            if (closeNonMeleeEnemy != null
                && Variables._Player.Distance(closeNonMeleeEnemy) <= closeNonMeleeEnemy.AttackRange - 85
                && !closeNonMeleeEnemy.IsMelee)
            {
                return Variables._Player.ServerPosition.Extend(Game.CursorPos, 300f).IsSafeEx()
                    ? Variables._Player.ServerPosition.Extend(Game.CursorPos, 300f).To3D()
                    : Vector3.Zero;
            }


            #endregion

            #region Logic for multiple enemies / allies around.

            foreach (var position in safePositions)
            {
                var enemy = GetClosestEnemy(position);
                if (!enemy.IsValidTarget())
                {
                    continue;
                }

                var avgDist = GetAvgDistance(position);

                if (avgDist > -1)
                {
                    var closestDist = Variables._Player.ServerPosition.Distance(enemy.ServerPosition);
                    var weightedAvg = closestDist * ClosestDistanceWeight + avgDist * AverageDistanceWeight;
                    if (weightedAvg > bestWeightedAvg && position.To2D().IsSafeEx())
                    {
                        bestWeightedAvg = weightedAvg;
                        BestPosition = position.To2D();
                    }
                }
            }
            #endregion

            var endPosition = (BestPosition.To3D().IsSafe()) ? BestPosition.To3D() : Vector3.Zero;

            #region Couldn't even tumble to ally, just go to mouse

            if (endPosition == Vector3.Zero)
            {
                var mousePosition = Variables._Player.ServerPosition.Extend(Game.CursorPos, 300f);
                if (mousePosition.To3D().IsSafe())
                {
                    endPosition = mousePosition.To3D();
                }
            }

            #endregion
            return endPosition;
           
        }

        #region Q Logic Extensions
        public static AIHeroClient GetClosestEnemy(Vector3 from)
        {
            if (Orbwalker.LastTarget is AIHeroClient)
            {
                var owAI = TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(), DamageType.Physical);
                if (owAI.IsValidTarget(Variables._Player.GetAutoAttackRange(null) + 120f, true, from))
                {
                    return owAI;
                }
            }

            return null;
        }

        public static List<Vector3> GetRotatedQPositions()
        {
            const int currentStep = 30;
            // var direction = Variables._Player.Direction.To2D().Perpendicular();
            var direction = (Game.CursorPos - Variables._Player.ServerPosition).Normalized().To2D();

            var list = new List<Vector3>();
            for (var i = -70; i <= 70; i += currentStep)
            {
                var angleRad = Geometry.DegreeToRadian(i);
                var rotatedPosition = Variables._Player.Position.To2D() + (300f * direction.Rotated(angleRad));
                list.Add(rotatedPosition.To3D());
            }
            return list;
        }

        private static List<Vector2> GetEnemyPoints(bool dynamic = true)
        {
            var staticRange = 360f;
            var polygonsList = EnemiesClose.Select(enemy => new Geometry.Polygon.Circle(enemy.ServerPosition.To2D(), (dynamic ? (enemy.IsMelee ? enemy.AttackRange * 1.5f : enemy.AttackRange) : staticRange) + enemy.BoundingRadius + 20)).ToList();
            var pathList = Geometry.ClipPolygons(polygonsList);
            var pointList = pathList.SelectMany(path => path, (path, point) => new Vector2(point.X, point.Y)).Where(currentPoint => !currentPoint.IsWall()).ToList();
            return pointList;
        }

        public static IEnumerable<Obj_AI_Base> EnemiesClose
        {
            get
            {
                return
                    EntityManager.Heroes.Enemies.Where(
                        m =>
                            m.Distance(Variables._Player, true) <= Math.Pow(1000, 2) && m.IsValidTarget(1500) &&
                            m.CountEnemiesInRange(m.IsMelee ? m.AttackRange * 1.5f : m.AttackRange + 20 * 1.5f) > 0);
            }
        }
        public static bool IsSafe(this Vector3 position)
        {
            return position.To2D().IsSafeEx()
                   && position.IsNotIntoEnemies()
                   && EntityManager.Heroes.Enemies.All(m => m.Distance(position) > 350f)
                   &&
                   (!Variables.UnderEnemyTower((Vector2)position) ||
                    (Variables.UnderEnemyTower((Vector2)Variables._Player.ServerPosition) &&
                     Variables.UnderEnemyTower((Vector2)position) && Variables._Player.HealthPercent > 10));
            //Either it is not under turret or both the player and the position are under turret already and the health percent is greater than 10.
        }

        public static bool IsSafeEx(this Vector2 Position)
        {
            if (Variables.UnderEnemyTower((Vector2)Position) &&
                !Variables.UnderEnemyTower((Vector2)Variables._Player.ServerPosition))
            {
                return false;
            }
            var range = 1000f;
            var lowHealthAllies =
                EntityManager.Heroes.Allies.Where(a => a.IsValidTarget(range, false) && a.HealthPercent < 10 && !a.IsMe);
            var lowHealthEnemies =
                EntityManager.Heroes.Allies.Where(a => a.IsValidTarget(range) && a.HealthPercent < 10);
            var enemies = Variables._Player.CountEnemiesInRange(range);
            var allies = Variables._Player.CountAlliesInRange(range);
            var enemyTurrets = Turrets.EnemyTurrets.Where(m => m.IsValidTarget(975f));
            var allyTurrets = Turrets.AllyTurrets.Where(m => m.IsValidTarget(975f, false));

            return (allies - lowHealthAllies.Count() + allyTurrets.Count() * 2 + 1 >=
                    enemies - lowHealthEnemies.Count() +
                    (!Variables.UnderEnemyTower((Vector2)Variables._Player.ServerPosition) ? enemyTurrets.Count() * 2 : 0));
        }

        public static bool IsNotIntoEnemies(this Vector3 position)
        {

            var enemyPoints = GetEnemyPoints();
            if (enemyPoints.Contains(position.To2D()) &&
                !enemyPoints.Contains(Variables._Player.ServerPosition.To2D()))
            {
                return false;
            }

            var closeEnemies =
                EntityManager.Heroes.Enemies.FindAll(
                    en =>
                        en.IsValidTarget(1500f) &&
                        !(en.Distance(Variables._Player.ServerPosition) < en.AttackRange + 65f));
            if (!closeEnemies.All(enemy => position.CountEnemiesInRange(enemy.AttackRange) <= 1))
            {
                return false;
            }

            return true;
        }

        public static float GetAvgDistance(Vector3 from)
        {
            var numberOfEnemies = from.CountEnemiesInRange(1200f);
            if (numberOfEnemies != 0)
            {
                var enemies = EntityManager.Heroes.Enemies.Where(en => en.IsValidTarget(1200f, true, from)
                                                                       &&
                                                                       en.Health >
                                                                       Variables._Player.GetAutoAttackDamage(en) * 3 +
                                                                       Variables._Player.GetSpellDamage(en, SpellSlot.W) +
                                                                       Variables._Player.GetSpellDamage(en, SpellSlot.Q))
                    ;
                var enemiesEx = EntityManager.Heroes.Enemies.Where(en => en.IsValidTarget(1200f, true, from));
                var LHEnemies = enemiesEx.Count() - enemies.Count();

                var totalDistance = (LHEnemies > 1 && enemiesEx.Count() > 2)
                    ? enemiesEx.Sum(en => en.Distance(Variables._Player.ServerPosition))
                    : enemies.Sum(en => en.Distance(Variables._Player.ServerPosition));

                return totalDistance / numberOfEnemies;
            }
            return -1;
        }
    }
    #endregion

}

    

