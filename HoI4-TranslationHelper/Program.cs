using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Xml;

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
                    CreateMissingTranslationFiles();
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

        private static void CreateMissingTranslationFiles()
        {
            List<string> missingTranslationFiles = FindMissingTranslationFiles();
            foreach(string file  in missingTranslationFiles) 
            { 
                if( true == File.Exists(file) )
                {
                    continue;
                }

                using(FileStream fs = File.Create(file)) 
                {
                    byte[] content = new UTF8Encoding(true).GetBytes("l_german:\n");
//                    fs.Write(content, 0, content.Length);
                }
            }
        }

        private static List<string> FindMissingTranslationFiles()
        {
            DirectoryParserCompare directoryParserEnglish = new DirectoryParserCompare();
            Dictionary<string, string> filesEnglish = directoryParserEnglish.ParseDirectory(HoI4_TranslationHelper_Config.PathEnglish);
            Dictionary<string, string> filesEnglishReplaced = RemoveStringFromKey(filesEnglish, Constants.localisationEnglish);

            DirectoryParserCompare directoryParserGerman = new DirectoryParserCompare();
            Dictionary<string, string> filesGerman = directoryParserGerman.ParseDirectory(HoI4_TranslationHelper_Config.PathGerman);
            Dictionary<string, string> filesGermanReplaced = RemoveStringFromKey(filesGerman, Constants.localisationGerman);


            List<string> missingGerman = filesEnglishReplaced.Keys.Except(filesGermanReplaced.Keys).ToList();
            List<string> toMuchGerman = filesGermanReplaced.Keys.Except(filesEnglishReplaced.Keys).ToList();

            Console.WriteLine("##### German translation files to delete:");
            foreach (string file in toMuchGerman)
            {
                Console.WriteLine(file +Constants.localisationGerman +Constants.localisationExtension);
            }


            Console.WriteLine("##### German translation files missing:");
            foreach (string file in missingGerman)
            {
                Console.WriteLine(file +Constants.localisationGerman +Constants.localisationExtension);
            }

            var firstNotSecond = filesEnglishReplaced.Except(filesGermanReplaced).ToList();
            List<string> missingTranslationFiles = new List<string>();
            foreach (var file in firstNotSecond)
            {
                missingTranslationFiles.Add(Path.Combine(HoI4_TranslationHelper_Config.PathGerman, Path.GetFileName(file.Value + Constants.localisationGerman + Constants.localisationExtension)));
            }

            return missingTranslationFiles;
        }

        private static Dictionary<string,string> RemoveStringFromValue(Dictionary<string,string> list, string toRemove )
        {
            Dictionary<string,string> result = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> s in list ) 
            { 
                result.Add( s.Key, s.Value.Replace(toRemove,"") );
            }

            return result;
        }

        private static Dictionary<string, string> RemoveStringFromKey(Dictionary<string, string> list, string toRemove)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> s in list)
            {
                result.Add(s.Key.Replace(toRemove, ""), s.Value);
            }

            return result;
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
            IgnoreCommentLines(stringParser);
            return stringParser;
        }

        private static FileReader Icons()
        {
            FileReader fileReader = new FileReader();
            fileReader.PathReplace = "icons";
            fileReader.StringParser = ParseIcons();
            return fileReader;
        }

        private static FolderReader FolderCompare(string fileNamePartToIgnore)
        {
            FolderReader folderReader = new FolderReader();
            folderReader.fileNamePartToIgnore = fileNamePartToIgnore;
            return folderReader;
        }

        private static StringParser ParseIcons()
        {
            StringParser stringParser = new StringParser();
            stringParser.StartTag = "£";
            stringParser.EndTags.Add(" ");
            stringParser.EndTags.Add("\n");
            stringParser.EndTags.Add("\"");
            IgnoreCommentLines(stringParser);
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
            IgnoreCommentLines(stringParser);
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
            IgnoreCommentLines(stringParser);
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
            IgnoreCommentLines(stringParser);
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
            IgnoreCommentLines(stringParser);
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
            XmlDocument config = XMLFileUtility.Load(Constants.config);
            if( null == config )
            {
                Console.WriteLine("Unable to load config: " + Constants.config + Environment.NewLine);
                return;
            }
            XmlNodeList configPaths = config.SelectNodes("/HoI4-TranslationHelper/Paths");

            string pathEnglish = FindNodeByName(configPaths, Constants.configNodePathEnglish);
            if( null != pathEnglish )
            {
                HoI4_TranslationHelper_Config.PathEnglish = pathEnglish;
            }

            string pathGerman = FindNodeByName(configPaths, Constants.configNodePathGerman);
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

        private static void IgnoreCommentLines(StringParserBase stringParserBase)
        {
            stringParserBase.LineIgnores.Add("#");
            stringParserBase.LineIgnores.Add(" #");
            stringParserBase.LineIgnores.Add("  #");
        }
    }
}
