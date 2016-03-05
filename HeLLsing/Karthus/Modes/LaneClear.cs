using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace Karthus.Modes
{
    // ReSharper disable ConvertIfStatementToReturnStatement
    public sealed class LaneClear : ModeBase
    {
        private Dictionary<SpellSlot, CheckBox> SpellUsage { get; set; }
        private Slider ManaUsage { get; set; }

        public LaneClear(Karthus instance) : base(instance)
        {
            // Initialize properties
            SpellUsage = new Dictionary<SpellSlot, CheckBox>();

            // Setup menu
            Menu.AddGroupLabel("Büyü Kullanımı");
            SpellUsage[SpellSlot.Q] = Menu.Add("Q", new CheckBox("Kullan Q"));
            SpellUsage[SpellSlot.E] = Menu.Add("E", new CheckBox("Kullan E"));

            Menu.AddSeparator();
            Menu.AddGroupLabel("Mana Kullanımı");
            ManaUsage = Menu.Add("mana", new Slider("Sadece manam şundan fazla %", 50, 0, 99));
        }

        public override bool ShouldBeExecuted(Orbwalker.ActiveModes activeModes)
        {
            return activeModes.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override bool Execute()
        {
            // Check for mana
            if (ManaUsage.CurrentValue < Player.Instance.ManaPercent)
            {
                if (Instance.SpellHandler.IsDefileActive())
                {
                    return Instance.SpellHandler.E.Cast();
                }
                return false;
            }

            // Cast active spells
            return SpellUsage.Keys.Any(slot => SpellUsage[slot].CurrentValue && Player.GetSpell(slot).IsReady && Instance.SpellHandler.CastOnBestTarget(slot));
        }
    }
}
