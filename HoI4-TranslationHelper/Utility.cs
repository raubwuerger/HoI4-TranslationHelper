using System;
using System.Collections.Generic;
using System.IO;
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
        public static string FindChildNodeByName(XmlNodeList nodes, string nodeName)
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
                if (node.Name == nodeName)
                {
                    return node.InnerText;
                }
            }
            return null;
        }
        public static string GetAttributeValueByName(XmlAttributeCollection xmlAttributeCollection, string name )
        {
            if ( xmlAttributeCollection == null )
            {
                return null;
            }

            foreach ( XmlAttribute attribute in xmlAttributeCollection )
            { 
                if ( attribute.Name == name ) 
                {  
                    return attribute.Value; 
                } 
            }

            return null;
        }

        public static string CreateStringFromTranslationFile(TranslationFile translationFile)
        {
            if (translationFile == null)
            {
                return null;
            }

            Dictionary<ulong, LineObject> _lines = translationFile.Lines;
            List<LineObject> lines = _lines.Values.ToList();

            string completeString = "";
            foreach ( LineObject lineObject in lines )
            {
                string substitutedLine = lineObject.OriginalLineSubstituted;
                if ( substitutedLine == null )
                {
                    substitutedLine = lineObject.OriginalLine;
                }
                completeString += substitutedLine;
                completeString += Environment.NewLine;
            }

            return completeString;
        }

        /**
         * Double keys will be bypassed.
         */
        public static Dictionary<string, string> ConvertToDictionary(List<string> lines)
        {
            Dictionary<string, string> resubstitutes = new Dictionary<string, string>();
            if (lines == null)
            {
                return resubstitutes;
            }

            foreach (string line in lines)
            {
                string[] splitted = line.Split(';');
                if (splitted.Length < 2)
                {
                    continue;
                }

                if (true == string.IsNullOrEmpty(splitted[0]))
                {
                    continue;
                }
                try
                {
                    resubstitutes.Add(splitted[0], splitted[1]);
                }
                catch(Exception ex)
                {
                    continue;
                }
            }

            return resubstitutes;
        }

        public static void WriteTranslationFile(TranslationFile translationFile)
        {
            if (translationFile == null)
            {
                return;
            }

            string fileName = Path.GetFileNameWithoutExtension(translationFile.FileName);
            string substitionFileSuffix = ".sub";

            Dictionary<ulong, LineObject> _lines = translationFile.Lines;
            List<LineObject> lineObjects = _lines.Values.ToList();

            Console.WriteLine("Writing substituted source file started: " + translationFile.FileName);
            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(translationFile.FileName + substitionFileSuffix))
            {
                foreach (LineObject line in lineObjects)
                {
                    outputFile.WriteLine(GetSubstitutedLine(line));
                }
            }
            Console.WriteLine("Writing substituted source file finished ...");

        }

        private static string GetSubstitutedLine(LineObject lineObject)
        {
            if (lineObject.OriginalLineSubstituted == null)
            {
                return lineObject.OriginalLine;
            }

            if (lineObject.OriginalLineSubstituted.Length <= 0)
            {
                return lineObject.OriginalLine;
            }

            return lineObject.OriginalLineSubstituted;
        }

    }
}
