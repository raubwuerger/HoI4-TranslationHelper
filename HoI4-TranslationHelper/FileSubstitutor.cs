using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    internal class FileSubstitutor
    {
        private Dictionary<int, string> _nestingStringsSubstitute = new Dictionary<int, string>(); //ulong substitute number, string original text
        IStringParser parserNestingStrings = StringParserFactory.Instance.CreateParserNestingStrings();

        public void Substitute( TranslationFile translationFile )
        {
            if ( translationFile == null ) 
            {
                return;
            }

            Substitute( translationFile.Lines.Values.ToList() );
            Console.WriteLine("Done");
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
            }
        }

        private void SubstituteNestingString(LineObject lineObject)
        {
            List<string> token = new List<string>();
            token = parserNestingStrings.GetToken(lineObject.OriginalLine, token);

            string substitute = lineObject.OriginalLine;
            foreach (string subs in token)
            {
                lineObject.OriginalLineSubstituted = substitute.Replace(subs, GenerateSubNestingString(subs));
            }
        }

        private string GenerateSubNestingString( string sub )
        {
            int count = _nestingStringsSubstitute.Count();
            count++;
            _nestingStringsSubstitute.Add( count, sub );
            return count.ToString();
        }
    }
}
