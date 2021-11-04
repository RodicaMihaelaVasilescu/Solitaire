using Prism.Commands;
using Solitaire.Constants;
using Solitaire.Manager;
using Solitaire.Model;
using System.Linq;
using System.Windows.Input;

namespace Solitaire.ViewModel
{
  public class SolitaireBoardViewModel : BindableBase
  {
    public AvailablePileOfCardsRegion AvailablePileOfCards { get; set; }

    public TargetPileOfCardsRegion TargetPileOfCards { get; set; }

    public MainPileOfCardsRegion MainPileOfCards { get; set; }

    public ICommand ShuffleAvailableCardsCommand { get; set; }

    public ICommand GetAvailableCardsCommand { get; set; }

    public ICommand SelectedCardChangedCommand { get; set; }
    public SelectedRegion previousRegion { get; private set; }

    public SolitaireBoardViewModel()
    {
      SelectedCardChangedCommand = new DelegateCommand<object>(SelectedCardChangedCommandExecute);
      ShuffleAvailableCardsCommand = new DelegateCommand(ShuffleAvailableCardsCommandReceived);
      GetAvailableCardsCommand = new DelegateCommand(GetAvailableCardsCommandReceived);
      InitializeBoard();
    }

    private void InitializeBoard()
    {
      MainPileOfCards = new MainPileOfCardsRegion();
      AvailablePileOfCards = new AvailablePileOfCardsRegion();
      TargetPileOfCards = new TargetPileOfCardsRegion();
    }

    private void SelectedCardChangedCommandExecute(object parameter)
    {
      var values = (object[])parameter;

      var currentRegion = new SelectedRegion
      {
        Name = (string)values[0],
        Index = (int)values[1],
        CardValue = values[2].ToString()
      };

      if (previousRegion == null)
      {
        previousRegion = currentRegion;
        return;
      }

      if (currentRegion.Name == RegionConstants.TargetPileOfCards)
      {
        currentRegion.CardValue = TargetPileOfCards.GetLastCardByListIndex(currentRegion.Index);
      }

      if (currentRegion.Name == RegionConstants.TargetPileOfCards && previousRegion.CardValue != "placeholder")
      {
        if (previousRegion.Name == RegionConstants.MainPileOfCards)
        {
          if (MainPileOfCards.IsLastCard(previousRegion.CardValue, previousRegion.Index) &&
            CardsManager.AreConsecutiveCards(currentRegion.CardValue, previousRegion.CardValue)
            && CardsManager.HasTheSameSuit(currentRegion.CardValue, previousRegion.CardValue))
          {
            TargetPileOfCards.Add(previousRegion.CardValue, currentRegion.Index);
            MainPileOfCards.Remove(previousRegion.CardValue, previousRegion.Index);
            currentRegion = previousRegion = null;
            return;
          }
        }
        if (previousRegion.Name == RegionConstants.AvailablePileOfCards)
        {
          if (AvailablePileOfCards.IsLastCard(previousRegion.CardValue) &&
            CardsManager.AreConsecutiveCards(currentRegion.CardValue, previousRegion.CardValue) &&
            CardsManager.HasTheSameSuit(currentRegion.CardValue, previousRegion.CardValue))
          {
            TargetPileOfCards.Add(previousRegion.CardValue, currentRegion.Index);
            AvailablePileOfCards.Remove(previousRegion.CardValue);
            currentRegion = previousRegion = null;
            return;
          }
        }
      }

      if (currentRegion.Name == RegionConstants.MainPileOfCards)
      {
        if (previousRegion.Name == RegionConstants.MainPileOfCards)
        {
          if (CardsManager.IsValidConfiguration(previousRegion, currentRegion))
          {
            MainPileOfCards.MoveCards(previousRegion, currentRegion);
            previousRegion = null;
            currentRegion = null;
            return;
          }
          else if (previousRegion.CardValue.First() == 'K' && currentRegion.CardValue == "placeholder")
          {
            MainPileOfCards.MoveCards(previousRegion, currentRegion);
            currentRegion = previousRegion = null;
            return;
          }
        }
        else if (previousRegion.Name == RegionConstants.AvailablePileOfCards)
        {
          if (AvailablePileOfCards.IsLastCard(previousRegion.CardValue) &&
            CardsManager.IsValidConfiguration(previousRegion, currentRegion))
          {
            MainPileOfCards.Add(previousRegion.CardValue, currentRegion.Index);
            AvailablePileOfCards.Remove(previousRegion.CardValue);
            currentRegion = previousRegion = null;
            return;
          }
        }
        else if (previousRegion.Name == RegionConstants.TargetPileOfCards)
        {
          if (CardsManager.IsValidConfiguration(previousRegion, currentRegion) &&
            MainPileOfCards.IsLastCard(currentRegion.CardValue, currentRegion.Index))
          {
            MainPileOfCards.Add(previousRegion.CardValue, currentRegion.Index);
            TargetPileOfCards.RemoveLastCard(previousRegion.Index);
            currentRegion = previousRegion = null;
            return;
          }
        }
      }

      previousRegion = currentRegion;

    }

    private void GetAvailableCardsCommandReceived()
    {

    }

    private void ShuffleAvailableCardsCommandReceived()
    {
      previousRegion = null;
      AvailablePileOfCards.GetNextThreeCards();
      RefreshBoard();
    }

    private void RefreshBoard()
    {
      MainPileOfCards.Refresh();
      TargetPileOfCards.Refresh();
    }
  }
}
