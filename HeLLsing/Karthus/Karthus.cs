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

namespace Karthus
{
    public class Karthus
    {
        private static Karthus _instance;
        public static Karthus Instance
        {
            get { return _instance ?? (_instance = new Karthus()); }
        }

        internal static void Main(string[] args)
        {
            // Wait till the game has fully loaded
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            // Check for the correct champion
            if (Player.Instance.Hero != Champion.Karthus)
            {
                return;
            }

            // Initialize the addons
            Instance.Initialize();
        }

        public Menu Menu { get; private set; }
        public Menu DrawingMenu { get; private set; }
        public SpellHandler SpellHandler { get; private set; }
        public ModeHandler ModeHandler { get; private set; }
        public bool Initialized { get; private set; }

        public bool IsDead
        {
            get { return Player.Instance.Buffs.Any(o => o.DisplayName == "KarthusDefile"); }
        }

        private Karthus()
        {
            // Initialize properties
            Menu = MainMenu.AddMenu("Karthus", "karthus", "Karthus - King Killsteal");
            SpellHandler = new SpellHandler(this,
                new Spell.Skillshot(SpellSlot.Q, 875, SkillShotType.Circular, spellSpeed: int.MaxValue, spellWidth: 160 * 2, castDelay: 750),
                new Spell.Skillshot(SpellSlot.W, 1000, SkillShotType.Circular, spellWidth: 100),
                new Spell.Active(SpellSlot.E, 550),
                new Spell.Active(SpellSlot.R));

            // Setup global menu
            Menu.AddGroupLabel("Hoşgeldin Karthus 3K!");
            Menu.AddLabel("Sen bu addonu kendine göre düzenleyebilirsin");
            Menu.AddLabel("Genel Ayarlar altında");
            Menu.AddLabel("Çeviri TRAdana");

            Menu.AddSeparator();
            Menu.AddGroupLabel("Genel Ayarlar");
            Menu.Add("ComboWhileDead", new CheckBox("Ölünce kombo yap"));

            // Setup mode handler
            ModeHandler = new ModeHandler(this);

            // Setup drawing menu
            DrawingMenu = Menu.AddSubMenu("Drawings");
            DrawingMenu.AddGroupLabel("Bilgi");
            DrawingMenu.AddLabel("Sen büyü menzillerini isteğine göre aktif etmelisin");
            DrawingMenu.AddSeparator();
            DrawingMenu.AddGroupLabel("Büyü Menzilleri");
            DrawingMenu.Add("Q", new CheckBox("Göster Q Menzili"));
            DrawingMenu.Add("E", new CheckBox("Göster E Menzili", false));
            DrawingMenu.Add("W", new CheckBox("Göster W Menzili"));
            DrawingMenu.Add("W2", new CheckBox("Göster W en fazla menzili"));

            // Listen to required events
            Game.OnTick += OnTick;
            Drawing.OnDraw += OnDraw;
        }

        private void OnDraw(EventArgs args)
        {
            // Draw the spell ranges
            if (SpellHandler.Q.IsLearned && DrawingMenu.Get<CheckBox>("Q").CurrentValue)
            {
                Circle.Draw(SharpDX.Color.Red, SpellHandler.Q.Range, Player.Instance);
            }
            if (SpellHandler.W.IsLearned && DrawingMenu.Get<CheckBox>("W").CurrentValue)
            {
                Circle.Draw(SharpDX.Color.PaleVioletRed, SpellHandler.W.Range, Player.Instance);
            }
            if (SpellHandler.W.IsLearned && DrawingMenu.Get<CheckBox>("W2").CurrentValue)
            {
                Circle.Draw(SharpDX.Color.PaleVioletRed, SpellHandler.WallOfPainMaxRange, Player.Instance);
            }
            if (SpellHandler.E.IsLearned && DrawingMenu.Get<CheckBox>("E").CurrentValue)
            {
                Circle.Draw(SharpDX.Color.OrangeRed, SpellHandler.E.Range, Player.Instance);
            }
        }

        private void OnTick(EventArgs args)
        {
            if (!Player.Instance.IsDead)
            {
                // Execute modes
                ModeHandler.OnTick();
            }
        }

        public T GetGlobal<T>(string indentifier) where T : ValueBase
        {
            T global = null;
            foreach (var menu in new [] { Menu }.Concat(ModeHandler.Modes.Select(o => o.Menu)))
            {
                global = menu.Get<T>(indentifier);
                if (global != null)
                {
                    break;
                }
            }
            return global;
        }

        public void Initialize()
        {
            // Only initialize once
            if (Initialized)
            {
                return;
            }
            Initialized = true;
        }
    }
}
