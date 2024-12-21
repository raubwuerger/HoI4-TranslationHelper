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
        public const string CONFIG_BASEPATH = "/HoI4-TranslationHelper/Paths";
        public const string CONFIG_MOD = "/Mod";
        private List<DataSetMod> _modList = new List<DataSetMod>();
        public bool Read()
        {
            _modList.Clear();
            XmlDocument config = XMLFileUtility.Load(Constants.config);
            if (null == config)
            {
                Console.WriteLine("Unable to load config: " + Constants.config + Environment.NewLine);
                return false;
            }
            
            XmlNodeList configPaths = config.SelectNodes(CONFIG_BASEPATH + CONFIG_MOD);

            foreach (XmlNode configPath in configPaths) 
            {
                string modName = Utility.GetAttributeValueByName(configPath.Attributes, "name");
                if (null == modName )
                {
                    continue;
                }

                DataSetMod dataSetMod = new DataSetMod(modName);
                string pathEnglish = Utility.FindNodeByName(configPaths, Constants.configNodePathEnglish);
                if (null != pathEnglish)
                {
                    dataSetMod.PathEnglish = pathEnglish;
                }

                string pathGerman = Utility.FindNodeByName(configPaths, Constants.configNodePathGerman);
                if (null != pathGerman)
                {
                    dataSetMod.PathGerman = pathGerman;
                }

                _modList.Add(dataSetMod);
            }

            return true;
        }

        internal List<DataSetMod> ModList { get => _modList; }
    }
}
