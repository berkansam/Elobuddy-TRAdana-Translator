﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
namespace KonoHa_Khazix
{
    using EloBuddy.SDK.Enumerations;
    using EloBuddy.SDK.Events;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;
    using EloBuddy.SDK.Rendering;

    using KonoHa_Khazix.Modes;

    using SharpDX;

    class Program
    {
        public static Menu menu, ComboMenu, HarrassMenu,DrawingMenu,DoubleJumpMenu,LastHitMenu;
        public static bool Jumping;
        public static bool evolQ=false, evolW=false, evolE=false, evolR=false;
        private static Spell.Targeted Q;
        private static Spell.Skillshot W, WE;
      private static Spell.Skillshot E;
        private static Spell.Active R;

        public static  Spell.Skillshot getE
        {
            get
            {
                return E;
            }
        }
        public static Spell.Skillshot getW
        {
            get
            {
                return W;
            }
        }
        public static Spell.Skillshot getWE
        {
            get
            {
                return WE;
            }
        }
        public static Spell.Targeted getQ
        {
            get
            {
                return Q;
            }
        }
        public static Spell.Active getR
        {
            get
            {
                return R;
            }
        }
        public static AIHeroClient myHero
        {
            get
            {
                return ObjectManager.Player;
            }
        }
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoad;
        }

        private static void OnLoad(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Khazix) return;
            Q = new Spell.Targeted(SpellSlot.Q,325);
            W = new Spell.Skillshot(SpellSlot.W, 1000, SkillShotType.Linear, 225,828,80);
            WE = new Spell.Skillshot(SpellSlot.W, 1000, SkillShotType.Linear, 225, 828, 80);
            E=new Spell.Skillshot(SpellSlot.E,600,SkillShotType.Circular,25,1000,100 );
            R = new Spell.Active(SpellSlot.R);
            var shop = ObjectManager.Get<Obj_Shop>().FirstOrDefault(o => o.IsAlly);
            if (shop != null)
            {
               JumpsHandler.bases = shop.Position;
            }
             loadMenu();
        Game.OnUpdate += OnGameUpdate;
        Game.OnUpdate += JumpsHandler.DoubleJumpLogic;
        Drawing.OnDraw += OnDraw;
        }

        private static void OnDraw(EventArgs args)
        {
            var qSpell = DrawingMenu["DQ"].Cast<CheckBox>().CurrentValue;
            var wSpell = DrawingMenu["DW"].Cast<CheckBox>().CurrentValue;
            var eSpell = DrawingMenu["DE"].Cast<CheckBox>().CurrentValue;
          if (qSpell) Circle.Draw(Color.BlueViolet, Q.Range,  Player.Instance.Position);
          if (wSpell) Circle.Draw(Color.BlueViolet, W.Range, Player.Instance.Position);
          if (eSpell) Circle.Draw(Color.BlueViolet, E.Range, Player.Instance.Position);
        }

        private static void OnGameUpdate(EventArgs args)
        {
            if (Player.Instance.IsDead) return;

            if (!evolQ || !evolW || !evolE) checkEvol();
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo)
            {
                Combo.Do();
            }
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Harass)
            {
                Harrass.Do();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            {
               LastHit.Do();
            }
        }

        private static void checkEvol()
        {
            if (!evolQ && Player.HasBuff("khazixqevo"))
            {
                Q = new Spell.Targeted(SpellSlot.Q, 375);
                evolQ = true;
            }
            if (!evolW && Player.HasBuff("khazixwevo"))
            {
                evolW = true;
                Combo.Wnorm = false;
                W = new Spell.Skillshot(SpellSlot.W, 1000, SkillShotType.Linear, 225, 828, 100);
            }
            if (!evolE && Player.HasBuff("khazixeevo"))
            {
                E = new Spell.Skillshot(SpellSlot.E,1000, SkillShotType.Circular, 25, 1000, 100);
               evolE = true;
            }
        }

        private static void loadMenu()
        {
            menu = MainMenu.AddMenu("KhaZix", "Khazix");
            ComboMenu = menu.AddSubMenu("Kombo", "Combo");
            ComboMenu.Add("QC", new CheckBox("Kullan Q", true));
            ComboMenu.Add("WC", new CheckBox("Kullan W", true));
            ComboMenu.Add("EC", new CheckBox("Kullan E", true));
            ComboMenu.Add("RC", new CheckBox("Kullan R", true));
            ComboMenu.AddSeparator(25);
            ComboMenu.Add("IC", new CheckBox("İtemleri Kullan", true));
            ComboMenu.Add("ECG", new CheckBox("Gapcloser için E", true));
            ComboMenu.Add("WCG", new CheckBox("W için E ile Gapclose", true));
            ComboMenu.Add("RCG", new CheckBox("Rden sonra uzun Gapclose", true));

            HarrassMenu = menu.AddSubMenu("Dürtme", "Harrass");
            HarrassMenu.Add("QH", new CheckBox("Kullan Q", true));
            HarrassMenu.Add("WH", new CheckBox("Kullan W", true));
            LastHitMenu = menu.AddSubMenu("SonVuruş", "LastHit");
            LastHitMenu.Add("QL", new CheckBox("Kullan Q", true));
            DrawingMenu = menu.AddSubMenu("Gösterge", "Drawing");
            DrawingMenu.Add("DQ", new CheckBox("Göster Q", true));
            DrawingMenu.Add("DW", new CheckBox("Göster W", true));
            DrawingMenu.Add("DE", new CheckBox("Göster E", true));
            DoubleJumpMenu = menu.AddSubMenu("Çift Zıplama", "DoubleJump");
            DoubleJumpMenu.Add("DE", new CheckBox("Aktif"));
        //    DoubleJumpMenu.Add("SliderD",new Slider("Delay between jumps",250, 250, 500));
       //     DoubleJumpMenu.Add("DQ", new CheckBox("Wait for Q instead of AutoAttacks"));
       //     DoubleJumpMenu.Add("DC", new CheckBox("Jump to cursor(true)  for addon logic (false)"));
       //     DoubleJumpMenu.Add("DS", new CheckBox("Do Second Jump"));
       //     DoubleJumpMenu.Add("DCS", new CheckBox("SecondJump to cursor(true)  for addon logic (false)"));
        }
    }
}
