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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyWork.Views
{
    public sealed partial class SideloadDataDialog : UserControl
    {
        public SideloadDataDialog()
        {
            this.InitializeComponent();
        }

        private void CloseRateUsView(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = (MainPageViewModel)this.DataContext;
            if(dataContext != null)
            {
                dataContext.HideSideloadDataDialog();
            }
        }
    }
}
