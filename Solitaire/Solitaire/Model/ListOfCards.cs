using Solitaire.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Solitaire.Model
{
  public class ListOfCards : BindableBase
  {
    private ObservableCollection<Card> _cards;

    public ListOfCards(List<Card> cards, int index = 0)
    {
      Cards = new ObservableCollection<Card>(cards);
      Index = index;
    }

    public ObservableCollection<Card> Cards
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

    public int Index { get; set; }

    public void Refresh()
    {
      CollectionViewSource.GetDefaultView(Cards).Refresh();
    }
  }
}
