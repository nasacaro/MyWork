using MyWork.ViewModels;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyWork
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }

        public MainPageViewModel ViewModel
        {
            get
            {
                var dataContext = this.DataContext as MainPageViewModel;
                if (dataContext == null)
                {
                    dataContext = MainPageViewModel.GetInstance();//MainPageViewModel();
                    //dataContext.CurrentView = this;
                }
                return dataContext;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.RefreshPage();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox item = (TextBox)sender;
            if(item != null)
            {
                ViewModel.SearchUserFromList(item.Text);
            }
        }
    }
}
