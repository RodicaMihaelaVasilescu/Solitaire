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

    private string _displayedImage;

    public string DisplayedImage
    {
      get
      {
        return string.Format("../../Resources/Cards/{0}.png", Value);
      }
      //set
      //{
      //  _displayedImage = value;
      //  OnPropertyChanged();
      //}
    }
  }
}
