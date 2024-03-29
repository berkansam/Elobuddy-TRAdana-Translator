﻿namespace EloBuddy_PandaTeemo
{
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

    using Color = System.Drawing.Color;

    /// <summary>
    /// Made by KarmaPanda. Ported from LeagueSharp.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Teemo's Name
        /// </summary>
        private const string ChampionName = "Teemo";

        /// <summary>
        /// Array of ADC Names
        /// </summary>
        private static readonly string[] Marksman =
        {
            "Ashe", "Caitlyn", "Corki", "Draven", "Ezreal", "Graves", "Jinx",
            "Kalista", "Kindred", "KogMaw", "Lucian", "MissFortune", "Quinn", "Sivir", "Teemo", "Tristana", "Twitch",
            "Urgot", "Varus", "Vayne"
        };

        /// <summary>
        /// Spell Q
        /// </summary>
        public static Spell.Targeted Q;

        /// <summary>
        /// Spell W
        /// </summary>
        public static Spell.Active W;

        /// <summary>
        /// Spell E
        /// </summary>
        public static Spell.Active E;

        /// <summary>
        /// Spell R
        /// </summary>
        public static Spell.Skillshot R;

        /// <summary>
        /// Last time R was Used.
        /// </summary>
        public static int LastR;

        /// <summary>
        /// Delay for R
        /// </summary>
        public static int Delay = new Random().Next(1000, 5000);

        /// <summary>
        /// Last time R was Used in LaneClear
        /// </summary>
        public static int LaneClearLastR;

        /// <summary>
        /// Initializes File Handler
        /// </summary>
        public static FileHandler Handler;

        /// <summary>
        /// Initializes Shroom Positions
        /// </summary>
        public static ShroomTables ShroomPositions;

        /// <summary>
        /// Initializes the Menu
        /// </summary>
        public static Menu PandaTeemo,
            ComboMenu,
            HarassMenu,
            LaneClearMenu,
            JungleClearMenu,
            KillStealMenu,
            FleeMenu,
            DrawingMenu,
            InterruptMenu,
            MiscMenu,
            Debug;

        /// <summary>
        /// Called when program starts
        /// </summary>
        private static void Main()
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        /// <summary>
        /// Load when game starts.
        /// </summary>
        /// <param name="args"></param>
        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            // Checks if Player is Teemo
            if (Player.Instance.BaseSkinName != ChampionName)
            {
                return;
            }

            Bootstrap.Init(null);

            Q = new Spell.Targeted(SpellSlot.Q, 680);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Skillshot(SpellSlot.R, 300, SkillShotType.Circular, 500, 1000, 120);

            // Menu
            PandaTeemo = MainMenu.AddMenu("PandaTeemo", "PandaTeemo");
            PandaTeemo.AddGroupLabel("Bu addon KarmaPanda Tarafından Geliştirilmiştir.");
            PandaTeemo.AddGroupLabel(
                "Kimseden yardım alınmamıştır");
            PandaTeemo.AddGroupLabel("Addonumu Kullandığınız için Teşekkür Ederim");
            PandaTeemo.AddGroupLabel("Çeviri TRAdana");

            // Combo Menu
            ComboMenu = PandaTeemo.AddSubMenu("Kombo", "Combo");
            ComboMenu.AddGroupLabel("Kombo Ayarları");
            ComboMenu.Add("qcombo", new CheckBox("Q kullan komboda"));
            ComboMenu.Add("wcombo", new CheckBox("W Kullan Komboda"));
            ComboMenu.Add("rcombo", new CheckBox("Komboda R için Kitela"));
            ComboMenu.Add("useqADC", new CheckBox("Kombo Sırasında Q yu Her zaman ADCye At", false));
            ComboMenu.Add("wCombat", new CheckBox("Wyi Sadece Rakip Menzildeyse Kullan"));
            ComboMenu.Add("rCharge", new Slider("R Kullanmadan Önce Şu Kadar Biriksin", 2, 1, 3));
            ComboMenu.Add("checkCamo", new CheckBox("Aktif Edildiğinde Komboda Gizliliği Önler", false));

            // Harass Menu
            HarassMenu = PandaTeemo.AddSubMenu("Dürtme", "Harass");
            HarassMenu.AddGroupLabel("Dürtme Ayarları");
            HarassMenu.Add("qharass", new CheckBox("Q ile Dürt"));

            // LaneClear Menu
            LaneClearMenu = PandaTeemo.AddSubMenu("LaneClear", "LaneClear");
            LaneClearMenu.AddGroupLabel("LaneClear Ayarları");
            LaneClearMenu.Add("qclear", new CheckBox("Q kullan Lanetemizlerken", false));
            LaneClearMenu.Add("qManaManager", new Slider("Q için gereken mana", 50));
            LaneClearMenu.Add("attackTurret", new CheckBox("Kuleye Vur"));
            LaneClearMenu.Add("attackWard", new CheckBox("Toteme Vur"));
            LaneClearMenu.Add("rclear", new CheckBox("Laneyi R ile Temizle"));
            LaneClearMenu.Add("minionR", new Slider("R için gereken minyon", 3, 1, 4));

            // JungleClear Menu
            JungleClearMenu = PandaTeemo.AddSubMenu("Orman Temizleme", "JungleClear");
            JungleClearMenu.AddGroupLabel("Orman Temizleme Ayarları");
            JungleClearMenu.Add("qclear", new CheckBox("Q ile Orman Temizle"));
            JungleClearMenu.Add("rclear", new CheckBox("R ile ORman Temizle"));
            JungleClearMenu.Add("qManaManager", new Slider("Q için gereken mana", 25));

            // Interrupter && Gapcloser
            InterruptMenu = PandaTeemo.AddSubMenu("Interrupt / Gapcloser", "Interrupt");
            InterruptMenu.AddGroupLabel("Interruptter and Gapcloser Ayarları");
            InterruptMenu.Add("intq", new CheckBox("Q ile Interrupt "));
            InterruptMenu.Add("gapR", new CheckBox("R ile Gapclose"));

            // KillSteal Menu
            KillStealMenu = PandaTeemo.AddSubMenu("Kill Çalma", "KSMenu");
            KillStealMenu.AddGroupLabel("Kill Çalma Ayarları");
            KillStealMenu.Add("KSQ", new CheckBox("Q ile Çal"));
            KillStealMenu.Add("KSR", new CheckBox("R ile Çal"));

            // Flee Menu
            FleeMenu = PandaTeemo.AddSubMenu("Flee Menu", "Flee");
            FleeMenu.AddGroupLabel("Flee(kaçma) Ayarları");
            FleeMenu.Add("w", new CheckBox("Kaçarken W ile Kaç"));
            FleeMenu.Add("r", new CheckBox("Kaçarken R kullan"));
            FleeMenu.Add("rCharge", new Slider("R için gereken yük", 2, 1, 3));

            // Drawing Menu
            DrawingMenu = PandaTeemo.AddSubMenu("Göstergeler", "Drawing");
            DrawingMenu.AddGroupLabel("Gösterge Ayarları");
            DrawingMenu.Add("drawQ", new CheckBox("Göster Q Range"));
            DrawingMenu.Add("drawR", new CheckBox("Göster R Range"));
            DrawingMenu.Add("colorBlind", new CheckBox("Renk Körü Modu", false));
            DrawingMenu.Add("drawautoR", new CheckBox("Önemli Mantar Alanlarını Göster"));
            DrawingMenu.Add("DrawVision", new Slider("Mantar Görüşü", 1500, 2500, 1000));

            // Debug Menu
            Debug = PandaTeemo.AddSubMenu("Debug", "debug");
            Debug.AddGroupLabel("Debug Ayarları");
            Debug.Add("debugdraw", new CheckBox("Draw Kordinatlar", false));
            Debug.Add("x", new Slider("Nerede Göster X", 500, 0, 3840));
            Debug.Add("y", new Slider("Nerede Göster Y", 500, 0, 2160));
            Debug.Add("debugpos", new CheckBox("Göster Mantar Kordinatlarını"));

            // Misc
            MiscMenu = PandaTeemo.AddSubMenu("Ek", "Misc");
            MiscMenu.AddGroupLabel("EK Ayarlar");
            MiscMenu.Add("autoQ", new CheckBox("Otomatik Q", false));
            MiscMenu.Add("autoW", new CheckBox("Otomatik W", false));
            MiscMenu.Add("autoR", new CheckBox("Otomatik Önemli Yerlere Mantar Yerleştirme"));
            MiscMenu.Add("rCharge", new Slider("Otomatik R atmadan önce şu kadar yük olsun", 2, 1, 3));
            MiscMenu.Add("autoRPanic", new KeyBind("Otomatik R için panik tuşu", false, KeyBind.BindTypes.HoldActive, 84));
            MiscMenu.Add("customLocation", new CheckBox("Use Custom Location for Auto Shroom (Requires Reload)"));
            MiscMenu.AddSeparator();
            MiscMenu.Add("checkAA", new CheckBox("Menzilden Çıkmışsa Q (AA Kontrol Et)"));
            MiscMenu.Add("checkaaRange", new Slider("Q menzilinden ne Kadar çıktıysa denetle (denetleAA)", 100, 0, 180));

            // Events
            Game.OnTick += Game_OnTick;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Orbwalker.OnPreAttack += Orbwalker_OnPreAttack;
            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;

            Chat.Print("PandaTeemo EloBuddy Edition Yuklenti KarmaPanda Tarafindan- tradana iyi oyunlar diler", Color.LightBlue);

            // Loads ShroomPosition
            Handler = new FileHandler();
            ShroomPositions = new ShroomTables();
        }

        /// <summary>
        /// Interrupts interruptable spell
        /// </summary>
        /// <param name="sender">Enemy</param>
        /// <param name="e">The Arguments</param>
        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender,
            Interrupter.InterruptableSpellEventArgs e)
        {
            var intq = InterruptMenu["intq"].Cast<CheckBox>().CurrentValue;

            if (!intq || !Q.IsReady())
            {
                return;
            }
            if (sender == null)
            {
                return;
            }
            if (e.DangerLevel == DangerLevel.High)
            {
                Q.Cast(sender);
            }
        }

        /// <summary>
        /// Gapcloses whenever possible.
        /// </summary>
        /// <param name="sender">Enemy</param>
        /// <param name="e">The Arguments</param>
        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            var gapR = InterruptMenu["gapR"].Cast<CheckBox>().CurrentValue;

            if (!gapR || !sender.IsValidTarget() || !sender.IsFacing(Player.Instance))
            {
                return;
            }
            var pred = R.GetPrediction(sender);

            if (pred.HitChance >= HitChance.High)
            {
                R.Cast(sender.Position);
            }
        }

        /// <summary>
        /// After Attack
        /// </summary>
        /// <param name="target">The Target that was attacked</param>
        /// <param name="args">The Args</param>
        private static void Orbwalker_OnPreAttack(AttackableUnit target, EventArgs args)
        {
            var t = target as AIHeroClient;
            var checkAa = MiscMenu["checkAA"].Cast<CheckBox>().CurrentValue;
            var checkaaRange = MiscMenu["checkaaRange"].Cast<Slider>().CurrentValue;

            if (t != null && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                var useQCombo = ComboMenu["qcombo"].Cast<CheckBox>().CurrentValue;
                var targetAdc = ComboMenu["useqADC"].Cast<CheckBox>().CurrentValue;

                #region Check AA

                if (checkAa)
                {
                    if (targetAdc)
                    {
                        foreach (var adc in Marksman)
                        {
                            if (t.Name == adc && useQCombo && Q.IsReady() &&
                                Player.Instance.Distance(target) < Q.Range - checkaaRange)
                            {
                                Q.Cast(t);
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (useQCombo && Q.IsReady() && Player.Instance.Distance(target) < Q.Range - checkaaRange)
                        {
                            Q.Cast(t);
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                    #endregion

                    #region No Check AA

                else
                {
                    if (targetAdc)
                    {
                        foreach (var adc in Marksman)
                        {
                            if (t.Name == adc && useQCombo && Q.IsReady() && Q.IsInRange(t))
                            {
                                Q.Cast(t);
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (useQCombo && Q.IsReady() && Q.IsInRange(t))
                        {
                            Q.Cast(t);
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                #endregion
            }
            if (t == null || !Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                return;
            }
            var useQHarass = HarassMenu["qharass"].Cast<CheckBox>().CurrentValue;

            #region Q Cast

            if (checkAa)
            {
                if (useQHarass && Q.IsReady() && Player.Instance.Distance(t) < Q.Range - checkaaRange)
                {
                    Q.Cast(t);
                }
            }
            else
            {
                if (useQHarass && Q.IsReady() && Player.Instance.Distance(t) < Q.Range)
                {
                    Q.Cast(t);
                }
            }

            #endregion
        }

        /// <summary>
        /// Whenever a Spell gets Casted
        /// </summary>
        /// <param name="sender">The Player</param>
        /// <param name="args">The Spell</param>
        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe)
            {
                return;
            }

            if (args.SData.Name.ToLower() == "teemorcast")
            {
                LastR = Environment.TickCount;
            }
        }

        /// <summary>
        /// Checks if there is shroom in location
        /// </summary>
        /// <param name="position">The location of check</param>
        /// <returns>If that location has a shroom.</returns>
        private static bool IsShroomed(Vector3 position)
        {
            return
                ObjectManager.Get<Obj_AI_Base>()
                    .Where(obj => obj.Name == "Noxious Trap")
                    .Any(obj => position.Distance(obj.Position) <= 250);
        }

        /// <summary>
        /// Does the Combo
        /// </summary>
        private static void Combo()
        {
            var checkCamo = ComboMenu["checkCamo"].Cast<CheckBox>().CurrentValue;

            if (checkCamo && Player.Instance.HasBuff("CamouflageStealth"))
            {
                return;
            }

            var enemies =
                EntityManager.Heroes.Enemies.FirstOrDefault(
                    t => t.IsValidTarget() && Player.Instance.IsInAutoAttackRange(t));
            var rtarget = TargetSelector.GetTarget(R.Range, DamageType.Magical);
            var useW = ComboMenu["wcombo"].Cast<CheckBox>().CurrentValue;
            var useR = ComboMenu["rcombo"].Cast<CheckBox>().CurrentValue;
            var wCombat = ComboMenu["wCombat"].Cast<CheckBox>().CurrentValue;
            var rCount = Player.Instance.Spellbook.GetSpell(SpellSlot.R).Ammo;
            var rCharge = ComboMenu["rCharge"].Cast<Slider>().CurrentValue;

            if (W.IsReady() && useW && !wCombat)
            {
                W.Cast();
            }

            if (useW && wCombat)
            {
                if (W.IsReady() && enemies != null)
                {
                    W.Cast();
                }
            }

            if (rtarget == null)
            {
                return;
            }
            var predictionR = R.GetPrediction(rtarget);

            if (!R.IsReady() || !useR || !R.IsInRange(rtarget) || rCharge > rCount || !rtarget.IsValidTarget()
                || IsShroomed(predictionR.CastPosition))
            {
                return;
            }
            if (predictionR.HitChance >= HitChance.High)
            {
                R.Cast(predictionR.CastPosition);
            }
        }

        /// <summary>
        /// Kill Steal
        /// </summary>
        private static void KillSteal()
        {
            var ksq = KillStealMenu["KSQ"].Cast<CheckBox>().CurrentValue;
            var ksr = KillStealMenu["KSR"].Cast<CheckBox>().CurrentValue;

            if (ksq)
            {
                var target =
                    EntityManager.Heroes.Enemies.Where(
                        t =>
                            t.IsValidTarget() && Q.IsInRange(t) &&
                            DamageLibrary.CalculateDamage(t, true, false, false, false) >= t.Health)
                        .OrderBy(t => t.Health)
                        .FirstOrDefault();

                if (target != null && Q.IsReady())
                {
                    Q.Cast(target);
                }
            }

            if (!ksr)
            {
                return;
            }

            var rTarget =
                EntityManager.Heroes.Enemies.Where(
                    t =>
                        t.IsValidTarget() && R.IsInRange(t) &&
                        DamageLibrary.CalculateDamage(t, false, false, false, true) >= t.Health)
                    .OrderBy(t => t.Health)
                    .FirstOrDefault();

            if (rTarget == null || !R.IsReady())
            {
                return;
            }
            var pred = R.GetPrediction(rTarget);

            if (pred.HitChance >= HitChance.High)
            {
                R.Cast(pred.CastPosition);
            }
        }

        /// <summary>
        /// LaneClear
        /// </summary>
        private static void LaneClear()
        {
            var qClear = LaneClearMenu["qclear"].Cast<CheckBox>().CurrentValue;
            var qManaManager = LaneClearMenu["qManaManager"].Cast<Slider>().CurrentValue;
            var qMinion =
                EntityManager.MinionsAndMonsters.EnemyMinions.Where(
                    t => Q.IsInRange(t) && t.IsValidTarget());

            foreach (var m in qMinion.Where(m => Q.IsReady()
                                                 && qClear
                                                 &&
                                                 m.Health <= DamageLibrary.CalculateDamage(m, true, false, false, false)
                                                 && qManaManager <= (int) Player.Instance.ManaPercent))
            {
                Q.Cast(m);
            }

            var useR = LaneClearMenu["rclear"].Cast<CheckBox>().CurrentValue;

            if (useR)
            {
                var allMinionsR =
                    EntityManager.MinionsAndMonsters.EnemyMinions.Where(t => R.IsInRange(t) && t.IsValidTarget())
                        .OrderBy(t => t.Health);
                var rLocation = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(allMinionsR, R.Width,
                    (int) R.Range);
                var minionR = LaneClearMenu["minionR"].Cast<Slider>().CurrentValue;

                if (rLocation.HitNumber >= minionR
                    && Environment.TickCount - LaneClearLastR >= Delay)
                {
                    R.Cast(rLocation.CastPosition);
                    LaneClearLastR = Environment.TickCount;
                }
            }
        }

        /// <summary>
        /// Does the JungleClear
        /// </summary>
        private static void JungleClear()
        {
            var useQ = JungleClearMenu["qclear"].Cast<CheckBox>().CurrentValue;
            var useR = JungleClearMenu["rclear"].Cast<CheckBox>().CurrentValue;
            var ammoR = Player.Instance.Spellbook.GetSpell(SpellSlot.R).Ammo;
            var qManaManager = JungleClearMenu["qManaManager"].Cast<Slider>().CurrentValue;
            var jungleMobQ =
                EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.ServerPosition, Q.Range)
                    .FirstOrDefault();
            var jungleMobR = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.ServerPosition, R.Range);

            if (useQ && jungleMobQ != null)
            {
                if (Q.IsReady() && qManaManager <= (int) Player.Instance.ManaPercent)
                {
                    Q.Cast(jungleMobQ);
                }
            }

            var firstjunglemobR = jungleMobR.FirstOrDefault();

            if (!useR || firstjunglemobR == null)
            {
                return;
            }

            if (R.IsReady() && Environment.TickCount - LaneClearLastR >= Delay && ammoR >= 1)
            {
                R.Cast(firstjunglemobR.ServerPosition);
                LaneClearLastR = Environment.TickCount;
            }
        }

        /// <summary>
        /// Does the AutoShroom
        /// </summary>
        private static void AutoShroom()
        {
            var autoRPanic = MiscMenu["autoRPanic"].Cast<KeyBind>().CurrentValue;

            if (autoRPanic)
            {
                Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
            }

            if (!R.IsReady() || autoRPanic)
            {
                return;
            }

            var rCharge = MiscMenu["rCharge"].Cast<Slider>().CurrentValue;
            var rCount = Player.Instance.Spellbook.GetSpell(SpellSlot.R).Ammo;

            switch (Game.MapId)
            {
                case GameMapId.SummonersRift:
                    if (!ShroomPositions.SummonersRift.Any())
                    {
                        return;
                    }
                    foreach (
                        var place in
                            ShroomPositions.SummonersRift.Where(
                                pos => pos.Distance(Player.Instance.ServerPosition) <= R.Range && !IsShroomed(pos))
                                .Where(place => rCharge <= rCount && Environment.TickCount - LastR > Delay))
                    {
                        R.Cast(new Vector3(place.X + new Random().Next(0, 100), place.Y + new Random().Next(0, 100),
                            place.Z + new Random().Next(0, 100)));
                    }
                    break;
                case GameMapId.HowlingAbyss:
                    if (!ShroomPositions.HowlingAbyss.Any())
                    {
                        return;
                    }
                    foreach (
                        var place in
                            ShroomPositions.HowlingAbyss.Where(
                                pos => pos.Distance(Player.Instance.ServerPosition) <= R.Range && !IsShroomed(pos))
                                .Where(place => rCharge <= rCount && Environment.TickCount - LastR > Delay))
                    {
                        R.Cast(new Vector3(place.X + new Random().Next(0, 100), place.Y + new Random().Next(0, 100),
                            place.Z + new Random().Next(0, 100)));
                    }
                    break;
                case GameMapId.CrystalScar:
                    if (!ShroomPositions.CrystalScar.Any())
                    {
                        return;
                    }
                    foreach (
                        var place in
                            ShroomPositions.CrystalScar.Where(
                                pos => pos.Distance(Player.Instance.ServerPosition) <= R.Range && !IsShroomed(pos))
                                .Where(place => rCharge <= rCount && Environment.TickCount - LastR > Delay))
                    {
                        R.Cast(new Vector3(place.X + new Random().Next(0, 100), place.Y + new Random().Next(0, 100),
                            place.Z + new Random().Next(0, 100)));
                    }
                    break;
                case GameMapId.TwistedTreeline:
                    if (!ShroomPositions.TwistedTreeline.Any())
                    {
                        return;
                    }
                    foreach (
                        var place in
                            ShroomPositions.TwistedTreeline.Where(
                                pos => pos.Distance(Player.Instance.ServerPosition) <= R.Range && !IsShroomed(pos))
                                .Where(place => rCharge <= rCount && Environment.TickCount - LastR > Delay))
                    {
                        R.Cast(new Vector3(place.X + new Random().Next(0, 100), place.Y + new Random().Next(0, 100),
                            place.Z + new Random().Next(0, 100)));
                    }
                    break;
                default:
                    if (Game.MapId.ToString() == "Unknown")
                    {
                        if (!ShroomPositions.ButcherBridge.Any())
                        {
                            return;
                        }
                        foreach (
                            var place in
                                ShroomPositions.ButcherBridge.Where(
                                    pos =>
                                        pos.Distance(Player.Instance.ServerPosition) <= R.Range && !IsShroomed(pos))
                                    .Where(place => rCharge <= rCount && Environment.TickCount - LastR > Delay))
                        {
                            R.Cast(new Vector3(place.X + new Random().Next(0, 100), place.Y + new Random().Next(0, 100),
                                place.Z + new Random().Next(0, 100)));
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Does the Flee
        /// </summary>
        private static void Flee()
        {
            // Checks if toggle is on
            var useW = FleeMenu["w"].Cast<CheckBox>().CurrentValue;
            var useR = FleeMenu["r"].Cast<CheckBox>().CurrentValue;
            var rCharge = FleeMenu["rCharge"].Cast<Slider>().CurrentValue;

            // Uses W if avaliable and if toggle is on
            if (useW && W.IsReady())
            {
                W.Cast();
            }

            // Uses R if avaliable and if toggle is on
            if (useR && R.IsReady() && rCharge <= Player.Instance.Spellbook.GetSpell(SpellSlot.R).Ammo)
            {
                R.Cast(Player.Instance.ServerPosition);
            }
        }

        /// <summary>
        /// Auto Q
        /// </summary>
        private static void AutoQ()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            var allMinionsQ =
                EntityManager.MinionsAndMonsters.EnemyMinions.Where(t => Q.IsInRange(t)).OrderBy(t => t.Health);

            if (target == null)
            {
                return;
            }

            if (Q.IsReady() && allMinionsQ.Any())
            {
                foreach (
                    var minion in
                        allMinionsQ.Where(
                            minion =>
                                minion.Health < DamageLibrary.CalculateDamage(minion, true, false, false, false) &&
                                Q.IsInRange(minion)))
                {
                    Q.Cast(minion);
                }
            }
            else if (Q.IsReady() && target.IsValidTarget(Q.Range) && Player.Instance.ManaPercent >= 25)
            {
                Q.Cast(target);
            }
        }

        /// <summary>
        /// Auto W
        /// </summary>
        private static void AutoW()
        {
            if (!W.IsReady())
            {
                return;
            }

            if (W.IsReady())
            {
                W.Cast();
            }
        }

        /// <summary>
        /// Called when Game Updates.
        /// </summary>
        /// <param name="args"></param>
        private static void Game_OnTick(EventArgs args)
        {
            R = new Spell.Skillshot(SpellSlot.R, (uint) (new[] {0, 400, 650, 900}[R.Level]), SkillShotType.Circular, 500, 1000, 120);

            var autoQ = MiscMenu["autoQ"].Cast<CheckBox>().CurrentValue;
            var autoW = MiscMenu["autoW"].Cast<CheckBox>().CurrentValue;

            if (!Player.Instance.IsRecalling() && !Player.Instance.IsDead)
            {
                if (autoQ)
                {
                    AutoQ();
                }

                if (autoW)
                {
                    AutoW();
                }

                if (MiscMenu["autoR"].Cast<CheckBox>().CurrentValue)
                {
                    AutoShroom();
                }
            }

            if (KillStealMenu["KSQ"].Cast<CheckBox>().CurrentValue
                || KillStealMenu["KSR"].Cast<CheckBox>().CurrentValue)
            {
                KillSteal();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Combo();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                LaneClear();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                JungleClear();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                Flee();
            }
        }

        /// <summary>
        /// Called when Game Draws
        /// </summary>
        /// <param name="args">
        /// The Args
        /// </param>
        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Debug["debugdraw"].Cast<CheckBox>().CurrentValue)
            {
                Drawing.DrawText(
                    Debug["x"].Cast<Slider>().CurrentValue,
                    Debug["y"].Cast<Slider>().CurrentValue,
                    Color.Red,
                    Player.Instance.Position.ToString());
            }

            var drawQ = DrawingMenu["drawQ"].Cast<CheckBox>().CurrentValue;
            var drawR = DrawingMenu["drawR"].Cast<CheckBox>().CurrentValue;
            var colorBlind = DrawingMenu["colorBlind"].Cast<CheckBox>().CurrentValue;
            var player = Player.Instance.Position;

            if (drawQ && colorBlind)
            {
                Circle.Draw(Q.IsReady() ? SharpDX.Color.YellowGreen : SharpDX.Color.Red, Q.Range, player);
            }

            if (drawQ && !colorBlind)
            {
                Circle.Draw(Q.IsReady() ? SharpDX.Color.LightGreen : SharpDX.Color.Red, Q.Range, player);
            }

            if (drawR && colorBlind)
            {
                Circle.Draw(R.IsReady() ? SharpDX.Color.YellowGreen : SharpDX.Color.Red, R.Range, player);
            }

            if (drawR && !colorBlind)
            {
                Circle.Draw(R.IsReady() ? SharpDX.Color.LightGreen : SharpDX.Color.Red, R.Range, player);
            }

            var drawautoR = DrawingMenu["drawautoR"].Cast<CheckBox>().CurrentValue;

            if (drawautoR && Game.MapId == GameMapId.SummonersRift)
            {
                if (!ShroomPositions.SummonersRift.Any())
                {
                    return;
                }
                foreach (
                    var place in
                        ShroomPositions.SummonersRift.Where(
                            pos =>
                                pos.Distance(Player.Instance.Position) <=
                                DrawingMenu["DrawVision"].Cast<Slider>().CurrentValue))
                {
                    if (colorBlind)
                    {
                        Circle.Draw(IsShroomed(place) ? SharpDX.Color.Red : SharpDX.Color.YellowGreen, 100, place);
                    }
                    else
                    {
                        Circle.Draw(IsShroomed(place) ? SharpDX.Color.Red : SharpDX.Color.LightGreen, 100, place);
                    }
                }
            }
            else if (drawautoR && Game.MapId == GameMapId.CrystalScar)
            {
                if (!ShroomPositions.CrystalScar.Any())
                {
                    return;
                }
                foreach (
                    var place in
                        ShroomPositions.CrystalScar.Where(
                            pos =>
                                pos.Distance(Player.Instance.Position) <=
                                DrawingMenu["DrawVision"].Cast<Slider>().CurrentValue))
                {
                    if (colorBlind)
                    {
                        Circle.Draw(IsShroomed(place) ? SharpDX.Color.Red : SharpDX.Color.YellowGreen, 100, place);
                    }
                    else
                    {
                        Circle.Draw(IsShroomed(place) ? SharpDX.Color.Red : SharpDX.Color.LightGreen, 100, place);
                    }
                }
            }
            else if (drawautoR && Game.MapId == GameMapId.HowlingAbyss)
            {
                if (!ShroomPositions.HowlingAbyss.Any())
                {
                    return;
                }
                foreach (
                    var place in
                        ShroomPositions.HowlingAbyss.Where(
                            pos =>
                                pos.Distance(Player.Instance.Position) <=
                                DrawingMenu["DrawVision"].Cast<Slider>().CurrentValue))
                {
                    if (colorBlind)
                    {
                        Circle.Draw(IsShroomed(place) ? SharpDX.Color.Red : SharpDX.Color.YellowGreen, 100, place);
                    }
                    else
                    {
                        Circle.Draw(IsShroomed(place) ? SharpDX.Color.Red : SharpDX.Color.LightGreen, 100, place);
                    }
                }
            }
            else if (drawautoR && Game.MapId == GameMapId.TwistedTreeline)
            {
                if (!ShroomPositions.TwistedTreeline.Any())
                {
                    return;
                }
                foreach (
                    var place in
                        ShroomPositions.TwistedTreeline.Where(
                            pos =>
                                pos.Distance(Player.Instance.Position) <=
                                DrawingMenu["DrawVision"].Cast<Slider>().CurrentValue))
                {
                    if (colorBlind)
                    {
                        Circle.Draw(IsShroomed(place) ? SharpDX.Color.Red : SharpDX.Color.YellowGreen, 100, place);
                    }
                    else
                    {
                        Circle.Draw(IsShroomed(place) ? SharpDX.Color.Red : SharpDX.Color.LightGreen, 100, place);
                    }
                }
            }
            else if (drawautoR && ShroomPositions.ButcherBridge.Any())
            {
                foreach (
                    var place in
                        ShroomPositions.ButcherBridge.Where(
                            pos =>
                                pos.Distance(Player.Instance.Position)
                                <= DrawingMenu["DrawVision"].Cast<Slider>().CurrentValue))
                {
                    if (colorBlind)
                    {
                        Circle.Draw(IsShroomed(place) ? SharpDX.Color.Red : SharpDX.Color.YellowGreen, 100, place);
                    }
                    else
                    {
                        Circle.Draw(IsShroomed(place) ? SharpDX.Color.Red : SharpDX.Color.LightGreen, 100, place);
                    }
                }
            }
        }
    }
}