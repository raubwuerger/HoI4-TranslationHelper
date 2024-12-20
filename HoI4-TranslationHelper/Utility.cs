using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HoI4_TranslationHelper
{
    public class Utility
    {
        public static Dictionary<string, string> RemoveStringFromKey(Dictionary<string, string> list, string toRemove)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> s in list)
            {
                result.Add(s.Key.Replace(toRemove, ""), s.Value);
            }

            return result;
        }

        public static Dictionary<string, string> RemoveStringFromValue(Dictionary<string, string> list, string toRemove)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> s in list)
            {
                result.Add(s.Key, s.Value.Replace(toRemove, ""));
            }

            return result;
        }

        public static string FindNodeByNameAttribute(XmlNodeList nodes, string nodeName)
        {
            foreach (XmlNode node in nodes)
            {
                return node[nodeName].InnerText;
            }
            return null;
        }
        public static string FindNodeByName(XmlNodeList nodes, string nodeName)
        {
            foreach (XmlNode node in nodes)
            {
                return node[nodeName].InnerText;
            }
            return null;
        }

        public static void ParameterizeGerman(FileNameReplacer fileNameReplacer)
        {
            fileNameReplacer.tokenToFind = "_l_german";
            fileNameReplacer.tokenReplaceWith = "";
            fileNameReplacer.extendPath = "german";
        }

        public static void ParameterizeEnglish(FileNameReplacer fileNameReplacer)
        {
            fileNameReplacer.tokenToFind = "_l_english";
            fileNameReplacer.tokenReplaceWith = "";
            fileNameReplacer.extendPath = "english";
        }
    }
}
