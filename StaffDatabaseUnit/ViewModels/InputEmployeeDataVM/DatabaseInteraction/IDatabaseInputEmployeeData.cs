using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamworkDB;

namespace StaffDatabaseUnit
{
    public interface IDatabaseInputEmployeeData
    {
        // Main methods.
        void Initiation(EmployeeData employee);
        void Update(EmployeeData employee);
        string AddEmployee(EmployeeData employee);
        bool IsUserExist(EmployeeData employeeData);
        string EditEmployee(EmployeeData employee);
        void DeleteEmployee(Employee selectedEmployee);
        void ReconnectEmployeeWithProjects(Employee selectedEmployee, Employee updatedEmployee);

        // Supplementary methods.
        void PositionsQuery(EmployeeData employee);
        void GroupFiltersQuery(EmployeeData employee);
        void SkillsQuery(EmployeeData employee);
        void CitiesQuery(EmployeeData employee);
        void LanguagesQuery(EmployeeData employee);
        void SelectDays(EmployeeData employee);    
    }
}
