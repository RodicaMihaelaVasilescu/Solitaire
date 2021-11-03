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
  public class DisplayPlaceholderCardConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      var cards = (IEnumerable<Card>)values[0];
      return new BitmapImage(new Uri(string.Format("pack://application:,,,/Resources/Cards/{0}.png", cards.Last().Value)));
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
