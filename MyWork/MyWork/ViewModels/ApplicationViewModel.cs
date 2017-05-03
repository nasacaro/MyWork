using MyWork.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MyWork.ViewModels
{
    public class ApplicationViewModel : BaseViewModel
    {
        private static ApplicationViewModel _default;

        public static ApplicationViewModel Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new ApplicationViewModel();
                }
                return _default;
            }
        }

        public void NavigateToMainPage()
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.CacheSize = 1;
                Window.Current.Content = rootFrame;
            }
            rootFrame.Navigate(typeof(MainPage));
        }

        public void NavigateToMyWorkPage()
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.CacheSize = 1;
                Window.Current.Content = rootFrame;
            }
            rootFrame.Navigate(typeof(MyWorkPage));
        }
    }
}
