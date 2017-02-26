using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamworkDB;
using TeamworkDBEntity;
using System.Collections.ObjectModel;
using System.Windows;

namespace StaffDatabaseUnit
{
    public class DatabaseAuthenticationEntity : IDatabaseAuthentication
    {
        public Employee GetEmployeeByAccount(string login, string password)
        {
            Employee employee = null;
            using (var database = new TeamworkDBContext())
            {
                var query = (from Employees in database.Employees
                             from Accounts in database.Accounts
                             where Accounts.Employee_Id == Employees.Id
                             where Accounts.Login == login
                             where Accounts.Password == password
                             select Employees).SingleOrDefault();

                if (query != null)
                {
                    employee = new Employee();
                    employee = query;
                }
            }
            return employee;
        }

        public Employee GetEmployeeByLogin(string login)
        {
            Employee employee = null;
            using (var database = new TeamworkDBContext())
            {
                var query = (from Employees in database.Employees
                             from Accounts in database.Accounts
                             where Accounts.Employee_Id == Employees.Id
                             where Accounts.Login == login
                             select Employees).SingleOrDefault();

                if (query != null)
                {
                    employee = new Employee();
                    employee = query;
                }
            }
            return employee;
        }

        public void AddNewEmployeeAccount(string login, string password, 
            string name, string surname)
        {
            Employee employee = null;
            using (var database = new TeamworkDBContext())
            {
                var employeeQuery = (from Employees in database.Employees
                             where Employees.Name == name
                             where Employees.Surname == surname
                             select Employees).SingleOrDefault();

                if (employeeQuery != null)
                {
                    employee = new Employee();
                    employee = employeeQuery;

                    int accessLevel_Id;
                    if (employee.Position.Name == "Project Manager")
                    {
                        var queryAccessLevel = (from AccessLevels in database.AccessLevels
                                     where AccessLevels.Name == "Administrator"
                                     select AccessLevels).SingleOrDefault();
                        accessLevel_Id = queryAccessLevel.Id;
                    }
                    else
                    {
                        var queryAccessLevel = (from AccessLevels in database.AccessLevels
                                     where AccessLevels.Name == "User"
                                     select AccessLevels).SingleOrDefault();
                        accessLevel_Id = queryAccessLevel.Id;
                    }

                    Account userAccount = new Account(employee.Id, login, password, accessLevel_Id);
                    employee.Account = userAccount;
                }
            }
        }
    }
}
