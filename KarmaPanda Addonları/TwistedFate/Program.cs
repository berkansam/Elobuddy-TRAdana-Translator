namespace TwistedBuddy
{
    using System;

    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Enumerations;
    using EloBuddy.SDK.Events;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;
    using EloBuddy.SDK.Rendering;

    using SharpDX;

    internal class Program
    {
        /// <summary>
        /// Q
        /// </summary>
        public static Spell.Skillshot Q;

        /// <summary>
        /// W
        /// </summary>
        public static Spell.Active W;

        /// <summary>
        /// E
        /// </summary>
        public static Spell.Active E;

        /// <summary>
        /// R
        /// </summary>
        public static Spell.Active R;

        /// <summary>
        /// Twisted Fate's Name
        /// </summary>
        public const string ChampionName = "TwistedFate";

        /// <summary>
        /// Called when program starts
        /// </summary>
        private static void Main()
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        /// <summary>
        /// Called when the game finishes loading.
        /// </summary>
        /// <param name="args">The Args.</param>
        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.BaseSkinName != ChampionName)
            {
                return;
            }

            Q = new Spell.Skillshot(SpellSlot.Q, 1450, SkillShotType.Linear, 0, 1000, 40)
            {
                AllowedCollisionCount = int.MaxValue
            };
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Active(SpellSlot.R, 5500);

            // Menu
            Essentials.MainMenu = MainMenu.AddMenu("Twisted Fate", "TwistedFate");

            // Card Selector Menu
            Essentials.CardSelectorMenu = Essentials.MainMenu.AddSubMenu("Card Selector Menu", "csMenu");
            Essentials.CardSelectorMenu.AddGroupLabel("Kart seçme  Ayarları");
            Essentials.CardSelectorMenu.Add("useY", new KeyBind("Kullan Sarı Card", false, KeyBind.BindTypes.HoldActive, "W".ToCharArray()[0]));
            Essentials.CardSelectorMenu.Add("useB", new KeyBind("Kullan Mavi Card", false, KeyBind.BindTypes.HoldActive, "E".ToCharArray()[0]));
            Essentials.CardSelectorMenu.Add("useR", new KeyBind("Kullan Kırmızı Card", false, KeyBind.BindTypes.HoldActive, "T".ToCharArray()[0]));

            // Combo
            Essentials.ComboMenu = Essentials.MainMenu.AddSubMenu("Combo Menu", "comboMenu");
            Essentials.ComboMenu.AddGroupLabel("Combo Ayarları");
            Essentials.ComboMenu.Add("useQ", new CheckBox("Q Kullan"));
            Essentials.ComboMenu.Add("useCard", new CheckBox("W Kullan"));
            Essentials.ComboMenu.Add("useQStun", new CheckBox("Q sadece stunluysa kullan", false));
            Essentials.ComboMenu.Add("qPred", new Slider("Q İsabet Oranı %", 75));
            Essentials.ComboMenu.Add("wSlider", new Slider("Düşman menzildeyken kartı seç", 300, 0, 10000));
            Essentials.ComboMenu.Add("manaManagerQ", new Slider("Q dan önce ne kadar manan olsun %", 25));
            Essentials.ComboMenu.AddGroupLabel("Kart seçme Ayarları");
            Essentials.ComboMenu.Add("chooser", new ComboBox("Card Seçme Modu", new[] { "Smart", "Blue", "Red", "Yellow" }));
            Essentials.ComboMenu.Add("enemyW", new Slider("Komboda kırmızı kart için kaç hedef gerekli", 4, 1, 5));
            Essentials.ComboMenu.Add("manaW", new Slider("Komboda mavi kart seçmek için gereken mana %", 25));

            // Harass Menu
            Essentials.HarassMenu = Essentials.MainMenu.AddSubMenu("Harass Menu", "harassMenu");
            Essentials.HarassMenu.AddGroupLabel("Dürtme Ayarları");
            Essentials.HarassMenu.Add("useQ", new CheckBox("Q Kullan"));
            Essentials.HarassMenu.Add("useCard", new CheckBox("W Kullan"));
            Essentials.HarassMenu.Add("qPred", new Slider("Q İsabet Oranı %", 75));
            Essentials.HarassMenu.Add("wSlider", new Slider("Düşman menzildeyken kartı seç", 300, 0, 10000));
            Essentials.HarassMenu.Add("manaManagerQ", new Slider("Q dan önce ne kadar manan olsun %", 25));
            Essentials.HarassMenu.AddGroupLabel("Kart seçme Ayarları");
            Essentials.HarassMenu.Add("chooser", new ComboBox("Card Seçme Modu", new[] { "Smart", "Blue", "Red", "Yellow" }));
            Essentials.HarassMenu.Add("enemyW", new Slider("Dürtmede kırmızı kart için kaç hedef gerekli", 3, 1, 5));
            Essentials.HarassMenu.Add("manaW", new Slider("Dürtmede mavi kart seçmek için gereken mana %", 25));

            // Lane Clear Menu
            Essentials.LaneClearMenu = Essentials.MainMenu.AddSubMenu("Lane Clear", "laneclearMenu");
            Essentials.LaneClearMenu.AddGroupLabel("LaneClear Ayarları");
            Essentials.LaneClearMenu.Add("useQ", new CheckBox("Q Kullan", false));
            Essentials.LaneClearMenu.Add("useCard", new CheckBox("W Kullan"));
            Essentials.LaneClearMenu.Add("qPred", new Slider("Q için gereken minyon", 3, 1, 5));
            Essentials.LaneClearMenu.Add("manaManagerQ", new Slider("Q için gereken mana %", 50));
            Essentials.LaneClearMenu.AddGroupLabel("Kart seçme Ayarları");
            Essentials.LaneClearMenu.Add("chooser", new ComboBox("Card Seçme Modu", new[] { "Smart", "Blue", "Red", "Yellow" }));
            Essentials.LaneClearMenu.Add("enemyW", new Slider("kırmızı kart için kaç hedef gerekli", 2, 1, 5));
            Essentials.LaneClearMenu.Add("manaW", new Slider("mavi kart seçmek için gereken mana %", 25));

            // Jungle Clear Menu
            Essentials.JungleClearMenu = Essentials.MainMenu.AddSubMenu("Jungle Clear Menu", "jgMenu");
            Essentials.JungleClearMenu.AddGroupLabel("JungleClear Ayarları");
            Essentials.JungleClearMenu.Add("useQ", new CheckBox("Q Kullan", false));
            Essentials.JungleClearMenu.Add("useCard", new CheckBox("W Kullan"));
            Essentials.JungleClearMenu.Add("qPred", new Slider("Q İsabet Oranı %", 75));
            Essentials.JungleClearMenu.Add("manaManagerQ", new Slider("Q için gereken minyon", 50));
            Essentials.JungleClearMenu.AddGroupLabel("Kart seçme Ayarları");
            Essentials.JungleClearMenu.Add("chooser", new ComboBox("Card Seçme Modu", new[] {"Smart", "Blue", "Red", "Yellow"}));
            Essentials.JungleClearMenu.Add("enemyW", new Slider("kırmızı kart için kaç hedef gerekli", 2, 1, 5));
            Essentials.JungleClearMenu.Add("manaW", new Slider("mavi kart seçmek için gereken mana %", 25));

            // Kill Steal Menu
            Essentials.KillStealMenu = Essentials.MainMenu.AddSubMenu("Kill Steal Menu", "ksMenu");
            Essentials.KillStealMenu.AddGroupLabel("KillSteal Ayarları");
            Essentials.KillStealMenu.Add("useQ", new CheckBox("Use Q to KS"));
            Essentials.KillStealMenu.Add("qPred", new Slider("Q İsabet Oranı %", 75));
            Essentials.KillStealMenu.Add("manaManagerQ", new Slider("Q için gereken mana %", 15));
            Essentials.KillStealMenu.AddSeparator();

            // Drawing Menu
            Essentials.DrawingMenu = Essentials.MainMenu.AddSubMenu("Drawing Menu", "drawMenu");
            Essentials.DrawingMenu.AddGroupLabel("Gösterge Ayarları");
            Essentials.DrawingMenu.Add("drawQ", new CheckBox("Göster Q Menzili"));
            Essentials.DrawingMenu.Add("drawR", new CheckBox("Göster R Menzili"));
            Essentials.DrawingMenu.AddSeparator();

            // Misc Menu
            Essentials.MiscMenu = Essentials.MainMenu.AddSubMenu("Misc Menu", "miscMenu");
            Essentials.MiscMenu.AddGroupLabel("Misc Ayarları");
            Essentials.MiscMenu.Add("autoQ", new CheckBox("Hedef stunlanınca otomatik Q"));
            Essentials.MiscMenu.Add("qPred", new Slider("Q İsabet Oranı %", 75));
            Essentials.MiscMenu.Add("autoY", new CheckBox("Rden sonra oto sarı kart"));
            Essentials.MiscMenu.Add("delay", new CheckBox("karde seçme gecikmesi", false));
            Essentials.MiscMenu.Add("disableAA", new CheckBox("otomatik atak kapa"));

            // Prints Message
            Chat.Print("TwistedBuddy 2.3 - By KarmaPanda ceviri tradana", System.Drawing.Color.Green);

            // Events
            Game.OnUpdate += Game_OnUpdate;
            Orbwalker.OnPreAttack += Orbwalker_OnPreAttack;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            Obj_AI_Base.OnSpellCast += Obj_AI_Base_OnSpellCast;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        /// <summary>
        /// Called before Auto Attack is casted.
        /// </summary>
        /// <param name="target">The Target</param>
        /// <param name="args"></param>
        private static void Orbwalker_OnPreAttack(AttackableUnit target, Orbwalker.PreAttackArgs args)
        {
            if (Essentials.MiscMenu["disableAA"].Cast<CheckBox>().CurrentValue &&
                CardSelector.Status == SelectStatus.Selecting)
            {
                args.Process = false;
                return;
            }
            args.Process = true;
        }

        /// <summary>
        /// Called after Spell Cast
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void Obj_AI_Base_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe)
            {
                return;
            }

            var target = args.Target as AIHeroClient;

            if (target == null || args.SData.Name.ToLower() != "goldcardpreattack" || !Q.IsReady() || !Q.IsInRange(target))
            {
                return;
            }

            if (Essentials.MiscMenu["autoQ"].Cast<CheckBox>().CurrentValue)
            {
                var pred = Q.GetPrediction(target);

                if (pred != null && pred.HitChancePercent >= Essentials.MiscMenu["qPred"].Cast<Slider>().CurrentValue)
                {
                    Q.Cast(pred.CastPosition);
                }
                else
                {
                    Essentials.UseStunQ = true;
                    Essentials.StunnedTarget = target;
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) &&
                Essentials.ComboMenu["useQStun"].Cast<CheckBox>().CurrentValue)
            {
                var pred = Q.GetPrediction(target);

                if (pred != null && pred.HitChancePercent >= Essentials.ComboMenu["qPred"].Cast<Slider>().CurrentValue)
                {
                    Q.Cast(pred.CastPosition);
                }
                else
                {
                    Essentials.UseStunQ = true;
                    Essentials.StunnedTarget = target;
                }
            }
        }

        /// <summary>
        /// Called on Spell Cast
        /// </summary>
        /// <param name="sender">The Person who casted a spell</param>
        /// <param name="args">The Args</param>
        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe)
            {
                return;
            }

            if (args.SData.Name.ToLower() == "gate" && Essentials.MiscMenu["autoY"].Cast<CheckBox>().CurrentValue)
            {
                CardSelector.StartSelecting(Cards.Yellow);
            }
        }

        /// <summary>
        /// Called when game draws.
        /// </summary>
        /// <param name="args">The Args.</param>
        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Essentials.DrawingMenu["drawQ"].Cast<CheckBox>().CurrentValue)
            {
                if (Player.Instance != null)
                {
                    Circle.Draw(Q.IsReady() ? Color.Green : Color.Red, Q.Range, Player.Instance.Position);
                }
            }

            if (!Essentials.DrawingMenu["drawR"].Cast<CheckBox>().CurrentValue)
            {
                return;
            }

            if (Player.Instance != null)
            {
                Circle.Draw(R.IsReady() ? Color.Green : Color.Red, R.Range, Player.Instance.Position);
            }
        }

        /// <summary>
        /// Called when game updates.
        /// </summary>
        /// <param name="args">The Args.</param>
        private static void Game_OnUpdate(EventArgs args)
        {
            if (!Player.Instance.IsRecalling() && !Player.Instance.IsInShopRange())
            {
                var useY = Essentials.CardSelectorMenu["useY"].Cast<KeyBind>().CurrentValue;
                var useB = Essentials.CardSelectorMenu["useB"].Cast<KeyBind>().CurrentValue;
                var useR = Essentials.CardSelectorMenu["useR"].Cast<KeyBind>().CurrentValue;

                if (useY)
                {
                    CardSelector.StartSelecting(Cards.Yellow);
                }

                if (useB)
                {
                    CardSelector.StartSelecting(Cards.Blue);
                }

                if (useR)
                {
                    CardSelector.StartSelecting(Cards.Red);
                }

                StateManager.AutoQ();
            }

            if (Essentials.KillStealMenu["useQ"].Cast<CheckBox>().CurrentValue)
            {
                StateManager.KillSteal();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                StateManager.Combo();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                StateManager.LaneClear();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                StateManager.JungleClear();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                StateManager.Harass();
            }
        }
    }
}
