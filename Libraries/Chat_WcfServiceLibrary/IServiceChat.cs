using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Chat_WcfServiceLibrary
{
    [ServiceContract(CallbackContract = typeof(IClientCallback))]
    public interface IServiceChat
    {
        [OperationContract(IsOneWay = true)]
        void sendMessage(string message);
        [OperationContract(IsOneWay = true)]
        void ConectChat(string user);
        [OperationContract(IsOneWay = true)]
        void LeaveChat(string user);
    }


    public interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void sendMessageCl(string mes);

        [OperationContract(IsOneWay = true)]
        void AddUsers(List<string> users);

        [OperationContract(IsOneWay = true)]
        void DelUser(string user);
    }
}
