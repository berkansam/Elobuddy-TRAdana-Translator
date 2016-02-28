﻿using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using OKTRAIO.Menu_Settings;
using MainMenu = OKTRAIO.Menu_Settings.MainMenu;

// ReSharper disable once RedundantDefaultMemberInitializer

namespace OKTRAIO.Utility
{
    public class AutoLantern : UtilityAddon
    {
        public AutoLantern(Menu menu) : base(menu)
        {
        }

        public override UtilityInfo GetUtilityInfo()
        {
            return new UtilityInfo(this, "Otomatik Fener", "autolantern", "iRaxe");
        }

        protected override void InitializeMenu()
        {
            if (EntityManager.Heroes.Allies.Any(a => a.ChampionName == "Thresh"))
            {
                Menu.AddGroupLabel("OKTR AIO - Otomatik Fener " + Player.Instance.ChampionName,
                    "autolantern.grouplabel.utilitymenu");
                Menu.AddCheckBox("autolantern.auto", "Otomatik Fener Kullan");
                Menu.AddSeparator();
                Menu.AddComboBox("autolantern.mode", "Grab Method", new List<string> {"Combo", "All Modes"}, 1);
                Menu.AddSeparator();
                Menu.AddSlider("autolantern.hp", "Maximium Can % olunca Kullan", 30);
                Menu.AddSlider("autolantern.dist", "Kullanılacak maksimum mesafe", 150, 0, 700);
            }
            else
            {
                Menu.AddLabel("Üzgünüm, Senin Thresh'in olmadığından - UTILITY Özellikleri Devredışı");
            }
        }

        public override void Initialize()
        {
            if (EntityManager.Heroes.Allies.Any(a => a.ChampionName != "Thresh")) return;
            if (MainMenu.Menu["useonupdate"].Cast<CheckBox>().CurrentValue)
            {
                Game.OnUpdate += OnTick;
            }
            else
            {
                Game.OnTick += OnTick;
            }
        }

        private static void OnTick(EventArgs args)
        {
            var lantern =
                ObjectManager.Get<Obj_AI_Base>()
                    .FirstOrDefault(l => l.Name.Equals("ThreshLantern")
                                         && l.IsValid && l.IsAlly &&
                                         Player.Instance.Distance(l) <= Value.Get("lant.dist"));

            if (lantern == null) return;

            if (Value.Use("autolantern.auto") && Player.Instance.HealthPercent <= Value.Get("autolantern.hp"))
            {
                if (Value.ComboGet("autolantern.mode") == 1)
                {
                    Player.UseObject(lantern);
                }
                else if (Value.ComboGet("autolantern.mode") == 0 && Value.Mode(Orbwalker.ActiveModes.Combo))
                {
                    Player.UseObject(lantern);
                }
            }
        }
    }
}