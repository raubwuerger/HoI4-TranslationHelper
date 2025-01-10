using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    internal class LineObject
    {
        long _lineNumber;
        string _key;
        List<string> _brackets = new List<string>();

        public LineObject(long lineNumber)
        {
            _lineNumber = lineNumber;
        }

        private LineObject() { }

        public long LineNumber { get => _lineNumber; }
        public string Key { get => _key; set => _key = value; }
        public List<string> Brackets { get => _brackets; set => _brackets = value; }
    }
}
