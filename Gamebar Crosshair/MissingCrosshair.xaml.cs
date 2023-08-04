using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gamebar_Crosshair
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MissingCrosshair : Page
    {
        public MissingCrosshair()
        {
            this.InitializeComponent();
        }

        private async void OpenSteamAppButton_Click(object sender, RoutedEventArgs e)
        {
            string uriToLaunch = @"https://store.steampowered.com/app/2385900/Crosshair_Magic__in_game_overlay/";
            var uri = new Uri(uriToLaunch);
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);

            //if (success)
            //{
            //    // URI launched
            //}
            //else
            //{
            //    // URI launch failed
            //}
        }
    }
}
