using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamWork
{


       

    
    public class treeElem
    {
        public treeElem()
        {
            Name = "";
            isCatagory = false;
            subEl = new ObservableCollection<treeElem>();
            id = -1;
        }
        public treeElem(string Name, ObservableCollection<treeElem> subEl, bool isCatagory = false)
        {
            this.Name = Name;
            this.isCatagory = isCatagory;
            this.subEl = subEl;
            id = -1;
        }
        public treeElem(int id, string Name, ObservableCollection<treeElem> subEl, bool isCatagory = false)
        {
            this.id = id;

            this.Name = Name;
            this.isCatagory = isCatagory;
            this.subEl = subEl;
        }
        public bool isCatagory { get; set; }
        public string Name { get; set; }
        public int id { get; set; }
        public ObservableCollection<treeElem> subEl { get; set; }

    }
}
