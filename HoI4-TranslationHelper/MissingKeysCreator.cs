using System;
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

            Dictionary<string, FileWithToken> overallKeys = new Dictionary<string, FileWithToken>();
            Dictionary<string, string> duplicateKeys = new Dictionary<string, string>();

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
                List<LineTextTupel> missingGermans = new List<LineTextTupel>();

                foreach (LineTextTupel lineTextTupel in keysEnglish)
                {
                    if (true == overallKeys.ContainsKey(lineTextTupel.Token))
                    {
                        FileWithToken allreadyExisting = overallKeys[lineTextTupel.Token];
                        LineTextTupel lineTextTupelAllreadyExisting = allreadyExisting.GetLineTextTupels.Find( x => x.Token.Equals(lineTextTupel.Token));
                        if (false == duplicateKeys.ContainsKey(lineTextTupel.Token))
                        {
                            duplicateKeys.Add(lineTextTupel.Token, allreadyExisting.FileName + ":" + lineTextTupelAllreadyExisting.LineNumber + " and " + fileEnglish.FileName + ":" + lineTextTupel.LineNumber);
                        }
                    }
                    else
                    {
                        overallKeys.Add(lineTextTupel.Token, fileEnglish);
                    }
                    var item = keysGerman.FirstOrDefault(o => o.Token.Equals(lineTextTupel.Token));
                    if (item != null)
                    {
                        continue;
                    }
                    missingGermans.Add(lineTextTupel);

                }

                if (false == missingGermans.Any())
                {
                    continue;
                }

                Console.WriteLine("##### Analyzing file: " + fileGerman.FileName);

                if (true == missingGermans.Any())
                {
                    Console.WriteLine("## German translation keys missing");
                    foreach (LineTextTupel missingGerman in missingGermans)
                    {
                        Console.WriteLine(missingGerman.LineNumber + " - " +missingGerman.Token);
                    }
                }

                Console.WriteLine(Environment.NewLine);
            }

            if (true == duplicateKeys.Any())
            {
                Console.WriteLine("##### Following keys exist twice: ");
                foreach (KeyValuePair<string, string> entry in duplicateKeys)
                {
                    Console.WriteLine("key: " + entry.Key + " | files: " + entry.Value);
                }
            }
        }
    }

}
