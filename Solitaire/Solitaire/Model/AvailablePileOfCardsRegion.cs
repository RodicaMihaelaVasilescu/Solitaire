using Solitaire.Manager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Model
{
  public class AvailablePileOfCardsRegion : RegionBase
  {
    private ListOfCards _threeAvailableCards;
    public int Index;

    public ListOfCards ThreeAvailableCards
    {
      get
      {
        return _threeAvailableCards;
      }
      set
      {
        _threeAvailableCards = value;
        OnPropertyChanged();
      }
    }

    public ListOfCards AvailableCards { get; private set; }

    public AvailablePileOfCardsRegion()
    {
      InitializeCards();
    }

    private void InitializeCards()
    {
      AvailableCards = new ListOfCards(CardsManager.GetRandomCards(24));
    }

    public void GetNextThreeCards()
    {
      if (Index + 3 < AvailableCards.Cards.Count())
      {
        ThreeAvailableCards = new ListOfCards(GetThreeCardsByIndex(Index));
        Index += 3;
      }
      else if (Index < AvailableCards.Cards.Count())
      {
        ThreeAvailableCards = new ListOfCards(GetThreeCardsByIndex(AvailableCards.Cards.Count() - 3));
        Index += 3;
      }
      else
      {
        ThreeAvailableCards = new ListOfCards(new List<Card>());
        Index = 0;
      }
    }

    private List<Card> GetThreeCardsByIndex(int index)
    {
      var list = new List<Card>();
      for (int i = index; i < index + 3; i++)
      {
        if (i >= 0 && i < AvailableCards.Cards.Count())
        {
          list.Add(AvailableCards.Cards[i]);
        }
        else
        {
          int investigateThis = 1;
        }
      }
      return list;
    }

    internal bool IsLastCard(string cardValue)
    {
      return ThreeAvailableCards.Cards.Last().Value == cardValue;
    }

    internal void Remove(string cardValue)
    {
      var card = AvailableCards.Cards.First(c => c.Value == cardValue);
      AvailableCards.Cards.Remove(card);
      ThreeAvailableCards.Cards.Remove(card);
    }
  }
}
