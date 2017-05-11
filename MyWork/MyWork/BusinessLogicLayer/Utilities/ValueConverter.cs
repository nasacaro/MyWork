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

    public class BacklogItemBackgroudColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            int priority = System.Convert.ToInt16(value);
            switch (priority)
            {
                case 0:
                    return ColorUtils.GetColorFromHex("#F2575F");
                case 1:
                    return ColorUtils.GetColorFromHex("#FFFF80");
                case 2:
                    return ColorUtils.GetColorFromHex("#51A8FF");
                case 3:
                    return ColorUtils.GetColorFromHex("#CDCDCD");
                default:
                    return ColorUtils.GetColorFromHex("#CDCDCD");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
