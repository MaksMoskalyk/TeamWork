using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TeamworkDB;
using TeamworkDBEntity;
using System.IO;
using System.Drawing;
using VM_Base;
using DelegateCommandNS;
namespace StaffDatabaseUnit
{
    public class EmployeeData : ViewModelBase
    {
        #region Fields
        // Main data.
        protected Employee employee;
        protected ObservableCollection<Position> positions;
        protected Position selectedPosition;

        // Contact info.
        protected string inputPhone;
        protected ObservableCollection<string> phones;
        protected string selectedPhone;
        protected string inputMail;
        protected ObservableCollection<string> mails;
        protected string selectedMail;
        protected string inputWebAccount;
        protected ObservableCollection<WebService> webServices;
        protected WebService selectedWebService;
        protected ObservableCollection<WebServiceTableUnit> webAccounts;
        protected WebServiceTableUnit selectedWebAccount;

        // Skills info.
        protected ObservableCollection<Position> positionFilters;
        protected Position selectedPositionFilter;
        protected ObservableCollection<SkillsGroup> groupFilters;
        protected SkillsGroup selectedGroupFilter;
        protected ObservableCollection<SkillTableUnit> skillTableUnits;
        protected List<SkillTableUnit> skillsTotalInfo;

        // Experience info.
        protected Position companySelectedPosition;
        protected ObservableCollection<Company> companies;
        protected Company selectedCompany;        
        protected ObservableCollection<string> months;
        protected string selectedMonthOfHiring;
        protected string selectedMonthOfDismissal;
        protected ObservableCollection<string> years;
        protected string selectedYearOfHiring;
        protected string selectedYearOfDismissal;
        protected ObservableCollection<ExperienceTableUnit> experienceTableUnits;
        protected ExperienceTableUnit selectedExperienceTableUnit;

        // Education info.
        protected ObservableCollection<EducationFacility> educationFacilities;
        protected EducationFacility selectedEducationFacility;
        protected ObservableCollection<EducationSpecialty> educationSpecialties;
        protected EducationSpecialty selectedEducationSpecialty;
        protected ObservableCollection<EducationEntitlingDocument> educationEntitlingDocuments;
        protected EducationEntitlingDocument selectedEducationEntitlingDocument;
        protected string selectedMonthOfGraduation;
        protected string selectedYearOfGraduation;
        protected ObservableCollection<EducationTableUnit> educationTableUnits;
        protected EducationTableUnit selectedEducationTableUnit;

        // General info.
        protected ObservableCollection<LanguageTableUnit> languageTableUnits;
        protected ObservableCollection<string> days;
        protected string selectedDayOfBirth;
        protected string selectedMonthOfBirth;
        protected string selectedYearOfBirth;
        protected ObservableCollection<string> genders;
        protected string selectedGender;
        protected ObservableCollection<Citizenship> citizenships;
        protected Citizenship selectedCitizenship;
        protected ObservableCollection<Country> countries;
        protected Country selectedCountry;
        protected ObservableCollection<City> cities;              
        protected City selectedCity;
        #endregion

        #region Constructor
        public EmployeeData()
        {
            // Main data.
            employee = new Employee();
            positions = new ObservableCollection<Position>();
            selectedPosition = new Position();

            // Contact info.
            inputPhone = "";
            phones = new ObservableCollection<string>();
            selectedPhone = "";
            inputMail = "";
            mails = new ObservableCollection<string>();
            selectedMail = "";
            inputWebAccount = "";
            webServices = new ObservableCollection<WebService>();
            selectedWebService = new WebService();
            webAccounts = new ObservableCollection<WebServiceTableUnit>();
            selectedWebAccount = new WebServiceTableUnit();

            // Skills info.
            positionFilters = new ObservableCollection<Position>();
            selectedPositionFilter = new Position();
            groupFilters = new ObservableCollection<SkillsGroup>();
            selectedGroupFilter = new SkillsGroup();
            skillTableUnits = new ObservableCollection<SkillTableUnit>();
            skillsTotalInfo = new List<SkillTableUnit>();

            // Experience info.
            companySelectedPosition = new Position();
            companies = new ObservableCollection<Company>();
            selectedCompany = new Company();
            months = new ObservableCollection<string>();
            selectedMonthOfHiring = "";
            selectedMonthOfDismissal = "";
            years = new ObservableCollection<string>();
            selectedYearOfHiring = "";
            selectedYearOfDismissal = "";
            experienceTableUnits = new ObservableCollection<ExperienceTableUnit>();
            selectedExperienceTableUnit = new ExperienceTableUnit();

            // Education info.
            educationFacilities = new ObservableCollection<EducationFacility>();
            selectedEducationFacility = new EducationFacility();
            educationSpecialties = new ObservableCollection<EducationSpecialty>();
            selectedEducationSpecialty = new EducationSpecialty();
            educationEntitlingDocuments = new ObservableCollection<EducationEntitlingDocument>();
            selectedEducationEntitlingDocument = new EducationEntitlingDocument();
            selectedMonthOfGraduation = "";
            selectedYearOfGraduation = "";
            educationTableUnits = new ObservableCollection<EducationTableUnit>();
            selectedEducationTableUnit = new EducationTableUnit();

            // General info.
            languageTableUnits = new ObservableCollection<LanguageTableUnit>();
            days = new ObservableCollection<string>();
            selectedDayOfBirth = "";
            selectedMonthOfBirth = "";
            selectedYearOfBirth = "";
            genders = new ObservableCollection<string>();
            genders.Add("Male");
            genders.Add("Female");
            selectedGender = genders[0];
            citizenships = new ObservableCollection<Citizenship>();
            selectedCitizenship = new Citizenship();
            countries = new ObservableCollection<Country>();
            selectedCountry = new Country();
            cities = new ObservableCollection<City>();
            selectedCity = new City();


            //TEST !!!
            //employee.Photo = new Bitmap(Path.GetFullPath("../../../Photo.png"));

            //employee.Name = "Stepan";
            //employee.Surname = "Sidorchuk";
            //employee.GeneralDescription = "Nothing to say.";
            //employee.ProfessionalDescription = "Nothing to say.";            
            //InputPhone = "0972859805";
            //InputMail = "stepan_sidorchuk@ukr.net";
            //InputWebAccount = "stepan_sidorchuk";
           
            //SkillTableUnit skillEntry = new SkillTableUnit();
            //skillEntry.Skill.Name = "C++";
            //SkillProficiency proficiency1 = new SkillProficiency("base");
            //SkillProficiency proficiency2 = new SkillProficiency("advanced");
            //skillEntry.ProficiencyList.Add(proficiency1);
            //skillEntry.ProficiencyList.Add(proficiency2);
            //skillEntry.SelectedProficiency = skillEntry.ProficiencyList[0];
            //skillEntry.IsChecked = true;
            //SkillTableUnits.Add(skillEntry);

            //ExperienceTableUnit experienceEntry = new ExperienceTableUnit();
            //experienceEntry.Company.Name = "Intersog";
            //experienceEntry.Position.Name = "Developer";
            //experienceEntry.DateOfHiring = "04-11-2014";
            //experienceEntry.DateOfDismissal = "03-10-2015";
            //ExperienceTableUnits.Add(experienceEntry);

            //EducationTableUnit educationEntry = new EducationTableUnit();
            //educationEntry.EducationFacility.Name = "IT Step";
            //educationEntry.Specialty.Name = "Programming";
            //educationEntry.EntitlingDocument.Name = "Certificate";
            //educationEntry.DateOfGraduation = "23-04-2016";
            //EducationTableUnits.Add(educationEntry);

            //LanguageTableUnit languageEntry = new LanguageTableUnit();
            //languageEntry.IsChecked = true;
            //languageEntry.Language.Name = "English";
            //languageEntry.ProficiencyList.Add(new LanguageProficiency("elementary"));
            //languageEntry.ProficiencyList.Add(new LanguageProficiency("intermediate"));
            //languageEntry.ProficiencyList.Add(new LanguageProficiency("advanced"));
            //languageEntry.SelectedProficiency = languageEntry.ProficiencyList[0];
            //LanguageTableUnits.Add(languageEntry);

            //Phones.Add("0972859805");
            //Mails.Add("stepan_sidorchuk@ukr.net");
            //WebServiceTableUnit webServiceEntry = new WebServiceTableUnit();
            //webServiceEntry.Service = new WebService("Scype");
            //webServiceEntry.Account = "stepan_sidorchuk";
            //WebAccounts.Add(webServiceEntry);
        }
        #endregion

        #region Properties

        #region Main data properties
        public Employee Employee
        {
            get { return employee; }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        public string EmployeeName
        {
            get { return employee.Name; }
            set
            {
                employee.Name = value;
                OnPropertyChanged("EmployeeName");
            }
        }

        public string EmployeeSurname
        {
            get { return employee.Surname; }
            set
            {
                employee.Surname = value;
                OnPropertyChanged("EmployeeSurname");
            }
        }

        public byte[] EmployeePhoto
        {
            get { return employee.Photo; }
            set
            {
                employee.Photo = value;
                OnPropertyChanged("EmployeePhoto");
            }
        }

        public string EmployeeGeneralDescription
        {
            get { return employee.GeneralDescription; }
            set
            {
                employee.GeneralDescription = value;
                OnPropertyChanged("EmployeeGeneralDescription");
            }
        }

        public string EmployeeProfessionallDescription
        {
            get { return employee.ProfessionalDescription; }
            set
            {
                employee.ProfessionalDescription = value;
                OnPropertyChanged("EmployeeProfessionallDescription");
            }
        }

        //public string EmployeePhotoURL
        //{
        //    get { return employee.PhotoURL; }
        //    set
        //    {
        //        employee.PhotoURL = value;
        //        OnPropertyChanged("EmployeePhotoURL");
        //    }
        //}

        public ObservableCollection<Position> Positions
        {
            get { return positions; }
            set
            {
                positions = value;
                OnPropertyChanged("Positions");
            }
        }

        public Position SelectedPosition
        {
            get { return selectedPosition; }
            set
            {
                selectedPosition = value;
                OnPropertyChanged("SelectedPosition");
            }
        }
        #endregion

        #region Contact info properties
        public string InputPhone
        {
            get { return inputPhone; }
            set
            {
                inputPhone = value;
                OnPropertyChanged("InputPhone");
            }
        }

        public ObservableCollection<string> Phones
        {
            get { return phones; }
            set
            {
                phones = value;
                OnPropertyChanged("Phones");
            }
        }

        public string SelectedPhone
        {
            get { return selectedPhone; }
            set
            {
                selectedPhone = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        public string InputMail
        {
            get { return inputMail; }
            set
            {
                inputMail = value;
                OnPropertyChanged("InputMail");
            }
        }

        public ObservableCollection<string> Mails
        {
            get { return mails; }
            set
            {
                mails = value;
                OnPropertyChanged("Mails");
            }
        }

        public string SelectedMail
        {
            get { return selectedMail; }
            set
            {
                selectedMail = value;
                OnPropertyChanged("SelectedMail");
            }
        }

        public string InputWebAccount
        {
            get { return inputWebAccount; }
            set
            {
                inputWebAccount = value;
                OnPropertyChanged("InputWebAccount");
            }
        }

        public ObservableCollection<WebService> WebServices
        {
            get { return webServices; }
            set
            {
                webServices = value;
                OnPropertyChanged("WebServices");
            }
        }

        public WebService SelectedWebService
        {
            get { return selectedWebService; }
            set
            {
                selectedWebService = value;
                OnPropertyChanged("SelectedWebService");
            }
        }

        public ObservableCollection<WebServiceTableUnit> WebAccounts
        {
            get { return webAccounts; }
            set
            {
                webAccounts = value;
                OnPropertyChanged("WebAccounts");
            }
        }

        public WebServiceTableUnit SelectedWebAccount
        {
            get { return selectedWebAccount; }
            set
            {
                selectedWebAccount = value;
                OnPropertyChanged("SelectedWebAccount");
            }
        }
        #endregion

        #region Skills info properties
        public ObservableCollection<Position> PositionFilters
        {
            get { return positionFilters; }
            set
            {
                positionFilters = value;
                OnPropertyChanged("PositionFilters");
            }
        }

        public Position SelectedPositionFilter
        {
            get { return selectedPositionFilter; }
            set
            {
                selectedPositionFilter = value;
                OnPropertyChanged("SelectedPositionFilter");
            }
        }

        public ObservableCollection<SkillsGroup> GroupFilters
        {
            get { return groupFilters; }
            set
            {
                groupFilters = value;
                OnPropertyChanged("GroupFilters");
            }
        }

        public SkillsGroup SelectedGroupFilter
        {
            get { return selectedGroupFilter; }
            set
            {
                selectedGroupFilter = value;
                OnPropertyChanged("SelectedGroupFilter");
            }
        }
        public ObservableCollection<SkillTableUnit> SkillTableUnits
        {
            get { return skillTableUnits; }
            set
            {
                skillTableUnits = value;
                OnPropertyChanged("SkillTableUnits");
            }
        }

        public List<SkillTableUnit> SkillsTotalInfo
        {
            get { return skillsTotalInfo; }
            set
            {
                skillsTotalInfo = value;
            }
        }

        #endregion

        #region Experience info properties
        public Position CompanySelectedPosition
        {
            get { return companySelectedPosition; }
            set
            {
                companySelectedPosition = value;
                OnPropertyChanged("CompanySelectedPosition");
            }
        }

        public ObservableCollection<Company> Companies
        {
            get { return companies; }
            set
            {
                companies = value;
                OnPropertyChanged("Companies");
            }
        }

        public Company SelectedCompany
        {
            get { return selectedCompany; }
            set
            {
                selectedCompany = value;
                OnPropertyChanged("SelectedCompany");
            }
        }

        public ObservableCollection<string> Months
        {
            get { return months; }
            set
            {
                months = value;
                OnPropertyChanged("Months");
            }
        }

        public string SelectedMonthOfHiring
        {
            get { return selectedMonthOfHiring; }
            set
            {
                selectedMonthOfHiring = value;
                OnPropertyChanged("SelectedMonthOfHiring");
            }
        }

        public string SelectedMonthOfDismissal
        {
            get { return selectedMonthOfDismissal; }
            set
            {
                selectedMonthOfDismissal = value;
                OnPropertyChanged("SelectedMonthOfDismissal");
            }
        }

        public ObservableCollection<string> Years
        {
            get { return years; }
            set
            {
                years = value;
                OnPropertyChanged("Years");
            }
        }

        public string SelectedYearOfHiring
        {
            get { return selectedYearOfHiring; }
            set
            {
                selectedYearOfHiring = value;
                OnPropertyChanged("SelectedYearOfHiring");
            }
        }

        public string SelectedYearOfDismissal
        {
            get { return selectedYearOfDismissal; }
            set
            {
                selectedYearOfDismissal = value;
                OnPropertyChanged("SelectedYearOfDismissal");
            }
        }

        public ObservableCollection<ExperienceTableUnit> ExperienceTableUnits
        {
            get { return experienceTableUnits; }
            set
            {
                experienceTableUnits = value;
                OnPropertyChanged("ExperienceTableUnits");
            }
        }

        public ExperienceTableUnit SelectedExperienceTableUnit
        {
            get { return selectedExperienceTableUnit; }
            set
            {
                selectedExperienceTableUnit = value;
                OnPropertyChanged("SelectedExperienceTableUnit");
            }
        }
        #endregion

        #region Education info properties
        public ObservableCollection<EducationFacility> EducationFacilities
        {
            get { return educationFacilities; }
            set
            {
                educationFacilities = value;
                OnPropertyChanged("EducationFacilities");
            }
        }

        public EducationFacility SelectedEducationFacility
        {
            get { return selectedEducationFacility; }
            set
            {
                selectedEducationFacility = value;
                OnPropertyChanged("SelectedEducationFacility");
            }
        }

        public ObservableCollection<EducationSpecialty> EducationSpecialties
        {
            get { return educationSpecialties; }
            set
            {
                educationSpecialties = value;
                OnPropertyChanged("EducationSpecialties");
            }
        }

        public EducationSpecialty SelectedEducationSpecialty
        {
            get { return selectedEducationSpecialty; }
            set
            {
                selectedEducationSpecialty = value;
                OnPropertyChanged("SelectedEducationSpecialty");
            }
        }

        public ObservableCollection<EducationEntitlingDocument> EducationEntitlingDocuments
        {
            get { return educationEntitlingDocuments; }
            set
            {
                educationEntitlingDocuments = value;
                OnPropertyChanged("EducationEntitlingDocuments");
            }
        }

        public EducationEntitlingDocument SelectedEducationEntitlingDocument
        {
            get { return selectedEducationEntitlingDocument; }
            set
            {
                selectedEducationEntitlingDocument = value;
                OnPropertyChanged("SelectedEducationEntitlingDocument");
            }
        }

        public string SelectedMonthOfGraduation
        {
            get { return selectedMonthOfGraduation; }
            set
            {
                selectedMonthOfGraduation = value;
                OnPropertyChanged("SelectedMonthOfGraduation");
            }
        }

        public string SelectedYearOfGraduation
        {
            get { return selectedYearOfGraduation; }
            set
            {
                selectedYearOfGraduation = value;
                OnPropertyChanged("SelectedYearOfGraduation");
            }
        }

        public ObservableCollection<EducationTableUnit> EducationTableUnits
        {
            get { return educationTableUnits; }
            set
            {
                educationTableUnits = value;
                OnPropertyChanged("EducationTableUnits");
            }
        }
        public EducationTableUnit SelectedEducationTableUnit
        {
            get { return selectedEducationTableUnit; }
            set
            {
                selectedEducationTableUnit = value;
                OnPropertyChanged("SelectedEducationTableUnit");
            }
        }
        #endregion

        #region General info properties
        public ObservableCollection<LanguageTableUnit> LanguageTableUnits
        {
            get { return languageTableUnits; }
            set
            {
                languageTableUnits = value;
                OnPropertyChanged("LanguageTableUnits");
            }
        }

        public ObservableCollection<string> Days
        {
            get { return days; }
            set
            {
                days = value;
                OnPropertyChanged("Days");
            }
        }

        public string SelectedDayOfBirth
        {
            get { return selectedDayOfBirth; }
            set
            {
                selectedDayOfBirth = value;
                OnPropertyChanged("SelectedDayOfBirth");
            }
        }

        public string SelectedMonthOfBirth
        {
            get { return selectedMonthOfBirth; }
            set
            {
                selectedMonthOfBirth = value;
                OnPropertyChanged("SelectedMonthOfBirth");
            }
        }

        public string SelectedYearOfBirth
        {
            get { return selectedYearOfBirth; }
            set
            {
                selectedYearOfBirth = value;
                OnPropertyChanged("SelectedYearOfBirth");
            }
        }

        public ObservableCollection<string> Genders
        {
            get { return genders; }
            set
            {
                genders = value;
                OnPropertyChanged("Genders");
            }
        }

        public string SelectedGender
        {
            get { return selectedGender; }
            set
            {
                selectedGender = value;
                OnPropertyChanged("SelectedGender");
            }
        }

        public ObservableCollection<Citizenship> Citizenships
        {
            get { return citizenships; }
            set
            {
                citizenships = value;
                OnPropertyChanged("Citizenships");
            }
        }
        public Citizenship SelectedCitizenship
        {
            get { return selectedCitizenship; }
            set
            {
                selectedCitizenship = value;
                OnPropertyChanged("SelectedCitizenship");
            }
        }
        public ObservableCollection<Country> Countries
        {
            get { return countries; }
            set
            {
                countries = value;
                OnPropertyChanged("Countries");
            }
        }

        public Country SelectedCountry
        {
            get { return selectedCountry; }
            set
            {
                selectedCountry = value;
                OnPropertyChanged("SelectedCountry");
            }
        }

        public ObservableCollection<City> Cities
        {
            get { return cities; }
            set
            {
                cities = value;
                OnPropertyChanged("Cities");
            }
        }

        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
            }
        }
        #endregion

        #endregion
    }
}
