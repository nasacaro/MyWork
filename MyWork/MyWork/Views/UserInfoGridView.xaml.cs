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
    public sealed partial class UserInfoGridView : UserControl
    {
        public UserInfoGridView()
        {
            this.InitializeComponent();
        }

        private void OnGridViewSizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                ItemsWrapGrid itemsPanel = (ItemsWrapGrid)GrVDocuments.ItemsPanelRoot;
                double margin = 10.0;
                var itemNumberPerRow = 3;
                itemsPanel.ItemWidth = (e.NewSize.Width - margin * itemNumberPerRow) / (double)itemNumberPerRow;
                itemsPanel.ItemHeight = 110;
            }
            catch (Exception) { }
        }

        private void Rectangle_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            //dragTranslation.X += e.Delta.Translation.X;
            //dragTranslation.Y += e.Delta.Translation.Y;
        }

        private void Rectangle_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {

        }
    }
}
