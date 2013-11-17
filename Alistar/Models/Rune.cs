using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alistar.Models
{
    public class Rune
    {
        public string Name { get; set; }
        public string Magnitude { get; set; }
        public RuneType RuneType { get; set; }
        public string Descriptor { get; set; }
        public Stat[] Stats { get; set; }
    }
}
