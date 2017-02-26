﻿using System;
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

        public static List<string> GetAllCustomers()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from customers in context.Customers select customers.Name;
                return q.ToList();
            }
        }

       

        public static string EditCustomer(string oldName, string newName)
        {
            if (string.IsNullOrWhiteSpace(oldName) || string.IsNullOrEmpty(oldName) ||
                string.IsNullOrWhiteSpace(newName) || string.IsNullOrEmpty(newName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var customersWithNewName = from customers in context.Customers where customers.Name == newName.Trim() select customers;
                    if (customersWithNewName.Count() > 0)
                        return "Selected operation failed";

                    var q = (from customer in context.Customers where customer.Name == oldName.Trim() select customer).Single();
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

        

        public static string DeleteCustomer(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from customer in context.Customers where customer.Name == name.Trim() select customer;
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

        public static List<string> GetAllDurations()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from durations in context.Durations select durations.Name;
                return q.ToList();
            }
        }

       

        public static string EditDuration(string oldName, string newName)
        {
            if (string.IsNullOrWhiteSpace(oldName) || string.IsNullOrEmpty(oldName) ||
                string.IsNullOrWhiteSpace(newName) || string.IsNullOrEmpty(newName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var durationsWithNewName = from durations in context.Durations where durations.Name == newName.Trim() select durations;
                    if (durationsWithNewName.Count() > 0)
                        return "Selected operation failed";

                    var q = (from duration in context.Durations where duration.Name == oldName.Trim() select duration).Single();
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


        public static string DeleteDuration(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from duration in context.Durations where duration.Name == name.Trim() select duration;
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

        public static List<string> GetAllObjectives()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from objectives in context.Objectives select objectives.Name;
                return q.ToList();
            }
        }

      

        public static string EditObjective(string oldName, string newName)
        {
            if (string.IsNullOrWhiteSpace(oldName) || string.IsNullOrEmpty(oldName) ||
                string.IsNullOrWhiteSpace(newName) || string.IsNullOrEmpty(newName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var objectivesWithNewName = from objectives in context.Objectives where objectives.Name == newName.Trim() select objectives;
                    if (objectivesWithNewName.Count() > 0)
                        return "Selected operation failed";

                    var q = (from objective in context.Objectives where objective.Name == oldName.Trim() select objective).Single();
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

      

        public static string DeleteObjective(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from objective in context.Objectives where objective.Name == name.Trim() select objective;
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

        public static List<string> GetAllOSs()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from OSs in context.OperationSystems select OSs.Name;
                return q.ToList();
            }
        }

       

        public static string EditOS(string oldName, string newName)
        {
            if (string.IsNullOrWhiteSpace(oldName) || string.IsNullOrEmpty(oldName) ||
                string.IsNullOrWhiteSpace(newName) || string.IsNullOrEmpty(newName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var OSsWithNewName = from OSs in context.OperationSystems where OSs.Name == newName.Trim() select OSs;
                    if (OSsWithNewName.Count() > 0)
                        return "Selected operation failed";

                    var q = (from os in context.OperationSystems where os.Name == oldName.Trim() select os).Single();
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

       

        public static string DeleteOS(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from os in context.OperationSystems where os.Name == name.Trim() select os;
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
        public static string AddNewSkill(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)) return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from skill in context.Skills where skill.Name == name.Trim() select skill;
                    if (q.Count() > 0)
                        return "Selected operation failed";
                    var group = context.SkillsGroups.Find(1);
                    Skill s = new Skill();
                    s.SkillsGroup = group;
                    s.Name = name.Trim();
                    context.Skills.Add(s);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static List<string> GetAllSkills()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from skills in context.Skills select skills.Name;
                return q.ToList();
            }
        }

      

        public static string EditSkill(string oldName, string newName)
        {
            if (string.IsNullOrWhiteSpace(oldName) || string.IsNullOrEmpty(oldName) ||
                string.IsNullOrWhiteSpace(newName) || string.IsNullOrEmpty(newName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var skillsWithNewName = from skills in context.Skills where skills.Name == newName.Trim() select skills;
                    if (skillsWithNewName.Count() > 0)
                        return "Selected operation failed";

                    var q = (from skill in context.Skills where skill.Name == oldName.Trim() select skill).Single();
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

      

        public static string DeleteSkill(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from skill in context.Skills where skill.Name == name.Trim() select skill;
                    if (q.Count() == 0)
                        return "Selected operation failed";
                    context.Skills.Remove(q.Single());
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

        public static List<string> GetAllStages()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from stage in context.Stages select stage.Name;
                return q.ToList();
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

        public static string EditStage(string oldName, string newName)
        {
            if (string.IsNullOrWhiteSpace(oldName) || string.IsNullOrEmpty(oldName) ||
                string.IsNullOrWhiteSpace(newName) || string.IsNullOrEmpty(newName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var stagesWithNewName = from stages in context.Skills where stages.Name == newName.Trim() select stages;
                    if (stagesWithNewName.Count() > 0)
                        return "Selected operation failed";

                    var q = (from stages in context.Stages where stages.Name == oldName.Trim() select stages).Single();
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

        public static string EditStage(int id, string newName)
        {
            if (id < 0 ||
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

        public static string DeleteStage(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from stage in context.Stages where stage.Name == name.Trim() select stage;
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

        public static List<string> GetAllTypes()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from types in context.ProjectTypes select types.Name;
                return q.ToList();
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

        public static string EditType(string oldName, string newName)
        {
            if (string.IsNullOrWhiteSpace(oldName) || string.IsNullOrEmpty(oldName) ||
                string.IsNullOrWhiteSpace(newName) || string.IsNullOrEmpty(newName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var typesWithNewName = from types in context.ProjectTypes where types.Name == newName.Trim() select types;
                    if (typesWithNewName.Count() > 0)
                        return "Selected operation failed";

                    var q = (from type in context.ProjectTypes where type.Name == oldName.Trim() select type).Single();
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

        public static string EditType(int id, string newName)
        {
            if (id < 0 ||
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

        public static string DeleteType(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from type in context.ProjectTypes where type.Name == name.Trim() select type;
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

        public static List<string> GetAllProjectsName()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from projects in context.Projects select projects.Name;
                return q.ToList();
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

    

       

        public static string GetProjectCustomer(int id)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project.Customer.Name).Single();
                    return q.ToString();
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

     

        public static string EditProjectCustomer(int id, string newCustomer)
        {
            if (id < 0 ||
                string.IsNullOrWhiteSpace(newCustomer) ||
                string.IsNullOrEmpty(newCustomer))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project).Single();
                    var qCustomer = (from customer in context.Customers where customer.Name == newCustomer.Trim() select customer).Single();
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

        

        public static string GetProjectDuration(int id)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project.Duration.Name).Single();
                    return q.ToString();
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        

        public static string EditProjectDuration(int id, string newDuration)
        {
            if (id < 0 ||
                string.IsNullOrWhiteSpace(newDuration) ||
                string.IsNullOrEmpty(newDuration))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project).Single();
                    var qDuration = (from duration in context.Durations where duration.Name == newDuration.Trim() select duration).Single();
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


        public static string GetProjectStage(int id)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project.Stage.Name).Single();
                    return q.ToString();
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

      

        public static string EditProjectStage(int id, string newStage)
        {
            if (id < 0 ||
                string.IsNullOrWhiteSpace(newStage) ||
                string.IsNullOrEmpty(newStage))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project).Single();
                    var qStage = (from stage in context.Stages where stage.Name == newStage.Trim() select stage).Single();
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


        public static string GetProjectObjective(int id)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project.Objective.Name).Single();
                    return q.ToString();
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static string EditProjectObjective(int id, string newObjective)
        {
            if (id < 0 ||
                string.IsNullOrWhiteSpace(newObjective) ||
                string.IsNullOrEmpty(newObjective))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project).Single();
                    var qObjective = (from objective in context.Objectives where objective.Name == newObjective.Trim() select objective).Single();
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



        public static string AddSkillToProject(int id, string skillName)
        {
            if (id < 0 ||
                string.IsNullOrWhiteSpace(skillName) ||
                string.IsNullOrEmpty(skillName))
                return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project).Single();
                    var qSkill = (from skill in context.Skills where skill.Name == skillName.Trim() select skill).Single();
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

        

        public static List<string> GetProjectSkills(int id)
        {
            if (id < 0)
                return null;

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects
                             where project.Id == id
                             select project.Skills.Select(s => s.Name)).Single();

                    return q.ToList();
                }
                catch
                {
                    return null;
                }
            }
        }

       

        public static string RemoveProjectSkill(int id, string skillName)
        {
            if (id < 0 ||
                string.IsNullOrWhiteSpace(skillName) || string.IsNullOrEmpty(skillName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qProj = (from project in context.Projects where project.Id == id select project).Single();
                    var qSkill = (from skill in context.Skills where skill.Name == skillName.Trim() select skill).Single();
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

       

        public static string AddTypeToProject(int id, string typeName)
        {
            if (id < 0 ||
                string.IsNullOrWhiteSpace(typeName) ||
                string.IsNullOrEmpty(typeName))
                return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project).Single();
                    var qType = (from type in context.ProjectTypes where type.Name == typeName.Trim() select type).Single();
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

       

        public static List<string> GetProjectTypes(int id)
        {
            if (id < 0)
                return null;

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q =  (from project in context.Projects
                             where project.Id == id
                             select project.Types.Select(s => s.Name)).Single();

                    return q.ToList();
                }
                catch
                {
                    return null;
                }
            }
        }


        public static string AddOSToProject(int id, string osName)
        {
            if (id < 0 ||
                string.IsNullOrWhiteSpace(osName) ||
                string.IsNullOrEmpty(osName))
                return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects where project.Id == id select project).Single();
                    var qOS = (from os in context.OperationSystems where os.Name == osName.Trim() select os).Single();
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

     

        public static List<string> GetProjectOSs(int id)
        {
            if (id < 0)
                return null;

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from project in context.Projects
                             where project.Id == id
                             select project.OperationSystems.Select(s => s.Name)).Single();

                    return q.ToList();
                }
                catch
                {
                    return null;
                }
            }
        }

        

        public static string RemoveProjectOS(int id, string osName)
        {
            if (id < 0 ||
                string.IsNullOrWhiteSpace(osName) || string.IsNullOrEmpty(osName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qProj = (from project in context.Projects where project.Id == id select project).Single();
                    var qOS = (from os in context.OperationSystems where os.Name == osName.Trim() select os).Single();
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