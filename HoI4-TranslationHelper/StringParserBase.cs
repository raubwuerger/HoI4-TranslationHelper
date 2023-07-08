using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public abstract class StringParserBase : IStringParser
    {
        private string _startTag;
        public string StartTag { get => _startTag; set => _startTag = value; }

        private int _subStringCount = 0;
        public int SubStringCount { get => _subStringCount; set => _subStringCount = value; }

        private List<string> _endTags = new List<string>() { };
        public List<string> EndTags { get => _endTags; set => _endTags = value; }

        private List<string> _lineIgnores = new List<string>() { };
        public List<string> LineIgnores { get => _lineIgnores; set => _lineIgnores = value; }

        abstract public List<string> GetToken(string source, List<string> tokens);

        protected bool IgnoreLine(string source)
        {
            foreach( string ignore in _lineIgnores )
            {
                if( 0 == source.IndexOf(ignore) )
                {
                    return true;
                }
            }

            return false;
        }
    }
}
