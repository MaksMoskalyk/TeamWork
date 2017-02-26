using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ProgramMessenger_WcfSL
{
    [ServiceContract(CallbackContract = typeof(IClientCallbackProgramMessenger))]
    public interface IServiceProgramMessenger
    {
        [OperationContract(IsOneWay = true)]
        void sendSystemMessageSPM(string message);

        [OperationContract(IsOneWay = true)]
        void ConnectSPM(string user);

        [OperationContract(IsOneWay = true)]
        void DisconnectSPM(string user);

        [OperationContract(IsOneWay = true)]
        void UpdateProjectsSPM();

        [OperationContract(IsOneWay = true)]
        void UpdateTasksSPM();

        [OperationContract(IsOneWay = true)]
        void UpdateEmployeesSPM();
    }


    public interface IClientCallbackProgramMessenger
    {
        [OperationContract(IsOneWay = true)]
        void sendSystemMessageCB(string mes);

        [OperationContract(IsOneWay = true)]
        void UpdateProjectsCB();

        [OperationContract(IsOneWay = true)]
        void UpdateTasksCB();

        [OperationContract(IsOneWay = true)]
        void UpdateEmployeesCB();
    }
}
