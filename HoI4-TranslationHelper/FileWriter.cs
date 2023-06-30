using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public class FileWriter
    {
        public static bool Write(FileWithToken fileWithToken)
        {
            if( null == fileWithToken )
            {
                return false;
            }

            string fileName = fileWithToken.FileName;
            if( null == fileName )
            {
                return false;
            }

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            try
            {
                using (FileStream fs = File.Create(fileName))
                {
                    foreach (LineTextTupel lineTextTupel in fileWithToken.GetLineTextTupels)
                    {
                        string lineTemp = lineTextTupel._lineNumber.ToString();
                        lineTemp += "\t";
                        lineTemp += lineTextTupel._text;
                        lineTemp += Environment.NewLine;

                        byte[] line = new UTF8Encoding(true).GetBytes(lineTemp);

                        fs.Write(line, 0, line.Length);
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

    }
}
