using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace FedDrMundoV2
{
    internal class StateManager
    {
        public static Menu ComboMenu, HarassMenu, FarmMenu, JungleMenu, MiscMenu;
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Init()
        {
            ComboMenu = Program.menu.AddSubMenu("Combo", "FedSeriesCombo");
            ComboMenu.AddGroupLabel("Kombo Ayarları");

            ComboMenu.AddLabel("Space Bas ve Eğlen :3");
            ComboMenu.Add("cQ", new CheckBox("Kullan Q", true));
            ComboMenu.Add("cW", new CheckBox("Kullan W", true));
            ComboMenu.Add("cE", new CheckBox("Kullan E", true));

            HarassMenu = Program.menu.AddSubMenu("Harass Settings");
            HarassMenu.AddGroupLabel("Dürtme Ayarları");
            HarassMenu.Add("life", new Slider("Dürtme için en az can", 20, 0, 100));
            HarassMenu.Add("hQ", new CheckBox("Kullan Q", true));
            HarassMenu.Add("autoharass", new CheckBox("Otomatik Q fırlat", false));

            FarmMenu = Program.menu.AddSubMenu("Farming Settings");
            FarmMenu.AddGroupLabel("LaneTemizleme Ayarları");
            FarmMenu.Add("fQ", new CheckBox("Q Kullan"));
            FarmMenu.Add("fW", new CheckBox("W Kullan"));
            FarmMenu.Add("fE", new CheckBox("E Kullan"));
            FarmMenu.AddGroupLabel("SonVuruş Ayarları");
            FarmMenu.Add("lQ", new CheckBox("Q Kullan"));
            FarmMenu.Add("lW", new CheckBox("W Kullan"));
            FarmMenu.Add("lE", new CheckBox("E Kullan"));


            JungleMenu = Program.menu.AddSubMenu("Jungle Settings");
            JungleMenu.AddGroupLabel("OrmanTemizleme Ayarları");
            JungleMenu.Add("jQ", new CheckBox("Q Kullan"));
            JungleMenu.Add("jW", new CheckBox("W Kullan"));
            JungleMenu.Add("jE", new CheckBox("E Kullan"));

            MiscMenu = Program.menu.AddSubMenu("Ek Menu", "FSMisc");
            MiscMenu.Add("lifesave", new CheckBox("Ulti kullan güvenli şekilde?"));
            MiscMenu.Add("percenthp", new Slider("Ulti boz ve kaç canım şuna inince", 30, 100, 0));

            Game.OnTick += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            LifeSave();
            if (Player.HasBuff("BurningAgony"))
            {
                Program.WActive = true;
            }
            else
            {
                Program.WActive = false;
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) Combo();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass)) Harass();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit)) LastHit();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear)) Jungle();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)) WaveClear();
            if (HarassMenu["autoharass"].Cast<CheckBox>().CurrentValue)
            {
                AutoHarass();
            }
        }
        private static void LifeSave()
        {
            var RLife = MiscMenu["percenthp"].Cast<Slider>().CurrentValue;
            if (MiscMenu["lifesave"].Cast<CheckBox>().CurrentValue)
            {
                if (Program.R.IsReady() && _Player.HealthPercent < RLife)
                {
                    Program.R.Cast();
                }
            }
        }

        public static void LastHit()
        {
            var minions = EntityManager.MinionsAndMonsters.EnemyMinions.Where(a => a.Distance(Player.Instance) < 1400).OrderBy(a => a.Health);
            var minion =
                minions.FirstOrDefault(
                    a => Extended.BuffedEnemy != null && a.NetworkId == Extended.BuffedEnemy.NetworkId) ??
                minions.FirstOrDefault();
            if (minion == null || !minion.IsValidTarget()) return;
            if (FarmMenu["lQ"].Cast<CheckBox>().CurrentValue && (Damage.QDamage(minion) > minion.Health) && Program.Q.IsReady() && Program.Q.IsInRange(minion))
            {
                Program.Q.Cast(minion);
            }
            if (FarmMenu["lW"].Cast<CheckBox>().CurrentValue && (Damage.WDamage(minion) > minion.Health) && Program.W.IsReady() && Program.W.IsInRange(minion))
            {
                Program.W.Cast();
            }
            if (FarmMenu["lE"].Cast<CheckBox>().CurrentValue && (Damage.EDamage(minion) > minion.Health) && Program.E.IsReady() && Program.E.IsInRange(minion))
            {
                Program.E.Cast();
            }
        }

        public static void WaveClear()
        {
            var minions = EntityManager.MinionsAndMonsters.EnemyMinions.Where(a => a.Distance(Player.Instance) < 1400).OrderBy(a => a.Health);
            var minion = (Extended.BuffedEnemy != null && Extended.BuffedEnemy.IsValidTarget(1400)) ? Extended.BuffedEnemy : minions.FirstOrDefault();
            if (minion == null) return;
            
            if (FarmMenu["fQ"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() && Program.Q.IsInRange(minion))
            {
                Program.Q.Cast(minion);
            }
            if (FarmMenu["fW"].Cast<CheckBox>().CurrentValue && Program.W.IsReady() && Program.W.IsInRange(minion))
            {
                Program.W.Cast();
            }
            if (FarmMenu["fE"].Cast<CheckBox>().CurrentValue && Program.E.IsReady() && Program.E.IsInRange(minion))
            {
                Program.E.Cast();
            }
        }

        public static void Jungle()
        {
            var source = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderByDescending(a => a.MaxHealth).FirstOrDefault(b => b.Distance(Player.Instance) < 1300);
            if (source == null || !source.IsValidTarget()) return;

            if (JungleMenu["jQ"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() && source.Distance(_Player) < Program.Q.Range)
            {
                Program.Q.Cast(source);
            }
            if (JungleMenu["jW"].Cast<CheckBox>().CurrentValue && Program.W.IsReady() && source.Distance(_Player) < Program.W.Range)
            {
                Program.W.Cast();
            }
            if (JungleMenu["jE"].Cast<CheckBox>().CurrentValue && Program.E.IsReady() && source.Distance(_Player) < Program.E.Range)
            {
                Program.E.Cast();
            }
        }

        public static void Combo()
        {
            var target = TargetSelector.GetTarget(Program.Q.Range, DamageType.Magical);

            if (target != null && ComboMenu["cQ"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() && _Player.Distance(target) <= Program.Q.Range)
            {
                var qPred = Program.Q.GetPrediction(target);
                if (qPred.HitChance >= HitChance.High)
                    Program.Q.Cast(qPred.CastPosition);
            }
            if (!Program.WActive && target != null && ComboMenu["cW"].Cast<CheckBox>().CurrentValue && Program.W.IsReady() && _Player.Distance(target) <= 300)
            {
                Program.W.Cast();
            }
            if (target != null && ComboMenu["cE"].Cast<CheckBox>().CurrentValue && Program.E.IsReady() && _Player.Distance(target) <= Program.Q.Range)
            {
                Program.E.Cast();
            }

        }

        public static void Harass()
        {
            var qTarget = TargetSelector.GetTarget(Program.Q.Range + Program.Q.Width, DamageType.Magical);

            var RLife = HarassMenu["life"].Cast<Slider>().CurrentValue;
            var LPercentR = _Player.Health * 100 / _Player.MaxHealth;

            if (qTarget != null && Program.Q.IsReady() && LPercentR >= RLife && _Player.Distance(qTarget) <= Program.Q.Range)
            {
                var qPred = Program.Q.GetPrediction(qTarget);
                if (qPred.HitChance >= HitChance.High)
                    Program.Q.Cast(qPred.CastPosition);
            }
        }

        static void AutoHarass()
        {
            var qTarget = TargetSelector.GetTarget(Program.Q.Range + Program.Q.Width, DamageType.Magical);

            var RLife = HarassMenu["life"].Cast<Slider>().CurrentValue;
            var LPercentR = _Player.Health * 100 / _Player.MaxHealth;

            if (qTarget != null && Program.Q.IsReady() && LPercentR >= RLife && _Player.Distance(qTarget) <= Program.Q.Range)
            {
                var qPred = Program.Q.GetPrediction(qTarget);
                if (qPred.HitChance >= HitChance.High)
                    Program.Q.Cast(qPred.CastPosition);
            }
        }
    }
}