using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;

// ReSharper disable SwitchStatementMissingSomeCases

namespace Mario_sTemplate
{
    internal class Spells
    {
        #region Consts
        public const DamageType dmgType = DamageType.Physical;
        public const int highestRange = 625;
        #endregion Consts

        public static void InitSpells()
        {
            SpellsSettings();
        }
        public static Spell.Active Q;
        public static Spell.Active W;
        public static Spell.Targeted E;
        public static Spell.Active R;
        public static List<Spell.SpellBase> SpellList = new List<Spell.SpellBase>();

        private static void SpellsSettings()
        {
            Q = new Spell.Active(SpellSlot.Q, 300);
            SpellList.Add(Q);
            W = new Spell.Active(SpellSlot.W, 175);
            SpellList.Add(W);
            E = new Spell.Targeted(SpellSlot.E, 635);
            SpellList.Add(E);
            R = new Spell.Active(SpellSlot.R, 320);
            SpellList.Add(R);
        }

        #region Damages
        public static float GetDamage(SpellSlot slot, Obj_AI_Base target)
        {
            var lvl = Player.Instance.Spellbook.GetSpell(slot).Level - 1;
            var AD = Player.Instance.FlatPhysicalDamageMod;
            var AP = Player.Instance.FlatMagicDamageMod;
            var dmg = 0f;

            switch (slot)
            {
                case SpellSlot.Q:
                    if (Q.IsReady())
                    {
                        dmg += new float[] { 30, 60, 90, 120, 150 }[lvl] + 0.1f * AD + Player.Instance.GetAutoAttackDamage(target);
                    }
                    break;
                case SpellSlot.W:
                    if (W.IsReady())
                    {
                        dmg += new float[] { 70, 115, 160, 205, 250 }[lvl] + 0.6f * AP;
                    }
                    break;
                case SpellSlot.E:
                    if (E.IsReady())
                    {
                        dmg += new float[] { 60, 105, 150, 195, 240 }[lvl] + 0.8f * AD;
                    }
                    break;
                case SpellSlot.R:
                    if (R.IsReady())
                    {
                        dmg += (new float[] { 20, 110, 200 }[lvl] + 1.1f * AD) * 3.5f;
                    }
                    break;
            }
            return Player.Instance.CalculateDamageOnUnit(target, dmgType, dmg - 10);
        }

        public static float GetTotalDamage(Obj_AI_Base target)
        {
            var dmg = GetDamage(SpellSlot.Q, target) + GetDamage(SpellSlot.W, target) + GetDamage(SpellSlot.E, target) +
                      GetDamage(SpellSlot.R, target) + Player.Instance.GetAutoAttackDamage(target);
            return dmg;
        }
        #endregion Damages
    }
}
