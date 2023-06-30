using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public class StringParser
    {
        public static List<string> GetToken(string source, string startTag, string endTag)
        {
            List<string> tokens = new List<string>();
            if (false == source.Contains(startTag) )
            {
                return tokens;
            }

            if( false == source.Contains(endTag))
            {
                return tokens;
            }

            int startPos = source.IndexOf(startTag, 0) + startTag.Length;
            int endPos = source.IndexOf(endTag, startPos);
            tokens.Add(source.Substring(startPos, endPos - startPos));

            return tokens;
        }
    }
}
