using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsb_agent_win
{
    public class Drink: Product
    {
        public DrinkType DrinkType { get; set; }
        public string DrinkTastes { get; set; }
    }
}
