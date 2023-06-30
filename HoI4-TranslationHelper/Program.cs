using System;
using System.Collections.Generic;

namespace HoI4_TranslationHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            List<FileWithToken> filesGerman =  DirectoryParser.ParseDirectory(Constance.pathGerman);
            FileNameReplacer fileNameReplacer = new FileNameReplacer();
            fileNameReplacer.tokenToFind = "_l_german";
            fileNameReplacer.tokenReplaceWith = "";
            fileNameReplacer.extendPath = "german";
            foreach ( FileWithToken fileWithToken in filesGerman )
            {
                fileNameReplacer.Replace(fileWithToken);
                FileWriter.Write(fileWithToken);
            }

            fileNameReplacer.tokenToFind = "_l_english";
            fileNameReplacer.tokenReplaceWith = "";
            fileNameReplacer.extendPath = "english";
            List<FileWithToken> filesEnglish = DirectoryParser.ParseDirectory(Constance.pathEnglish);
            foreach (FileWithToken fileWithToken in filesEnglish)
            {
                fileNameReplacer.Replace(fileWithToken);
                FileWriter.Write(fileWithToken);
            }
        }
    }
}
