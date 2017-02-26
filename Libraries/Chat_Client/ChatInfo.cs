using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client
{
    public class ChatInfo
    {
        public ChatInfo()
        {
            user = new ObservableCollection<ChatUser>();
            message = new ObservableCollection<ChatMessage>();
        }
        public ObservableCollection<ChatUser> user;

        public ObservableCollection<ChatMessage> message;
    }
}
