using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using TeamworkDB;
using VM_Base;
namespace StaffDatabaseUnit
{
    public class ShowEmployeeDataGlueCode : ViewModelBase
    {        
        // Basic fields
        protected ObservableCollection<SpecificEmployee> employees;
        protected SpecificEmployee currentEmployee;
        protected IDatabaseShowEmployeeData database;
        protected AbstractViewFactory viewFactory;
        protected IView currentView;
        protected IView addEmployeeView;
        protected IView editEmployeeView;
        protected IMessageBox messageBox;
        protected IPhotoConverter photoConverter;

        // Search fields
        protected bool isSearchRequested;
        protected string visibilityInfo;
        protected ObservableCollection<Position> positionsForSearch;
        protected Position selectedPosition;
        protected ObservableCollection<SkillElement> skillsForSearch;

        public ShowEmployeeDataGlueCode()
        {
            // Basic fields
            employees = new ObservableCollection<SpecificEmployee>();
            database = new DatabaseShowEmployeeDataEntity();
            database.LoadEmployeesData(employees);
            if (employees.Count > 0)
                currentEmployee = employees[0];
            messageBox = new CustomMessageBox();

            // Search fields
            isSearchRequested = false;
            visibilityInfo = "Collapsed";
            positionsForSearch = new ObservableCollection<Position>();
            selectedPosition = new Position();
            skillsForSearch = new ObservableCollection<SkillElement>();

            database.LoadPositions(positionsForSearch, selectedPosition);
            if(positionsForSearch.Count > 0)
                selectedPosition = positionsForSearch[0];
            database.LoadSkillsList(skillsForSearch);
        }

        public void UpdateView(object sender, EventArgs e)
        {
            Employees = new ObservableCollection<SpecificEmployee>();
            database.LoadEmployeesData(employees);
            currentEmployee = employees[0];
        }

        #region Properties
        // Basic properties
        public ObservableCollection<SpecificEmployee> Employees
        {
            get
            {
                return employees;
            }

            set
            {
                employees = value;
                OnPropertyChanged("Employees");
            }
        }

        public SpecificEmployee CurrentEmployee
        {
            get
            {
                return currentEmployee;
            }

            set
            {
                currentEmployee = value;
                OnPropertyChanged("CurrentEmployee");
            }
        }

        // Search properties
        public string VisibilityInfo
        {
            get
            {
                return visibilityInfo;
            }

            set
            {
                visibilityInfo = value;
                OnPropertyChanged("VisibilityInfo");
            }
        }

        public ObservableCollection<Position> PositionsForSearch
        {
            get
            {
                return positionsForSearch;
            }

            set
            {
                positionsForSearch = value;
                OnPropertyChanged("PositionsForSearch");
            }
        }

        public Position SelectedPosition
        {
            get
            {
                return selectedPosition;
            }

            set
            {
                selectedPosition = value;
                OnPropertyChanged("SelectedPosition");
            }
        }

        public ObservableCollection<SkillElement> SkillsForSearch
        {
            get
            {
                return skillsForSearch;
            }

            set
            {
                skillsForSearch = value;
                OnPropertyChanged("SkillsForSearch");
            }
        }
        #endregion
    }
}
