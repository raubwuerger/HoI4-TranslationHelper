using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public class StringParser
    {
        public static string GetToken(string source, string startTag, string endTag)
        {

            if (false == source.Contains(startTag) )
            {
                return null;
            }

            if( false == source.Contains(endTag))
            {
                return null;
            }

            int startPos = source.IndexOf(startTag, 0) + startTag.Length;
            int endPos = source.IndexOf(endTag, startPos);
            return source.Substring(startPos, endPos - startPos);
        }
    }
}
