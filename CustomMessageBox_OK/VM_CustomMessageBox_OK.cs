using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DelegateCommandNS;
using VM_Base;

namespace CustomMessageBox_OK
{
    public class VM_CustomMessageBox_OK : ViewModelBase
    {
        public VM_CustomMessageBox_OK(string caption, string message)
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
        private DelegateCommand ButtonOkClick;
        public ICommand ButtonOk
        {
            get
            {
                if (ButtonOkClick == null)
                {
                    ButtonOkClick = new DelegateCommand(param => this.CloserDialog(), param => true);
                }
                return ButtonOkClick;
            }
        }

        void CloserDialog()
        {
            DialogResult = true;
        }

    }
}
