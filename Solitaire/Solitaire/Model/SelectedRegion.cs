using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Model
{
  public class SelectedRegion
  {
    public string CardValue { get; set; }
    public string Name { get; set; }
    public int Index { get; internal set; }

  }
}
