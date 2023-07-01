using System;
using System.Collections.Generic;

namespace HoI4_TranslationHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseDirectoryGerman();
            ParseDirectoryEnglish();
        }

        private static void ParseDirectoryGerman()
        {
            DirectoryParser directoryParser = new DirectoryParser();
            StringParser stringParser = new StringParser();
//            stringParser.StartTag = "£";
//            stringParser.EndTag = " ";
            FileReader fileReader = new FileReader();
            fileReader.StringParser = stringParser;
            directoryParser.FileReader = fileReader;
            Parameterize(directoryParser);

            List<FileWithToken> filesGerman = directoryParser.ParseDirectory(Constance.pathGerman);
            FileNameReplacer fileNameReplacer = new FileNameReplacer();
            ParameterizeGerman(fileNameReplacer);

            foreach (FileWithToken fileWithToken in filesGerman)
            {
                fileNameReplacer.Replace(fileWithToken);
                FileWriter.Write(fileWithToken);
            }
        }

        private static void ParseDirectoryEnglish()
        {
            DirectoryParser directoryParser = new DirectoryParser();
            FileReader fileReader = new FileReader();
            directoryParser.FileReader = fileReader;
            Parameterize(directoryParser);

            FileNameReplacer fileNameReplacer = new FileNameReplacer();
            ParameterizeEnglis(fileNameReplacer);
            List<FileWithToken> filesEnglish = directoryParser.ParseDirectory(Constance.pathEnglish);
            foreach (FileWithToken fileWithToken in filesEnglish)
            {
                fileNameReplacer.Replace(fileWithToken);
                FileWriter.Write(fileWithToken);
            }
        }

        private static void Parameterize(DirectoryParser directoryParser)
        {

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
