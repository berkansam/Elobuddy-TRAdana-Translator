﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Spells;

namespace AimBot
{
    internal class Program
    {
        private static void Main()
        {
            Loading.OnLoadingComplete += delegate
            {
                Initialize();
            };
        }

        internal static Menu _menu;
        internal static AIHeroClient MyHero
        {
            get { return Player.Instance; }
        }
        internal static bool Enabled
        {
            get { return _menu["Enable"].Cast<KeyBind>().CurrentValue; }
        }
        internal static readonly Dictionary<SpellSlot, ComboBox> SlotsComboBox = new Dictionary<SpellSlot, ComboBox>();
        internal static readonly Dictionary<SpellSlot, Slider> SlotsSlider = new Dictionary<SpellSlot, Slider>();
        internal static List<SpellInfo> _spells;
        private static void Initialize()
        {
            _spells = SpellDatabase.GetSpellInfoList(MyHero.BaseSkinName);
            if (_spells.Count > 0)
            {
                var hasChargeableSpell = _spells.Any(i => i.Chargeable);
                YasuoWallManager.Initialize();
                _menu = MainMenu.AddMenu("AimBot", "AimBot 6.4.0 " + MyHero.BaseSkinName);
                _menu.Add("Enable", new KeyBind("Açık / Kapalı", true, KeyBind.BindTypes.PressToggle, 'K'));
                var slots = new HashSet<SpellSlot>();
                foreach (var info in _spells)
                {
                    slots.Add(info.Slot);
                }
                foreach (var slot in slots)
                {
                    _menu.AddGroupLabel(slot + " Ayarlar");
                    SlotsComboBox[slot] = _menu.Add(slot + "ComboBox", new ComboBox("Kullan", new List<string> { "Asla", "Komboda", "Her zaman" }, 2));
                    SlotsSlider[slot] = _menu.Add(slot + "HitChancePercent", new Slider("Tutma oranı yüzde", 60));
                }
                Game.OnTick += Game_OnTick;
                if (hasChargeableSpell)
                {
                    Spellbook.OnCastSpell += delegate (Spellbook sender, SpellbookCastSpellEventArgs args)
                    {
                        if (sender.Owner.IsMe)
                        {
                            if (_spells.Any(i => i.Slot == args.Slot && i.Chargeable))
                            {
                                _lastChargeTime = Core.GameTickCount;
                            }
                        }
                    };
                }
                Drawing.OnDraw += delegate
                {
                    Drawing.DrawText(MyHero.Position.WorldToScreen(), Color.White, "AimBot " + (Enabled ? "Açık" : "Kapalı"), 10);
                };
            }
        }

        private static int _lastChargeTime;
        private static void Game_OnTick(EventArgs args)
        {
            if (MyHero.IsDead || !Enabled || !Orbwalker.CanMove)
            {
                return;
            }
            foreach (var slot in SlotsComboBox.Where(i => Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) ? i.Value.CurrentValue >= 1 : i.Value.CurrentValue == 2).Select(i => i.Key))
            {
                Cast(slot);
            }
        }

        private static bool IsCharging
        {
            get { return MyHero.Spellbook.IsCharging || Core.GameTickCount - _lastChargeTime < 300 + Game.Ping / 2; }
        }

        private static void Cast(SpellSlot slot)
        {
            var first = _spells.FirstOrDefault(spell => spell.Slot == slot && (string.IsNullOrEmpty(spell.SpellName) || MyHero.Spellbook.GetSpell(slot).Name == spell.SpellName));
            if (first != null)
            {
                var allowedCollisionCount = first.Collisions.Length > 0 ? 0 : int.MaxValue;
                var collidesWithWall = first.Collisions.Contains(CollisionType.YasuoWall);
                SpellBase spell = null;
                switch (first.Type)
                {
                    case EloBuddy.SDK.Spells.SpellType.Self:
                        spell = new SpellBase(slot, SpellType.Self, first.Range) { Delay = first.Delay + first.MissileFixedTravelTime, Speed = first.MissileSpeed, AllowedCollisionCount = allowedCollisionCount, CollidesWithYasuoWall = collidesWithWall };
                        break;
                    case EloBuddy.SDK.Spells.SpellType.Circle:
                        spell = new SpellBase(slot, SpellType.Circular, first.Range) { Delay = first.Delay + first.MissileFixedTravelTime, Speed = first.MissileSpeed, AllowedCollisionCount = allowedCollisionCount, CollidesWithYasuoWall = collidesWithWall, Width = first.Radius };
                        break;
                    case EloBuddy.SDK.Spells.SpellType.Line:
                        spell = new SpellBase(slot, SpellType.Linear, first.Range) { Delay = first.Delay + first.MissileFixedTravelTime, Speed = first.MissileSpeed, AllowedCollisionCount = allowedCollisionCount, CollidesWithYasuoWall = collidesWithWall, Width = first.Radius };
                        break;
                    case EloBuddy.SDK.Spells.SpellType.Cone:
                        spell = new SpellBase(slot, SpellType.Cone, first.Range) { Delay = first.Delay + first.MissileFixedTravelTime, Speed = first.MissileSpeed, AllowedCollisionCount = allowedCollisionCount, CollidesWithYasuoWall = collidesWithWall, Width = first.Radius };
                        break;
                    case EloBuddy.SDK.Spells.SpellType.Ring:
                        break;
                    case EloBuddy.SDK.Spells.SpellType.MissileLine:
                        spell = new SpellBase(slot, SpellType.Linear, first.Range) { Delay = first.Delay + first.MissileFixedTravelTime, Speed = first.MissileSpeed, AllowedCollisionCount = allowedCollisionCount, CollidesWithYasuoWall = collidesWithWall, Width = first.Radius };
                        break;
                    case EloBuddy.SDK.Spells.SpellType.MissileAoe:
                        spell = new SpellBase(slot, SpellType.Circular, first.Range) { Delay = first.Delay + first.MissileFixedTravelTime, Speed = first.MissileSpeed, AllowedCollisionCount = allowedCollisionCount, CollidesWithYasuoWall = collidesWithWall, Width = first.Radius };
                        break;
                }
                if (spell != null)
                {
                    if (first.Chargeable)
                    {
                        if (IsCharging)
                        {
                            var percentageGrowth =
                                Math.Min(
                                    (Core.GameTickCount - _lastChargeTime - first.CastRangeGrowthStartTime) /
                                    first.CastRangeGrowthDuration, 1);
                            spell.Range = (first.CastRangeGrowthMax - first.CastRangeGrowthMin) * percentageGrowth + first.CastRangeGrowthMin;
                            spell.ReleaseCast();
                        }
                        else
                        {
                            spell.StartCast();
                        }
                    }
                    else if (!IsCharging)
                    {
                        spell.Cast();
                    }
                }
            }
        }
    }
}
