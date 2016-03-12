using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;

namespace Mario_sTemplate
{
    internal class EventsManager
    {
        public static bool CanPreAttack { get; private set; }
        public static bool CanPostAttack { get; private set; }

        public static void InitEventManagers()
        {
            Orbwalker.OnPreAttack += Orbwalker_OnPreAttack;
            Orbwalker.OnPostAttack += Orbwalker_OnPostAttack;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender == null || sender.IsAlly) return;

            if (Spells.W.IsReady() && sender.IsValidTarget(200) && Helpers.GetCheckBoxValue(Helpers.MenuTypes.Settings, "spellGapcloser"))
            {
                Spells.W.Cast();
            }
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            var hero = sender as AIHeroClient;
            if (hero == null || hero.IsAlly) return;

            if (Spells.R.IsReady() && hero.IsValidTarget(Spells.R.Range) && Helpers.GetCheckBoxValue(Helpers.MenuTypes.Settings, "spellInterrupt"))
            {
                Spells.R.Cast();
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
            var ready = Helpers.GetCheckBoxValue(Helpers.MenuTypes.Drawings, "readyDraw");

            if (Helpers.GetCheckBoxValue(Helpers.MenuTypes.Drawings, "qDraw") && (ready ? Spells.Q.IsReady() : Spells.Q.IsLearned))
            {
                Circle.Draw(SharpDX.Color.Red, Spells.Q.Range, 1f, Player.Instance);
            }

            if (Helpers.GetCheckBoxValue(Helpers.MenuTypes.Drawings, "eDraw") && (ready ? Spells.E.IsReady() : Spells.E.IsLearned))
            {
                Circle.Draw(SharpDX.Color.Purple, Spells.E.Range, 1f, Player.Instance);
            }

            if (Helpers.GetCheckBoxValue(Helpers.MenuTypes.Drawings, "rDraw") && (ready ? Spells.R.IsReady() : Spells.R.IsLearned))
            {
                Circle.Draw(SharpDX.Color.Orange, Spells.R.Range, 1f, Player.Instance);
            }
        }
    }
}
