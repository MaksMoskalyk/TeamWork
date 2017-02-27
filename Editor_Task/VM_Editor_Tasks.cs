using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CustomMessageBox_OK;
using CustomMessageBox_YesNo;
using DelegateCommandNS;
using Facade;
using VM_Base;

namespace Editor_Task
{
    public class VM_Editor_Tasks : ViewModelBase
    {
        public bool IsChanged { set; get; }
        public VM_Editor_Tasks()
        {
            loadAllPriority();
            loadAllStatus();
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
        #region Prioritys
        #region fields
        string nameNewPriority;
        List<KeyValuePair<int, string>> listPriority;
        int selDelPriority;
        string edNameNewPriority;
        int selEditPriority;
        #endregion
        #region properties
        public string NameNewPriority
        {
            get
            {
                return nameNewPriority;
            }
            set
            {
                nameNewPriority = value;
                OnPropertyChanged("NameNewPriority");
            }
        }
        public List<KeyValuePair<int, string>> ListPriority
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
        public int SelDelPriority
        {
            get
            {
                return selDelPriority;
            }
            set
            {
                selDelPriority = value;
                OnPropertyChanged("SelDelPriority");
            }
        }
        public string EdNameNewPriority
        {
            get
            {
                return edNameNewPriority;
            }
            set
            {
                edNameNewPriority = value;
                OnPropertyChanged("EdNameNewPriority");
            }
        }
        public int SelEditPriority
        {
            get
            {
                return selEditPriority;
            }
            set
            {
                selEditPriority = value;
                OnPropertyChanged("SelEditPriority");
            }
        }
        #endregion
        #region Commands
        private DelegateCommand ButtonAddPriorityClick;
        public ICommand BAddPriority_Click
        {
            get
            {
                if (ButtonAddPriorityClick == null)
                {
                    ButtonAddPriorityClick = new DelegateCommand(param => this.AddPriority(), param => IsEmptyField(NameNewPriority));
                }
                return ButtonAddPriorityClick;
            }
        }
        public void AddPriority()
        {
            
            if (messBoxYesNo("Do you want to add this priority?", "Add new priority"))
            {
                string mess = F_Task.AddNewPriority(NameNewPriority.Trim());
                if (mess == null)
                {
                    messBoxOk("New priority was added successfully.", "Add new priority");
                    loadAllPriority();
                    IsChanged = true;
                    NameNewPriority = "";
                }
                else
                    messBoxOk(mess, "Add new priority");
                
            }
        }
        private DelegateCommand ButtonDelPriorityClick;
        public ICommand BDelPriority_Click
        {
            get
            {
                if (ButtonDelPriorityClick == null)
                {
                    ButtonDelPriorityClick = new DelegateCommand(param => this.DelPriority(), param => IsSelected(SelDelPriority));
                }
                return ButtonDelPriorityClick;
            }
        }
        public void DelPriority()
        {
           
            if (messBoxYesNo("Do you want to delete this priority?", "Delete priority"))
            {
                string mess = F_Task.DeletePriority(ListPriority[SelDelPriority].Key);
                if (mess == null)
                {
                    messBoxOk("Priority was deleted successfully.", "Delete priority");
                    loadAllPriority();
                    IsChanged = true;
                }
                else
                    messBoxOk(mess, "Delete priority");
               
            }
        }
        private DelegateCommand ButtonEditPriorityClick;
        public ICommand BEditPriority_Click
        {
            get
            {
                if (ButtonEditPriorityClick == null)
                {
                    ButtonEditPriorityClick = new DelegateCommand(param => this.EditPriority(), param => IsEmptyField(EdNameNewPriority) && IsSelected(SelEditPriority));
                }
                return ButtonEditPriorityClick;
            }
        }
        public void EditPriority()
        {
           
            if (messBoxYesNo("Do you want to edit this status?", "Edit status"))
            {
                string mess = F_Task.EditPriority(ListPriority[SelEditPriority].Key, EdNameNewPriority.Trim());
                if (mess == null)
                {
                    messBoxOk("Status was edited successfully.", "Edit status");
                    loadAllPriority();
                    IsChanged = true;
                    EdNameNewPriority = "";
                }
                else
                    messBoxOk(mess, "Edit status");
               
            }
        }
        #endregion
        #region function
        public void loadAllPriority()
        {
            ListPriority = F_Task.GetAllPriorityes();
        }
        #endregion
        #endregion

        #region Status
        #region fields
        string nameNewStatus;
        List<KeyValuePair<int, string>> listStatus;
        int selDelStatus;
        string edNameNewStatus;
        int selEditStatus;
        #endregion
        #region properties
        public string NameNewStatus
        {
            get
            {
                return nameNewStatus;
            }
            set
            {
                nameNewStatus = value;
                OnPropertyChanged("NameNewStatus");
            }
        }
        public List<KeyValuePair<int, string>> ListStatus
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
        public int SelDelStatus
        {
            get
            {
                return selDelStatus;
            }
            set
            {
                selDelStatus = value;
                OnPropertyChanged("SelDelStatus");
            }
        }
        public string EdNameNewStatus
        {
            get
            {
                return edNameNewStatus;
            }
            set
            {
                edNameNewStatus = value;
                OnPropertyChanged("EdNameNewStatus");
            }
        }
        public int SelEditStatus
        {
            get
            {
                return selEditStatus;
            }
            set
            {
                selEditStatus = value;
                OnPropertyChanged("SelEditStatus");
            }
        }
        #endregion
        #region Commands
        private DelegateCommand ButtonAddStatusClick;
        public ICommand BAddStatus_Click
        {
            get
            {
                if (ButtonAddStatusClick == null)
                {
                    ButtonAddStatusClick = new DelegateCommand(param => this.AddStatus(), param => IsEmptyField(NameNewStatus));
                }
                return ButtonAddStatusClick;
            }
        }
        public void AddStatus()
        {
            if (messBoxYesNo("Do you want to add this status?", "Add new status"))
            {
                string mess = F_Task.AddNewStatus(NameNewStatus.Trim());
                if (mess == null)
                {
                    messBoxOk("New status was added successfully.", "Add new status");
                    loadAllStatus();
                    IsChanged = true;
                    NameNewStatus = "";
                }
                else
                    messBoxOk(mess, "Add new status");
               
            }
        }
        private DelegateCommand ButtonDelStatusClick;
        public ICommand BDelStatus_Click
        {
            get
            {
                if (ButtonDelStatusClick == null)
                {
                    ButtonDelStatusClick = new DelegateCommand(param => this.DelStatus(), param => IsSelected(SelDelStatus));
                }
                return ButtonDelStatusClick;
            }
        }
        public void DelStatus()
        {
            if (messBoxYesNo("Do you want to delete this status?", "Delete status"))
            {
                string mess = F_Task.DeleteStatus(ListStatus[SelDelStatus].Key);
                if (mess == null)
                {
                    messBoxOk("Status was deleted successfully.", "Delete status");
                    loadAllStatus();
                    IsChanged = true;
                }
                else
                    messBoxOk(mess, "Delete status");
                
            }
        }
        private DelegateCommand ButtonEditStatusClick;
        public ICommand BEditStatus_Click
        {
            get
            {
                if (ButtonEditStatusClick == null)
                {
                    ButtonEditStatusClick = new DelegateCommand(param => this.EditStatus(), param => IsEmptyField(EdNameNewStatus) && IsSelected(SelEditStatus));
                }
                return ButtonEditStatusClick;
            }
        }
        public void EditStatus()
        {
            if (messBoxYesNo("Do you want to edit this status?", "Edit status"))
            {
                string mess = F_Task.EditStatus(ListStatus[SelEditStatus].Key, EdNameNewStatus.Trim());
                if (mess == null)
                {
                    messBoxOk("Status was edited successfully.", "Edit status");
                    loadAllStatus();
                    IsChanged = true;
                    EdNameNewStatus = "";
                }
                else
                    messBoxOk(mess, "Edit status");
                
            }
        }
        #endregion
        #region function
        public void loadAllStatus()
        {
            ListStatus = F_Task.GetAllStatuses();
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
            if (messBoxYesNo("Do you want to add this type?", "Add new type"))
            {
                string mess = F_Task.AddNewType(NameNewType.Trim());
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
            if (messBoxYesNo("Do you want to delete this type?", "Delete type"))
            {
                string mess = F_Task.DeleteType(ListType[SelDelType].Key);
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
           
            if (messBoxYesNo("Do you want to edit this type?", "Edit type"))
            {
                string mess = F_Task.EditType(ListType[SelEditType].Key, EdNameNewType.Trim());
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
            ListType = F_Task.GetAllTaskTypes(); 
        }
        #endregion
        #endregion

    }
}
