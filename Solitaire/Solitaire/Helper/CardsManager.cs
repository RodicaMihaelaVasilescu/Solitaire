using Solitaire.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Manager
{
  static public class CardsManager
  {
    static public List<string> GetAllCards()
    {
      return new List<string>
      {
      "AS", "AH","AD", "AC",
      "2S", "2H","2D", "2C",
      "3S", "3H","3D", "3C",
      "4S", "4H","4D", "4C",
      "5S", "5H","5D", "5C",
      "6S", "6H","6D", "6C",
      "7S", "7H","7D", "7C",
      "8S", "8H","8D", "8C",
      "9S", "9H","9D", "9C",
      "10S", "10H","10D", "10C",
      "JS", "JH","JD", "JC",
      "QS", "QH","QD", "QC",
      "KS", "KH","KD", "KC"
      };

    }

    static public List<string> RemainingCards = new List<string>
      {
      "AS", "AH","AD", "AC",
      "2S", "2H","2D", "2C",
      "3S", "3H","3D", "3C",
      "4S", "4H","4D", "4C",
      "5S", "5H","5D", "5C",
      "6S", "6H","6D", "6C",
      "7S", "7H","7D", "7C",
      "8S", "8H","8D", "8C",
      "9S", "9H","9D", "9C",
      "10S", "10H","10D", "10C",
      "JS", "JH","JD", "JC",
      "QS", "QH","QD", "QC",
      "KS", "KH","KD", "KC"
      };

    static public List<Card> GetRandomCards(int number = 5)
    {
      var list = new List<Card>();
      var cards = RemainingCards;
      Random random = new Random();
      for (int i = 0; i < number; i++)
      {
        var id = Guid.NewGuid();
        var randomIndex = random.Next(cards.Count());
        list.Add(new Card
        {
          Id = id,
          Value = cards[randomIndex]
        });
        cards.Remove(cards[randomIndex]);
      }
      return list;
    }
  }
}
