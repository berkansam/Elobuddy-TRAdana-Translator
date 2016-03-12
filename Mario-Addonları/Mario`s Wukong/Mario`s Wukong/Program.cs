using System;
using EloBuddy.SDK.Events;

namespace Mario_sTemplate
{
    internal class Program
    {
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            try
            {
                Spells.InitSpells();
                MenuSettings.LoadMenu();
                ModeManager.InitModeManager();
                EventsManager.InitEventManagers();

                Ultilities.DamageIndicator.Init();
            }
            catch (Exception exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("============================= ERROR =============================");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(exp);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("============================= ERROR =============================");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}
