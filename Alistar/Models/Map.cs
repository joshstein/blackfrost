using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alistar.Models
{
    public class Map
    {
        public string Name { get; set; }
        public GameType Mode { get; set; }
        public int TeamSize { get; set; }
        public string Description { get; set; }
    }
}