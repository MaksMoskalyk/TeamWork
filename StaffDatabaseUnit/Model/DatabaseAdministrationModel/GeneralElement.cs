using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamworkDB;
using TeamworkDBEntity;
using VM_Base;
using DelegateCommandNS;
namespace StaffDatabaseUnit
{
    public class GeneralElement : ViewModelBase
    {
        private IElement categoryElement;
        private bool isChecked;

        public GeneralElement() { }

        public IElement CategoryElement
        {
            get { return categoryElement; }
            set { categoryElement = value; }
        }

        public bool IsChecked
        {
            get { return isChecked; }
            set 
            { 
                isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
    }
}
