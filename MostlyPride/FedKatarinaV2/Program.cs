﻿using System;
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

namespace FedKatarinaV2
{
    internal class Program
    {
                //Spells
        public static Spell.Targeted Q;
        public static Spell.Active W;
        public static Spell.Targeted E;
        public static Spell.Active R;
        public static Menu menu, DrawingMenu, KillStealMenu;

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
            if (Player.Instance.Hero != Champion.Katarina) return;

            menu = MainMenu.AddMenu("FedKatarinaV2", "FedSeries");
            menu.AddGroupLabel("Fed KatarinaV2");
            menu.AddLabel("Version: " + "1.0.0.0");
            menu.AddSeparator();
            menu.AddLabel("MostlyPride");
            menu.AddSeparator();
            menu.AddLabel("+Rep If you use this :)");
            menu.AddLabel("Çeviri TRAdana");

            DrawingMenu = menu.AddSubMenu("Gösterge", "FedSeriesDrawings");
            DrawingMenu.AddGroupLabel("Gösterge Ayarları");
            DrawingMenu.Add("dQ", new CheckBox("Göster Q", true));
            DrawingMenu.Add("dW", new CheckBox("Göster W", true));
            DrawingMenu.Add("dE", new CheckBox("Göster E", true));
            DrawingMenu.Add("dR", new CheckBox("Göster R", true));

            KillStealMenu = menu.AddSubMenu("Kill Çalma", "FedSeriesKillSteal");
            KillStealMenu.AddGroupLabel("Kill Çalma Ayarları");
            KillStealMenu.Add("kQ", new CheckBox("Kullan Q", true));
            KillStealMenu.Add("kW", new CheckBox("Kullan W", true));
            KillStealMenu.Add("kE", new CheckBox("Kullan E", true));

            Q = new Spell.Targeted(SpellSlot.Q, 675);

            W = new Spell.Active(SpellSlot.W, 375);

            E = new Spell.Targeted(SpellSlot.E, 700);

            R = new Spell.Active(SpellSlot.R, 550);


            Drawing.OnDraw += Drawing_OnDraw;
            //SupaKS.Init();
            StateManager.Init();
            WardJumper.Init();
            Game.OnTick += Game_OnTick;

            Chat.Print("FedKatarinaV2 Yuklendi!tradana iyi oyunlar diler", System.Drawing.Color.LightBlue);
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
                        { W.Cast(enemy); }
                        if (KillStealMenu["kE"].Cast<CheckBox>().CurrentValue && (_Player.GetSpellDamage(enemy, E.Slot) >= enemy.Health) && E.IsInRange(enemy) && E.IsReady())
                        { E.Cast(enemy); }
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
            if (DrawingMenu["dW"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(W.IsReady() ? Color.Green : Color.Red, W.Range, Player.Instance.Position);
            }
            if (DrawingMenu["dE"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(E.IsReady() ? Color.Green : Color.Red, E.Range, Player.Instance.Position);
            }
            if (DrawingMenu["dR"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(R.IsReady() ? Color.Green : Color.Red, R.Range, Player.Instance.Position);
            }
        }
    }
}