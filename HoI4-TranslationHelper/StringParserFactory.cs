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

        private StringParserFactory() { }

        public IStringParser CreateParserBrackets()
        {
            StringParser stringParser = new StringParser();
            stringParser.StartTag = "[";
            stringParser.EndTags.Clear();
            stringParser.EndTags.Add("]");
            IgnoreCommentLines(stringParser);
            return stringParser;
        }

        public StringParser CreateParserIcons()
        {
            StringParser stringParser = new StringParser();
            stringParser.StartTag = "£";
            stringParser.EndTags.Add(" ");
            stringParser.EndTags.Add("\n");
            stringParser.EndTags.Add("\"");
            IgnoreCommentLines(stringParser);
            return stringParser;
        }

        public IStringParser CreateParserVariables()
        {
            StringParser stringParser = new StringParser();
            stringParser.StartTag = "$";
            stringParser.EndTags.Add("$");
            IgnoreCommentLines(stringParser);
            return stringParser;
        }

        public IStringParser CreateParserColors()
        {
            StringParser stringParser = new StringParser();
            stringParser.StartTag = "§";
            stringParser.EndTags.Add("§!");
            IgnoreCommentLines(stringParser);
            stringParser.SubStringCount = 1;
            return stringParser;
        }
        public IStringParser CreateParserInnerDoubleQuotes()
        {
            StringParserFirstLast stringParser = new StringParserFirstLast();
            stringParser.StartTag = "\"";
            IgnoreCommentLines(stringParser);
            stringParser.EndTags.Add("\"");
            return stringParser;
        }
        public IStringParser CreateParserKey()
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
