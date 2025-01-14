using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HoI4_TranslationHelper
{
    internal class ReSubstitutor
    {
        private Dictionary<string, string> _nestingStringsReSubstitute = new Dictionary<string, string>();
        private Dictionary<string, string> _colorCodeReSubstitute = new Dictionary<string, string>();
        private Dictionary<string, string> _namespaceReSubstitute = new Dictionary<string, string>();
        private Dictionary<string, string> _iconReSubstitute = new Dictionary<string, string>();
        private Dictionary<string, string> _newLineReSubstitute = new Dictionary<string, string>();

        FileReaderSubstitutionItem fileReaderSubstitutionItem = new FileReaderSubstitutionItem();

        private static string SUBSTITUTION_FILE_APPENDIX = ".substituted.txt";
        private TranslationFileSetSubstitution _translationFileSetSubstitution;

        internal TranslationFileSetSubstitution TranslationFileSetSubstitution { get => _translationFileSetSubstitution; set => _translationFileSetSubstitution = value; }

        public void ReSubstitute()
        {
            if (_translationFileSetSubstitution == null)
            {
                return;
            }

            ReadSubstitutionFiles();

            ValidateAgaintsSubstitutionDataFiles();

            ReSubstitute(_translationFileSetSubstitution.SubstitutedFile.Lines.Values.ToList());
//            ReSubstitute(_translationFileSetSubstitution.SubstitutedFile.FileName);
        }

        private bool ReadSubstitutionFiles()
        {
            fileReaderSubstitutionItem.FileName = _translationFileSetSubstitution.PathNestingStringsFile;
            _nestingStringsReSubstitute = fileReaderSubstitutionItem.Read();

            fileReaderSubstitutionItem.FileName = _translationFileSetSubstitution.PathColorCodeFile;
            _colorCodeReSubstitute = fileReaderSubstitutionItem.Read();

            fileReaderSubstitutionItem.FileName = _translationFileSetSubstitution.PathNamespaceFile;
            _namespaceReSubstitute = fileReaderSubstitutionItem.Read();

            fileReaderSubstitutionItem.FileName = _translationFileSetSubstitution.PathIconFile;
            _iconReSubstitute = fileReaderSubstitutionItem.Read();

            AddNewLineToResubstitute();
            //TODO: 2025-01-14 - JHA - Check if all files have been successfully read

            return true;
        }

        private void AddNewLineToResubstitute()
        {
            _newLineReSubstitute.Add("___NL___", "\\n");
        }

        private void ReSubstitute( string fileName )
        {
            string allText = File.ReadAllText(fileName);
            foreach( KeyValuePair<string,string> keyValuePair in _namespaceReSubstitute )
            {
                allText = allText.Replace(keyValuePair.Key, keyValuePair.Value);
            }
        }

        private void ReSubstitute(List<LineObject> lineObjects )
        {
            if (lineObjects == null)
            {
                return;
            }

            if (lineObjects.Count == 0)
            {
                return;
            }

            foreach (var lineObject in lineObjects)
            {
                ReSubstitute(lineObject, _nestingStringsReSubstitute);
                ReSubstitute(lineObject, _colorCodeReSubstitute);
                ReSubstitute(lineObject, _namespaceReSubstitute);
                ReSubstitute(lineObject, _iconReSubstitute);
            }

            TranslationFileCreator translationFileCreator = new TranslationFileCreator();
            TranslationFile translationFile = translationFileCreator.CopyExceptFileName(_translationFileSetSubstitution.SubstitutedFile.FileName + SUBSTITUTION_FILE_APPENDIX, _translationFileSetSubstitution.SubstitutedFile);

            Utility.WriteTranslationFile(translationFile);

            Console.WriteLine("Finished ...");
        }

        //TODO: 2025-01-14 - JHA - Extract in separate class SubstitutionFileValidator
        private void ValidateAgaintsSubstitutionDataFiles()
        {
            Console.WriteLine("Validating file: " + _translationFileSetSubstitution.SubstitutedFile.FileName);
            string allText = File.ReadAllText(_translationFileSetSubstitution.SubstitutedFile.FileName);
            int fileOriginal = GetItemCount();

            _nestingStringsReSubstitute = Validate(allText, _nestingStringsReSubstitute);
            _colorCodeReSubstitute = Validate(allText, _colorCodeReSubstitute);
            _namespaceReSubstitute = Validate(allText, _namespaceReSubstitute);
            _iconReSubstitute = Validate(allText, _iconReSubstitute);

            Console.WriteLine("Overall items missing: " + (fileOriginal - GetItemCount()));
            Console.WriteLine();
        }

        private int GetItemCount()
        {
            return _nestingStringsReSubstitute.Count + _colorCodeReSubstitute.Count + _namespaceReSubstitute.Count + _iconReSubstitute.Count;
        }

        private Dictionary<string, string> Validate( string text, Dictionary<string, string> substitutionSubSet )
        {
            Dictionary<string, string> validItems = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> keyValuePair in substitutionSubSet)
            {
                if (text.Contains(keyValuePair.Key))
                {
                    validItems.Add(keyValuePair.Key, keyValuePair.Value);
                    continue;
                }

                Console.WriteLine("Substitution item not found: " + keyValuePair.Key + ";" + keyValuePair.Value);
            }
            Console.WriteLine("Items missing: " + (substitutionSubSet.Count - validItems.Count).ToString());

            return validItems;
        }

        private void ReSubstitute(LineObject lineObject, Dictionary<string, string> substitutions )
        {
            if (false == substitutions.Any())
            {
                return;
            }

            KeyValuePair<string, string> keyValuePair = substitutions.First();
            if (false == lineObject.OriginalLine.Contains(keyValuePair.Key))
            {
                return;
            }

            lineObject.OriginalLine = lineObject.OriginalLine.Replace(keyValuePair.Key, keyValuePair.Value);
            substitutions.Remove(keyValuePair.Key);
            Console.WriteLine("Substituted (" + lineObject.LineNumber + ") " + keyValuePair.Key + " --> " + keyValuePair.Value);
        }
    }
}
