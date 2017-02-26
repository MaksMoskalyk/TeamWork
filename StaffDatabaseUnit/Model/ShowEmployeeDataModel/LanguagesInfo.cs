using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamworkDB;

namespace StaffDatabaseUnit
{
    public class LanguagesInfo
    {
        private Language language;
        private LanguageProficiency proficiency;

        public LanguagesInfo()
        {
            language = new Language();
            proficiency = new LanguageProficiency();
        }

        public LanguagesInfo(Language language, LanguageProficiency proficiency)
        {
            this.language = language;
            this.proficiency = proficiency;
        }

        public Language Language
        {
            get { return language; }
            set { language = value; }
        }

        public LanguageProficiency Proficiency
        {
            get { return proficiency; }
            set { proficiency = value; }
        }
    }
}
