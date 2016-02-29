using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using System.Linq;

namespace FedDrMundoV2
{
    internal class Program
    {
        //Spells
        public static Spell.Skillshot Q;
        public static Spell.Active W;
        public static Spell.Active E;
        public static Spell.Active R;
        public static Menu menu, DrawingMenu, KillStealMenu;
        public static bool WActive = false;

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        private static void Main(string[] args)
        {
            if (args != null)
            {
                try
                {
                    Loading.OnLoadingComplete += Load_OnLoad;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static void Load_OnLoad(EventArgs a)
        {
            if (Player.Instance.Hero != Champion.DrMundo) return;

            menu = MainMenu.AddMenu("FedDrMundoV2", "FedSeries");
            menu.AddGroupLabel("Fed DrMundoV2");
            menu.AddLabel("Version: " + "2.0.0.0");
            menu.AddSeparator();
            menu.AddLabel("MostlyPride");
            menu.AddSeparator();
            menu.AddLabel("+Rep If you use this :)");
            menu.AddLabel("Çeviri TRAdana");

            DrawingMenu = menu.AddSubMenu("Gösterge", "FedSeriesDrawings");
            DrawingMenu.AddGroupLabel("Gösterge Ayarları");
            DrawingMenu.Add("dQ", new CheckBox("Göster Q", true));

            KillStealMenu = menu.AddSubMenu("Kill Çalma", "FedSeriesKillSteal");
            KillStealMenu.AddGroupLabel("Kill Çalma Ayarları");
            KillStealMenu.Add("kQ", new CheckBox("Kullan Q", true));
            KillStealMenu.Add("kW", new CheckBox("Kullan W", true));
            KillStealMenu.Add("kE", new CheckBox("Kullan E", true));


            Q = new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear, 50, 2000, 60);
            W = new Spell.Active(SpellSlot.W, 162);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Active(SpellSlot.R);


            Drawing.OnDraw += Drawing_OnDraw;
            //SupaKS.Init();
            StateManager.Init();
            Game.OnTick += Game_OnTick;

            Chat.Print("FedDrMundoV2 Yuklendi!,tradana iyi oyunlar diler", System.Drawing.Color.LightBlue);
        }

        private static void Game_OnTick(EventArgs args)
        {
            KillSteal();
        }

        public static void KillSteal()
        {
              foreach (var enemy in EntityManager.Heroes.Enemies.Where(a => !a.IsDead && !a.IsZombie && a.Health > 0))
            {
                if (enemy.IsValidTarget(E.Range) && enemy.HealthPercent <= 40)
                {

                    if (_Player.GetSpellDamage(enemy, SpellSlot.Q) + _Player.GetSpellDamage(enemy, SpellSlot.W) + _Player.GetSpellDamage(enemy, SpellSlot.E) >= enemy.Health)
                    {
                        if (KillStealMenu["kQ"].Cast<CheckBox>().CurrentValue && (_Player.GetSpellDamage(enemy, Q.Slot) >= enemy.Health) && Q.IsInRange(enemy) && Q.IsReady())
                        { Q.Cast(enemy); }
                        if (KillStealMenu["kW"].Cast<CheckBox>().CurrentValue && (_Player.GetSpellDamage(enemy, W.Slot) >= enemy.Health) && W.IsInRange(enemy) && W.IsReady())
                        { W.Cast(); }
                        if (KillStealMenu["kE"].Cast<CheckBox>().CurrentValue && (_Player.GetSpellDamage(enemy, E.Slot) >= enemy.Health) && E.IsInRange(enemy) && E.IsReady())
                        { E.Cast(); }
                    }

                }
            }

        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (DrawingMenu["dQ"].Cast<CheckBox>().CurrentValue)
            {
               Circle.Draw(Q.IsReady() ? Color.Green : Color.Red, Q.Range, Player.Instance.Position); 
            }
        }
    }
}