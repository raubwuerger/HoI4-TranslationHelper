using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

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
        public override string ToString()
        {
            return _fileName;
        }
        public override bool Equals(object obj) => this.Equals(obj as TranslationFile);

        public bool Equals(TranslationFile translationFile)
        {
            if (translationFile is null)
            {
                return false;
            }

            if (System.Object.ReferenceEquals(this, translationFile))
            {
                return true;
            }

            if (this.GetType() != translationFile.GetType())
            {
                return false;
            }

            return this._fileName == translationFile._fileName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_fileName);
        }
    }
}
