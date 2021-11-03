using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Model
{
  public class SelectedCardModel
  {
    public string CardValue { get; set; }
    public string RegionName { get; set; }
    public bool IsFlipped { get; internal set; }
    public int Index { get; internal set; }
  }
}
