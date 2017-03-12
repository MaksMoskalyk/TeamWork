using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamworkDB;
using TeamworkDBEntity;
using System.Collections.ObjectModel;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using VM_Base;
using DelegateCommandNS;
namespace StaffDatabaseUnit
{
    public class InputEmployeeDataViewModel : InputEmployeeDataGlueCode
    {
        #region Commands declaration
        // Main data commands.
        private DelegateCommand bAddPhoto;
        private DelegateCommand bAddEmployee;
        private DelegateCommand bSaveChanges;
        private DelegateCommand bClear;
        private DelegateCommand bAdministration;
        private DelegateCommand bUpdate;
        private DelegateCommand cbPositions;

        // Contact info commands.
        private DelegateCommand bAddPhone;
        private DelegateCommand bEditPhone;
        private DelegateCommand bEditPhoneSaveChanges;
        private DelegateCommand bEditPhoneCancelChanges;        
        private DelegateCommand bDeletePhone;

        private DelegateCommand bAddMail;
        private DelegateCommand bEditMail;
        private DelegateCommand bEditMailSaveChanges;
        private DelegateCommand bEditMailCancelChanges;
        private DelegateCommand bDeleteMail;

        private DelegateCommand bAddWebAccount;
        private DelegateCommand bEditWebAccount;
        private DelegateCommand bEditWebAccountSaveChanges;
        private DelegateCommand bEditWebAccountCancelChanges;
        private DelegateCommand bDeleteWebAccount;

        // Skills info commands.
        private DelegateCommand cbPositionFilters;
        private DelegateCommand cbGroupFilters;
        private DelegateCommand checkSkill;
        private DelegateCommand uncheckSkill;

        // Experience info commands.
        private DelegateCommand bAddExperienceEntry;
        private DelegateCommand bEditExperienceEntry;
        private DelegateCommand bEditExperienceEntrySaveChanges;
        private DelegateCommand bEditExperienceEntryCancelChanges;
        private DelegateCommand bDeleteExperienceEntry;

        // Education info commands.
        private DelegateCommand bAddEducationEntry;
        private DelegateCommand bEditEducationEntry;
        private DelegateCommand bEditEducationEntrySaveChanges;
        private DelegateCommand bEditEducationEntryCancelChanges;
        private DelegateCommand bDeleteEduactionEntry;

        // General info commands.       
        private DelegateCommand cbChooseMonthOfBirth;
        private DelegateCommand cbChooseYearOfBirth;
        private DelegateCommand cbChooseCountry;

        public InputEmployeeDataViewModel() { }
        #endregion

        #region Commands implementation

        #region Main data commands

        #region Add photo command
        public ICommand ButtonAddPhoto
        {
            get
            {
                if (bAddPhoto == null)
                {
                    bAddPhoto = new DelegateCommand(d => AddPhoto(), d => IsAddPhotoAvailable());
                }
                return bAddPhoto;
            }
        }

        private void AddPhoto()
        {
            try
            {
                string photoPath = openFileDialog.GetFilePath();
                EmployeeData.EmployeePhoto = photoConverter.GetBytesFromImage(photoPath);
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsAddPhotoAvailable()
        {
            return true;
        }
        #endregion

        #region Add Employee command
        public ICommand ButtonAddEmployee
        {
            get
            {
                if (bAddEmployee == null)
                {
                    bAddEmployee = new DelegateCommand(d => AddEmployee(), d => IsAddEmployeeAvailable());
                }
                return bAddEmployee;
            }
        }

        private void AddEmployee()
        {
            try
            {
                string result = "Done!";

                if (InputDataVerification(employeeData))
                {
                    if (!database.IsUserExist(employeeData))
                    {
                        result = database.AddEmployee(employeeData);
                        InitiateEmployeeAdditionEvent();
                        currentWindow.CloseView();
                    }
                    else
                    {
                        result = "Such user is already exist!";
                    }
                }
                else
                    result = "You have to fill name, surname and add at least one phone number and e-mail!";

                messageBox.ShowNotification(result, currentWindow.Caption);
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);                
            }
        }

        private bool IsAddEmployeeAvailable()
        {
            return true;
        }
        #endregion

        #region Save changes command
        public ICommand ButtonSaveChanges
        {
            get
            {
                if (bSaveChanges == null)
                {
                    bSaveChanges = new DelegateCommand(d => SaveChanges(), d => IsSaveChangesAvailable());
                }
                return bSaveChanges;
            }
        }

        private void SaveChanges()
        {
            try
            {
                string result = "Done!";

                if (InputDataVerification(employeeData))
                {
                    string message = "Do you want to save changes?";
                    if (messageBox.GetActionConfirmation(message, currentWindow.Caption))
                    {
                        database.ReconnectEmployeeWithProjects(employeeData.Employee, employeeData.Employee);
                        currentWindow.CloseView();
                    }                    
                }
                else
                    result = "You have to fill name, surname and add at least one phone number and e-mail!";

                messageBox.ShowNotification(result, currentWindow.Caption);                
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool InputDataVerification(EmployeeData employeeData)
        {
            return (employeeData.Employee.Name != "" && employeeData.Employee.Surname != "" &&
                employeeData.Phones.Count > 0 && employeeData.Mails.Count > 0);
        }

        private bool IsSaveChangesAvailable()
        {
            return true;
        }
        #endregion

        #region Clear view command
        public ICommand ButtonClearView
        {
            get
            {
                if (bClear == null)
                {
                    bClear = new DelegateCommand(d => ClearView(), d => IsClearViewAvailable());
                }
                return bClear;
            }
        }

        private void ClearView()
        {
            try
            {
                employeeData.EmployeeName = "";
                employeeData.EmployeeSurname = "";
                //employeeData.EmployeePhotoURL = Path.GetFullPath("../../../Photo.png");
                //database.PositionsQuery(employeeData);

                employeeData.InputPhone = "";
                employeeData.Phones = new ObservableCollection<string>();
                employeeData.InputMail = "";
                employeeData.Mails = new ObservableCollection<string>();
                employeeData.InputWebAccount = "";
                employeeData.WebAccounts = new ObservableCollection<WebServiceTableUnit>();

                employeeData.SkillsTotalInfo = new List<SkillTableUnit>();
                database.SkillsQuery(employeeData);

                employeeData.ExperienceTableUnits = new ObservableCollection<ExperienceTableUnit>();
                employeeData.EducationTableUnits = new ObservableCollection<EducationTableUnit>();

                database.LanguagesQuery(employeeData);
                employeeData.EmployeeGeneralDescription = "";
                employeeData.EmployeeProfessionallDescription = "";
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsClearViewAvailable()
        {
            return true;
        }
        #endregion

        #region Open administration window command
        public ICommand ButtonOpenAdministrationWindow
        {
            get
            {
                if (bAdministration == null)
                {
                    bAdministration = new DelegateCommand(d => OpenAdministrationWindow(), d => IsOpenAdministrationWindowAvailable());
                }
                return bAdministration;
            }
        }

        private void OpenAdministrationWindow()
        {
            try
            {
                viewFactory = new DatabaseAdministrationViewFactory();
                administrationWindow = viewFactory.CreateView();

                DatabaseAdministrationViewModel databaseAdministrationViewModel =
                   new DatabaseAdministrationViewModel();
                databaseAdministrationViewModel.GetInitialData();
                databaseAdministrationViewModel.administrationFulfilledEvent 
                    += new AdministrationFulfilled(UpdateView);

                administrationWindow.Data = databaseAdministrationViewModel;
                databaseAdministrationViewModel.CurrentWindow = administrationWindow;
                administrationWindow.ShowView();
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsOpenAdministrationWindowAvailable()
        {
            return true;
        }
        #endregion

        #region Update view command
        public ICommand ButtonUpdate
        {
            get
            {
                if (bUpdate == null)
                {
                    bUpdate = new DelegateCommand(d => UpdateView(), d => IsUpdateViewAvailable());
                }
                return bUpdate;
            }
        }

        private void UpdateView()
        {
            try
            {
                database.Update(employeeData);
                RestoreSkillsTableUnitsData(employeeData);
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsUpdateViewAvailable()
        {
            return true;
        }
        #endregion

        #region Choose position
        public ICommand ComboBoxChoosePosition
        {
            get
            {
                if (cbPositions == null)
                {
                    cbPositions = new DelegateCommand(d => ChoosePosition(), d => IsChoosePositionAvailable());
                }
                return cbPositions;
            }
        }

        private void ChoosePosition()
        {
            try
            {
                
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsChoosePositionAvailable()
        {
            return true;
        }
        #endregion

        #endregion


        #region Contact info commands

        #region Add phone command
        public ICommand ButtonAddPhone
        {
            get
            {
                if (bAddPhone == null)
                {
                    bAddPhone = new DelegateCommand(d => AddPhone(), d => IsAddPhoneAvailable());
                }
                return bAddPhone;
            }
        }

        private void AddPhone()
        {
            try
            {
                if (IsPhoneValid(employeeData.InputPhone))
                {
                    employeeData.Phones.Add(employeeData.InputPhone);
                    PhoneInputError = "";
                }
                else
                    PhoneInputError = "Phone isn't correct!";
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }            
        }

        private bool IsPhoneValid(string phone)
        {            
            string pattern = "^[0-9]+$";
            Regex mask = new Regex(pattern);
            return mask.IsMatch(phone);
        }

        private bool IsAddPhoneAvailable()
        {
            if (employeeData.InputPhone == "" || employeeData.InputPhone == null
                || isPhoneEditionStarted)
            {
                PhoneInputError = "";
                return false;
            }
            else
                return true;
        }
        #endregion

        #region Edit phone command
        public ICommand ButtonEditPhone
        {
            get
            {
                if (bEditPhone == null)
                {
                    bEditPhone = new DelegateCommand(d => EditPhone(), d => IsEditPhoneAvailable());
                }
                return bEditPhone;
            }
        }

        private void EditPhone()
        {
            try
            {
                employeeData.InputPhone = employeeData.SelectedPhone;
                isPhoneEditionStarted = true;
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }           
        }

        private bool IsEditPhoneAvailable()
        {
            if (employeeData.SelectedPhone == "" || employeeData.SelectedPhone == null
                || isPhoneEditionStarted)
                return false;
            else
                return true;
        }
        #endregion

        #region Save phone edition changes command
        public ICommand ButtonEditPhoneSaveChanges
        {
            get
            {
                if (bEditPhoneSaveChanges == null)
                {
                    bEditPhoneSaveChanges = new DelegateCommand(d => EditPhoneSaveChanges(), 
                        d => IsEditPhoneSaveChangesAvailable());
                }
                return bEditPhoneSaveChanges;
            }
        }

        private void EditPhoneSaveChanges()
        {
            try
            {
                employeeData.Phones.Remove(employeeData.SelectedPhone);
                employeeData.Phones.Add(employeeData.InputPhone);
                employeeData.InputPhone = "";
                isPhoneEditionStarted = false;
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsEditPhoneSaveChangesAvailable()
        {
            return (isPhoneEditionStarted);
        }
        #endregion

        #region Cancel phone edition changes command
        public ICommand ButtonEditPhoneCancelChanges
        {
            get
            {
                if (bEditPhoneCancelChanges == null)
                {
                    bEditPhoneCancelChanges = new DelegateCommand(d => EditPhoneCancelChanges(),
                        d => IsEditPhoneCancelChangesAvailable());
                }
                return bEditPhoneCancelChanges;
            }
        }

        private void EditPhoneCancelChanges()
        {
            try
            {
                isPhoneEditionStarted = false;
                employeeData.InputPhone = "";
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsEditPhoneCancelChangesAvailable()
        {
            return (isPhoneEditionStarted);
        }
        #endregion

        #region Delete phone command
        public ICommand ButtonDeletePhone
        {
            get
            {
                if (bDeletePhone == null)
                {
                    bDeletePhone = new DelegateCommand(d => DeletePhone(), d => IsDeletePhoneAvailable());
                }
                return bDeletePhone;
            }
        }

        private void DeletePhone()
        {
            try
            {
                employeeData.Phones.Remove(employeeData.SelectedPhone);
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }            
        }

        private bool IsDeletePhoneAvailable()
        {
            if (employeeData.SelectedPhone == "" || employeeData.SelectedPhone == null 
                || isPhoneEditionStarted)
                return false;
            else
                return true;
        }
        #endregion

        #region Add mail command
        public ICommand ButtonAddMail
        {
            get
            {
                if (bAddMail == null)
                {
                    bAddMail = new DelegateCommand(d => AddMail(), d => IsAddMailAvailable());
                }
                return bAddMail;
            }
        }

        private void AddMail()
        {
            try
            {
                if (IsMailValid(employeeData.InputMail))
                {                    
                    employeeData.Mails.Add(employeeData.InputMail);
                    MailInputError = "";
                }
                else
                    MailInputError = "Mail isn't correct!";           
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }            
        }

        private bool IsMailValid(string mail)
        {
            string pattern = @"^[A-Za-z](\w{2,16})(@)([a-z]+\.)([a-z]+\.)?([a-z]{2,3})$";
            Regex mask = new Regex(pattern);
            return mask.IsMatch(mail);
        }

        private bool IsAddMailAvailable()
        {
            if (employeeData.InputMail == "" || employeeData.InputMail == null
                || isMailEditionStarted)
            {
                MailInputError = "";
                return false;
            }
            else
                return true;
        }
        #endregion

        #region Edit mail command
        public ICommand ButtonEditMail
        {
            get
            {
                if (bEditMail == null)
                {
                    bEditMail = new DelegateCommand(d => EditMail(), d => IsEditMailAvailable());
                }
                return bEditMail;
            }
        }

        private void EditMail()
        {
            try
            {
                employeeData.InputMail = employeeData.SelectedMail;
                isMailEditionStarted = true;
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }           
        }

        private bool IsEditMailAvailable()
        {
            if (employeeData.SelectedMail == "" || employeeData.SelectedMail == null
                || isMailEditionStarted)
                return false;
            else
                return true;
        }
        #endregion

        #region Save mail edition changes command
        public ICommand ButtonEditMailSaveChanges
        {
            get
            {
                if (bEditMailSaveChanges == null)
                {
                    bEditMailSaveChanges = new DelegateCommand(d => EditMailSaveChanges(),
                        d => IsEditMailSaveChangesAvailable());
                }
                return bEditMailSaveChanges;
            }
        }

        private void EditMailSaveChanges()
        {
            try
            {
                employeeData.Mails.Remove(employeeData.SelectedMail);
                employeeData.Mails.Add(employeeData.InputMail);
                employeeData.InputMail = "";
                isMailEditionStarted = false;
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsEditMailSaveChangesAvailable()
        {
            return (isMailEditionStarted);
        }
        #endregion

        #region Cancel mail edition changes command
        public ICommand ButtonEditMailCancelChanges
        {
            get
            {
                if (bEditMailCancelChanges == null)
                {
                    bEditMailCancelChanges = new DelegateCommand(d => EditMailCancelChanges(),
                        d => IsEditMailCancelChangesAvailable());
                }
                return bEditMailCancelChanges;
            }
        }

        private void EditMailCancelChanges()
        {
            try
            {
                isMailEditionStarted = false;
                employeeData.InputMail = "";
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsEditMailCancelChangesAvailable()
        {
            return (isMailEditionStarted);
        }
        #endregion

        #region Delete mail command
        public ICommand ButtonDeleteMail
        {
            get
            {
                if (bDeleteMail == null)
                {
                    bDeleteMail = new DelegateCommand(d => DeleteMail(), d => IsDeleteMailAvailable());
                }
                return bDeleteMail;
            }
        }
        private void DeleteMail()
        {
            try
            {
                employeeData.Mails.Remove(employeeData.SelectedMail);
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }            
        }

        private bool IsDeleteMailAvailable()
        {
            if (employeeData.SelectedMail == "" || employeeData.SelectedMail == null 
                || isMailEditionStarted)
                return false;
            else
                return true;
        }
        #endregion

        #region Add web service account command
        public ICommand ButtonAddWebServiceAccount
        {
            get
            {
                if (bAddWebAccount == null)
                {
                    bAddWebAccount = new DelegateCommand(d => AddWebServiceAccount(), d => IsAddWebServiceAccountAvailable());
                }
                return bAddWebAccount;
            }
        }

        private void AddWebServiceAccount()
        {
            try
            {
                WebServiceTableUnit unit = new WebServiceTableUnit(employeeData.SelectedWebService, 
                    employeeData.InputWebAccount);
                employeeData.WebAccounts.Add(unit);
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }            
        }

        private bool IsAddWebServiceAccountAvailable()
        {
            if (employeeData.InputWebAccount == "" || employeeData.InputWebAccount == null
                || isWebAccountEditionStarted)
                return false;
            else
                return true;
        }
        #endregion

        #region Edit web service account command
        public ICommand ButtonEditWebServiceAccount
        {
            get
            {
                if (bEditWebAccount == null)
                {
                    bEditWebAccount = new DelegateCommand(d => EditWebServiceAccount(), d => IsEditWebServiceAccountAvailable());
                }
                return bEditWebAccount;
            }
        }

        private void EditWebServiceAccount()
        {
            try
            {
                employeeData.SelectedWebService = employeeData.SelectedWebAccount.Service;
                employeeData.InputWebAccount = employeeData.SelectedWebAccount.Account;
                isWebAccountEditionStarted = true;
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }            
        }

        private bool IsEditWebServiceAccountAvailable()
        {
            if (employeeData.SelectedWebAccount == null || isWebAccountEditionStarted)
                return false;
            else
                return true;
        }
        #endregion

        #region Save web account edition changes command
        public ICommand ButtonEditWebAccountSaveChanges
        {
            get
            {
                if (bEditWebAccountSaveChanges == null)
                {
                    bEditWebAccountSaveChanges = new DelegateCommand(d => EditWebAccountSaveChanges(),
                        d => IsEditWebAccountSaveChangesAvailable());
                }
                return bEditWebAccountSaveChanges;
            }
        }

        private void EditWebAccountSaveChanges()
        {
            try
            {
                employeeData.WebAccounts.Remove(employeeData.SelectedWebAccount);
                WebServiceTableUnit unit = new WebServiceTableUnit(employeeData.SelectedWebService,
                    employeeData.InputWebAccount);
                employeeData.WebAccounts.Add(unit);

                employeeData.InputWebAccount = "";
                isWebAccountEditionStarted = false;
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsEditWebAccountSaveChangesAvailable()
        {
            return (isWebAccountEditionStarted);
        }
        #endregion

        #region Cancel web account edition changes command
        public ICommand ButtonEditWebAccountCancelChanges
        {
            get
            {
                if (bEditWebAccountCancelChanges == null)
                {
                    bEditWebAccountCancelChanges = new DelegateCommand(d => EditWebAccountCancelChanges(),
                        d => IsEditWebAccountCancelChangesAvailable());
                }
                return bEditWebAccountCancelChanges;
            }
        }

        private void EditWebAccountCancelChanges()
        {
            try
            {
                isWebAccountEditionStarted = false;
                employeeData.InputWebAccount = "";
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsEditWebAccountCancelChangesAvailable()
        {
            return (isWebAccountEditionStarted);
        }
        #endregion

        #region Delete web service account command
        public ICommand ButtonDeleteWebServiceAccount
        {
            get
            {
                if (bDeleteWebAccount == null)
                {
                    bDeleteWebAccount = new DelegateCommand(d => DeleteWebServiceAccount(), d => IsDeleteWebServiceAccountAvailable());
                }
                return bDeleteWebAccount;
            }
        }

        private void DeleteWebServiceAccount()
        {
            try
            {
                employeeData.WebAccounts.Remove(employeeData.SelectedWebAccount);
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }           
        }

        private bool IsDeleteWebServiceAccountAvailable()
        {
            if (employeeData.SelectedWebAccount == null || isWebAccountEditionStarted)
                return false;
            else
                return true;
        }
        #endregion

        #endregion


        #region Skills info commands

        #region Choose position filter command
        public ICommand ComboBoxChoosePositionFilter
        {
            get
            {
                if (cbPositionFilters == null)
                {
                    cbPositionFilters = new DelegateCommand(d => ChoosePositionFilter(), d => true);
                }
                return cbPositionFilters;
            }
        }

        private void ChoosePositionFilter()
        {
            try
            {
                if(employeeData.SelectedPositionFilter != null)
                    database.GroupFiltersQuery(employeeData);
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }           
        }      
        #endregion

        #region Choose group filter command
        public ICommand ComboBoxChooseGroupFilter
        {
            get
            {
                if (cbGroupFilters == null)
                {
                    cbGroupFilters = new DelegateCommand(d => ChooseGroupFilter(), d => true);
                }
                return cbGroupFilters;
            }
        }

        private void ChooseGroupFilter()
        {
            try
            {
                if (employeeData.SelectedGroupFilter != null)
                {
                    database.SkillsQuery(employeeData);
                    RestoreSkillsTableUnitsData(employeeData);
                }
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }           
        }       
        #endregion

        #region Check skill command        
        public ICommand CheckSkill
        {
            get
            {
                if (checkSkill == null)
                {
                    checkSkill = new DelegateCommand(d => CheckSkillHandler(), d => true);
                }
                return checkSkill;
            }
        }

        private void CheckSkillHandler()
        {
            try
            {
                foreach (SkillTableUnit currentUnit in employeeData.SkillTableUnits)
                {
                    if (currentUnit.IsChecked)                        
                    {
                        bool isMatch = false;
                        foreach (SkillTableUnit globalUnit in employeeData.SkillsTotalInfo)
                        {
                            if (globalUnit.Skill.Name == currentUnit.Skill.Name)
                                isMatch = true;
                        }
                        if (!isMatch)
                            employeeData.SkillsTotalInfo.Add(currentUnit);
                    }
                }
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }
        #endregion

        #region Uncheck skill command
        public ICommand UncheckSkill
        {
            get
            {
                if (uncheckSkill == null)
                {
                    uncheckSkill = new DelegateCommand(d => UncheckSkillHandler(), d => true);
                }
                return uncheckSkill;
            }
        }

        private void UncheckSkillHandler()
        {
            try
            {
                foreach (SkillTableUnit currentUnit in employeeData.SkillTableUnits)
                {
                    if (!currentUnit.IsChecked)
                    {
                        bool isMatch = false;
                        foreach (SkillTableUnit globalUnit in employeeData.SkillsTotalInfo)
                        {
                            if (globalUnit.Skill.Name == currentUnit.Skill.Name)
                                isMatch = true;
                        }
                        if (isMatch)
                            employeeData.SkillsTotalInfo.Remove(currentUnit);
                    }
                }
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }
        #endregion

        #endregion


        #region Experience info commands

        #region Add experience entry command
        public ICommand ButtonAddExperienceEntry
        {
            get
            {
                if (bAddExperienceEntry == null)
                {
                    bAddExperienceEntry = new DelegateCommand(d => AddExperienceEntry(),
                        d => IsAddExperienceEntryAvailable());
                }
                return bAddExperienceEntry;
            }
        }

        private void AddExperienceEntry()
        {
            try
            {
                ExperienceTableUnit unit = new ExperienceTableUnit();
                unit.Company = employeeData.SelectedCompany;
                unit.Position = employeeData.CompanySelectedPosition;
                unit.DateOfHiring = employeeData.SelectedMonthOfHiring + "/" + employeeData.SelectedYearOfHiring;
                unit.DateOfDismissal = employeeData.SelectedMonthOfDismissal + "/" + employeeData.SelectedYearOfDismissal;
                employeeData.ExperienceTableUnits.Add(unit);
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsAddExperienceEntryAvailable()
        {
            return !isExperienceEditionStarted;
        }
        #endregion

        #region Edit experience entry command
        public ICommand ButtonEditExperienceEntry
        {
            get
            {
                if (bEditExperienceEntry == null)
                {
                    bEditExperienceEntry = new DelegateCommand(d => EditExperienceEntry(), d => IsEditExperienceEntryAvailable());
                }
                return bEditExperienceEntry;
            }
        }

        private void EditExperienceEntry()
        {
            try
            {                               
                employeeData.CompanySelectedPosition = employeeData.SelectedExperienceTableUnit.Position;
                employeeData.SelectedCompany = employeeData.SelectedExperienceTableUnit.Company;

                string dateOfHiring = employeeData.SelectedExperienceTableUnit.DateOfHiring;
                employeeData.SelectedMonthOfHiring = dateOfHiring.Substring(0, dateOfHiring.LastIndexOf('/'));
                employeeData.SelectedYearOfHiring = dateOfHiring.Substring(dateOfHiring.IndexOf('/') + 1);

                string dateOfDismissal = employeeData.SelectedExperienceTableUnit.DateOfDismissal;
                employeeData.SelectedMonthOfDismissal = dateOfDismissal.Substring(0, dateOfDismissal.LastIndexOf('/'));
                employeeData.SelectedYearOfDismissal = dateOfDismissal.Substring(dateOfDismissal.IndexOf('/') + 1);

                isExperienceEditionStarted = true;
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsEditExperienceEntryAvailable()
        {
            if (employeeData.SelectedExperienceTableUnit == null || isExperienceEditionStarted)
                return false;
            else
                return true;
        }
        #endregion

        #region Edit experience entry save changes command
        public ICommand ButtonEditExperienceEntrySaveChanges
        {
            get
            {
                if (bEditExperienceEntrySaveChanges == null)
                {
                    bEditExperienceEntrySaveChanges = new DelegateCommand(d => EditExperienceEntrySaveChanges(), 
                        d => IsEditExperienceEntrySaveChangesAvailable());
                }
                return bEditExperienceEntrySaveChanges;
            }
        }

        private void EditExperienceEntrySaveChanges()
        {
            try
            {
                DeleteExperienceEntry();
                AddExperienceEntry();
                isExperienceEditionStarted = false;
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsEditExperienceEntrySaveChangesAvailable()
        {
            return isExperienceEditionStarted;
        }
        #endregion

        #region Edit experience entry cancel changes command
        public ICommand ButtonEditExperienceEntryCancelChanges
        {
            get
            {
                if (bEditExperienceEntryCancelChanges == null)
                {
                    bEditExperienceEntryCancelChanges = new DelegateCommand(d => EditExperienceEntryCancelChanges(),
                        d => IsEditExperienceEntryCancelChangesAvailable());
                }
                return bEditExperienceEntryCancelChanges;
            }
        }

        private void EditExperienceEntryCancelChanges()
        {
            try
            {
                isExperienceEditionStarted = false;
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsEditExperienceEntryCancelChangesAvailable()
        {
            return isExperienceEditionStarted;
        }
        #endregion

        #region Delete experience entry command
        public ICommand ButtonDeleteExperienceEntry
        {
            get
            {
                if (bDeleteExperienceEntry == null)
                {
                    bDeleteExperienceEntry = new DelegateCommand(d => DeleteExperienceEntry(), d => IsDeleteExperienceEntryAvailable());
                }
                return bDeleteExperienceEntry;
            }
        }

        private void DeleteExperienceEntry()
        {
            try
            {
                employeeData.ExperienceTableUnits.Remove(employeeData.SelectedExperienceTableUnit);
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsDeleteExperienceEntryAvailable()
        {
            if (employeeData.SelectedExperienceTableUnit == null || isExperienceEditionStarted)
                return false;
            else
                return true;
        }
        #endregion

        #endregion


        #region Education info commands

        #region Add education entry command
        public ICommand ButtonAddEducationEntry
        {
            get
            {
                if (bAddEducationEntry == null)
                {
                    bAddEducationEntry = new DelegateCommand(d => AddEducationEntry(), 
                        d => IsAddEducationEntryAvailable());
                }
                return bAddEducationEntry;
            }
        }

        private void AddEducationEntry()
        {
            try
            {
                EducationTableUnit unit = new EducationTableUnit();
                unit.EducationFacility = employeeData.SelectedEducationFacility;
                unit.Specialty = employeeData.SelectedEducationSpecialty;
                unit.EntitlingDocument = employeeData.SelectedEducationEntitlingDocument;
                unit.DateOfGraduation = employeeData.SelectedMonthOfGraduation + "/" + employeeData.SelectedYearOfGraduation;
                employeeData.EducationTableUnits.Add(unit);
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsAddEducationEntryAvailable()
        {
            return (!isEducationEditionStarted);
        }
        #endregion

        #region Edit education entry command
        public ICommand ButtonEditEducationEntry
        {
            get
            {
                if (bEditEducationEntry == null)
                {
                    bEditEducationEntry = new DelegateCommand(d => EditEducationEntry(), d => IsEditEducationEntryAvailable());
                }
                return bEditEducationEntry;
            }
        }

        private void EditEducationEntry()
        {
            try
            {
                employeeData.SelectedEducationFacility = employeeData.SelectedEducationTableUnit.EducationFacility;
                employeeData.SelectedEducationSpecialty = employeeData.SelectedEducationTableUnit.Specialty;
                employeeData.SelectedEducationEntitlingDocument = employeeData.SelectedEducationTableUnit.EntitlingDocument;

                string dateOfGraduation = employeeData.SelectedEducationTableUnit.DateOfGraduation;
                employeeData.SelectedMonthOfGraduation = dateOfGraduation.Substring(0, dateOfGraduation.LastIndexOf('/'));
                employeeData.SelectedYearOfGraduation = dateOfGraduation.Substring(dateOfGraduation.IndexOf('/') + 1);

                isEducationEditionStarted = true;
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsEditEducationEntryAvailable()
        {
            if (employeeData.SelectedEducationTableUnit == null || isEducationEditionStarted)
                return false;
            else
                return true;
        }
        #endregion

        #region Edit education entry save changes command
        public ICommand ButtonEditEducationEntrySaveChanges
        {
            get
            {
                if (bEditEducationEntrySaveChanges == null)
                {
                    bEditEducationEntrySaveChanges = new DelegateCommand(d => EditEducationEntrySaveChanges(), 
                        d => IsEditEducationEntrySaveChangesAvailable());
                }
                return bEditEducationEntrySaveChanges;
            }
        }

        private void EditEducationEntrySaveChanges()
        {
            try
            {
                DeleteEducationEntry();
                AddEducationEntry();
                isEducationEditionStarted = false;
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsEditEducationEntrySaveChangesAvailable()
        {
            return isEducationEditionStarted;
        }
        #endregion

        #region Edit education entry cancel changes command
        public ICommand ButtonEditEducationEntryCancelChanges
        {
            get
            {
                if (bEditEducationEntryCancelChanges == null)
                {
                    bEditEducationEntryCancelChanges = new DelegateCommand(d => EditEducationEntryCancelChanges(),
                        d => IsEditEducationEntryCancelChangesAvailable());
                }
                return bEditEducationEntryCancelChanges;
            }
        }

        private void EditEducationEntryCancelChanges()
        {
            try
            {
                isEducationEditionStarted = false;
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsEditEducationEntryCancelChangesAvailable()
        {
            return isEducationEditionStarted;
        }
        #endregion

        #region Delete education entry command
        public ICommand ButtonDeleteEducationEntry
        {
            get
            {
                if (bDeleteEduactionEntry == null)
                {
                    bDeleteEduactionEntry = new DelegateCommand(d => DeleteEducationEntry(), 
                        d => IsDeleteEducationEntryAvailable());
                }
                return bDeleteEduactionEntry;
            }
        }

        private void DeleteEducationEntry()
        {
            try
            {
                employeeData.EducationTableUnits.Remove(employeeData.SelectedEducationTableUnit);
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }

        private bool IsDeleteEducationEntryAvailable()
        {
            if (employeeData.SelectedEducationTableUnit == null || isEducationEditionStarted)
                return false;
            else
                return true;
        }
        #endregion

        #endregion


        #region General info commands

        #region Choose month of birth command
        public ICommand ComboBoxChooseMonthOfBirth
        {
            get
            {
                if (cbChooseMonthOfBirth == null)
                {
                    cbChooseMonthOfBirth = new DelegateCommand(d => ChooseMonthOfBirth(), d => true);
                }
                return cbChooseMonthOfBirth;
            }
        }

        private void ChooseMonthOfBirth()
        {
            try
            {
                if(employeeData.SelectedMonthOfBirth != null)
                    database.SelectDays(employeeData);
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }
        #endregion

        #region Choose year of birth command
        public ICommand ComboBoxChooseYearOfBirth
        {
            get
            {
                if (cbChooseYearOfBirth == null)
                {
                    cbChooseYearOfBirth = new DelegateCommand(d => ChooseYearOfBirth(), d => true);
                }
                return cbChooseYearOfBirth;
            }
        }
        private void ChooseYearOfBirth()
        {
            try
            {
                if(employeeData.SelectedMonthOfBirth == "02")
                    database.SelectDays(employeeData);
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }
        #endregion

        #region Choose country command
        public ICommand ComboBoxChooseCountry
        {
            get
            {
                if (cbChooseCountry == null)
                {
                    cbChooseCountry = new DelegateCommand(d => ChooseCountry(), d => true);
                }
                return cbChooseCountry;
            }
        }
        private void ChooseCountry()
        {
            try
            {
                if (employeeData.SelectedCountry != null)
                    database.CitiesQuery(employeeData);
            }
            catch (Exception error)
            {
                messageBox.ShowNotification(error.Message, currentWindow.Caption);
            }
        }
        #endregion

        #endregion

        #endregion
    }
}
