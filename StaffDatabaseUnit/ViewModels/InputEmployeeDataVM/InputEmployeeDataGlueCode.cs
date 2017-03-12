using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM_Base;

namespace StaffDatabaseUnit
{
    public delegate void NewEmployeeAddition(object sender, EventArgs e);
    public class InputEmployeeDataGlueCode : ViewModelBase
    {
        public event NewEmployeeAddition employeeAdditionEvent;
        protected EmployeeData employeeData;
        protected IDatabaseInputEmployeeData database;
        protected AbstractViewFactory viewFactory;
        protected IView currentWindow;
        protected IView administrationWindow;
        protected IMessageBox messageBox;
        protected IOpenFileDialog openFileDialog;
        protected IPhotoConverter photoConverter;

        protected bool isPhoneEditionStarted;
        protected bool isMailEditionStarted;
        protected bool isWebAccountEditionStarted;

        protected bool isExperienceEditionStarted;
        protected bool isEducationEditionStarted;

        protected string caption;
        protected string phoneInputError;
        protected string mailInputError;

        public InputEmployeeDataGlueCode()
        {
            employeeData = new EmployeeData();
            database = new DatabaseInputEmployeeDataEntity();

            messageBox = new CustomMessageBox();
            openFileDialog = new CustomOpenFileDialog();

            photoConverter = new PhotoDatabaseConvertor();
            string defaultPhotoPath = "../../Resources/Photo.png";
            EmployeeData.EmployeePhoto = photoConverter.GetBytesFromImage(defaultPhotoPath);
        }

        protected void InitiateEmployeeAdditionEvent()
        {
            if (employeeAdditionEvent != null)
                employeeAdditionEvent(this, EventArgs.Empty);
        }
       
        public EmployeeData EmployeeData
        {
            get { return employeeData; }
            set
            {
                employeeData = value;
                OnPropertyChanged("EmployeeData");
            }
        }

        public string PhoneInputError
        {
            get { return phoneInputError; }
            set
            {
                phoneInputError = value;
                OnPropertyChanged("PhoneInputError");
            }
        }

        public string MailInputError
        {
            get { return mailInputError; }
            set
            {
                mailInputError = value;
                OnPropertyChanged("MailInputError");
            }
        }

        public IView CurrentWindow
        {
            get { return currentWindow; }
            set { currentWindow = value; }
        }

        protected void UpdateView(object sender, EventArgs e)
        {
            database.Update(employeeData);
            RestoreSkillsTableUnitsData(employeeData);
        }

        protected void RestoreSkillsTableUnitsData(EmployeeData employeeData)
        {
            for (int i = 0; i < employeeData.SkillTableUnits.Count; i++)
            {
                foreach (SkillTableUnit globalUnit in employeeData.SkillsTotalInfo)
                {
                    if (employeeData.SkillTableUnits[i].Skill.Name == globalUnit.Skill.Name)
                    {
                        employeeData.SkillTableUnits[i] = globalUnit;
                    }
                }
            }
        }

        public void LoadData()
        {
            database.Initiation(employeeData);
        }
    }
}
