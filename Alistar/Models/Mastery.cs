using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alistar.Models
{
    public class Mastery
    {
        public MasteryTreeType Tree { get; set; }
        public int Tier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Maximum { get; set; }
        public Stat[] Stats { get; set; }
    }
}