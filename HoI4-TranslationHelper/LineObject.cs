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
        List<string> _colorCodes = new List<string>();
        List<string> _icons = new List<string>();
        string _originalLine;
        string _originalLineSubstituted;
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
        public List<string> ColorCodes { get => _colorCodes; set => _colorCodes = value; }
        public List<string> Icons { get => _icons; set => _icons = value; }
        public string OriginalLine { get => _originalLine; set => _originalLine = value;  }
        public string OriginalLineSubstituted { get => _originalLineSubstituted; set => _originalLineSubstituted = value; }
    }
}
