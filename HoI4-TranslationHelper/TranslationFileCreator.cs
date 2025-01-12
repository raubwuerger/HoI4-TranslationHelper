using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    internal class TranslationFileCreator
    {
        LineObjectCreator lineObjectCreator = new LineObjectCreator();
        public TranslationFile Create(string fileName)
        {
            TranslationFile translationFile = new TranslationFile(fileName);
            translationFile.FileNameWithoutLocalisation = CreateFileNameWithoutLocalisation(fileName);

            lineObjectCreator.TranslationFile = translationFile;
            translationFile.Lines = CreateLineObjects(File.ReadAllLines(fileName));

            return translationFile;
        }

        private string CreateFileNameWithoutLocalisation(string fileName)
        {
            string fileNameOnly = Path.GetFileNameWithoutExtension(fileName);
            int indexOf_LocalisationStartString = fileNameOnly.IndexOf(Constants.LOCALISATION_START_STRING, StringComparison.OrdinalIgnoreCase);
            if (indexOf_LocalisationStartString != -1)
            {
                return fileNameOnly.Substring(0, indexOf_LocalisationStartString);
            }
            else
            {
                return fileNameOnly;
            }
        }

        private Dictionary<ulong,LineObject> CreateLineObjects(string[] lines)
        {
            if (lines == null || lines.Length == 0)
            {  
                return null;
            }   

            Dictionary<ulong, LineObject> lineObjects = new Dictionary<ulong, LineObject>();
            List<LineTextTupel> lineTextTupels = new List<LineTextTupel>();

            ulong lineNumber = 0;
            foreach (string line in lines)
            {
                SetKey(line);
                SetBrackets(line);
                SetNestingStrings(line);
                SetColorCodes(line);

                lineNumber++;
                LineObject lineObject = lineObjectCreator.Create(lineNumber);
                lineObject.OriginalLine = line;
                lineObjects.Add(lineNumber, lineObject);
            }

            return lineObjects;
        }

        private void SetKey(string line)
        {
            IStringParser stringParser = StringParserFactory.Instance.CreateParserKey();
            List<string> token = new List<string>();
            token = stringParser.GetToken(line, token);
            if (token.Count > 0)
            {
                lineObjectCreator.Key = token[0];
            }
        }

        private void SetBrackets(string line)
        {
            IStringParser stringParser = StringParserFactory.Instance.CreateParserNamespace();
            List<string> token = new List<string>();
            lineObjectCreator.Brackets = stringParser.GetToken(line, token);
        }

        private void SetNestingStrings(string line)
        {
            IStringParser stringParser = StringParserFactory.Instance.CreateParserNestingStrings();
            List<string> token = new List<string>();
            lineObjectCreator.NestingStrings = stringParser.GetToken(line, token);
        }

        private void SetColorCodes(string line)
        {
            IStringParser stringParser = StringParserFactory.Instance.CreateParserColorCodes(); 
            List<string> token = new List<string>();
            lineObjectCreator.ColorCodes = stringParser.GetToken(line, token);
        }
    }
}
