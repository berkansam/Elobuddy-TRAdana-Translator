using System;
using System.Linq;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Rendering;
using MasterMind.Components;

namespace MasterMind
{
    public static class MasterMind
    {
        public static readonly TextureLoader TextureLoader = new TextureLoader();

        public static bool IsSpectatorMode { get; private set; }

        public static Menu Menu { get; private set; }

        private static readonly IComponent[] Components =
        {
            new CooldownTracker(),
            new WardTracker()
        };

        public static void Main(string[] args)
        {
            // Load the addon in a real match and when spectating games
            Loading.OnLoadingComplete += OnLoadingComplete;
            Loading.OnLoadingCompleteSpectatorMode += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            // Initialize menu
            Menu = MainMenu.AddMenu("MasterMind", "MasterMind", "MasterMind - Improve Yourself!");

            Menu.AddGroupLabel("MasterMind'e hoşgeldin, iyi vakit geçirmen dileğiyle");
            Menu.AddLabel("Bu addon sizin oyun deneyiminizi geliştirecek bazı özellikler sunuyor");
            Menu.AddLabel("FPS drop yapmadan eğlenebilirsiniz");
            Menu.AddSeparator();
            Menu.AddLabel("Alt Menulere göz atin, kendinize göre düzenleyin, gerek yok bildiğin ayarlar işte onu göster bunu göster");

            // Initialize properties
            IsSpectatorMode = Bootstrap.IsSpectatorMode;

            // Initialize components
            foreach (var component in Components.Where(component => component.ShouldLoad(IsSpectatorMode)))
            {
                component.InitializeComponent();
            }
        }
    }
}
