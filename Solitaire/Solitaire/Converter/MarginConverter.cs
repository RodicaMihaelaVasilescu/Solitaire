using Solitaire.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Solitaire.Converter
{
  public class MarginConverter : IMultiValueConverter
  {
    public object Convert(object[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      var cardList = (IEnumerable<Card>)value[1];
      var card = cardList.First();
      string cardValue = card == null ? string.Empty : card.Value;
      if (cardValue == (string)value[0])
      {
        return new Thickness(5, 0, 5, 0);
      }
      else
      {
        return new Thickness(5, -130, 5, 0);
      }
    }

    public object[] ConvertBack(object value, Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return null;
    }
  }
}