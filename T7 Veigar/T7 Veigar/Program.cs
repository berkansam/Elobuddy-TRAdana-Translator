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


namespace SoManyHoursSpentOnThisProject 
{
    class ΤοΠιλλ
    {
        private static void Main(string[] args) { Loading.OnLoadingComplete += OnLoad; }
        public static AIHeroClient myhero { get { return ObjectManager.Player; } }
        private static Menu menu,combo,harass,laneclear,misc,draw,pred,sequence1,sequence2,sequence3;
        public static Spell.Targeted ignt = new Spell.Targeted(myhero.GetSpellSlotFromName("summonerdot"), 550);
          
        public static void OnLoad(EventArgs arg)
        {
            DemSpells.Q.AllowedCollisionCount = 1;
            if (Player.Instance.ChampionName != "Veigar") { return; }
            Chat.Print("<font color='#0040FF'>T7</font><font color='#A901DB'> Veigar</font> : Loaded!(v1.1)");
            Chat.Print("<font color='#04B404'>By </font><font color='#FF0000'>T</font><font color='#FA5858'>o</font><font color='#FF0000'>y</font><font color='#FA5858'>o</font><font color='#FF0000'>t</font><font color='#FA5858'>a</font><font color='#0040FF'>7</font><font color='#FF0000'> <3 </font>");
            Drawing.OnDraw += OnDraw;
            Obj_AI_Base.OnLevelUp += OnLvlUp;
            DatMenu();
            Game.OnTick += OnTick;
            
        } 
        private static void OnLvlUp(Obj_AI_Base guy ,Obj_AI_BaseLevelUpEventArgs args)
        {
            if (!guy.IsMe) return;
/*Q>W>E*/   SpellSlot[] sequence1 = { SpellSlot.Unknown, SpellSlot.E, SpellSlot.W, SpellSlot.Q, SpellSlot.Q, SpellSlot.R, SpellSlot.Q, SpellSlot.W, SpellSlot.Q, SpellSlot.E, SpellSlot.R, SpellSlot.W, SpellSlot.E, SpellSlot.W, SpellSlot.W, SpellSlot.R, SpellSlot.E, SpellSlot.E };
/*Q>E>W*/   SpellSlot[] sequence2 = { SpellSlot.Unknown, SpellSlot.E, SpellSlot.Q, SpellSlot.W, SpellSlot.Q, SpellSlot.R, SpellSlot.Q, SpellSlot.E, SpellSlot.Q, SpellSlot.E, SpellSlot.R, SpellSlot.E, SpellSlot.E, SpellSlot.W, SpellSlot.W, SpellSlot.R, SpellSlot.W, SpellSlot.W };
/*E>Q>W*/   SpellSlot[] sequence3 = { SpellSlot.Unknown, SpellSlot.Q, SpellSlot.E, SpellSlot.Q, SpellSlot.E, SpellSlot.R, SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.E, SpellSlot.R, SpellSlot.Q, SpellSlot.Q, SpellSlot.W, SpellSlot.W, SpellSlot.R, SpellSlot.W, SpellSlot.W };

            if(misc["autoS"].Cast<CheckBox>().CurrentValue) Player.LevelSpell(sequence1[myhero.Level]);
        } 
        private static void OnTick(EventArgs args)
        {
            if (myhero.IsDead) return;

            Misc();
            
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo) Combo();

            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.LaneClear && laneclear["LcM"].Cast<Slider>().CurrentValue <= myhero.ManaPercent ) Laneclear();

            if (laneclear["AutoL"].Cast<CheckBox>().CurrentValue && laneclear["LcM"].Cast<Slider>().CurrentValue <= myhero.ManaPercent) Laneclear();

            if (laneclear["Qlk"].Cast<KeyBind>().CurrentValue && laneclear["LcM"].Cast<Slider>().CurrentValue <= myhero.ManaPercent) QStack();
                
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Harass && myhero.ManaPercent >= harass["minMH"].Cast<Slider>().CurrentValue) Harass();

            if (harass["autoH"].Cast<CheckBox>().CurrentValue && myhero.ManaPercent > harass["minMH"].Cast<Slider>().CurrentValue) Harass();
        }
        private static float ComboDMG(Obj_AI_Base target)
        {
            if (target != null)
            {
                float cdmg = 0;

                if (DemSpells.Q.IsReady()) { cdmg = cdmg + myhero.GetSpellDamage(target, SpellSlot.Q); }
                if (DemSpells.W.IsReady()) { cdmg = cdmg + myhero.GetSpellDamage(target, SpellSlot.W); }
                if (DemSpells.R.IsReady()) { cdmg = cdmg + myhero.GetSpellDamage(target, SpellSlot.R); }

                if (ignt.Slot != SpellSlot.Unknown && ignt.IsReady()) { cdmg = cdmg + myhero.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite); }
                return cdmg;
            }
            return 0;
        } 
        public static void Harass() 
        {        
            var target = TargetSelector.GetTarget(1000, DamageType.Magical, Player.Instance.Position);

            if (target != null)
            {
                if (harass["hQ"].Cast<CheckBox>().CurrentValue && DemSpells.Q.IsReady() && DemSpells.Q.IsInRange(target))
                {
                    var Qpred = DemSpells.Q.GetPrediction(target);
                    switch (pred["Qhit"].Cast<ComboBox>().CurrentValue)
                    {
                        case 0:
                            if (Qpred.HitChance >= HitChance.Low) DemSpells.Q.Cast(Qpred.CastPosition);
                            break;
                        case 1:
                            if (Qpred.HitChance >= HitChance.Medium) DemSpells.Q.Cast(Qpred.CastPosition);
                            break;
                        case 2:
                            if (Qpred.HitChance >= HitChance.High) DemSpells.Q.Cast(Qpred.CastPosition);
                            break;
                    }
                }

                if (harass["hW"].Cast<CheckBox>().CurrentValue && DemSpells.W.IsReady() && DemSpells.W.IsInRange(target))
                {
                    var Wpred = DemSpells.W.GetPrediction(target);
                    switch (harass["hWm"].Cast<ComboBox>().CurrentValue)
                    {
                        case 0:
                            switch (pred["Whit"].Cast<ComboBox>().CurrentValue)
                            {
                                case 0:
                                    if (Wpred.HitChance == HitChance.Low) DemSpells.W.Cast(Wpred.CastPosition);
                                    break;
                                case 1:
                                    if (Wpred.HitChance == HitChance.Medium) DemSpells.W.Cast(Wpred.CastPosition);
                                    break;
                                case 2:
                                    if (Wpred.HitChance == HitChance.High) DemSpells.W.Cast(Wpred.CastPosition);
                                    break;
                            }
                            break;
                        case 1:
                            DemSpells.W.Cast(target.Position);
                            break;
                        case 2:
                            if (Wpred.HitChance == HitChance.Immobile || target.HasBuffOfType(BuffType.Stun)) DemSpells.W.Cast(Wpred.CastPosition);
                            break;

                    }
                }
            }                 
        } 
        private static void Combo() 
        {
           
            var target = TargetSelector.GetTarget(1000, DamageType.Magical, Player.Instance.Position);
            
            if(target != null)
            { 
                var Qpred = DemSpells.Q.GetPrediction(target);
                var Wpred = DemSpells.W.GetPrediction(target);

                if (combo["useQ"].Cast<CheckBox>().CurrentValue && DemSpells.Q.IsReady() && DemSpells.Q.IsInRange(target))
                {
                    switch (pred["Qhit"].Cast<ComboBox>().CurrentValue)
                    {
                        case 0:
                            if (Qpred.HitChance >= HitChance.Low) DemSpells.Q.Cast(Qpred.CastPosition);
                            break;
                        case 1:
                            if (Qpred.HitChance >= HitChance.Medium) DemSpells.Q.Cast(Qpred.CastPosition);
                            break;
                        case 2:
                            if (Qpred.HitChance >= HitChance.High) DemSpells.Q.Cast(Qpred.CastPosition);
                            break;
                    }
                }
                if (combo["useE"].Cast<CheckBox>().CurrentValue && DemSpells.E.IsReady() && DemSpells.E.IsInRange(target))
                {
                    switch (combo["Es"].Cast<CheckBox>().CurrentValue)
                    {
                        case false:
                            DemSpells.E.Cast(target.Position);
                            break;
                        case true:
                            if (!target.HasBuffOfType(BuffType.Stun)) DemSpells.E.Cast(target.Position);
                            break;
                    }
                }
                if (combo["useW"].Cast<CheckBox>().CurrentValue && DemSpells.W.IsReady() && DemSpells.W.IsInRange(target))
                {
                    switch (combo["useWs"].Cast<CheckBox>().CurrentValue)
                    {
                        case true:
                            if (target.HasBuffOfType(BuffType.Stun)) DemSpells.W.Cast(target.Position);
                            break;
                        case false:
                            switch (pred["Whit"].Cast<ComboBox>().CurrentValue)
                            {
                                case 0:
                                    if (Wpred.HitChance == HitChance.Low) DemSpells.W.Cast(Wpred.CastPosition);
                                    break;
                                case 1:
                                    if (Wpred.HitChance == HitChance.Medium) DemSpells.W.Cast(Wpred.CastPosition);
                                    break;
                                case 2:
                                    if (Wpred.HitChance == HitChance.High) DemSpells.W.Cast(Wpred.CastPosition);
                                    break;
                            }
                            break;
                    }
                }
                if (combo["useR"].Cast<CheckBox>().CurrentValue && DemSpells.R.IsReady() && DemSpells.R.IsInRange(target) && ComboDMG(target) > target.Health) DemSpells.R.Cast(target);

                if (combo["igntC"].Cast<CheckBox>().CurrentValue && ignt.IsReady() && ComboDMG(target) > target.Health && ignt.IsInRange(target)) ignt.Cast(target);
            }
        } 
        private static void QStack()
        {
            var farm = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, myhero.Position, DemSpells.W.Range).Where(x => x.Health <= myhero.GetSpellDamage(x, SpellSlot.Q));
            var FarmPred = EntityManager.MinionsAndMonsters.GetLineFarmLocation(farm, DemSpells.Q.Width, (int)DemSpells.Q.Range);

            switch (laneclear["Qlm"].Cast<ComboBox>().CurrentValue)
            {
                case 0:
                    if (FarmPred.HitNumber >= 1) DemSpells.Q.Cast(FarmPred.CastPosition);
                    break;
                case 1:
                    if (FarmPred.HitNumber == 2) DemSpells.Q.Cast(FarmPred.CastPosition);
                    break;
                case 2:
                    var BigMinions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, myhero.Position, DemSpells.W.Range).Where(x => x.BaseSkinName.Contains("Siege") && x.Health <= myhero.GetSpellDamage(x, SpellSlot.Q));
                    var BMpred = EntityManager.MinionsAndMonsters.GetLineFarmLocation(BigMinions, DemSpells.Q.Width, (int)DemSpells.Q.Range);
                    if (BMpred.HitNumber == 1) DemSpells.Q.Cast(BMpred.CastPosition);
                    break;
            }
        }
        private static void Laneclear()
        {
            var minion = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, myhero.Position, DemSpells.W.Range);

            if (laneclear["LQ"].Cast<CheckBox>().CurrentValue  && DemSpells.Q.IsReady() && !laneclear["Qlk"].Cast<KeyBind>().CurrentValue)
            {             
                   var Qpred = EntityManager.MinionsAndMonsters.GetLineFarmLocation(minion, DemSpells.Q.Width, (int)DemSpells.Q.Range);

                   if (Qpred.HitNumber >= 1) DemSpells.Q.Cast(Qpred.CastPosition);
            }

            if (laneclear["LW"].Cast<CheckBox>().CurrentValue && minion != null && DemSpells.W.IsReady())
            {
                var Wpred = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(minion, DemSpells.W.Width, (int)DemSpells.W.Range);

                if (Wpred.HitNumber >= laneclear["Wmm"].Cast<Slider>().CurrentValue) DemSpells.W.Cast(Wpred.CastPosition);
            }      
        } 
        private static void Misc()
        {
            if (misc["sh"].Cast<CheckBox>().CurrentValue)
            {
                myhero.SetSkinId((int)misc["sID"].Cast<ComboBox>().CurrentValue);
            }

            var target = TargetSelector.GetTarget(1000, DamageType.Magical, Player.Instance.Position);

            if (target != null)
            {
                var Qpred = DemSpells.Q.GetPrediction(target);
                var Wpred = DemSpells.W.GetPrediction(target);

                if (misc["ksQ"].Cast<CheckBox>().CurrentValue && myhero.GetSpellDamage(target, SpellSlot.Q) > target.Health && target.Distance(myhero) < DemSpells.W.Range && DemSpells.Q.IsReady() && !target.IsInvulnerable)
                {
                    if (target.HasBuffOfType(BuffType.Stun)) DemSpells.Q.Cast(target.Position);
                    else
                    {
                        switch (pred["Qhit"].Cast<ComboBox>().CurrentValue)
                        {
                            case 0:
                                if (Qpred.HitChance >= HitChance.Low) DemSpells.Q.Cast(Qpred.CastPosition);
                                break;
                            case 1:
                                if (Qpred.HitChance >= HitChance.Medium) DemSpells.Q.Cast(Qpred.CastPosition);
                                break;
                            case 2:
                                if (Qpred.HitChance >= HitChance.High) DemSpells.Q.Cast(Qpred.CastPosition);
                                break;
                        }
                    }
                }
                if (misc["ksW"].Cast<CheckBox>().CurrentValue && myhero.GetSpellDamage(target, SpellSlot.W) > target.Health && target.Distance(myhero) < DemSpells.W.Range && DemSpells.W.IsReady() && !target.IsInvulnerable)
                {

                    if (Wpred.HitChance == HitChance.Immobile || Wpred.HitChance >= HitChance.Medium) DemSpells.W.Cast(Wpred.CastPosition);
                }
            }
                        
        }
        private static void OnDraw(EventArgs args)
        {
         
            if (draw["drawQ"].Cast<CheckBox>().CurrentValue && DemSpells.Q.Level > 0 && !myhero.IsDead && !draw["nodraw"].Cast<CheckBox>().CurrentValue)
            {

                if (draw["nodrawc"].Cast<CheckBox>().CurrentValue) { Drawing.DrawCircle(myhero.Position, DemSpells.Q.Range,DemSpells.Q.IsOnCooldown ? Color.Transparent:Color.SkyBlue); }

                else if (!draw["nodrawc"].Cast<CheckBox>().CurrentValue) { Drawing.DrawCircle(myhero.Position, DemSpells.Q.Range, Color.SkyBlue); }

            }

            if (draw["drawW"].Cast<CheckBox>().CurrentValue && DemSpells.W.Level > 0 && !myhero.IsDead && !draw["nodraw"].Cast<CheckBox>().CurrentValue)
            {

                if (draw["nodrawc"].Cast<CheckBox>().CurrentValue) { Drawing.DrawCircle(myhero.Position, DemSpells.W.Range, DemSpells.W.IsOnCooldown ? Color.Transparent : Color.SkyBlue); }

                else if (!draw["nodrawc"].Cast<CheckBox>().CurrentValue) { Drawing.DrawCircle(myhero.Position, DemSpells.W.Range, Color.SkyBlue); }

            }

            if (draw["drawE"].Cast<CheckBox>().CurrentValue && DemSpells.E.Level > 0 && !myhero.IsDead && !draw["nodraw"].Cast<CheckBox>().CurrentValue)
            {

                if (draw["nodrawc"].Cast<CheckBox>().CurrentValue) { Drawing.DrawCircle(myhero.Position, DemSpells.E.Range, DemSpells.E.IsOnCooldown ? Color.Transparent : Color.SkyBlue); }

                else if (!draw["nodrawc"].Cast<CheckBox>().CurrentValue) { Drawing.DrawCircle(myhero.Position, DemSpells.E.Range, Color.SkyBlue); }

            }

            if (draw["drawR"].Cast<CheckBox>().CurrentValue && DemSpells.R.Level > 0 && !myhero.IsDead && !draw["nodraw"].Cast<CheckBox>().CurrentValue)
            {

                if (draw["nodrawc"].Cast<CheckBox>().CurrentValue) { Drawing.DrawCircle(myhero.Position, DemSpells.R.Range, DemSpells.R.IsOnCooldown ? Color.Transparent : Color.SkyBlue); }

                else if (!draw["nodrawc"].Cast<CheckBox>().CurrentValue) { Drawing.DrawCircle(myhero.Position, DemSpells.R.Range, Color.SkyBlue); }

            }

            if (draw["drawAA"].Cast<CheckBox>().CurrentValue && !myhero.IsDead && !draw["nodraw"].Cast<CheckBox>().CurrentValue) Drawing.DrawCircle(myhero.Position, myhero.AttackRange, Color.White); 

            foreach (var enemy in EntityManager.Heroes.Enemies)
            {
                if (draw["drawk"].Cast<CheckBox>().CurrentValue && !draw["nodraw"].Cast<CheckBox>().CurrentValue && enemy.IsVisible) Drawing.DrawText(Drawing.WorldToScreen(enemy.Position).X, Drawing.WorldToScreen(enemy.Position).Y - 30, ComboDMG(enemy) > enemy.Health ? Color.Green : Color.Transparent, "Killable");
            }

            Drawing.DrawText(Drawing.WorldToScreen(myhero.Position).X - 50, Drawing.WorldToScreen(myhero.Position).Y + 10 ,Color.Red, laneclear["Qlk"].Cast<KeyBind>().CurrentValue ? "Auto Stacking: ON" : "Auto Stacking: OFF");
        } 
        private static void DatMenu()
        {

            menu = MainMenu.AddMenu("T7 Veigar", "veigarxd");
            combo = menu.AddSubMenu("Kombo" ,"combo");
            harass = menu.AddSubMenu("Dürtme","harass");
            laneclear = menu.AddSubMenu("LaneTemizleme", "lclear");
            draw = menu.AddSubMenu("Göstergeler", "draw");
            misc = menu.AddSubMenu("Ek" ,"misc");
            pred = menu.AddSubMenu("Prediction", "pred");

            menu.AddGroupLabel("T7 Veigar Hoşgeldiniz!");
            menu.AddGroupLabel("Version 1.0");
            menu.AddGroupLabel("Yapımcı: Toyota7");
            menu.AddSeparator();
            menu.AddGroupLabel("Sorunları Lütfen Bildirin <3");
            menu.AddGroupLabel("Çeviri-TRAdana");

            combo.AddGroupLabel("Büyüler");
            combo.Add("useQ", new CheckBox("Komboda Q Kullan",true)); 
            combo.Add("useW", new CheckBox("Komboda W Kullan",true));
            combo.Add("useE", new CheckBox("Komboda E Kullan",true));
            combo.Add("useR", new CheckBox("Komboda R Kullan",true));
            combo.Add("igntC", new CheckBox("Kullan Tutuştur", false));
            combo.AddSeparator();
            combo.Add("Es", new CheckBox("Immobile Düşmanlarda E Kullanma",true));
            combo.Add("useWs", new CheckBox("Sadece Sabitlenmiş Düşmanlara W Kullan", true));
            
            harass.AddGroupLabel("Büyüler");
            harass.Add("hQ", new CheckBox("Kullan Q",true));
            harass.Add("hW", new CheckBox("Kullan W", false));
            harass.AddGroupLabel("W Modu:");
            harass.Add("hWm", new ComboBox("Modu Seçin",2,"With Prediciton","Without Prediction(Not Recommended)","Only On Stunned Enemies"));
            harass.AddSeparator();
            harass.AddGroupLabel("Dürtme için gereken mana");
            harass.Add("minMH", new Slider("Dürtmeyi Durdur şunun altına düşünce % ", 40, 0, 100));
            harass.AddSeparator();
            harass.AddGroupLabel("Otomatik Dürtme");
            harass.Add("autoH", new CheckBox("Otomatik Dürtme Kullan", false));

            laneclear.AddGroupLabel("Büyüler");
            laneclear.Add("LQ", new CheckBox("Kullan Q", true)); 
            laneclear.Add("LW", new CheckBox("Kullan W", false));
            laneclear.AddSeparator();
            laneclear.AddGroupLabel("Q Yük Kasımı");
            laneclear.Add("Qlk", new KeyBind("Otomatik Yük Kasma",true,KeyBind.BindTypes.PressToggle,'F'));
            laneclear.Add("Qlm",new ComboBox("Seç MOD",1,"Sonvuruş 1 minyon","sonvuruş 2 minyon","sonvuruş sadece büyük minyona"));
            laneclear.AddSeparator();
            laneclear.AddGroupLabel("En az W minyonları");
            laneclear.Add("Wmm", new Slider("W için en az minyon", 2, 1, 6));
            laneclear.AddSeparator();
            laneclear.AddGroupLabel("LaneTemizleme manam şunun altına düşerse durdur % ");
            laneclear.Add("LcM", new Slider("%", 50, 0, 100));
            laneclear.AddSeparator();
            laneclear.AddGroupLabel("Otomatik LaneTemizleme");
            laneclear.Add("AutoL", new CheckBox("Otomatik LaneTemizleme", false));
            

            

            draw.Add("nodraw", new CheckBox("Tüm Göstergeler Devredışı",false)); 
            draw.AddSeparator();
            draw.Add("drawQ", new CheckBox("Göster Q Menzili", true));
            draw.Add("drawW", new CheckBox("Göster W Menzili", true));
            draw.Add("drawE", new CheckBox("Göster E Menzili", true));
            draw.Add("drawR", new CheckBox("Göster R Menzili", true));
            draw.Add("drawAA", new CheckBox("Göster AA Menzili", false));
            draw.Add("drawk", new CheckBox("Göster Öldürülebilir Düşmanları", false));
            draw.Add("nodrawc", new CheckBox("Göster Sadece Hazır Büyüleri", false));

            misc.Add("ksQ", new CheckBox("Q ile Kill Çal"));
            misc.Add("ksW", new CheckBox("W ile Kill Çal"));
            misc.AddSeparator();
            misc.AddGroupLabel("Otomatik Level Arttırma");
            misc.Add("autoS",new CheckBox("Aktif Otomatik Level Arttırma", true));
            misc.Add("lvlSpells", new ComboBox("Sıra Seç" , 0 , "Q>W>E"));
            misc.Add("lolKappa",new CheckBox("Daha Fazla Sıra Yakında!",false));
            misc.AddSeparator();
            misc.AddGroupLabel("Skin Değiştirici");
            misc.Add("sh", new CheckBox("Aktif Skin Değiştirici"));
            misc.Add("sID", new ComboBox("Skin Numarası", 0, "Varsayılan", "White Mage", "Curling", "Veigar Greybeard", "Leprechaun", "Baron Von", "Superb Villain", "Bad Santa", "Final Boss"));


            pred.AddGroupLabel("Q İsabet Oranı");
            pred.Add("Qhit",new ComboBox("Seç İsabet Oranı", 1,"Düşük","Normal","Yüksek"));
            pred.AddSeparator();
            pred.AddGroupLabel("W İsabet Oranı");
            pred.Add("Whit", new ComboBox("Seç İsabet Oranı", 1, "Düşük", "Normal", "Yüksek"));
                
        }          
    }

    public static class DemSpells
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Targeted R { get; private set; }

        static DemSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Linear, 250, 2000, 70);
            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Circular, 1350, 0, 225);
            E = new Spell.Skillshot(SpellSlot.E, 700, SkillShotType.Circular, 500, 0, 425);
            R = new Spell.Targeted(SpellSlot.R, 650);
        }
    }
}
