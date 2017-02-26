using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client
{
    public class ChatMessage : VM_Base
    {
        private string mes;
        public ChatMessage()
        {

        }
        public ChatMessage(string MessText)
        {
            this.MessText = MessText;
        }
        public string MessText
        {
            get
            {
                return mes;
            }
            set
            {
                mes = value;
                OnPropertyChanged("MessText");
            }
        }
    }
}
