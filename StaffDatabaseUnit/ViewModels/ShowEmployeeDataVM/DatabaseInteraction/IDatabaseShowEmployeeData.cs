using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TeamworkDB;

namespace StaffDatabaseUnit
{
    public interface IDatabaseShowEmployeeData
    {
        void LoadEmployeesData(ObservableCollection<SpecificEmployee> employees);
        void DeleteEmployee(Employee selectedEmployee);

        void LoadPositions(ObservableCollection<Position> positions, Position selectedPosition);
        void LoadSkillsList(ObservableCollection<SkillElement> skills);
        void SearchEmployees(ObservableCollection<SpecificEmployee> employees, Position selectedPosition,
            ObservableCollection<SkillElement> skills);

        void LoadDataForEmployeeEdition(EmployeeData employeeData, SpecificEmployee currentEmployee);
    }
}
