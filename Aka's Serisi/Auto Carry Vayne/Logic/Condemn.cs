using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using SharpDX;


namespace Auto_Carry_Vayne.Logic
{
    class Condemn
    {
        //my condemn logic so far
        public static void condemn()
        {
            foreach (var target in EntityManager.Heroes.Enemies.Where(h => h.IsValidTarget(Manager.SpellManager.E.Range)))
            {
                var pushDistance = Manager.MenuManager.CondemnPushDistance;
                var targetPosition = Manager.SpellManager.E2.GetPrediction(target).UnitPosition;
                var pushDirection = (targetPosition - ObjectManager.Player.ServerPosition).Normalized();
                float checkDistance = pushDistance / 40f;
                for (int i = 0; i < 40; i++)
                {
                    Vector3 finalPosition = targetPosition + (pushDirection * checkDistance * i);
                    var collFlags = NavMesh.GetCollisionFlags(finalPosition);
                    if (collFlags.HasFlag(CollisionFlags.Wall) || collFlags.HasFlag(CollisionFlags.Building)) //not sure about building, I think its turrets, nexus etc
                    {
                        Manager.SpellManager.E.Cast(target);
                    }
                }
            }

        }
    }
}


