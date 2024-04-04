using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    public class HoI4_TranslationHelper_Config
    {
        private static string pathEnglish = Constants.pathEnglish;
        private static string pathGerman = Constants.pathGerman;

        public static string PathEnglish { get => pathEnglish; set => pathEnglish = value; }
        public static string PathGerman { get => pathGerman; set => pathGerman = value; }
    }
}
