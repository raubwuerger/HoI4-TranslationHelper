using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    internal class FileSubstitutor
    {
        private Dictionary<string, string> _nestingStringsSubstitute = new Dictionary<string, string>(); //ulong substitute number, string original text
        private Dictionary<string, string> _colorCodeSubstitute = new Dictionary<string, string>(); //ulong substitute number, string original text
        private Dictionary<string, string> _namespaceSubstitute = new Dictionary<string, string>(); //ulong substitute number, string original text
        private Dictionary<string, string> _iconSubstitute = new Dictionary<string, string>(); //ulong substitute number, string original text
        private Dictionary<string, string> _newLineSubstitute = new Dictionary<string, string>(); //ulong substitute number, string original text


        private Dictionary<string, string> _nestingStringsReSubstitute = new Dictionary<string, string>();
        private Dictionary<string, string> _colorCodeReSubstitute = new Dictionary<string, string>();
        private Dictionary<string, string> _namespaceReSubstitute = new Dictionary<string, string>();
        private Dictionary<string, string> _iconReSubstitute = new Dictionary<string, string>();
        private Dictionary<string, string> _newLineReSubstitute = new Dictionary<string, string>();

        FileWriterSubstitutionItem fileWriterSubstitutionItem = new FileWriterSubstitutionItem();
        FileReaderSubstitutionItem fileReaderSubstitutionItem = new FileReaderSubstitutionItem();

        public void ReSubstitute( TranslationFileSetSubstitution translationFileSetSubstitution )
        {
            if (translationFileSetSubstitution == null)
            {
                return;
            }

            fileReaderSubstitutionItem.FileName = translationFileSetSubstitution.PathNestingStringsFile;
            _nestingStringsReSubstitute = fileReaderSubstitutionItem.Read();

            fileReaderSubstitutionItem.FileName = translationFileSetSubstitution.PathColorCodeFile;
            _colorCodeReSubstitute = fileReaderSubstitutionItem.Read();

            fileReaderSubstitutionItem.FileName = translationFileSetSubstitution.PathNamespaceFile;
            _namespaceReSubstitute = fileReaderSubstitutionItem.Read();

            fileReaderSubstitutionItem.FileName = translationFileSetSubstitution.PathIconFile;
            _iconReSubstitute = fileReaderSubstitutionItem.Read();

            fileReaderSubstitutionItem.FileName = translationFileSetSubstitution.PathNewLineFile;
            _newLineReSubstitute = fileReaderSubstitutionItem.Read();

            DoReSubstitute(translationFileSetSubstitution);
        }

        public void DoReSubstitute( TranslationFileSetSubstitution translationFileSetSubstitution )
        {
            List<LineObject> lineObjects = translationFileSetSubstitution.SubstitutedFile.Lines.Values.ToList();
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
                ReSubstituteNestingString(lineObject);
                ReSubstituteColorCode(lineObject);
                ReSubstituteNamespace(lineObject);
                ReSubstituteIcon(lineObject);
                ReSubstituteNewLine(lineObject);
            }

            TranslationFileCreator translationFileCreator = new TranslationFileCreator();
            TranslationFile translationFile = translationFileCreator.CopyExceptFileName(translationFileSetSubstitution.SubstitutedFile.FileName + ".substituted.txt", translationFileSetSubstitution.SubstitutedFile);

            Utility.WriteTranslationFile(translationFile);

            Console.WriteLine("Finished ...");
        }

        private void ReSubstituteNestingString(LineObject lineObject)
        {
            if (false == _nestingStringsReSubstitute.Any())
            {
                return;
            }

            KeyValuePair<string, string> keyValuePair = _nestingStringsReSubstitute.First();
            if( false == lineObject.OriginalLine.Contains(keyValuePair.Key) )
            {
                return;
            }

            lineObject.OriginalLine = StringExtensionMethods.ReplaceFirst(lineObject.OriginalLine, keyValuePair.Key, keyValuePair.Value);
            _nestingStringsReSubstitute.Remove(keyValuePair.Key);
            Console.WriteLine("Substituted (" + lineObject.LineNumber + ") " + keyValuePair.Key + " --> " + keyValuePair.Value);

            if (false == _nestingStringsReSubstitute.Any())
            {
                return;
            }
            keyValuePair = _nestingStringsReSubstitute.First();
            if (true == lineObject.OriginalLine.Contains(keyValuePair.Key))
            {
                ReSubstituteNestingString(lineObject);
            }
        }

        private void ReSubstituteColorCode(LineObject lineObject)
        {
            if (false == _colorCodeReSubstitute.Any())
            {
                return;
            }

            KeyValuePair<string, string> keyValuePair = _colorCodeReSubstitute.First();
            if (false == lineObject.OriginalLine.Contains(keyValuePair.Key))
            {
                return;
            }

            lineObject.OriginalLine = StringExtensionMethods.ReplaceFirst(lineObject.OriginalLine, keyValuePair.Key, keyValuePair.Value);
            _colorCodeReSubstitute.Remove(keyValuePair.Key);
            Console.WriteLine("Substituted (" + lineObject.LineNumber + ") " + keyValuePair.Key + " --> " + keyValuePair.Value);

            if (false == _colorCodeReSubstitute.Any())
            {
                return;
            }

            keyValuePair = _colorCodeReSubstitute.First();
            if (true == lineObject.OriginalLine.Contains(keyValuePair.Key))
            {
                ReSubstituteColorCode(lineObject);
            }
        }

        private void ReSubstituteNamespace(LineObject lineObject)
        {
            if (false == _namespaceReSubstitute.Any())
            {
                return;
            }

            KeyValuePair<string, string> keyValuePair = _namespaceReSubstitute.First();
            if (false == lineObject.OriginalLine.Contains(keyValuePair.Key))
            {
                return;
            }

            lineObject.OriginalLine = StringExtensionMethods.ReplaceFirst(lineObject.OriginalLine, keyValuePair.Key, keyValuePair.Value);
            _namespaceReSubstitute.Remove(keyValuePair.Key);
            Console.WriteLine("Substituted (" + lineObject.LineNumber + ") " + keyValuePair.Key + " --> " + keyValuePair.Value);

            if (false == _namespaceReSubstitute.Any())
            {
                return;
            }

            keyValuePair = _namespaceReSubstitute.First();
            if (true == lineObject.OriginalLine.Contains(keyValuePair.Key))
            {
                ReSubstituteNamespace(lineObject);
            }
        }
        private void ReSubstituteIcon(LineObject lineObject)
        {
            if(false == _iconReSubstitute.Any() )
            {
                return;
            }

            KeyValuePair<string, string> keyValuePair = _iconReSubstitute.First();
            if (false == lineObject.OriginalLine.Contains(keyValuePair.Key))
            {
                return;
            }

            lineObject.OriginalLine = StringExtensionMethods.ReplaceFirst(lineObject.OriginalLine, keyValuePair.Key, keyValuePair.Value);
            _iconReSubstitute.Remove(keyValuePair.Key);
            Console.WriteLine("Substituted (" + lineObject.LineNumber + ") " + keyValuePair.Key + " --> " + keyValuePair.Value);

            if (false == _iconReSubstitute.Any())
            {
                return;
            }

            keyValuePair = _iconReSubstitute.First();
            if (true == lineObject.OriginalLine.Contains(keyValuePair.Key))
            {
                ReSubstituteIcon(lineObject);
            }
        }

        private void ReSubstituteNewLine(LineObject lineObject)
        {
            if (false == _newLineReSubstitute.Any())
            {
                return;
            }

            KeyValuePair<string, string> keyValuePair = _newLineReSubstitute.First();
            if (false == lineObject.OriginalLine.Contains(keyValuePair.Key))
            {
                return;
            }

            lineObject.OriginalLine = StringExtensionMethods.ReplaceFirst(lineObject.OriginalLine, keyValuePair.Key, keyValuePair.Value);
            _newLineReSubstitute.Remove(keyValuePair.Key);
            Console.WriteLine("Substituted (" + lineObject.LineNumber + ") " + keyValuePair.Key + " --> " + keyValuePair.Value);

            if (false == _newLineReSubstitute.Any())
            {
                return;
            }

            keyValuePair = _newLineReSubstitute.First();
            if (true == lineObject.OriginalLine.Contains(keyValuePair.Key))
            {
                ReSubstituteIcon(lineObject);
            }
        }
        public void Substitute( TranslationFile translationFile )
        {
            if ( translationFile == null ) 
            {
                return;
            }

            Substitute( translationFile.Lines.Values.ToList() );

            fileWriterSubstitutionItem.FileName = translationFile.FileName;
            fileWriterSubstitutionItem.FileSuffix = "";
            WriteSubstitionFile(translationFile);

            fileWriterSubstitutionItem.FileSuffix = "." + FileSubstitutionConstants.NESTING_STRING_SUFFIX;
            WriteSubstitionFile(_nestingStringsSubstitute);

            fileWriterSubstitutionItem.FileSuffix = "." + FileSubstitutionConstants.COLOR_CODE_SUFFIX;
            WriteSubstitionFile(_colorCodeSubstitute);

            fileWriterSubstitutionItem.FileSuffix = "." + FileSubstitutionConstants.NAMESPACE_SUFFIX;
            WriteSubstitionFile(_namespaceSubstitute);

            fileWriterSubstitutionItem.FileSuffix = "." + FileSubstitutionConstants.ICON_SUFFIX;
            WriteSubstitionFile(_iconSubstitute);

            fileWriterSubstitutionItem.FileSuffix = "." + FileSubstitutionConstants.NEW_LINE_SUFFIX;
            WriteSubstitionFile(_newLineSubstitute);
        }

        public void Substitute( List<LineObject> lineObjects )
        {
            if( lineObjects == null )
            {
                return;
            }

            if( lineObjects.Count == 0 ) 
            { 
                return;
            }

            foreach ( var lineObject in lineObjects ) 
            {
                SubstituteNestingString(lineObject);
                SubstituteColorCode(lineObject);
                SubstituteNamespace(lineObject);
                SubstituteIcon(lineObject);
                SubstituteNewLine(lineObject);
            }
        }

        private void SubstituteNestingString(LineObject lineObject)
        {
            List<string> token = lineObject.NestingStrings;

            string substitute = lineObject.OriginalLine;
            foreach (string subs in token)
            {
                substitute = StringExtensionMethods.ReplaceFirst(substitute,GenerateCompleteNestingStringToken(subs), GenerateNestingStringSubstitute(GenerateCompleteNestingStringToken(subs)));
            }

            lineObject.OriginalLineSubstituted = substitute;
        }
        private string GenerateCompleteNestingStringToken(string subs)
        {
            return FileSubstitutionConstants.NESTING_STRING_SIGN_START + subs + FileSubstitutionConstants.NESTING_STRING_SIGN_END;
        }

        private string GenerateNestingStringSubstitute(string sub)
        {
            int count = _nestingStringsSubstitute.Count();
            count++;
            string subString = FileSubstitutionConstants.SUBSTITUTION_START + FileSubstitutionConstants.NESTING_STRING_SUFFIX + count.ToString() + FileSubstitutionConstants.SUBSTITUTION_END;
            _nestingStringsSubstitute.Add(subString, sub);
            return subString;
        }

        private void SubstituteColorCode(LineObject lineObject)
        {
            List<string> token = lineObject.ColorCodes;

            string substitute = lineObject.OriginalLineSubstituted;
            foreach (string subs in token)
            {
                substitute = StringExtensionMethods.ReplaceFirst(substitute, GenerateCompleteColorCodeToken(subs), GenerateColorCodeSubstitute(GenerateCompleteColorCodeToken(subs)));
            }

            lineObject.OriginalLineSubstituted = substitute;
        }

        private string GenerateCompleteColorCodeToken(string subs)
        {
            return FileSubstitutionConstants.COLOR_CODE_SIGN_START + subs;
        }

        private string GenerateColorCodeSubstitute(string sub)
        {
            int count = _colorCodeSubstitute.Count();
            count++;
            string subString = FileSubstitutionConstants.SUBSTITUTION_START + FileSubstitutionConstants.COLOR_CODE_SUFFIX + count.ToString() + FileSubstitutionConstants.SUBSTITUTION_END;
            _colorCodeSubstitute.Add(subString, sub);
            return subString;
        }


        private void SubstituteNamespace(LineObject lineObject)
        {
            List<string> token = lineObject.NameSpaces;

            string substitute = lineObject.OriginalLineSubstituted;
            foreach (string subs in token)
            {
                substitute = StringExtensionMethods.ReplaceFirst(substitute, GenerateCompleteNamespaceToken(subs), GenerateNamespaceSubstitute(GenerateCompleteNamespaceToken(subs)));
            }

            lineObject.OriginalLineSubstituted = substitute;
        }

        private string GenerateCompleteNamespaceToken(string subs)
        {
            return FileSubstitutionConstants.NAMESPACE_START_SIGN_START + subs + FileSubstitutionConstants.NAMESPACE_START_SIGN_END;
        }

        private string GenerateNamespaceSubstitute(string sub)
        {
            int count = _namespaceSubstitute.Count();
            count++;
            string subString = FileSubstitutionConstants.SUBSTITUTION_START + FileSubstitutionConstants.NAMESPACE_SUFFIX + count.ToString() + FileSubstitutionConstants.SUBSTITUTION_END;
            _namespaceSubstitute.Add(subString, sub);
            return subString;
        }
        private void SubstituteIcon(LineObject lineObject)
        {
            List<string> token = lineObject.Icons;

            string substitute = lineObject.OriginalLineSubstituted;
            foreach (string subs in token)
            {
                substitute = StringExtensionMethods.ReplaceFirst(substitute, GenerateCompleteIconToken(subs), GenerateIconSubstitute(GenerateCompleteIconToken(subs)));
            }

            lineObject.OriginalLineSubstituted = substitute;
        }

        private string GenerateCompleteIconToken(string subs)
        {
            return FileSubstitutionConstants.ICON_START_SIGN_START + subs + FileSubstitutionConstants.ICON_START_SIGN_END;
        }

        private string GenerateIconSubstitute(string sub)
        {
            int count = _iconSubstitute.Count();
            count++;
            string subString = FileSubstitutionConstants.SUBSTITUTION_START + FileSubstitutionConstants.ICON_SUFFIX + count.ToString() + FileSubstitutionConstants.SUBSTITUTION_END;
            _iconSubstitute.Add(subString, sub);
            return subString;
        }

        private void SubstituteNewLine(LineObject lineObject)
        {
            List<string> token = lineObject.NewLines;

            string substitute = lineObject.OriginalLineSubstituted;
            foreach (string subs in token)
            {
                substitute = StringExtensionMethods.ReplaceFirst(substitute, GenerateCompleteNewLineToken(subs), GenerateNewLineSubstitute(GenerateCompleteNewLineToken(subs)));
            }

            lineObject.OriginalLineSubstituted = substitute;
        }

        private string GenerateCompleteNewLineToken(string subs)
        {
            return FileSubstitutionConstants.NEW_LINE_SIGN_START + subs + FileSubstitutionConstants.NEW_LINE_SIGN_END;
        }

        private string GenerateNewLineSubstitute(string sub)
        {
            int count = _newLineSubstitute.Count();
            count++;
            string subString = FileSubstitutionConstants.SUBSTITUTION_START + FileSubstitutionConstants.NEW_LINE_SUFFIX + count.ToString() + FileSubstitutionConstants.SUBSTITUTION_END;
            _newLineSubstitute.Add(subString, sub);
            return subString;
        }

        private void WriteSubstitionFile(TranslationFile translationFile)
        {
            Utility.WriteTranslationFile(translationFile);
        }
        private void WriteSubstitionFile( Dictionary<string, string> nestingStrings )
        {
            if ( nestingStrings == null )
            {
                return;
            }
            fileWriterSubstitutionItem.Write( nestingStrings );
        }
    }
}
