using Solitaire.Constants;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Solitaire.Model
{
  public class TargetPileOfCardsRegion : RegionBase
  {
    public TargetPileOfCardsRegion()
    {
      InitializeCards();
    }

    public override void InitializeCards()
    {
      PilesOfCards = new ObservableCollection<ListOfCards>();

      for (int i = 0; i < 4; i++)
      {
        var placeholder = new List<Card> { new Card { Value = CardsConstants.Placeholder } };
        PilesOfCards.Add(new ListOfCards(placeholder, i));
      }
    }

    internal void Add(string cardValue, int index)
    {
      PilesOfCards[index].Cards.Add(new Card
      {
        Value = cardValue
      });
      Refresh();
    }

    public string GetLastCardByListIndex(int index)
    {
      return PilesOfCards[index].Cards.Last().Value;
    }

    internal void Refresh()
    {
      CollectionViewSource.GetDefaultView(PilesOfCards).Refresh();
    }

    internal void RemoveLastCard(int index)
    {
      var card = PilesOfCards[index].Cards.Last();
      PilesOfCards[index].Cards.Remove(card);
      Refresh();
    }
  }
}
