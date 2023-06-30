using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public class LineTextTupel
    {
        public int _lineNumber;
        public string _text;

        public LineTextTupel( int lineNumber, string text )
        {
            _lineNumber = lineNumber;
            _text = text;
        }
    }
}
