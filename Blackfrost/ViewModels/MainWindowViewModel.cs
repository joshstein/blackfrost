using System;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Diagnostics;
using Alistar.Models;
using Alistar.Utilities;
using BazamWPF.ViewModels;
using System.Windows.Controls;
using Blackfrost.Views;

namespace Blackfrost.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public UserControl ChampionsControl { get; set; }

        private string _TheMessage;
        public string TheMessage
        {
            get { return _TheMessage; }
            set
            {
                _TheMessage = value;
                OnPropertyChanged("TheMessage");
            }
        }

        public MainWindowViewModel()
        {
            TheMessage = "Sup from the viewmodel?";
            ChampionsControl = new ChampionsControl();

            string alistarDataDirectory = Path.GetFullPath("assets/AlistarData");

            AlistarDataStore Tester = new AlistarDataStore(alistarDataDirectory);
            Champion ahri = Tester.Champions.Where(c => c.Name == "Ahri").FirstOrDefault();
            Item rylais = Tester.Items.Where(i => i.Name == "Rylai's Crystal Scepter").FirstOrDefault();
            Rune lesserMarkOfMight = Tester.Runes.Where(r => r.Name == "Lesser Mark of Might").FirstOrDefault();
            Spell flash = Tester.Spells.Where(s => s.Name == "Flash").FirstOrDefault();
            Map crystalScar = Tester.Maps.Where(m => m.Name == "Crystal Scar").FirstOrDefault();
            Mastery havoc = Tester.Masteries.Where(ma => ma.Name == "Havoc").FirstOrDefault();

            Debug.WriteLine("Sup");
        }

        public void GoToPage(UserControl page)
        {

        }
    }
}