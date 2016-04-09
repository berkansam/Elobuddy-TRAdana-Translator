using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;

namespace VayneBuddy
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Game_OnStart;
        }

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static Spell.Ranged Q;
        public static Spell.Targeted E;
        public static Spell.Active R;

        public static Menu Menu,
            ComboMenu,
            HarassMenu,
            FarmMenu,
            CondemnMenu,
            DrawMenu,
            InterruptorMenu,
            GapCloserMenu,
            CondemnPriorityMenu;
        
        public static string[] DangerSliderValues = {"Low", "Medium", "High"};
        public static string[] PriorityValues = {"Very Low", "Low", "Medium", "High", "Very High"};
        public static List<Vector2> Points = new List<Vector2>();

        private static void Game_OnStart(EventArgs args)
        {
            if (!_Player.ChampionName.ToLower().Contains("vayne")) return;

            Q = new Spell.Skillshot(SpellSlot.Q, int.MaxValue, SkillShotType.Linear);
            E = new Spell.Targeted(SpellSlot.E, 590);
            Condemn.ESpell = new Spell.Skillshot(SpellSlot.E, 590, SkillShotType.Linear, 250, 1250);
            R = new Spell.Active(SpellSlot.R);


            Menu = MainMenu.AddMenu("Vayne Buddy", "vBuddy");

            Menu.AddGroupLabel("Vayne Buddy");
            Menu.AddLabel("Version: " + "0.0.0.2");
            Menu.AddSeparator();
            Menu.AddLabel("By Fluxy ;)");
            Menu.AddSeparator();
            Menu.AddLabel("p.s. imeh is faggot");

            ComboMenu = Menu.AddSubMenu("Combo", "vBuddyCombo");
            ComboMenu.AddGroupLabel("Combo Ayarlarý");
            ComboMenu.Add("useQCombo", new CheckBox("Kullan Q"));
            ComboMenu.Add("useQKite", new CheckBox("Qyu kite için kullan", false));
            ComboMenu.Add("useECombo", new CheckBox("Kullan E (Execute)"));
            ComboMenu.AddLabel("R Settings");
            ComboMenu.Add("useRCombo", new CheckBox("Kullan R", false));
            ComboMenu.Add("noRUnderTurret", new CheckBox("Düþman kule altýnda R Kullanma sakýn"));

            CondemnPriorityMenu = Menu.AddSubMenu("Auto Condemn", "vBuddyCondemnPriority");
            CondemnPriorityMenu.AddGroupLabel("E atma önceliði");
            foreach (var enem in ObjectManager.Get<AIHeroClient>().Where(a => a.IsEnemy))
            {
                var champValue = CondemnPriorityMenu.Add(enem.ChampionName + "priority",
                    new Slider(enem.ChampionName + ": ", 1, 1, 5));
                var enem1 = enem;
                champValue.OnValueChange += delegate
                {
                    champValue.DisplayName = enem1.ChampionName + ": " + PriorityValues[champValue.CurrentValue];
                };
                champValue.DisplayName = enem1.ChampionName + ": " + PriorityValues[champValue.CurrentValue];
            }
            CondemnPriorityMenu.AddSeparator();
            var sliderValue = CondemnPriorityMenu.Add("minSliderAutoCondemn",
                new Slider("Oto E için öncelik: ", 2, 1, 5));
            sliderValue.OnValueChange += delegate
            {
                sliderValue.DisplayName = "Min Priority for Auto Condemn: " + PriorityValues[sliderValue.CurrentValue];
            };
            sliderValue.DisplayName = "Min Priority for Auto Condemn: " + PriorityValues[sliderValue.CurrentValue];
            CondemnPriorityMenu.Add("autoCondemnToggle",
                new KeyBind("Otomatik E", false, KeyBind.BindTypes.PressToggle, 'H'));
            CondemnPriorityMenu.AddSeparator();

            CondemnMenu = Menu.AddSubMenu("Condemn", "vBuddyCondemn");
            CondemnMenu.AddGroupLabel("Condemn Ayarlarý");
            CondemnMenu.AddSeparator();
            CondemnMenu.Add("pushDistance", new Slider("E atma mesafesi", 410, 350, 420));
            CondemnMenu.Add("condemnPercent", new Slider("E yüzdesi", 33, 1));
            CondemnMenu.AddSeparator();
            CondemnMenu.AddLabel("Aktif Mod Ayarlarý");
            CondemnMenu.Add("smartVsCheap",
                new CheckBox("On (SMART CONDEMN (saves fps)) / OFF (360 degree check)", true));
            CondemnMenu.AddSeparator();
            CondemnMenu.Add("condemnCombo", new CheckBox("Komboda E Kullan", true));
            CondemnMenu.Add("condemnComboTrinket", new CheckBox("Trinket Bush After E", true));
            CondemnMenu.Add("condemnHarass", new CheckBox("Dürtmede E kullan", true));

            HarassMenu = Menu.AddSubMenu("Harass", "vBuddyHarass");
            HarassMenu.AddGroupLabel("Dürtme Ayarlarý");
            HarassMenu.Add("useQHarass", new CheckBox("Kullan Q", true));

            FarmMenu = Menu.AddSubMenu("Farming", "vBuddyFarm");
            FarmMenu.AddGroupLabel("Farming Ayarlarý");
            FarmMenu.Add("onlyTumbleToCursor", new CheckBox("Only Tumble To Cursor", false));
            FarmMenu.AddLabel("Last Hit");
            FarmMenu.Add("useQLastHit", new CheckBox("Kullan Q son", true));
            FarmMenu.AddLabel("WaveClear");
            FarmMenu.Add("useQWaveClear", new CheckBox("Kullan Q lanetemizleme", true));

            DrawMenu = Menu.AddSubMenu("Misc Menu", "vBuddyMisc");
            DrawMenu.AddGroupLabel("Göster Ayarlarý");
            DrawMenu.Add("drawERange", new CheckBox("Göster E Menzili", false));
            DrawMenu.Add("condemnVisualiser", new CheckBox("Göster Condemn(e)", false));
            DrawMenu.Add("drawStacks", new CheckBox("Göster W Stacks", false));
            DrawMenu.AddLabel("Misc");
            DrawMenu.Add("wallJumpKey", new KeyBind("Duvardan taklayla geç", false, KeyBind.BindTypes.HoldActive, 'Z'));
            DrawMenu.Add("condemnNextAA", new KeyBind("AAdan sonra E kullan", false, KeyBind.BindTypes.PressToggle, 'E'));
            DrawMenu.AddLabel("Anti-Champions");
            DrawMenu.Add("antiKalista", new CheckBox("Anti-Kalista"));
            DrawMenu.Add("antiRengar", new CheckBox("Anti-Rengar"));

            InterruptorMenu = Menu.AddSubMenu("Interrupter", "InterruptorvBuddy");
            InterruptorMenu.AddGroupLabel("Interrupter Menu");
            InterruptorMenu.Add("enableInterrupter", new CheckBox("Aktif Interrupter"));
            InterruptorMenu.AddSeparator();
            var dangerSlider = InterruptorMenu.Add("dangerLevel", new Slider("Tehlike seviyesi: ", 3, 1, 3));
            var dangerSliderDisplay = InterruptorMenu.Add("dangerLevelDisplay",
                new Label("Danger Level: " + DangerSliderValues[dangerSlider.Cast<Slider>().CurrentValue - 1]));
            dangerSlider.Cast<Slider>().OnValueChange += delegate
            {
                dangerSliderDisplay.Cast<Label>().DisplayName =
                    "Danger Level: " + DangerSliderValues[dangerSlider.Cast<Slider>().CurrentValue - 1];
            };

            GapCloserMenu = Menu.AddSubMenu("Anti-GapClosers", "gapClosersvBuddy");
            GapCloserMenu.AddGroupLabel("Anti-GapCloser Menu");
            GapCloserMenu.Add("enableGapCloser", new CheckBox("Aktif Anti-GapCloser"));

            Orbwalker.OnPreAttack += Events.Orbwalker_OnPreAttack;
            Game.OnUpdate += Game_OnUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
            Gapcloser.OnGapcloser += Events.Gapcloser_OnGapCloser;
            Interrupter.OnInterruptableSpell += Events.Interrupter_OnInterruptableSpell;
            Obj_AI_Base.OnProcessSpellCast += AIHeroClient_OnProcessSpellCast;
            Obj_AI_Base.OnSpellCast += Obj_AI_Base_OnSpellCast;
            Obj_AI_Base.OnBasicAttack += Events.ObjAiBaseOnOnBasicAttack;
            GameObject.OnCreate += Events.GameObject_OnCreate;
        }

        private static void Obj_AI_Base_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;
            if (args.SData.IsAutoAttack())
            {
                var target = (Obj_AI_Base) args.Target;

                if (target is AIHeroClient)
                {
                    if (DrawMenu["condemnNextAA"].Cast<KeyBind>().CurrentValue && E.IsReady())
                    {
                        E.Cast(target);
                        DrawMenu["condemnNextAA"].Cast<KeyBind>().CurrentValue = false;
                    }
                    if (target.IsValidTarget() && Q.IsReady() &&
                        (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) &&
                        ComboMenu["useQCombo"].Cast<CheckBox>().CurrentValue ||
                         Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass) &&
                         HarassMenu["useQHarass"].Cast<CheckBox>().CurrentValue))
                    {
                        var pos = (_Player.Position.Extend(Game.CursorPos, 300).Distance(target) <=
                                   _Player.GetAutoAttackRange(target) &&
                                   _Player.Position.Extend(Game.CursorPos, 300).Distance(target) > 100
                            ? Game.CursorPos
                            : (_Player.Position.Extend(target.Position, 300).Distance(target) < 100)
                                ? target.Position
                                : new Vector3());

                        if (pos.IsValid())
                        {
                            Player.CastSpell(SpellSlot.Q, pos);
                        }
                    }

                    if ( ComboMenu["useQKite"].Cast<CheckBox>().CurrentValue &&
                        EntityManager.Heroes.Enemies.Any(
                            a => a.IsMelee && a.Distance(Player.Instance) < a.GetAutoAttackRange(Player.Instance)))
                    {
                        Player.CastSpell(SpellSlot.Q,
                            target.Position.Extend(Player.Instance.Position,
                                target.Position.Distance(Player.Instance) + 300).To3D());
                    }
                }

                if ((Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit) && FarmMenu["useQLastHit"].Cast<CheckBox>().CurrentValue || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) && FarmMenu["useQWaveClear"].Cast<CheckBox>().CurrentValue) && Q.IsReady())
                {
                    var source =
                        EntityManager.MinionsAndMonsters.EnemyMinions
                            .Where(
                                a =>  a.NetworkId != target.NetworkId && a.Distance(Player.Instance) < 300 + Player.Instance.GetAutoAttackRange(a) &&
                                    Prediction.Health.GetPrediction(a, (int) Player.Instance.AttackDelay) < Player.Instance.GetAutoAttackDamage(a, true) + Damages.QDamage(a))
                            .OrderBy(a => a.Health)
                            .FirstOrDefault();

                    if (source == null || Player.Instance.Position.Extend(Game.CursorPos, 300).Distance(source) >
                        Player.Instance.GetAutoAttackRange(source) &&
                        FarmMenu["onlyTumbleToCursor"].Cast<CheckBox>().CurrentValue) return;
                    Orbwalker.ForcedTarget = source;
                    Player.CastSpell(SpellSlot.Q, Player.Instance.Position.Extend(Game.CursorPos, 300).Distance(source) <= Player.Instance.GetAutoAttackRange(source) ? Game.CursorPos : source.Position);
                }
            }
        }

        private static void AIHeroClient_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;
            if (args.SData.Name == Player.GetSpell(SpellSlot.R).Name)
            {
                Events.LastR = Environment.TickCount + new[] {8000, 10000, 12000}[R.Level - 1];
            }
            if (args.SData.Name.ToLower().Contains("vaynetumble"))
            {
                Orbwalker.ResetAutoAttack();
            }
            if (args.SData.Name == Player.GetSpell(SpellSlot.E).Name)
            {
                DrawMenu["condemnNextAA"].Cast<KeyBind>().CurrentValue = false;
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            WallQ.Drawing_OnDraw();

            if (DrawMenu["drawStacks"].Cast<CheckBox>().CurrentValue && Events.AAedTarget != null)
            {
                var color = new [] {Color.White, Color.Aqua}[Events.AaStacks - 1];
                new Circle() { Color = color, Radius = 200 }.Draw(Events.AAedTarget.Position);
            }
            if (DrawMenu["drawERange"].Cast<CheckBox>().CurrentValue)
            {
                new Circle() { Color = Color.White, Radius = E.Range }.Draw(_Player.Position);
            }
            if (DrawMenu["condemnVisualiser"].Cast<CheckBox>().CurrentValue)
            {
                foreach (var point in Points)
                {
                    new Circle() {Color = (NavMesh.GetCollisionFlags(point.To3D()).HasFlag(CollisionFlags.Wall) ||
                            NavMesh.GetCollisionFlags(point.To3D()).HasFlag(CollisionFlags.Building)) ? Color.Blue : Color.Red, Radius = 10}.Draw(point.To3D());
                }
            }
        }

        private static void Game_OnUpdate(EventArgs args)
        {
                if (Orbwalker.ForcedTarget == null || Orbwalker.ForcedTarget.Health <= 0 || Orbwalker.ForcedTarget.IsDead ||
                    Orbwalker.ForcedTarget.Distance(Player.Instance) > (Player.Instance.IsDashing()
                    ? Player.Instance.GetAutoAttackRange() + 300
                    : Player.Instance.GetAutoAttackRange()))
                {
                    Orbwalker.ForcedTarget = null;
                }

            if (Events.AAedTarget == null || Events.LastAa + 3500 + 400 <= Environment.TickCount || Events.AAedTarget.IsDead || !Events.AAedTarget.HasBuff("vaynesilvereddebuff") && (Events.LastAa + 1000 < Environment.TickCount))
            {
                Events.AAedTarget = null;
                Events.AaStacks = 0;
            }

            if (DrawMenu["wallJumpKey"].Cast<KeyBind>().CurrentValue)
            {
                WallQ.WallTumble();
            }
            else
            {
                Orbwalker.DisableMovement = false;
            }
            if (CondemnPriorityMenu["autoCondemnToggle"].Cast<KeyBind>().CurrentValue)
            {
                var condemnTarget = Condemn.CondemnTarget();
                if (condemnTarget != null)
                {
                    E.Cast(condemnTarget);
                }
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                States.Combo();
            }
            else if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                States.Harass();
            }
        }

    }
}
