using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EssentialMapHack.Utilities;

namespace EssentialMapHack
{
    internal class Program
    {
        private static Menu menu;
        private static float lastUpdate;
        private static List<Champ> champions;

        private static void Main()
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            float dt = Game.Time - lastUpdate;
            lastUpdate = Game.Time;
            int i = 0;
            foreach (Champ ch in champions.OrderBy(c => c.invisTime*c.hero.MoveSpeed))
            {
                if (!ch.hero.IsDead&&!ch.hero.IsVisible()&&ch.position.Distance(ch.spawn)<300)
                {
                    ch.position = ch.spawn;
                    ch.place = i;
                    i++;
                }
                ch.Update(dt);
            }
        }


        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            #region menu
            menu = MainMenu.AddMenu("Maphack", "mx");
            menu.AddGroupLabel("GerekliHarita");
            CheckBox en = menu.Add("en", new CheckBox("Aktif", true));
            Globals.Enabled = en.CurrentValue;
            en.OnValueChange += en_OnValueChange;

            CheckBox sr = menu.Add("sr", new CheckBox("B atmayı göster", true));
            Globals.ShowRecalls = sr.CurrentValue;
            sr.OnValueChange += sr_OnValueChange;

            CheckBox sh = menu.Add("sh", new CheckBox("Canı Solda Göster", false));
            Globals.ShowHP = sh.CurrentValue;
            sh.OnValueChange += sh_OnValueChange;

            CheckBox sc = menu.Add("sc", new CheckBox("Canı az olup b atan şampiyonu daireyle göster", true));
            Globals.ShowIG = sc.CurrentValue;
            sc.OnValueChange += sc_OnValueChange;

            Slider s = menu.Add("cq", new Slider("Daire Kalitesi", 500, 15, 1000));
            s.OnValueChange += Program_OnValueChange;
            Globals.CircleQuality = s.CurrentValue;

            Slider s2 = menu.Add("ct", new Slider("Daire Kalınlığı", 9, 3, 30));
            s2.OnValueChange += s2_OnValueChange;
            Globals.CircleWidth = s2.CurrentValue/10f;

            Slider s3 = menu.Add("hc", new Slider("Gizle Daire treshold", 32, 10, 50));
            s3.OnValueChange += s3_OnValueChange;
            Globals.HideCircleTime = s3.CurrentValue;
            menu.AddGroupLabel("Tarafından Christian Brutal Sniper");
            menu.AddLabel("Çeviri TRAdana");
            #endregion
            Init();
        }

        static void Init()
        {
            if (!Globals.Enabled) return;
            if (Util.MinimapMul < 0)
            {
                Core.DelayAction(Init, 1000);
                return;
            }
            lastUpdate = Game.Time;
            champions = new List<Champ>();
            foreach (Champ ch in ObjectManager.Get<AIHeroClient>().Where(hero => !hero.Team.Equals(ObjectManager.Player.Team)).Select(hero => new Champ(hero)))
                champions.Add(ch);
            Game.OnTick += Game_OnUpdate;
            Drawing.OnEndScene += Drawing_OnEndScene;
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            foreach (Champ ch in champions.OrderBy(c => c.invisTime * c.hero.MoveSpeed))
                ch.Draw();
        }


        private static void en_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            if (args.NewValue == false)
            {
                foreach (Champ ch in champions)
                    ch.Kill();
                Drawing.OnEndScene -= Drawing_OnEndScene;
                Game.OnUpdate -= Game_OnUpdate;
            }
            else
                Init();
        }

        static void s2_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
        {
            Globals.CircleWidth = args.NewValue/10f;
        }

        static void s3_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
        {
            Globals.HideCircleTime = args.NewValue;
        }

        static void Program_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
        {
            Globals.CircleQuality = args.NewValue;
        }
        static void sc_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            Globals.ShowIG = args.NewValue;
        }

        static void sh_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            Globals.ShowHP = args.NewValue;
        }

        static void sr_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            Globals.ShowRecalls = args.NewValue;
        }
    }
}
