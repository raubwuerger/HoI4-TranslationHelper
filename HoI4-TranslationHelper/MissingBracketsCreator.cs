using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    internal class MissingBracketsCreator
    {
        static string FILE_PATTERN = "*.yml";
        public static void Create()
        {
            string[] files = Directory.GetFiles(HoI4_TranslationHelper_Config.PathEnglish, FILE_PATTERN, SearchOption.AllDirectories);
            if (files.Length <= 0)
            {
                return;
            }

            List<TranslationFile> translationFiles = new List<TranslationFile>();
            TranslationFileCreator translationFileCreator = new TranslationFileCreator();

            FileTokenReader fileTokenReader = FileTokenReaderFactory.Instance.CreateReaderBrackets();
            foreach (string file in files)
            {
                translationFiles.Add(translationFileCreator.Create(file));
            }

        }
    }
}