using System;
using EloBuddy;
using EloBuddy.SDK.Events;

namespace AddonTemplate
{
    public static class Program
    {
        public const string ChampName = "Ezreal";
        public static int SkinBase;

        public static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != ChampName)
            {
                Chat.Print(Player.Instance.ChampionName);
                return;
            }
            SkinBase = Player.Instance.SkinId;
            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();
           
            Drawing.OnDraw += GameEvent.OnDraw;
            Config.Modes.Draw._useHax.OnValueChange += GameEvent.UseHax_OnValueChanged;
            Config.Modes.Draw._skinhax.OnValueChange += GameEvent.SkinHax_OnValueChanged;
            Config.Modes.Misc._SelfW.OnValueChange += GameEvent.SelfW_OnValueChanged;
            if (Config.Modes.Misc.EGapClos)
                Gapcloser.OnGapcloser += GameEvent.Gapcloser_OnGapCloser;
            Obj_AI_Base.OnBasicAttack += GameEvent.ObjTurret_OnTurretDamage;
        }
    }
}
