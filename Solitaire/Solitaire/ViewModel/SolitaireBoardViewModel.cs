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

      if (currentRegion.Name == "TargetPileOfCards" && previousRegion.CardValue != "placeholder")
      {
        currentRegion.CardValue = TargetPileOfCards.GetLastCardByListIndex(currentRegion.Index);
        if (previousRegion.Name == "MainPileOfCards")
        {
          if (MainPileOfCards.IsLastCard(previousRegion.CardValue, previousRegion.Index) &&
            CardsManager.AreConsecutiveCards(currentRegion.CardValue, previousRegion.CardValue)
            && CardsManager.HasTheSameSuit(currentRegion.CardValue, previousRegion.CardValue))
          {
            TargetPileOfCards.Add(previousRegion.CardValue, currentRegion.Index);
            MainPileOfCards.Remove(previousRegion.CardValue, previousRegion.Index);
          }
        }
        if (previousRegion.Name == "AvailablePileOfCards")
        {
          if (/*AvailablePileOfCards.IsLastCard(previousRegion.CardValue) &&*/
            CardsManager.AreConsecutiveCards(currentRegion.CardValue, previousRegion.CardValue) &&
            CardsManager.HasTheSameSuit(currentRegion.CardValue, previousRegion.CardValue))
          {
            TargetPileOfCards.Add(previousRegion.CardValue, currentRegion.Index);
            AvailablePileOfCards.Remove(previousRegion.CardValue);
          }
        }
      }

      if (currentRegion.Name == "MainPileOfCards")
      {
        if (previousRegion.Name == "MainPileOfCards")
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
            return;
          }
        }
        else if (previousRegion.Name == "AvailablePileOfCards")
        {
          if (/*AvailablePileOfCards.IsLastCard(previousRegion.CardValue) &&*/
            CardsManager.IsValidConfiguration(previousRegion, currentRegion))
          {
            MainPileOfCards.Add(previousRegion.CardValue, currentRegion.Index);
            AvailablePileOfCards.Remove(previousRegion.CardValue);
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
    }
  }
}
