using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public class DirectoryParserCompare
    {
        private string _filePattern = "*.yml";
        public string FilePattern { get => _filePattern; set => _filePattern = value; }

        private FileReader _fileReader = new FileReader();
        public FileReader FileReader { get => _fileReader; set => _fileReader = value; }
        public List<string> ParseDirectory(string directory)
        {
            if (false == Directory.Exists(directory))
            {
                return null;
            }
            string[] files = Directory.GetFiles(directory, _filePattern, SearchOption.AllDirectories);

            List<string> result = new List<string>();
            result.AddRange(files);

            return result;
        }
    }
}

