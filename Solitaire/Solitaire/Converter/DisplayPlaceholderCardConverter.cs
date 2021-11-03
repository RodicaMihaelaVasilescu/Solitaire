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
      var index = (int)values[1];
      var cards = (IEnumerable<IEnumerable<Card>>)values[0];
      var list = cards.ElementAt(index);
      return new BitmapImage(new Uri(string.Format("pack://application:,,,/Resources/Cards/{0}.png", list.Last().Value)));

      //}
      //else
      //{
      //  return new BitmapImage(new Uri(string.Format("pack://application:,,,/Resources/Cards/{0}.png", value)));

      //}
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
