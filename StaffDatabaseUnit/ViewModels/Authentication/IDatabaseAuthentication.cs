using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamworkDB;
using TeamworkDBEntity;

namespace StaffDatabaseUnit
{
    public interface IDatabaseAuthentication
    {
        Employee GetEmployeeByAccount(string login, string password);
        Employee GetEmployeeByLogin(string login);
        void AddNewEmployeeAccount(string login, string password,
            string name, string surname);
    }
}
