using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    internal class LineObject
    {
        ulong _lineNumber;
        TranslationFile _translationFile;
        string _key;
        List<string> _brackets = new List<string>();
        List<string> _nestingStrings = new List<string>();

        public LineObject(ulong lineNumber)
        {
            _lineNumber = lineNumber;
        }

        private LineObject() { }

        public ulong LineNumber { get => _lineNumber; }
        public string Key { get => _key; set => _key = value; }
        internal TranslationFile TranslationFile { get => _translationFile; set => _translationFile = value; }
        public List<string> Brackets { get => _brackets; set => _brackets = value; }
        public List<string> NestingStrings { get => _nestingStrings; set => _nestingStrings = value; }
    }
}
