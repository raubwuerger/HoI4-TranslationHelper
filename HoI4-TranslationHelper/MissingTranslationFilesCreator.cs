using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public class MissingTranslationFilesCreator
    {
        public static void Create()
        {
                List<string> missingTranslationFiles = FindMissingTranslationFiles();
                //INFO: 2024-12-06 - JHA - Currently not active as not fully thought through
                /*
                foreach(string file  in missingTranslationFiles) 
                { 
                    if( true == File.Exists(file) )
                    {
                        continue;
                    }

                    using(FileStream fs = File.Create(file)) 
                    {
                        byte[] content = new UTF8Encoding(true).GetBytes("l_german:\n");
                        //fs.Write(content, 0, content.Length);
                    }
                }
                */
        }

        private static List<string> FindMissingTranslationFiles()
        {
            DirectoryParserCompare directoryParserEnglish = new DirectoryParserCompare();
            Dictionary<string, string> filesEnglish = directoryParserEnglish.ParseDirectory(HoI4_TranslationHelper_Config.PathEnglish);
            Dictionary<string, string> filesEnglishReplaced = Utility.RemoveStringFromKey(filesEnglish, Constants.localisationEnglish);

            DirectoryParserCompare directoryParserGerman = new DirectoryParserCompare();
            Dictionary<string, string> filesGerman = directoryParserGerman.ParseDirectory(HoI4_TranslationHelper_Config.PathGerman);
            Dictionary<string, string> filesGermanReplaced = Utility.RemoveStringFromKey(filesGerman, Constants.localisationGerman);


            List<string> missingGerman = filesEnglishReplaced.Keys.Except(filesGermanReplaced.Keys, StringComparer.OrdinalIgnoreCase).ToList();
            List<string> toMuchGerman = filesGermanReplaced.Keys.Except(filesEnglishReplaced.Keys, StringComparer.OrdinalIgnoreCase).ToList();

            Console.WriteLine("################################################################################");
            Console.WriteLine("##### German translation files to delete:");
            Console.WriteLine("################################################################################");
            foreach (string file in toMuchGerman)
            {
                Console.WriteLine(file + Constants.localisationGerman + Constants.localisationExtension);
            }


            List<string> missingTranslationFiles = new List<string>();
            Console.WriteLine("################################################################################");
            Console.WriteLine("##### German translation files missing:");
            Console.WriteLine("################################################################################");
            foreach (string file in missingGerman)
            {
                Console.WriteLine(file + Constants.localisationGerman + Constants.localisationExtension);
                missingTranslationFiles.Add(CreateMissingGermanTranslationFile(file));
            }

            return missingTranslationFiles;
        }
        private static string CreateMissingGermanTranslationFile(string missingFile)
        {
            return Path.Combine(HoI4_TranslationHelper_Config.PathGerman, Path.GetFileName(missingFile + Constants.localisationGerman + Constants.localisationExtension));
        }

    }
}
