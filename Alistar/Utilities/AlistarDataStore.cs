using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;
using Alistar.Models;
using Bazam.Modules;
using Bazam.Slugging;

namespace Alistar.Utilities
{
    public class AlistarDataStore
    {
        public string DataDirectory { get; private set; }
        private List<Champion> _Champions;
        private List<Item> _Items;
        private List<Rune> _Runes;

        public AlistarDataStore(string dataDirectory)
        {
            DataDirectory = dataDirectory;
            _Champions = new List<Champion>();
            _Items = new List<Item>();
            _Runes = new List<Rune>();

            GetChampionList();
            GetItemList();
            GetRuneList();
        }

        // populate _Champions with data from Champions.xml
        private void GetChampionList()
        {
            _Champions = new List<Champion>();

            string champsDocPath = Path.Combine(DataDirectory, "Champions.xml");
            XDocument champsDoc = XDocument.Load(champsDocPath);

            _Champions.AddRange(
                from champion in champsDoc.Root.Elements("Champion")
                select new Champion()
                {
                    Name = XMLPal.GetString(champion.Attribute("name")),
                    Title = XMLPal.GetString(champion.Attribute("title")),
                    Range = XMLPal.GetInt(champion.Attribute("range")),
                    Gender = GetGender(XMLPal.GetString(champion.Attribute("gender"))),
                    Rp = XMLPal.GetInt(champion.Attribute("rp")),
                    Ip = XMLPal.GetInt(champion.Attribute("ip")),
                    Spotlight = XMLPal.GetString(champion.Attribute("spotlight")),
                    AbilityVideoID = XMLPal.GetInt(champion.Attribute("abilityVideoID")),
                    Story = XMLPal.GetString(champion.Element("Story")),
                    OvercompensatingQuote = XMLPal.GetString(champion.Element("OvercompensatingQuote")),
                    Tags = (
                        from tag in champion.Element("Tags").Elements("Tag")
                        select tag.Value
                    ).ToArray(),
                    Armor = (
                        from stat in champion.Element("Stats").Elements("AdditiveStat")
                        where XMLPal.GetString(stat.Attribute("statType")) == "Armor" && !XMLPal.GetBool(stat.Attribute("isPerLevel"))
                        select XMLPal.GetDouble(stat.Attribute("value"))
                    ).FirstOrDefault(),
                    Damage = (
                        from stat in champion.Element("Stats").Elements("AdditiveStat")
                        where XMLPal.GetString(stat.Attribute("statType")) == "Damage" && !XMLPal.GetBool(stat.Attribute("isPerLevel"))
                        select XMLPal.GetDouble(stat.Attribute("value"))
                    ).FirstOrDefault(),
                    Health = (
                        from stat in champion.Element("Stats").Elements("AdditiveStat")
                        where XMLPal.GetString(stat.Attribute("statType")) == "Health" && !XMLPal.GetBool(stat.Attribute("isPerLevel"))
                        select XMLPal.GetInt(stat.Attribute("value"))
                    ).FirstOrDefault(),
                    HealthRegen = (
                        from stat in champion.Element("Stats").Elements("AdditiveStat")
                        where XMLPal.GetString(stat.Attribute("statType")) == "HealthRegen" && !XMLPal.GetBool(stat.Attribute("isPerLevel"))
                        select XMLPal.GetDouble(stat.Attribute("value"))
                    ).FirstOrDefault(),
                    Mana = (
                        from stat in champion.Element("Stats").Elements("AdditiveStat")
                        where XMLPal.GetString(stat.Attribute("statType")) == "Mana" && !XMLPal.GetBool(stat.Attribute("isPerLevel"))
                        select XMLPal.GetInt(stat.Attribute("value"))
                    ).FirstOrDefault(),
                    ManaRegen = (
                        from stat in champion.Element("Stats").Elements("AdditiveStat")
                        where XMLPal.GetString(stat.Attribute("statType")) == "ManaRegen" && !XMLPal.GetBool(stat.Attribute("isPerLevel"))
                        select XMLPal.GetDouble(stat.Attribute("value"))
                    ).FirstOrDefault(),
                    MoveSpeed = (
                        from stat in champion.Element("Stats").Elements("AdditiveStat")
                        where XMLPal.GetString(stat.Attribute("statType")) == "MoveSpeed" && !XMLPal.GetBool(stat.Attribute("isPerLevel"))
                        select XMLPal.GetInt(stat.Attribute("value"))
                    ).FirstOrDefault(),
                    MagicResistance = (
                        from stat in champion.Element("Stats").Elements("AdditiveStat")
                        where XMLPal.GetString(stat.Attribute("statType")) == "MagicResistance" && !XMLPal.GetBool(stat.Attribute("isPerLevel"))
                        select XMLPal.GetDouble(stat.Attribute("value"))
                    ).FirstOrDefault(),
                    ArmorPerLevel = (
                        from stat in champion.Element("Stats").Elements("AdditiveStat")
                        where XMLPal.GetString(stat.Attribute("statType")) == "Armor" && XMLPal.GetBool(stat.Attribute("isPerLevel"))
                        select XMLPal.GetDouble(stat.Attribute("value"))
                    ).FirstOrDefault(),
                    DamagePerLevel = (
                        from stat in champion.Element("Stats").Elements("AdditiveStat")
                        where XMLPal.GetString(stat.Attribute("statType")) == "Damage" && XMLPal.GetBool(stat.Attribute("isPerLevel"))
                        select XMLPal.GetDouble(stat.Attribute("value"))
                    ).FirstOrDefault(),
                    HealthPerLevel = (
                        from stat in champion.Element("Stats").Elements("AdditiveStat")
                        where XMLPal.GetString(stat.Attribute("statType")) == "Health" && XMLPal.GetBool(stat.Attribute("isPerLevel"))
                        select XMLPal.GetInt(stat.Attribute("value"))
                    ).FirstOrDefault(),
                    HealthRegenPerLevel = (
                        from stat in champion.Element("Stats").Elements("AdditiveStat")
                        where XMLPal.GetString(stat.Attribute("statType")) == "HealthRegen" && XMLPal.GetBool(stat.Attribute("isPerLevel"))
                        select XMLPal.GetDouble(stat.Attribute("value"))
                    ).FirstOrDefault(),
                    ManaPerLevel = (
                        from stat in champion.Element("Stats").Elements("AdditiveStat")
                        where XMLPal.GetString(stat.Attribute("statType")) == "Mana" && XMLPal.GetBool(stat.Attribute("isPerLevel"))
                        select XMLPal.GetDouble(stat.Attribute("value"))
                    ).FirstOrDefault(),
                    ManaRegenPerLevel = (
                        from stat in champion.Element("Stats").Elements("AdditiveStat")
                        where XMLPal.GetString(stat.Attribute("statType")) == "ManaRegen" && XMLPal.GetBool(stat.Attribute("isPerLevel"))
                        select XMLPal.GetDouble(stat.Attribute("value"))
                    ).FirstOrDefault(),
                    QAbility = GetAbility(champion.Element("Abilities").Elements("Ability"), AbilityHotkey.Q),
                    WAbility = GetAbility(champion.Element("Abilities").Elements("Ability"), AbilityHotkey.W),
                    EAbility = GetAbility(champion.Element("Abilities").Elements("Ability"), AbilityHotkey.E),
                    RAbility = GetAbility(champion.Element("Abilities").Elements("Ability"), AbilityHotkey.R),
                    PassiveAbility = GetAbility(champion.Element("Abilities").Elements("Ability"), AbilityHotkey.PASSIVE),
                    Quotations = (
                        from quotation in champion.Element("Quotations").Elements("Quotation")
                        select new Quotation()
                        {
                            Type = XMLPal.GetString(quotation.Attribute("type")),
                            Text = XMLPal.GetString(quotation.Attribute("text")),
                            FileName = XMLPal.GetString(quotation.Attribute("fileName")),
                            Description = XMLPal.GetString(quotation.Attribute("description"))
                        }
                    ).ToArray(),
                    Skins = (
                        from skin in champion.Element("Skins").Elements("Skin")
                        select new Skin()
                        {
                            Name = XMLPal.GetString(skin.Attribute("name")),
                            Price = XMLPal.GetInt(skin.Attribute("rp"))
                        }
                    ).ToArray()
                }
            );
        }

        // populate _Items with data from Items.xml
        private void GetItemList()
        {
            _Items = new List<Item>();

            string itemsDocPath = Path.Combine(DataDirectory, "Items.xml");
            XDocument itemsDoc = XDocument.Load(itemsDocPath);
            Dictionary<string, List<string>> itemRelationships = new Dictionary<string,List<string>>();

            foreach(XElement itemElement in itemsDoc.Root.Elements("Item"))
            {
                Item item = new Item()
                {
                    Name = XMLPal.GetString(itemElement.Attribute("name")),
                    Id = XMLPal.GetInt(itemElement.Attribute("id")),
                    Cost = XMLPal.GetInt(itemElement.Attribute("cost")),
                };

                if(itemElement.Element("Stats") != null)
                {
                    List<Stat> stats = new List<Stat>();

                    foreach(XElement statElement in itemElement.Element("Stats").DescendantNodes())
                    {
                        Stat stat = new Stat() 
                        {
                            StatType = GetStatType(XMLPal.GetString(statElement.Attribute("statType"))),
                            Value = XMLPal.GetString(statElement.Attribute("value"))
                        };
                        stats.Add(stat);
                    }

                    item.Stats = stats.ToArray();
                }

                if(itemElement.Element("Effects") != null)
                {
                    List<Effect> effects = new List<Effect>();

                    foreach(XElement effectElement in itemElement.Element("Effects").Elements("Effect"))
                    {
                        Effect effect = new Effect()
                        {
                            IsActive = (XMLPal.GetString(effectElement.Attribute("isActive")) == "true" ? true : false),
                            IsAura = (XMLPal.GetString(effectElement.Attribute("isAura")) == "true" ? true : false),
                            IsUnique = (XMLPal.GetString(effectElement.Attribute("isUnique")) == "true" ? true : false),
                            Description = XMLPal.GetString(effectElement)
                        };

                        effects.Add(effect);
                    }

                    item.Effects = effects.ToArray();
                }

                if(itemElement.Elements("BuiltFrom").Count() > 0)
                {
                    List<string> builtFromItems = new List<string>();
                    builtFromItems.AddRange(
                        from subItem in itemElement.Elements("BuiltFrom").Elements("Item")
                        select XMLPal.GetString(subItem.Attribute("slug"))
                    );

                    itemRelationships.Add(Slugger.Slugify(item.Name), builtFromItems);
                }

                _Items.Add(item);
            }

            foreach(string itemSlug in itemRelationships.Keys)
            {
                List<Item> builtFromItems = new List<Item>();
                Item item = _Items.Where(i => Slugger.Slugify(i.Name) == itemSlug).FirstOrDefault();

                foreach(string builtFromItemSlug in itemRelationships[itemSlug])
                {
                    Item builtFromItem = _Items.Where(i => Slugger.Slugify(i.Name) == builtFromItemSlug).FirstOrDefault();
                    builtFromItems.Add(builtFromItem);
                }

                item.BuiltFrom = builtFromItems.ToArray();
            }

        }

        // populate _Runes with data from Runes.xml
        private void GetRuneList()
        {
            _Runes = new List<Rune>();

            string runesDocPath = Path.Combine(DataDirectory, "Runes.xml");
            XDocument runesDoc = XDocument.Load(runesDocPath);

            foreach(XElement rune in runesDoc.Root.Elements("Rune"))
            {
                List<Stat> stats = new List<Stat>();

                Rune newRune = new Rune()
                {
                    Magnitude = XMLPal.GetString(rune.Attribute("magnitude")),
                    RuneType = GetRuneType(XMLPal.GetString(rune.Attribute("type"))),
                    Descriptor = XMLPal.GetString(rune.Attribute("descriptor"))
                };

                foreach (XElement statElement in rune.Element("Stats").DescendantNodes())
                {
                    Stat stat = new Stat()
                    {
                        StatType = GetStatType(XMLPal.GetString(statElement.Attribute("statType"))),
                        Value = XMLPal.GetString(statElement.Attribute("value"))
                    };

                    stats.Add(stat);
                }

                newRune.Stats = stats.ToArray();
                newRune.Name = newRune.Magnitude + " " + StringBeast.Capitalize(newRune.RuneType.ToString(), true) + " of " + newRune.Descriptor;

                _Runes.Add(newRune);
            }
        }

        private Ability GetAbility(IEnumerable<XElement> abilityElements, AbilityHotkey hotkey)
        {
            return (
                from ability in abilityElements
                where XMLPal.GetString(ability.Attribute("hotkey")) == hotkey.ToString().ToLower() || (hotkey == AbilityHotkey.PASSIVE && ability.Attribute("hotkey") == null)
                select new Ability()
                {
                    Name = XMLPal.GetString(ability.Attribute("name")),
                    Cost = XMLPal.GetString(ability.Attribute("cost")),
                    Cooldown = XMLPal.GetString(ability.Attribute("cooldown")),
                    DamageType = GetDamageType(XMLPal.GetString(ability.Attribute("damageType"))),
                    Range = XMLPal.GetInt(ability.Attribute("range")),
                    Details = XMLPal.GetString(ability.Element("Details")),
                    Scalars = (
                        ability.Element("Scalar") != null ?
                        (
                            from scalar in ability.Element("Scalars").Elements("Scalar")
                            select new Scalar()
                            {
                                ScalarType = GetScalarType(XMLPal.GetString(scalar.Attribute("source"))),
                                Value = XMLPal.GetDouble(scalar.Attribute("value"))
                            }
                        ).ToArray()
                        : new Scalar[] { }
                    )
                }
            ).FirstOrDefault();
        }

        public Champion[] Champions 
        { 
            get { return _Champions.ToArray();  } 
        } 

        public Item[] Items
        {
            get { return _Items.ToArray(); }
        }

        public Rune[] Runes
        {
            get { return _Runes.ToArray(); }
        }

        private Gender GetGender(string data)
        {
            switch (data)
            {
                case "m": 
                    return Gender.MALE;
                case "f":
                    return Gender.FEMALE;
                case "a":
                    return Gender.COURTEOUSLY_ABSTAINING;
            }

            throw new InvalidOperationException("Couldn't find a gender that matches " + data + ".");
        }

        private DamageType GetDamageType(string data)
        {
            switch (data)
            {
                case "Physical":
                    return DamageType.PHYSICAL;
                case "Magical":
                    return DamageType.MAGICAL;
                case "MagicalAndPhysical":
                    return DamageType.MIXED;
                case "True":
                    return DamageType.TRUE;
                default:
                    return DamageType.UNDEFINED;
            }
        }

        private ScalarType GetScalarType(string data)
        {
            switch (data)
            {
                case "Armor":
                    return ScalarType.ARMOR;
                case "ManaMax":
                    return ScalarType.MANA_MAX;
                case "ManaMissing":
                    return ScalarType.MANA_MISSING;
                case "AttackDamage":
                    return ScalarType.ATTACK_DAMAGE;
                case "AbilityPower":
                    return ScalarType.ABILITY_POWER;
                case "HealthBonus":
                    return ScalarType.HEALTH_BONUS;
                case "HealthMax":
                    return ScalarType.HEALTH_MAX;
                case "HealthMissing":
                    return ScalarType.HEALTH_MISSING;
                case "TargetHealthCurrent":
                    return ScalarType.TARGET_HEALTH_CURRENT;
                case "TargetHealthMax":
                    return ScalarType.TARGET_HEALTH_MAX;
                case "TargetHealthMissing":
                    return ScalarType.TARGET_HEALTH_MISSING;
                case "TargetAbilityPower":
                    return ScalarType.TARGET_ABILITY_POWER;
                case "NasusDamage":
                    return ScalarType.NASUS_DAMAGE;
            }

            throw new InvalidOperationException("Couldn't find a scalar type that matches " + data + ".");
        }

        private StatType GetStatType(string data)
        {
            switch (data)
            {
                case "AbilityPower":
                    return StatType.ABILITY_POWER;
                case "Armor":
                    return StatType.ARMOR;
                case "ArmorPenetration":
                    return StatType.ARMOR_PENETRATION;
                case "AttackSpeed":
                    return StatType.ATTACK_SPEED;
                case "CaptureRate":
                    return StatType.CAPTURE_RATE;
                case "CooldownReduction":
                    return StatType.COOLDOWN_REDUCTION;
                case "CriticalChance":
                    return StatType.CRITICAL_CHANCE;
                case "CriticalDamage":
                    return StatType.CRITICAL_DAMAGE;
                case "Damage":
                    return StatType.DAMAGE;
                case "Dodge":
                    return StatType.DODGE;
                case "Energy":
                    return StatType.ENERGY;
                case "EnergyRegen":
                    return StatType.ENERGY_REGEN;
                case "ExperienceGained":
                    return StatType.EXPERIENCE_GAINED;
                case "GoldPer10":
                    return StatType.GOLD_PER_10;
                case "Health":
                    return StatType.HEALTH;
                case "HealthRegen":
                    return StatType.HEALTH_REGEN;
                case "LifeSteal":
                    return StatType.LIFESTEAL;
                case "MagicPenetration":
                    return StatType.MAGIC_PENETRATION;
                case "MagicResistance":
                    return StatType.MAGIC_RESISTANCE;
                case "Mana":
                    return StatType.MANA;
                case "ManaRegen":
                    return StatType.MANA_REGEN;
                case "MoveSpeed":
                    return StatType.MOVESPEED;
                case "SpellVamp":
                    return StatType.SPELL_VAMP;
                case "Tenacity":
                    return StatType.TENACITY;
                case "TimeDead":
                    return StatType.TIME_DEAD;
            }

            throw new InvalidOperationException("Couldn't find a stat type that matches " + data + ".");
        }

        private RuneType GetRuneType(string data)
        {
            switch (data)
            {
                case "Mark":
                    return RuneType.MARK;
                case "Seal":
                    return RuneType.SEAL;
                case "Glyph":
                    return RuneType.GLYPH;
                case "Quintessence":
                    return RuneType.QUINTESSENCE;
            }
            throw new InvalidOperationException("Couldn't find a rune type that matches " + data + ".");
        }
    }
}