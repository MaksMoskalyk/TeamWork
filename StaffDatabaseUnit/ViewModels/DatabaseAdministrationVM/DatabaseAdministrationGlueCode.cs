using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TeamworkDB;
using TeamworkDBEntity;
using VM_Base;
namespace StaffDatabaseUnit
{
    public delegate void AdministrationFulfilled(object sender, EventArgs e);
    public class DatabaseAdministrationGlueCode : ViewModelBase
    {
        #region Events
        public event AdministrationFulfilled administrationFulfilledEvent;

        protected void InitiateAdministrationFulfilledEvent()
        {
            if (administrationFulfilledEvent != null)
                administrationFulfilledEvent(this, EventArgs.Empty);
        }
        #endregion


        #region Fields
        // Skills tab.
        protected List<Skill> skills;
        protected ObservableCollection<SkillElement> chosenSkills;

        protected ObservableCollection<Position> positions;
        protected Position currentPosition;

        protected ObservableCollection<SkillsGroup> groups;
        protected SkillsGroup currentGroup;

        protected Skill newSkill;
        protected bool isNotEditableSkill;
        protected Skill editSkill;
        protected SkillElement selectedSkill;

        // General tab.
        protected List<IElement> categoryElements;
        protected ObservableCollection<GeneralElement> generalElementsList;     
  
        protected List<string> mainFilterList;
        protected string currentMainFilter;

        protected string newElement;
        protected bool isSupplementaryFilter;
        protected bool isCities;
        protected bool isGroups;

        protected ObservableCollection<IElement> supplementaryCategoryElements;
        protected IElement currentSupplementaryElement;

        protected bool isNotEditableElement;
        protected string editElementName;
        protected GeneralElement selectedElement;

        // Data.
        protected ElementCreator elementCreator;
        protected IElement chosenElement;
        protected IDatabaseAdministration database;
        protected IView currentWindow;
        protected IMessageBox messageBox;

        public IView CurrentWindow
        {
            get { return currentWindow; }
            set { currentWindow = value; }
        }
        #endregion


        #region Constructor
        public DatabaseAdministrationGlueCode() 
        {
            // Skills tab.
            skills = new List<Skill>();
            chosenSkills = new ObservableCollection<SkillElement>();

            positions = new ObservableCollection<Position>();
            currentPosition = new Position();

            groups = new ObservableCollection<SkillsGroup>();
            currentGroup = new SkillsGroup();

            newSkill = new Skill();
            isNotEditableSkill = true;
            editSkill = new Skill();
            selectedSkill = new SkillElement();

            // General tab.
            categoryElements = new List<IElement>();
            generalElementsList = new ObservableCollection<GeneralElement>();   
         
            mainFilterList = new List<string>();
            FillMainFilterList();
            currentMainFilter = "";

            newElement = "";
            supplementaryCategoryElements = new ObservableCollection<IElement>();

            isNotEditableElement = true;
            editElementName = "";
            selectedElement = new GeneralElement();

            // Data.
            database = new DatabaseAdministrationEntity();           
        }
        #endregion


        #region Skills Tab Properties
        // Skills tab.
        public ObservableCollection<SkillElement> ChosenSkills
        {
            get { return chosenSkills; }
            set { chosenSkills = value; }
        }

        public ObservableCollection<Position> Positions
        {
            get { return positions; }
            set { positions = value; }
        }

        public Position CurrentPosition
        {
            get { return currentPosition; }
            set 
            { 
                currentPosition = value;
                OnPropertyChanged("CurrentPosition");
            }
        }

        public ObservableCollection<SkillsGroup> Groups
        {
            get { return groups; }
            set { groups = value; }
        }

        public SkillsGroup CurrentGroup
        {
            get { return currentGroup; }
            set 
            { 
                currentGroup = value;
                OnPropertyChanged("CurrentGroup");
            }
        }

        public Skill NewSkill
        {
            get { return newSkill; }
            set 
            { 
                newSkill = value;
                OnPropertyChanged("NewSkill");
            }
        }

        public bool IsNotEditableSkill
        {
            get { return isNotEditableSkill; }
            set
            {
                isNotEditableSkill = value;
                OnPropertyChanged("IsNotEditableSkill");
            }
        }

        public Skill EditSkill
        {
            get { return editSkill; }
            set
            {
                editSkill = value;
                OnPropertyChanged("EditSkill");
            }
        }

        public SkillElement SelectedSkill
        {
            get { return selectedSkill; }
            set
            {
                selectedSkill = value;
                OnPropertyChanged("SelectedSkill");
            }
        }
        #endregion


        #region General Tab Properties
        // General tab.
        public ObservableCollection<GeneralElement> GeneralElementsList
        {
            get { return generalElementsList; }
            set { generalElementsList = value; }
        }

        public List<string> MainFilterList
        {
            get { return mainFilterList; }
            set { mainFilterList = value; }
        }

        public string CurrentMainFilter
        {
            get { return currentMainFilter; }
            set 
            { 
                currentMainFilter = value;
                OnPropertyChanged("CurrentMainFilter");
            }
        }

        public string NewElement
        {
            get { return newElement; }
            set 
            { 
                newElement = value;
                OnPropertyChanged("NewElement");
            }
        }

        public bool IsSupplementaryFilter
        {
            get { return isSupplementaryFilter; }
            set
            {
                isSupplementaryFilter = value;
                OnPropertyChanged("IsSupplementaryFilter");
            }
        }

        public ObservableCollection<IElement> SupplementaryCategoryElements
        {
            get { return supplementaryCategoryElements; }
            set { supplementaryCategoryElements = value; }
        }

        public IElement CurrentSupplementaryElement
        {
            get { return currentSupplementaryElement; }
            set
            {
                currentSupplementaryElement = value;
                OnPropertyChanged("CurrentSupplementaryElement");
            }
        }

        public bool IsNotEditableElement
        {
            get { return isNotEditableElement; }
            set
            {
                isNotEditableElement = value;
                OnPropertyChanged("IsNotEditableElement");
            }
        }

        public string EditElementName
        {
            get { return editElementName; }
            set
            {
                editElementName = value;
                OnPropertyChanged("EditElementName");
            }
        }

        public GeneralElement SelectedElement
        {
            get { return selectedElement; }
            set
            {
                selectedElement = value;
                OnPropertyChanged("SelectedElement");
            }
        }
       
        private void FillMainFilterList()
        {
            mainFilterList.Add("Positions");
            mainFilterList.Add("Companies");
            mainFilterList.Add("Education Facilities");
            mainFilterList.Add("Education Specialties");
            mainFilterList.Add("Entitling Documents");
            mainFilterList.Add("Languages");
            mainFilterList.Add("Language Proficiencies");
            mainFilterList.Add("Skills Groups");
            mainFilterList.Add("Skill Proficiencies");
            mainFilterList.Add("Citizenships");
            mainFilterList.Add("Countries");
            mainFilterList.Add("Cities");
            mainFilterList.Add("Web Services");
            mainFilterList.Add("Access Levels");
        }
        #endregion
    }
}
