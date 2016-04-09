using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using YasuoBuddy.EvadePlus;

namespace YasuoBuddy
{
    internal class Yasuo
    {
        public static Menu Menu, ComboMenu, HarassMenu, FarmMenu, FleeMenu, DrawMenu, MiscSettings;
        private static int _cleanUpTime;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Yasuo) return;

            Menu = MainMenu.AddMenu("YasuoBuddy", "yasuobuddyfluxy");

            ComboMenu = Menu.AddSubMenu("Combo", "yasuCombo");
            ComboMenu.AddGroupLabel("Combo Ayarları");
            ComboMenu.Add("combo.Q", new CheckBox("Kullan Q"));
            ComboMenu.Add("combo.E", new CheckBox("Kullan E"));
            ComboMenu.Add("combo.stack", new CheckBox("Stack yap Q"));
            ComboMenu.Add("combo.leftclickRape", new CheckBox("Sol tıkladıma tecavüz et :D"));
            ComboMenu.AddSeparator();
            ComboMenu.AddLabel("R Ayarları");
            ComboMenu.Add("combo.R", new CheckBox("Kullan R"));
            ComboMenu.Add("combo.RTarget", new CheckBox("Ryi sadece tıkladım hedefe kullan"));
            ComboMenu.Add("combo.RKillable", new CheckBox("Ölecek hedefe R"));
            ComboMenu.Add("combo.MinTargetsR", new Slider("R için en az düşman", 2, 1, 5));

            HarassMenu = Menu.AddSubMenu("Harass", "yasuHarass");
            HarassMenu.AddGroupLabel("Dürtme Ayarları");
            HarassMenu.Add("harass.Q", new CheckBox("Kullan Q"));
            HarassMenu.Add("harass.E", new CheckBox("Kullan E"));
            HarassMenu.Add("harass.stack", new CheckBox("Stack Q"));

            FarmMenu = Menu.AddSubMenu("Farming Settings", "yasuoFarm");
            FarmMenu.AddGroupLabel("Farming Ayarları");
            FarmMenu.AddLabel("Last Hit");
            FarmMenu.Add("LH.Q", new CheckBox("Kullan Q"));
            FarmMenu.Add("LH.E", new CheckBox("Kullan E"));
            FarmMenu.Add("LH.EUnderTower", new CheckBox("Kule altında E", false));

            FarmMenu.AddLabel("WaveClear");
            FarmMenu.Add("WC.Q", new CheckBox("Kullan Q"));
            FarmMenu.Add("WC.E", new CheckBox("Kullan E"));
            FarmMenu.Add("WC.EUnderTower", new CheckBox("Kule altında E", false));

            FarmMenu.AddLabel("Jungle");
            FarmMenu.Add("JNG.Q", new CheckBox("Kullan Q"));
            FarmMenu.Add("JNG.E", new CheckBox("Kullan E"));

            FleeMenu = Menu.AddSubMenu("Flee/Evade", "yasuoFlee");
            FleeMenu.AddGroupLabel("Flee Ayarları");
            FleeMenu.Add("Flee.E", new CheckBox("Kullan E"));
            FleeMenu.Add("Flee.stack", new CheckBox("Stack Q"));
            FleeMenu.AddGroupLabel("Evade Ayarları");
            FleeMenu.Add("Evade.E", new CheckBox("Kaçarken E"));
            FleeMenu.Add("Evade.W", new CheckBox("Kaçarken W"));
            FleeMenu.Add("Evade.WDelay", new Slider("İnsancıl gecikme(ms)", 0, 0, 1000));

            MiscSettings = Menu.AddSubMenu("Misc Settings");
            MiscSettings.AddGroupLabel("KillSteal Ayarları");
            MiscSettings.Add("KS.Q", new CheckBox("Kullan Q"));
            MiscSettings.Add("KS.E", new CheckBox("Kullan E"));
            MiscSettings.AddGroupLabel("Otomatik Q Ayarları");
            MiscSettings.Add("Auto.Q3", new CheckBox("Kullan Q3"));
            MiscSettings.Add("Auto.Active", new KeyBind("Otomatik Q Düşmana", false, KeyBind.BindTypes.PressToggle, 'M'));

            Program.Main(null);

            DrawMenu = Menu.AddSubMenu("Draw", "yasuoDraw");
            DrawMenu.AddGroupLabel("Gösterge");

            DrawMenu.Add("Draw.Q", new CheckBox("Göster Q", false));
            DrawMenu.AddColourItem("Draw.Q.Colour");
            DrawMenu.AddSeparator();

            DrawMenu.Add("Draw.E", new CheckBox("Göster E", false));
            DrawMenu.AddColourItem("Draw.E.Colour");
            DrawMenu.AddSeparator();

            DrawMenu.Add("Draw.R", new CheckBox("Göster R", false));
            DrawMenu.AddColourItem("Draw.R.Colour");
            DrawMenu.AddSeparator();

            DrawMenu.AddLabel("When Spell is Down Colour = ");
            DrawMenu.AddColourItem("Draw.Down", 7);
            
            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
            EEvader.Init();
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (DrawMenu["Draw.Q"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(
                    SpellManager.Q.IsReady() ? DrawMenu.GetColour("Draw.Q.Colour") : DrawMenu.GetColour("Draw.Down"),
                    SpellManager.Q.Range, Player.Instance.Position);
            }
            if (DrawMenu["Draw.R"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(
                    SpellManager.R.IsReady() ? DrawMenu.GetColour("Draw.R.Colour") : DrawMenu.GetColour("Draw.Down"),
                    SpellManager.R.Range, Player.Instance.Position);
            }
            if (DrawMenu["Draw.E"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(
                    SpellManager.E.IsReady() ? DrawMenu.GetColour("Draw.E.Colour") : DrawMenu.GetColour("Draw.Down"),
                    SpellManager.E.Range, Player.Instance.Position);
            }
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (_cleanUpTime < Environment.TickCount)
            {
                GC.Collect();
                _cleanUpTime = Environment.TickCount + 1000000;
            }
            StateManager.KillSteal();
            if (MiscSettings["Auto.Active"].Cast<KeyBind>().CurrentValue)
            {
                StateManager.AutoQ();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                StateManager.Flee();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                StateManager.Harass();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                StateManager.Combo();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            {
                StateManager.LastHit();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                StateManager.WaveClear();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                StateManager.Jungle();
            }
        }
    }
}