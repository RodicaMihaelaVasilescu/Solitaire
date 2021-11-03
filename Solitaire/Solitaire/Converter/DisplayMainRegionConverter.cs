using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Solitaire.Converter
{
  public class DisplayMainRegionConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      bool frontImageDisplayed = (bool)values[0];
      string value = (string)values[1];
      if (frontImageDisplayed || value == "placeholder")
      {
        return new BitmapImage(new Uri(string.Format("pack://application:,,,/Resources/Cards/{0}.png", value)));
      }
      else
      {
        return new BitmapImage(new Uri("pack://application:,,,/Resources/red_back.png"));
      }
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
