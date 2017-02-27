using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DelegateCommandNS;
using Facade;
using VM_Base;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using CustomMessageBox_OK;
using CustomMessageBox_YesNo;

namespace Editor_Projects
{
    public class VM_Editor_Projects : ViewModelBase
    {
        public bool IsChanged { set; get; }
        public VM_Editor_Projects()
        {
            loadAllCustomer();
            loadAllDuration();
            loadAllObjective();
            loadAllOperationSystem();
            loadAllStage();
            loadAllType();
            IsChanged = false;
        }
        bool IsEmptyField(string str)
        {
            return (str != null && str.Trim().Length > 0);
        }
        bool IsSelected(int SelEl)
        {
            return SelEl >= 0;
        }
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
        #region Customers
        #region fields
        string nameNewCustomer;
        List<KeyValuePair<int, string>> listCustomer;
        int selDelCustomer;
        string edNameNewCustomer;
        int selEditCustomer;
        #endregion
        #region properties
        public string NameNewCustomer
        {
            get
            {
                return nameNewCustomer;
            }
            set
            {
                nameNewCustomer = value;
                OnPropertyChanged("NameNewCustomer");
            }
        }
        public List<KeyValuePair<int, string>> ListCustomer
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
        public int SelDelCustomer
        {
            get
            {
                return selDelCustomer;
            }
            set
            {
                selDelCustomer = value;
                OnPropertyChanged("SelDelCustomer");
            }
        }
        public string EdNameNewCustomer
        {
            get
            {
                return edNameNewCustomer;
            }
            set
            {
                edNameNewCustomer = value;
                OnPropertyChanged("EdNameNewCustomer");
            }
        }
        public int SelEditCustomer
        {
            get
            {
                return selEditCustomer;
            }
            set
            {
                selEditCustomer = value;
                OnPropertyChanged("SelEditCustomer");
            }
        }
        #endregion
        #region Commands
        private DelegateCommand ButtonAddCustomerClick;
        public ICommand BAddCustomer_Click
        {
            get
            {
                if (ButtonAddCustomerClick == null)
                {
                    ButtonAddCustomerClick = new DelegateCommand(param => this.AddCustomer(), param => IsEmptyField(NameNewCustomer));
                }
                return ButtonAddCustomerClick;
            }
        }

        public void AddCustomer()
        {
            if(messBoxYesNo("Do you want to add this customer?", "Add new customer"))
            {
                string mess = F_Projects.AddNewCustomer(NameNewCustomer.Trim());
                if (mess == null)
                {
                    messBoxOk("New customer was added successfully.", "Add new customer");
                    loadAllCustomer();
                    IsChanged = true;
                    NameNewCustomer = "";
                }
                else
                    messBoxOk(mess, "Add new customer");
                
            }
        }
        private DelegateCommand ButtonDelCustomerClick;
        public ICommand BDelCustomer_Click
        {
            get
            {
                if (ButtonDelCustomerClick == null)
                {
                    ButtonDelCustomerClick = new DelegateCommand(param => this.DelCustomer(), param => IsSelected(SelDelCustomer));
                }
                return ButtonDelCustomerClick;
            }
        }
        public void DelCustomer()
        {
            if(messBoxYesNo("Do you want to delete this customer?", "Delete customer"))
            {
                string mess = F_Projects.DeleteCustomer(ListCustomer[SelDelCustomer].Key);
                if (mess == null)
                {
                    messBoxOk("Customer was deleted successfully.", "Delete customer");
                    loadAllCustomer();
                    IsChanged = true;
                }
                else
                    messBoxOk(mess, "Delete customer");
                
            }
        }
        private DelegateCommand ButtonEditCustomerClick;
        public ICommand BEditCustomer_Click
        {
            get
            {
                if (ButtonEditCustomerClick == null)
                {
                    ButtonEditCustomerClick = new DelegateCommand(param => this.EditCustomer(), param => IsEmptyField(EdNameNewCustomer)&& IsSelected(SelEditCustomer));
                }
                return ButtonEditCustomerClick;
            }
        }
        public void EditCustomer()
        {
            if(messBoxYesNo("Do you want to edit this customer?", "Edit customer"))
            {
                string mess = F_Projects.EditCustomer(ListCustomer[SelEditCustomer].Key, EdNameNewCustomer.Trim());
                if (mess == null)
                {
                    messBoxOk("Customer was edited successfully.", "Edit customer");
                    loadAllCustomer();
                    IsChanged = true;
                    EdNameNewCustomer = "";
                }
                else
                    messBoxOk(mess, "Edit customer");
                
            }
        }
        #endregion
        #region function
        public void loadAllCustomer()
        {
            ListCustomer = F_Projects.GetAllCustomers();
        }
        #endregion
        #endregion

        #region Durations
        #region fields
        string nameNewDuration;
        List<KeyValuePair<int, string>> listDuration;
        int selDelDuration;
        string edNameNewDuration;
        int selEditDuration;
        #endregion
        #region properties
        public string NameNewDuration
        {
            get
            {
                return nameNewDuration;
            }
            set
            {
                nameNewDuration = value;
                OnPropertyChanged("NameNewDuration");
            }
        }
        public List<KeyValuePair<int, string>> ListDuration
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
        public int SelDelDuration
        {
            get
            {
                return selDelDuration;
            }
            set
            {
                selDelDuration = value;
                OnPropertyChanged("SelDelDuration");
            }
        }
        public string EdNameNewDuration
        {
            get
            {
                return edNameNewDuration;
            }
            set
            {
                edNameNewDuration = value;
                OnPropertyChanged("EdNameNewDuration");
            }
        }
        public int SelEditDuration
        {
            get
            {
                return selEditDuration;
            }
            set
            {
                selEditDuration = value;
                OnPropertyChanged("SelEditDuration");
            }
        }
        #endregion
        #region Commands
        private DelegateCommand ButtonAddDurationClick;
        public ICommand BAddDuration_Click
        {
            get
            {
                if (ButtonAddDurationClick == null)
                {
                    ButtonAddDurationClick = new DelegateCommand(param => this.AddDuration(), param => IsEmptyField(NameNewDuration));
                }
                return ButtonAddDurationClick;
            }
        }
        public void AddDuration()
        {
            if(messBoxYesNo("Do you want to add this duration?", "Add new duration"))
            {
                string mess = F_Projects.AddNewDuration(NameNewDuration.Trim());
                if (mess == null)
                {
                    messBoxOk("New duration was added successfully.", "Add new duration");
                    loadAllDuration();
                    IsChanged = true;
                    NameNewDuration = "";
                }
                else
                    messBoxOk(mess, "Add new duration");
                
            }
        }
        private DelegateCommand ButtonDelDurationClick;
        public ICommand BDelDuration_Click
        {
            get
            {
                if (ButtonDelDurationClick == null)
                {
                    ButtonDelDurationClick = new DelegateCommand(param => this.DelDuration(), param => IsSelected(SelDelDuration));
                }
                return ButtonDelDurationClick;
            }
        }
        public void DelDuration()
        {
            if(messBoxYesNo("Do you want to delete this duration?", "Delete duration"))
            {
                string mess = F_Projects.DeleteDuration(ListDuration[SelDelDuration].Key);
                if (mess == null)
                {
                    messBoxOk("Duration was deleted successfully.", "Delete duration");
                    loadAllDuration();
                    IsChanged = true;
                }
                else
                    messBoxOk(mess, "Delete duration");
                
            }
        }
        private DelegateCommand ButtonEditDurationClick;
        public ICommand BEditDuration_Click
        {
            get
            {
                if (ButtonEditDurationClick == null)
                {
                    ButtonEditDurationClick = new DelegateCommand(param => this.EditDuration(), param => IsEmptyField(EdNameNewDuration) && IsSelected(SelEditDuration));
                }
                return ButtonEditDurationClick;
            }
        }
        public void EditDuration()
        {
            if(messBoxYesNo("Do you want to edit this duration?", "Edit duration"))
            {
                string mess = F_Projects.EditDuration(ListDuration[SelEditDuration].Key, EdNameNewDuration.Trim());
                if (mess == null)
                {
                    messBoxOk("Duration was edited successfully.", "Edit duration");
                    loadAllDuration();
                    IsChanged = true;
                    EdNameNewDuration = "";
                }
                else
                    messBoxOk(mess, "Edit duration");
               
            }
        }
        #endregion
        #region function
        public void loadAllDuration()
        {
            ListDuration = F_Projects.GetAllDurations();
        }
        #endregion
        #endregion

        #region Objectives
        #region fields
        string nameNewObjective;
        List<KeyValuePair<int, string>> listObjective;
        int selDelObjective;
        string edNameNewObjective;
        int selEditObjective;
        #endregion
        #region properties
        public string NameNewObjective
        {
            get
            {
                return nameNewObjective;
            }
            set
            {
                nameNewObjective = value;
                OnPropertyChanged("NameNewObjective");
            }
        }
        public List<KeyValuePair<int, string>> ListObjective
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
        public int SelDelObjective
        {
            get
            {
                return selDelObjective;
            }
            set
            {
                selDelObjective = value;
                OnPropertyChanged("SelDelObjective");
            }
        }
        public string EdNameNewObjective
        {
            get
            {
                return edNameNewObjective;
            }
            set
            {
                edNameNewObjective = value;
                OnPropertyChanged("EdNameNewObjective");
            }
        }
        public int SelEditObjective
        {
            get
            {
                return selEditObjective;
            }
            set
            {
                selEditObjective = value;
                OnPropertyChanged("SelEditObjective");
            }
        }
        #endregion
        #region Commands
        private DelegateCommand ButtonAddObjectiveClick;
        public ICommand BAddObjective_Click
        {
            get
            {
                if (ButtonAddObjectiveClick == null)
                {
                    ButtonAddObjectiveClick = new DelegateCommand(param => this.AddObjective(), param => IsEmptyField(NameNewObjective));
                }
                return ButtonAddObjectiveClick;
            }
        }
        public void AddObjective()
        {
            if(messBoxYesNo("Do you want to add this objective?", "Add new objective"))
            {
                string mess = F_Projects.AddNewObjective(NameNewObjective.Trim());
                if (mess == null)
                {
                    messBoxOk("New objective was added successfully.", "Add new objective");
                    loadAllObjective();
                    IsChanged = true;
                    NameNewObjective = "";
                }
                else
                    messBoxOk(mess, "Add new objective");
               
            }
        }
        private DelegateCommand ButtonDelObjectiveClick;
        public ICommand BDelObjective_Click
        {
            get
            {
                if (ButtonDelObjectiveClick == null)
                {
                    ButtonDelObjectiveClick = new DelegateCommand(param => this.DelObjective(), param => IsSelected(SelDelObjective));
                }
                return ButtonDelObjectiveClick;
            }
        }
        public void DelObjective()
        {
            if(messBoxYesNo("Do you want to delete this objective?", "Delete objective"))
            {
                string mess = F_Projects.DeleteObjective(ListObjective[SelDelObjective].Key);
                if (mess == null)
                {
                    messBoxOk("Objective was deleted successfully.", "Delete objective");
                    loadAllObjective();
                    IsChanged = true;
                }
                else
                    messBoxOk(mess, "Delete objective");
                
            }
        }
        private DelegateCommand ButtonEditObjectiveClick;
        public ICommand BEditObjective_Click
        {
            get
            {
                if (ButtonEditObjectiveClick == null)
                {
                    ButtonEditObjectiveClick = new DelegateCommand(param => this.EditObjective(), param => IsEmptyField(EdNameNewObjective) && IsSelected(SelEditObjective));
                }
                return ButtonEditObjectiveClick;
            }
        }
        public void EditObjective()
        {
            if(messBoxYesNo("Do you want to edit this objective?", "Edit objective"))
            {
                string mess = F_Projects.EditObjective(ListObjective[SelEditObjective].Key, EdNameNewObjective.Trim());
                if (mess == null)
                {
                    messBoxOk("Objective was edited successfully.", "Edit objective");
                    loadAllObjective();
                    IsChanged = true;
                    EdNameNewObjective = "";
                }
                else
                    messBoxOk(mess, "Edit objective");
                
            }
        }
        #endregion
        #region function
        public void loadAllObjective()
        {
            ListObjective = F_Projects.GetAllObjectives();
        }
        #endregion
        #endregion

        #region OSs
        #region fields
        string nameNewOperationSystem;
        List<KeyValuePair<int, string>> listOperationSystem;
        int selDelOperationSystem;
        string edNameNewOperationSystem;
        int selEditOperationSystem;
        #endregion
        #region properties
        public string NameNewOperationSystem
        {
            get
            {
                return nameNewOperationSystem;
            }
            set
            {
                nameNewOperationSystem = value;
                OnPropertyChanged("NameNewOperationSystem");
            }
        }
        public List<KeyValuePair<int, string>> ListOperationSystem
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
        public int SelDelOperationSystem
        {
            get
            {
                return selDelOperationSystem;
            }
            set
            {
                selDelOperationSystem = value;
                OnPropertyChanged("SelDelOperationSystem");
            }
        }
        public string EdNameNewOperationSystem
        {
            get
            {
                return edNameNewOperationSystem;
            }
            set
            {
                edNameNewOperationSystem = value;
                OnPropertyChanged("EdNameNewOperationSystem");
            }
        }
        public int SelEditOperationSystem
        {
            get
            {
                return selEditOperationSystem;
            }
            set
            {
                selEditOperationSystem = value;
                OnPropertyChanged("SelEditOperationSystem");
            }
        }
        #endregion
        #region Commands
        private DelegateCommand ButtonAddOperationSystemClick;
        public ICommand BAddOperationSystem_Click
        {
            get
            {
                if (ButtonAddOperationSystemClick == null)
                {
                    ButtonAddOperationSystemClick = new DelegateCommand(param => this.AddOperationSystem(), param => IsEmptyField(NameNewOperationSystem));
                }
                return ButtonAddOperationSystemClick;
            }
        }
        public void AddOperationSystem()
        {
            if(messBoxYesNo("Do you want to add this operation system?", "Add new OS"))
            {
                string mess = F_Projects.AddNewOS(NameNewOperationSystem);
                if (mess == null)
                {
                    messBoxOk("New operation system was added successfully.", "Add new OS");
                    loadAllOperationSystem();
                    IsChanged = true;
                    NameNewOperationSystem = "";
                }
                else
                    messBoxOk(mess, "Add new OS");
               
            }
        }
        private DelegateCommand ButtonDelOperationSystemClick;
        public ICommand BDelOperationSystem_Click
        {
            get
            {
                if (ButtonDelOperationSystemClick == null)
                {
                    ButtonDelOperationSystemClick = new DelegateCommand(param => this.DelOperationSystem(), param => IsSelected(SelDelOperationSystem));
                }
                return ButtonDelOperationSystemClick;
            }
        }
        public void DelOperationSystem()
        {
            if(messBoxYesNo("Do you want to delete this operation system?", "Delete OS"))
            {
                string mess = F_Projects.DeleteOS(ListOperationSystem[SelDelOperationSystem].Key);
                if (mess == null)
                {
                    messBoxOk("Operation system was deleted successfully.", "Delete OS");
                    loadAllOperationSystem();
                    IsChanged = true;
                }
                else
                    messBoxOk(mess, "Delete OS");
                
            }
        }
        private DelegateCommand ButtonEditOperationSystemClick;
        public ICommand BEditOperationSystem_Click
        {
            get
            {
                if (ButtonEditOperationSystemClick == null)
                {
                    ButtonEditOperationSystemClick = new DelegateCommand(param => this.EditOperationSystem(), param => IsEmptyField(EdNameNewOperationSystem) && IsSelected(SelEditOperationSystem));
                }
                return ButtonEditOperationSystemClick;
            }
        }
        public void EditOperationSystem()
        {
            if(messBoxYesNo("Do you want to edit this operation system?", "Edit OS"))
            {
                string mess = F_Projects.EditOS(ListOperationSystem[SelEditOperationSystem].Key, EdNameNewOperationSystem.Trim());
                if (mess == null)
                {
                    messBoxOk("Operation system was edited successfully.", "Edit OS");
                    loadAllOperationSystem();
                    IsChanged = true;
                    EdNameNewOperationSystem = "";
                }
                else
                    messBoxOk(mess, "Edit OS");
                
            }
        }
        #endregion
        #region function
        public void loadAllOperationSystem()
        {
            ListOperationSystem = F_Projects.GetAllOSs();
        }
        #endregion
        #endregion



        #region Stage
        #region fields
        string nameNewStage;
        List<KeyValuePair<int, string>> listStage;
        int selDelStage;
        string edNameNewStage;
        int selEditStage;
        #endregion
        #region properties
        public string NameNewStage
        {
            get
            {
                return nameNewStage;
            }
            set
            {
                nameNewStage = value;
                OnPropertyChanged("NameNewStage");
            }
        }
        public List<KeyValuePair<int, string>> ListStage
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
        public int SelDelStage
        {
            get
            {
                return selDelStage;
            }
            set
            {
                selDelStage = value;
                OnPropertyChanged("SelDelStage");
            }
        }
        public string EdNameNewStage
        {
            get
            {
                return edNameNewStage;
            }
            set
            {
                edNameNewStage = value;
                OnPropertyChanged("EdNameNewStage");
            }
        }
        public int SelEditStage
        {
            get
            {
                return selEditStage;
            }
            set
            {
                selEditStage = value;
                OnPropertyChanged("SelEditStage");
            }
        }
        #endregion
        #region Commands
        private DelegateCommand ButtonAddStageClick;
        public ICommand BAddStage_Click
        {
            get
            {
                if (ButtonAddStageClick == null)
                {
                    ButtonAddStageClick = new DelegateCommand(param => this.AddStage(), param => IsEmptyField(NameNewStage));
                }
                return ButtonAddStageClick;
            }
        }
        public void AddStage()
        {
            if(messBoxYesNo("Do you want to add this stage?", "Add new stage"))
            {
                string mess = F_Projects.AddNewStage(NameNewStage.Trim());
                if (mess == null)
                {
                    messBoxOk("New stage was added successfully.", "Add new stage");
                    loadAllStage();
                    IsChanged = true;
                    NameNewStage = "";
                }
                else
                    messBoxOk(mess, "Add new stage");
                
            }
        }
        private DelegateCommand ButtonDelStageClick;
        public ICommand BDelStage_Click
        {
            get
            {
                if (ButtonDelStageClick == null)
                {
                    ButtonDelStageClick = new DelegateCommand(param => this.DelStage(), param => IsSelected(SelDelStage));
                }
                return ButtonDelStageClick;
            }
        }
        public void DelStage()
        {
            if(messBoxYesNo("Do you want to delete this stage?", "Delete stage"))
            {
                string mess = F_Projects.DeleteStage(ListStage[SelDelStage].Key);
                if (mess == null)
                {
                    messBoxOk("Stage was deleted successfully.", "Delete stage");
                    loadAllStage();
                    IsChanged = true;
                }
                else
                    messBoxOk(mess, "Delete stage");
                
            }
        }
        private DelegateCommand ButtonEditStageClick;
        public ICommand BEditStage_Click
        {
            get
            {
                if (ButtonEditStageClick == null)
                {
                    ButtonEditStageClick = new DelegateCommand(param => this.EditStage(), param => IsEmptyField(EdNameNewStage) && IsSelected(SelEditStage));
                }
                return ButtonEditStageClick;
            }
        }
        public void EditStage()
        {
            if(messBoxYesNo("Do you want to edit this stage?", "Edit stage"))
            {
                string mess = F_Projects.EditStage(ListStage[SelEditStage].Key, EdNameNewStage.Trim());
                if (mess == null)
                {
                    messBoxOk("Stage was edited successfully.", "Edit stage");
                    loadAllStage();
                    IsChanged = true;
                    EdNameNewStage = "";
                }
                else
                    messBoxOk(mess, "Edit stage");
                
            }
        }
        #endregion
        #region function
        public void loadAllStage()
        {
            ListStage = F_Projects.GetAllStages();
        }
        #endregion
        #endregion

        #region Type
        #region fields
        string nameNewType;
        List<KeyValuePair<int, string>> listType;
        int selDelType;
        string edNameNewType;
        int selEditType;
        #endregion
        #region properties
        public string NameNewType
        {
            get
            {
                return nameNewType;
            }
            set
            {
                nameNewType = value;
                OnPropertyChanged("NameNewType");
            }
        }
        public List<KeyValuePair<int, string>> ListType
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
        public int SelDelType
        {
            get
            {
                return selDelType;
            }
            set
            {
                selDelType = value;
                OnPropertyChanged("SelDelType");
            }
        }
        public string EdNameNewType
        {
            get
            {
                return edNameNewType;
            }
            set
            {
                edNameNewType = value;
                OnPropertyChanged("EdNameNewType");
            }
        }
        public int SelEditType
        {
            get
            {
                return selEditType;
            }
            set
            {
                selEditType = value;
                OnPropertyChanged("SelEditType");
            }
        }
        #endregion
        #region Commands
        private DelegateCommand ButtonAddTypeClick;
        public ICommand BAddType_Click
        {
            get
            {
                if (ButtonAddTypeClick == null)
                {
                    ButtonAddTypeClick = new DelegateCommand(param => this.AddType(), param => IsEmptyField(NameNewType));
                }
                return ButtonAddTypeClick;
            }
        }
        public void AddType()
        {
            if(messBoxYesNo("Do you want to add this type?", "Add new type"))
            {
                string mess = F_Projects.AddNewType(NameNewType.Trim());
                if (mess == null)
                {
                    messBoxOk("New type was added successfully.", "Add new type");
                    loadAllType();
                    IsChanged = true;
                    NameNewType = "";
                }
                else
                    messBoxOk(mess, "Add new type");
                
            }
        }
        private DelegateCommand ButtonDelTypeClick;
        public ICommand BDelType_Click
        {
            get
            {
                if (ButtonDelTypeClick == null)
                {
                    ButtonDelTypeClick = new DelegateCommand(param => this.DelType(), param => IsSelected(SelDelType));
                }
                return ButtonDelTypeClick;
            }
        }
        public void DelType()
        {
            if(messBoxYesNo("Do you want to delete this type?", "Delete type"))
            {
                string mess = F_Projects.DeleteType(ListType[SelDelType].Key);
                if (mess == null)
                {
                    messBoxOk("Type was deleted successfully.", "Delete type");
                    loadAllType();
                    IsChanged = true;
                }
                else
                    messBoxOk(mess, "Delete type");
                
            }
        }
        private DelegateCommand ButtonEditTypeClick;
        public ICommand BEditType_Click
        {
            get
            {
                if (ButtonEditTypeClick == null)
                {
                    ButtonEditTypeClick = new DelegateCommand(param => this.EditType(), param => IsEmptyField(EdNameNewType) && IsSelected(SelEditType));
                }
                return ButtonEditTypeClick;
            }
        }
        public void EditType()
        {
            if(messBoxYesNo("Do you want to edit this type?", "Edit type"))
            {
                string mess = F_Projects.EditType(ListType[SelEditType].Key, EdNameNewType.Trim());
                if (mess == null)
                {
                    messBoxOk("Type was edited successfully.", "Edit type");
                    loadAllType();
                    IsChanged = true;
                    EdNameNewType = "";
                }
                else
                    messBoxOk(mess, "Edit type");
                
            }
        }
        #endregion
        #region function
        public void loadAllType()
        {
            ListType = F_Projects.GetAllTypes();
        }
        #endregion
        #endregion
    }
}
