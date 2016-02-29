using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using Settings = AddonTemplate.Config.Modes.Harass;

namespace AddonTemplate.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        { 
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            if (Settings.UseQ && Player.Instance.ManaPercent > Settings.ManaQ && Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Q.Range - 50, DamageType.Physical);
                if (target != null)
                {
                    var predQ = Q.GetPrediction(target);
                    if (predQ.HitChance >= SpellManager.PredQ() &&
                        Config.Modes.MenuHarass[target.ChampionName + "harass"].Cast<CheckBox>().CurrentValue)
                    {
                        Q.Cast(predQ.CastPosition);
                    }
                }
            }
            if (Settings.UseW && Player.Instance.ManaPercent > Settings.ManaW && W.IsReady())
            {
                var target = TargetSelector.GetTarget(W.Range - 50, DamageType.Physical);
                if (target != null)
                {
                    var predW = W.GetPrediction(target);
                    if (predW.HitChance >= SpellManager.PredW() &&
                        Config.Modes.MenuHarass[target.ChampionName + "harass"].Cast<CheckBox>().CurrentValue)
                    {
                        W.Cast(predW.CastPosition);
                    }
                }
            }
        }
    }
}
