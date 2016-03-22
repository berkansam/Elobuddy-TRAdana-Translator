namespace KappaUtility.Misc
{
    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;

    internal class AutoQSS
    {
        protected static readonly Item Mercurial_Scimitar = new Item(ItemId.Mercurial_Scimitar);

        protected static readonly Item Quicksilver_Sash = new Item(ItemId.Quicksilver_Sash);

        public static Menu QssMenu { get; private set; }

        internal static void OnLoad()
        {
            QssMenu = Load.UtliMenu.AddSubMenu("AutoQSS");
            QssMenu.AddGroupLabel("AutoQSS Ayarları");
            QssMenu.Add("enable", new CheckBox("Aktif", false));
            QssMenu.Add("Mercurial", new CheckBox("Civa Yatağan kullan", false));
            QssMenu.Add("Quicksilver", new CheckBox("Siyah kuşak kullan", false));
            QssMenu.AddLabel("Cleanse Ayarları:");
            QssMenu.Add("blind", new CheckBox("Kör olunca?", false));
            QssMenu.Add("charm", new CheckBox("Cezbedilince(ahri)?", false));
            QssMenu.Add("disarm", new CheckBox("zararsız duruma getirilince?", false));
            QssMenu.Add("fear", new CheckBox("Korkunca?", false));
            QssMenu.Add("frenzy", new CheckBox("Donunca?", false));
            QssMenu.Add("silence", new CheckBox("Susturulunca?", false));
            QssMenu.Add("snare", new CheckBox("Tuzağa düşünce?", false));
            QssMenu.Add("sleep", new CheckBox("Uyuyunca?", false));
            QssMenu.Add("stun", new CheckBox("Sabitlenince?", false));
            QssMenu.Add("supperss", new CheckBox("Use On Supperss?", false));
            QssMenu.Add("slow", new CheckBox("Yavaşlayınca?", false));
            QssMenu.Add("knockup", new CheckBox("Use On Knock Ups?", false));
            QssMenu.Add("knockback", new CheckBox("Use On Knock Backs?", false));
            QssMenu.Add("nearsight", new CheckBox("Yakında biri varsa?", false));
            QssMenu.Add("root", new CheckBox("Köklenirse?", false));
            QssMenu.Add("tunt", new CheckBox("Alay edilirse?", false));
            QssMenu.Add("poly", new CheckBox("Use On Polymorph?", false));
            QssMenu.Add("poison", new CheckBox("Zehrlenirsem?", false));
            QssMenu.Add("hp", new Slider("Sadece canım şundan azsa kullan %", 25, 0, 100));
            QssMenu.Add("human", new Slider("İnsancıl Gecikme", 150, 0, 1500));
            QssMenu.Add("Rene", new Slider("Yakında yakalanabilecek hedef varsa", 1, 0, 5));
            QssMenu.Add("enemydetect", new Slider("Düşman tespit etme menzili", 1000, 0, 2000));
            Obj_AI_Base.OnBuffUpdate += OnBuffGain;
        }

        private static void OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffUpdateEventArgs args)
        {
            if (sender.IsEnemy && args.Buff.Caster.IsEnemy && sender is AIHeroClient && args.Buff.Caster is AIHeroClient)
            {
                Clean();
            }
        }

        public static void Clean()
        {
            if (QssMenu["enable"].Cast<CheckBox>().CurrentValue)
            {
                var debuff = (QssMenu["charm"].Cast<CheckBox>().CurrentValue && Player.Instance.IsCharmed)
                             || (QssMenu["root"].Cast<CheckBox>().CurrentValue && Player.Instance.IsRooted)
                             || (QssMenu["tunt"].Cast<CheckBox>().CurrentValue && Player.Instance.IsTaunted)
                             || (QssMenu["stun"].Cast<CheckBox>().CurrentValue && Player.Instance.IsStunned)
                             || (QssMenu["fear"].Cast<CheckBox>().CurrentValue
                                 && Player.Instance.HasBuffOfType(BuffType.Fear))
                             || (QssMenu["silence"].Cast<CheckBox>().CurrentValue
                                 && Player.Instance.HasBuffOfType(BuffType.Silence))
                             || (QssMenu["snare"].Cast<CheckBox>().CurrentValue
                                 && Player.Instance.HasBuffOfType(BuffType.Snare))
                             || (QssMenu["supperss"].Cast<CheckBox>().CurrentValue
                                 && Player.Instance.HasBuffOfType(BuffType.Suppression))
                             || (QssMenu["sleep"].Cast<CheckBox>().CurrentValue
                                 && Player.Instance.HasBuffOfType(BuffType.Sleep))
                             || (QssMenu["poly"].Cast<CheckBox>().CurrentValue
                                 && Player.Instance.HasBuffOfType(BuffType.Polymorph))
                             || (QssMenu["frenzy"].Cast<CheckBox>().CurrentValue
                                 && Player.Instance.HasBuffOfType(BuffType.Frenzy))
                             || (QssMenu["disarm"].Cast<CheckBox>().CurrentValue
                                 && Player.Instance.HasBuffOfType(BuffType.Disarm))
                             || (QssMenu["nearsight"].Cast<CheckBox>().CurrentValue
                                 && Player.Instance.HasBuffOfType(BuffType.NearSight))
                             || (QssMenu["knockback"].Cast<CheckBox>().CurrentValue
                                 && Player.Instance.HasBuffOfType(BuffType.Knockback))
                             || (QssMenu["knockup"].Cast<CheckBox>().CurrentValue
                                 && Player.Instance.HasBuffOfType(BuffType.Knockup))
                             || (QssMenu["slow"].Cast<CheckBox>().CurrentValue
                                 && Player.Instance.HasBuffOfType(BuffType.Slow))
                             || (QssMenu["poison"].Cast<CheckBox>().CurrentValue
                                 && Player.Instance.HasBuffOfType(BuffType.Poison))
                             || (QssMenu["blind"].Cast<CheckBox>().CurrentValue
                                 && Player.Instance.HasBuffOfType(BuffType.Blind));
                var enemys = QssMenu["Rene"].Cast<Slider>().CurrentValue;
                var hp = QssMenu["hp"].Cast<Slider>().CurrentValue;
                var enemysrange = QssMenu["enemydetect"].Cast<Slider>().CurrentValue;
                if (debuff && ObjectManager.Player.HealthPercent <= hp
                    && enemys >= ObjectManager.Player.Position.CountEnemiesInRange(enemysrange))
                {
                    Core.DelayAction(QssCast, QssMenu["human"].Cast<Slider>().CurrentValue);
                }
            }
        }

        public static void QssCast()
        {
            if (Quicksilver_Sash.IsOwned() && Quicksilver_Sash.IsReady()
                && QssMenu["Quicksilver"].Cast<CheckBox>().CurrentValue)
            {
                if (Quicksilver_Sash.Cast())
                {
                    return;
                }
            }

            if (Mercurial_Scimitar.IsOwned() && Mercurial_Scimitar.IsReady()
                && QssMenu["Mercurial"].Cast<CheckBox>().CurrentValue)
            {
                Mercurial_Scimitar.Cast();
            }
        }
    }
}