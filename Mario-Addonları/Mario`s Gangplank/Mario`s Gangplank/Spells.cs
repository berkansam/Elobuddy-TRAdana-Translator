using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

// ReSharper disable SwitchStatementMissingSomeCases

namespace Mario_sGangplank
{
    internal class Spells
    {
        #region Consts
        public const DamageType dmgType = DamageType.Physical;
        public const int highestRange = 1150;
        #endregion Consts

        public static void InitSpells()
        {
            SpellsSettings();
        }
        public static Spell.Targeted Q;
        public static Spell.Active W;
        public static Spell.Skillshot E;
        public static Spell.Skillshot R;
        public static List<Spell.SpellBase> SpellList = new List<Spell.SpellBase>(); 

        private static void SpellsSettings()
        {
            Q =  new Spell.Targeted(SpellSlot.Q, 625);
            SpellList.Add(Q);
            W =  new Spell.Active(SpellSlot.W);
            SpellList.Add(W);
            E =  new Spell.Skillshot(SpellSlot.E, 1150, SkillShotType.Circular, 450, 2000, 390)
            {
                AllowedCollisionCount = int.MaxValue
            };
            SpellList.Add(E);
            R =  new Spell.Skillshot(SpellSlot.R, int.MaxValue, SkillShotType.Circular, 250, int.MaxValue, 600)
            {
                AllowedCollisionCount = int.MaxValue
            };
            SpellList.Add(R);
        }

        #region Damages
        public static float GetDamage(SpellSlot slot,Obj_AI_Base target)
        {
            var lvl = Player.Instance.Spellbook.GetSpell(slot).Level - 1;
            var AD = Player.Instance.FlatPhysicalDamageMod;
            var TotalAD = Player.Instance.TotalAttackDamage;
            var AP = Player.Instance.FlatMagicDamageMod;
            var dmg = 0f;

            switch (slot)
            {
                case SpellSlot.Q:
                    if (Q.IsReady())
                    {
                        dmg += new float[] { 20, 45, 70, 95, 120 }[lvl] +1f * TotalAD;
                    }
                    break;
                case SpellSlot.W:
                    if (W.IsReady())
                    {
                        dmg += new float[] { 0, 0, 0, 0, 0 }[lvl] + 1f * AD;
                    }
                    break;
                case SpellSlot.E:
                    if (E.IsReady())
                    {
                        dmg += new float[] {80, 110, 140, 170, 200}[lvl];
                    }
                    break;
                case SpellSlot.R:
                    if (R.IsReady())
                    {
                        dmg += new float[] { 600, 840, 1080 }[lvl] * 0.6f + 1.2f * AP;
                    }
                    break;
            }
            return Player.Instance.CalculateDamageOnUnit(target, dmgType, dmg - 10);
        }

        public static float GetRKSDamage(Obj_AI_Base target)
        {
            var lvl = Player.Instance.Spellbook.GetSpell(SpellSlot.R).Level - 1;
            var AP = Player.Instance.FlatMagicDamageMod;
            var dmg = 0f;

            if (R.IsReady())
            {
                dmg += new float[] { 150, 210, 270 }[lvl] + 1.2f * AP;
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
