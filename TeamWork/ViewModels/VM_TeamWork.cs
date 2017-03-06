using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using DelegateCommandNS;
using Editor_Projects;
using Editor_Task;
using Facade;
using VM_Base;
using TeamworkDB;
using StaffDatabaseUnit;
using System.IO;
using TeamWork.FileSharingService;
using TeamWork.ServiceProgramMessenge;
using ProgramMessenger_WcfSL;
using CustomMessageBox_OK;
using CustomMessageBox_YesNo;

namespace TeamWork
{ 
    public class VM_TeamWork: ViewModelBase, IServiceProgramMessengerCallback
    {
        // временно разместил в начале, для удобства поиска
        private string szSearchProjName;
        private string szSearchTaskName;
        private ObservableCollection<KeyValuePair<int, string>> listFindProjects;
        private ObservableCollection<KeyValuePair<int, string>> listFindTasks;

        private ObservableCollection<checkEl<KeyValuePair<int, string>>> listCustomer;
        private ObservableCollection<checkEl<KeyValuePair<int, string>>> listDuration;
        private ObservableCollection<checkEl<KeyValuePair<int, string>>> listLead;
        private ObservableCollection<checkEl<KeyValuePair<int, string>>> listOperationSystem;
        private ObservableCollection<checkEl<KeyValuePair<int, string>>> listSkill;
        private ObservableCollection<checkEl<KeyValuePair<int, string>>> listStage;
        private ObservableCollection<checkEl<KeyValuePair<int, string>>> listType;
        private ObservableCollection<checkEl<KeyValuePair<int, string>>> listObjective;
        
        private ObservableCollection<checkEl<KeyValuePair<int, string>>> listAssignee;
        private ObservableCollection<checkEl<KeyValuePair<int, string>>> listPriority;
        private ObservableCollection<checkEl<KeyValuePair<int, string>>> listStatus;
        private ObservableCollection<checkEl<KeyValuePair<int, string>>> listTypeTasks;

        public ObservableCollection<KeyValuePair<int, string>> ListFindProjects
        {
            get
            {
                return listFindProjects;
            }
            set
            {
                listFindProjects = value;
                OnPropertyChanged("ListFindProjects");
            }
        }
        public ObservableCollection<KeyValuePair<int, string>> ListFindTasks
        {
            get
            {
                return listFindTasks;
            }
            set
            {
                listFindTasks = value;
                OnPropertyChanged("ListFindTasks");
            }
        }
        public string SearchProjName
        {
            get
            {
                return szSearchProjName;
            }
            set
            {
                szSearchProjName = value;
                OnPropertyChanged("SearchProjName");
            }
        }
        public ObservableCollection<checkEl<KeyValuePair<int, string>>> ListCustomer
        {
            get
            {
                return listCustomer;
            }
            set
            {
                listCustomer = value;
                OnPropertyChanged("ListCustomer");
            }
        }
        public ObservableCollection<checkEl<KeyValuePair<int, string>>> ListDuration
        {
            get
            {
                return listDuration;
            }
            set
            {
                listDuration = value;
                OnPropertyChanged("ListDuration");
            }
        }
        public ObservableCollection<checkEl<KeyValuePair<int, string>>> ListLead
        {
            get
            {
                return listLead;
            }
            set
            {
                listLead = value;
                OnPropertyChanged("ListLead");
            }
        }
        public ObservableCollection<checkEl<KeyValuePair<int, string>>> ListOperationSystem
        {
            get
            {
                return listOperationSystem;
            }
            set
            {
                listOperationSystem = value;
                OnPropertyChanged("ListOperationSystem");
            }
        }
        public ObservableCollection<checkEl<KeyValuePair<int, string>>> ListSkill
        {
            get
            {
                return listSkill;
            }
            set
            {
                listSkill = value;
                OnPropertyChanged("ListSkill");
            }
        }
        public ObservableCollection<checkEl<KeyValuePair<int, string>>> ListStage
        {
            get
            {
                return listStage;
            }
            set
            {
                listStage = value;
                OnPropertyChanged("ListStage");
            }
        }
        public ObservableCollection<checkEl<KeyValuePair<int, string>>> ListType
        {
            get
            {
                return listType;
            }
            set
            {
                listType = value;
                OnPropertyChanged("ListType");
            }
        }
        public ObservableCollection<checkEl<KeyValuePair<int, string>>> ListObjective
        {
            get
            {
                return listObjective;
            }
            set
            {
                listObjective = value;
                OnPropertyChanged("ListObjective");
            }
        }


        public string SearchTaskName
        {
            get
            {
                return szSearchTaskName;
            }
            set
            {
                szSearchTaskName = value;
                OnPropertyChanged("SearchTaskName");
            }
        }
        public ObservableCollection<checkEl<KeyValuePair<int, string>>> ListAssignee
        {
            get
            {
                return listAssignee;
            }
            set
            {
                listAssignee = value;
                OnPropertyChanged("ListAssignee");
            }
        }
        public ObservableCollection<checkEl<KeyValuePair<int, string>>> ListPriority
        {
            get
            {
                return listPriority;
            }
            set
            {
                listPriority = value;
                OnPropertyChanged("ListPriority");
            }
        }
        public ObservableCollection<checkEl<KeyValuePair<int, string>>> ListStatus
        {
            get
            {
                return listStatus;
            }
            set
            {
                listStatus = value;
                OnPropertyChanged("ListStatus");
            }
        }
        public ObservableCollection<checkEl<KeyValuePair<int, string>>> ListTypeTasks
        {
            get
            {
                return listTypeTasks;
            }
            set
            {
                listTypeTasks = value;
                OnPropertyChanged("ListTypeTasks");
            }
        }

        public void loadCustomerNorm()
        {
            ListCustomer.Clear();
            List<KeyValuePair<int, string>> tempList = F_Projects.GetAllCustomers();
            foreach (var temp in tempList)
            {
                ListCustomer.Add(new checkEl<KeyValuePair<int, string>>(false, temp));
            }
        }
        public void loadDurationNorm()
        {
            ListDuration.Clear();
            List<KeyValuePair<int, string>> tempList = F_Projects.GetAllDurations();
            foreach (var temp in tempList)
            {
                ListDuration.Add(new checkEl<KeyValuePair<int, string>>(false, temp));
            }
        }
        public void loadLeadNorm()
        {
            ListLead.Clear();

            List<KeyValuePair<int, string>> tempList = F_Staff.GetTeamLeaders();
            foreach (var temp in tempList)
            {
                ListLead.Add(new checkEl<KeyValuePair<int, string>>(false, temp));
            }
        }
        public void loadOperationSystemNorm()
        {
            ListOperationSystem.Clear();
            List<KeyValuePair<int, string>> tempList = F_Projects.GetAllOSs();
            foreach (var temp in tempList)
            {
                ListOperationSystem.Add(new checkEl<KeyValuePair<int, string>>(false, temp));
            }
        }
        public void loadSkillNorm()
        {
            ListSkill.Clear();
            List<KeyValuePair<int, string>> tempList = F_Projects.GetAllSkills();
            foreach (var temp in tempList)
            {
                ListSkill.Add(new checkEl<KeyValuePair<int, string>>(false, temp));
            }
        }
        public void loadStageNorm()
        {
            ListStage.Clear();
            List<KeyValuePair<int, string>> tempList = F_Projects.GetAllStages();
            foreach (var temp in tempList)
            {
                ListStage.Add(new checkEl<KeyValuePair<int, string>>(false, temp));
            }
        }
        public void loadTypeNorm()
        {
            ListType.Clear();
            List<KeyValuePair<int, string>> tempList = F_Projects.GetAllTypes();
            foreach (var temp in tempList)
            {
                ListType.Add(new checkEl<KeyValuePair<int, string>>(false, temp));
            }
        }
        public void loadObjectiveNorm()
        {
            ListObjective.Clear();
            List<KeyValuePair<int, string>> tempList = F_Projects.GetAllObjectives();
            foreach (var temp in tempList)
            {
                ListObjective.Add(new checkEl<KeyValuePair<int, string>>(false, temp));
            }
        }

        public void loadAssigneeNorm()
        {
            ListAssignee.Clear();
            List<KeyValuePair<int, string>> tempList = F_Staff.GetEmployeesFullNameWithId();
            foreach (var temp in tempList)
            {
                ListAssignee.Add(new checkEl<KeyValuePair<int, string>>(false, temp));
            }
        }
        public void loadPriorityNorm()
        {
            ListPriority.Clear();
            List<KeyValuePair<int, string>> tempList = F_Task.GetAllPriorityes();
            foreach (var temp in tempList)
            {
                ListPriority.Add(new checkEl<KeyValuePair<int, string>>(false, temp));
            }
        }
        public void loadStatusNorm()
        {
            ListStatus.Clear();
            List<KeyValuePair<int, string>> tempList = F_Task.GetAllStatuses();
            foreach (var temp in tempList)
            {
                ListStatus.Add(new checkEl<KeyValuePair<int, string>>(false, temp));
            }
        }
        public void loadTypeTasksNorm()
        {
            ListTypeTasks.Clear();
            List<KeyValuePair<int, string>> tempList = F_Task.GetAllTaskTypes();
            foreach (var temp in tempList)
            {
                ListTypeTasks.Add(new checkEl<KeyValuePair<int, string>>(false, temp));
            }
        }

        private void LoadSearchNorms()
        {
            loadCustomerNorm();
            loadDurationNorm();
            loadLeadNorm();
            loadOperationSystemNorm();
            loadSkillNorm();
            loadStageNorm();
            loadTypeNorm();
            loadObjectiveNorm();
            loadAssigneeNorm();
            loadPriorityNorm();
            loadStatusNorm();
            loadTypeTasksNorm();
        }
        private DelegateCommand ButtonSearchClick;
        public ICommand BSearchProject_Click
        {
            get
            {
                if (ButtonSearchClick == null)
                {
                    ButtonSearchClick = new DelegateCommand(param => this.SearchProject(), param => true);
                }
                return ButtonSearchClick;
            }
        }
        List<List<KeyValuePair<int, string>>> CheckSearchProjNorm()
        {
            List<List<KeyValuePair<int, string>>> resList = new List<List<KeyValuePair<int, string>>>();
            List<KeyValuePair<int, string>> tempList = new List<KeyValuePair<int, string>>();
            foreach (var temp in listCustomer)
            {
                if (temp.isCheck)
                    tempList.Add(temp.chClass);
            }
            resList.Add(tempList);
            tempList = new List<KeyValuePair<int, string>>();
            foreach (var temp in listDuration)
            {
                if (temp.isCheck)
                    tempList.Add(temp.chClass);
            }
            resList.Add(tempList);
            tempList = new List<KeyValuePair<int, string>>();
            foreach (var temp in ListObjective)
            {
                if (temp.isCheck)
                    tempList.Add(temp.chClass);
            }
            resList.Add(tempList);
            tempList = new List<KeyValuePair<int, string>>();
            foreach (var temp in listOperationSystem)
            {
                if (temp.isCheck)
                    tempList.Add(temp.chClass);
            }
            resList.Add(tempList);
            tempList = new List<KeyValuePair<int, string>>();
            foreach (var temp in listSkill)
            {
                if (temp.isCheck)
                    tempList.Add(temp.chClass);
            }
            resList.Add(tempList);
            tempList = new List<KeyValuePair<int, string>>();
            foreach (var temp in listStage)
            {
                if (temp.isCheck)
                    tempList.Add(temp.chClass);
            }
            resList.Add(tempList);
            tempList = new List<KeyValuePair<int, string>>();
            foreach (var temp in listType)
            {
                if (temp.isCheck)
                    tempList.Add(temp.chClass);
            }
            resList.Add(tempList);

            return resList;
        }
        void SearchProject()
        {
            List<List<KeyValuePair<int, string>>> tempNorm = CheckSearchProjNorm();
            List<KeyValuePair<int, string>> tempList = new List<KeyValuePair<int, string>>();
            ListFindProjects.Clear();
            if (SearchProjName.Length > 0)
            {
                tempList = F_Projects.GetAllProjectsNameFilter(SearchProjName,
                    tempNorm[0].Select(n => n.Key).ToList(),tempNorm[1].Select(n => n.Key).ToList(),
                    tempNorm[2].Select(n => n.Key).ToList(), tempNorm[3].Select(n => n.Key).ToList(),
                    tempNorm[4].Select(n => n.Key).ToList(), tempNorm[5].Select(n => n.Key).ToList(), tempNorm[6].Select(n => n.Key).ToList());
            }
            foreach (var temp in tempList)
            {
                ListFindProjects.Add(temp);
            }
        }
        private DelegateCommand ButtonSearchTaskClick;
        public ICommand BSearchTasks_Click
        {
            get
            {
                if (ButtonSearchTaskClick == null)
                {
                    ButtonSearchTaskClick = new DelegateCommand(param => this.SearchTasks(), param => true);
                }
                return ButtonSearchTaskClick;
            }
        }
        List<List<KeyValuePair<int, string>>> CheckSearchTasksNorm()
        {
            List<List<KeyValuePair<int, string>>> resList = new List<List<KeyValuePair<int, string>>>();
            List<KeyValuePair<int, string>> tempList = new List<KeyValuePair<int, string>>();
            foreach (var temp in ListAssignee)
            {
                if (temp.isCheck)
                    tempList.Add(temp.chClass);
            }
            resList.Add(tempList);
            foreach (var temp in ListPriority)
            {
                if (temp.isCheck)
                    tempList.Add(temp.chClass);
            }
            resList.Add(tempList);
            foreach (var temp in ListStatus)
            {
                if (temp.isCheck)
                    tempList.Add(temp.chClass);
            }
            resList.Add(tempList);
            foreach (var temp in ListTypeTasks)
            {
                if (temp.isCheck)
                    tempList.Add(temp.chClass);
            }
            resList.Add(tempList);
            return resList;
        }
        void SearchTasks()
        {
            List<List<KeyValuePair<int, string>>> tempNorm = CheckSearchTasksNorm();
            List<KeyValuePair<int, string>> tempList = new List<KeyValuePair<int, string>>();
            ListFindTasks.Clear();
            if (SearchTaskName.Length > 0)
            {
                tempList = F_Task.GetAllIssuesFilter(CurrentProject.Id, SearchTaskName,
                    tempNorm[0].Select(n => n.Key).ToList(), tempNorm[1].Select(n => n.Key).ToList(),
                    tempNorm[2].Select(n => n.Key).ToList(), tempNorm[3].Select(n => n.Key).ToList());
            }
            foreach (var temp in tempList)
            {
                ListFindTasks.Add(temp);
            }
        }
        #region fields
        private string WsfConName;
        private List<treeElem> listTasks;
        private List<treeElem> listProj;
        private ServiceProgramMessengerClient proxyProgramMessenger;
        public SynchronizationContext uiContext;
        private InstanceContext site;
        private Visibility vPrName;
        private Visibility vCustomerName;
        private Visibility vStage;
        private Visibility vDueDate;
        private Visibility vDuration;
        private Visibility vObjective;
        private Visibility vDescription;
        private Visibility vTaskName;
        private Visibility vDueDateTask;
        private Visibility vStatus;
        private Visibility vTaskType;
        private Visibility vTaskAssignees;
        private Visibility vTaskDescription;
        private Visibility vTaskPriority;
        private int cbSelTaskAssignees;
        private List<checkEl<KeyValuePair<int, string>>> taskAssignees;
        private List<KeyValuePair<int, string>> listTaskAssignees;
        private int CurrentIssueID;
        private bool isEnabledSavePr;
        private bool isEnabledFavPr;
        private bool isEnabledAddPr;
        private bool isEnabledDelPr;
        private bool isEnabledSaveTask;
        private bool isEnabledFavTask;
        private bool isEnabledAddTask;
        private bool isEnabledDelTask;
        private bool isSelectedPrjct = false;
        private bool isSelectedTask = false;
        public string curUserName;
        public List<string> sysPMList;
        protected IDatabaseShowEmployeeData employeeDatabase;
        public AbstractViewFactory viewFactory;
        protected IView showEmployeeView;
        protected IView addEmployeeView;
        private bool isNewPrjct;
        private bool isNewTask;
        private string login;
        private IDatabaseAuthentication database;
        private Employee currentEmployee;
        private int cbSelPrSkill;
        private int cbSelPrOS;
        private int cbSelPrTypes;
        private int cbEmplPrjct;
        private int ISysPMList;
        private bool isEnableEditPr = true;
        private bool isEnableEditTask = true;
        private List<KeyValuePair<int, string>> listProjName;
        private List<KeyValuePair<int, string>> listProjDueDate;
        private List<KeyValuePair<int, string>> listProjCreatDate;
        private List<KeyValuePair<int, string>> listProjDuration;
        private List<KeyValuePair<int, string>> listProjCustomer;
        private List<KeyValuePair<int, string>> listProjLead;
        private List<KeyValuePair<int, string>> listProjStage;
        private List<KeyValuePair<int, string>> listProjObjective;
        private List<KeyValuePair<int, string>> listProjSkills;
        private List<KeyValuePair<int, string>> listProjTypes;
        private List<KeyValuePair<int, string>> listProjOS;
        private List<KeyValuePair<int, string>> listEmpl;
        
        private List<checkEl<KeyValuePair<int, string>>> developTeam;
        private string currentProjName;
        private DateTime currentProjDueDate; 
        private DateTime currentProjCreatDate;
        private string currentProjDesc;
        private KeyValuePair<int, string> currentProjDuration;
        private KeyValuePair<int, string> currentProjCustomer;
        private KeyValuePair<int, string> currentProjLead;
        private KeyValuePair<int, string> currentProjStage;
        private KeyValuePair<int, string> currentProjObjective;
        private List<checkEl<KeyValuePair<int, string>>> currentProjSkills;
        private List<checkEl<KeyValuePair<int, string>>> currentProjTypes;
        private List<checkEl<KeyValuePair<int, string>>> currentProjOS;
        private object selTreeElemPr;

        private List<checkEl<TeamworkDB.ProjectFile>> curProjFiles;

        private TeamworkDB.Project currentProject;
        private ObservableCollection<treeElem> listAllProj;
        private ObservableCollection<treeElem> listAllTask;
        private object selTreeElemTask;
        private string currentTask;
        private string сurTaskName;
        private DateTime curTaskCreationDate;
        private DateTime curTaskDueDate;
        private string curTaskCreator;
        private KeyValuePair<int, string> curTaskStatus;
        private KeyValuePair<int, string> curTaskPriority;
        private KeyValuePair<int, string> curTaskType;
        private string curTaskProject;
        private string curTaskDescription;
        private List<checkEl<TeamworkDB.TaskFile>> taskFiles;
        private List<TeamworkDB.TaskComment> listComments;
        private string commentText;

        private List<KeyValuePair<int, string>> listTaskStatus;
        private List<KeyValuePair<int, string>> listTaskPriority;
        private List<KeyValuePair<int, string>> listTaskType;
        
        #endregion

        public VM_TeamWork()
        {
            WsfConName ="";
            SysPMList = new List<string>();
            uiContext = SynchronizationContext.Current;
            site = new InstanceContext(this);
            proxyProgramMessenger = new ServiceProgramMessengerClient(site, "wsEndpointPM");
             vPrName = Visibility.Visible;
             vCustomerName = Visibility.Visible;
             vStage = Visibility.Visible;
             vDueDate = Visibility.Visible;
             vDuration = Visibility.Visible;
             vObjective = Visibility.Visible;
             vDescription = Visibility.Visible;
            vTaskName = Visibility.Visible;
            vDueDateTask = Visibility.Visible;
            vStatus = Visibility.Visible;
            vTaskType = Visibility.Visible;
            vTaskAssignees = Visibility.Visible;
            vTaskDescription = Visibility.Visible;
            vTaskPriority = Visibility.Visible;
            isEnabledSavePr = true;
            isEnabledFavPr = true;
            isEnabledAddPr = true;
            isEnabledDelPr = true;
            isEnabledSaveTask = true;
            isEnabledFavTask = true;
            isEnabledAddTask = true;
            isEnabledDelTask = true;
            isNewPrjct = false;
            isNewTask = false;
            IsNewProj = false;
            isSelectedPrjct = false;
            isSelectedTask = false;
            database = new DatabaseAuthenticationEntity();
            DevelopTeam = new List<checkEl<KeyValuePair<int, string>>>();
            ListProj = new List<treeElem>();
            ListTasks = new List<treeElem>();
            uiContext = SynchronizationContext.Current;
            site = new InstanceContext(this);
            selTreeElemPr = new object();
            listAllProj = new ObservableCollection<treeElem>();
            listFindProjects = new ObservableCollection<KeyValuePair<int, string>>();
            listFindTasks = new ObservableCollection<KeyValuePair<int, string>>();

            listCustomer = new ObservableCollection<checkEl<KeyValuePair<int, string>>>();
            listDuration = new ObservableCollection<checkEl<KeyValuePair<int, string>>>();
            listLead = new ObservableCollection<checkEl<KeyValuePair<int, string>>>();
            listOperationSystem = new ObservableCollection<checkEl<KeyValuePair<int, string>>>();
            listSkill = new ObservableCollection<checkEl<KeyValuePair<int, string>>>();
            listStage = new ObservableCollection<checkEl<KeyValuePair<int, string>>>();
            listType = new ObservableCollection<checkEl<KeyValuePair<int, string>>>();
            listObjective = new ObservableCollection<checkEl<KeyValuePair<int, string>>>();
            listAssignee = new ObservableCollection<checkEl<KeyValuePair<int, string>>>();
            listPriority = new ObservableCollection<checkEl<KeyValuePair<int, string>>>();
            listStatus = new ObservableCollection<checkEl<KeyValuePair<int, string>>>();
            listTypeTasks = new ObservableCollection<checkEl<KeyValuePair<int, string>>>();


            loadAllProjects();
            loadAllTasks();
            LoadSearchNorms();
        }
        ~VM_TeamWork()
        { CloseProgram(); }
        #region properties
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                currentEmployee = database.GetEmployeeByLogin(login);
                if (currentEmployee != null)
                {
                    loadAllProjects();
                    loadAllTasks();
                    EnterProgram();
                }
                OnPropertyChanged("Login");
            }
        }
        public List<KeyValuePair<int, string>> ListTaskAssignees
        {
            get
            {
                return listTaskAssignees;
            }
            set
            {
                listTaskAssignees = value;
                
                
                OnPropertyChanged("ListTaskAssignees");
            }
        }
        public int CBSelTaskAssignees
        {
            get
            {
                try
                {
                    return cbSelTaskAssignees;
                }
                catch { }
                return -1;
            }
            set
            {
                cbSelTaskAssignees = value;
                OnPropertyChanged("CBSelTaskAssignees");
            }
        }
        public List<checkEl<KeyValuePair<int, string>>> TaskAssignees
        {
            get
            {
                try
                {
                    return taskAssignees;
                }
                catch { }
                return null;
            }
            set
            {
                taskAssignees = value;
                if (taskAssignees!= null &&taskAssignees.Count > 0)
                    VTaskAssignees = Visibility.Collapsed;
                else
                    VTaskAssignees = Visibility.Visible;

                OnPropertyChanged("TaskAssignees");
            }
        }
        public Employee CurrentEmployee
        {
            get { return currentEmployee; }
            set
            {
                currentEmployee = value;
            }
        }


        public List<treeElem> ListProj
        {
            get
            {
                return listProj;
            }
            set
            {
                listProj = value;
                OnPropertyChanged("ListProj");
            }
        }

        public List<treeElem> ListTasks
        {
            get
            {
                return listTasks;
            }
            set
            {
                listTasks = value;
                OnPropertyChanged("ListTasks");
            }
        }
        public bool IsEnabledSaveTask
        {
            get
            {
                return isEnabledSaveTask;
            }
            set
            {
                isEnabledSaveTask = value;
                OnPropertyChanged("IsEnabledSaveTask");
            }
        }
        public bool IsEnabledFavTask
        {
            get
            {
                return isEnabledFavTask;
            }
            set
            {
                isEnabledFavTask = value;
                OnPropertyChanged("IsEnabledFavTask");
            }
        }
        public bool IsEnabledAddTask
        {
            get
            {
                return isEnabledAddTask;
            }
            set
            {
                isEnabledAddTask = value;
                OnPropertyChanged("IsEnabledAddTask");
            }
        }
        public bool IsEnabledDelTask
        {
            get
            {
                return isEnabledDelTask;
            }
            set
            {
                isEnabledDelTask = value;
                OnPropertyChanged("IsEnabledDelTask");
            }
        }
        public bool IsEnabledSavePr
        {
            get
            {
                return isEnabledSavePr;
            }
            set
            {
                isEnabledSavePr = value;
                OnPropertyChanged("IsEnabledSavePr");
            }
        }
        public bool IsEnabledFavPr
        {
            get
            {
                return isEnabledFavPr;
            }
            set
            {
                isEnabledFavPr = value;
                OnPropertyChanged("IsEnabledFavPr");
            }
        }
        public bool IsEnabledAddPr
        {
            get
            {
                return isEnabledAddPr;
            }
            set
            {
                isEnabledAddPr = value;
                OnPropertyChanged("IsEnabledAddPr");
            }
        }
        public bool IsEnabledDelPr
        {
            get
            {
                return isEnabledDelPr;
            }
            set
            {
                isEnabledDelPr = value;
                OnPropertyChanged("IsEnabledDelPr");
            }
        }

        public string CurUserName
        {
            get
            {
                if (login != null)
                    return login;
                else
                    return null;
            }
        }

        public int iSysPMList
        {
            get
            {
                return ISysPMList;
            }
            set
            {
                ISysPMList = value;
                OnPropertyChanged("iSysPMList");
            }
        }
        public List<string> SysPMList
        {
            get
            {
                return sysPMList;
            }
            set
            {
                sysPMList = value;
                iSysPMList = 0;
                OnPropertyChanged("SysPMList");
            }
        }
        public List<checkEl<KeyValuePair<int, string>>> DevelopTeam
        {
            get
            {
                return developTeam;
            }
            set
            {
                developTeam = value;
                OnPropertyChanged("DevelopTeam");
            }
        }
        public List<KeyValuePair<int, string>> ListTaskStatus
        {
            get
            {
                return listTaskStatus;
            }
            set
            {
                listTaskStatus = value;
                OnPropertyChanged("ListTaskStatus");
            }
        }
        public List<KeyValuePair<int, string>> ListTaskPriority
        {
            get
            {
                return listTaskPriority;
            }
            set
            {
                listTaskPriority = value;
                OnPropertyChanged("ListTaskPriority");
            }
        }
        public List<KeyValuePair<int, string>> ListTaskType
        {
            get
            {
                return listTaskType;
            }
            set
            {
                listTaskType = value;
                OnPropertyChanged("ListTaskType");
            }
        }
        
        
        public List<KeyValuePair<int, string>> ListProjName
        {
            get
            {
                return listProjName;
            }
            set
            {
                listProjName = value;
                OnPropertyChanged("ListProjName");
            }
        }
        public List<KeyValuePair<int, string>> ListProjDueDate
        {
            get
            {
                return listProjDueDate;
            }
            set
            {
                listProjDueDate = value;
                OnPropertyChanged("ListProjDueDate");
            }
        }
        public List<KeyValuePair<int, string>> ListProjCreatDate
        {
            get
            {
                return listProjCreatDate;
            }
            set
            {
                listProjCreatDate = value;
                OnPropertyChanged("ListProjCreatDate");
            }
        }
        public List<KeyValuePair<int, string>> ListProjDuration
        {
            get
            {
                return listProjDuration;
            }
            set
            {
                listProjDuration = value;
                OnPropertyChanged("ListProjDuration");
            }
        }
        public List<KeyValuePair<int, string>> ListProjCustomer
        {
            get
            {
                return listProjCustomer;
            }
            set
            {
                listProjCustomer = value;
               
                OnPropertyChanged("ListProjCustomer");
            }
        }
        public List<KeyValuePair<int, string>> ListProjLead
        {
            get
            {
                return listProjLead;
            }
            set
            {
                listProjLead = value;
                OnPropertyChanged("ListProjLead");
            }
        }
        public List<KeyValuePair<int, string>> ListProjStage
        {
            get
            {
                return listProjStage;
            }
            set
            {
                listProjStage = value;
                OnPropertyChanged("ListProjStage");
            }
        }
        
        public List<KeyValuePair<int, string>> ListProjObjective
        {
            get
            {
                return listProjObjective;
            }
            set
            {
                listProjObjective = value;
                OnPropertyChanged("ListProjObjective");
            }
        }
        public List<KeyValuePair<int, string>> ListProjSkills
        {
            get
            {
                return listProjSkills;
            }
            set
            {
                listProjSkills = value;
                OnPropertyChanged("ListProjSkills");
            }
        }
        public List<KeyValuePair<int, string>> ListProjTypes
        {
            get
            {
                return listProjTypes;
            }
            set
            {
                listProjTypes = value;
                OnPropertyChanged("ListProjTypes");
            }
        }
        public List<KeyValuePair<int, string>> ListProjOS
        {
            get
            {
                return listProjOS;
            }
            set
            {
                listProjOS = value;
                OnPropertyChanged("ListProjOS");
            }
        }
        public List<KeyValuePair<int, string>> ListEmpl
        {
            get
            {
                return listEmpl;
            }
            set
            {
                listEmpl = value;
                OnPropertyChanged("ListEmpl");
            }
        }
        
        public object SelTreeElemPr
        {
            get
            {
                return selTreeElemPr;
            }
            set
            {
                selTreeElemPr = value;
                treeElem temp = selTreeElemPr as treeElem;
                if (temp != null && !temp.isCatagory)
                { 
                    loadCurPrInfo();
                    loadAllTasks();
                    IsSelectedPrjct = true;
                    isEnabledSavePr = true;
                    isEnabledFavPr = true;
                    isEnabledAddPr = true;
                    isEnabledDelPr = true;
                }
                OnPropertyChanged("SelTreeElemPr");
            }
        }
        
        public object SelTreeElemTask
        {
            get
            {
                return selTreeElemTask;
            }
            set
            {
                selTreeElemTask = value;
                treeElem temp = selTreeElemTask as treeElem;
                if (temp != null && !temp.isCatagory)
                {
                    loadCurTsInfo();
                    isEnabledSaveTask = true;
                    isEnabledFavTask = true;
                    isEnabledAddTask = true;
                    isEnabledDelTask = true;
                }
                OnPropertyChanged("SelTreeElemTask");
            }
        }

        
        public bool IsSelectedPrjct
        {
            get
            {
                return isSelectedPrjct;
            }
            set
            {
                isSelectedPrjct = value;
                OnPropertyChanged("IsSelectedPrjct");
            }
        }
        
        public bool IsSelectedTask
        {
            get
            {
                return isSelectedTask;
            }
            set
            {
                isSelectedTask = value;
                OnPropertyChanged("IsSelectedTask");
            }
        }
   
        public Project CurrentProject
        {
            get
            {
                return currentProject;
            }
            set
            {
                currentProject = value;
                OnPropertyChanged("CurrentProject");
            }
        }
        public string CurProjName
        {
            get
            {
                try
                {
                    return currentProjName;
                }
                catch { }
                return null;
            }
            set
            {
                currentProjName = value;
                if(currentProjName.Length > 0)
                    VPrName = Visibility.Collapsed;
                else
                    VPrName = Visibility.Visible;
                OnPropertyChanged("CurProjName");
            }

        }
        
        public DateTime CurProjDueDate
        {
            get
            {
                return currentProjDueDate;
            }
            set
            {
                currentProjDueDate = value;
                if(currentProjDueDate> CurProjCreationDate)
                    VDueDate = Visibility.Collapsed;
                else
                    VDueDate = Visibility.Visible;
                OnPropertyChanged("CurProjDueDate");
            }

        }
            public DateTime CurProjCreationDate

        {
            get
            {
                
                return currentProjCreatDate;
            }
            set
            {

                currentProjCreatDate = value;
                OnPropertyChanged("CurProjCreationDate");
            }

        }
        
        public string CurProjDescription
        {
            get
            {
                try
                {
                    return currentProjDesc;
                }
                catch { }
                return null;
            }
            set
            {
                currentProjDesc = value;
                if (currentProjDesc.Length > 0)
                    VDescription = Visibility.Collapsed;
                else
                    VDescription = Visibility.Visible;
                OnPropertyChanged("CurProjDescription");
            }
        }
        
        public KeyValuePair<int, string> CurProjDuration
        {
            get
            {
                try
                {
                    return currentProjDuration;
                }
                catch { }
                return new KeyValuePair<int, string>();
            }
            set
            {
                currentProjDuration = value;
                if (currentProjDuration.Value!=null)
                    VDuration = Visibility.Collapsed;
                else
                    VDuration = Visibility.Visible;
                
                OnPropertyChanged("CurProjDuration");
            }
        }

        public KeyValuePair<int, string> CurProjCustomer
        {
            get
            {
                try
                {
                    return currentProjCustomer;
                }
                catch { }
                return new KeyValuePair<int, string>();
            }
            set
            {
                currentProjCustomer = value;
                if (currentProjCustomer.Value !=null)
                    VCustomerName = Visibility.Collapsed;
                else
                    VCustomerName = Visibility.Visible;
                OnPropertyChanged("CurProjCustomer");
            }
        }
        public KeyValuePair<int, string> CurProjLead
        {
            get
            {
                try
                {
                    return currentProjLead;
            }
                catch { }
                return new KeyValuePair<int, string>();
        }
            set
            {
                currentProjLead = value;
                OnPropertyChanged("CurProjLead");
            }
        }
        
        public KeyValuePair<int, string> CurProjStage
        {
            get
            {
                try
                {
                    return currentProjStage;
                }
                catch { }
                return new KeyValuePair<int, string>();
            }
            set
            {
                currentProjStage = value;
                if (currentProjStage.Value!=null)
                    VStage = Visibility.Collapsed;
                else
                    VStage = Visibility.Visible;
                
                OnPropertyChanged("CurProjStage");
            }
        }
        
        public KeyValuePair<int, string> CurProjObjective
        {
            get
            {
                try
                {
                    return currentProjObjective;
            }
                catch { }
                return new KeyValuePair<int, string>();
        }
            set
            {
                currentProjObjective = value;
                if (currentProjObjective.Value !=null)
                    VObjective = Visibility.Collapsed;
                else
                    VObjective = Visibility.Visible;
                OnPropertyChanged("CurProjObjective");
            }
        }

        public List<checkEl<ProjectFile>> CurProjFiles
        {
            get
            {
                try
                {
                    return curProjFiles;
    }
                catch { }
                return null;
}
            set
            {
                curProjFiles = value;
                OnPropertyChanged("CurProjFiles");
            }
        }
        
        public List<checkEl<KeyValuePair<int, string>>> CurProjSkills
        {
            get
            {
                try
                {
                    return currentProjSkills;
    }
                catch { }
                return null;
}
            set
            {
                currentProjSkills = value;
                OnPropertyChanged("CurProjSkills");
            }
        }
        public List<checkEl<KeyValuePair<int, string>>> CurProjTypes
        {
            get
            {
                try
                {
                    return currentProjTypes;
    }
                catch { }
                return null;
}
            set
            {
                currentProjTypes = value;
                OnPropertyChanged("CurProjTypes");
            }
        }
        public List<checkEl<KeyValuePair<int, string>>> CurProjOS
        {
            get
            {
                try
                {
                    return currentProjOS;
    }
                catch { }
                return null;
}
            set
            {
                currentProjOS = value;
                OnPropertyChanged("CurProjOS");
            }
        }

        public string СurrentTask
        {
            get
            {
                try
                {
                    return currentTask;
                }
                catch { }
                return null;
            }
            set
            {
                currentTask = value;
                OnPropertyChanged("СurrentTask");
            }
        }
        public string CurTaskName
        {
            get
            {
                try
                {
                    return сurTaskName;
                }
                catch { }
                return null;
            }
            set
            {
                сurTaskName = value;
                if (сurTaskName.Length > 0)
                    VTaskName = Visibility.Collapsed;
                else
                    VTaskName = Visibility.Visible;
                
                OnPropertyChanged("CurTaskName");
            }
        }
        public DateTime CurTaskCreationDate
        {
            get
            {
                return curTaskCreationDate;
            }
            set
            {
                curTaskCreationDate = value;
                OnPropertyChanged("CurTaskCreationDate");
            }
        }
        
        public int CBSelPrSkill
        {
            get
            {
                try
                {
                    return cbSelPrSkill;
                }
                catch { }
                return -1;
            }
            set
            {
                cbSelPrSkill = value;
                OnPropertyChanged("CBSelPrSkill");
            }
        }
        public int CBEmplPrjct
        {
            get
            {
                try
                {
                    return cbEmplPrjct;
                }
                catch { }
                return -1;
            }
            set
            {
                cbEmplPrjct = value;
                OnPropertyChanged("CBEmplPrjct");
            }
        }
        
        public int CBSelPrOS
        {
            get
            {
                try
                {
                    return cbSelPrOS;
                }
                catch { }
                return -1;
            }
            set
            {
                cbSelPrOS = value;
                OnPropertyChanged("CBSelPrOS");
            }
        }

        public int CBSelPrTypes
        {
            get
            {
                try
                {
                    return cbSelPrTypes;
                }
                catch { }
                return -1;
            }
            set
            {
                cbSelPrTypes = value;
                OnPropertyChanged("CBSelPrTypes");
            }
        }
        public DateTime CurTaskDueDate
        {
            get
            {
                return curTaskDueDate;
            }
            set
            {
                curTaskDueDate = value;
                if (curTaskDueDate > CurTaskCreationDate)
                    VDueDateTask = Visibility.Collapsed;
                else
                    VDueDateTask = Visibility.Visible;
                OnPropertyChanged("CurTaskDueDate");
            }
        }

        public string CurTaskCreator
        {
            get
            {
                try
                {
                    return curTaskCreator;
                }
                catch { }
                return null;
            }
            set
            {
                curTaskCreator = value;
                OnPropertyChanged("CurTaskCreator");
            }
        }
        public KeyValuePair<int,string> CurTaskStatus
        {
            get
            {
                try
                {
                    return curTaskStatus;
                }
                catch { }
                return new KeyValuePair<int,string>();
            }
            set
            {
                curTaskStatus = value;
                if (curTaskStatus.Value!=null)
                    VStatus = Visibility.Collapsed;
                else
                    VStatus = Visibility.Visible;
                OnPropertyChanged("CurTaskStatus");
            }
        }
        public KeyValuePair<int, string> CurTaskPriority
        {
            get
            {
                try
                {
                    return curTaskPriority;
                }
                catch { }
                return new KeyValuePair<int,string>();
            }
            set
            {
                curTaskPriority = value;
                if (curTaskPriority.Value != null)
                    VTaskPriority = Visibility.Collapsed;
                else
                    VTaskPriority = Visibility.Visible;
                
                OnPropertyChanged("CurTaskPriority");
            }
        }
        public KeyValuePair<int, string> CurTaskType
        {
            get
            {
                try
                {
                    return curTaskType;
                }
                catch { }
                return new KeyValuePair<int,string>();
            }
            set
            {
                curTaskType = value;
                if (curTaskType.Value != null)
                    VTaskType = Visibility.Collapsed;
                else
                    VTaskType = Visibility.Visible;
                OnPropertyChanged("CurTaskType");
            }
        }
        public string CurTaskProject
        {
            get
            {
                try
                {
                    return curTaskProject;
                }
                catch { }
                return null;
            }
            set
            {
                curTaskProject = value;
                OnPropertyChanged("CurTaskProject");
            }
        }
        public string CurTaskDescription
        {
            get
            {
                try
                {
                    return curTaskDescription;
                }
                catch { }
                return null;
            }
            set
            {
                curTaskDescription = value;
                if (curTaskDescription.Length>0)
                    VTaskDescription = Visibility.Collapsed;
                else
                    VTaskDescription = Visibility.Visible;
                OnPropertyChanged("CurTaskDescription");
            }
        }
        bool isNewProj;
        public bool IsNewProj
        {
            get
            {
                return !isNewProj;
            }
            set
            {
                isNewProj = value;
                OnPropertyChanged("IsNewProj");

            }
        }
        public bool IsNewTask
        {
            get
            {
                return !isNewTask;
            }
            set
            {
                isNewTask = value;
                OnPropertyChanged("IsNewTask");

            }
        }
        public List<checkEl<TeamworkDB.TaskFile>> TaskFiles
        {
            get
            {
                try
                {
                    return taskFiles;
                }
                catch { }
                return null;
            }
            set
            {
                taskFiles = value;
                OnPropertyChanged("TaskFiles");
            }
        }
        public List<TeamworkDB.TaskComment> ListComments
        {
            get
            {
                try
                {
                    return listComments;
                }
                catch { }
                return null;
            }
            set
            {
                listComments = value;
                OnPropertyChanged("ListComments");
            }
        }
        public string CommentText
        {
            get
            {

                    return commentText;
            }
            set
            {
                commentText = value;
                OnPropertyChanged("CommentText");
            }
        }

        #endregion


        #region functions
        #region load func
        void loadAllListPrInfo()
        {
            try
            {
                ListProjName = new List<KeyValuePair<int, string>>(F_Projects.GetAllProjectsName());
                ListProjObjective = new List<KeyValuePair<int, string>>(F_Projects.GetAllObjectives());
                ListProjDuration = new List<KeyValuePair<int, string>>(F_Projects.GetAllDurations());
                ListProjCustomer = new List<KeyValuePair<int, string>>(F_Projects.GetAllCustomers());
                ListProjLead = new List<KeyValuePair<int, string>>(F_Staff.GetTeamLeaders());
                ListProjStage = new List<KeyValuePair<int, string>>(F_Projects.GetAllStages());
                ListProjSkills = new List<KeyValuePair<int, string>>(F_Projects.GetAllSkills());
                ListProjTypes = new List<KeyValuePair<int, string>>(F_Projects.GetAllTypes());
                ListProjOS = new List<KeyValuePair<int, string>>(F_Projects.GetAllOSs());
                ListEmpl = new List<KeyValuePair<int, string>>(F_Staff.GetUsualEmployees());
                ListProjOS.Sort();
                ListProjTypes.Sort();
                ListProjSkills.Sort();
            }
            catch (Exception ex)
            {
                SysPMList.Add(ex.Message);
            }

        }
        void loadCurPrInfo()
        {
            try
            {
                treeElem tempPr = selTreeElemPr as treeElem;
                if (tempPr == null || tempPr.isCatagory)
                    return;
                currentProject = F_Projects.GetProject(tempPr.id);
                List<KeyValuePair<int, string>> tempClass;
                loadAllListPrInfo();

                CurProjName = tempPr.Name;
                CurProjCreationDate = F_Projects.GetProjectCreationDate(tempPr.id);
                CurProjDueDate = F_Projects.GetProjectDueDate(tempPr.id);
                
                CurProjDescription = F_Projects.GetProjectDescription(tempPr.id);
                CurProjDuration = F_Projects.GetProjectDuration(tempPr.id);
                CurProjCustomer = F_Projects.GetProjectCustomer(tempPr.id);

                CurProjStage = F_Projects.GetProjectStage(tempPr.id);
                CurProjObjective = F_Projects.GetProjectObjective(tempPr.id);
                CurProjFiles = new List<checkEl<ProjectFile>>();
                List<TeamworkDB.ProjectFile> tempFiels = F_Projects.GetProjectFiles(tempPr.id);
                foreach (var temp in tempFiels)
                    CurProjFiles.Add(new checkEl<TeamworkDB.ProjectFile>(temp));
                CurProjFiles = new List<checkEl<ProjectFile>>(CurProjFiles);
                CurProjSkills = new List<checkEl<KeyValuePair<int, string>>>();
                tempClass = F_Projects.GetProjectSkills(tempPr.id);
                foreach (var temp in tempClass)
                {
                    CurProjSkills.Add(new checkEl<KeyValuePair<int, string>>(temp));
                    ListProjSkills.Remove(temp);
                }
                CurProjSkills = new List<checkEl<KeyValuePair<int, string>>>(CurProjSkills);
                CurProjTypes = new List<checkEl<KeyValuePair<int, string>>>();
                tempClass = F_Projects.GetProjectTypes(tempPr.id);
                foreach (var temp in tempClass)
                {
                    CurProjTypes.Add(new checkEl<KeyValuePair<int, string>>(temp));
                    ListProjTypes.Remove(temp);
                }
                CurProjTypes = new List<checkEl<KeyValuePair<int, string>>>(CurProjTypes);
                CurProjOS = new List<checkEl<KeyValuePair<int, string>>>();
                tempClass = F_Projects.GetProjectOSs(tempPr.id);
                foreach (var temp in tempClass)
                {
                    CurProjOS.Add(new checkEl<KeyValuePair<int, string>>(temp));
                    ListProjOS.Remove(temp);
                }
                CurProjOS = new List<checkEl<KeyValuePair<int, string>>>(CurProjOS);
                DevelopTeam = new List<checkEl<KeyValuePair<int, string>>>();
                var tempClasskp = F_Staff.GetProjectEmployees(tempPr.id);
                foreach (var temp in tempClasskp)
                {
                    DevelopTeam.Add(new checkEl<KeyValuePair<int, string>>(temp));
                    ListEmpl.Remove(temp);
                }
                DevelopTeam = new List<checkEl<KeyValuePair<int, string>>>(DevelopTeam);
                CurProjLead = F_Staff.GetProjectLead(currentProject.Id);
            }
            catch (Exception ex)
            {
                SysPMList.Add(ex.Message);
            }
        }
        void loadCurTsInfo()
        {
            try
            {
                treeElem tempPr = selTreeElemPr as treeElem;
                if (tempPr == null || tempPr.isCatagory)
                    return;
                treeElem tempTs = SelTreeElemTask as treeElem;
                if (tempTs == null || tempTs.isCatagory)
                    return;
                CurrentIssueID = tempTs.id;
                CurTaskName = tempTs.Name;
                CurTaskCreationDate = F_Task.GetIssueCreationDate(tempPr.id, tempTs.id);
                CurTaskDueDate = F_Task.GetIssueDueDate(tempPr.id, tempTs.id);
                CurTaskCreator = F_Task.GetIssueCreatorName(tempPr.id, tempTs.id);
                CurTaskStatus = F_Task.GetIssueStatusNameWithId(tempPr.id, tempTs.id);
                CurTaskPriority = F_Task.GetIssuePriorityNameWithId(tempPr.id, tempTs.id);
                CurTaskType = F_Task.GetIssueTypeNameWithId(tempPr.id, tempTs.id);
                CurTaskProject = tempPr.Name;
                CurTaskDescription = F_Task.GetIssueDescription(tempPr.id, tempTs.id);
                ListTaskAssignees = new List<KeyValuePair<int, string>>(F_Staff.GetProjectAllEmployees(tempPr.id));
                TaskAssignees = new List<checkEl<KeyValuePair<int, string>>>();
                List<KeyValuePair<int, string>> tempClass = F_Task.GetIssueAssigneesNameWithId(tempPr.id, tempTs.id);
                foreach (var temp in tempClass)
                    TaskAssignees.Add(new checkEl<KeyValuePair<int, string>>(temp));
                TaskAssignees = new List<checkEl<KeyValuePair<int, string>>>(TaskAssignees);

                TaskFiles = new List<checkEl<TaskFile>>();
                List<TeamworkDB.TaskFile> tempFiels = F_Task.GetIssueFiles(tempPr.id, tempTs.id);
                foreach (var temp in tempFiels)
                    TaskFiles.Add(new checkEl<TeamworkDB.TaskFile>(temp));
                TaskFiles = new List<checkEl<TaskFile>>(TaskFiles);
                ListComments = F_Task.GetAllCommentsObjects(tempPr.id, tempTs.id);

                IsNewTask = false;
            }
            catch (Exception ex)
            {
                SysPMList.Add(ex.Message);
            }
        }
        public void uploadAllProjects()
        {
            try
            {
                if (currentEmployee == null)
                    return;
                ListProj[0].subEl = new ObservableCollection<TeamWork.treeElem>();
                ListProj[1].subEl = new ObservableCollection<TeamWork.treeElem>();
                ListProj[2].subEl = new ObservableCollection<TeamWork.treeElem>();
                List<KeyValuePair<int, string>> tempPrj = F_Projects.GetAllProjectsNameWithId();
                foreach (var temp in tempPrj)
                {
                    string imPath = F_Projects.GetProjectStage(temp.Value);
                    ListProj[0].subEl.Add(new treeElem(temp.Key, temp.Value, new ObservableCollection<treeElem>()));
                }
                tempPrj = F_Projects.GetUserFavouriteProjectsNameWithId(currentEmployee.Id);
                foreach (var temp in tempPrj)
                {
                    string imPath = F_Projects.GetProjectStage(temp.Value);
                    ListProj[1].subEl.Add(new treeElem(temp.Key, temp.Value, new ObservableCollection<treeElem>()));
                }
                tempPrj = F_Projects.GetUserProjectsNameWithId(currentEmployee.Id);
                foreach (var temp in tempPrj)
                {
                    string imPath = F_Projects.GetProjectStage(temp.Value);
                    ListProj[2].subEl.Add(new treeElem(temp.Key, temp.Value, new ObservableCollection<treeElem>()));
                }
                ListProj = new List<treeElem>(ListProj);
            }
            catch (Exception ex)
            {
                SysPMList.Add(ex.Message);
            }
        }
        public void loadAllProjects()
        {
            try
            {
                if (currentEmployee == null)
                    return;

                ListProj.Clear();
                ListProj.Add(new treeElem("All", new ObservableCollection<treeElem>(), true,true));
                List<KeyValuePair<int, string>> tempPrj = F_Projects.GetAllProjectsNameWithId();
                foreach (var temp in tempPrj)
                {
                    string imPath = F_Projects.GetProjectStage(temp.Value);
                    ListProj[0].subEl.Add(new treeElem(temp.Key, temp.Value, new ObservableCollection<treeElem>()));
                }
                ListProj.Add(new treeElem("Favorite", new ObservableCollection<treeElem>(), true, true));
                tempPrj = F_Projects.GetUserFavouriteProjectsNameWithId(currentEmployee.Id);
                foreach (var temp in tempPrj)
                {
                    string imPath = F_Projects.GetProjectStage(temp.Value);
                    ListProj[1].subEl.Add(new treeElem(temp.Key, temp.Value, new ObservableCollection<treeElem>()));
                }
                ListProj.Add(new treeElem("Your", new ObservableCollection<treeElem>(), true, true));
                tempPrj = F_Projects.GetUserProjectsNameWithId(currentEmployee.Id);
                foreach (var temp in tempPrj)
                {
                    string imPath = F_Projects.GetProjectStage(temp.Value);
                    ListProj[2].subEl.Add(new treeElem(temp.Key, temp.Value, new ObservableCollection<treeElem>()));
                }
                ListProj = new List<treeElem>(ListProj);
            }
            catch (Exception ex)
            {
                SysPMList.Add(ex.Message);
            }
        }
        public void loadAllTasks()
        {
            if (currentEmployee == null || CurrentProject == null)
                return;
            ListTaskStatus = F_Task.GetAllStatuses();
            ListTaskPriority = F_Task.GetAllPriorityes();
            ListTaskType = F_Task.GetAllTaskTypes();

            ListTasks.Clear();
            ListTasks.Add(new treeElem("All", new ObservableCollection<treeElem>(), true, true));
            ListTasks.Add(new treeElem("Your", new ObservableCollection<treeElem>(), true, true));
            List<KeyValuePair<int, string>> tempTasks = F_Task.GetMainIssuesNameWithId(CurrentProject.Id);
            var AllStatus = F_Task.GetAllStatuses();

            if (tempTasks != null)
            {
                foreach (var tempst in AllStatus)
                {
                    treeElem tempTree = new treeElem("Status: " + tempst.Value, new ObservableCollection<treeElem>(), true);
                    foreach (var temp in tempTasks)
                    {

                        string Status = F_Task.GetIssueStatus(CurrentProject.Id, temp.Key);

                        if (tempst.Value == Status)
                        {
                            tempTree.subEl.Add(new treeElem(temp.Key, temp.Value, new ObservableCollection<treeElem>()));
                        }

                    }
                    ListTasks[0].subEl.Add(tempTree);
                }
            }

            tempTasks = F_Task.GetUserMainIssuesNameWithId(CurrentProject.Id, CurrentEmployee.Id);
            if (tempTasks == null)
            {
                foreach (var tempst in AllStatus)
                {
                    treeElem tempTree = new treeElem("Status: " + tempst.Value, new ObservableCollection<treeElem>(), true);
                    foreach (var temp in tempTasks)
                    {
                        string Status = F_Task.GetIssueStatus(CurrentProject.Id, temp.Key);
                        if (tempst.Value == Status)
                        {
                            tempTree.subEl.Add(new treeElem(temp.Key, temp.Value, new ObservableCollection<treeElem>()));
                        }
                    }
                    ListTasks[1].subEl.Add(tempTree);
                }
            }
            ListTasks = new List<treeElem>(ListTasks);
            IsNewTask = false;
        }
        public void uploadAllTasks()
        {
            if (currentEmployee == null || CurrentProject == null)
                return;
            ListTaskStatus = F_Task.GetAllStatuses();
            ListTaskPriority = F_Task.GetAllPriorityes();
            ListTaskType = F_Task.GetAllTaskTypes();
            ListTasks[0].subEl=new ObservableCollection<TeamWork.treeElem> ();
            ListTasks[1].subEl = new ObservableCollection<TeamWork.treeElem>();
            List<KeyValuePair<int, string>> tempTasks = F_Task.GetMainIssuesNameWithId(CurrentProject.Id);
            var AllStatus = F_Task.GetAllStatuses();

            if (tempTasks != null)
            {
                foreach (var tempst in AllStatus)
                {
                    treeElem tempTree = new treeElem("Status: " + tempst.Value, new ObservableCollection<treeElem>(), true);
                    foreach (var temp in tempTasks)
                    {

                        string Status = F_Task.GetIssueStatus(CurrentProject.Id, temp.Key);

                        if (tempst.Value == Status)
                        {
                            tempTree.subEl.Add(new treeElem(temp.Key, temp.Value, new ObservableCollection<treeElem>()));
                        }

                    }
                    ListTasks[0].subEl.Add(tempTree);
                }
            }

            tempTasks = F_Task.GetUserMainIssuesNameWithId(CurrentProject.Id, CurrentEmployee.Id);
            if (tempTasks == null)
            {
                foreach (var tempst in AllStatus)
                {
                    treeElem tempTree = new treeElem("Status: " + tempst.Value, new ObservableCollection<treeElem>(), true);
                    foreach (var temp in tempTasks)
                    {
                        string Status = F_Task.GetIssueStatus(CurrentProject.Id, temp.Key);
                        if (tempst.Value == Status)
                        {
                            tempTree.subEl.Add(new treeElem(temp.Key, temp.Value, new ObservableCollection<treeElem>()));
                        }
                    }
                    ListTasks[1].subEl.Add(tempTree);
                }
            }
            ListTasks = new List<treeElem>(ListTasks);
            IsNewTask = false;
        }
        #endregion
        void messBoxOk(string text, string header)
        {
            MessageBox_OK MB_OK = new MessageBox_OK();
            VM_CustomMessageBox_OK VM_OK = new VM_CustomMessageBox_OK(header, text);
            MB_OK.DataContext = VM_OK;
            if ((bool)MB_OK.ShowDialog()) { }
        }
        bool messBoxYesNo(string text, string header)
        {
            MessageBox_YesNo MB_YesNo = new MessageBox_YesNo();
            VM_CustomMessageBox_YesNo VM_YesNo = new VM_CustomMessageBox_YesNo(header, text);
            MB_YesNo.DataContext = VM_YesNo;

            if ((bool)MB_YesNo.ShowDialog())
                return true;
            else
                return false;
        }
        private async void EnterProgram()
        {
            await Task.Run(() =>
            {
                try
                {
                    WsfConName = CurrentEmployee.Name + " " + CurrentEmployee.Surname + DateTime.Now.Ticks;
                    proxyProgramMessenger.ConnectSPMAsync(WsfConName);

                }
                catch (Exception ex)
                {
                    uiContext.Send(d => SysPMList.Add("System " + ex.Message), null);
                }
            });
        }
        private async void CloseProgram()
        {
            await Task.Run(() =>
            {
                try
                {
                    proxyProgramMessenger.DisconnectSPMAsync(WsfConName);

                }
                catch (Exception ex)
                {
                    uiContext.Send(d => SysPMList.Add("System " + ex.Message), null);
                }
            });
        }
        public void sendSystemMessageCB(string mes)
        {
            uiContext.Send(d => SysPMList.Add(mes + " - Time: " + DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString()), null);
            iSysPMList = 0;
        }
        public void UpdateEmployeesCB()
        {
            uiContext.Send(d => SysPMList.Add("Information about employees was updated - Time: " + DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString()), null);
            iSysPMList = 0;
        }
        public void UpdateProjectsCB()
        {
            uiContext.Send(d => SysPMList.Add("Suppor information about projects was updated - Time: " + DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString()), null);
            loadAllProjects();
            iSysPMList = 0;
        }
        public void UpdateTasksCB()
        {
            uiContext.Send(d => SysPMList.Add("Suppor information about tasks was updated - Time: " + DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString()), null);
            loadAllTasks();
            iSysPMList = 0;
        }
        #endregion
        
        #region Commands
        #region Menu Commands
        private DelegateCommand MAddEmployee;
        public ICommand MAddEmployee_Click
        {
            get
            {
                if (MAddEmployee == null)
                {
                    MAddEmployee = new DelegateCommand(param => AddEmployee(), param=> true);
                }
                return MAddEmployee;
            }
        }

        void AddEmployee()
        {
            viewFactory = new InputEmployeeDataViewFactory();
            addEmployeeView = viewFactory.CreateView();

            InputEmployeeDataViewModel inputEmployeeDataViewModel = new InputEmployeeDataViewModel();
            inputEmployeeDataViewModel.LoadData();
            addEmployeeView.Data = inputEmployeeDataViewModel;
            inputEmployeeDataViewModel.CurrentWindow = addEmployeeView;
            addEmployeeView.ShowView();
        }

        private DelegateCommand MShwAllEmployee;
        public ICommand MShwAllEmployee_Click
        {
            get
            {
                if (MShwAllEmployee == null)
                {
                    MShwAllEmployee = new DelegateCommand(param => ShowAllEmployee(), param => true);
                }
                return MShwAllEmployee;
            }
        }
        void ShowAllEmployee()
        {
            try
            {
                viewFactory = new ShowEmployeeDataViewFactory();
                showEmployeeView = viewFactory.CreateView();

                ShowEmployeeDataViewModel showEmployeeDataViewModel = new ShowEmployeeDataViewModel();
                showEmployeeView.Data = showEmployeeDataViewModel;
                showEmployeeView.ShowView();
            }
            catch (Exception error)
            {
                messBoxOk(error.Message,"Error");
            }
        }

        private DelegateCommand MEditPrjSupp;
        public ICommand MEditPrjSupp_Click
        {
            get
            {
                if (MEditPrjSupp == null)
                {
                    MEditPrjSupp = new DelegateCommand(param => EditProjectSuppInfo(), param => true);
                }
                return MEditPrjSupp;
            }
        }
        void EditProjectSuppInfo()
        {
            V_Editor_Projects WinEdPr = new V_Editor_Projects();
            VM_Editor_Projects VM_EdPr = new VM_Editor_Projects();
            WinEdPr.DataContext = VM_EdPr;
            WinEdPr.ShowDialog();
            if (VM_EdPr.IsChanged)
            {
                loadAllProjects();
                SysPMList.Add("Suppor information about projects was updated - Time: " + DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString());
            }

        }
        private DelegateCommand MEditTskSupp;
        public ICommand MEditTskSupp_Click
        {
            get
            {
                if (MEditTskSupp == null)
                {
                    MEditTskSupp = new DelegateCommand(param => EditTaskSuppInfo(), param => true);
                }
                return MEditTskSupp;
            }
        }
        void EditTaskSuppInfo()
        {
            V_Editor_Task WinEdT = new V_Editor_Task();
            VM_Editor_Tasks VM_EdTask = new VM_Editor_Tasks();
            WinEdT.DataContext = VM_EdTask;
            WinEdT.ShowDialog();
            if (VM_EdTask.IsChanged)
            {
                loadAllTasks();
                SysPMList.Add("Suppor information about tasks was updated - Time: " + DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString());
            }
        }
        #endregion

        
        private DelegateCommand ButtonAddPrFilesClick;
        public ICommand BAddPrFiles_Click
        {
            get
            {
                if (ButtonAddPrFilesClick == null)
                {
                    ButtonAddPrFilesClick = new DelegateCommand(param => this.AddPrFiles(), param => true);
                }
                return ButtonAddPrFilesClick;
            }
        }
        void AddPrFiles()
        {
            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.Title = "Open File";
                openFileDialog.Filter = "png files (*.png)|*.png";

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string path = openFileDialog.FileName;
                    var fileInfo = new FileInfo(path);

                    byte[] bytes = File.ReadAllBytes(openFileDialog.FileName);

                    FileTransferManager transfer = new FileTransferManager();
                    FileTransferResponse success = transfer.SendFileToServer(fileInfo.Name, bytes, CurrentProject.Id);

                    string result = F_Projects.AddNewFileToProject(currentProject.Id, fileInfo.Name, fileInfo.Length, fileInfo.Extension, path);
                    if (result == "Added")
                    {
                        messBoxOk("File loaded sucessfully", "File Loaded");
                    }
                    CurProjFiles = new List<checkEl<ProjectFile>>();
                    List<TeamworkDB.ProjectFile> tempFiels = F_Projects.GetProjectFiles(CurrentProject.Id);
                    foreach (var temp in tempFiels)
                        CurProjFiles.Add(new checkEl<TeamworkDB.ProjectFile>(temp));
                    CurProjFiles = new List<checkEl<ProjectFile>>(CurProjFiles);
                }
            }
        }

        private DelegateCommand ButtonDelPrFilesClick;
        public ICommand BDelPrFiles_Click
        {
            get
            {
                if (ButtonDelPrFilesClick == null)
                {
                    ButtonDelPrFilesClick = new DelegateCommand(param => this.DelPrFiles(), param => true);
                }
                return ButtonDelPrFilesClick;
            }
        }
        void DelPrFiles()
        {
            for (int i = CurProjFiles.Count - 1; i >= 0; i--)
            {
                if (CurProjFiles[i].isCheck)
                {
                    string fileName = CurProjFiles[i].chClass.Name;
                    FileTransferManager transfer = new FileTransferManager();
                    FileTransferResponse success = transfer.DeleteFileFromServer(fileName, currentProject.Id);
                    string result = string.Empty;
                    if (success.Message == "File was Deleted")
                    {
                        CurProjFiles.Remove(CurProjFiles[i]);
                        result = F_Projects.DeleteFile(currentProject.Id, fileName);
                        if (result == "Deleted")
                        {
                            messBoxOk("File deleted sucessfully", "File Deleted");
                        }
                    }
                }
            }
            CurProjFiles = new List<checkEl<TeamworkDB.ProjectFile>>(CurProjFiles);
        }

        private DelegateCommand ButtonDownPrFiles_Click;
                public ICommand BDownPrFiles_Click
        {
            get
            {
                if (ButtonDownPrFiles_Click == null)
                {
                    ButtonDownPrFiles_Click = new DelegateCommand(param => this.DownPrFiles(), param => true);
                }
                return ButtonDownPrFiles_Click;
            }
        }
        void DownPrFiles()
        {
            for (int i = CurProjFiles.Count - 1; i >= 0; i--)
            {
                if (CurProjFiles[i].isCheck)
                {


                    using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
                    {
                        System.Windows.Forms.DialogResult result = fbd.ShowDialog();

                        if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                        {
                            string fileName = CurProjFiles[i].chClass.Name;
                            FileTransferManager transfer = new FileTransferManager();
                            FileTransferResponse success = transfer.GetFile(fileName, currentProject.Id, -1, fbd.SelectedPath + "\\" + fileName);

                            if (success.ResponseStatus == "Successful")
                            {
                                messBoxOk("File downloaded sucessfully", "File Downloaded");

                            }
                        }
                    }
                }
            }
        }
        private DelegateCommand ButtonAddTaskFiles_Click;
        public ICommand BAddTaskFiles_Click
        {
            get
            {
                if (ButtonAddTaskFiles_Click == null)
                {
                    ButtonAddTaskFiles_Click = new DelegateCommand(param => this.AddTaskFiles(), param => true);
                }
                return ButtonAddTaskFiles_Click;
            }
        }
        void AddTaskFiles()
        {
            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.Title = "Open File";
                openFileDialog.Filter = "png files (*.png)|*.png";

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string path = openFileDialog.FileName;
                    var fileInfo = new FileInfo(path);

                    byte[] bytes = File.ReadAllBytes(openFileDialog.FileName);

                    FileTransferManager transfer = new FileTransferManager();
                    FileTransferResponse success = transfer.SendFileToServer(fileInfo.Name, bytes, CurrentProject.Id, CurrentIssueID);

                    string result = F_Task.AddFileToIssue(currentProject.Id, CurrentIssueID, fileInfo.Name, fileInfo.Length, fileInfo.Extension, path);
                    if (success.ResponseStatus == "Sucessfull" || result == "Added")
                    {
                        messBoxOk("File loaded sucessfully", "File Loaded");
                        TaskFiles = new List<checkEl<TaskFile>>();
                        List<TeamworkDB.TaskFile> tempFiels = F_Task.GetIssueFiles(currentProject.Id, CurrentIssueID);
                        foreach (var temp in tempFiels)
                            TaskFiles.Add(new checkEl<TeamworkDB.TaskFile>(temp));
                        TaskFiles = new List<checkEl<TaskFile>>(TaskFiles);
                    }
                }
            }
        }

        private DelegateCommand ButtonDelTaskFiles_Click;
                public ICommand BDelTaskFiles_Click
        {
            get
            {
                if (ButtonDelTaskFiles_Click == null)
                {
                    ButtonDelTaskFiles_Click = new DelegateCommand(param => this.DelTaskFiles(), param => true);
                }
                return ButtonDelTaskFiles_Click;
            }
        }
        void DelTaskFiles()
        {
            for (int i = TaskFiles.Count - 1; i >= 0; i--)
            {
                if (TaskFiles[i].isCheck)
                {
                    string fileName = TaskFiles[i].chClass.Name;
                    FileTransferManager transfer = new FileTransferManager();
                    FileTransferResponse success = transfer.DeleteFileFromServer(fileName, currentProject.Id, CurrentIssueID);
                    string result = string.Empty;
                    if (success.Message == "File was Deleted")
                    {
                        TaskFiles.Remove(TaskFiles[i]);
                        result = F_Task.DeleteFile(currentProject.Id,CurrentIssueID, fileName);
                        if (result == "Deleted")
                        {
                            messBoxOk("File deleted sucessfully", "File Deleted");
                            TaskFiles = new List<checkEl<TaskFile>>();
                            List<TeamworkDB.TaskFile> tempFiels = F_Task.GetIssueFiles(currentProject.Id, CurrentIssueID);
                            foreach (var temp in tempFiels)
                                TaskFiles.Add(new checkEl<TeamworkDB.TaskFile>(temp));
                            TaskFiles = new List<checkEl<TaskFile>>(TaskFiles);
                        }

                    }
                }
            }
        }
        private DelegateCommand ButtonDownTaskFiles_Click;
                public ICommand BDownTaskFiles_Click
        {
            get
            {
                if (ButtonDownTaskFiles_Click == null)
                {
                    ButtonDownTaskFiles_Click = new DelegateCommand(param => this.DownTaskFiles(), param => true);
                }
                return ButtonDownTaskFiles_Click;
            }
        }
        void DownTaskFiles()
        {
            for (int i = TaskFiles.Count - 1; i >= 0; i--)
            {
                if (TaskFiles[i].isCheck)
                {


                    using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
                    {
                        System.Windows.Forms.DialogResult result = fbd.ShowDialog();

                        if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                        {
                            string fileName = TaskFiles[i].chClass.Name;
                            FileTransferManager transfer = new FileTransferManager();
                            FileTransferResponse success = transfer.GetFile(fileName, currentProject.Id, CurrentIssueID, fbd.SelectedPath + "\\" + fileName);

                            if (success.ResponseStatus == "Successful")
                            {
                                messBoxOk("File downloaded sucessfully", "File Downloaded");
                            }
                        }
                    }
                }
            }
        }
        
            
            
        private DelegateCommand ButtonSaveProjClick;
        public ICommand BSaveProj_Click
        {
            get
            {
                if (ButtonSaveProjClick == null)
                {
                    ButtonSaveProjClick = new DelegateCommand(param => this.SaveAllChProj(), param => IsEnblSavePr());
                }
                return ButtonSaveProjClick;
            }
        }
        bool IsEnblSavePr()
        {
            return (!IsProjectNameAvailable() &&!IsCustomerNameAvailable() &&!IsStageAvailable()
                && !IsDueDateAvailable() && !IsDurationAvailable() && !IsObjectiveAvailable() && !IsDescriptionAvailable());
        }
        void SaveAllChProj()
        {
            if (!F_Projects.IsProjectInDb(CurProjName))
            {
                if (messBoxYesNo("Do you want to save new project?", "Save new project"))
                {
                   string mess = F_Projects.AddNewProject(CurProjName, CurProjCustomer.Value, CurProjDuration.Value, CurProjStage.Value, CurProjObjective.Value, CurProjDueDate, CurProjDescription);

                    if (mess == null)
                    {
                        messBoxOk("Project saved successful.", "Save project");
                        IsNewProj = false;
                    }
                    else
                        messBoxOk(mess, "Save project");
                }
              }
            else
            {
               if (messBoxYesNo("Do you want to edit this project?", "Edit project"))
                {
                    EditPrName();
                    EditPrStage();
                    EditPrDueDate();
                    EditPrObjective();
                    EditPrOS();
                    EditPrDescription();
                    EditPrSkills();
                    EditPrLead();
                    EditPrDuration();
                    EditPrCustomer();
                    EditPrTypes();
                    EditPrEmployees();
                    messBoxOk("Project edit successful.", "Edit project");

                }
                
            }
            
            loadAllProjects();

        }
        private async void UpdateProgramSer()
        {
            await Task.Run(() =>
            {
                try
                {
                    proxyProgramMessenger.UpdateProjectsSPMAsync();

                }
                catch (Exception ex)
                {
                    uiContext.Send(d => SysPMList.Add("System " + ex.Message), null);
                    iSysPMList = 0;
                }
            });
        }
        public void EditPrEmployees()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            var allPrEmployees = F_Staff.GetProjectEmployees(CurrentProject.Id);
            for (int i = 0; i < DevelopTeam.Count; i++)
            {
                if (allPrEmployees.IndexOf(DevelopTeam[i].chClass) == -1)
                    F_Staff.AddEmployeeToProject(CurrentProject.Id, DevelopTeam[i].chClass.Key);
            }
        }
        public void EditPrLead()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            F_Staff.AddEmployeeToProject(CurrentProject.Id, CurProjLead.Key);
            F_Staff.ChangeProjectLead(CurProjLead.Key, tempPr.id);
            
        }

        private DelegateCommand ButtonSaveTaskClick;
        public ICommand BSaveTask_Click
        {
            get
            {
                if (ButtonSaveTaskClick == null)
                {
                    ButtonSaveTaskClick = new DelegateCommand(param => this.SaveAllChTask(),param => isEnblSaveTask());
                }
                return ButtonSaveTaskClick;
            }
        }
        bool isEnblSaveTask()
        {
            return (IsSelTask() && !IsTaskNameAvailable() && !IsDueDateTaskAvailable() && 
                !IsStatusAvailable() &&!IsTaskPriorityAvailable() && 
                !IsTaskTypeAvailable() && !IsTaskAssigneesAvailable() && !IsTaskDescriptionAvailable());

        }
        bool IsSelTask()
        {
            treeElem tempPr = SelTreeElemPr as treeElem;
            treeElem tempTs = SelTreeElemTask as treeElem;
            return (tempPr != null && tempTs != null&& !tempPr.isCatagory && !tempTs.isCatagory) 
                ||(tempPr != null && isNewTask && !tempPr.isCatagory);
        }
        void SaveAllChTask()
        {
            if (isNewTask)
            {
                if (messBoxYesNo("Do you want to save new task?", "Save new task"))
                {
                    string mess = F_Task.AddNewIssue(CurrentProject.Id, CurTaskName, CurTaskStatus.Key, CurTaskPriority.Key,
                    CurrentEmployee.Id, CurTaskType.Key, CurTaskDueDate, TaskAssignees.Select(x => x.chClass.Key).ToList(),CurTaskDescription);

                    if (mess == null)
                    {
                        messBoxOk("Task saved successful.", "Save new task");
                        IsNewTask = false;
                    }
                    else
                        messBoxOk(mess, "Save new task");
                }

                }
            else
            {
                if (messBoxYesNo("Do you want to edit this task?", "Edit task"))
                {
                    EditTaskAssignees();
                    EditTaskDescription();
                    EditTaskDueDate();
                    EditTaskName();
                    EditTaskPriority();
                    EditTaskStatus();
                   
                    EditTaskType();
                    messBoxOk("Task edit successful.", "Edit task");
                }
            }
            loadAllTasks();

        }
        

        private DelegateCommand ButtonSendCommentClick;
        public ICommand BSendComment_Click
        {
            get
            {
                if (ButtonSendCommentClick == null)
                {
                    ButtonSendCommentClick = new DelegateCommand(param => this.SendComments(),param => true);
                }
                return ButtonSendCommentClick;
            }
        }

        public void SendComments()
        {
            F_Task.AddNewComment(CurProjName, CurTaskName, CurrentEmployee.Name, CommentText);
            ListComments = new List<TaskComment>( F_Task.GetAllCommentsObjects(CurrentProject.Id, CurrentIssueID));
            CommentText = "";
        }
        
        public Visibility VTaskName
        {
            get { return vTaskName; }
            set
            {
                vTaskName = value;
                OnPropertyChanged("VTaskName");
            }
        }
        public Visibility VDueDateTask
        {
            get { return vDueDateTask; }
            set
            {
                vDueDateTask = value;
                OnPropertyChanged("VDueDateTask");
            }
        }
        public Visibility VStatus
        {
            get { return vStatus; }
            set
            {
                vStatus = value;
                OnPropertyChanged("VStatus");
            }
        }
        public Visibility VTaskType
        {
            get { return vTaskType; }
            set
            {
                vTaskType = value;
                OnPropertyChanged("VTaskType");
            }
        }
        public Visibility VTaskAssignees
        {
            get { return vTaskAssignees; }
            set
            {
                vTaskAssignees = value;
                OnPropertyChanged("VTaskAssignees");
            }
        }
        public Visibility VTaskDescription
        {
            get { return vTaskDescription; }
            set
            {
                vTaskDescription = value;
                OnPropertyChanged("VTaskDescription");
            }
        }
        public Visibility VTaskPriority
        {
            get { return vTaskPriority; }
            set
            {
                vTaskPriority = value;
                OnPropertyChanged("VTaskPriority");
            }
        }
        
        public Visibility VPrName
        {
            get { return vPrName; }
            set
            {
                vPrName = value;
                OnPropertyChanged("VPrName");
            }
        }
        
        public Visibility VCustomerName
        {
            get { return vCustomerName; }
            set
            {
                vCustomerName = value;
                OnPropertyChanged("VCustomerName");
            }
        }
        
        public Visibility VStage
        {
            get { return vStage; }
            set
            {
                vStage = value;
                OnPropertyChanged("VStage");
            }
        }
            
            public Visibility VDueDate
            {
                get { return vDueDate; }
                set
                {
                    vDueDate = value;
                    OnPropertyChanged("VDueDate");
                }
            }
            
            public Visibility VDuration
            {
                get { return vDuration; }
                set
                {
                    vDuration = value;
                    OnPropertyChanged("VDuration");
                }
            }
            
            public Visibility VObjective
            {
                get { return vObjective; }
                set
                {
                    vObjective = value;
                    OnPropertyChanged("VObjective");
                }
            }
           
            public Visibility VDescription
            {
                get { return vDescription; }
                set
                {
                    vDescription = value;
                    OnPropertyChanged("VDescription");
                }
            }
        //private DelegateCommand ButtonBPrName;
        //public ICommand BPrName
        //{
        //    get
        //    {
        //        if (ButtonBPrName == null)
        //        {
        //            ButtonBPrName = new DelegateCommand(param => { }, param => IsProjectNameAvailable());
        //        }
        //        return ButtonBPrName;
        //    }
        //}
        private bool IsProjectNameAvailable()
        {
            return (CurProjName == null || CurProjName.Length == 0);
        }

        //private DelegateCommand ButtonCustomer;
        //public ICommand BCustomerName
        //{
        //    get
        //    {
        //        if (ButtonCustomer == null)
        //        {
        //            ButtonCustomer = new DelegateCommand(param => { }, param => IsCustomerNameAvailable());
        //        }
        //        return ButtonCustomer;
        //    }
        //}
        private bool IsCustomerNameAvailable()
        {
            return (CurProjCustomer.Value == null || CurProjCustomer.Value.Trim().Length == 0);
        }

        //private DelegateCommand ButtonStage;
        //public ICommand BStage
        //{
        //    get
        //    {
        //        if (ButtonStage == null)
        //        {
        //            ButtonStage = new DelegateCommand(param => { }, param => IsStageAvailable());
        //        }
        //        return ButtonStage;
        //    }
        //}
        private bool IsStageAvailable()
        {
            return (CurProjStage.Value == null || CurProjStage.Value.Trim().Length == 0);
        }

        //private DelegateCommand ButtonDueDate;
        //public ICommand BDueDate
        //{
        //    get
        //    {
        //        if (ButtonDueDate == null)
        //        {
        //            ButtonDueDate = new DelegateCommand(param => { }, param => IsDueDateAvailable());
        //        }
        //        return ButtonDueDate;
        //    }
        //}
        private bool IsDueDateAvailable()
        {
            return (CurProjDueDate == null || CurProjDueDate.ToString() == "");
        }

        //private DelegateCommand ButtonDuration;
        //public ICommand BDuration
        //{
        //    get
        //    {
        //        if (ButtonDuration == null)
        //        {
        //            ButtonDuration = new DelegateCommand(param => { }, param => IsDurationAvailable());
        //        }
        //        return ButtonDuration;
        //    }
        //}
        private bool IsDurationAvailable()
        {
            return (CurProjDuration.Value == null || CurProjDuration.Value.Trim().Length==0);
        }

        //private DelegateCommand ButtonObjective;
        //public ICommand BObjective
        //{
        //    get
        //    {
        //        if (ButtonObjective == null)
        //        {
        //            ButtonObjective = new DelegateCommand(param => { }, param => IsObjectiveAvailable());
        //        }
        //        return ButtonObjective;
        //    }
        //}
        private bool IsObjectiveAvailable()
        {
            return (CurProjObjective.Value == null || CurProjObjective.Value.Trim().Length==0);
        }

        //private DelegateCommand ButtonDescription;
        //public ICommand BDescription
        //{
        //    get
        //    {
        //        if (ButtonDescription == null)
        //        {
        //            ButtonDescription = new DelegateCommand(param => { }, param => IsDescriptionAvailable());
        //        }
        //        return ButtonDescription;
        //    }
        //}
        private bool IsDescriptionAvailable()
        {
            return (CurProjDescription == null || CurProjDescription.Length == 0);
        }

        //private DelegateCommand ButtonTaskName;
        //public ICommand BTaskName
        //{
        //    get
        //    {
        //        if (ButtonTaskName == null)
        //        {
        //            ButtonTaskName = new DelegateCommand(param => { }, param => IsTaskNameAvailable());
        //        }
        //        return ButtonTaskName;
        //    }
        //}
        private bool IsTaskNameAvailable()
        {
            return (CurTaskName == null || CurTaskName.Length == 0);
        }

        //private DelegateCommand ButtonDueDateTask;
        //public ICommand BDueDateTask
        //{
        //    get
        //    {
        //        if (ButtonDueDateTask == null)
        //        {
        //            ButtonDueDateTask = new DelegateCommand(param => { }, param => IsDueDateTaskAvailable());
        //        }
        //        return ButtonDueDateTask;
        //    }
        //}
        private bool IsDueDateTaskAvailable()
        {
            return (CurTaskDueDate == null || CurTaskDueDate.ToString() == "");
        }

        //private DelegateCommand ButtonStatus;
        //public ICommand BStatus
        //{
        //    get
        //    {
        //        if (ButtonStatus == null)
        //        {
        //            ButtonStatus = new DelegateCommand(param => { }, param => IsStatusAvailable());
        //        }
        //        return ButtonStatus;
        //    }
        //}
        private bool IsStatusAvailable()
        {
            return (CurTaskStatus.Value == null || CurTaskStatus.Value.Length == 0);
        }
        //private DelegateCommand ButtonTaskPriority;
        //public ICommand BTaskPriority
        //{
        //    get
        //    {
        //        if (ButtonTaskPriority == null)
        //        {
        //            ButtonTaskPriority = new DelegateCommand(param => { }, param => IsTaskPriorityAvailable());
        //        }
        //        return ButtonTaskPriority;
        //    }
        //}
        private bool IsTaskPriorityAvailable()
        {
            return (CurTaskPriority.Value == null || CurTaskPriority.Value.Length == 0);
        }
        //private DelegateCommand ButtonTaskType;
        //public ICommand BTaskType
        //{
        //    get
        //    {
        //        if (ButtonTaskType == null)
        //        {
        //            ButtonTaskType = new DelegateCommand(param => { }, param => IsTaskTypeAvailable());
        //        }
        //        return ButtonTaskType;
        //    }
        //}
        private bool IsTaskTypeAvailable()
        {
            return (CurTaskType.Value == null || CurTaskType.Value.Length == 0);
        }
        //private DelegateCommand ButtonTaskAssignees;
        //        public ICommand BTaskAssignees
        //{
        //    get
        //    {
        //        if (ButtonTaskAssignees == null)
        //        {
        //            ButtonTaskAssignees = new DelegateCommand(param => { }, param => IsTaskAssigneesAvailable());
        //        }
        //        return ButtonTaskAssignees;
        //    }
        //}
        private bool IsTaskAssigneesAvailable()
        {
            return (TaskAssignees == null || TaskAssignees.Count == 0);
        }
        //private DelegateCommand ButtonTaskDescription;
        
        //public ICommand BTaskDescription
        //{
        //    get
        //    {
        //        if (ButtonTaskDescription == null)
        //        {
        //            ButtonTaskDescription = new DelegateCommand(param => { }, param => IsTaskDescriptionAvailable());
        //        }
        //        return ButtonTaskDescription;
        //    }
        //}
        private bool IsTaskDescriptionAvailable()
        {
            return (CurTaskDescription == null || CurTaskDescription.Length == 0);
        }
        private DelegateCommand ButtonSetFavProjClick;
        public ICommand BSetFavProj_Click
        {
            get
            {
                if (ButtonSetFavProjClick == null)
                {
                    ButtonSetFavProjClick = new DelegateCommand(param => this.SetFavProj(),param => IsSelProject());
                }
                return ButtonSetFavProjClick;
            }
        }
        bool IsSelProject()
        {
            return CurrentProject!=null;
        }
        public void SetFavProj()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null || tempPr.isCatagory)
            {
                messBoxOk("Not selected project", "Favorite project");
                return;
            }
            if (messBoxYesNo("Do you want set favorite this project?", "Favorite project"))
            {

                bool isFav = F_Projects.IsProjectFavourite(tempPr.id, CurrentEmployee.Id);
                string mess= F_Projects.SetFavouriteProject(tempPr.id, CurrentEmployee.Id, !isFav);
                if (mess == null)
                {
                    messBoxOk("Project set favorite successful.", "Favorite project");
                    loadAllListPrInfo();
                    loadAllProjects();

                }
                else
                    messBoxOk(mess, "Favorite project");
            }

        }
        
        private DelegateCommand ButtonAddProjClick;
        public ICommand BAddProj_Click
        {
            get
            {
                if (ButtonAddProjClick == null)
                {
                    ButtonAddProjClick = new DelegateCommand(param => this.AddProj(),param => true);
                }
                return ButtonAddProjClick;
            }
        }
        public void AddProj()
        {
            CurProjName = "New Project";
            CurProjCreationDate = DateTime.Now;
            CurProjDueDate = DateTime.Now.AddHours(1);
            CurProjDescription = "";
            CurProjDuration = new KeyValuePair<int, string>();
            CurProjCustomer = new KeyValuePair<int, string>();
            CurProjLead = new KeyValuePair<int, string>();
            CurProjStage = new KeyValuePair<int, string>();
            CurProjObjective = new KeyValuePair<int, string>();
            CurProjFiles = new List<checkEl<ProjectFile>>();
            CurProjSkills = new List<checkEl<KeyValuePair<int, string>>>();
            CurProjTypes = new List<checkEl<KeyValuePair<int, string>>>();
            CurProjOS = new List<checkEl<KeyValuePair<int, string>>>();
            IsNewProj = true;
            IsEnabledSavePr = true;
            loadAllListPrInfo();
            IsSelectedPrjct = true;
        }
        
        private DelegateCommand ButtonDelProjClick;
        public ICommand BDelProj_Click
        {
            get
            {
                if (ButtonDelProjClick == null)
                {
                    ButtonDelProjClick = new DelegateCommand(param => this.DelProj(),param => IsSelProject());
                }
                return ButtonDelProjClick;
            }
        }
        public void DelProj()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
            {
                messBoxOk("Not selected project", "Project");
                return;
            }
            if (messBoxYesNo("Do you want to delete this project?", "Delete project"))
            {
                
                string mess = F_Projects.DeleteProject(tempPr.id);
                if (mess == null)
                {
                    messBoxOk("Project deleted successful.", "Delete project");
                    
                }
                else
                    messBoxOk(mess, "Delete project");
            }
            loadAllListPrInfo();
        }

       
        private DelegateCommand ButtonAddTaskClick;
        public ICommand BAddTask_Click
        {
            get
            {
                if (ButtonAddTaskClick == null)
                {
                    ButtonAddTaskClick = new DelegateCommand(param => this.AddTask(), param =>  isEnblAddTask());
                }
                return ButtonAddTaskClick;
            }
        }
        bool isEnblAddTask()
        {
             treeElem tempPr = SelTreeElemPr as treeElem;
             return tempPr != null;
        }
        public void AddTask()
        {
            DateTime curDate = DateTime.Now;
            string newTaskName = "New Task";
            CurTaskName = newTaskName;
            TaskAssignees = new List<checkEl<KeyValuePair<int, string>>>();
            CurTaskCreationDate = curDate;
            CurTaskDueDate = curDate.AddHours(1);
            CurTaskCreator = CurrentEmployee.Name + " " + CurrentEmployee.Surname;
            CurTaskStatus = new KeyValuePair<int,string>();
            CurTaskPriority = new KeyValuePair<int, string>();
            CurTaskType = new KeyValuePair<int, string>();
            CurTaskProject = CurProjName;
            CurTaskDescription = "";
            ListTaskAssignees = F_Staff.GetProjectAllEmployees(CurrentProject.Id);
            TaskFiles = new List<checkEl<TaskFile>>();
            ListComments = new List<TaskComment> ();
            IsNewTask = true;
            IsSelectedTask = true;
        }
        private DelegateCommand ButtonDelTaskClick;
        public ICommand BDelTask_Click
        {
            get
            {
                if (ButtonDelTaskClick == null)
                {
                    ButtonDelTaskClick = new DelegateCommand(param => this.DelTask(),param => IsdelTask());
                }
                return ButtonDelTaskClick;
            }
        }
        bool IsdelTask()
        {
            treeElem tempPr = SelTreeElemPr as treeElem;
            treeElem tempTs = SelTreeElemTask as treeElem;
            return (tempPr != null && tempTs != null && !tempPr.isCatagory && !tempTs.isCatagory && !isNewTask);
        }
        public void DelTask()
        {
            treeElem tempPr = SelTreeElemPr as treeElem;
                if (tempPr == null)
                {
                messBoxOk("Not selected project", "Delete task");
                    return;
                }
                treeElem tempTs = SelTreeElemTask as treeElem;
                if (tempTs == null)
                    if (tempPr == null)
                    {
                    messBoxOk("Not selected task", "Delete task");
                        return;
                    }
            if (messBoxYesNo("Do you want to delete this task?", "Delete task"))
            {
                
                string mess = F_Task.DeleteIssue(tempPr.id, tempTs.id);
                if (mess == null)
                {
                    messBoxOk("Task deleted successful.", "Delete task");
                    isNewPrjct = false;
                }
                else
                    messBoxOk(mess, "Delete project");
            }

            loadAllTasks();

        }

        private DelegateCommand ButtonEditPrNameClick;
        public ICommand BEditPrName_Click
        {
            get
            {
                if (ButtonEditPrNameClick == null)
                {
                    ButtonEditPrNameClick = new DelegateCommand(param => this.EditPrName(),param => true);
                }
                return ButtonEditPrNameClick;
            }
        }
        public void EditPrName()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            F_Projects.EditProjectName(tempPr.id, CurProjName);
        }
        private DelegateCommand ButtonEditPrStageClick;
        public ICommand BEditPrStage_Click
        {
            get
            {
                if (ButtonEditPrStageClick == null)
                {
                    ButtonEditPrStageClick = new DelegateCommand(param => this.EditPrStage(),param => true);
                }
                return ButtonEditPrStageClick;
            }
        }
        public void EditPrStage()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            F_Projects.EditProjectStage(tempPr.id, CurProjStage.Key);
        }

        private DelegateCommand ButtonEditPrDueDateClick;
        public ICommand BEditPrDueDate_Click
        {
            get
            {
                if (ButtonEditPrDueDateClick == null)
                {
                    ButtonEditPrDueDateClick = new DelegateCommand(param => this.EditPrDueDate(),param => true);
                }
                return ButtonEditPrDueDateClick;
            }
        }
        public void EditPrDueDate()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            F_Projects.EditProjectDueDate(tempPr.id, CurProjDueDate);
        }
        private DelegateCommand ButtonEditPrObjectiveClick;
        public ICommand BEditPrObjective_Click
        {
            get
            {
                if (ButtonEditPrObjectiveClick == null)
                {
                    ButtonEditPrObjectiveClick = new DelegateCommand(param => this.EditPrObjective(),param => true);
                }
                return ButtonEditPrObjectiveClick;
            }
        }
        public void EditPrObjective()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;

            F_Projects.EditProjectObjective(tempPr.id, CurProjObjective.Key);
        }
        private DelegateCommand ButtonEditPrOSClick;
        public ICommand BEditPrOS_Click
        {
            get
            {
                if (ButtonEditPrOSClick == null)
                {
                    ButtonEditPrOSClick = new DelegateCommand(param => this.EditPrOS(),param => true);
                }
                return ButtonEditPrOSClick;
            }
        }
        public void EditPrOS()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            List<KeyValuePair<int, string>> allPrOss = F_Projects.GetProjectOSs(tempPr.id);
            for(int i =0;i< CurProjOS.Count; i++)
            {
                if (allPrOss.IndexOf(CurProjOS[i].chClass)==-1)
                    F_Projects.AddOSToProject(tempPr.id, CurProjOS[i].chClass.Key);
                
            }
            List<KeyValuePair<int, string>> temp = new List<KeyValuePair<int, string>>();
            foreach (var str in CurProjOS)
                temp.Add(str.chClass);
            for (int i = 0; i < allPrOss.Count; i++)
            {
                if (temp.IndexOf(allPrOss[i]) == -1)
                    F_Projects.RemoveProjectOS(tempPr.id, allPrOss[i].Key);
            }
        }
        
        private DelegateCommand ButtonAddPrSkillsClick;
        public ICommand BAddCurPrSkills_Click
        {
            get
            {
                if (ButtonAddPrSkillsClick == null)
                {
                    ButtonAddPrSkillsClick = new DelegateCommand(param => this.AddPrSkills(),param => true);
                }
                return ButtonAddPrSkillsClick;
            }
        }

        public void AddPrSkills()
        {
           CurProjSkills.Add(new checkEl<KeyValuePair<int, string>>(ListProjSkills[CBSelPrSkill]));
           CurProjSkills = new List<checkEl<KeyValuePair<int, string>>>(CurProjSkills);
            ListProjSkills.Remove(ListProjSkills[CBSelPrSkill]);
            ListProjSkills = new List<KeyValuePair<int, string>>(ListProjSkills);
        }
        private DelegateCommand ButtonDelPrSkillsClick;
        public ICommand BDelPrSkills_Click
        {
            get
            {
                if (ButtonDelPrSkillsClick == null)
                {
                    ButtonDelPrSkillsClick = new DelegateCommand(param => this.DelPrSkills(),param => true);
                }
                return ButtonDelPrSkillsClick;
            }
        }

        public void DelPrSkills()
        {
            for (int i = CurProjSkills.Count - 1; i >= 0; i--)
            {
                if (CurProjSkills[i].isCheck)
                {
                    ListProjSkills.Add(CurProjSkills[i].chClass);
                    ListProjSkills.Sort();
                    ListProjSkills = new List<KeyValuePair<int, string>>(ListProjSkills);
                    CurProjSkills.Remove(CurProjSkills[i]);
                }
            }
            CurProjSkills = new List<checkEl<KeyValuePair<int, string>>>(CurProjSkills);
        }
            private DelegateCommand ButtonAddEmplPrjctClick;
        public ICommand BAddEmplPrjct_Click
        {
            get
            {
                if (ButtonAddEmplPrjctClick == null)
                {
                    ButtonAddEmplPrjctClick = new DelegateCommand(param => this.AddEmplPrjct(), param => true);
                }
                return ButtonAddEmplPrjctClick;
            }
        }

        public void AddEmplPrjct()
        {
            DevelopTeam.Add(new checkEl<KeyValuePair<int, string>>(ListEmpl[CBEmplPrjct]));
            DevelopTeam = new List<checkEl<KeyValuePair<int, string>>>(DevelopTeam);
            ListEmpl.Remove(ListEmpl[CBEmplPrjct]);
            ListEmpl = new List<KeyValuePair<int, string>>(ListEmpl);
        }
        private DelegateCommand ButtonDelEmplPrjctClick;
        public ICommand BDelEmplPrjct_Click
        {
            get
            {
                if (ButtonDelEmplPrjctClick == null)
                {
                    ButtonDelEmplPrjctClick = new DelegateCommand(param => this.DelEmplPrjct(), param => true);
                }
                return ButtonDelEmplPrjctClick;
            }
        }

        public void DelEmplPrjct()
        {
            for (int i = DevelopTeam.Count - 1; i >= 0; i--)
            {
                if (DevelopTeam[i].isCheck)
                {
                    ListEmpl.Add(DevelopTeam[i].chClass);
                    ListEmpl.Sort();
                    ListEmpl = new List<KeyValuePair<int, string>>(ListEmpl);
                    DevelopTeam.Remove(DevelopTeam[i]);
                }
            }
            DevelopTeam = new List<checkEl<KeyValuePair<int, string>>>(DevelopTeam);
        }
        private DelegateCommand ButtonAddPrOSClick;
        public ICommand BAddCurPrOS_Click
        {
            get
            {
                if (ButtonAddPrOSClick == null)
                {
                    ButtonAddPrOSClick = new DelegateCommand(param => this.AddPrOS(),param => true);
                }
                return ButtonAddPrOSClick;
            }
        }

        public void AddPrOS()
        {
            CurProjOS.Add(new checkEl<KeyValuePair<int, string>>(ListProjOS[CBSelPrOS]));
            CurProjOS = new List<checkEl<KeyValuePair<int, string>>>(CurProjOS);
            ListProjOS.Remove(ListProjOS[CBSelPrOS]);
            ListProjOS = new List<KeyValuePair<int, string>>(ListProjOS);
        }
        private DelegateCommand ButtonDelPrOSClick;
        public ICommand BDelPrOS_Click
        {
            get
            {
                if (ButtonDelPrOSClick == null)
                {
                    ButtonDelPrOSClick = new DelegateCommand(param => this.DelPrOS(),param => true);
                }
                return ButtonDelPrOSClick;
            }
        }

        public void DelPrOS()
        {
            for (int i = CurProjOS.Count - 1; i >= 0; i--)
            {
                if (CurProjOS[i].isCheck)
                {
                    ListProjOS.Add(CurProjOS[i].chClass);
                    ListProjOS.Sort();
                    ListProjOS = new List<KeyValuePair<int, string>>(ListProjOS);
                    CurProjOS.Remove(CurProjOS[i]);
                }
            }
            CurProjOS = new List<checkEl<KeyValuePair<int, string>>>(CurProjOS);
            
        }
        private DelegateCommand ButtonAddPrTypesClick;
        public ICommand BAddCurPrTypes_Click
        {
            get
            {
                if (ButtonAddPrTypesClick == null)
                {
                    ButtonAddPrTypesClick = new DelegateCommand(param => this.AddPrTypes(),param => true);
                }
                return ButtonAddPrTypesClick;
            }
        }

        public void AddPrTypes()
        {
            
            CurProjTypes.Add(new checkEl<KeyValuePair<int, string>>(ListProjTypes[CBSelPrTypes]));
            CurProjTypes = new List<checkEl<KeyValuePair<int, string>>>(CurProjTypes);
            ListProjTypes.Remove(ListProjTypes[CBSelPrTypes]);
            ListProjTypes = new List<KeyValuePair<int, string>>(ListProjTypes);
        }
        private DelegateCommand ButtonDelPrTypesClick;
        public ICommand BDelPrTypes_Click
        {
            get
            {
                if (ButtonDelPrTypesClick == null)
                {
                    ButtonDelPrTypesClick = new DelegateCommand(param => this.DelPrTypes(),param => true);
                }
                return ButtonDelPrTypesClick;
            }
        }

        public void DelPrTypes()
        {
            for (int i = CurProjTypes.Count - 1; i >= 0; i--)
            {
                if (CurProjTypes[i].isCheck)
                {
                    ListProjTypes.Add(CurProjTypes[i].chClass);
                    ListProjTypes.Sort();
                    ListProjTypes = new List<KeyValuePair<int, string>>(ListProjTypes);
                    CurProjTypes.Remove(CurProjTypes[i]);
                }
            }
            CurProjTypes = new List<checkEl<KeyValuePair<int, string>>>(CurProjTypes);
        }
        
        
        
        private DelegateCommand ButtonEditPrDescriptionClick;
        public ICommand BEditPrDescription_Click
        {
            get
            {
                if (ButtonEditPrDescriptionClick == null)
                {
                    ButtonEditPrDescriptionClick = new DelegateCommand(param => this.EditPrDescription(),param => true);
                }
                return ButtonEditPrDescriptionClick;
            }
        }
        public void EditPrDescription()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            F_Projects.EditProjectDescription(tempPr.id, CurProjDescription);
        }
        private DelegateCommand ButtonEditPrSkillsClick;
        public ICommand BEditPrSkills_Click
        {
            get
            {
                if (ButtonEditPrSkillsClick == null)
                {
                    ButtonEditPrSkillsClick = new DelegateCommand(param => this.EditPrSkills(),param => true);
                }
                return ButtonEditPrSkillsClick;
            }
        }
        public void EditPrSkills()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            List<KeyValuePair<int, string>> allPrSkills = F_Projects.GetProjectSkills(tempPr.id);
            for (int i = 0; i < CurProjSkills.Count; i++)
            {
                if (allPrSkills.IndexOf(CurProjSkills[i].chClass) == -1)
                    F_Projects.AddSkillToProject(tempPr.id, CurProjSkills[i].chClass.Key);
            }
            List<KeyValuePair<int, string>> temp = new List<KeyValuePair<int, string>>();
            foreach (var str in CurProjSkills)
                temp.Add(str.chClass);
            for (int i = 0; i < allPrSkills.Count; i++)
            {
                if (temp.IndexOf(allPrSkills[i]) == -1)
                    F_Projects.RemoveProjectSkill(tempPr.id, allPrSkills[i].Key);
            }
        }
        


        private DelegateCommand ButtonEditPrDurationClick;
        public ICommand BEditPrDuration_Click
        {
            get
            {
                if (ButtonEditPrDurationClick == null)
                {
                    ButtonEditPrDurationClick = new DelegateCommand(param => this.EditPrDuration(),param => true);
                }
                return ButtonEditPrDurationClick;
            }
        }
        public void EditPrDuration()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            F_Projects.EditProjectDuration(tempPr.id, CurProjDuration.Key);
        }
        private DelegateCommand ButtonEditPrCustomerClick;
        public ICommand BEditPrCustomer_Click
        {
            get
            {
                if (ButtonEditPrCustomerClick == null)
                {
                    ButtonEditPrCustomerClick = new DelegateCommand(param => this.EditPrCustomer(),param => true);
                }
                return ButtonEditPrCustomerClick;
            }
        }
        public void EditPrCustomer()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            F_Projects.EditProjectCustomer(tempPr.id, CurProjCustomer.Key);
        }
        private DelegateCommand ButtonEditPrTypesClick;
        public ICommand BEditPrTypes_Click
        {
            get
            {
                if (ButtonEditPrTypesClick == null)
                {
                    ButtonEditPrTypesClick = new DelegateCommand(param => this.EditPrTypes(),param => true);
                }
                return ButtonEditPrTypesClick;
            }
        }
        public void EditPrTypes()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            List<KeyValuePair<int, string>> allPrTypes = F_Projects.GetProjectTypes(tempPr.id);
            for (int i = 0; i < CurProjTypes.Count; i++)
            {
                if (allPrTypes.IndexOf(CurProjTypes[i].chClass) == -1)
                    F_Projects.AddTypeToProject(tempPr.id, CurProjTypes[i].chClass.Key);
            }
            List<KeyValuePair<int, string>> temp = new List<KeyValuePair<int, string>>();
            foreach (var str in CurProjTypes)
                temp.Add(str.chClass);
            for (int i = 0; i < allPrTypes.Count; i++)
            {
                if (temp.IndexOf(allPrTypes[i]) == -1)
                    F_Projects.RemoveProjectType(tempPr.id, allPrTypes[i].Key);
            }
        }
        private DelegateCommand ButtonEditTaskNameClick;
        public ICommand BEditTaskName_Click
        {
            get
            {
                if (ButtonEditTaskNameClick == null)
                {
                    ButtonEditTaskNameClick = new DelegateCommand(param => this.EditTaskName(),param => true);
                }
                return ButtonEditTaskNameClick;
            }
        }
        public void EditTaskName()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            treeElem tempTs = SelTreeElemTask as treeElem;
            if (tempTs == null)
                return;
            isEnableEditTask = !isEnableEditTask;
            F_Task.EditIssueName(tempPr.id, tempTs.id, CurTaskName);
        }
        private DelegateCommand ButtonEditTaskDueDateClick;
        public ICommand BEditTaskDueDate_Click
        {
            get
            {
                if (ButtonEditTaskDueDateClick == null)
                {
                    ButtonEditTaskDueDateClick = new DelegateCommand(param => this.EditTaskDueDate(),param => true);
                }
                return ButtonEditTaskDueDateClick;
            }
        }
        public void EditTaskDueDate()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            treeElem tempTs = SelTreeElemTask as treeElem;
            if (tempTs == null)
                return;
            isEnableEditTask = !isEnableEditTask;
            F_Task.EditIssueDueDate(tempPr.id, tempTs.id, CurTaskDueDate);
        }

        private DelegateCommand ButtonEditTaskStatusClick;
        public ICommand BEditTaskStatus_Click
        {
            get
            {
                if (ButtonEditTaskStatusClick == null)
                {
                    ButtonEditTaskStatusClick = new DelegateCommand(param => this.EditTaskStatus(),param => true);
                }
                return ButtonEditTaskStatusClick;
            }
        }
        public void EditTaskStatus()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            treeElem tempTs = SelTreeElemTask as treeElem;
            if (tempTs == null)
                return;
            isEnableEditTask = !isEnableEditTask;
            F_Task.EditIssueStatus(tempPr.id, tempTs.id, CurTaskStatus.Key);
        }
        private DelegateCommand ButtonEditTaskPriorityClick;
        public ICommand BEditTaskPriority_Click
        {
            get
            {
                if (ButtonEditTaskPriorityClick == null)
                {
                    ButtonEditTaskPriorityClick = new DelegateCommand(param => this.EditTaskPriority(),param => true);
                }
                return ButtonEditTaskPriorityClick;
            }
        }
        public void EditTaskPriority()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            treeElem tempTs = SelTreeElemTask as treeElem;
            if (tempTs == null)
                return;
            isEnableEditTask = !isEnableEditTask;
            F_Task.EditIssuePriority(tempPr.id, tempTs.id, CurTaskPriority.Key);
        }
        private DelegateCommand ButtonEditTaskTypeClick;
        public ICommand BEditTaskType_Click
        {
            get
            {
                if (ButtonEditTaskTypeClick == null)
                {
                    ButtonEditTaskTypeClick = new DelegateCommand(param => this.EditTaskType(),param => true);
                }
                return ButtonEditTaskTypeClick;
            }
        }
        public void EditTaskType()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            treeElem tempTs = SelTreeElemTask as treeElem;
            if (tempTs == null)
                return;
            isEnableEditTask = !isEnableEditTask;

            F_Task.EditIssueType(tempPr.id, tempTs.id, CurTaskType.Key);
        }
       
        private DelegateCommand ButtonEditTaskDescriptionClick;
        public ICommand BEditTaskDescription_Click
        {
            get
            {
                if (ButtonEditTaskDescriptionClick == null)
                {
                    ButtonEditTaskDescriptionClick = new DelegateCommand(param => this.EditTaskDescription(),param => true);
                }
                return ButtonEditTaskDescriptionClick;
            }
        }
        public void EditTaskDescription()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            treeElem tempTs = SelTreeElemTask as treeElem;
            if (tempTs == null)
                return;
            isEnableEditTask = !isEnableEditTask;
            F_Task.EditIssueDescription(tempPr.id, tempTs.id, CurTaskDescription);
        }
        private DelegateCommand ButtonAddTaskAssigneesClick;
        public ICommand BAddCurTaskAssignees_Click
        {
            get
            {
                if (ButtonAddTaskAssigneesClick == null)
                {
                    ButtonAddTaskAssigneesClick = new DelegateCommand(param => this.AddTaskAssignees(), param => true);
                }
                return ButtonAddTaskAssigneesClick;
            }
        }

        public void AddTaskAssignees()
        {
            if (CBSelTaskAssignees == -1)
                return;
            TaskAssignees.Add(new checkEl<KeyValuePair<int, string>>(ListTaskAssignees[CBSelTaskAssignees]));
            TaskAssignees = new List<checkEl<KeyValuePair<int, string>>>(TaskAssignees);
            ListTaskAssignees.Remove(ListTaskAssignees[CBSelTaskAssignees]);
            ListTaskAssignees = new List<KeyValuePair<int, string>>(ListTaskAssignees);
        }
        private DelegateCommand ButtonDelTaskAssigneesClick;
        public ICommand BDelTaskAssignees_Click
        {
            get
            {
                if (ButtonDelTaskAssigneesClick == null)
                {
                    ButtonDelTaskAssigneesClick = new DelegateCommand(param => this.DelTaskAssignees(), param => true);
                }
                return ButtonDelTaskAssigneesClick;
            }
        }

        public void DelTaskAssignees()
        {
            for (int i = TaskAssignees.Count - 1; i >= 0; i--)
            {
                if (TaskAssignees[i].isCheck)
                {
                    ListTaskAssignees.Add(TaskAssignees[i].chClass);
                    ListTaskAssignees = new List<KeyValuePair<int, string>>(ListTaskAssignees);
                    TaskAssignees.Remove(TaskAssignees[i]);
                }
            }
            TaskAssignees = new List<checkEl<KeyValuePair<int, string>>>(TaskAssignees);
        }
        public void EditTaskAssignees()
        {
            treeElem tempPr = selTreeElemPr as treeElem;
            if (tempPr == null)
                return;
            treeElem tempTs = SelTreeElemTask as treeElem;
            if (tempTs == null)
                return;
            isEnableEditTask = !isEnableEditTask;
            List<KeyValuePair<int, string>> allIssueAssignees = F_Task.GetIssueAssigneesNameWithId(tempPr.id, tempTs.id);
            for (int i = 0; i < TaskAssignees.Count; i++)
            {
                if (allIssueAssignees.IndexOf(TaskAssignees[i].chClass) == -1)
                    F_Task.AddAssigneeToIssue(tempPr.id, tempTs.id, TaskAssignees[i].chClass.Key);

            }
            List<KeyValuePair<int, string>> temp = new List<KeyValuePair<int, string>>();
            foreach (var str in TaskAssignees)
                temp.Add(str.chClass);
            for (int i = 0; i < allIssueAssignees.Count; i++)
            {
                if (temp.IndexOf(allIssueAssignees[i]) == -1)
                    F_Task.RemoveAssigneeFromIssue(tempPr.id, tempTs.id, allIssueAssignees[i].Key);
            }
        }
        #endregion
    }
}
