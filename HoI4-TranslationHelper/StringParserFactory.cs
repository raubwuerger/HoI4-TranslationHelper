using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public class StringParserFactory
    {
        private static StringParserFactory instance;
        public static StringParserFactory Instance
        {
            get
            {
                if( null == instance )
                {
                    instance = new StringParserFactory();
                }
                return instance;
            }
        }

        public StringParserFactory() { }

        public IStringParser CreateKey()
        {
            StringParserKey stringParser = new StringParserKey();
            stringParser.StartTag = "\"";
            IgnoreCommentLines(stringParser);
            return stringParser;
        }

        private void IgnoreCommentLines(StringParserBase stringParserBase)
        {
            stringParserBase.LineIgnores.Add("#");
            stringParserBase.LineIgnores.Add(" #");
            stringParserBase.LineIgnores.Add("  #");
        }

    }
}
