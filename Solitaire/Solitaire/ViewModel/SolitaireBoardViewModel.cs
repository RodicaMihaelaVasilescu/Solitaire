using Prism.Commands;
using Solitaire.Manager;
using Solitaire.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Solitaire.ViewModel
{
  public class SolitaireBoardViewModel : BindableBase
  {

    public ICommand ShuffleAvailableCardsCommand { get; set; }

    public ICommand GetAvailableCardsCommand { get; set; }

    public ObservableCollection<Card> AvailableCards;

    private ObservableCollection<Card> _threeAvailableCards;

    public ObservableCollection<Card> ThreeAvailableCards
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

    public ObservableCollection<ObservableCollection<Card>> Placeholders
    {
      get
      {
        return _placeholders;
      }
      set
      {
        _placeholders = value;
        OnPropertyChanged();
      }
    }

    private ObservableCollection<ObservableCollection<Card>> _cardsTable;

    public ObservableCollection<ObservableCollection<Card>> CardsTable
    {
      get
      {
        return _cardsTable;
      }
      set
      {
        _cardsTable = value;
        OnPropertyChanged();
      }
    }

    public ICommand SelectedCardChangedCommand { get; set; }

    public SelectedCardModel SelectedCard
    {
      get
      {
        return _selectedCard;
      }
      set
      {
        _selectedCard = value;
        OnPropertyChanged();
      }
    }

    public SolitaireBoardViewModel()
    {
      InitializeBoard();
    }

    private void InitializeBoard()
    {
      SelectedCardChangedCommand = new DelegateCommand<object>(SelectedCardChangedCommandExecute);
      InitializeAvailableCards();
      InitializeTableCards();
      InitializePlaceholders();

    }

    private void SelectedCardChangedCommandExecute(object parameter)
    {
      var values = (object[])parameter;
      var currentSelectedCard = new SelectedCardModel
      {
        CardValue = (string)values[0],
        RegionName = (string)values[1],
        IsFlipped = (bool)values[2],
        Index = (int)values[3]
      };

      if (currentSelectedCard.RegionName == "Placeholder")
      {
        var placeholder = Placeholders[currentSelectedCard.Index];
        if (placeholder.Last().Value == "placeholder" && SelectedCard.CardValue[0] == 'A' ||
          CardsManager.AreConsecutiveCards(placeholder.Last().Value, SelectedCard.CardValue) && SelectedCard.CardValue.Last() == placeholder.Last().Value.Last())
        {
          if (SelectedCard.RegionName == "DisplayedCards")
          {
            var list = CardsTable.First(l => l.Any(c => c.Value == SelectedCard.CardValue));
            var card = list.First(c => c.Value == SelectedCard.CardValue);
            placeholder.Add(card);
            list.Remove(card);
            if (list.Any() && list.Last().IsFlipped == true)
            {
              list.Last().IsFlipped = false;
              CollectionViewSource.GetDefaultView(CardsTable).Refresh();
            }
          }
          else if (SelectedCard.RegionName == "ThreeAvailableCards")
          {
            var card = ThreeAvailableCards.Where(c => c.Value == SelectedCard.CardValue).First();
            placeholder.Add(card);
            ThreeAvailableCards.Remove(card);
            AvailableCards.Remove(card);
            CollectionViewSource.GetDefaultView(ThreeAvailableCards).Refresh();
          }
          CollectionViewSource.GetDefaultView(Placeholders).Refresh();
        }
      }

      if (SelectedCard != null)
      {
        if (SelectedCard.RegionName == "DisplayedCards" && currentSelectedCard.RegionName == "DisplayedCards")
        {
          if (CardsManager.IsValidConfiguration(SelectedCard, currentSelectedCard))
          {
            //if(currentSelectedCard.CardValue == "placeholder")
            //{
            //  var card = CardsTable[currentSelectedCard.Index].First();
            //  CardsTable[currentSelectedCard.Index].Remove(card);
            //}
            MoveCards(SelectedCard, currentSelectedCard);
            SelectedCard = null;
          }
          //else if (SelectedCard.CardValue.First() == 'K' && currentSelectedCard.CardValue == "placeholder")
          //{
          //  var list = CardsTable.First(l => l.Any(c => c.Value == SelectedCard.CardValue));
          //  var card = list.First(c => c.Value == SelectedCard.CardValue);
          //  CardsTable[currentSelectedCard.Index].Add(card);
          //  list.Remove(card);
          //  return;
          //}
        }
        else if (SelectedCard.RegionName == "ThreeAvailableCards" && currentSelectedCard.RegionName == "DisplayedCards")
        {
          if (CardsManager.IsValidConfiguration(SelectedCard, currentSelectedCard))
          {
            var card = ThreeAvailableCards.Where(v => v.Value == SelectedCard.CardValue).First();
            card.IsFlipped = false;
            CardsTable.Where(l => l.Any(c => c.Value == currentSelectedCard.CardValue)).First().Add(card);
            ThreeAvailableCards.Remove(card);
            AvailableCards.Remove(card);
            SelectedCard = null;
          }
        }
      }

      SelectedCard = currentSelectedCard;

    }

    private void MoveCards(SelectedCardModel selectedCard, SelectedCardModel currentSelectedCard)
    {
      int sourceIndex = 0;
      ObservableCollection<Card> sourceObsCollection = null, destinationObsCollection = null;
      var sourceList = new List<Card>();
      int cnt = 0;
      foreach (var listOfCards in CardsTable)
      {
        int index = 0;
        foreach (var card in listOfCards)
        {
          if (card.Value == selectedCard.CardValue)
          {
            sourceObsCollection = listOfCards;
            sourceList = listOfCards.ToList().GetRange(index, listOfCards.Count() - index);
            sourceIndex = cnt;
          }
          if (card.Value == currentSelectedCard.CardValue)
          {
            destinationObsCollection = listOfCards;
            if (card != listOfCards.Last())
            {
              return;
            }
          }
          index++;
        }
        cnt++;
      }

      foreach (var card in sourceList)
      {
        destinationObsCollection.Add(card);
        sourceObsCollection.Remove(card);
      }
      if (sourceObsCollection.Any())
      {
        sourceObsCollection.Last().IsFlipped = false;
        CollectionViewSource.GetDefaultView(CardsTable).Refresh();
      }
      if (!sourceObsCollection.Any())
      {
        sourceObsCollection.Add(new Card
        {
          Value = "placeholder",
          Index = sourceIndex
        });
      }
    }

    private void InitializePlaceholders()
    {
      Placeholders = new ObservableCollection<ObservableCollection<Card>>();

      for (int i = 0; i < 4; i++)
      {
        var id = Guid.NewGuid();
        Placeholders.Add(new ObservableCollection<Card>{new Card
        {
          Id = id,
          Index = i,
          Value = "placeholder"
        }});

      }
    }

    private void InitializeTableCards()
    {
      CardsTable = new ObservableCollection<ObservableCollection<Card>>();
      for (int i = 0; i < 7; i++)
      {
        CardsTable.Add(new ObservableCollection<Card>(CardsManager.GetRandomCards(i + 1)));

      }
    }

    private void InitializeAvailableCards()
    {
      ShuffleAvailableCardsCommand = new DelegateCommand(ShuffleAvailableCardsCommandReceived);
      GetAvailableCardsCommand = new DelegateCommand(GetAvailableCardsCommandReceived);
      AvailableCards = new ObservableCollection<Card>(CardsManager.GetRandomCards(24));
    }
    int index = 0;
    private ObservableCollection<ObservableCollection<Card>> _placeholders;
    private SelectedCardModel _selectedCard;

    private void GetAvailableCardsCommandReceived()
    {

    }

    private void ShuffleAvailableCardsCommandReceived()
    {
      if (index + 3 < AvailableCards.Count())
      {
        ThreeAvailableCards = new ObservableCollection<Card>
        {AvailableCards[index], AvailableCards[index+1],AvailableCards[index+2] };
        index += 3;
      }
      else if (index < AvailableCards.Count())
      {
        ThreeAvailableCards = new ObservableCollection<Card>
        {
          AvailableCards[AvailableCards.Count()-1],
          AvailableCards[AvailableCards.Count()-2],
          AvailableCards[AvailableCards.Count()-3] };
        index += 3;
      }
      else
      {
        ThreeAvailableCards = new ObservableCollection<Card>();
        index = 0;
      }
    }
  }
}
