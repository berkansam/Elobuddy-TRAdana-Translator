namespace KappaUtility.Items
{
    using System.Linq;

    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;

    internal class AutoQSS
    {
        public static Spell.Active Cleanse;

        public static bool Cleaned;

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
            if (Player.Spells.FirstOrDefault(o => o.SData.Name.Contains("SummonerBoost")) != null)
            {
                QssMenu.Add("Cleanse", new CheckBox("Temizleme Kullan", false));
                Cleanse = new Spell.Active(Player.Instance.GetSpellSlotFromName("SummonerBoost"));
            }

            QssMenu.AddSeparator();
            QssMenu.AddGroupLabel("Debuffs Settings:");
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

            QssMenu.AddSeparator();
            QssMenu.AddGroupLabel("Ults Ayarları:");
            QssMenu.Add("liss", new CheckBox("Kullan Lissandra Ult?", false));
            QssMenu.Add("naut", new CheckBox("KullanNautilus Ult?", false));
            QssMenu.Add("zed", new CheckBox("Kullan Zed Ult?", false));
            QssMenu.Add("vlad", new CheckBox("Kullan Vlad Ult?", false));
            QssMenu.Add("fizz", new CheckBox("Kullan Fizz Ult?", false));
            QssMenu.Add("fiora", new CheckBox("Kullan Fiora Ult?", false));
            QssMenu.AddSeparator();
            QssMenu.Add("hp", new Slider("Sadece canım şundan azsa kullan %", 25, 0, 100));
            QssMenu.Add("human", new Slider("İnsancıl Gecikme", 150, 0, 1500));
            QssMenu.Add("Rene", new Slider("Yakında yakalanabilecek hedef varsa", 1, 0, 5));
            QssMenu.Add("enemydetect", new Slider("Düşman tespit etme menzili", 1000, 0, 2000));
            Obj_AI_Base.OnBuffGain += OnUpdate;
        }

        private static void OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (QssMenu["enable"].Cast<CheckBox>().CurrentValue)
            {
                if (sender.IsMe)
                {
                    var debuff = (QssMenu["charm"].Cast<CheckBox>().CurrentValue && args.Buff.Type == BuffType.Charm)
                                 || (QssMenu["tunt"].Cast<CheckBox>().CurrentValue && args.Buff.Type == BuffType.Taunt)
                                 || (QssMenu["stun"].Cast<CheckBox>().CurrentValue && args.Buff.Type == BuffType.Stun)
                                 || (QssMenu["fear"].Cast<CheckBox>().CurrentValue && args.Buff.Type == BuffType.Fear)
                                 || (QssMenu["silence"].Cast<CheckBox>().CurrentValue
                                     && args.Buff.Type == BuffType.Silence)
                                 || (QssMenu["snare"].Cast<CheckBox>().CurrentValue && args.Buff.Type == BuffType.Snare)
                                 || (QssMenu["supperss"].Cast<CheckBox>().CurrentValue
                                     && args.Buff.Type == BuffType.Suppression)
                                 || (QssMenu["sleep"].Cast<CheckBox>().CurrentValue && args.Buff.Type == BuffType.Sleep)
                                 || (QssMenu["poly"].Cast<CheckBox>().CurrentValue
                                     && args.Buff.Type == BuffType.Polymorph)
                                 || (QssMenu["frenzy"].Cast<CheckBox>().CurrentValue
                                     && args.Buff.Type == BuffType.Frenzy)
                                 || (QssMenu["disarm"].Cast<CheckBox>().CurrentValue
                                     && args.Buff.Type == BuffType.Disarm)
                                 || (QssMenu["nearsight"].Cast<CheckBox>().CurrentValue
                                     && args.Buff.Type == BuffType.NearSight)
                                 || (QssMenu["knockback"].Cast<CheckBox>().CurrentValue
                                     && args.Buff.Type == BuffType.Knockback)
                                 || (QssMenu["knockup"].Cast<CheckBox>().CurrentValue
                                     && args.Buff.Type == BuffType.Knockup)
                                 || (QssMenu["slow"].Cast<CheckBox>().CurrentValue && args.Buff.Type == BuffType.Slow)
                                 || (QssMenu["poison"].Cast<CheckBox>().CurrentValue
                                     && args.Buff.Type == BuffType.Poison)
                                 || (QssMenu["blind"].Cast<CheckBox>().CurrentValue && args.Buff.Type == BuffType.Blind)
                                 || (QssMenu["zed"].Cast<CheckBox>().CurrentValue && args.Buff.Name == "zedrtargetmark")
                                 || (QssMenu["vlad"].Cast<CheckBox>().CurrentValue
                                     && args.Buff.Name == "vladimirhemoplaguedebuff")
                                 || (QssMenu["liss"].Cast<CheckBox>().CurrentValue
                                     && args.Buff.Name == "LissandraREnemy2")
                                 || (QssMenu["fizz"].Cast<CheckBox>().CurrentValue
                                     && args.Buff.Name == "fizzmarinerdoombomb")
                                 || (QssMenu["naut"].Cast<CheckBox>().CurrentValue
                                     && args.Buff.Name == "nautilusgrandlinetarget")
                                 || (QssMenu["fiora"].Cast<CheckBox>().CurrentValue && args.Buff.Name == "fiorarmark");
                    var enemys = QssMenu["Rene"].Cast<Slider>().CurrentValue;
                    var hp = QssMenu["hp"].Cast<Slider>().CurrentValue;
                    var enemysrange = QssMenu["enemydetect"].Cast<Slider>().CurrentValue;
                    var delay = QssMenu["human"].Cast<Slider>().CurrentValue;
                    if (debuff && Player.Instance.HealthPercent <= hp
                        && enemys >= Player.Instance.Position.CountEnemiesInRange(enemysrange))
                    {
                        Cleaned = false;
                        Core.DelayAction(QssCast, delay);
                    }
                }
            }
        }

        public static void QssCast()
        {
            if (Cleaned == false)
            {
                if (Quicksilver_Sash.IsOwned() && Quicksilver_Sash.IsReady()
                    && QssMenu["Quicksilver"].Cast<CheckBox>().CurrentValue)
                {
                    Quicksilver_Sash.Cast();
                    Cleaned = true;
                }

                if (Mercurial_Scimitar.IsOwned() && Mercurial_Scimitar.IsReady()
                    && QssMenu["Mercurial"].Cast<CheckBox>().CurrentValue)
                {
                    Mercurial_Scimitar.Cast();
                    Cleaned = true;
                }

                if (QssMenu["Cleanse"].Cast<CheckBox>().CurrentValue && Cleanse.IsReady() && Cleanse != null)
                {
                    Cleanse.Cast();
                    Cleaned = true;
                }
            }
        }
    }
}