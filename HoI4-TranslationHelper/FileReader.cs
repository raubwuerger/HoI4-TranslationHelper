using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public class FileReader
    {
        private string openTag = "[";
        private string closeTag = "]";

        public FileWithToken Read(string textFile)
        {
            string[] lines = File.ReadAllLines(textFile);
            if( false == lines.Any() )
            {
                return null;
            }

            List<LineTextTupel> parsedFile = ParseStrings(lines);
            if(false == parsedFile.Any())
            {
                return null;
            }

            return new FileWithToken(textFile, parsedFile);
        }

        private List<LineTextTupel> ParseStrings(string[] lines )
        {
            List<LineTextTupel> lLineTextTupels = new List<LineTextTupel>();
            int lineNumber = 0;
            foreach( string line in lines )
            {
                lineNumber++;
                List<string> tokens = new List<string>();
                tokens = StringParser.GetToken(line, tokens);
                if( null == tokens )
                {
                    continue;
                }

                foreach( string token in tokens )
                {
                    lLineTextTupels.Add(new LineTextTupel(lineNumber, token));
                }
            }

            return lLineTextTupels;
        }
    }
}
