using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Xml;
using WeThePeople_ModdingTool.FileUtilities;

namespace HoI4_TranslationHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                LogInfos("No arguments passed ...");
                return;
            }

            ReadConfig();

            switch (args[0])
            {
                case "0":
                    ParseDirectoryGerman(FolderCompare());
                    ParseDirectoryEnglish(FolderCompare());
                    break;
                case "1":
                    ParseDirectoryGerman(Brackets());
                    ParseDirectoryEnglish(Brackets());
                    break;
                case "2":
                    ParseDirectoryGerman(Icons());
                    ParseDirectoryEnglish(Icons());
                    break;
                case "3":
                    ParseDirectoryGerman(Variables());
                    ParseDirectoryEnglish(Variables());
                    break;
                case "4":
                    ParseDirectoryGerman(Colors());
                    ParseDirectoryEnglish(Colors());
                    break;
                case "5":
                    ParseDirectoryGerman(InnerDoubleQuotes());
                    ParseDirectoryEnglish(InnerDoubleQuotes());
                    break;
                case "6":
                    ParseDirectoryGerman(Keys());
                    ParseDirectoryEnglish(Keys());
                    break;
                default:
                    LogInfos(string.Format("Wrong argument {0} passed ...", args[0]));
                    break;
            }
        }

        private static void LogInfos(string text)
        {
            Console.WriteLine(text + Environment.NewLine);
            Console.WriteLine("0 --> folderCompare" + Environment.NewLine);
            Console.WriteLine("1 --> brackets \"[]\"" + Environment.NewLine);
            Console.WriteLine("2 --> icons \"£\"" + Environment.NewLine);
            Console.WriteLine("3 --> variables \"$\"" + Environment.NewLine);
            Console.WriteLine("4 --> colors \"§\"" + Environment.NewLine);
            Console.WriteLine("5 --> innerDoubleQuotes \" ... \"\" ... \"" + Environment.NewLine);
            Console.WriteLine("6 --> keys \"...\"" + Environment.NewLine);
        }

        private static FileReader Brackets()
        {
            FileReader fileReader = new FileReader();
            fileReader.PathReplace = "brackets";
            fileReader.StringParser = ParseBrackets();
            return fileReader;
        }

        private static IStringParser ParseBrackets()
        {
            StringParser stringParser = new StringParser();
            stringParser.StartTag = "[";
            stringParser.EndTags.Clear();
            stringParser.EndTags.Add("]");
            return stringParser;
        }

        private static FileReader Icons()
        {
            FileReader fileReader = new FileReader();
            fileReader.PathReplace = "icons";
            fileReader.StringParser = ParseIcons();
            return fileReader;
        }

        private static FileReader FolderCompare()
        {
            FileReader fileReader = new FileReader();
            return fileReader;
        }

        private static StringParser ParseIcons()
        {
            StringParser stringParser = new StringParser();
            stringParser.StartTag = "£";
            stringParser.EndTags.Add(" ");
            stringParser.EndTags.Add("\n");
            stringParser.EndTags.Add("\"");
            return stringParser;
        }

        private static FileReader Variables()
        {
            FileReader fileReader = new FileReader();
            fileReader.PathReplace = "variables";
            fileReader.StringParser = ParseVariables();
            return fileReader;
        }

        private static IStringParser ParseVariables()
        {
            StringParser stringParser = new StringParser();
            stringParser.StartTag = "$";
            stringParser.EndTags.Add("$");
            return stringParser;
        }

        private static FileReader Colors()
        {
            FileReader fileReader = new FileReader();
            fileReader.PathReplace = "colors";
            fileReader.StringParser = ParseColors();
            return fileReader;
        }

        private static IStringParser ParseColors()
        {
            StringParser stringParser = new StringParser();
            stringParser.StartTag = "§";
            stringParser.EndTags.Add("§!");
            stringParser.SubStringCount = 1;
            return stringParser;
        }

        private static FileReader InnerDoubleQuotes()
        {
            FileReader fileReader = new FileReader();
            fileReader.PathReplace = "innerDoubleQuotes";
            fileReader.StringParser = ParseInnerDoubleQuotes();
            return fileReader;
        }

        private static IStringParser ParseInnerDoubleQuotes()
        {
            StringParserFirstLast stringParser = new StringParserFirstLast();
            stringParser.StartTag = "\"";
            stringParser.EndTags.Add("\"");
            return stringParser;
        }

        private static FileReader Keys()
        {
            FileReader fileReader = new FileReader();
            fileReader.PathReplace = "keys";
            fileReader.StringParser = ParseKeys();
            return fileReader;
        }

        private static IStringParser ParseKeys()
        {
            StringParserKey stringParser = new StringParserKey();
            stringParser.StartTag = "\"";
            stringParser.LineIgnores.Add("#");
            stringParser.LineIgnores.Add(" #");
            stringParser.LineIgnores.Add("  #");
            return stringParser;
        }


        private static void ParseDirectoryGerman(FileReader fileReader)
        {
            DirectoryParser directoryParser = new DirectoryParser();
            directoryParser.FileReader = fileReader;

            List<FileWithToken> files = directoryParser.ParseDirectory(HoI4_TranslationHelper_Config.PathGerman);
            Console.WriteLine("Parsing directory: " + HoI4_TranslationHelper_Config.PathGerman +"; found count files: " + files.Count);

            FileNameReplacer fileNameReplacer = new FileNameReplacer();
            ParameterizeGerman(fileNameReplacer);

            foreach (FileWithToken fileWithToken in files)
            {
                fileNameReplacer.Replace(fileWithToken);
                FileWriter.Write(fileWithToken);
            }
        }
        private static void ParseDirectoryEnglish(FileReader fileReader)
        {
            DirectoryParser directoryParser = new DirectoryParser();
            directoryParser.FileReader = fileReader;

            List<FileWithToken> files = directoryParser.ParseDirectory(HoI4_TranslationHelper_Config.PathEnglish);
            Console.WriteLine("Parsing directory: " + HoI4_TranslationHelper_Config.PathEnglish + "; found count files: " + files.Count);

            FileNameReplacer fileNameReplacer = new FileNameReplacer();
            ParameterizeEnglis(fileNameReplacer);

            foreach (FileWithToken fileWithToken in files)
            {
                fileNameReplacer.Replace(fileWithToken);
                FileWriter.Write(fileWithToken);
            }
        }

        private static void ParameterizeGerman( FileNameReplacer fileNameReplacer )
        {
            fileNameReplacer.tokenToFind = "_l_german";
            fileNameReplacer.tokenReplaceWith = "";
            fileNameReplacer.extendPath = "german";
        }

        private static void ParameterizeEnglis(FileNameReplacer fileNameReplacer)
        {
            fileNameReplacer.tokenToFind = "_l_english";
            fileNameReplacer.tokenReplaceWith = "";
            fileNameReplacer.extendPath = "english";
        }

        private static void ReadConfig()
        {
            XmlDocument config = XMLFileUtility.Load(Constance.config);
            if( null == config )
            {
                Console.WriteLine("Unable to load config: " + Constance.config + Environment.NewLine);
                return;
            }
            XmlNodeList configPaths = config.SelectNodes("/HoI4-TranslationHelper/Paths");

            string pathEnglish = FindNodeByName(configPaths, Constance.configNodePathEnglish);
            if( null != pathEnglish )
            {
                HoI4_TranslationHelper_Config.PathEnglish = pathEnglish;
            }

            string pathGerman = FindNodeByName(configPaths, Constance.configNodePathGerman);
            if (null != pathGerman )
            {
                HoI4_TranslationHelper_Config.PathGerman = pathGerman;
            }
        }

        private static string FindNodeByName(XmlNodeList nodes, string nodeName )
        {
            foreach(XmlNode node in nodes )
            {
                return node[nodeName].InnerText;
            }
            return null;
        }
    }
}
