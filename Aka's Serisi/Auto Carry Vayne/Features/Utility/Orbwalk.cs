
using EloBuddy;
using EloBuddy.SDK;


namespace Auto_Carry_Vayne.Features.Utility
{
    class Orbwalk
    {
        public static void Customorbwalker()
        {
            var target = TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(),
DamageType.Physical);

            if (Variables.stopmove && Game.Time * 1000 > Variables.lastaaclick + ObjectManager.Player.AttackCastDelay * 1000 + 25f)
            {
                Variables.stopmove = false;
            }

            if (!Variables.stopmove && Game.Time * 1000 > Variables.lastaa + ObjectManager.Player.AttackCastDelay * 1000 - Game.Ping / 2.15 + ObjectManager.Player.AttackSpeedMod * 15.2 && Game.Time * 1000 > Variables.lastmove + 150f)
            {
                Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                Variables.lastmove = Game.Time * 1000;
            }

            if (target != null)
            {
                if (Game.Time * 1000 > Variables.lastaa + ObjectManager.Player.AttackDelay * 1000 - Game.Ping * 2.15)
                {
                    Variables.stopmove = true;
                    Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                }
                Botrk(target);
            }
        }

        public static
    void EloBuddyOrbDisabler()

        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                if (Orbwalker.DisableAttacking)
                {
                    Orbwalker.DisableAttacking = false;
                }
                if (Orbwalker.DisableMovement)
                {
                    Orbwalker.DisableMovement = false;
                }
            }
            else
            {
                if (!Orbwalker.DisableAttacking)
                {
                    Orbwalker.DisableAttacking = true;
                }
                if (!Orbwalker.DisableMovement)
                {
                    Orbwalker.DisableMovement = true;
                }
            }
        }

        public static void Botrk(Obj_AI_Base unit)
        {
            if (AfterAttack && (unit.Distance(ObjectManager.Player) > 500f || (ObjectManager.Player.Health / ObjectManager.Player.MaxHealth) * 100 <= 95))
            {
                if (Item.HasItem(3144) && Item.CanUseItem(3144))
                {
                    Item.UseItem(3144, unit);
                }
                if (Item.HasItem(3153) && Item.CanUseItem(3153))
                {
                    Item.UseItem(3153, unit);
                }
            }
        }

        public static bool AfterAttack
        {
            get
            {
                if (Game.Time * 1000 < Variables.lastaa + ObjectManager.Player.AttackDelay * 1000 - ObjectManager.Player.AttackDelay * 1000 / 2.35 && Game.Time * 1000 > Variables.lastaa + ObjectManager.Player.AttackCastDelay * 1000 + 25f)
                {
                    return true;
                }
                return false;
            }
        }

        public static void AutoAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe)
            {
                Variables.stopmove = false;
                Variables.lastaa = Game.Time * 1000;
            }
        }

        public static void IssueOrder(Obj_AI_Base sender, PlayerIssueOrderEventArgs args)
        {
            var target = TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(),
DamageType.Physical);

            if (sender.IsMe && args.Order.HasFlag(GameObjectOrder.AttackUnit))
            {
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) && target == null)
                {
                    Variables.stopmove = false;
                }
                Variables.lastaaclick = Game.Time * 1000;
            }

            if (sender.IsMe
                && (args.Order == GameObjectOrder.AttackUnit || args.Order == GameObjectOrder.AttackTo)
                &&
                (Manager.MenuManager.RNoAA &&
                 Variables._Player.CountEnemiesInRange(1000f) >
                 Manager.MenuManager.RNoAASlider)
                && Variables.UltActive() || Variables._Player.HasBuffOfType(BuffType.Invisibility)
                && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                args.Process = false;
            }
        }

    }
}
