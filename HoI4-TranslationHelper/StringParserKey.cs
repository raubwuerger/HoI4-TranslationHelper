using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public class StringParserKey : StringParserBase
    {
        public override List<string> GetToken(string source, List<string> tokens)
        {
            if (false == source.Contains(StartTag))
            {
                return tokens;
            }

            if( true == IgnoreLine(source) )
            {
                return tokens;
            }

            int _startPos = source.IndexOf(StartTag, 0) + StartTag.Length;

            int _endPos = _startPos;
            _startPos = 0;
            if (SubStringCount == 0)
            {
                int count = _endPos - 1;
                if (count > 0)
                {
                    count--;
                }
                tokens.Add(source.Substring(_startPos, count));
            }
            else
            {
                tokens.Add(source.Substring(_startPos, SubStringCount));
            }

            return tokens;
        }

    }
}
