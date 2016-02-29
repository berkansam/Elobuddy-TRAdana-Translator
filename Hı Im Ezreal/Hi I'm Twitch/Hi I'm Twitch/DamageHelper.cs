using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using Color = System.Drawing.Color;

namespace AddonTemplate
{
    public static class DamageHelper
    {
        private const int BarWidth = 104;
        private const int LineThickness = 9;
        public delegate float DamageToUnitDelegate(AIHeroClient hero);

        private static DamageToUnitDelegate DamageToUnit { get; set; }

        private static readonly Vector2 BarOffset = new Vector2(2.5f, -6);

        private static Color _drawingColor;
        public static Color DrawingColor
        {
            get { return _drawingColor; }
            set { _drawingColor = Color.FromArgb(170, value); }
        }
        public static void Initialize(DamageToUnitDelegate damageToUnit)
        {
            // Apply needed field delegate for damage calculation
            DamageToUnit = damageToUnit;
            DrawingColor = Color.Green;

            // Register event handlers
            Drawing.OnEndScene += OnEndScene;
        }

        public static float TotalShieldHealth(this Obj_AI_Base target)
        {
            return target.Health + target.AllShield + target.AttackShield + target.MagicShield;
        }

        private static void OnEndScene(EventArgs args)
        {
            if (Config.Modes.Draw.DamageIndicator)
            {
                foreach (var unit in EntityManager.Heroes.Enemies.Where(u => u.IsValidTarget() && u.IsHPBarRendered))
                {
                    // Get damage to unit
                    var damage = DamageToUnit(unit);

                    // Continue on 0 damage
                    if (damage <= 0)
                    {
                        continue;
                    }

                        // Get remaining HP after damage applied in percent and the current percent of health
                        var damagePercentage = ((unit.TotalShieldHealth() - damage) > 0 ? (unit.TotalShieldHealth() - damage) : 0) /
                                               (unit.MaxHealth + unit.AllShield + unit.AttackShield + unit.MagicShield);
                        var currentHealthPercentage = unit.TotalShieldHealth() / (unit.MaxHealth + unit.AllShield + unit.AttackShield + unit.MagicShield);

                        // Calculate start and end point of the bar indicator
                        var startPoint = new Vector2((int)(unit.HPBarPosition.X + BarOffset.X + damagePercentage * BarWidth), (int)(unit.HPBarPosition.Y + BarOffset.Y));
                        var endPoint = new Vector2((int)(unit.HPBarPosition.X + BarOffset.X + currentHealthPercentage * BarWidth) + 1, (int)(unit.HPBarPosition.Y + BarOffset.Y));

                        // Draw the line
                        Drawing.DrawLine(startPoint, endPoint, LineThickness, DrawingColor);

                }
            }
        }

        public static float GetEDamage(AIHeroClient target)
        {
            var buff = target.HasBuff("twitchdeadlyvenom");
            var bc = target.GetBuffCount("twitchdeadlyvenom");
            if (!buff || !SpellManager.E.IsLearned) return 0f;
            var total = Player.Instance.CalculateDamageOnUnit(target, DamageType.True, (float)
                (new[] { 15, 20, 25, 30, 35 }[SpellManager.E.Level - 1] * bc +
                0.2 * Player.Instance.FlatMagicDamageMod +
                0.25 * Player.Instance.FlatPhysicalDamageMod +
                new[] { 20, 35, 50, 65, 80 }[SpellManager.E.Level - 1]));
            Chat.Print("Total : " + total);
            var damage = DamageLibrary.GetSpellDamage(Player.Instance, target, SpellSlot.E);
            Chat.Print("Auto : " + damage);
            return total;
        }
      
    }
}
