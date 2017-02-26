using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class AccessLevel : IElement
    {
        private int id;
        private string name;


        public AccessLevel() { }

        public AccessLevel(string name)
        {
            this.name = name;
        }


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
