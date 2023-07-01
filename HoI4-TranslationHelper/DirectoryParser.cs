using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public class DirectoryParser
    {
        private string _filePattern = "*.yml";
        public string FilePattern { get => _filePattern; set => _filePattern = value; }

        private FileReader _fileReader = new FileReader();
        public FileReader FileReader { get => _fileReader; set => _fileReader = value; }
        public List<FileWithToken> ParseDirectory(string directory)
        {
            string[] files = Directory.GetFiles(directory, _filePattern, SearchOption.AllDirectories);

            List<FileWithToken> filesWithTokens = new List<FileWithToken>();

            foreach(string file in files)
            {
                FileWithToken fileWithToken = _fileReader.Read(file);
                if( null == fileWithToken )
                {
                    continue;
                }
                filesWithTokens.Add(fileWithToken);
            }

            return filesWithTokens;
        }
    }
}
