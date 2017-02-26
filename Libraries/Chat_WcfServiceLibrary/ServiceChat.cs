using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace Chat_WcfServiceLibrary
{
    // класс службы, реализующий контракт
    public class ServiceChat : IServiceChat
    {
        static List<DataValues>  src = new List<DataValues>();
        static List<string> users = new List<string>();
        
        public void sendMessage(string message)
        {
            foreach(var temp in src)
                temp.sendMessage(message);
        }

        public void ConectChat(string user)
        {

            src.Add(new DataValues());
            src[src.Count - 1].callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
            if (users.IndexOf(user) > -1)
            {
                return;
            }
            users.Add(user);
            foreach (var temp in src)
            {
                temp.AddUsers(users);
                temp.sendMessage(user + " Enter in the chat");
            }
        }
        
        public void LeaveChat(string user)
        {

            int i = users.IndexOf(user);
            users.RemoveAt(i);
            src.RemoveAt(i);
            foreach (var temp in src)
            {
                temp.DelUser(user);
                temp.sendMessage(user + " Leave the chat");
            }
        }
    }


    public class DataValues
    {
        //ссылка на контракт обратного вызова
        public IClientCallback callback = null;

        public void sendMessage(string message)
        {
            try
            {
                callback.sendMessageCl(message);
            }
            catch (Exception ex)
            {
                callback.sendMessageCl("Service: " + ex.Message);
            }
        }
        public void AddUsers(List<string> users)
        {
            try
            {
                callback.AddUsers(users);
            }
            catch (Exception ex)
            {
                callback.sendMessageCl("Service: " + ex.Message);
            }
        }

        public void DelUser(string user)
        {
            try
            {
                callback.DelUser(user);
            }
            catch (Exception ex)
            {
                callback.sendMessageCl("Service: " + ex.Message);
            }
        }
    }
}
