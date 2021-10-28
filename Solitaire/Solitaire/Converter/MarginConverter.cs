using System;
using System.Windows;
using System.Windows.Data;

namespace Solitaire.Converter
{
  public class MarginConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if ((int)value == 0)
      {
        return new Thickness(0);
      }
      else
      {
        return new Thickness(0, -130, 0, 0);
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return null;
    }
  }
}