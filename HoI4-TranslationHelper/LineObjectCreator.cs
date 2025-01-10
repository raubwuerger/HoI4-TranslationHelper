using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    internal class LineObjectCreator
    {
        TranslationFile _translationFile;
        public TranslationFile TranslationFile { get => _translationFile; set => _translationFile = value; }

        string _key;
        public string Key { get => _key; set => _key = value; }

        List<string> _brackets;
        public List<string> Brackets { get => _brackets; set => _brackets = value; }
        public List<string> NestingStrings { get => _nestingStrings; set => _nestingStrings = value; }

        List<string> _nestingStrings;

        public LineObject Create( ulong lineNumber )
        {
            LineObject lineObject = new LineObject( lineNumber );
            lineObject.TranslationFile = _translationFile;
            lineObject.Key = _key;
            lineObject.Brackets = _brackets;
            lineObject.NestingStrings = _nestingStrings;

            CleanUp();

            return lineObject;
        }

        private void CleanUp()
        {
            _key = null;
            _brackets = null;
            _nestingStrings = null;
        }
    }
}
