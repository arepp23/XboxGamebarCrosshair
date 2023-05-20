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
using Windows.UI.Xaml.Shapes;
using Microsoft.Gaming.XboxGameBar;
using System.Diagnostics;
using Microsoft.Gaming.XboxGameBar.Authentication;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Gamebar_Crosshair
{

    public sealed partial class Crosshair : Page
    {
        private XboxGameBarWidget widget = null;

        static Rectangle verticalLine = new Rectangle();
        static Rectangle horizontalLine = new Rectangle();

        public Crosshair()
        {
            Debug.WriteLine("In Constructor");
            this.InitializeComponent();

            //layoutRoot.Children.Add(verticalLine);
            //layoutRoot.Children.Add(horizontalLine);

            //drawCrosshair();

            

            BitmapImage bitmapImage = new BitmapImage();
            // Call BaseUri on the root Page element and combine it with a relative path
            // to consruct an absolute URI.
            //bitmapImage.UriSource = new Uri(this.BaseUri, @"Assets/Dropoff.png");
            //capturedPhoto.Source = bitmapImage;
            loadImage();
        }

        private async void loadImage()
        {
            Windows.Storage.StorageFolder storageFolder =Windows.Storage.ApplicationData.Current.LocalFolder;
            var localPath = @"C:\green.png";
    //        Windows.Storage.StorageFolder localFolder =
    //Windows.Storage.KnownFolders.PicturesLibrary;
    //        Windows.Storage.StorageFile sampleFile = await localFolder.GetFileAsync("green.png");
            try
            {
                Windows.Storage.StorageFile sampleFile = await StorageFile.GetFileFromPathAsync(localPath);
                using (IRandomAccessStream fileStream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap
                    BitmapImage bitmapImage = new BitmapImage();
                    // Decode pixel sizes are optional
                    // It's generally a good optimisation to decode to match the size you'll display
                    bitmapImage.DecodePixelHeight = 100;
                    bitmapImage.DecodePixelWidth = 100;

                    await bitmapImage.SetSourceAsync(fileStream);
                    capturedPhoto.Source = bitmapImage;
                }
            }
            catch (System.UnauthorizedAccessException e)
            {
                Debug.WriteLine("Missing perms");
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof(MissingPermissions));
            }
        }

        static void drawCrosshair()
        {
            verticalLine.Fill = new SolidColorBrush(CrosshairData.color);
            verticalLine.Width = CrosshairData.thickness;
            verticalLine.Height = CrosshairData.length;

            horizontalLine.Fill = new SolidColorBrush(CrosshairData.color);
            horizontalLine.Width = CrosshairData.length;
            horizontalLine.Height = CrosshairData.thickness;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            widget = e.Parameter as XboxGameBarWidget;

            widget.SettingsClicked += Widget_SettingsClicked;
            widget.GameBarDisplayModeChanged += Widget_GameBarDisplayModeChangedAsync;
        }

        private async void Widget_GameBarDisplayModeChangedAsync(XboxGameBarWidget sender, object args)
        {
            Debug.WriteLine("Game Bar View Mode: " + widget.GameBarDisplayMode.ToString());
            if (widget.GameBarDisplayMode == XboxGameBarDisplayMode.PinnedOnly)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    CenterButton.Visibility = Visibility.Collapsed;
                    UpArrow.Visibility = Visibility.Collapsed;
                    PinText.Visibility = Visibility.Collapsed;
                });
                
            }
            else
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    CenterButton.Visibility = Visibility.Visible;
                    UpArrow.Visibility = Visibility.Visible;
                    PinText.Visibility = Visibility.Visible;
                });
            }
        }

        private async void Widget_SettingsClicked(XboxGameBarWidget sender, object args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                drawCrosshair();
            });

            await widget.ActivateSettingsAsync();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await widget.CenterWindowAsync();
        }
    }
}