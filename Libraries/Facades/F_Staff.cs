using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamworkDBEntity;
using TeamworkDB;
using System.Data.Entity;

namespace Facade
{
     public static class F_Staff
    {
        public static List<KeyValuePair<int, string>> GetTeamLeaders()
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from Employees in database.Employees
                            from Positions in database.Positions
                            where Positions.Name == "Team leader"
                            where Employees.Position == Positions
                            select new { Employees.Id, Employees.Name, Employees.Surname };
                var res = query.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name + " " + item.Surname)).ToList();
                return res;
            }
        }

        public static List<KeyValuePair<int, string>> GetUsualEmployees()
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from Employees in database.Employees
                            from Positions in database.Positions
                            where Positions.Name != "Team leader"
                            where Employees.Position==Positions
                            select new { Employees.Id, Employees.Name, Employees.Surname };
                var res = query.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name + " " + item.Surname)).ToList();
                return res;
            }
        }

         public static bool ChangeProjectLead(int idNewLead, int idProject)
         {
             using (var context = new TeamworkDBContext())
             {
                 try
                 {
                     var oldLead = (from proj in context.Projects 
                             from empProj in context.EmployeesAndProjects
                             where proj.Id==idProject
                             where empProj.Employee.Position.Name=="Team leader"
                             select empProj).SingleOrDefault();

                     if (oldLead == null)
                         return false;

                     context.EmployeesAndProjects.Remove(oldLead);

                     var newLead = (from x in context.Employees where x.Id == idNewLead select x).SingleOrDefault();
                     var project =  (from x in context.Projects where x.Id == idProject select x).SingleOrDefault();

                     if (newLead == null || project==null)
                         return false;

                     context.EmployeesAndProjects.Add(new EmployeeAndProject(newLead.Id, project.Id));
                     context.SaveChanges();
                     return true;
                 }
                 catch 
                 {
                     return false;
                 }
             }
         }

         public static KeyValuePair<int, string> GetProjectLead(int projectId)
         {
             using (var context = new TeamworkDBContext())
             {
                 var q = from emp in context.EmployeesAndProjects
                         where emp.Project_Id == projectId
                         where emp.Employee.Position.Name == "Team leader"
                         select new { emp.Employee.Id, emp.Employee.Name, emp.Employee.Surname };
                var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name + " " + item.Surname));


                return query.SingleOrDefault();
             }
         }

         public static List<KeyValuePair<int, string>> GetEmployeesFullNameWithId()
         {
             using (var context = new TeamworkDBContext())
             {
                 var q = from employees in context.Employees select new { employees.Id, employees.Name, employees.Surname };
                 var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name + " " + item.Surname)).ToList();
                 return query;
             }
         }

         public static string AddEmployeeToProject(int projectId, int employeeId)
         {
             using (var context = new TeamworkDBContext())
             {
                 try
                 {
                     var qProjEmp = from projemp in context.EmployeesAndProjects
                                    where projemp.Employee_Id == employeeId
                                    where projemp.Project_Id == projectId
                                    select projemp;

                     if (qProjEmp.Count() > 0)
                         return "Data already had added";

                     var qEmp = from employee in context.Employees where employee.Id == employeeId select employee;
                     var qProj = from proj in context.Projects where proj.Id == projectId select proj;
                     if (qEmp.Count() == 0 || qProj.Count() == 0)
                         return "Selected operation failed";

                     EmployeeAndProject empandproj = new EmployeeAndProject();
                     empandproj.Project = qProj.First();
                     empandproj.Employee = qEmp.First();
                     context.EmployeesAndProjects.Add(empandproj);
                     context.SaveChanges();
                     return "Added";
                 }
                 catch (Exception ex)
                 {
                     return "Exception: " + ex.Message;
                 }
             }
         }

         public static List<KeyValuePair<int, string>> GetProjectEmployees(int projectId)
         {
             using (var context = new TeamworkDBContext())
             {
                 var q = from x in context.EmployeesAndProjects 
                         where x.Project_Id == projectId 
                         where x.Employee.Position.Name!="Team leader"
                         select new { x.Employee.Id, x.Employee.Name, x.Employee.Surname };
                 var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name + " " + item.Surname)).ToList();
                 return query;
             }
         }

         public static List<KeyValuePair<int, string>> GetProjectAllEmployees(int projectId)
         {
             using (var context = new TeamworkDBContext())
             {
                 var q = from x in context.EmployeesAndProjects
                         where x.Project_Id == projectId
                         select new { x.Employee.Id, x.Employee.Name, x.Employee.Surname };
                 var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name + " " + item.Surname)).ToList();
                 return query;
             }
         }

         public static string RemoveEmployeeFromProject(int projectId, int employeeId)
         {
             if (employeeId < 0)
                 return "Selected operation failed";

             using (var context = new TeamworkDBContext())
             {
                 try
                 {
                     var q = from x in context.EmployeesAndProjects
                             where x.Project.Id == projectId
                             where x.Employee.Id == employeeId
                             select x;

                     if (q.Count() == 0)
                         return "Selected operation failed";
                     context.EmployeesAndProjects.Remove(q.Single());
                     context.SaveChanges();
                     return "Deleted";
                 }
                 catch (Exception ex)
                 {
                     return "Exception: " + ex.Message;
                 }
             }
         }






    }
}
