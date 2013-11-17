using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alistar.Models
{
    public class Champion
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public int Range { get; set; }
        public Gender Gender { get; set; }
        public int Rp { get; set; }
        public int Ip { get; set; }
        public string Spotlight { get; set; }
        public int AbilityVideoID { get; set; }
        public string Story { get; set; }
        public string OvercompensatingQuote { get; set; }
        public string[] Tags { get; set; }
        public double Armor { get; set; }
        public double Damage { get; set; }
        public int Health { get; set; }
        public double HealthRegen { get; set; }
        public int Mana { get; set; }
        public double ManaRegen { get; set; }
        public int MoveSpeed { get; set; }
        public double MagicResistance { get; set; }
        public double ArmorPerLevel { get; set; }
        public double DamagePerLevel { get; set; }
        public int HealthPerLevel { get; set; }
        public double HealthRegenPerLevel { get; set; }
        public double ManaPerLevel { get; set; }
        public double ManaRegenPerLevel { get; set; }
        public double MagicResistancePerLevel { get; set; }
        public Ability QAbility { get; set; }
        public Ability WAbility { get; set; }
        public Ability EAbility { get; set; }
        public Ability RAbility { get; set; }
        public Ability PassiveAbility { get; set; }
        public Quotation[] Quotations { get; set; }
        public Skin[] Skins { get; set; }
    }
}
