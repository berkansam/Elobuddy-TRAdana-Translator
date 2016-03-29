using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace Auto_Carry_Vayne.Features.Modes
{
    class Combo
    {
        public static void Load()
        {
            var target = TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(),
    DamageType.Physical);

            UseQ();
            UseE();
            UseR();
            UseTrinket(target);
            if (Manager.MenuManager.CustomOrbwalk) Utility.Orbwalk.Customorbwalker();
        }

        public static void UseQ()
        {
            var target = TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(), DamageType.Physical);

            if (Utility.Orbwalk.AfterAttack && Manager.MenuManager.UseQ)
            {
                if (target == null) return;
                #region check for 2 w stacks
                if (Manager.MenuManager.UseQStacks && target.GetBuffCount("vaynesilvereddebuff") != 2)
                {
                    return;
                }
                #endregion
                var QPosition = Logic.MyQLogic.GetQPosition();
                Player.CastSpell(SpellSlot.Q, QPosition);
            }
        }

        public static void UseE()
        {
            var target = TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(), DamageType.Physical);

            if (Utility.Orbwalk.AfterAttack && Manager.MenuManager.UseE)
            {
                Logic.Condemn.condemn();
            }
        }

        public static void UseR()
        {
            if (Manager.MenuManager.UseR && Manager.SpellManager.R.IsReady())
            {
                if (Variables._Player.CountEnemiesInRange(1000) >= Manager.MenuManager.UseRSlider)
                {
                    Manager.SpellManager.R.Cast();
                }
            }
        }

        public static void UseTrinket(Obj_AI_Base target)
        {
            if (target == null)
            {
                return;
            }
            if (Variables._Player.Spellbook.GetSpell(SpellSlot.Trinket).IsReady &&
                Variables._Player.Spellbook.GetSpell(SpellSlot.Trinket).SData.Name.ToLower().Contains("totem"))
            {
                Core.DelayAction(delegate
                {
                    if (Manager.MenuManager.AutoTrinket)
                    {
                        var pos = Logic.Mechanics.GetFirstNonWallPos(Variables._Player.Position.To2D(), target.Position.To2D());
                        if (NavMesh.GetCollisionFlags(pos).HasFlag(CollisionFlags.Grass))
                        {
                            Manager.SpellManager.totem.Cast(pos.To3D());
                        }
                    }
                }, 200);
            }
        }
    }
}

