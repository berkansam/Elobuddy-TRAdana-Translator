using System;
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace AddonTemplate
{
    public static class Program
    {
        public const string ChampName = "Twitch";

        public static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != ChampName)
            {
                return;
            }

            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();

            DamageHelper.Initialize(DamageHelper.GetEDamage);

            Drawing.OnDraw += GameEvent.OnDraw;
        }

        private static void OnDraw(EventArgs args)
        {
            Circle.Draw(Color.Red, SpellManager.Q.Range, Player.Instance.Position);
        }
    }
}
