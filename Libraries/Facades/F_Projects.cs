using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TeamworkDBEntity;
using TeamworkDB;

namespace Facade
{
    public static class F_Projects
    {
        //может принять List<int> с count = 0 и должно проверить(если это необходимо)
        public static List<KeyValuePair<int, string>> GetAllProjectsNameFilter(string name, List<int> customers, List<int> durations,
                                                                List<int> objectives, List<int> OSs, List<int> skills,
                                                                List<int> stages, List<int> types)
        {

            if (customers.Count()==0||customers == null)
                customers = GetAllCustomers().Select(x=>x.Key).ToList();

            if (durations.Count() == 0 || durations == null)
                durations = GetAllDurations().Select(x => x.Key).ToList();

            if (objectives.Count() == 0 || objectives == null)
                objectives = GetAllObjectives().Select(x => x.Key).ToList();

            if (stages.Count() == 0 || stages == null)
                stages = GetAllStages().Select(x => x.Key).ToList();

            using (var context = new TeamworkDBContext())
            {
                var q = from projects in context.Projects
                        where projects.Name.Contains(name.Trim())
                        where customers.Contains(projects.Customer.Id)
                        where durations.Contains(projects.Duration.Id)
                        where objectives.Contains(projects.Objective.Id)
                        where stages.Contains(projects.Stage.Id)
                        select new { projects.Id, projects.Name };

                List<Project> projFilter = new List<Project>();
                
                if (OSs.Count() != 0 && OSs != null)
                {
                    List<OperationSystem> qOs = (from x in context.OperationSystems where OSs.Contains(x.Id) select x).ToList();
                    foreach (var item in qOs.Select(x => x.Projects).ToList())
                    {
                        foreach (var i in item)
                        {
                            projFilter.Add(i);
                        }
                    }
                }
                
                if (skills.Count() != 0 && skills != null)
                {
                    List<Skill> qSkills = (from x in context.Skills where skills.Contains(x.Id) select x).ToList();
                    foreach (var item in qSkills.Select(x => x.Projects).ToList())
                    {
                        foreach (var i in item)
                        {
                            projFilter.Add(i);
                        }
                    }
                }
               
                if (types.Count() != 0 && types != null)
                {
                    List<ProjectType> qTypes = (from x in context.ProjectTypes where types.Contains(x.Id) select x).ToList();
                    foreach (var item in qTypes.Select(x => x.Projects).ToList())
                    {
                        foreach (var i in item)
                        {
                            projFilter.Add(i);
                        }
                    }
                }
                projFilter.Distinct();

                List<KeyValuePair<int, string>> projFilterKeyValue = new List<KeyValuePair<int, string>>();
                foreach (var item in projFilter)
                {
                    projFilterKeyValue.Add(new KeyValuePair<int, string>(item.Id, item.Name));
                }

                List<KeyValuePair<int, string>> prjts = q.AsEnumerable().Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList();
                IEnumerable<KeyValuePair<int, string>> filtered = null;

                if (projFilter.Count()==0)
                    filtered = projFilterKeyValue.Intersect(prjts);
                else
                    filtered = prjts;

                return filtered.ToList();
            }
        }
        #region Customers
        public static string AddNewCustomer(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)) return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from customer in context.Customers where customer.Name == name.Trim() select customer;
                    if (q.Count() > 0)
                        return "Selected operation failed";
                    ProjectCustomer c = new ProjectCustomer();
                    c.Name = name.Trim();
                    context.Customers.Add(c);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static List<KeyValuePair<int, string>> GetAllCustomers()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from customers in context.Customers select new { customers.Id, customers.Name };
                var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();
                return query;
            }
        }

       

        public static string EditCustomer(int id, string newName)
        {
            if (id < 0 || string.IsNullOrWhiteSpace(newName) || string.IsNullOrEmpty(newName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var customersWithNewName = from customers in context.Customers where customers.Name == newName.Trim() select customers;
                    if (customersWithNewName.Count() > 0)
                        return "Selected operation failed";

                    var q = (from customer in context.Customers where customer.Id == id select customer).Single();
                    q.Name = newName.Trim();
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        

        public static string DeleteCustomer(int id)
        {
            if (id<0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from customer in context.Customers where customer.Id == id select customer;
                    if (q.Count() == 0)
                        return "Selected operation failed";
                    context.Customers.Remove(q.Single());
                    context.SaveChanges();
                   return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        

        #endregion

        #region Durations
        public static string AddNewDuration(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)) return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from duration in context.Durations where duration.Name == name.Trim() select duration;
                    if (q.Count() > 0)
                        return "Selected operation failed";
                    ProjectDuration d = new ProjectDuration();
                    d.Name = name.Trim();
                    context.Durations.Add(d);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static List<KeyValuePair<int, string>> GetAllDurations()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from durations in context.Durations select new { durations.Id, durations.Name };
                var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();
                return query;
            }
        }

       

        public static string EditDuration(int id, string newName)
        {
            if (id < 0 || string.IsNullOrWhiteSpace(newName) || string.IsNullOrEmpty(newName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var durationsWithNewName = from durations in context.Durations where durations.Name == newName.Trim() select durations;
                    if (durationsWithNewName.Count() > 0)
                        return "Selected operation failed";

                    var q = (from duration in context.Durations where duration.Id == id select duration).Single();
                    q.Name = newName.Trim();
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }


        public static string DeleteDuration(int id)
        {
            if (id < 0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from duration in context.Durations where duration.Id == id select duration;
                    if (q.Count() == 0)
                        return "Selected operation failed";
                    context.Durations.Remove(q.Single());
                    context.SaveChanges();
                   return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

      

        #endregion

        #region Objectives
        public static string AddNewObjective(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)) return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from objective in context.Objectives where objective.Name == name.Trim() select objective;
                    if (q.Count() > 0)
                        return "Selected operation failed";
                    ProjectObjective o = new ProjectObjective();
                    o.Name = name.Trim();
                    context.Objectives.Add(o);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static List<KeyValuePair<int, string>> GetAllObjectives()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from objectives in context.Objectives select new { objectives.Id, objectives.Name };
                var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();
                return query;
            }
        }

      

        public static string EditObjective(int id, string newName)
        {
            if (id <0 ||
                string.IsNullOrWhiteSpace(newName) || string.IsNullOrEmpty(newName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var objectivesWithNewName = from objectives in context.Objectives where objectives.Name == newName.Trim() select objectives;
                    if (objectivesWithNewName.Count() > 0)
                        return "Selected operation failed";

                    var q = (from objective in context.Objectives where objective.Id == id select objective).Single();
                    q.Name = newName.Trim();
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

      

        public static string DeleteObjective(int id)
        {
            if (id <0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from objective in context.Objectives where objective.Id == id select objective;
                    if (q.Count() == 0)
                        return "Selected operation failed";
                    context.Objectives.Remove(q.Single());
                    context.SaveChanges();
                   return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

       
        #endregion

        #region OSs
        public static string AddNewOS(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)) return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from os in context.OperationSystems where os.Name == name.Trim() select os;
                    if (q.Count() > 0)
                        return "Selected operation failed";
                    OperationSystem o = new OperationSystem();
                    o.Name = name.Trim();
                    context.OperationSystems.Add(o);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static List<KeyValuePair<int, string>> GetAllOSs()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from OSs in context.OperationSystems select new { OSs.Id, OSs.Name };
               return q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList(); ;
            }
        }

       

        public static string EditOS(int id, string newName)
        {
            if (id <0 ||
                string.IsNullOrWhiteSpace(newName) || string.IsNullOrEmpty(newName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var OSsWithNewName = from OSs in context.OperationSystems where OSs.Name == newName.Trim() select OSs;
                    if (OSsWithNewName.Count() > 0)
                        return "Selected operation failed";

                    var q = (from os in context.OperationSystems where os.Id == id select os).Single();
                    q.Name = newName.Trim();
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }


        public static string DeleteOS(int id)
        {
            if (id < 0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from os in context.OperationSystems where os.Id == id select os;
                    if (q.Count() == 0)
                        return "Selected operation failed";
                    context.OperationSystems.Remove(q.Single());
                    context.SaveChanges();
                   return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }
        #endregion

        #region Skills
        public static List<KeyValuePair<int, string>> GetAllSkills()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from skills in context.Skills select new { skills.Id, skills.Name };
               return q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList(); ;
            }
        }        
        #endregion

        #region Stage
        public static string AddNewStage(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)) return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from stage in context.Stages where stage.Name == name.Trim() select stage;
                    if (q.Count() > 0)
                        return "Selected operation failed";
                    ProjectStage s = new ProjectStage();
                    s.Name = name.Trim();
                    context.Stages.Add(s);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static List<KeyValuePair<int, string>> GetAllStages()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from stage in context.Stages select new { stage.Id, stage.Name };
               return q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList(); ;
            }
        }

        public static List<KeyValuePair<int, string>> GetAllStagesNameWithId()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from stage in context.Stages select new { stage.Id, stage.Name };
                var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();
                return query;
            }
        }

        public static string EditStage(int id, string newName)
        {
            if (id <0 ||
                string.IsNullOrWhiteSpace(newName) || string.IsNullOrEmpty(newName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var stagesWithNewName = from stages in context.Skills where stages.Name == newName.Trim() select stages;
                    if (stagesWithNewName.Count() > 0)
                        return "Selected operation failed";

                    var q = (from stages in context.Stages where stages.Id == id select stages).Single();
                    q.Name = newName.Trim();
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }


        public static string DeleteStage(int id)
        {
            if (id < 0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from stage in context.Stages where stage.Id == id select stage;
                    if (q.Count() == 0)
                        return "Selected operation failed";
                    context.Stages.Remove(q.Single());
                    context.SaveChanges();
                   return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }
        #endregion

        #region Type
        public static string AddNewType(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)) return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from type in context.ProjectTypes where type.Name == name.Trim() select type;
                    if (q.Count() > 0)
                        return "Selected operation failed";
                    ProjectType t = new ProjectType();
                    t.Name = name.Trim();
                    context.ProjectTypes.Add(t);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static List<KeyValuePair<int, string>> GetAllTypes()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from types in context.ProjectTypes select new { types.Id, types.Name };
               return q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList(); ;
            }
        }

        public static List<KeyValuePair<int, string>> GetAllTypesNameWithId()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from types in context.ProjectTypes select new { types.Id, types.Name };
                var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();
                return query;
            }
        }

        public static string EditType(int id, string newName)
        {
            if (id <0 ||
                string.IsNullOrWhiteSpace(newName) || string.IsNullOrEmpty(newName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var typesWithNewName = from types in context.ProjectTypes where types.Name == newName.Trim() select types;
                    if (typesWithNewName.Count() > 0)
                        return "Selected operation failed";

                    var q = (from type in context.ProjectTypes where type.Id == id select type).Single();
                    q.Name = newName.Trim();
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }


        public static string DeleteType(int id)
        {
            if (id < 0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from type in context.ProjectTypes where type.Id == id select type;
                    if (q.Count() == 0)
                        return "Selected operation failed";
                    context.ProjectTypes.Remove(q.Single());
                    context.SaveChanges();
                   return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }
        #endregion

        #region Project

        public static string AddNewProject(string name, int customerID, int durationID, int stageID, int objectiveID,
            DateTime? dueDate, string description, int LeadID, List<int> OssID, List<int> SkillsID, List<int> TypesID, List<int> EmployeesID)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qProj = from project in context.Projects where project.Name == name.Trim() select project;
                    if (qProj.Count() > 0)
                        return "Selected operation failed";

                    var qCustomer = (from cust in context.Customers where cust.Id == customerID select cust).Single();
                    var qDuration = (from dur in context.Durations where dur.Id == durationID select dur).Single();
                    var qStage = (from stg in context.Stages where stg.Id == stageID select stg).Single();
                    var qObjective = (from obj in context.Objectives where obj.Id == objectiveID select obj).Single();

                    Project proj = new Project();
                    proj.Name = name.Trim();
                    proj.Customer = qCustomer;
                    proj.Duration = qDuration;
                    proj.Stage = qStage;
                    proj.Objective = qObjective;
                    proj.DueDate = dueDate;
                    proj.CreationDate = DateTime.Now;
                    if (!string.IsNullOrWhiteSpace(description) && !string.IsNullOrEmpty(description))
                        proj.Description = description;

                    if (OssID.Count > 0)
                    {
                        var qOss = from x in context.OperationSystems where OssID.Contains(x.Id) select x;
                        foreach (var item in qOss)
                        {
                            proj.OperationSystems.Add(item);
                        }
                    }
                    if (SkillsID.Count > 0)
                    {
                        var qSkills = from x in context.Skills where SkillsID.Contains(x.Id) select x;
                        foreach (var item in qSkills)
                        {
                            proj.Skills.Add(item);
                        }
                    }
                    if (TypesID.Count > 0)
                    {
                        var qTypes = from x in context.ProjectTypes where TypesID.Contains(x.Id) select x;
                        foreach (var item in qTypes)
                        {
                            proj.Types.Add(item);
                        }
                    }
                    if (EmployeesID.Count > 0)
                    {
                        foreach (var item in EmployeesID)
                        {
                            var qEmp = from employee in context.Employees where employee.Id == item select employee;
                            EmployeeAndProject ep = new EmployeeAndProject { Employee = qEmp.Single(), Project = qProj.Single() };
                            context.EmployeesAndProjects.Add(ep);
                        }
                    }
                    context.Projects.Add(proj);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }

        }
        public static string AddNewProject(string name, string customer, string duration, string stage, string objective, DateTime? dueDate = null, string description = "")
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name) ||
                string.IsNullOrWhiteSpace(customer) || string.IsNullOrEmpty(customer) ||
                string.IsNullOrWhiteSpace(duration) || string.IsNullOrEmpty(duration) ||
                string.IsNullOrWhiteSpace(stage) || string.IsNullOrEmpty(stage) ||
                string.IsNullOrWhiteSpace(objective) || string.IsNullOrEmpty(objective))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qProj = from project in context.Projects where project.Name == name.Trim() select project;
                    if (qProj.Count() > 0)
                        return "Selected operation failed";

                    var qCustomer = (from cust in context.Customers where cust.Name == customer.Trim() select cust).Single();
                    var qDuration = (from dur in context.Durations where dur.Name == duration.Trim() select dur).Single();
                    var qStage = (from stg in context.Stages where stg.Name == stage.Trim() select stg).Single();
                    var qObjective = (from obj in context.Objectives where obj.Name == objective.Trim() select obj).Single();

                    Project proj = new Project();
                    proj.Name = name.Trim();
                    proj.Customer = qCustomer;
                    proj.Duration = qDuration;
                    proj.Stage = qStage;
                    proj.Objective = qObjective;
                    proj.DueDate = dueDate;
                    proj.CreationDate = DateTime.Now;
                    if (!string.IsNullOrWhiteSpace(description) && !string.IsNullOrEmpty(description))
                        proj.Description = description;

                    context.Projects.Add(proj);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static List<KeyValuePair<int, string>> GetAllProjectsName()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from projects in context.Projects select new { projects.Id, projects.Name };
               return q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList(); ;
            }
        }


       
        public static Project GetProject(int ID)
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from projects in context.Projects where projects.Id == ID select projects;
                return q.SingleOrDefault();
            }
        }
        public static bool IsProjectInDb(string name)
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from projects in context.Projects where projects.Name == name select projects.Name;
                if (q.Count() > 0)
                    return true;
                else
                    return false;
            }
        }

        public static List<KeyValuePair<int, string>> GetAllProjectsNameWithId()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from projects in context.Projects select new { projects.Id, projects.Name };
                var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();
                return query;
            }
        }

        public static List<KeyValuePair<int, string>> GetUserProjectsNameWithId(int idUser)
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from projects in context.Projects 
                        from projEmp in context.EmployeesAndProjects
                        where projEmp.Project_Id==projects.Id
                        where projEmp.Employee_Id==idUser                   
                        select new { projects.Id, projects.Name };
                var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();
                return query;
            }
        }

        public static List<KeyValuePair<int, string>> GetUserFavouriteProjectsNameWithId(int idUser)
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from projects in context.Projects
                        from projEmp in context.EmployeesAndProjects
                        where projEmp.Project_Id == projects.Id
                        where projEmp.Employee_Id == idUser
                        where projEmp.IsFavorite==true
                        select new { projects.Id, projects.Name };
                var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();
                return query;
            }
        }

        

        public static string GetProjectDescription(int id)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project.Description).Single();
                    return q == null || q.ToString() == string.Empty ? "No Description" : q.ToString();
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }



        public static string DeleteProject(int id)
        {
            if (id < 0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from project in context.Projects where project.Id == id select project;
                    if (q.Count() == 0)
                        return "Selected operation failed";
                    context.Projects.Remove(q.Single());
                    context.SaveChanges();
                   return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

       

        public static DateTime GetProjectDueDate(int id)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project.DueDate).Single();
                    return q == null ? DateTime.Now : (DateTime)q;
                }
                catch (Exception ex)
                {
                    return DateTime.Now;
                }
            }
        }

        

        public static string EditProjectDueDate(int id, DateTime? newDate)
        {
            if (id < 0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project).Single();
                    q.DueDate = newDate;
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

      

        public static string EditProjectName(int id, string newName)
        {
            if (id < 0 || string.IsNullOrWhiteSpace(newName) || string.IsNullOrEmpty(newName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var projectsWithNewName = from projects in context.Projects where projects.Name == newName.Trim() select projects;
                    if (projectsWithNewName.Count() > 0)
                        return "Selected operation failed";

                    var q = (from project in context.Projects where project.Id == id select project).Single();
                    q.Name = newName.Trim();
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

       

        public static string EditProjectDescription(int id, string newDescription)
        {
            if (id < 0 || newDescription == null)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project).Single();
                    q.Description = newDescription.Trim();
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

    

       

        public static KeyValuePair<int, string> GetProjectCustomer(int id)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select new { project.Customer.Id, project.Customer.Name });
                    var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).Single();
                    return query;
                }
                catch (Exception ex)
                {
                    return new KeyValuePair<int, string> (-1,"Exception: " + ex.Message);
                }
            }
        }

     

        public static string EditProjectCustomer(int id, int CustomerId)
        {
            if (id < 0 || CustomerId<0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project).Single();
                    var qCustomer = (from customer in context.Customers where customer.Id == CustomerId select customer).Single();
                    q.Customer = qCustomer;
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        

        public static KeyValuePair<int, string> GetProjectDuration(int id)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select new { project.Duration.Id, project.Duration.Name });
                    var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).Single();
                    return query;
                }
                catch (Exception ex)
                {
                    return new KeyValuePair<int, string>(-1, "Exception: " + ex.Message);
                }
            }
        }

        

        public static string EditProjectDuration(int id, int DurationId)
        {
            if (id < 0 || DurationId<0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project).Single();
                    var qDuration = (from duration in context.Durations where duration.Id == DurationId select duration).Single();
                    q.Duration = qDuration;
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static string GetProjectStage(string projectName)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Name == projectName.Trim() select project.Stage.Name).Single();
                    return q.ToString();
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }


        public static KeyValuePair<int, string> GetProjectStage(int id)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select new { project.Stage.Id, project.Stage.Name });
                    var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).Single();
                    return query;
                }
                catch (Exception ex)
                {
                    return new KeyValuePair<int, string>(-1, "Exception: " + ex.Message);
                }
            }
        }

      

        public static string EditProjectStage(int id, int StageId)
        {
            if (id < 0 || StageId < 0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project).Single();
                    var qStage = (from stage in context.Stages where stage.Id == StageId select stage).Single();
                    q.Stage = qStage;
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }


        public static KeyValuePair<int, string> GetProjectObjective(int id)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select new { project.Objective.Id, project.Objective.Name});
                    var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).Single();
                    return query;
                }
                catch (Exception ex)
                {
                    return new KeyValuePair<int, string>(-1, "Exception: " + ex.Message);
                }
            }
        }

        public static string EditProjectObjective(int id, int ObjectiveId)
        {
            if (id < 0 || ObjectiveId<0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project).Single();
                    var qObjective = (from objective in context.Objectives where objective.Id == ObjectiveId select objective).Single();
                    q.Objective = qObjective;
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }



        public static string AddSkillToProject(int Prjid, int SkId)
        {
            if (Prjid < 0 || SkId<0)
                return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == Prjid select project).Single();
                    var qSkill = (from skill in context.Skills where skill.Id == SkId select skill).Single();
                    q.Skills.Add(qSkill);
                    context.SaveChanges();
                    return "Added";
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        

        public static List<KeyValuePair<int, string>> GetProjectSkills(int id)
        {
            if (id < 0)
                return null;

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects
                             where project.Id == id
                             select project.Skills.Select(s => new { s.Id, s.Name})).Single();

                   return q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList(); ;
                }
                catch
                {
                    return null;
                }
            }
        }

       

        public static string RemoveProjectSkill(int Prjid, int SkjId)
        {
            if (Prjid < 0 || SkjId<0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qProj = (from project in context.Projects where project.Id == Prjid select project).Single();
                    var qSkill = (from skill in context.Skills where skill.Id == SkjId select skill).Single();
                    qProj.Skills.Remove(qSkill);
                    context.SaveChanges();
                    return "Removed";
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

       

        public static string AddTypeToProject(int Prjid, int Tpid)
        {
            if (Prjid < 0 || Tpid<0)
                return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == Prjid select project).Single();
                    var qType = (from type in context.ProjectTypes where type.Id == Tpid select type).Single();
                    q.Types.Add(qType);
                    context.SaveChanges();
                    return "Added";
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }
        public static string RemoveProjectType(int Prjid, int Tpid)
        {
            if (Prjid < 0 || Tpid < 0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qProj = (from project in context.Projects where project.Id == Prjid select project).Single();
                    var qType = (from type in context.ProjectTypes where type.Id == Tpid select type).Single();
                    qProj.Types.Remove(qType);
                    context.SaveChanges();
                    return "Removed";
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }


        public static List<KeyValuePair<int, string>> GetProjectTypes(int id)
        {
            if (id < 0)
                return null;

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q =  (from project in context.Projects
                             where project.Id == id
                             select project.Types.Select(s => new { s.Id, s.Name })).Single();

                   return q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList(); ;
                }
                catch
                {
                    return null;
                }
            }
        }


        public static string AddOSToProject(int Prjid, int OSid)
        {
            if (Prjid < 0 || OSid<0)
                return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == Prjid select project).Single();
                    var qOS = (from os in context.OperationSystems where os.Id == OSid select os).Single();
                    q.OperationSystems.Add(qOS);
                    context.SaveChanges();
                    return "Added";
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

     

        public static List<KeyValuePair<int, string>> GetProjectOSs(int id)
        {
            if (id < 0)
                return null;

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects
                             where project.Id == id
                             select project.OperationSystems.Select(s => new { s.Id, s.Name })).Single();

                   return q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList(); ;
                }
                catch
                {
                    return null;
                }
            }
        }

        

        public static string RemoveProjectOS(int Prjid, int OSid)
        {
            if (Prjid < 0 || OSid<0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qProj = (from project in context.Projects where project.Id == Prjid select project).Single();
                    var qOS = (from os in context.OperationSystems where os.Id == OSid select os).Single();
                    qProj.OperationSystems.Remove(qOS);
                    context.SaveChanges();
                    return "Removed";
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

       

        public static DateTime GetProjectCreationDate(int id)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project.CreationDate).Single();
                    return q;
                }
                catch (Exception ex)
                {
                    return DateTime.Now;
                }
            }
        }


        #endregion

        #region File @ Project
        public static string AddNewFileToProject(int projectId, string fileName, float fileSizeKBytes, string fileExtension, string filePath)
        {
            if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrEmpty(fileName) ||
                string.IsNullOrWhiteSpace(fileExtension) || string.IsNullOrEmpty(fileExtension) ||
                string.IsNullOrWhiteSpace(filePath) || string.IsNullOrEmpty(filePath) ||
                fileSizeKBytes <= 0)
                return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qProj = (from project in context.Projects where project.Id == projectId select project).Single();
                    if (qProj.Files.Select(fl => fl.Name).Where(fl => fl == fileName).Count() > 0) //file with same name is already existing in project
                        return "Selected operation failed";

                    ProjectFile f = new ProjectFile();
                    f.Name = fileName.Trim();
                    f.SizeKBytes = fileSizeKBytes;
                    f.Extension = fileExtension;
                    f.Path = filePath;
                    f.LoadDate = DateTime.Now;
                    f.Project = qProj;
                    context.ProjectFiles.Add(f);
                    context.SaveChanges();
                    return "Added";
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static List<ProjectFile> GetProjectFiles(int id)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from proj in context.Projects where proj.Id == id select proj.Files).Single();
                   return q.ToList();
                }
                catch
                {
                    return null;
                }
            }
        }

        public static string DeleteFile(int projectId, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrEmpty(fileName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qFile = from files in context.ProjectFiles
                                where files.Name == fileName.Trim()
                                where files.Project.Id == projectId
                                select files;
                    if (qFile.Count() == 0)
                        return "Selected operation failed";
                    context.ProjectFiles.Remove(qFile.Single());
                    context.SaveChanges();
                   return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        #endregion

        #region Employee

        public static string SetFavouriteProject(int projectId, int employeeId, bool favourite)
        {
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
                    q.Single().IsFavorite = favourite;
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static bool IsProjectFavourite(int projectId, int employeeId)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from x in context.EmployeesAndProjects
                            where x.Project.Id == projectId
                            where x.Employee.Id == employeeId
                            select x;

                    if (q.Count() == 0)
                        return false;
                    return q.Single().IsFavorite;
                }
                catch
                {
                    return false;
                }
            }
        }


        #endregion

    }

}
