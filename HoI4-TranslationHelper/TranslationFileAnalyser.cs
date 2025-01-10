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
            CheckMissingKeys(localisationEnglish, localisationGerman);
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

        private static DataSetLineObjectCompare CreateDataSetLineObjectCompare(List<TranslationFile> localisation)
        {
            DataSetLineObjectCompare dataSetLineObjectCompare = new DataSetLineObjectCompare();

            foreach (TranslationFile translationFile in localisation)
            {
                foreach (var item in translationFile.Lines)
                {
                    if( null == item.Value.Key )
                    {
                        continue;
                    }

                    if (true == dataSetLineObjectCompare.keysUnique.ContainsKey(item.Value.Key))
                    {
                        dataSetLineObjectCompare.keysMultiple.Add(item.Value);
                    }
                    else
                    {
                        dataSetLineObjectCompare.keysUnique.Add(item.Value.Key, item.Value);
                    }
                }
            }
            return dataSetLineObjectCompare;
        }

        private static void CheckMissingKeys(List<TranslationFile> localisationEnglish, List<TranslationFile> localisationGerman)
        {
            DataSetLineObjectCompare dataSetLineObjectCompareEnglish = CreateDataSetLineObjectCompare(localisationEnglish);
            DataSetLineObjectCompare dataSetLineObjectCompareGerman = CreateDataSetLineObjectCompare(localisationGerman);

            LogMissingKeys(dataSetLineObjectCompareEnglish.keysUnique, dataSetLineObjectCompareGerman.keysUnique);
            LogMissingBrackets(dataSetLineObjectCompareEnglish.keysUnique, dataSetLineObjectCompareGerman.keysUnique);
//            LogMissingNestingStrings(dataSetLineObjectCompareEnglish.keysUnique, dataSetLineObjectCompareGerman.keysUnique);

            LogMultipleKeys(dataSetLineObjectCompareEnglish.keysMultiple);
            LogMultipleKeys(dataSetLineObjectCompareGerman.keysMultiple);
        }

        private static void LogMultipleKeys(List<LineObject> keysMultiple)
        {
            Console.WriteLine("Keys existing multiple times" +Environment.NewLine);
            keysMultiple.ForEach(key => { Console.WriteLine(key.TranslationFile + ": " + key.Key + ":" + key.LineNumber); });
            Console.WriteLine(Environment.NewLine);
        }

        private static void LogMissingKeys(Dictionary<string, LineObject> keysBase, Dictionary<string, LineObject> keysShould)
        {
            Console.WriteLine("Missing keys" + Environment.NewLine);
            List<LineObject> missingKeys = new List<LineObject>();
            keysBase.ToList().ForEach
            (
                pair =>
                {
                    if( false == keysShould.ContainsKey(pair.Key) )
                    {
                        missingKeys.Add(pair.Value);
                    }
                }
            );

            missingKeys.ForEach(keys => Console.WriteLine(keys.Key));
            Console.WriteLine(Environment.NewLine);
        }

        private static void LogMissingBrackets(Dictionary<string, LineObject> keysBase, Dictionary<string, LineObject> keysShould)
        {
            Console.WriteLine("Missing brackets []" + Environment.NewLine);
            List<LineObject> missingBrackets = new List<LineObject>();

            keysBase.ToList().ForEach
            (
                pair =>
                {
                    if (true == keysShould.ContainsKey(pair.Key))
                    {
                        LineObject lineObject = new LineObject(0UL);
                        if (true == keysShould.TryGetValue(pair.Key, out lineObject))
                        {
                            List<string> missing = pair.Value.Brackets.Except(lineObject.Brackets, StringComparer.OrdinalIgnoreCase).ToList();
                            if (missing.Count > 0)
                            {
                                LineObject missingLineObject = new LineObject(pair.Value.LineNumber);
                                missingLineObject.Brackets = missing;
                                missingBrackets.Add(missingLineObject);
                            }
                        }
                    }
                }
            );

            missingBrackets.ForEach
            (
                item => Console.WriteLine(item.LineNumber + ": " +string.Join(", ", item.Brackets)  )
            );
        }
        // + ": " + item.Brackets.ForEach(Console.WriteLine)
        //            missingNestingStrings.ForEach(item => Console.WriteLine(item.Key +": " +item.Brackets.ForEach(Console.WriteLine)));

        private static string CreateStringFromList(List<string> strings)
        {
            string result = string.Empty;
            foreach ( string s in strings )
            { 
                result += s;
                result += "|";
            }
            return result;
        }

        private static void LogMissingNestingStrings(Dictionary<string, LineObject> keysBase, Dictionary<string, LineObject> keysShould)
        {
            Console.WriteLine("Missing NestingStrings $$" + Environment.NewLine);
            List<LineObject> missingNestingStrings = new List<LineObject>();
            keysBase.ToList().ForEach
            (
                pair =>
                {
                    if (false == keysShould.ContainsKey(pair.Key))
                    {
                        missingNestingStrings.Add(pair.Value);
                    }
                }
            );

//            missingNestingStrings.ForEach(item => Console.WriteLine(item.Key +": " +item.Brackets.ForEach(Console.WriteLine)));
        }
    }
}