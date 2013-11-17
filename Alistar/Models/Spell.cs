using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alistar.Models
{
    public class Spell
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Cooldown { get; set; }
        public string Description { get; set; }
    }
}