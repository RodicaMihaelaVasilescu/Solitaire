using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Model
{
  public class Card : BindableBase
  {
    public Guid Id { get; set; }

    public string Value { get; set; }

    public int Size { get; set; } = 150;

    public string DisplayedImage
    {
      get
      {
        return string.Format("../../Resources/Cards/{0}.png", Value);
      }
    }

    public int Index { get; set; }
    public bool IsFlipped { get;  set; }
  }
}
