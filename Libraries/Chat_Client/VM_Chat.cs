using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Chat_Client.ServiceReference1;

namespace Chat_Client
{
    public class VM_Chat : VM_Base, IServiceChatCallback
    {
        public SynchronizationContext uiContext;
        public ChatInfo Chat;
        InstanceContext site;
        ServiceChatClient proxy ;
        public VM_Chat()
        {
            Chat = new ChatInfo();
            uiContext = SynchronizationContext.Current;
            UserLogin = "TestUser";
            isInChat = false;
            isEnabledEnter = true;
            site = new InstanceContext(this);
            proxy = new ServiceChatClient(site, "wsEndpoint");
        }
        private string szUserLogin;
        public string UserLogin
        {
            get
            {
                return szUserLogin;
            }
            set
            {
                szUserLogin = value;
                OnPropertyChanged("UserLogin");
            }
        }
        private string szMessText;
        public string MessText
        {
            get
            {
                return szMessText;
            }
            set
            {
                szMessText = value;
                OnPropertyChanged("MessText");
            }
        }

        private bool IsInChat;
        public bool isInChat
        {
            get
            {
                return IsInChat;
            }
            set
            {
                IsInChat = value;
                OnPropertyChanged("isInChat");
            }
        }
        private bool IsEnabledEnter;
        public bool isEnabledEnter
        {
            get
            {
                return IsEnabledEnter;
            }
            set
            {
                IsEnabledEnter = value;
                OnPropertyChanged("isEnabledEnter");
            }
        }
        public ObservableCollection<ChatUser> Users
        {
            get
            {
                return Chat.user;
            }
            set
            {
                Chat.user = value;
                OnPropertyChanged("Users");
            }
        }

        public ObservableCollection<ChatMessage> ChatMess
        {
            get
            {
                return Chat.message;
            }
            set
            {
                Chat.message = value;
                OnPropertyChanged("ChatMess");
            }
        }
        private DelegateCommand ButtonSend;
        public ICommand BSend
        {
            get
            {
                if (ButtonSend == null)
                {
                    ButtonSend = new DelegateCommand(param => SendMessage(), null);
                }
                return ButtonSend;
            }
        }


        // отправление сообщения
        private async void SendMessage()
        {
            await Task.Run(() =>
            {
                try
                {
                    proxy.sendMessageAsync(UserLogin+" : "+MessText);
                }
                catch (Exception ex)
                {
                    uiContext.Send(d => ChatMess.Add(new ChatMessage("System " + ex.Message)), null);
                }
            });
        }
        private DelegateCommand ButtonEnterChat;
        public ICommand BEnterChat
        {
            get
            {
                if (ButtonEnterChat == null)
                {
                    ButtonEnterChat = new DelegateCommand(param => EnterChat(), null);
                }
                return ButtonEnterChat;
            }
        }
        public void sendMessageCl(string mes)
        {
            if (isEnabledEnter)
                return;
            uiContext.Send(d => ChatMess.Add(new ChatMessage(mes)), null);
        }

        public void AddUsers(string[] users)
        {
            if (isEnabledEnter)
                return;
            uiContext.Send(d => Users.Clear(), null);
            foreach (var temp in users)
                uiContext.Send(d => Users.Add(new ChatUser(temp)), null);
        }

        public void DelUser(string user)
        {
            for (int i =0; ;i++)
            {
                if (Users[i].User == user)
                {
                    uiContext.Send(d => Users.RemoveAt(i), null);
                    break;
                }
            }
        }
        private async void EnterChat()
        {
            await Task.Run(() =>
            {
                try
                {
                    uiContext.Send(d => isInChat = true, null);
                    uiContext.Send(d => isEnabledEnter = false, null);
                    proxy.ConectChatAsync(UserLogin);

                }
                catch (Exception ex)
                {
                    uiContext.Send(d => ChatMess.Add(new ChatMessage("System " + ex.Message)), null);
                }
            });
        }
        private DelegateCommand ButtonLeaveChat;
        public ICommand BLeaveChat
        {
            get
            {
                if (ButtonLeaveChat == null)
                {
                    ButtonLeaveChat = new DelegateCommand(param => LeaveChat(), null);
                }
                return ButtonLeaveChat;
            }
        }
        ~VM_Chat()
        {
            ClosedChat();
        }
        public void ClosedChat()
        {
            try
            {
                proxy.LeaveChat(UserLogin);
            }
            catch (Exception ex) { }
        }
        private async void LeaveChat()
        {
            await Task.Run(() =>
            {
                try
                {
                    proxy.LeaveChatAsync(UserLogin);
                    uiContext.Send(d => ChatMess.Clear(), null);
                    uiContext.Send(d => isInChat = false, null);
                    uiContext.Send(d => isEnabledEnter = true, null);
                    uiContext.Send(d => Users.Clear(), null);
                }
                catch (Exception ex)
                {
                    uiContext.Send(d => ChatMess.Add(new ChatMessage("System " + ex.Message)), null);
                }
            });
        }
    }
}
