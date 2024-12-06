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
        public Dictionary<string,string> ParseDirectory(string directory)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (false == Directory.Exists(directory))
            {
                return result;
            }
            string[] files = Directory.GetFiles(directory, _filePattern, SearchOption.AllDirectories);

            foreach (var file in files)
            {
                try
                {
                    result.Add(Path.GetFileNameWithoutExtension(file).Replace("\\", "/"), file);
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine("File already registered: " +file.ToString());
                }
            }

            return result;
        }
    }
}

