using EloBuddy;

namespace Mario_sGangplank
{
    internal class Functions : Helpers
    {
        public static void castW()
        {
            if (Player.HasBuffOfType(BuffType.Stun) && GetCheckBoxValue(MenuTypes.Settings, "wBuffStun"))
            {
                W.Cast();
            }

            if (Player.HasBuffOfType(BuffType.Slow) && GetCheckBoxValue(MenuTypes.Settings, "wBuffSlow"))
            {
                W.Cast();
            }

            if (Player.HasBuffOfType(BuffType.Blind) && GetCheckBoxValue(MenuTypes.Settings, "wBuffBlind"))
            {
                W.Cast();
            }

            if (Player.HasBuffOfType(BuffType.Suppression) && GetCheckBoxValue(MenuTypes.Settings, "wBuffSupression"))
            {
                W.Cast();
            }

            if (Player.HasBuffOfType(BuffType.Snare) && GetCheckBoxValue(MenuTypes.Settings, "wBuffSnare"))
            {
                W.Cast();
            }

            if (Player.HasBuffOfType(BuffType.Taunt) && GetCheckBoxValue(MenuTypes.Settings, "wBuffTaunt"))
            {
                W.Cast();
            }
        }
    }
}
