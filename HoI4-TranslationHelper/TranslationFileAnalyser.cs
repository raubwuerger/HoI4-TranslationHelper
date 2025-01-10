using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    internal class TranslationFileAnalyser
    {
        static string FILE_PATTERN = "*.yml";
        public static void Analyse()
        {
            List<TranslationFile> localisationEnglish = AnalyseDirectory(HoI4_TranslationHelper_Config.PathEnglish);
            List<TranslationFile> localisationGerman = AnalyseDirectory(HoI4_TranslationHelper_Config.PathGerman);
            DoCompare(localisationEnglish, localisationGerman);

        }

        private static List<TranslationFile> AnalyseDirectory( string directory )
        {
            string[] files = Directory.GetFiles(directory, FILE_PATTERN, SearchOption.AllDirectories);
            if (files.Length <= 0)
            {
                return null;
            }

            List<TranslationFile> translationFiles = new List<TranslationFile>();
            TranslationFileCreator translationFileCreator = new TranslationFileCreator();

            FileTokenReader fileTokenReader = FileTokenReaderFactory.Instance.CreateReaderBrackets();
            foreach (string file in files)
            {
                translationFiles.Add(translationFileCreator.Create(file));
            }

            return translationFiles;
        }

        private static void DoCompare(List<TranslationFile> localisationEnglish, List<TranslationFile> localisationGerman)
        {
            if( null == localisationEnglish || null == localisationGerman )
            {
                return;
            }

            CheckMissingTranslationFile(localisationEnglish,localisationGerman);
        }

        private static void CheckMissingTranslationFile(List<TranslationFile> localisationEnglish, List<TranslationFile> localisationGerman)
        {
            List<string> localisationFileNamesEnglish = localisationEnglish.ConvertAll(s => s.FileNameWithoutLocalisation);
            List<string> localisationFileNamesGerman = localisationGerman.ConvertAll(s => s.FileNameWithoutLocalisation);
            List<string> missingTranslationFiles = localisationFileNamesEnglish.Except(localisationFileNamesGerman).ToList<string>();

            if ( missingTranslationFiles.Count > 0 )
            {
                Console.WriteLine("Following transtlation files are missing:" + Environment.NewLine);
                foreach (string translationFile in missingTranslationFiles)
                {
                    Console.WriteLine(translationFile + Environment.NewLine);
                }
            }
        }
    }
}