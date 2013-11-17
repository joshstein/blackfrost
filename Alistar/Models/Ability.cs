using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alistar.Models
{
    public class Ability
    {
        public string Name { get; set; }
        public string Cost { get; set; }
        public string Cooldown { get; set; }
        public DamageType DamageType { get; set; }
        public int Range { get; set; }
        public string Details { get; set; }
        public char HotKey { get; set; }
        public Scalar[] Scalars { get; set; }
    }
}