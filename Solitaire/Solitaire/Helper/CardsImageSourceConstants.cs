using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Helper
{
  public static class CardsImageSourceConstants
  {
    static public string Bomb = "../../Resources/red_back.png";
    static public string GetCardByValue(string value)
    {
      return string.Format("../../Resources/Card/{0}.png", value);
    }
  }
}
