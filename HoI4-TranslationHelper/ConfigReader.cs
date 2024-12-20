using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HoI4_TranslationHelper
{
    public class ConfigReader
    {
        public bool Read()
        {
            XmlDocument config = XMLFileUtility.Load(Constants.config);
            if (null == config)
            {
                Console.WriteLine("Unable to load config: " + Constants.config + Environment.NewLine);
                return false;
            }
            XmlNodeList configPaths = config.SelectNodes("/HoI4-TranslationHelper/Paths");

            string pathEnglish = Utility.FindNodeByName(configPaths, Constants.configNodePathEnglish);
            if (null != pathEnglish)
            {
                HoI4_TranslationHelper_Config.PathEnglish = pathEnglish;
            }

            string pathGerman = Utility.FindNodeByName(configPaths, Constants.configNodePathGerman);
            if (null != pathGerman)
            {
                HoI4_TranslationHelper_Config.PathGerman = pathGerman;
            }

            return true;
        }
    }
}
