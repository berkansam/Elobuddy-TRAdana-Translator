using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using KA_Kayle.DMGHandler;
using Color = System.Drawing.Color;

// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace KA_Kayle
{
    public static class Config
    {
        private static readonly string MenuName = "KA " + Player.Instance.ChampionName;

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("KA'ya hoşgeldin :) , çevirmen Tradana" + Player.Instance.ChampionName);
            Modes.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu SpellsMenu, FarmMenu, DrawMenu;
            public static readonly Menu SettingsMenu,MiscMenu;

            static Modes()
            {
                SpellsMenu = Menu.AddSubMenu("::Büyü Menüsü::");
                Combo.Initialize();
                Harass.Initialize();

                FarmMenu = Menu.AddSubMenu("::Farm::");
                LaneClear.Initialize();
                LastHit.Initialize();

                MiscMenu = Menu.AddSubMenu("::Ek::");
                Misc.Initialize();

                DrawMenu = Menu.AddSubMenu("::Drawings::");
                Draw.Initialize();

                DrawMenu = Menu.AddSubMenu("::Ayarlar::");
                Settings.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                static Combo()
                {
                    // Initialize the menu values
                    SpellsMenu.AddGroupLabel("Kombo Büyüleri");
                    _useQ = SpellsMenu.Add("comboQ", new CheckBox("Q Kullan"));
                    _useW = SpellsMenu.Add("comboW", new CheckBox("W Kullan"));
                    _useE = SpellsMenu.Add("comboE", new CheckBox("E Kullan"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;
                private static readonly Slider _manaHarass;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static int ManaHarass
                {
                    get { return _manaHarass.CurrentValue; }
                }

                static Harass()
                {
                    SpellsMenu.AddGroupLabel("Dürtme Büyüleri:");
                    _useQ = SpellsMenu.Add("harassQ", new CheckBox("Q Kullan"));
                    _useE = SpellsMenu.Add("harassE", new CheckBox("E Kullan ?"));
                    SpellsMenu.AddGroupLabel("Dürtme  Ayarları:");
                    _manaHarass = SpellsMenu.Add("harassMana", new Slider("Manam şundan fazlaysa dürterken büyü kullanabilirsin ({0}).", 30));
                }

                public static void Initialize()
                {
                }
            }

            public static class LaneClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;
                private static readonly Slider _laneMana;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static int LaneMana
                {
                    get { return _laneMana.CurrentValue; }
                }

                static LaneClear()
                {
                    FarmMenu.AddGroupLabel("LaneTemizleme Büyüleri:");
                    _useQ = FarmMenu.Add("laneclearQ", new CheckBox("Q Kullan ?"));
                    _useE = FarmMenu.Add("laneclearE", new CheckBox("E Kullan ?"));
                    FarmMenu.AddGroupLabel("LaneClear Settings:");
                    _laneMana = FarmMenu.Add("laneMana", new Slider("Eğer manam şundan yüksekse büyü kullanabilirsin ({0}).", 30));
                }

                public static void Initialize()
                {
                }
            }

            public static class LastHit
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;
                private static readonly Slider _lastMana;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }


                public static int LastMana
                {
                    get { return _lastMana.CurrentValue; }
                }

                static LastHit()
                {
                    FarmMenu.AddGroupLabel("Son Vuruş Büyüleri:");
                    _useQ = FarmMenu.Add("lasthitQ", new CheckBox("Q Kullan ?"));
                    _useE = FarmMenu.Add("lasthitE", new CheckBox("E Kullan?"));
                    FarmMenu.AddGroupLabel("LastHit Settings:");
                    _lastMana = FarmMenu.Add("lastMana", new Slider("Eğer manam şundan yüksekse büyü kullanabilirsin ({0}).", 30));
                }

                public static void Initialize()
                {
                }
            }

            public static class Misc
            {
                private static readonly CheckBox _antiGapCloserSpell;
                private static readonly Slider _miscMana;
                //R
                private static readonly CheckBox _useR;
                private static readonly Slider _useRHP;
                private static readonly Slider _useRMana;
                //W
                private static readonly CheckBox _useW;
                private static readonly Slider _useWHP;
                private static readonly Slider _useWMana;

                public static bool AntiGapCloser
                {
                    get { return _antiGapCloserSpell.CurrentValue; }
                }

                public static int MiscMana
                {
                    get { return _miscMana.CurrentValue; }
                }
                //R
                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                public static int UseRHealth
                {
                    get { return _useRHP.CurrentValue; }
                }

                public static int UseRMana
                {
                    get { return _useRMana.CurrentValue; }
                }
                //W
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static int UseWHealth
                {
                    get { return _useWHP.CurrentValue; }
                }

                public static int UseWMana
                {
                    get { return _useWMana.CurrentValue; }
                }

                static Misc()
                {
                    // Initialize the menu values
                    MiscMenu.AddGroupLabel("Ek");
                    _antiGapCloserSpell = MiscMenu.Add("gapcloserQ", new CheckBox("Q'yu Gapcloser'da Kullan ?"));
                    _miscMana = MiscMenu.Add("miscMana", new Slider("Gerekli mana ?", 20));
                    MiscMenu.AddGroupLabel("R Ayarları");
                    _useR = MiscMenu.Add("useRMisc", new CheckBox("R Kullan?"));
                    _useRHP = MiscMenu.Add("rHealth", new Slider("Herhangi kişinin canı şundan azsa ({0}) R Kullan.", 20));
                    _useRMana = MiscMenu.Add("rmana", new Slider("R için Gereken Mana ({0}).", 10));
                    foreach (var ally in EntityManager.Heroes.Allies)
                    {
                        MiscMenu.Add("allyUseR" + ally.ChampionName, new CheckBox("Use R to save " + ally.ChampionName +" (" +ally.Name + ") ?"));
                    }
                    MiscMenu.AddGroupLabel("W Ayarları");
                    _useW = MiscMenu.Add("useWMisc", new CheckBox("W Kullan ?"));
                    _useWHP = MiscMenu.Add("wHealth", new Slider("Herhangi kişinin canı şundan azsa ({0}) W kullan.", 50));
                    _useWMana = MiscMenu.Add("wMana", new Slider("Gereken Mana ({0}).", 30));
                    foreach (var ally in EntityManager.Heroes.Allies)
                    {
                        MiscMenu.Add("allyUseW" + ally.ChampionName, new CheckBox("Use W to heal " + ally.ChampionName + " (" + ally.Name + ") ?"));
                    }
                }

                public static void Initialize()
                {
                }
            }

            public static class Draw
            {
                private static readonly CheckBox _drawReady;
                private static readonly CheckBox _drawHealth;
                private static readonly CheckBox _drawPercent;
                private static readonly CheckBox _drawQ;
                private static readonly CheckBox _drawW;
                private static readonly CheckBox _drawE;
                private static readonly CheckBox _drawR;
                //Color Config
                private static readonly ColorConfig _qColor;
                private static readonly ColorConfig _wColor;
                private static readonly ColorConfig _eColor;
                private static readonly ColorConfig _rColor;
                private static readonly ColorConfig _healthColor;

                //CheckBoxes
                public static bool DrawReady
                {
                    get { return _drawReady.CurrentValue; }
                }

                public static bool DrawHealth
                {
                    get { return _drawHealth.CurrentValue; }
                }

                public static bool DrawPercent
                {
                    get { return _drawPercent.CurrentValue; }
                }

                public static bool DrawQ
                {
                    get { return _drawQ.CurrentValue; }
                }

                public static bool DrawW
                {
                    get { return _drawW.CurrentValue; }
                }

                public static bool DrawE
                {
                    get { return _drawE.CurrentValue; }
                }

                public static bool DrawR
                {
                    get { return _drawR.CurrentValue; }
                }
                //Colors
                public static Color HealthColor
                {
                    get { return _healthColor.GetSystemColor(); }
                }

                public static SharpDX.Color QColor
                {
                    get { return _qColor.GetSharpColor(); }
                }

                public static SharpDX.Color WColor
                {
                    get { return _wColor.GetSharpColor(); }
                }

                public static SharpDX.Color EColor
                {
                    get { return _eColor.GetSharpColor(); }
                }
                public static SharpDX.Color RColor
                {
                    get { return _rColor.GetSharpColor(); }
                }

                static Draw()
                {
                    DrawMenu.AddGroupLabel("Spell drawings Settings :");
                    _drawReady = DrawMenu.Add("drawOnlyWhenReady", new CheckBox("Draw the spells only if they are ready ?"));
                    _drawHealth = DrawMenu.Add("damageIndicatorDraw", new CheckBox("Draw damage indicator ?"));
                    _drawPercent = DrawMenu.Add("percentageIndicatorDraw", new CheckBox("Draw damage percentage ?"));
                    DrawMenu.AddSeparator(1);
                    _drawQ = DrawMenu.Add("qDraw", new CheckBox("Draw Q spell range ?"));
                    _drawW = DrawMenu.Add("wDraw", new CheckBox("Draw W spell range ?"));
                    _drawE = DrawMenu.Add("eDraw", new CheckBox("Draw E spell range ?"));
                    _drawR = DrawMenu.Add("rDraw", new CheckBox("Draw R spell range ?"));

                    _healthColor = new ColorConfig(DrawMenu, "healthColorConfig", Color.Orange, "Color Damage Indicator:");
                    _qColor = new ColorConfig(DrawMenu, "qColorConfig", Color.Blue, "Color Q:");
                    _wColor = new ColorConfig(DrawMenu, "wColorConfig", Color.Red, "Color W:");
                    _eColor = new ColorConfig(DrawMenu, "eColorConfig", Color.DeepPink, "Color E:");
                    _rColor = new ColorConfig(DrawMenu, "rColorConfig", Color.Yellow, "Color R:");
                }

                public static void Initialize()
                {
                }
            }

            public static class Settings
            {
                //Danger
                private static readonly Slider EnemyRange;
                private static readonly Slider EnemySlider;
                private static readonly CheckBox Spells;
                private static readonly CheckBox Skillshots;
                private static readonly CheckBox AAs;


                public static int RangeEnemy
                {
                    get { return EnemyRange.CurrentValue; }
                }

                public static int EnemyCount
                {
                    get { return EnemySlider.CurrentValue; }
                }

                public static bool ConsiderSpells
                {
                    get { return Spells.CurrentValue; }
                }

                public static bool ConsiderSkillshots
                {
                    get { return Skillshots.CurrentValue; }
                }

                public static bool ConsiderAttacks
                {
                    get { return AAs.CurrentValue; }
                }

                static Settings()
                {
                    SettingsMenu.AddGroupLabel("Danger Settings");
                    EnemySlider = SettingsMenu.Add("minenemiesinrange", new Slider("Min enemies in the range determined below", 1, 1, 5));
                    EnemyRange = SettingsMenu.Add("minrangeenemy", new Slider("Enemies must be in ({0}) range to be in danger", 1000, 600, 2500));
                    Spells = SettingsMenu.Add("considerspells", new CheckBox("Consider spells ?"));
                    Skillshots = SettingsMenu.Add("considerskilshots", new CheckBox("Consider SkillShots ?"));
                    AAs = SettingsMenu.Add("consideraas", new CheckBox("Consider Auto Attacks ?"));
                    SettingsMenu.AddSeparator();
                    SettingsMenu.AddGroupLabel("Dangerous Spells");
                    foreach (var spell in DangerousSpells.Spells.Where(x => EntityManager.Heroes.Enemies.Any(b => b.Hero == x.Hero)))
                    {
                        SettingsMenu.Add(spell.Hero.ToString() + spell.Slot, new CheckBox(spell.Hero + " - " + spell.Slot + ".", spell.DefaultValue));
                    }
                }

                public static void Initialize()
                {
                }
            }
        }
    }
}