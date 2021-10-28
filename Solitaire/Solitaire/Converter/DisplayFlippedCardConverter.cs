using Solitaire.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Solitaire.Converter
{
  public class DisplayFlippedCardConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      bool isFlipped = (bool)values[0];
      string value = (string)values[1];
      if (isFlipped)
      {
        return new BitmapImage(new Uri("pack://application:,,,/Resources/red_back.png"));

      }
      else
      {
        return new BitmapImage(new Uri(string.Format("pack://application:,,,/Resources/Cards/{0}.png", value)));

      }
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
