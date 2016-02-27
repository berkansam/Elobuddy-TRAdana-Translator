using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace KA_Lux
{
    public static class SpellManager
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Skillshot R { get; private set; }

        static SpellManager()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1175, SkillShotType.Linear, 250, 1200, 65)
            {
                AllowedCollisionCount = 1
            };
            W = new Spell.Skillshot(SpellSlot.W, 1075, SkillShotType.Linear, 0, 1400, 85)
            {
                AllowedCollisionCount = int.MaxValue 
            };
            E = new Spell.Skillshot(SpellSlot.E, 1050, SkillShotType.Circular, 250, 1300, 330)
            {
                AllowedCollisionCount = int.MaxValue 
            };
            R = new Spell.Skillshot(SpellSlot.R, 3200, SkillShotType.Circular, 500, int.MaxValue, 160)
            {
                AllowedCollisionCount = int.MaxValue 
            };
        }

        public static void Initialize()
        {
        }
    }
}
