using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Model
{
  public abstract class RegionBase : BindableBase
  {
    public string RegionName { get; set; }

    private ObservableCollection<ListOfCards> _cards;

    public ObservableCollection<ListOfCards> PilesOfCards
    {
      get
      {
        return _cards;
      }
      set
      {
        _cards = value;
        OnPropertyChanged();
      }
    }
    public virtual void InitializeCards()
    {
      PilesOfCards = new ObservableCollection<ListOfCards>();
    }
  }
}
