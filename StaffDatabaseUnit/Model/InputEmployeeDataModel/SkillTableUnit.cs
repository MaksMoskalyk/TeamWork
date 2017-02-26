using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamworkDB;
using TeamworkDBEntity;
using System.Collections.ObjectModel;

namespace StaffDatabaseUnit
{
    public class SkillTableUnit
    {
        private bool isChecked;
        private Skill skill;
        private ObservableCollection<SkillProficiency> proficiencyList;
        private SkillProficiency selectedProficiency;

        public SkillTableUnit()
        {
            isChecked = false;
            skill = new Skill();
            proficiencyList = new ObservableCollection<SkillProficiency>();
        }

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        public Skill Skill
        {
            get { return skill; }
            set { skill = value; }
        }

        public ObservableCollection<SkillProficiency> ProficiencyList
        {
            get { return proficiencyList; }
            set { proficiencyList = value; }
        }

        public SkillProficiency SelectedProficiency
        {
            get { return selectedProficiency; }
            set { selectedProficiency = value; }
        }
    }
}
