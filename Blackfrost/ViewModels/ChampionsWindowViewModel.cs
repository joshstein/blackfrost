using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Alistar.Models;
using Alistar.Utilities;
using BazamWPF.ViewModels;

namespace Blackfrost.ViewModels
{
    public class ChampionsWindowViewModel : ViewModelBase
    {
        public List<Champion> ChampionList { get; set; }

        public ChampionsWindowViewModel()
        {
            string alistarDataDirectory = Path.GetFullPath("assets/AlistarData");
            AlistarDataStore dataStore = new AlistarDataStore(alistarDataDirectory);
            ChampionList = dataStore.Champions.ToList();
        }
    }
}