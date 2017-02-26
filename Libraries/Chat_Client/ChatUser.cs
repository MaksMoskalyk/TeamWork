using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client
{
    public class ChatUser : VM_Base
    {

        private string user;
        public ChatUser()
        {

        }
        public ChatUser(string User)
        {
            this.User = User;
        }
        public string User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }
    }
}
