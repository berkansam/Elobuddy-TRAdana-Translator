using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace KGalio
{
    internal class Program
    {

        public const string ChampionName = "Galio";
        public static Menu Menu, ModesMenu1, ModesMenu2, DrawMenu, Misc;
        public static AIHeroClient PlayerInstance
        {
            get { return Player.Instance; }
        }
        private static float HealthPercent()
        {
            return (PlayerInstance.Health / PlayerInstance.MaxHealth) * 100;
        }

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }



        public static Spell.Skillshot Q;
        public static Spell.Targeted W;
        public static Spell.Skillshot E;
        public static Spell.Active R;



        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Game_OnStart;
            Game.OnUpdate += Game_OnUpdate;
            Drawing.OnDraw += Game_OnDraw;
            //GameObject.OnCreate += Game_ObjectCreate;
            //GameObject.OnDelete += Game_OnDelete;
            //Orbwalker.OnPostAttack += Reset;
            //Game.OnTick += Game_OnTick;
            //Interrupter.OnInterruptableSpell += KInterrupter;
            //Gapcloser.OnGapcloser += KGapCloser;

        }
        static void Game_OnStart(EventArgs args)
        {

            try
            {
                if (ChampionName != PlayerInstance.BaseSkinName)
                {
                    return;
                }

                Bootstrap.Init(null);
                Chat.Print("KGalio Addonu Basariyle Yuklendi", Color.Green);


                Q = new Spell.Skillshot(SpellSlot.Q, 940, SkillShotType.Circular, 1, 1300, 120);
                Q.AllowedCollisionCount = int.MaxValue;
                W = new Spell.Targeted(SpellSlot.W, 830);
                E = new Spell.Skillshot(SpellSlot.E, 1180, SkillShotType.Linear, 1, 1200, 140);
                E.AllowedCollisionCount = int.MaxValue;
                R = new Spell.Active(SpellSlot.R, 560);
                /*R = new Spell.Skillshot(SpellSlot.R, 560, SkillShotType.Circular, 1, 0, 300);
                R.AllowedCollisionCount = int.MaxValue;*/



                Menu = MainMenu.AddMenu("KGalio", "galio");
                Menu.AddSeparator();
                Menu.AddLabel("Criado por Bruno105");
                Menu.AddLabel("Çeviri TRAdana Güncelleme Gelirse Pm Atın");


                //------------//
                //-Mode Menu-//
                //-----------//

                var Enemies = EntityManager.Heroes.Enemies.Where(a => !a.IsMe).OrderBy(a => a.BaseSkinName);
                ModesMenu1 = Menu.AddSubMenu("Combo/Harass/KS", "Modes1Galio");
                ModesMenu1.AddSeparator();
                ModesMenu1.AddLabel("Kombo Ayarları");
                ModesMenu1.Add("ComboQ", new CheckBox("Komboda Q Kullan", true));
                ModesMenu1.Add("ComboW", new CheckBox("Komboda W Kullan", true));
                ModesMenu1.Add("ComboE", new CheckBox("Komboda E Kullan", true));
                ModesMenu1.Add("ComboR", new CheckBox("Komboda R Kullan", true));
                ModesMenu1.Add("MinR", new Slider("R Kullanmak için menzilde şu kadar düşman:", 3, 1, 5));
                ModesMenu1.AddSeparator();
                ModesMenu1.AddLabel("Dürtme Ayarları");
                ModesMenu1.Add("ManaH", new Slider("Manam Şundan azsa Kullanma <=", 40));
                ModesMenu1.Add("HarassQ", new CheckBox("Dürtmede Q Kullan", true));
                ModesMenu1.Add("HarassW", new CheckBox("Dürtmede W Kullan", true));
                ModesMenu1.Add("HarassE", new CheckBox("Dürtmede E Kullan", true));
                ModesMenu1.AddSeparator();
                ModesMenu1.AddLabel("Kill Çalma Ayarları");
                ModesMenu1.Add("KQ", new CheckBox("Kill Çalmak için Q Kullan", true));
                ModesMenu1.Add("KE", new CheckBox("Kill Çalmak için E Kullan", true));
                ModesMenu1.Add("KR", new CheckBox("Kill Çalmak için R Kullan", true));

                ModesMenu2 = Menu.AddSubMenu("Lane/Orman/SonV", "Modes2Galio");
                ModesMenu2.AddLabel("SonVuruş Ayarları");
                ModesMenu2.Add("ManaL", new Slider("Büyü Kullanma Manam Şundan Azsa <= ", 40));
                ModesMenu2.Add("LastQ", new CheckBox("Son vuruşta Q Kullan", true));
                ModesMenu2.Add("LastE", new CheckBox("Son vuruşta E Kullan", true));
                ModesMenu2.AddLabel("Lane Temizleme Ayarları");
                ModesMenu2.Add("ManaF", new Slider("Manam Şundan Azsa Büyü Kullanma <=", 40));
                ModesMenu2.Add("FarmQ", new CheckBox("LaneTemizlemede Q Kullan", true));
                ModesMenu2.Add("MinionQ", new Slider("Q için gerekli minyon şu veya şundan fazla :", 2, 1, 5));
                ModesMenu2.Add("FarmE", new CheckBox("LaneTemizlemede E Kullan", true));
                ModesMenu2.Add("MinionE", new Slider("E için gerekli minyon şu veya şundan fazla :", 2, 1, 5));
                ModesMenu2.AddLabel("Orman Temizleme Ayarları");
                ModesMenu2.Add("ManaJ", new Slider("Manam Şundan azsa Büyü Kullanma <=", 40));
                ModesMenu2.Add("JungQ", new CheckBox("OrmanTemizlemede Q Kullan", true));
                ModesMenu2.Add("JungW", new CheckBox("OrmanTemizlemede W Kullan", true));
                ModesMenu2.Add("JungE", new CheckBox("OrmanTemizlemede E Kullan", true));



                //------------//
                //-Draw Menu-//
                //----------//
                DrawMenu = Menu.AddSubMenu("Göstergeler", "DrawGalio");
                DrawMenu.Add("drawAA", new CheckBox("AA'yı Göster", true));
                DrawMenu.Add("drawQ", new CheckBox(" Q Menzilini Göster", true));
                DrawMenu.Add("drawE", new CheckBox(" E Menzilini Göster", true));
                DrawMenu.Add("drawR", new CheckBox(" R Menzilini Göster", true));
                //------------//
                //-Misc Menu-//
                //----------//
                /*  Misc = Menu.AddSubMenu("MiscMenu", "Misc");
                  Misc.Add("useEGapCloser", new CheckBox("E on GapCloser", true));
                  Misc.Add("useRGapCloser", new CheckBox("R on GapCloser", true));
                  Misc.Add("useEInterrupter", new CheckBox("use E to Interrupt", true));
                  Misc.Add("useRInterrupter", new CheckBox("use R to Interrupt", true));
                  Misc.Add("Key", new KeyBind("Key to insec", false, KeyBind.BindTypes.HoldActive, (uint)'A'));*/

            }

            catch (Exception e)
            {
                Chat.Print("KGalio: Exception occured while Initializing Addon. Error: " + e.Message);

            }

        }

        private static void Game_OnDraw(EventArgs args)
        {

            Circle.Draw(Color.Red, _Player.GetAutoAttackRange(), Player.Instance.Position);
            if (Q.IsReady() && Q.IsLearned)
            {
                Circle.Draw(Color.White, Q.Range, Player.Instance.Position);
            }
            if (W.IsReady() && W.IsLearned)
            {
                Circle.Draw(Color.Green, W.Range, Player.Instance.Position);
            }
            if (E.IsReady() && E.IsLearned)
            {
                Circle.Draw(Color.Aqua, E.Range, Player.Instance.Position);
            }
            if (R.IsReady() && R.IsLearned)
            {
                Circle.Draw(Color.Black, R.Range, Player.Instance.Position);
            }
        }

        static void Game_OnUpdate(EventArgs args)
        {


            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                ModesManager.Combo();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                ModesManager.Harass();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {

                ModesManager.LaneClear();

            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {

                ModesManager.JungleClear();
            }



            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            {
                ModesManager.LastHit();

            }



        }
        public static void Game_OnTick(EventArgs args)
        {
            ModesManager.KillSteal();

        }
    }
}
