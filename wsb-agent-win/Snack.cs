using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsb_agent_win
{
    public class Snack: Product
    {
        public SnackType SnackType { get; set; }
        public string Taste { get; set; }
    }
}
