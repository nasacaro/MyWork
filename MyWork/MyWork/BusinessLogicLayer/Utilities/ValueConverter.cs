using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MyWork.BusinessLogicLayer.Utilities
{
    public class IndexToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            int tabIndex = System.Convert.ToInt16(parameter);
            int selectedIndex = System.Convert.ToInt16(value);
            if (tabIndex == selectedIndex)
            {
                return ColorUtils.GetColorFromHex("#00A2E8");
            }
            else
            {
                return new SolidColorBrush(Colors.Transparent);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class HeaderTextToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            int tabIndex = System.Convert.ToInt16(parameter);
            int selectedIndex = System.Convert.ToInt16(value);
            if (tabIndex == selectedIndex)
            {
                return new SolidColorBrush(Colors.White);
            }
            else
            {
                return ColorUtils.GetColorFromHex("#00A2E8");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
