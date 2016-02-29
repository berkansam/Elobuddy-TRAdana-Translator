namespace Jinx
{
    using EloBuddy;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;

    internal class Config
    {
        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu ConfigMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu ComboMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu LastHitMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu HarassMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu LaneClearMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu KillStealMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu JungleClearMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu JungleStealMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu FleeMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu DrawingMenu;

        /// <summary>
        /// Initializes and Contains the Menu.
        /// </summary>
        public static Menu MiscMenu;

        /// <summary>
        /// Creates the Menu
        /// </summary>
        public static void Initialize()
        {
            ConfigMenu = MainMenu.AddMenu("Jin-XXX", "Jin-XXX");
            ConfigMenu.AddGroupLabel("Bu Addon KarmaPanda tarafından geliştirilmiştir iyi oyunlar");
            ConfigMenu.AddGroupLabel("Any unauthorized redistribution without credits will result in severe consequences.");
            ConfigMenu.AddGroupLabel("Kullandığın için teşekkürler iyi oyunlar!");
            ConfigMenu.AddGroupLabel("Çeviri TRAdana");

            ComboMenu = ConfigMenu.AddSubMenu("Kombo", "Combo");
            ComboMenu.AddLabel("Kombo");
            ComboMenu.Add("useQ", new CheckBox("Kullan Q"));
            ComboMenu.Add("useW", new CheckBox("Kullan W"));
            ComboMenu.Add("useE", new CheckBox("Kullan E"));
            ComboMenu.Add("useR", new CheckBox("Kullan R"));
            ComboMenu.AddLabel("ManaYardımcısı");
            ComboMenu.Add("manaQ", new Slider("Q için gereken mana %", 25));
            ComboMenu.Add("manaW", new Slider("W için gereken mana %", 25));
            ComboMenu.Add("manaE", new Slider("E için gereken mana %", 25));
            ComboMenu.Add("manaR", new Slider("R için gereken mana %", 25));
            ComboMenu.AddLabel("Lütfen bu bölümü ayarlayın kendinize göre");
            ComboMenu.Add("qCountC", new Slider("Q şu kadar şampiyona çarpacaksa", 3, 1, 5));
            ComboMenu.Add("rCountC", new Slider("R şu kadar şampiyona çarpacaksa", 5, 1, 5));
            ComboMenu.AddLabel("Prediction Settings");
            ComboMenu.Add("wSlider", new Slider("W İsabet Oranı %  {0}", 80));
            ComboMenu.Add("eSlider", new Slider("E İsabet Oranı %  {0}", 80));
            ComboMenu.Add("rSlider", new Slider("R İsabet Oranı %  {0}", 80));
            ComboMenu.AddLabel("Extra Settings");
            ComboMenu.Add("wRange2", new Slider("W sadece hedef şu menzilden uzaktaysa{0}", 150, 0, 1450));
            ComboMenu.Add("eRange", new Slider("E sadece hedef şu menzildeden uzaktaysa {0}", 150, 0, 900));
            ComboMenu.Add("eRange2", new Slider("en fazla  E menzili", 900, 0, 900));
            ComboMenu.Add("rRange2", new Slider("En fazlA R Menzili", 3000, 0, 3000));

            LastHitMenu = ConfigMenu.AddSubMenu("SonVuruş", "LastHit");
            LastHitMenu.AddGroupLabel("SonVuruş");
            LastHitMenu.Add("useQ", new CheckBox("Kullan Q"));
            LastHitMenu.Add("qCountM", new Slider("Q şu kadar minyona çarpacaksa", 3, 1, 7));
            LastHitMenu.AddLabel("ManaManager");
            LastHitMenu.Add("manaQ", new Slider("ManaManager for Q", 25));

            HarassMenu = ConfigMenu.AddSubMenu("Dürtme", "Harass");
            HarassMenu.AddLabel("Dürtme Ayarları");
            HarassMenu.Add("useQ", new CheckBox("Kullan Q"));
            HarassMenu.Add("useW", new CheckBox("Kullan W"));
            HarassMenu.AddLabel("Mana Yardımcısı");
            HarassMenu.Add("manaQ", new Slider("Q için gereken mana %", 15));
            HarassMenu.Add("manaW", new Slider("W için gereken mana %", 35));
            HarassMenu.AddLabel("Bu bölümü lütfen ayarlayın kendinize göre");
            HarassMenu.Add("qCountC", new Slider("Q İsabet Oranı {0} Champion(s)", 3, 1, 5));
            HarassMenu.Add("qCountM", new Slider("Q İsabet Oranı {0} Minion(s)", 3, 1, 7));
            HarassMenu.AddLabel("İsabet Oranı Ayarları");
            HarassMenu.Add("wSlider", new Slider("W İsabet Oranı % {0}", 95));
            HarassMenu.AddLabel("Ekstra Ayarları");
            HarassMenu.Add("wRange2", new Slider("Hedef Menzilimdeyse Lütfen W Kullanma {0}", 0, 0, 1450));

            LaneClearMenu = ConfigMenu.AddSubMenu("LaneTemizleme", "LaneClear");
            LaneClearMenu.AddLabel("LaneTemizleme Ayarları");
            LaneClearMenu.Add("useQ", new CheckBox("Kullan Q"));
            LaneClearMenu.Add("lastHit", new CheckBox("Minyon menzilden çıkarsa Son Vuruş Q", false));
            LaneClearMenu.Add("manaQ", new Slider("Q için gereken mana %", 25));
            LaneClearMenu.Add("qCountM", new Slider("şu kadar minyona çarpacaksa ", 3, 1, 7));

            KillStealMenu = ConfigMenu.AddSubMenu("Kill Çalma", "KillSteal");
            KillStealMenu.Add("toggle", new CheckBox("Kill Çalma Kullan"));
            KillStealMenu.Add("useW", new CheckBox("KS'de W Kullan"));
            KillStealMenu.Add("useR", new CheckBox("KS'de R Kullan"));
            KillStealMenu.AddLabel("Mana Yardımcısı");
            KillStealMenu.Add("manaW", new Slider("W için gereken mana %", 25));
            KillStealMenu.Add("manaR", new Slider("E için gereken mana %", 25));
            KillStealMenu.AddLabel("İsabet Oranı Ayarları");
            KillStealMenu.Add("wSlider", new Slider("W İsabet Oranı % {0}", 80));
            KillStealMenu.Add("rSlider", new Slider("E İsabet Oranı % is {0}", 80));
            KillStealMenu.AddLabel("Spell Ayarları");
            KillStealMenu.Add("wRange", new Slider("En az W menzili", 450, 0, 3000));
            KillStealMenu.Add("rRange", new Slider("En fazla R Menzili", 3000, 0, 3000));

            JungleClearMenu = ConfigMenu.AddSubMenu("OrmanTemizleme", "JungleClear");
            JungleClearMenu.AddLabel("OrmanTemizleme Ayarları");
            JungleClearMenu.Add("useQ", new CheckBox("Kullan Q"));
            JungleClearMenu.Add("useW", new CheckBox("Kullan W", false));
            JungleClearMenu.AddLabel("Mana Yardımcısı");
            JungleClearMenu.Add("manaQ", new Slider("Q için gereken mana %", 25));
            JungleClearMenu.Add("manaW", new Slider("W için gereken mana %", 25));
            JungleClearMenu.AddLabel("Misc Settings");
            JungleClearMenu.Add("wSlider", new Slider("Use W if HitChance % is {0}", 85));

            JungleStealMenu = ConfigMenu.AddSubMenu("Orman Çalma", "JungleSteal");
            JungleStealMenu.AddLabel("Orman Çalma Ayarları");
            JungleStealMenu.Add("toggle", new CheckBox("Orman Çalma Kullan", false));
            JungleStealMenu.Add("manaR", new Slider("R için gerekli mana", 25));
            JungleStealMenu.Add("rRange", new Slider("R atmak için en fazla menzil", 3000, 0, 3000));
            if (Game.MapId == GameMapId.SummonersRift)
            {
                JungleStealMenu.AddLabel("Efsanevi");
                JungleStealMenu.Add("SRU_Baron", new CheckBox("Baron"));
                JungleStealMenu.Add("SRU_Dragon", new CheckBox("Ejder"));
                JungleStealMenu.AddLabel("Bufflar");
                JungleStealMenu.Add("SRU_Blue", new CheckBox("Mavi", false));
                JungleStealMenu.Add("SRU_Red", new CheckBox("Kırmızı", false));
                JungleStealMenu.AddLabel("Küçük Kamplar");
                JungleStealMenu.Add("SRU_Gromp", new CheckBox("Kurbağa", false));
                JungleStealMenu.Add("SRU_Murkwolf", new CheckBox("AlacaKurt", false));
                JungleStealMenu.Add("SRU_Krug", new CheckBox("Golem", false));
                JungleStealMenu.Add("SRU_Razorbeak", new CheckBox("SivriGagalar", false));
                JungleStealMenu.Add("Sru_Crab", new CheckBox("Yampiri Yengeç", false));
            }

            if (Game.MapId == GameMapId.TwistedTreeline)
            {
                JungleStealMenu.AddLabel("Epics");
                JungleStealMenu.Add("TT_Spiderboss8.1", new CheckBox("Örümcek"));
                JungleStealMenu.AddLabel("Kamplar");
                JungleStealMenu.Add("TT_NWraith1.1", new CheckBox("Wraith", false));
                JungleStealMenu.Add("TT_NWraith4.1", new CheckBox("Wraith", false));
                JungleStealMenu.Add("TT_NGolem2.1", new CheckBox("Golem", false));
                JungleStealMenu.Add("TT_NGolem5.1", new CheckBox("Golem", false));
                JungleStealMenu.Add("TT_NWolf3.1", new CheckBox("Kurt", false));
                JungleStealMenu.Add("TT_NWolf6.1", new CheckBox("Kurt", false));
            }

            FleeMenu = ConfigMenu.AddSubMenu("Flee", "Flee");
            FleeMenu.AddLabel("Flee Ayarları");
            FleeMenu.Add("useW", new CheckBox("Kaçarken W Kullan"));
            FleeMenu.Add("useE", new CheckBox("Kaçarken E Kullan"));
            FleeMenu.Add("wSlider", new Slider("W İsabet Oranı {0}", 75));
            FleeMenu.Add("eSlider", new Slider("E İsabet Oranı {0}", 75));

            DrawingMenu = ConfigMenu.AddSubMenu("Gösterge", "Drawing");
            DrawingMenu.AddLabel("Gösterge Ayarları");
            DrawingMenu.Add("drawQ", new CheckBox("Göster Q Range"));
            DrawingMenu.Add("drawW", new CheckBox("Göster W Range"));
            DrawingMenu.Add("drawE", new CheckBox("Göster E Range", false));
            DrawingMenu.AddLabel("İsabet Oranı Ayarları");
            DrawingMenu.Add("predW", new CheckBox("Göster W İsabet Oranı"));
            DrawingMenu.Add("predR", new CheckBox("Göster R İsabet Oranı (In consideration of Range before R)", false));
            DrawingMenu.AddLabel("DamageIndicator");
            DrawingMenu.Add("draw.Damage", new CheckBox("Draw Damage"));
            DrawingMenu.Add("draw.Q", new CheckBox("Calculate Q Damage", false));
            DrawingMenu.Add("draw.W", new CheckBox("Calculate W Damage"));
            DrawingMenu.Add("draw.E", new CheckBox("Calculate E Damage", false));
            DrawingMenu.Add("draw.R", new CheckBox("Calculate R Damage"));
            DrawingMenu.AddLabel("Renk Ayarları Hasar Tespitçisi İçin");
            DrawingMenu.Add("draw_Alpha", new Slider("Alpha: ", 255, 0, 255));
            DrawingMenu.Add("draw_Red", new Slider("Kırmızı: ", 255, 0, 255));
            DrawingMenu.Add("draw_Green", new Slider("Yeşil: ", 0, 0, 255));
            DrawingMenu.Add("draw_Blue", new Slider("Mavi: ", 0, 0, 255));

            MiscMenu = ConfigMenu.AddSubMenu("Ek Menu", "Misc");
            MiscMenu.AddLabel("Interrupter");
            MiscMenu.Add("interruptE", new CheckBox("İnterrupt için E"));
            MiscMenu.Add("interruptmanaE", new Slider("İnterrupt için E gerekli mana", 25));
            MiscMenu.AddLabel("Gapcloser");
            MiscMenu.Add("gapcloserE", new CheckBox("Gapcloser için  E"));
            MiscMenu.Add("gapclosermanaE", new Slider("Gapcloser için  E gereken mana", 25));
            MiscMenu.AddLabel("Büyü Ayarları");
            MiscMenu.Add("autoW", new CheckBox("Belli Durumlarda Otomatik W Kullan"));
            MiscMenu.Add("autoE", new CheckBox("Belli Durumlarda Otomatik E Kullan"));
            MiscMenu.Add("wRange", new CheckBox("Wyi sadece hedef menzilimdeyse kullan", false));
            MiscMenu.Add("rRange", new Slider("Eğer hedef şu menzildeyse R Kullanma {0}", 800, 0, 3000));
            MiscMenu.AddLabel("Otomatik W Ayarları (Otomatik W açmalısın)");
            MiscMenu.Add("stunW", new CheckBox("W kullan sabit hedefe", false));
            MiscMenu.Add("charmW", new CheckBox("Use W on Charmed Enemy", false));
            //MiscMenu.Add("tauntW", new CheckBox("Use W on Taunted Enemy", false));
            MiscMenu.Add("fearW", new CheckBox("Korkmuş Hedefe W Kullan", false));
            MiscMenu.Add("snareW", new CheckBox("Yavaşlamış hedefe W kullan", false));
            MiscMenu.Add("wRange2", new Slider("W yi sadece hedef şu menzilden çıkarsa kullan {0}", 450, 0, 1450));
            MiscMenu.AddLabel("İsabet Oranı Ayarları");
            MiscMenu.Add("wSlider", new Slider("İsabet Oranı W % is {0}", 75));
            MiscMenu.Add("eSlider", new Slider("E İsabet Oranı % is {0}", 75));
            MiscMenu.AddLabel("Allah Akbar");
            MiscMenu.Add("allahAkbarT", new CheckBox("Allahu Ekbeeeeeerrrrr!!! (R)", false));
        }
    }
}
