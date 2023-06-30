using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public class StringParser
    {
        public static string _startTag = "[";
        public static string _endTag = "]";
        private static int _startPos;
        private static int _endPos;
        public static List<string> GetToken(string source, List<string> tokens)
        {
            if (false == source.Contains(_startTag) )
            {
                return tokens;
            }

            if( false == source.Contains(_endTag))
            {
                return tokens;
            }

            int _startPos = source.IndexOf(_startTag, 0) + _startTag.Length;
            int _endPos = source.IndexOf(_endTag, _startPos);
            tokens.Add(source.Substring(_startPos, _endPos - _startPos));

            string remainingContent = source.Substring(_endPos + 1, source.Length - _endPos - 1);

            return GetToken(remainingContent, tokens);
        }
    }
}
