﻿using EloBuddy;
using EloBuddy.SDK;

namespace Bard
{
    public static class Utility
    {
        public static readonly Item HealthPotion;
        public static readonly Item CorruptingPotion;
        public static readonly Item RefillablePotion;
        public static readonly Item HuntersPotion;
        public static readonly Item TotalBiscuit;

        //VodkaSmite
        public static readonly string[] MonstersNames =
        {
            "SRU_Blue", "SRU_Gromp", "SRU_Murkwolf", "SRU_Razorbeak",
            "SRU_Red", "SRU_Krug", "SRU_Dragon", "Sru_Crab", "SRU_Baron", "SRU_RiftHerald"
        };

        public static readonly string[] SmiteNames =
        {
            "summonersmite", "s5_summonersmiteplayerganker", "s5_summonersmiteduel"
        };


        static Utility()
        {
            HealthPotion = new Item(2003);
            TotalBiscuit = new Item(2010);
            CorruptingPotion = new Item(2033);
            RefillablePotion = new Item(2031);
            HuntersPotion = new Item(2032);
        }

        public static float SmiteDmgMonster(Obj_AI_Base target)
        {
            return Player.Instance.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Smite);
        }

        public static float SmiteDmgHero(AIHeroClient target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.True,
                20.0f + Player.Instance.Level*8.0f);
        }

        public static float IgniteDmg(Obj_AI_Base target)
        {
            return Player.Instance.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite);
        }
    }
}