using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoI4_TranslationHelper
{
    internal class FileSubstitutor
    {
        public void ReSubstitute(TranslationFileSetSubstitution translationFileSetSubstitution)
        {
            if (translationFileSetSubstitution == null)
            {
                return;
            }
            ReSubstitutor reSubstitutor = new ReSubstitutor();
            reSubstitutor.TranslationFileSetSubstitution = translationFileSetSubstitution;
            reSubstitutor.ReSubstitute();
        }

        public void Substitute(TranslationFile translationFile)
        {
            if (translationFile == null)
            {
                return;
            }

            Substitutor substitutor = new Substitutor();
            substitutor.Substitute(translationFile);
        }
    }
}
