using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamworkDB;
using TeamworkDBEntity;
using VM_Base;
namespace StaffDatabaseUnit
{
    public class SkillElement : ViewModelBase
    {
        private Skill skill;
        private bool isChecked;

        public SkillElement() { }

        public Skill Skill
        {
            get { return skill; }
            set { skill = value; }
        }

        public bool IsChecked
        {
            get { return isChecked; }
            set 
            { 
                isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
    }
}
