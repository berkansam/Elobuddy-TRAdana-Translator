using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;

namespace Mario_sGangplank
{
    internal class EventsManager
    {
        public static bool CanPreAttack { get; private set; }
        public static bool CanPostAttack { get; private set; }

        public static void InitEventManagers()
        {
            Orbwalker.OnPreAttack += Orbwalker_OnPreAttack;
            Orbwalker.OnPostAttack += Orbwalker_OnPostAttack;
            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Base.OnBuffGain += Obj_AI_Base_OnBuffGain;
            Orbwalker.OnUnkillableMinion += Orbwalker_OnUnkillableMinion;
        }

        private static void Orbwalker_OnUnkillableMinion(Obj_AI_Base target, Orbwalker.UnkillableMinionArgs args)
        {
            var dmg = Prediction.Health.GetPrediction(target, Spells.Q.CastDelay) - 30 <= target.Health;
            var count = EntityManager.MinionsAndMonsters.GetLaneMinions().Count(m => m.IsEnemy && m.IsInRange(target, 600) && dmg);
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit) && Helpers.GetCheckBoxValue(Helpers.MenuTypes.LastHit, "qLast") && Spells.Q.IsReady() && dmg &&
                target.Health <= Player.Instance.GetAutoAttackDamage(target) && count >= 2)
            {
                Spells.Q.Cast(target);
            }
        }

        private static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (!sender.IsMe || !Spells.W.IsReady())return;


            if (args.Buff.IsStunOrSuppressed && Helpers.GetCheckBoxValue(Helpers.MenuTypes.Settings, "wBuffStun"))
            {
                Spells.W.Cast();
            }

            if (args.Buff.IsSlow && Helpers.GetCheckBoxValue(Helpers.MenuTypes.Settings, "wBuffSlow"))
            {
                Spells.W.Cast();
            }

            if (args.Buff.IsBlind && Helpers.GetCheckBoxValue(Helpers.MenuTypes.Settings, "wBuffBlind"))
            {
                Spells.W.Cast();
            }

            if (args.Buff.IsSuppression && Helpers.GetCheckBoxValue(Helpers.MenuTypes.Settings, "wBuffSupression"))
            {
                Spells.W.Cast();
            }

            if (args.Buff.IsRoot && Helpers.GetCheckBoxValue(Helpers.MenuTypes.Settings, "wBuffSnare"))
            {
                Spells.W.Cast();
            }

        }

        //Dont need to change
        private static void Orbwalker_OnPreAttack(AttackableUnit target, Orbwalker.PreAttackArgs args)
        {
            CanPostAttack = false;
            CanPreAttack = true;
            Core.DelayAction(() => CanPreAttack = false, 60);
        }

        private static void Orbwalker_OnPostAttack(AttackableUnit target, System.EventArgs args)
        {
            CanPreAttack = false;
            CanPostAttack = true;
            Core.DelayAction(() => CanPostAttack = false, 60);
        }

        private static void Drawing_OnDraw(System.EventArgs args)
        {
            if (Helpers.GetCheckBoxValue(Helpers.MenuTypes.Drawings, "barrelDraw"))
            {
                foreach (var barrel in Barrrels.GetBarrels())
                {
                    Circle.Draw(barrel.Health <= 1 ? SharpDX.Color.YellowGreen : SharpDX.Color.DarkRed, 350, 3f, barrel);
                }
            }

            var ready = Helpers.GetCheckBoxValue(Helpers.MenuTypes.Drawings, "readyDraw");

            if (Helpers.GetCheckBoxValue(Helpers.MenuTypes.Drawings, "qDraw") && (ready ? Spells.Q.IsReady() : Spells.Q.IsLearned))
            {
                Circle.Draw(SharpDX.Color.Red, Spells.Q.Range, 1f, Player.Instance);
            }

            if (Helpers.GetCheckBoxValue(Helpers.MenuTypes.Drawings, "wDraw") && (ready ? Spells.W.IsReady() : Spells.W.IsLearned))
            {
                Circle.Draw(SharpDX.Color.Blue, Spells.W.Range, 1f, Player.Instance);
            }

            if (Helpers.GetCheckBoxValue(Helpers.MenuTypes.Drawings, "eDraw") && (ready ? Spells.E.IsReady() : Spells.E.IsLearned))
            {
                Circle.Draw(SharpDX.Color.Purple, Spells.E.Range, 1f, Player.Instance);
            }
        }
    }
}
