using System;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using OKTRAIO.Utility;

namespace OKTRAIO.Menu_Settings
{
    internal class MainMenu
    {
        public static Menu Menu, Combo, Lane, Jungle, Lasthit, Harass, Flee, Misc, Ks, Draw;

        public static void Init()
        {
            try
            {
                /*
                    How does it work?

                    Just Call The method in Champion script when you need to use, remember some menus need variables for generating
                 * Test
                */

                //              
                //  Main Menu   
                //              
                Menu = EloBuddy.SDK.Menu.MainMenu.AddMenu("OKTR AIO ", "marks.aio", Player.Instance.ChampionName);
                Menu.AddGroupLabel("Tek Tuş ve Report AIO - " + Player.Instance.ChampionName);
                Menu.AddLabel("Hey Bu addonla Eğlen: ONE KEY TO RAXE AIO!");
                Menu.AddLabel("Doh! is Tek Tuşla Report >_< \n Or OneKeyToRape (Senin Düşmanların) ಠ_ಠ");
                Menu.AddLabel("Ceviri-TRAdana iyi oyunlar diler");
                Menu.AddSeparator();
                Menu.AddGroupLabel("Ana Ayarlar:");
                Menu.Add("playsound", new CheckBox("Başlangıç Sesini Çal"));
                Menu.AddSeparator();
                Menu.AddGroupLabel("Performans Ayarları:");
                Menu.Add("useonupdate", new CheckBox("İyileştir PERFORMANCE*", false));
                Menu.AddSeparator();
                Menu.AddLabel("*=Lütfen dinle, Fps sorunu yaşıyorsan performans iyileştirmesini aç");

                //Todo Improve Menu Design

                //
                //  Combo Menu
                //
                Combo = Menu.AddSubMenu("Combo Menüsü", "combo");
                Combo.AddGroupLabel("Combo Ayarları");
                //If wanna to add more function use MainMenu.Combo.Add... Call in Champion Script

                //
                //  Lane Clear
                //
                Lane = Menu.AddSubMenu("LaneClear Menu", "lane");

                //
                //  Jungle Clear
                //
                Jungle = Menu.AddSubMenu("JungleClear Menu", "jungle");

                //
                //  Harrass
                //
                Harass = Menu.AddSubMenu("Dürtme Menu", "harass");
                Harass.AddGroupLabel("Dürtme Ayarları");

                //
                //  Flee Menu
                //
                //Use Initiator FleeKeys(bool,bool,bool,bool)

                //
                //  KS
                //
                Ks = Menu.AddSubMenu("Kill Çalma", "killsteal");
                Ks.AddGroupLabel("Kill Çalma Ayarları");

                //
                // Misc Menu
                //MiscMenuInit();
                //
                //Call it on Champion Script... need to configurate manually

                //
                //  Draw Menu
                //
                Draw = Menu.AddSubMenu("Gösterge Menu", "draw");
                Draw.AddGroupLabel("Gösterge Ayarları");
                Draw.AddSeparator();
                Draw.AddCheckBox("draw.disable", "Göstergekeri devredışı bırak", false);
                Draw.AddCheckBox("draw.ready", "Sadece hazır skilleri göster");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Chat.Print(
                    "<font color='#23ADDB'>Marksman AIO:</font><font color='#E81A0C'> an error ocurred. (Code 2)</font>");
            }
            //TODO: Main menu stuff n things
        }

        public static void MiscMenu()
        {
            Misc = Menu.AddSubMenu("Ek Menu", "misc");
            Misc.AddGroupLabel("Ek Ayarları");
            Misc.Add("misc.advanced", new CheckBox("Göster Gelişmiş Menu", false)).OnValueChange +=
                Value.AdvancedModeChanged;
            JsonSettings.Profile.Options.Add(new JsonSetting("misc.advanced", Setting.Checkbox, Misc["misc.advanced"].Cast<CheckBox>().CurrentValue.ToString()));
        }

        public static void DamageIndicator(bool jungle = false, string draw = "")
        {
            Draw.AddSeparator();
            Draw.AddGroupLabel("Düşman Hasar Tespiti Ayarları");
            Draw.AddLabel("Drawing: " + (draw == "" ? "Combo damage" : draw));
            Draw.AddCheckBox("draw.enemyDmg", "Düşmanın Canbarını ne kadar hasar verileceğini göster");
            Draw.AddColorItem("draw.color.enemyDmg", 3);

            if (jungle)
            {
                Draw.AddSeparator();
                Draw.AddGroupLabel("Orman Temizleme Hasar Tespiti Ayarları");
                Draw.AddCheckBox("draw.jungleDmg", "Orman moblarının canbarlarını göster");
                Draw.AddColorItem("draw.color.jungleDmg", 15);
            }
        }

        public static void DrawKeys(
            bool useQ = true,
            bool defaultQ = true,
            bool useW = true,
            bool defaultW = true,
            bool useE = true,
            bool defaultE = true,
            bool useR = true,
            bool defaultR = true)
        {
            if (useQ)
            {
                Draw.AddSeparator();
                Draw.AddGroupLabel("Q Ayarları");
                Draw.AddCheckBox("draw.q", "Göster Q", defaultQ);
                Draw.AddColorItem("color.q");
                Draw.AddWidthItem("width.q");
            }
            if (useW)
            {
                Draw.AddSeparator();
                Draw.AddGroupLabel("W Ayarları");
                Draw.AddCheckBox("draw.w", "Göster W", defaultW);
                Draw.AddColorItem("color.w");
                Draw.AddWidthItem("width.w");
            }
            if (useE)
            {
                Draw.AddSeparator();
                Draw.AddGroupLabel("E Ayarları");
                Draw.AddCheckBox("draw.e", "Göster E", defaultE);
                Draw.AddColorItem("color.e");
                Draw.AddWidthItem("width.e");
            }
            if (useR)
            {
                Draw.AddSeparator();
                Draw.AddGroupLabel("R Ayarları");
                Draw.AddCheckBox("draw.r", "Göster R", defaultR);
                Draw.AddColorItem("color.r");
                Draw.AddWidthItem("width.r");
            }
            Draw.Add("draw.advanced", new CheckBox("Göster Gelişmiş Menu", false)).OnValueChange +=
                Value.AdvancedModeChanged;
            JsonSettings.Profile.Options.Add(new JsonSetting("draw.advanced", Setting.Checkbox, Draw["draw.advanced"].Cast<CheckBox>().CurrentValue.ToString()));
        }

        public static void ComboManaManager(bool q, bool w, bool e, bool r, int qmana, int wmana, int emana, int rmana)
        {
            if (q)
            {
                Combo.AddSlider("combo.q.mana", "Use Q if Mana is above {0}%", qmana, 0, 100, true);
            }
            if (w)
            {
                Combo.AddSlider("combo.w.mana", "Use W if Mana is above {0}%", wmana, 0, 100, true);
            }
            if (w)
            {
                Combo.AddSlider("combo.e.mana", "Use E if Mana is above {0}%", emana, 0, 100, true);
            }
            if (r)
            {
                Combo.AddSlider("combo.r.mana", "Use R if Mana is above {0}%", rmana, 0, 100, true);
            }
        }

        public static void LaneManaManager(bool q, bool w, bool e, bool r, int qmana, int wmana, int emana, int rmana)
        {
            if (q)
            {
                Lane.AddSlider("lane.q.mana", "Use Q if Mana is above {0}%", qmana, 0, 100, true);
            }
            if (w)
            {
                Lane.AddSlider("lane.w.mana", "Use W if Mana is above {0}%", wmana, 0, 100, true);
            }
            if (e)
            {
                Lane.AddSlider("lane.e.mana", "Use E if Mana is above {0}%", emana, 0, 100, true);
            }
            if (r)
            {
                Lane.AddSlider("lane.r.mana", "Use R if Mana is above {0}%", rmana, 0, 100, true);
            }
        }

        public static void JungleManaManager(bool q, bool w, bool e, bool r, int qmana, int wmana, int emana, int rmana)
        {
            if (q)
            {
                Jungle.AddSlider("jungle.q.mana", "Use Q if Mana is above {0}%", qmana, 0, 100, true);
            }
            if (w)
            {
                Jungle.AddSlider("jungle.w.mana", "Use W if Mana is above {0}%", wmana, 0, 100, true);
            }
            if (e)
            {
                Jungle.AddSlider("jungle.e.mana", "Use E if Mana is above {0}%", emana, 0, 100, true);
            }
            if (r)
            {
                Jungle.AddSlider("jungle.r.mana", "Use R if Mana is above {0}%", rmana, 0, 100, true);
            }
        }

        public static void LasthitManaManager(
            bool q,
            bool w,
            bool e,
            bool r,
            int qmana,
            int wmana,
            int emana,
            int rmana)
        {
            if (q)
            {
                Lasthit.AddSlider("lasthit.q.mana", "Use Q if Mana is above {0}%", qmana, 0, 100, true);
            }
            if (w)
            {
                Lasthit.AddSlider("lasthit.w.mana", "Use W if Mana is above {0}%", wmana, 0, 100, true);
            }
            if (e)
            {
                Lasthit.AddSlider("lasthit.e.mana", "Use E if Mana is above {0}%", emana, 0, 100, true);
            }
            if (r)
            {
                Lasthit.AddSlider("lasthit.r.mana", "Use R if Mana is above {0}%", rmana, 0, 100, true);
            }
        }

        public static void HarassManaManager(bool q, bool w, bool e, bool r, int qmana, int wmana, int emana, int rmana)
        {
            if (q)
            {
                Harass.AddSlider("harass.q.mana", "Use Q if Mana is aboven {0}%", qmana, 0, 100, true);
            }
            if (w)
            {
                Harass.AddSlider("harass.w.mana", "Use W if Mana is above {0}%", wmana, 0, 100, true);
            }
            if (e)
            {
                Harass.AddSlider("harass.e.mana", "Use E if Mana is above {0}%", emana, 0, 100, true);
            }
            if (r)
            {
                Harass.AddSlider("harass.r.mana", "Use R if Mana is above {0}%", rmana, 0, 100, true);
            }
        }

        public static void FleeManaManager(bool q, bool w, bool e, bool r, int qmana, int wmana, int emana, int rmana)
        {
            if (q)
            {
                Flee.AddSlider("flee.q.mana", "Use Q if Mana is above {0}%", qmana, 0, 100, true);
            }
            if (w)
            {
                Flee.AddSlider("flee.w.mana", "Use W if Mana is above {0}%", wmana, 0, 100, true);
            }
            if (e)
            {
                Flee.AddSlider("flee.e.mana", "Use E if Mana is above {0}%", emana, 0, 100, true);
            }
            if (r)
            {
                Flee.AddSlider("flee.r.mana", "Use R if Mana is above {0}%", rmana, 0, 100, true);
            }
        }

        public static void KsManaManager(bool q, bool w, bool e, bool r, int qmana, int wmana, int emana, int rmana)
        {
            if (q)
            {
                Ks.AddSlider("killsteal.q.mana", "Use Q if Mana is above {0}%", qmana, 0, 100, true);
            }
            if (w)
            {
                Ks.AddSlider("killsteal.w.mana", "Use W if Mana is above {0}%", wmana, 0, 100, true);
            }
            if (e)
            {
                Ks.AddSlider("killsteal.e.mana", "Use E if Mana is above {0}%", emana, 0, 100, true);
            }
            if (r)
            {
                Ks.AddSlider("killsteal.r.mana", "Use R if Mana is above {0}%", rmana, 0, 100, true);
            }
        }

        /// <summary>
        ///     Combo Keys
        /// </summary>
        /// <param name="useQ">Create Q COMBO menu</param>
        /// <param name="defaultQ">Determinate toggle status</param>
        /// <param name="useW">Create W COMBO menu</param>
        /// <param name="defaultW">Determinate toggle status</param>
        /// <param name="useE">Create E COMBO menu</param>
        /// <param name="defaultE">Determinate toggle status</param>
        /// <param name="useR">Create R COMBO menu</param>
        /// <param name="defaultR">Determinate toggle status</param>
        public static void ComboKeys(
            bool useQ = true,
            bool defaultQ = true,
            bool useW = true,
            bool defaultW = true,
            bool useE = true,
            bool defaultE = true,
            bool useR = true,
            bool defaultR = true)
        {
            if (useQ)
            {
                Combo.AddCheckBox("combo.q".AddName(), "Use Q", defaultQ);
            }
            if (useW)
            {
                Combo.AddCheckBox("combo.w".AddName(), "Use W", defaultW);
            }
            if (useE)
            {
                Combo.AddCheckBox("combo.e".AddName(), "Use E", defaultE);
            }
            if (useR)
            {
                Combo.AddCheckBox("combo.r".AddName(), "Use R", defaultR);
            }
            Combo.AddSeparator();
            Combo.Add("combo.advanced", new CheckBox("Göster Gelişmiş Menu", false)).OnValueChange +=
                Value.AdvancedModeChanged;
            JsonSettings.Profile.Options.Add(new JsonSetting("combo.advanced", Setting.Checkbox, Combo["combo.advanced"].Cast<CheckBox>().CurrentValue.ToString()));
        }

        /// <summary>
        ///     KS Keys
        /// </summary>
        /// <param name="useQ">Create Q KS menu</param>
        /// <param name="defaultQ">Determinate toggle status</param>
        /// <param name="useW">Create W KS menu</param>
        /// <param name="defaultW">Determinate toggle status</param>
        /// <param name="useE">Create E KS menu</param>
        /// <param name="defaultE">Determinate toggle status</param>
        /// <param name="useR">Create R KS menu</param>
        /// <param name="defaultR">Determinate toggle status</param>
        public static void KsKeys(
            bool useQ = true,
            bool defaultQ = true,
            bool useW = true,
            bool defaultW = true,
            bool useE = true,
            bool defaultE = true,
            bool useR = true,
            bool defaultR = true)
        {
            if (useQ)
            {
                Ks.AddCheckBox("killsteal.q", "Q kullan", defaultQ);
            }
            if (useW)
            {
                Ks.AddCheckBox("killsteal.w", "W kullan", defaultW);
            }
            if (useE)
            {
                Ks.AddCheckBox("killsteal.e", "E kullan", defaultE);
            }
            if (useR)
            {
                Ks.AddCheckBox("killsteal.r", "R kullan", defaultR);
            }
            Ks.AddSeparator();
            Ks.Add("killsteal.advanced", new CheckBox("Göster Gelişmiş Menu", false)).OnValueChange +=
                Value.AdvancedModeChanged;
            JsonSettings.Profile.Options.Add(new JsonSetting("killsteal.advanced", Setting.Checkbox, Ks["killsteal.advanced"].Cast<CheckBox>().CurrentValue.ToString()));
        }

        /// <summary>
        ///     Lane Clear Keys
        /// </summary>
        /// <param name="useQ">Create Q LC menu</param>
        /// <param name="defaultQ">Determinate toggle status</param>
        /// <param name="useW">Create W LC menu</param>
        /// <param name="defaultW">Determinate toggle status</param>
        /// <param name="useE">Create E LC menu</param>
        /// <param name="defaultE">Determinate toggle status</param>
        /// <param name="useR">Create R LC menu</param>
        /// <param name="defaultR">Determinate toggle status</param>
        public static void LaneKeys(
            bool useQ = true,
            bool defaultQ = true,
            bool useW = true,
            bool defaultW = true,
            bool useE = true,
            bool defaultE = true,
            bool useR = true,
            bool defaultR = true)
        {
            Lane.AddGroupLabel("LaneClear Ayarları");
            if (useQ)
            {
                Lane.AddCheckBox("lane.q", "Kullan Q", false);
            }
            if (useW)
            {
                Lane.AddCheckBox("lane.w", "Kullan W", false);
            }
            if (useE)
            {
                Lane.AddCheckBox("lane.e", "Kullan E", false);
            }
            if (useR)
            {
                Lane.AddCheckBox("lane.r", "Kullan R", false);
            }
            Lane.AddSeparator();
            Lane.Add("lane.advanced", new CheckBox("Göster Gelişmiş Menu", false)).OnValueChange +=
                Value.AdvancedModeChanged;
            JsonSettings.Profile.Options.Add(new JsonSetting("lane.advanced", Setting.Checkbox, Lane["lane.advanced"].Cast<CheckBox>().CurrentValue.ToString()));
        }

        /// <summary>
        ///     Jungle Clear Keys
        /// </summary>
        /// <param name="useQ">Create Q JC menu</param>
        /// <param name="defaultQ">Determinate toggle status</param>
        /// <param name="useW">Create W JC menu</param>
        /// <param name="defaultW">Determinate toggle status</param>
        /// <param name="useE">Create E JC menu</param>
        /// <param name="defaultE">Determinate toggle status</param>
        /// <param name="useR">Create R JC menu</param>
        /// <param name="defaultR">Determinate toggle status</param>
        /// <param name="junglesteal">Determinate toggle status for JungleSteal</param>
        public static void JungleKeys(
            bool useQ = true,
            bool defaultQ = true,
            bool useW = true,
            bool defaultW = true,
            bool useE = true,
            bool defaultE = true,
            bool useR = true,
            bool defaultR = true,
            bool junglesteal = false)
        {
            Jungle.AddGroupLabel("JungleClear Ayarları");
            if (useQ)
            {
                Jungle.AddCheckBox("jungle.q", "Kullan Q", false);
            }
            if (useW)
            {
                Jungle.AddCheckBox("jungle.w", "Kullan W", false);
            }
            if (useE)
            {
                Jungle.AddCheckBox("jungle.e", "Kullan E", false);
            }
            if (useR)
            {
                Jungle.AddCheckBox("jungle.r", "Kullan R", false);
            }
            Jungle.AddSeparator();
            Jungle.Add("jungle.advanced", new CheckBox("Göster Gelişmiş Menu", false)).OnValueChange +=
                Value.AdvancedModeChanged;
            JsonSettings.Profile.Options.Add(new JsonSetting("jungle.advanced", Setting.Checkbox, Jungle["jungle.advanced"].Cast<CheckBox>().CurrentValue.ToString()));
            if (junglesteal)
            {
                JungleSteal();
            }
        }

        private static void JungleSteal()
        {
            Jungle.AddSeparator();
            Jungle.AddGroupLabel("Orman temizleme ayarları", "jungle.grouplabel", true);
            Jungle.AddCheckBox("jungle.stealenabled", "Enable Jungle Steal", true, true);
            if (Game.MapId == GameMapId.SummonersRift)
            {
                Jungle.AddLabel("Epics", 25, "jungle.label", true);
                Jungle.AddCheckBox("jungle.SRU_Baron", "Baron", true, true);
                Jungle.AddCheckBox("jungle.SRU_Dragon", "Ejder", true, true);
                Jungle.AddLabel("Bufflar", 25, "jungle.label.1", true);
                Jungle.AddCheckBox("jungle.SRU_Blue", "Mavi", false, true);
                Jungle.AddCheckBox("jungle.SRU_Red", "Kırmızı", false, true);
                Jungle.AddLabel("Küçük Kamplar", 25, "jungle.label.2", true);
                Jungle.AddCheckBox("jungle.SRU_Gromp", "Kurbağa", false, true);
                Jungle.AddCheckBox("jungle.SRU_Murkwolf", "AlacaKurtlar", false, true);
                Jungle.AddCheckBox("jungle.SRU_Krug", "Yampiri Yengeç", false, true);
                Jungle.AddCheckBox("jungle.SRU_Razorbeak", "SivriGagalar", false, true);
                Jungle.AddCheckBox("jungle.Sru_Crab", "Skuttles", false, true);
            }

            if (Game.MapId == GameMapId.TwistedTreeline)
            {
                Jungle.AddLabel("Epics", 25, "jungle.label.3", true);
                Jungle.AddCheckBox("TT_Spiderboss8.1", "Vilemaw", true, true);
                Jungle.AddLabel("Camps", 25, "jungle.label.4", true);
                Jungle.AddCheckBox("TT_NWraith1.1", "Wraith", false, true);
                Jungle.AddCheckBox("TT_NWraith4.1", "Wraith", false, true);
                Jungle.AddCheckBox("TT_NGolem2.1", "Golem", false, true);
                Jungle.AddCheckBox("TT_NGolem5.1", "Golem", false, true);
                Jungle.AddCheckBox("TT_NWolf3.1", "Wolf", false, true);
                Jungle.AddCheckBox("TT_NWolf6.1", "Wolf", false, true);
            }
        }

        /// <summary>
        ///     Last Hit Keys
        /// </summary>
        /// <param name="useQ">Create Q LH menu</param>
        /// <param name="defaultQ">Determinate toggle status</param>
        /// <param name="useW">Create W LH menu</param>
        /// <param name="defaultW">Determinate toggle status</param>
        /// <param name="useE">Create E LH menu</param>
        /// <param name="defaultE">Determinate toggle status</param>
        /// <param name="useR">Create R LH menu</param>
        /// <param name="defaultR">Determinate toggle status</param>
        public static void LastHitKeys(
            bool useQ = true,
            bool defaultQ = true,
            bool useW = true,
            bool defaultW = true,
            bool useE = true,
            bool defaultE = true,
            bool useR = true,
            bool defaultR = true)
        {
            //
            //  LastHit
            //
            Lasthit = Menu.AddSubMenu("LastHit Menu", "lasthit");
            Lasthit.AddGroupLabel("Last Hit Settings");
            if (useQ)
            {
                Lasthit.AddCheckBox("lasthit.q", "Kullan Q", false);
            }
            if (useW)
            {
                Lasthit.AddCheckBox("lasthit.w", "Kullan W", false);
            }
            if (useE)
            {
                Lasthit.AddCheckBox("lasthit.e", "Kullan E", false);
            }
            if (useR)
            {
                Lasthit.AddCheckBox("lasthit.r", "Kullan R", false);
            }
            Lasthit.AddSeparator();
            Lasthit.Add("lasthit.advanced", new CheckBox("Göster Gelişmiş Menu", false)).OnValueChange +=
                Value.AdvancedModeChanged;
            JsonSettings.Profile.Options.Add(new JsonSetting("lasthit.advanced", Setting.Checkbox, Lasthit["lasthit.advanced"].Cast<CheckBox>().CurrentValue.ToString()));
        }

        /// <summary>
        ///     Harass Keys
        /// </summary>
        /// <param name="useQ">Create Q Harass menu</param>
        /// <param name="defaultQ">Determinate toggle status</param>
        /// <param name="useW">Create W Harass menu</param>
        /// <param name="defaultW">Determinate toggle status</param>
        /// <param name="useE">Create E Harass menu</param>
        /// <param name="defaultE">Determinate toggle status</param>
        /// <param name="useR">Create R Harass menu</param>
        /// <param name="defaultR">Determinate toggle status</param>
        public static void HarassKeys(
            bool useQ = true,
            bool defaultQ = true,
            bool useW = true,
            bool defaultW = true,
            bool useE = true,
            bool defaultE = true,
            bool useR = true,
            bool defaultR = true)
        {
            if (useQ)
            {
                Harass.AddCheckBox("harass.q", "Kullan Q");
            }
            if (useW)
            {
                Harass.AddCheckBox("harass.w", "Kullan W");
            }
            if (useE)
            {
                Harass.AddCheckBox("harass.e", "Kullan E");
            }
            if (useR)
            {
                Harass.AddCheckBox("harass.r", "Kullan R", false);
            }
            Harass.AddSeparator();
            Harass.Add("harass.advanced", new CheckBox("Göster Gelişmiş Menü", false)).OnValueChange +=
                Value.AdvancedModeChanged;
            JsonSettings.Profile.Options.Add(new JsonSetting("harass.advanced", Setting.Checkbox, Harass["harass.advanced"].Cast<CheckBox>().CurrentValue.ToString()));
        }

        public static void FleeKeys(
            bool useQ = true,
            bool defaultQ = true,
            bool useW = true,
            bool defaultW = true,
            bool useE = true,
            bool defaultE = true,
            bool useR = true,
            bool defaultR = true)
        {
            Flee = Menu.AddSubMenu("Flee Menu", "flee");
            Flee.AddGroupLabel("Flee Settings");
            if (useQ)
            {
                Flee.AddCheckBox("flee.q", "Kullan Q");
            }
            if (useW)
            {
                Flee.AddCheckBox("flee.w", "Kullan W");
            }
            if (useE)
            {
                Flee.AddCheckBox("flee.e", "Kullan E");
            }
            if (useR)
            {
                Flee.AddCheckBox("flee.r", "Kullan R ", false);
            }
            Flee.AddSeparator();
            Flee.Add("flee.advanced", new CheckBox("Göster Gelişmiş Menu", false)).OnValueChange +=
                Value.AdvancedModeChanged;
            JsonSettings.Profile.Options.Add(new JsonSetting("flee.advanced", Setting.Checkbox, Flee["flee.advanced"].Cast<CheckBox>().CurrentValue.ToString()));
        }
    }
}