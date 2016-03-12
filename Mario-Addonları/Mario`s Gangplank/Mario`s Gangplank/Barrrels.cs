using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace Mario_sGangplank
{
    internal class Barrrels
    {
        public static void InitBarrrels()
        {
            GameObject.OnCreate += GameObject_OnCreate;
        }

        private static void GameObject_OnCreate(GameObject sender, System.EventArgs args)
        {
                Chat.Print(sender.Name);
        }

        public static Obj_AI_Base GetKillBarrelClosest()
        {
            var barrel =
                ObjectManager.Get<Obj_AI_Base>()
                    .OrderBy(b => b.Health).ThenBy(ba => ba.Distance(Player.Instance.Position))
                    .FirstOrDefault(
                        o =>
                            o.Name.ToLower().Equals("barrel") && o.Health <= 1 && o.Health != 0);
            return barrel;
        }

        public static Obj_AI_Base GetKillBarrelWithEemyInside()
        {
            var barrel =
                ObjectManager.Get<Obj_AI_Base>()
                    .OrderBy(b => b.Health)
                    .FirstOrDefault(
                        o =>
                            o.Name.ToLower().Equals("barrel") && o.Health <= 1 && o.Health != 0 && o.CountEnemiesInRange(390) >= 1);
            return barrel;
        }

        public static Obj_AI_Base GetBarrelWithEemyInside()
        {
            var barrel =
                ObjectManager.Get<Obj_AI_Base>()
                    .OrderBy(b => b.Health)
                    .FirstOrDefault(
                        o =>
                            o.Name.ToLower().Equals("barrel") && o.Health != 0 && o.CountEnemiesInRange(390) >= 1);
            return barrel;
        }

        public static Obj_AI_Base GetBarrel()
        {
            var barrel =
                ObjectManager.Get<Obj_AI_Base>()
                    .OrderBy(b => b.Health).ThenBy(b => b.Distance(Player.Instance))
                    .FirstOrDefault(o => o.Name.ToLower().Equals("barrel") && o.CountEnemiesInRange(390) > 1 && o.Health != 0);
            return barrel;
        }

        public static IEnumerable<Obj_AI_Base> GetBarrels()
        {
            var barrel =
                ObjectManager.Get<Obj_AI_Base>()
                    .Where(o => o.Name.ToLower().Equals("barrel") && o.Health != 0);
                
            return barrel;
        }
    }
}
