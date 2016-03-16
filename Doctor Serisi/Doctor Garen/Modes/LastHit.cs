using EloBuddy;
using EloBuddy.SDK;
using Settings = Garen.Config.ModesMenu.LastHit;

namespace Garen.Modes
{
    public sealed class LastHit : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on lasthit mode
            //return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
            return false;
        }

        public override void Execute()
        {
           
        }
    }
}
