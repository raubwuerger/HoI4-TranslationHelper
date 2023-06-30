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
        public static List<FileWithToken> ParseDirectory(string directory)
        {
            string[] files = Directory.GetFiles(directory, "*.yml", SearchOption.AllDirectories);

            List<FileWithToken> filesWithTokens = new List<FileWithToken>();

            foreach(string file in files)
            {
                FileReader fileReader = new FileReader();
                FileWithToken fileWithToken = fileReader.Read(file);
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
