using System;
using EloBuddy;
using SharpDX;
using EloBuddy.SDK;
using System.Linq;
using System.Collections.Generic;

namespace Auto_Carry_Vayne
{
    class Variables
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static int currentSkin = 0;

        public static bool bought = false;

        public static int ticks = 0;

        public static bool VayneUltiIsActive { get; set; }

        public static SpellSlot FlashSlot;

        public static float lastaa, lastaaclick;

        public static bool stopmove;

        public static float lastmove; //new humanizer for inbuilt orbwalk.

        public static int[] AbilitySequence;

        public static int QOff = 0, WOff = 0, EOff = 0, ROff = 0;

        public static bool UltActive()
        {
            return (Variables._Player.HasBuff("vaynetumblefade") && !UnderEnemyTower((Vector2)_Player.Position));
        }

        public static bool UnderEnemyTower(Vector2 pos)
        {
            return EntityManager.Turrets.Enemies.Where(a => a.Health > 0 && !a.IsDead).Any(a => a.Distance(pos) < 950);
        }

        public static IEnumerable<AIHeroClient> ValidTargets { get { return EntityManager.Heroes.Enemies.Where(enemy => enemy.Health > 5 && enemy.IsVisible); } }

        #region MenuOptions

        public static bool Combo = false;
        public static bool Harass = false;
        public static bool Condemn = false;
        public static bool LC = false;
        public static bool JC = false;
        public static bool Misc = false;
        public static bool Activator = false;
        public static bool Flee = false;
        public static bool Draw = false;

        #endregion MenuOptions


    }
}
