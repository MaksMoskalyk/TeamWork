using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DelegateCommandNS;
using VM_Base;

namespace CustomMessageBox_YesNo
{
    public class VM_CustomMessageBox_YesNo : ViewModelBase
    {
        public VM_CustomMessageBox_YesNo(string caption, string message)
        {
            Caption = caption;
            Message = message;
        }
        public string Caption { get; set; }
        public string Message { get; set; }
        private bool? dialogResult;
        public bool? DialogResult
        {
            get { return dialogResult; }
            set
            {
                if (value != this.dialogResult)
                {
                    this.dialogResult = value;
                    OnPropertyChanged("DialogResult");
                }
            }
        }
        private DelegateCommand ButtonYesClick;
        public ICommand ButtonYes
        {
            get
            {
                if (ButtonYesClick == null)
                {
                    ButtonYesClick = new DelegateCommand(param => this.CloserDialogYes(), param => true);
                }
                return ButtonYesClick;
            }
        }
        private DelegateCommand ButtonNoClick;
        public ICommand ButtonNo
        {
            get
            {
                if (ButtonNoClick == null)
                {
                    ButtonNoClick = new DelegateCommand(param => this.CloserDialogNo(), param => true);
                }
                return ButtonNoClick;
            }
        }
        void CloserDialogYes()
        {
            DialogResult = true;
        }
        void CloserDialogNo()
        {
            DialogResult = false;
        }
    }
}
