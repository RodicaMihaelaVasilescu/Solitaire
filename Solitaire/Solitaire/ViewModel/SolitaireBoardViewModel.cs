using Prism.Commands;
using Solitaire.Manager;
using Solitaire.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public SolitaireBoardViewModel()
    {
      InitializeBoard();
    }

    private void InitializeBoard()
    {
      InitializeAvailableCards();
    }

    private void InitializeAvailableCards()
    {
      ShuffleAvailableCardsCommand = new DelegateCommand(ShuffleAvailableCardsCommandReceived);
      GetAvailableCardsCommand = new DelegateCommand(GetAvailableCardsCommandReceived);
      AvailableCards = new ObservableCollection<Card>(CardsManager.GetRandomCards(24));
    }
    int index = 0;
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
