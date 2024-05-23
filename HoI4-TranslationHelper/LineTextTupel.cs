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

        public override bool Equals(object obj) => this.Equals(obj as LineTextTupel);

        public bool Equals(LineTextTupel lineTextTupel)
        {
            if(lineTextTupel is null )
            {
                return false;
            }

            if (Object.ReferenceEquals(this, lineTextTupel))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != lineTextTupel.GetType())
            {
                return false;
            }

            return _text.Equals(this._text);
        }
    }
}
