﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    internal class Substitutor
    {
        private Dictionary<string, string> _nestingStringsSubstitute = new Dictionary<string, string>(); //ulong substitute number, string original text
        private Dictionary<string, string> _namespaceSubstitute = new Dictionary<string, string>(); //ulong substitute number, string original text
        private Dictionary<string, string> _iconSubstitute = new Dictionary<string, string>(); //ulong substitute number, string original text
        FileWriterSubstitutionItem fileWriterSubstitutionItem = new FileWriterSubstitutionItem();

        public void Substitute(TranslationFile translationFile)
        {
            if (translationFile == null)
            {
                return;
            }

            Console.WriteLine("Substituting file: " + translationFile.FileName);
            Substitute(translationFile.Lines.Values.ToList());
            Console.WriteLine("Substituted nesting strings : " + _nestingStringsSubstitute.Count);
            Console.WriteLine("Substituted name spaces : " + _namespaceSubstitute.Count);
            Console.WriteLine("Substituted icons : " + _iconSubstitute.Count);

            WriteSubstitionFiles(translationFile);
        }

        private bool WriteSubstitionFiles(TranslationFile translationFile)
        {
            fileWriterSubstitutionItem.FileName = translationFile.FileName;
            fileWriterSubstitutionItem.FileSuffix = "";
            WriteSubstitionFile(translationFile);

            fileWriterSubstitutionItem.FileSuffix = "." + FileSubstitutionConstants.NESTING_STRING_SUFFIX;
            WriteSubstitionFile(_nestingStringsSubstitute);

            fileWriterSubstitutionItem.FileSuffix = "." + FileSubstitutionConstants.NAMESPACE_SUFFIX;
            WriteSubstitionFile(_namespaceSubstitute);

            fileWriterSubstitutionItem.FileSuffix = "." + FileSubstitutionConstants.ICON_SUFFIX;
            WriteSubstitionFile(_iconSubstitute);

            Console.WriteLine("Overall items substituted: " + (_nestingStringsSubstitute.Count + _namespaceSubstitute.Count + _iconSubstitute.Count));

            //TODO: 2025-01-14 - JHA - Check if all files have been successfully written
            return true;

        }

        private void Substitute(List<LineObject> lineObjects)
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
                SubstituteNestingString(lineObject);
                SubstituteNamespace(lineObject);
                SubstituteIcon(lineObject);
            }
        }

        private void SubstituteNestingString(LineObject lineObject)
        {
            List<string> token = lineObject.NestingStrings;

            string substitute = lineObject.OriginalLine;
            foreach (string subs in token)
            {
                substitute = StringExtensionMethods.ReplaceFirst(substitute, GenerateCompleteNestingStringToken(subs), GenerateNestingStringSubstitute(GenerateCompleteNestingStringToken(subs), lineObject));
            }

            lineObject.OriginalLineSubstituted = substitute;
        }
        private string GenerateCompleteNestingStringToken(string subs)
        {
            return FileSubstitutionConstants.NESTING_STRING_SIGN_START + subs + FileSubstitutionConstants.NESTING_STRING_SIGN_END;
        }

        private string GenerateNestingStringSubstitute(string sub, LineObject lineObject)
        {
            int count = _nestingStringsSubstitute.Count();
            count++;
            string subString = FileSubstitutionConstants.SUBSTITUTION_START + FileSubstitutionConstants.NESTING_STRING_SUFFIX + count.ToString() + FileSubstitutionConstants.SUBSTITUTION_END;
            _nestingStringsSubstitute.Add(subString, CreateSubKeyLineTripel(sub, lineObject));
            return subString;
        }

        private void SubstituteNamespace(LineObject lineObject)
        {
            List<string> token = lineObject.NameSpaces;

            string substitute = lineObject.OriginalLineSubstituted;
            foreach (string subs in token)
            {
                substitute = StringExtensionMethods.ReplaceFirst(substitute, GenerateCompleteNamespaceToken(subs), GenerateNamespaceSubstitute(GenerateCompleteNamespaceToken(subs), lineObject));
            }

            lineObject.OriginalLineSubstituted = substitute;
        }

        private string GenerateCompleteNamespaceToken(string subs)
        {
            return FileSubstitutionConstants.NAMESPACE_START_SIGN_START + subs + FileSubstitutionConstants.NAMESPACE_START_SIGN_END;
        }

        private string GenerateNamespaceSubstitute(string sub, LineObject lineObject)
        {
            int count = _namespaceSubstitute.Count();
            count++;
            string subString = FileSubstitutionConstants.SUBSTITUTION_START + FileSubstitutionConstants.NAMESPACE_SUFFIX + count.ToString() + FileSubstitutionConstants.SUBSTITUTION_END;
            _namespaceSubstitute.Add(subString, CreateSubKeyLineTripel(sub, lineObject));
            return subString;
        }
        private void SubstituteIcon(LineObject lineObject)
        {
            List<string> token = lineObject.Icons;

            string substitute = lineObject.OriginalLineSubstituted;
            foreach (string subs in token)
            {
                substitute = StringExtensionMethods.ReplaceFirst(substitute, GenerateCompleteIconToken(subs), GenerateIconSubstitute(GenerateCompleteIconToken(subs), lineObject));
            }

            lineObject.OriginalLineSubstituted = substitute;
        }

        private string GenerateCompleteIconToken(string subs)
        {
            return FileSubstitutionConstants.ICON_START_SIGN_START + subs + FileSubstitutionConstants.ICON_START_SIGN_END;
        }

        private string GenerateIconSubstitute(string sub, LineObject lineObject)
        {
            int count = _iconSubstitute.Count();
            count++;
            string subString = FileSubstitutionConstants.SUBSTITUTION_START + FileSubstitutionConstants.ICON_SUFFIX + count.ToString() + FileSubstitutionConstants.SUBSTITUTION_END;
            _iconSubstitute.Add(subString, CreateSubKeyLineTripel(sub,lineObject));
            return subString;
        }

        private string CreateSubKeyLineTripel(string sub, LineObject lineObject)
        {
            return sub + ";" + lineObject.Key + ";" + lineObject.LineNumber;
        }

        private void WriteSubstitionFile(TranslationFile translationFile)
        {
            Utility.WriteTranslationFile(translationFile);
        }
        private void WriteSubstitionFile(Dictionary<string, string> nestingStrings)
        {
            if (nestingStrings == null)
            {
                return;
            }
            fileWriterSubstitutionItem.Write(nestingStrings);
        }

    }
}