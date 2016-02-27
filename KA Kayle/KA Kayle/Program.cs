using System;
using EloBuddy.SDK.Events;

namespace KA_Kayle
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Kayle.Initialize();
        }
    }
}
