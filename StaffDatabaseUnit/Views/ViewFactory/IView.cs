using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StaffDatabaseUnit
{
    public interface IView
    {
        void ShowView();
        void CloseView();
        object Data { get; set; }
        string Caption { get; set; }
    }
}