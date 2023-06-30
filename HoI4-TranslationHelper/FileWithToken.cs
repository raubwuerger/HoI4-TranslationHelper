using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public class FileWithToken
    {
        private string _fileName;
        private List<LineTextTupel> _lineTextTupels = new List<LineTextTupel>();

        public FileWithToken( string fileName, List<LineTextTupel> lineTextTupels)
        {
            _fileName = fileName;
            _lineTextTupels = lineTextTupels;
        }

        public string FileName { get => _fileName; set => _fileName = value; }

        public List<LineTextTupel> GetLineTextTupels { get => _lineTextTupels; set => _lineTextTupels = value; }
    }
}
