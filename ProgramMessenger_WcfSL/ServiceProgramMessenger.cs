using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ProgramMessenger_WcfSL
{
    public class ServiceProgramMessenger : IServiceProgramMessenger
    {
        static List<DataValuesProgramMessenger> src = new List<DataValuesProgramMessenger>();

        static List<string> users = new List<string>();

        public void sendSystemMessageSPM(string message)
        {
            foreach (var temp in src)
                temp.sendSystemMessage(message);
        }

        public void ConnectSPM(string user)
        {
            src.Add(new DataValuesProgramMessenger());
            src[src.Count - 1].callback = OperationContext.Current.GetCallbackChannel<IClientCallbackProgramMessenger>();
            if (users.IndexOf(user) > -1)
            {
                return;
            }
            users.Add(user);
            src[src.Count - 1].sendSystemMessage("Welcome in program TeamWork");
        }

        public void DisconnectSPM(string user)
        {

            int i = users.IndexOf(user);
            users.RemoveAt(i);
            src.RemoveAt(i);
        }
        public void UpdateProjectsSPM()
        {
            foreach (var temp in src)
                temp.UpdateProjects();
        }
        public void UpdateTasksSPM()
        {
            foreach (var temp in src)
                temp.UpdateTasks();
        }
        public void UpdateEmployeesSPM()
        {
            foreach (var temp in src)
                temp.UpdateTasks();
        }
    }


    public class DataValuesProgramMessenger
    {
        
        public IClientCallbackProgramMessenger callback = null;

        public void sendSystemMessage(string message)
        {
            try
            {
                callback.sendSystemMessageCB(message);
            }
            catch (Exception ex)
            {
                callback.sendSystemMessageCB("Service program messenger: " + ex.Message);
            }
        }
        public void UpdateProjects()
        {
            try
            {
                callback.UpdateProjectsCB();
            }
            catch (Exception ex)
            {
                callback.sendSystemMessageCB("Service program messenger: " + ex.Message);
            }
        }
        public void UpdateTasks()
        {
            try
            {
                callback.UpdateTasksCB();
            }
            catch (Exception ex)
            {
                callback.sendSystemMessageCB("Service program messenger: " + ex.Message);
            }
        }
        public void UpdateeEmployees()
        {
            try
            {
                callback.UpdateEmployeesCB();
            }
            catch (Exception ex)
            {
                callback.sendSystemMessageCB("Service program messenger: " + ex.Message);
            }
        }
    }
}
