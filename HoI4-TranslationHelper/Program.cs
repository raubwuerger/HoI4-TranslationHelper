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
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                LogInfos("No arguments passed ...");
                return;
            }

            ConfigReader configReader = new ConfigReader(); 
            if( false == configReader.Read() )
            {
                return;
            }

            switch (args[0])
            {
                case "0":
                    MissingTranslationFilesCreator.Create();
                    break;
                case "1":
                    ParseDirectoryGerman(FileTokenReaderFactory.Instance.CreateReaderBrackets());
                    ParseDirectoryEnglish(FileTokenReaderFactory.Instance.CreateReaderBrackets());
                    break;
                case "2":
                    ParseDirectoryGerman(FileTokenReaderFactory.Instance.CreateReaderIcons());
                    ParseDirectoryEnglish(FileTokenReaderFactory.Instance.CreateReaderIcons());
                    break;
                case "3":
                    ParseDirectoryGerman(FileTokenReaderFactory.Instance.CreateReaderVariables());
                    ParseDirectoryEnglish(FileTokenReaderFactory.Instance.CreateReaderVariables());
                    break;
                case "4":
                    ParseDirectoryGerman(FileTokenReaderFactory.Instance.CreateReaderColors());
                    ParseDirectoryEnglish(FileTokenReaderFactory.Instance.CreateReaderColors());
                    break;
                case "5":
                    ParseDirectoryGerman(FileTokenReaderFactory.Instance.CreateReaderInnerDoubleQuotes());
                    ParseDirectoryEnglish(FileTokenReaderFactory.Instance.CreateReaderInnerDoubleQuotes());
                    break;
                case "6":
                    MissingKeysCreator.Create();
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

        private static List<String> FindMissingKeysForTranslationFile(FileTokenReader fileReader)
        {
            DirectoryParser directoryParserEnglish = new DirectoryParser();
            directoryParserEnglish.FileReader = fileReader;

            List<FileWithToken> files = directoryParserEnglish.ParseDirectory(HoI4_TranslationHelper_Config.PathEnglish);
            Console.WriteLine("Parsing directory: " + HoI4_TranslationHelper_Config.PathGerman + "; found count files: " + files.Count);

            FileNameReplacer fileNameReplacer = new FileNameReplacer();
            Utility.ParameterizeGerman(fileNameReplacer);

            foreach (FileWithToken fileWithToken in files)
            {
                fileNameReplacer.Replace(fileWithToken);
                FileWriter.Write(fileWithToken);
            }

            return null;
        }

        private static FolderReader FolderCompare(string fileNamePartToIgnore)
        {
            FolderReader folderReader = new FolderReader();
            folderReader.fileNamePartToIgnore = fileNamePartToIgnore;
            return folderReader;
        }

        private static void ParseDirectoryGerman(FileTokenReader fileReader)
        {
            DirectoryParser directoryParser = new DirectoryParser();
            directoryParser.FileReader = fileReader;

            List<FileWithToken> files = directoryParser.ParseDirectory(HoI4_TranslationHelper_Config.PathGerman);
            Console.WriteLine("Parsing directory: " + HoI4_TranslationHelper_Config.PathGerman +"; found count files: " + files.Count);

            FileNameReplacer fileNameReplacer = new FileNameReplacer();
            Utility.ParameterizeGerman(fileNameReplacer);

            foreach (FileWithToken fileWithToken in files)
            {
                fileNameReplacer.Replace(fileWithToken);
                FileWriter.Write(fileWithToken);
            }
        }

        private static void ParseDirectoryEnglish(FileTokenReader fileReader)
        {
            DirectoryParser directoryParser = new DirectoryParser();
            directoryParser.FileReader = fileReader;

            List<FileWithToken> files = directoryParser.ParseDirectory(HoI4_TranslationHelper_Config.PathEnglish);
            Console.WriteLine("Parsing directory: " + HoI4_TranslationHelper_Config.PathEnglish + "; found count files: " + files.Count);

            FileNameReplacer fileNameReplacer = new FileNameReplacer();
            Utility.ParameterizeEnglish(fileNameReplacer);

            foreach (FileWithToken fileWithToken in files)
            {
                fileNameReplacer.Replace(fileWithToken);
                FileWriter.Write(fileWithToken);
            }
        }

    }
}
