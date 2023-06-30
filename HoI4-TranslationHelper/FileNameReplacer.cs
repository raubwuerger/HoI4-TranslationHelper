using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public class FileNameReplacer
    {
        public string tokenToFind = "";
        public string tokenReplaceWith = "";
        public string extendPath = "";
        public string fileTypeExtension = ".txt";
        public FileWithToken Replace(FileWithToken fileWithToken)
        {
            if( null == fileWithToken )
            {
                return fileWithToken;
            }

            string fileName = fileWithToken.FileName;
            if( null == fileName )
            {
                return fileWithToken;
            }

            string fileNameReplace = fileName.Replace(tokenToFind, tokenReplaceWith);
            fileNameReplace += fileTypeExtension;

            fileWithToken.FileName = fileNameReplace;

            return fileWithToken;
        }
    }
}
