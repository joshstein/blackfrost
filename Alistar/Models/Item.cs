using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alistar.Models
{
    public class Item
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Cost { get; set; }
        public Stat[] Stats { get; set; }
        public Effect[] Effects { get; set; }
        public Item[] BuiltFrom { get; set; }
    }
}
