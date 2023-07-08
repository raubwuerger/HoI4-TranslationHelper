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

        abstract public List<string> GetToken(string source, List<string> tokens);
    }
}
