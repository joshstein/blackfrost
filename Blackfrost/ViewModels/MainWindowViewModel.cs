using BazamWPF.ViewModels;
using Alistar.Models;
using Alistar.Utilities;
using System;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace Blackfrost.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
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

            string alistarDataDirectory = Path.GetFullPath("assets/AlistarData");
            AlistarDataStore Tester = new AlistarDataStore(alistarDataDirectory);
            Champion ahri = Tester.Champions.Where(c => c.Name == "Ahri").FirstOrDefault();
            Item rylais = Tester.Items.Where(i => i.Name == "Rylai's Crystal Scepter").FirstOrDefault();
            Debug.WriteLine("Sup");
        }
    }
}