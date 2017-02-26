using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CustomMessageBox_OK;
using CustomMessageBox_YesNo;

namespace StaffDatabaseUnit
{
    public class CustomMessageBox : IMessageBox
    {
        public bool GetActionConfirmation(string message)
        {
            MessageBox_YesNo MB_YesNo = new MessageBox_YesNo();
            VM_CustomMessageBox_YesNo VM_YesNo = new VM_CustomMessageBox_YesNo("", message);
            MB_YesNo.DataContext = VM_YesNo;
            if ((bool)MB_YesNo.ShowDialog())
                return true;
            else
                return false;
        }

        public bool GetActionConfirmation(string message, string caption)
        {
            MessageBox_YesNo MB_YesNo = new MessageBox_YesNo();
            VM_CustomMessageBox_YesNo VM_YesNo = new VM_CustomMessageBox_YesNo(caption, message);
            MB_YesNo.DataContext = VM_YesNo;
            if ((bool)MB_YesNo.ShowDialog())
                return true;
            else
                return false;
        }

        public void ShowNotification(string message)
        {
            MessageBox_OK MB_OK = new MessageBox_OK();
            VM_CustomMessageBox_OK VM_OK = new VM_CustomMessageBox_OK("", message);
            MB_OK.DataContext = VM_OK;
            if ((bool)MB_OK.ShowDialog()) { }
        }

        public void ShowNotification(string message, string caption)
        {
            MessageBox_OK MB_OK = new MessageBox_OK();
            VM_CustomMessageBox_OK VM_OK = new VM_CustomMessageBox_OK(caption, message);
            MB_OK.DataContext = VM_OK;
            if ((bool)MB_OK.ShowDialog()) { }
        }
    }
}
