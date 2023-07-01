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

            string fileNameToSave = fileWithToken.PathNameToSave;
            if( null == fileNameToSave )
            {
                return fileWithToken;
            }

            string fileNameReplace = fileNameToSave.Replace(tokenToFind, tokenReplaceWith);
            fileNameReplace += fileTypeExtension;

            fileWithToken.PathNameToSave = fileNameReplace;

            return fileWithToken;
        }
    }
}
