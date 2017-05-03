using MyWork.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyWork.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyWorkPage : Page
    {
        public static double ScreenWidth = 0;
        public static double ScreenHeight = 0;
        public MyWorkPage()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            ScreenWidth = bounds.Width;
            ScreenHeight = bounds.Height;
            //var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            //var size = new Size(bounds.Width * scaleFactor, bounds.Height * scaleFactor);
        }

        public MyWorkPageViewModel ViewModel
        {
            get
            {
                var dataContext = this.DataContext as MyWorkPageViewModel;
                if (dataContext == null)
                {
                    dataContext = MyWorkPageViewModel.GetInstance();//MainPageViewModel();
                    //dataContext.CurrentView = this;
                }
                return dataContext;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.RefreshPage();
        }

        private void Rectangle_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            //dragTranslation.X += e.Delta.Translation.X;
            //dragTranslation.Y += e.Delta.Translation.Y;
            ViewModel.PossitionX += e.Delta.Translation.X;
            ViewModel.PossitionY += e.Delta.Translation.Y;
        }

        private void Rectangle_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            double newX = ViewModel.PossitionX;
            double newY = ViewModel.PossitionX;
            if (newX < ScreenWidth / 3)
            {
                newX = 0;
            }
            else if (newX < 2 * ScreenWidth / 3)
            {
                newX = ScreenWidth / 3;
            }
            else
            {
                newX = 2 * ScreenWidth / 3;
            }
            ViewModel.PossitionX = newX;
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Rectangle_ManipulationDelta_1(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var currentX = ViewModel.PossitionX;
            var currentY = ViewModel.PossitionY;
            if (currentX + e.Delta.Translation.X < 0)
            {
                ViewModel.PossitionX = 0;
            }
            else if (currentX + e.Delta.Translation.X > ScreenWidth)
            {
                ViewModel.PossitionX = ScreenWidth - 30;
            }
            else
            {
                ViewModel.PossitionX += e.Delta.Translation.X;
            }

            if (currentY + e.Delta.Translation.Y < 0)
            {
                ViewModel.PossitionY = 0;
            }
            else if (currentY + e.Delta.Translation.Y > ScreenHeight)
            {
                ViewModel.PossitionY = ScreenHeight - 100;
            }
            else
            {
                ViewModel.PossitionY += e.Delta.Translation.Y;
            }           
        }

        private void Rectangle_ManipulationCompleted_1(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            double newX = ViewModel.PossitionX;
            double newY = ViewModel.PossitionX;
            if (newX < ScreenWidth / 4)
            {
                newX = 0;
            }
            else if (newX < ScreenWidth / 2)
            {
                newX = ScreenWidth / 4;
            }
            else if (newX < 3 * ScreenWidth / 4)
            {
                newX = ScreenWidth / 2;
            }
            else
            {
                newX = 3 * ScreenWidth / 4;
            }
            ViewModel.PossitionX = newX;
        }

        private void Rectangle_ManipulationDelta_2(object sender, ManipulationDeltaRoutedEventArgs e)
        {

        }

        private void Rectangle_ManipulationCompleted_2(object sender, ManipulationCompletedRoutedEventArgs e)
        {

        }

        private void Rectangle_ManipulationDelta_3(object sender, ManipulationDeltaRoutedEventArgs e)
        {

        }

        private void Rectangle_ManipulationCompleted_3(object sender, ManipulationCompletedRoutedEventArgs e)
        {

        }

        private void Rectangle_ManipulationDelta_4(object sender, ManipulationDeltaRoutedEventArgs e)
        {

        }

        private void Rectangle_ManipulationCompleted_4(object sender, ManipulationCompletedRoutedEventArgs e)
        {

        }
    }
}
