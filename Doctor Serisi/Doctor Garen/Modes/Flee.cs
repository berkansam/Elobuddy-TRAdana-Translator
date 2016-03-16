using EloBuddy.SDK;
using Settings = Garen.Config.ModesMenu.Flee;

namespace Garen.Modes
{
    public sealed class Flee : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on flee mode
            //return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee);
            return false;
        }

        public override void Execute()
        {
            // TODO: Add flee logic here
        }
    }
}
