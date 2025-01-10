using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    internal class TranslationFileCreator
    {
        public TranslationFile Create(string fileName)
        {
            TranslationFile translationFile = new TranslationFile(fileName);
            translationFile.FileNameWithoutLocalisation = CreateFileNameWithoutLocalisation(fileName);

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

        private Dictionary<long,LineObject> CreateLineObjects(string[] lines)
        {
            if (lines == null || lines.Length == 0)
            {  
                return null;
            }   

            Dictionary<long, LineObject> lineObjects = new Dictionary<long, LineObject>();
            List<LineTextTupel> lineTextTupels = new List<LineTextTupel>();

            IStringParser stringParser = StringParserFactory.Instance.CreateParserBrackets();

            int lineNumber = 0;
            foreach (string line in lines)
            {
                lineNumber++;
                LineObject lineObject = new LineObject(lineNumber);
                lineObjects.Add(lineNumber, lineObject);

                List<string> tokens = new List<string>();
                lineObject.Brackets = stringParser.GetToken(line, tokens);
            }

            return lineObjects;
        }
    }
}
