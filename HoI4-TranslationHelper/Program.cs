using System;
using System.Collections.Generic;

namespace HoI4_TranslationHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseDirectoryGerman(new FileReader());
            ParseDirectoryGerman(Icons());

            ParseDirectoryEnglish(new FileReader());
            ParseDirectoryEnglish(Icons());
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
            stringParser.EndTags.Clear();
            stringParser.EndTags.Add(" ");
            stringParser.EndTags.Add("\n");
            stringParser.EndTags.Add("\"");
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
