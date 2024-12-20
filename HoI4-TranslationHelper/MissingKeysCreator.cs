﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public class MissingKeysCreator
    {
        public static void Create()
        {
            DirectoryParser directoryParser = new DirectoryParser();
            directoryParser.FileReader = FileTokenReaderFactory.Instance.CreateReaderKeys();

            List<FileWithToken> filesEnglish = directoryParser.ParseDirectory(HoI4_TranslationHelper_Config.PathEnglish);
            Console.WriteLine("Parsing directory: " + HoI4_TranslationHelper_Config.PathEnglish + "; found count files: " + filesEnglish.Count);

            FileNameReplacer fileNameReplacerEnglish = new FileNameReplacer();
            Utility.ParameterizeEnglish(fileNameReplacerEnglish);
            fileNameReplacerEnglish.tokenReplaceWith = "_l_german";

            foreach (FileWithToken fileWithToken in filesEnglish)
            {
                fileNameReplacerEnglish.Replace(fileWithToken);
            }

            List<FileWithToken> filesGerman = directoryParser.ParseDirectory(HoI4_TranslationHelper_Config.PathGerman);
            Console.WriteLine("Parsing directory: " + HoI4_TranslationHelper_Config.PathGerman + "; found count files: " + filesGerman.Count);

            FileNameReplacer fileNameReplacerGerman = new FileNameReplacer();
            Utility.ParameterizeGerman(fileNameReplacerGerman);
            fileNameReplacerGerman.tokenReplaceWith = "_l_english";

            foreach (FileWithToken fileWithToken in filesGerman)
            {
                fileNameReplacerGerman.Replace(fileWithToken);
            }

            List<FileWithToken> filesToCompare = new List<FileWithToken>();
            foreach (FileWithToken fileEnglish in filesEnglish)
            {
                FileWithToken fileGerman = filesGerman.Find(f => (f.FileNameWithoutLocalisation.Equals(fileEnglish.FileNameWithoutLocalisation)));
                if (null == fileGerman)
                {
                    continue;
                }

                List<LineTextTupel> keysGerman = fileGerman.GetLineTextTupels;
                List<LineTextTupel> keysEnglish = fileEnglish.GetLineTextTupels;
                List<String> missingGermans = new List<String>();

                foreach (LineTextTupel lineTextTupel in keysEnglish)
                {
                    var item = keysGerman.FirstOrDefault(o => o._text.Equals(lineTextTupel._text));
                    if (item != null)
                    {
                        continue;
                    }
                    missingGermans.Add(lineTextTupel._text);

                }

                if (false == missingGermans.Any())
                {
                    return;
                }

                Console.WriteLine("##### Analyzing file: " + fileGerman.FileName);

                if (true == missingGermans.Any())
                {
                    Console.WriteLine("## German translation keys missing");
                    foreach (string missingGerman in missingGermans)
                    {
                        Console.WriteLine(missingGerman);
                    }
                }

                Console.WriteLine(Environment.NewLine);
            }

        }
    }

}
