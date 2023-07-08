using System;
using System.Collections.Generic;

namespace HoI4_TranslationHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            if( args.Length < 1 )
            {
                LogInfos("No arguments passed ...");
                return;
            }

            switch(args[0])
            {
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
                    LogInfos(string.Format("Wrong argument {0} passed ...", args[0]) );
                    break;
            }
        }

        private static void LogInfos(string text)
        {
            Console.WriteLine(text + Environment.NewLine);
            Console.WriteLine("1 --> brackets \"[]\"" + Environment.NewLine);
            Console.WriteLine("2 --> icons \"£\"" + Environment.NewLine);
            Console.WriteLine("3 --> variables \"$\"" + Environment.NewLine);
            Console.WriteLine("4 --> colors \"§\"" + Environment.NewLine);
            Console.WriteLine("5 --> innerDoubleQuotes \"\" ... \" \"" + Environment.NewLine);
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

            List<FileWithToken> filesGerman = directoryParser.ParseDirectory(Constance.pathGerman);
            FileNameReplacer fileNameReplacer = new FileNameReplacer();
            ParameterizeGerman(fileNameReplacer);

            foreach (FileWithToken fileWithToken in filesGerman)
            {
                fileNameReplacer.Replace(fileWithToken);
                FileWriter.Write(fileWithToken);
            }
        }
        private static void ParseDirectoryEnglish(FileReader fileReader)
        {
            DirectoryParser directoryParser = new DirectoryParser();
            directoryParser.FileReader = fileReader;

            FileNameReplacer fileNameReplacer = new FileNameReplacer();
            ParameterizeEnglis(fileNameReplacer);
            List<FileWithToken> filesEnglish = directoryParser.ParseDirectory(Constance.pathEnglish);
            foreach (FileWithToken fileWithToken in filesEnglish)
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
    }
}
