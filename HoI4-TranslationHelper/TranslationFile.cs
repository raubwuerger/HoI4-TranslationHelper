using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    internal class TranslationFile
    {
        private string _fileName;
        private string _fileNameWithoutLocalisation;
        private Dictionary<long, LineObject> _lines = new Dictionary<long, LineObject>(); //long is line number
        public TranslationFile(string filename)
        {
            _fileName = filename;
        }

        protected TranslationFile() 
        {
        }

        public Dictionary<long, LineObject> Lines { get => _lines; set => _lines = value; }
        public string FileName { get => _fileName; }
        public string FileNameWithoutLocalisation { get => _fileNameWithoutLocalisation; set => _fileNameWithoutLocalisation = value; }

        public override bool Equals(object obj)
        {
            return obj is TranslationFile file &&
                   _fileName == file._fileName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_fileName);
        }
    }
}
