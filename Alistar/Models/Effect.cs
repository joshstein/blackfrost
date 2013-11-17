using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alistar.Models
{
    public class Effect
    {
        public bool IsActive { get; set; }
        public bool IsAura { get; set; }
        public bool IsUnique { get; set; }
        public string Description { get; set; }
    }
}
