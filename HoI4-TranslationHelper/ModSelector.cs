using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    internal class ModSelector
    {
        private ConfigReader _configReader = null;

        public ConfigReader ConfigReader
        { set { _configReader = value; } }
     
        public bool SelectMod( string modName )
        {
            if( _configReader == null )
            {
                Console.WriteLine("ModSelector not initialized");
                return false;
            }

            DataSetMod found = _configReader.ModList.Find( i => i.Name == modName );
            if( found == null ) 
            {
                Console.WriteLine("Mod not found: " + modName);
                return false;
            }
            Console.WriteLine("Mod found: " + modName);

            HoI4_TranslationHelper_Config.PathEnglish = found.PathEnglish;
            HoI4_TranslationHelper_Config.PathGerman = found.PathGerman;

            return true;
        }

    }
}
