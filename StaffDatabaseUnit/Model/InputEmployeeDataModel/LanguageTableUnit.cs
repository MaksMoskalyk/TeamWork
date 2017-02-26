using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamworkDB;
using TeamworkDBEntity;

namespace StaffDatabaseUnit
{
    public class LanguageTableUnit
    {
        private bool isChecked;
        private Language language;
        private List<LanguageProficiency> proficiencyList;
        private LanguageProficiency selectedProficiency;

        public LanguageTableUnit()
        {
            isChecked = false;
            language = new Language();
            proficiencyList = new List<LanguageProficiency>();
        }

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        public Language Language
        {
            get { return language; }
            set { language = value; }
        }

        public List<LanguageProficiency> ProficiencyList
        {
            get { return proficiencyList; }
            set { proficiencyList = value; }
        }

        public LanguageProficiency SelectedProficiency
        {
            get { return selectedProficiency; }
            set { selectedProficiency = value; }
        }
    }
}
