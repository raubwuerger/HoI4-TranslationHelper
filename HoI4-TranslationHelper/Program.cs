using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace HoI4_TranslationHelper
{
    class Program
    {
        private static List<DataSetMod> _modList = new List<DataSetMod>();
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                LogInfosMods("No arguments passed ...");
                return;
            }

            if( false == SetActiveMod(args[0]))
            {
                LogInfosMods("No mod selected ...");
                return;
            }

            TranslationFileAnalyser.Analyse();
        }

        private static void LogInfosMods(string text)
        {
            Console.WriteLine(text + Environment.NewLine);
            Console.WriteLine("args[0] == mod name");
            Console.WriteLine("Known mods (HoI4-TranslationHelper.xml): ");
            Console.WriteLine(Environment.NewLine);
            foreach (DataSetMod dataSetMod in _modList )
            {
                Console.WriteLine( dataSetMod.Name + Environment.NewLine);
            }
        }

        private static bool SetActiveMod(string modName)
        {
            ConfigReader configReader = new ConfigReader();
            if (false == configReader.Read())
            {
                return false;
            }

            _modList = configReader.ModList;
            ModSelector modSelector = new ModSelector();

            modSelector.ConfigReader = configReader;
            if( false == modSelector.SelectMod(modName.Trim()) )
            {
                return false;
            }

            return true;
        }

    }
}
