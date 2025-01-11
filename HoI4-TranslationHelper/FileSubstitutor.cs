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
        private static string SUBSTITUTION_START = "___";
        private static string SUBSTITUTION_END = "___";

        private Dictionary<string, string> _nestingStringsSubstitute = new Dictionary<string, string>(); //ulong substitute number, string original text
        private static string NESTING_STRING_SUFFIX = "NS";
        private static string NESTING_STRING_SIGN_START = StringParserFactory.NESTING_STRINGS_START;
        private static string NESTING_STRING_SIGN_END = StringParserFactory.NESTING_STRINGS_END;
        IStringParser parserNestingStrings = StringParserFactory.Instance.CreateParserNestingStrings();


        private Dictionary<string, string> _colorCodeSubstitute = new Dictionary<string, string>(); //ulong substitute number, string original text
        private static string COLOR_CODE_SUFFIX = "CC";
        private static string COLOR_CODE_SIGN_START = StringParserFactory.COLOR_CODE_START;
        private static string COLOR_CODE_SIGN_END = StringParserFactory.COLOR_CODE_END;
        IStringParser parserColorCodes = StringParserFactory.Instance.CreateParserColorCodes();
        

        FileWriterSubstitutionItem fileWriterSubstitutionItem = new FileWriterSubstitutionItem();


        public void Substitute( TranslationFile translationFile )
        {
            if ( translationFile == null ) 
            {
                return;
            }

            fileWriterSubstitutionItem.FileName = translationFile.FileName;
            fileWriterSubstitutionItem.FileSuffix = "." + NESTING_STRING_SUFFIX;

            Substitute( translationFile.Lines.Values.ToList() );
            Console.WriteLine("Done");
            WriteSubstitionFile(translationFile);
            WriteSubstitionFile(_nestingStringsSubstitute);
            WriteSubstitionFile(_colorCodeSubstitute);
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
            }
        }

        private void SubstituteNestingString(LineObject lineObject)
        {
            List<string> token = new List<string>();
            token = parserNestingStrings.GetToken(lineObject.OriginalLine, token);

            string substitute = lineObject.OriginalLine;
            foreach (string subs in token)
            {
                substitute = substitute.Replace(NESTING_STRING_SIGN_START + subs + NESTING_STRING_SIGN_END, GenerateSubNestingString(NESTING_STRING_SIGN_START +subs +NESTING_STRING_SIGN_END));
            }

            lineObject.OriginalLineSubstituted = substitute;
        }
        private string GenerateSubNestingString(string sub)
        {
            int count = _nestingStringsSubstitute.Count();
            count++;
            string subString = SUBSTITUTION_START + NESTING_STRING_SUFFIX + count.ToString() + SUBSTITUTION_END;
            _nestingStringsSubstitute.Add(subString, sub);
            return subString;
        }

        private void SubstituteColorCode(LineObject lineObject)
        {
            List<string> token = new List<string>();
            token = parserColorCodes.GetToken(lineObject.OriginalLine, token);

            string substitute = lineObject.OriginalLine;
            foreach (string subs in token)
            {
                substitute = substitute.Replace(COLOR_CODE_SIGN_START + subs + COLOR_CODE_SIGN_END, GenerateSubColorCode(COLOR_CODE_SIGN_START + subs + COLOR_CODE_SIGN_END));
            }

            lineObject.OriginalLineSubstituted = substitute;
        }

        private string GenerateSubColorCode(string sub)
        {
            int count = _colorCodeSubstitute.Count();
            count++;
            string subString = SUBSTITUTION_START + COLOR_CODE_SUFFIX + count.ToString() + SUBSTITUTION_END;
            _colorCodeSubstitute.Add(subString, sub);
            return subString;
        }

        private void WriteSubstitionFile(TranslationFile translationFile)
        {
            if (translationFile == null)
            {
                return;
            }

            string fileName = Path.GetFileNameWithoutExtension(translationFile.FileName);
            string substitionFileSuffix = ".sub.yml";
            
            Dictionary<ulong, LineObject> _lines = translationFile.Lines;
            List<LineObject> lineObjects = _lines.Values.ToList();

            Console.WriteLine("Writing substituted source file started ...");
            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(translationFile.FileName + substitionFileSuffix))
            {
                foreach (LineObject line in lineObjects )
                {
                    outputFile.WriteLine(GetSubstitutedLine(line));
                }
            }
            Console.WriteLine("Writing substituted source file finished ...");
        }

        private string GetSubstitutedLine(LineObject lineObject)
        {
            if( lineObject.OriginalLineSubstituted.Length > 0 )
            {
                return lineObject.OriginalLineSubstituted;
            }
            return lineObject.OriginalLine;
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
