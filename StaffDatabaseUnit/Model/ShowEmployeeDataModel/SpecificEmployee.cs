using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TeamworkDB;
using System.IO;
using VM_Base;
namespace StaffDatabaseUnit
{
    public class SpecificEmployee : ViewModelBase
    {
        #region Fields
        // Main data
        private Employee employee;
        private string position;
        private string dateOfBirth;
        private string citizenship;
        private string gender;
        private string countryOfLiving;
        private string cityOfLiving;

        // Skills info
        private ObservableCollection<Position> positionFilters;
        private Position selectedPositionFilter;
        private ObservableCollection<SkillsGroup> groupFilters;
        private SkillsGroup selectedGroupFilter;
        private ObservableCollection<SkillsInfo> skillTableUnits;
        private List<SkillTableUnit> skillsTotalInfo;

        // Table data
        private ObservableCollection<string> phones;
        private ObservableCollection<string> mails;
        private ObservableCollection<WebServiceTableUnit> webAccounts;
        private ObservableCollection<ExperienceTableUnit> experienceTableUnits;
        private ObservableCollection<EducationTableUnit> educationTableUnits;
        private ObservableCollection<LanguagesInfo> languageTableUnits;
        #endregion

        #region Constructor
        public SpecificEmployee()
        {
            // Main data
            employee = new Employee();
            position = "";
            dateOfBirth = "";
            citizenship = "";
            gender = "";
            countryOfLiving = "";
            cityOfLiving = "";

            // Skills info
            positionFilters = new ObservableCollection<Position>();
            selectedPositionFilter = new Position();
            groupFilters = new ObservableCollection<SkillsGroup>();
            selectedGroupFilter = new SkillsGroup();
            skillTableUnits = new ObservableCollection<SkillsInfo>();
            skillsTotalInfo = new List<SkillTableUnit>();

            // Table data
            phones = new ObservableCollection<string>();
            mails = new ObservableCollection<string>();
            webAccounts = new ObservableCollection<WebServiceTableUnit>();
            experienceTableUnits = new ObservableCollection<ExperienceTableUnit>();
            educationTableUnits = new ObservableCollection<EducationTableUnit>();
            languageTableUnits = new ObservableCollection<LanguagesInfo>();
        }
        #endregion

        #region Properties
        public Employee Employee
        {
            get
            {
                return employee;
            }

            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        public string FullName
        {
            get
            {
                return employee.Name + " " + employee.Surname;
            }
        }

        public string Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }

        public string DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }

            set
            {
                dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        public string Citizenship
        {
            get
            {
                return citizenship;
            }

            set
            {
                citizenship = value;
                OnPropertyChanged("Citizenship");
            }
        }

        public string Gender
        {
            get
            {
                return gender;
            }

            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        public string CountryOfLiving
        {
            get
            {
                return countryOfLiving;
            }

            set
            {
                countryOfLiving = value;
                OnPropertyChanged("CountryOfLiving");
            }
        }

        public string CityOfLiving
        {
            get
            {
                return cityOfLiving;
            }

            set
            {
                cityOfLiving = value;
                OnPropertyChanged("CityOfLiving");
            }
        }

        public ObservableCollection<Position> PositionFilters
        {
            get
            {
                return positionFilters;
            }

            set
            {
                positionFilters = value;
                OnPropertyChanged("PositionFilters");
            }
        }

        public Position SelectedPositionFilter
        {
            get
            {
                return selectedPositionFilter;
            }

            set
            {
                selectedPositionFilter = value;
                OnPropertyChanged("SelectedPositionFilter");
            }
        }

        public ObservableCollection<SkillsGroup> GroupFilters
        {
            get
            {
                return groupFilters;
            }

            set
            {
                groupFilters = value;
                OnPropertyChanged("GroupFilters");
            }
        }

        public SkillsGroup SelectedGroupFilter
        {
            get
            {
                return selectedGroupFilter;
            }

            set
            {
                selectedGroupFilter = value;
                OnPropertyChanged("SelectedGroupFilter");
            }
        }

        public ObservableCollection<SkillsInfo> SkillTableUnits
        {
            get
            {
                return skillTableUnits;
            }

            set
            {
                skillTableUnits = value;
                OnPropertyChanged("SkillTableUnits");
            }
        }

        public List<SkillTableUnit> SkillsTotalInfo
        {
            get
            {
                return skillsTotalInfo;
            }

            set
            {
                skillsTotalInfo = value;
                OnPropertyChanged("SkillsTotalInfo");
            }
        }

        public ObservableCollection<string> Phones
        {
            get
            {
                return phones;
            }

            set
            {
                phones = value;
                OnPropertyChanged("Phones");
            }
        }

        public ObservableCollection<string> Mails
        {
            get
            {
                return mails;
            }

            set
            {
                mails = value;
                OnPropertyChanged("Mails");
            }
        }

        public ObservableCollection<WebServiceTableUnit> WebAccounts
        {
            get
            {
                return webAccounts;
            }

            set
            {
                webAccounts = value;
                OnPropertyChanged("WebAccounts");
            }
        }

        public ObservableCollection<ExperienceTableUnit> ExperienceTableUnits
        {
            get
            {
                return experienceTableUnits;
            }

            set
            {
                experienceTableUnits = value;
                OnPropertyChanged("ExperienceTableUnits");
            }
        }

        public ObservableCollection<EducationTableUnit> EducationTableUnits
        {
            get
            {
                return educationTableUnits;
            }

            set
            {
                educationTableUnits = value;
                OnPropertyChanged("EducationTableUnits");
            }
        }

        public ObservableCollection<LanguagesInfo> LanguageTableUnits
        {
            get
            {
                return languageTableUnits;
            }

            set
            {
                languageTableUnits = value;
                OnPropertyChanged("LanguageTableUnits");
            }
        }
        #endregion
    }
}
