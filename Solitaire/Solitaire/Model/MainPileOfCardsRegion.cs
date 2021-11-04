using Solitaire.Manager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Solitaire.Model
{
  public class MainPileOfCardsRegion : RegionBase
  {

    public MainPileOfCardsRegion()
    {
      InitializeCards();
    }

    public override void InitializeCards()
    {
      PilesOfCards = new ObservableCollection<ListOfCards>();
      for (int i = 0; i < 7; i++)
      {
        var cardsList = new ListOfCards(CardsManager.GetRandomCards(i + 1), i);
        cardsList.Cards.Last().FrontImageDisplayed = true;
        PilesOfCards.Add(cardsList);
      }
    }

    internal void Remove(string cardValue, int index)
    {
      var card = PilesOfCards[index].Cards.First(c => c.Value == cardValue);
      PilesOfCards[index].Cards.Remove(card);
      if (!PilesOfCards[index].Cards.Any())
      {
        PilesOfCards[index].Cards.Add(new Card
        {
          Value = "placeholder"
        });
      }
      PilesOfCards[index].Cards.Last().FrontImageDisplayed = true;

      CollectionViewSource.GetDefaultView(PilesOfCards).Refresh();
    }

    internal bool IsLastCard(string cardValue, int index)
    {
      return PilesOfCards[index].Cards.Last().Value == cardValue;
    }

    internal void MoveCards(SelectedRegion source, SelectedRegion destination)
    {
      if (!IsLastCard(destination.CardValue, destination.Index))
      {
        return;
      }

      var first = PilesOfCards[destination.Index].Cards.First();
      if (first.Value == "placeholder")
      {
        PilesOfCards[destination.Index].Cards.Remove(first);
      }

      var card = PilesOfCards[source.Index].Cards.First(c => c.Value == source.CardValue);
      int index = PilesOfCards[source.Index].Cards.IndexOf(card);
      var sourceList = PilesOfCards[source.Index].Cards.ToList().GetRange(index, PilesOfCards[source.Index].Cards.Count() - index);

      foreach (var item in sourceList)
      {
        PilesOfCards[destination.Index].Cards.Add(item);
        PilesOfCards[source.Index].Cards.Remove(item);
      }
      if (!PilesOfCards[source.Index].Cards.Any())
      {
        PilesOfCards[source.Index].Cards.Add(new Card { Value = "placeholder" });
      }
      PilesOfCards[source.Index].Cards.Last().FrontImageDisplayed = true;
      CollectionViewSource.GetDefaultView(PilesOfCards).Refresh();

    }

    public void Add(string cardValue, int index)
    {
      var first = PilesOfCards[index].Cards.First();
      if(first.Value == "placeholder")
      {
        PilesOfCards[index].Cards.Remove(first);
      }
      PilesOfCards[index].Cards.Add(new Card
      {
        Value = cardValue,
        FrontImageDisplayed = true
      });

      Refresh();
    }

    internal void Refresh()
    {
      CollectionViewSource.GetDefaultView(PilesOfCards).Refresh();
    }
  }
}

