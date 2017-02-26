using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffDatabaseUnit
{
    public interface IMessageBox
    {
        bool GetActionConfirmation(string message);
        bool GetActionConfirmation(string message, string caption);
        void ShowNotification(string message);
        void ShowNotification(string message, string caption);
    }
}
