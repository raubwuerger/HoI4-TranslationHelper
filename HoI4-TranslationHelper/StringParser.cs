using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public class StringParser
    {
        private string _startTag;
        public string StartTag { get => _startTag; set => _startTag = value; }

        private int _subStringCount = 0;
        public int SubStringCount { get => _subStringCount; set => _subStringCount = value; }

        private List<string> _endTags = new List<string>() {};
        public List<string> EndTags { get => _endTags; set => _endTags = value; }

        public List<string> GetToken(string source, List<string> tokens)
        {
            if (false == source.Contains(_startTag))
            {
                return tokens;
            }
            int _startPos = source.IndexOf(_startTag, 0) + _startTag.Length;

            foreach (string endTag in _endTags )
            {
                string subString = source.Substring(_startPos, source.Length - _startPos);

                if (false == subString.Contains(endTag))
                {
                    continue;
                }
                int _endPos = source.IndexOf(endTag, _startPos);

                if(_subStringCount == 0 )
                {
                    tokens.Add(source.Substring(_startPos, _endPos - _startPos));
                }
                else 
                {
                    tokens.Add(source.Substring(_startPos, _subStringCount));
                }

                string remainingContent = source.Substring(_endPos + 1, source.Length - _endPos - 1);

                return GetToken(remainingContent, tokens);
            }

            return tokens;
        }
    }
}
