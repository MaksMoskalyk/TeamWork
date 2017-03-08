using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamworkDB;
using TeamworkDBEntity;

namespace Facade
{
    public static class F_Task
    {
        public static List<KeyValuePair<int, string>> GetAllIssuesFilter(int idProject, string name, List<int> Assignee, List<int> Priority,
                                                                List<int> ListStatus, List<int> ListTypeTasks)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    if (Assignee == null || Assignee.Count() == 0)
                        Assignee = F_Staff.GetProjectEmployees(idProject).Select(x=>x.Key).ToList();

                    if (Priority == null || Priority.Count()==0)
                        Priority = GetAllPriorityes().Select(x => x.Key).ToList();

                    if (ListStatus == null || ListStatus.Count()==0)
                        ListStatus = GetAllStatuses().Select(x => x.Key).ToList();

                    if (ListTypeTasks == null || ListTypeTasks.Count()==0)
                        ListTypeTasks = GetAllTaskTypes().Select(x => x.Key).ToList();

                    var qProj = (from x in context.Projects where x.Id == idProject select x).Single();
                    List<Employee> qAssignee = null;
                    
                    if (Assignee.Count() == 0 || Assignee == null)
                    {
                        List<int> empId = F_Staff.GetProjectAllEmployees(idProject).Select(x => x.Key).ToList();
                        qAssignee = (from x in context.Employees where empId.Contains(x.Id) select x).ToList();
                    }
                    else
                        qAssignee = (from x in context.Employees where Assignee.Contains(x.Id) select x).ToList();
                   
                    var allIssues = from x in context.Issues
                                    where x.Project.Id == qProj.Id
                                    where x.Name.Contains(name.Trim())
                                    where Priority.Contains(x.Priority.Id)
                                    where ListStatus.Contains(x.Status.Id)
                                    where ListTypeTasks.Contains(x.Type.Id)
                                    select x;
                    
                    var issuesByAssignee = qAssignee.Select(x => x.Issues);
                    List<Issue> filteredIssues = new List<Issue>();
                    foreach (var item in issuesByAssignee)
                    {
                        foreach (var i in item.ToList())
                        {
                            filteredIssues.Add(i);
                        }
                    }
                    filteredIssues.Distinct();

                    var filtered = filteredIssues.Intersect(allIssues);

                    return filtered.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();
                }
                catch
                {
                    return null;
                }
            }
        }

        #region Type
        public static string AddNewType(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)) return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from x in context.TaskTypes where x.Name == name.Trim() select x;
                    if (q.Count() > 0)
                        return "Selected operation failed";
                    TaskType c = new TaskType();
                    c.Name = name.Trim();
                    context.TaskTypes.Add(c);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static List<KeyValuePair<int, string>> GetAllTaskTypes()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from x in context.TaskTypes select new { x.Id, x.Name };
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
                    var withNewName = from x in context.TaskTypes where x.Name == newName.Trim() select x;
                    if (withNewName.Count() > 0)
                        return "Selected operation failed";

                    var q = (from x in context.TaskTypes where x.Id == id select x).Single();
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
            if (id <0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from x in context.TaskTypes where x.Id == id select x;
                    if (q.Count() == 0)
                        return "Selected operation failed";
                    context.TaskTypes.Remove(q.Single());
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

        #region Priority
        public static string AddNewPriority(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)) return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from x in context.Priorityes where x.Name == name.Trim() select x;
                    if (q.Count() > 0)
                        return "Selected operation failed";
                    Priority c = new Priority();
                    c.Name = name.Trim();
                    context.Priorityes.Add(c);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static List<KeyValuePair<int, string>> GetAllPriorityes()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from x in context.Priorityes select new { x.Id, x.Name };
                var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();
                return query;
            }
        }

        public static string EditPriority(int id, string newName)
        {
            if (id <0 ||
                string.IsNullOrWhiteSpace(newName) || string.IsNullOrEmpty(newName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var withNewName = from x in context.Priorityes where x.Name == newName.Trim() select x;
                    if (withNewName.Count() > 0)
                        return "Selected operation failed";

                    var q = (from x in context.Priorityes where x.Id == id select x).Single();
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


        public static string DeletePriority(int id)
        {
            if (id <0 )
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from x in context.Priorityes where x.Id == id select x;
                    if (q.Count() == 0)
                        return "Selected operation failed";
                    context.Priorityes.Remove(q.Single());
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

        #region Assignee

        public static string EditIssueName(int projectId, int issueId, string newName)
        {
            using (var context = new TeamworkDBContext())
            {
                if (string.IsNullOrEmpty(newName) || string.IsNullOrWhiteSpace(newName))
                    return "Incorrect new name";
                var qIssue = (from x in context.Issues 
                              where x.Id==issueId
                              where x.Project.Id == projectId select x).SingleOrDefault();
                if (qIssue == null)
                    return "Error";
                qIssue.Name = newName;
                context.SaveChanges();
                return null;
            }
        }

        #endregion

        #region Status
        public static string AddNewStatus(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)) return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from x in context.Statuses where x.Name == name.Trim() select x;
                    if (q.Count() > 0)
                        return "Selected operation failed";
                    Status c = new Status();
                    c.Name = name.Trim();
                    context.Statuses.Add(c);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static List<KeyValuePair<int, string>> GetAllStatuses()
        {
            using (var context = new TeamworkDBContext())
            {
                var q = from x in context.Statuses select new { x.Id, x.Name };
                var query = q.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();
                return query;
            }
        }

        public static string EditStatus(int id, string newName)
        {
            if (id <0 ||
                string.IsNullOrWhiteSpace(newName) || string.IsNullOrEmpty(newName))
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var withNewName = from x in context.Statuses where x.Name == newName.Trim() select x;
                    if (withNewName.Count() > 0)
                        return "Selected operation failed";

                    var q = (from x in context.Statuses where x.Id == id select x).Single();
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

        public static string DeleteStatus(int id)
        {
            if (id < 0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = from x in context.Statuses where x.Id == id select x;
                    if (q.Count() == 0)
                        return "Selected operation failed";
                    context.Statuses.Remove(q.Single());
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

        #region File
        public static string AddFileToIssue(string projectName, string issueName, string fileName, float fileSizeKBytes, string fileExtension, string filePath)
        {
            if (string.IsNullOrWhiteSpace(projectName) || string.IsNullOrEmpty(projectName) ||
                string.IsNullOrWhiteSpace(fileName) || string.IsNullOrEmpty(fileName) ||
                string.IsNullOrWhiteSpace(fileExtension) || string.IsNullOrEmpty(fileExtension) ||
                string.IsNullOrWhiteSpace(filePath) || string.IsNullOrEmpty(filePath) ||
                string.IsNullOrWhiteSpace(issueName) || string.IsNullOrEmpty(issueName) ||
                fileSizeKBytes <= 0)
                return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from issue in context.Issues
                                  where issue.Name == issueName.Trim()
                                  where issue.Project.Name == projectName.Trim()
                                  select issue).Single();
                    if (qIssue.Files.Select(fl => fl.Name).Where(fl => fl == fileName).Count() > 0)
                        return "Selected operation failed";

                    TaskFile f = new TaskFile();
                    f.Name = fileName.Trim();
                    f.SizeKBytes = fileSizeKBytes;
                    f.Extension = fileExtension;
                    f.Path = filePath;
                    f.LoadDate = DateTime.Now;
                    f.Issue = qIssue;
                    context.TaskFiles.Add(f);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static string AddFileToIssue(int projectId, int issueId, string fileName, float fileSizeKBytes, string fileExtension, string filePath)
        {
            if (projectId < 0 || issueId < 0 || fileSizeKBytes <= 0 ||
                string.IsNullOrWhiteSpace(fileName) || string.IsNullOrEmpty(fileName) ||
                string.IsNullOrWhiteSpace(fileExtension) || string.IsNullOrEmpty(fileExtension) ||
                string.IsNullOrWhiteSpace(filePath) || string.IsNullOrEmpty(filePath))
                return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from issue in context.Issues
                                  where issue.Id == issueId
                                  where issue.Project.Id == projectId
                                  select issue).Single();
                    if (qIssue.Files.Select(fl => fl.Name).Where(fl => fl == fileName).Count() > 0)
                        return "Selected operation failed";

                    TaskFile f = new TaskFile();
                    f.Name = fileName.Trim();
                    f.SizeKBytes = fileSizeKBytes;
                    f.Extension = fileExtension;
                    f.Path = filePath;
                    f.LoadDate = DateTime.Now;
                    f.Issue = qIssue;
                    context.TaskFiles.Add(f);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static List<TaskFile> GetIssueFiles(int projectId, int issueId)
        {
            if (projectId < 0 || issueId < 0)
                return null;
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var q = (from x in context.Issues
                             where x.Id == issueId
                             where x.Project.Id == projectId
                             select x.Files).Single();
                    return q.ToList();
                }
                catch
                {
                    return null;
                }
            }
        }

        public static string DeleteFile(int projectId, int issueId, string fileName)
        {
            if (projectId < 0 || issueId < 0 )
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qFile = from files in context.TaskFiles
                                where files.Name == fileName
                                where files.Issue.Id == issueId
                                where files.Issue.Project.Id == projectId
                                select files;
                    if (qFile.Count() == 0)
                        return "Selected operation failed";
                    context.TaskFiles.Remove(qFile.Single());
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

        #region Comment
        public static string AddNewComment(string projectName, string issueName, string creatorName, string text)
        {
            if (string.IsNullOrWhiteSpace(issueName) || string.IsNullOrEmpty(issueName) ||
                string.IsNullOrWhiteSpace(projectName) || string.IsNullOrEmpty(projectName) ||
                string.IsNullOrWhiteSpace(text) || string.IsNullOrEmpty(text) ||
                string.IsNullOrWhiteSpace(creatorName) || string.IsNullOrEmpty(creatorName))
                return "Selected operation failed";
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from x in context.Issues
                                  where x.Project.Name == projectName.Trim()
                                  where x.Name == issueName.Trim()
                                  select x).Single();
                    var qCreator = (from x in context.Employees where x.Name == creatorName.Trim() select x).Single();
                    TaskComment c = new TaskComment();
                    c.Text = text.Trim();
                    c.Issue = qIssue;
                    c.Creator = qCreator;
                    c.CreationDate = DateTime.Now;
                    qCreator.IssueComments.Add(c);
                    context.Comments.Add(c);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static List<TaskComment> GetAllCommentsObjects(int projectId, int issueId)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from x in context.Issues
                                  where x.Project.Id == projectId
                                  where x.Id == issueId
                                  select x).Single();
                    List<TaskComment> list = new List<TaskComment>();
                    foreach (var item in qIssue.Comments)
                    {
                        TaskComment c = new TaskComment()
                        {
                            Id = item.Id,
                            Creator = item.Creator,
                            Issue = item.Issue,
                            CreationDate = item.CreationDate,
                            Text = item.Text,
                        };
                        list.Add(c);
                    }
                    return list;
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region Issue

        public static string AddNewIssue(int projectId, string issueName, int statusId,
            int priorityId, int creatorId, int typeId, DateTime dueDate, List<int> employeesId, string desc)
        {
            if (string.IsNullOrWhiteSpace(issueName) || string.IsNullOrEmpty(issueName) ||
                projectId < 0 || statusId < 0 || priorityId < 0 || creatorId < 0 || typeId < 0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qStatus = (from x in context.Statuses where x.Id == statusId select x).Single();
                    var qCreator = (from x in context.Employees where x.Id == creatorId select x).Single();
                    var qPriority = (from x in context.Priorityes where x.Id == priorityId select x).Single();
                    var qType = (from x in context.TaskTypes where x.Id == typeId select x).Single();
                    var qProj = (from x in context.Projects where x.Id == projectId select x).Single();
                    var qEmployees = (from x in context.Employees where employeesId.Contains(x.Id) select x).ToList();
                    int newNameCount = (from x in context.Issues
                                        where x.Project.Id == qProj.Id
                                        where x.Name == issueName.Trim()
                                        select x).Count();
                    if (newNameCount > 0)
                        return "Name is already exists.";
                    Issue i = new Issue();
                    i.Name = issueName.Trim();
                    i.Status = qStatus;
                    i.Priority = qPriority;
                    i.Type = qType;
                    i.CreationDate = DateTime.Now;
                    i.DueDate = dueDate;
                    i.Creator = qCreator;
                    i.Description = desc;
                    if (i.Employees == null)
                        i.Employees = new List<Employee>();   
                    foreach (var item in qEmployees)
                    {
                        i.Employees.Add(item);
                    }



                    qProj.Issues.Add(i);
                    context.Issues.Add(i);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

     

        public static List<KeyValuePair<int, string>> GetMainIssuesNameWithId(int projectId)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qProj = (from x in context.Projects where x.Id == projectId select x).Single();
                    var allIssues = from x in context.Issues where x.Project.Id == qProj.Id select new { x.Id, x.Name };


                    var query = allIssues.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();
                    return query;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static List<KeyValuePair<int, string>> GetUserMainIssuesNameWithId(int projectId, int userId)
        {
            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qProj = (from x in context.Projects where x.Id == projectId select x).Single();
                    var allIssues = from x in context.Issues
                                    from a in context.Employees
                                    where a.Id == userId
                                    where x.Project.Id == qProj.Id
                                    select new { x.Id, x.Name };

                    var query = allIssues.AsEnumerable().Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();
                    return query;
                }
                catch
                {
                    return null;
                }
            }
        }

     

        public static string DeleteIssue(int projectId, int issueId)
        {
            if (projectId < 0 || issueId < 0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {

                    var qProj = (from x in context.Projects where x.Id == projectId select x).Single();
                    var qIssue = (from x in context.Issues
                                  where x.Id == issueId
                                  where x.Project.Id == qProj.Id
                                  select x).Single();

                    context.Issues.Remove(qIssue);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static string GetIssueCreatorName(int projectId, int issueId)
        {
            if (projectId < 0 || issueId < 0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from x in context.Issues
                                  where x.Id == issueId
                                  where x.Project.Id == projectId
                                  select x).Single();

                    return qIssue.Creator.Name.ToString() + " " + qIssue.Creator.Surname.ToString();
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

    

        #region Issue@Type
       

        public static string EditIssueType(int projectId, int issueId, int newTypeId)
        {
            if (projectId < 0 || issueId < 0 || newTypeId<0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from x in context.Issues
                                  where x.Id == issueId
                                  where x.Project.Id == projectId
                                  select x).Single();
                    var qType = (from x in context.TaskTypes where x.Id == newTypeId select x).Single();
                    qIssue.Type = qType;
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

       

        public static KeyValuePair<int, string> GetIssueTypeNameWithId(int projectId, int issueId)
        {
            if (projectId < 0 || issueId < 0)
                return new KeyValuePair<int, string>(-1, "Selected operation failed:Wrond id params");

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue =  (from x in context.Issues
                                  where x.Id == issueId
                                  where x.Project.Id == projectId
                                  select x).Single();

                    return new KeyValuePair<int, string>(qIssue.Type.Id, qIssue.Type.Name);
                }
                catch (Exception ex)
                {
                    return new KeyValuePair<int, string>(-1, "Selected operation failed:Exception" + ex.Message);
                }
            }
        }

        #endregion

        #region Issue@Description
       

        public static string GetIssueDescription(int projectId, int issueId)
        {
            if (projectId < 0 || issueId < 0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from x in context.Issues
                                  where x.Id == issueId
                                  where x.Project.Id == projectId
                                  select x).Single();

                    return qIssue.Description;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static string EditIssueDescription(int projectId, int issueId, string newDescription)
        {
            if (projectId < 0 || issueId < 0 ||
                newDescription == null)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from x in context.Issues
                                  where x.Id == issueId
                                  where x.Project.Id == projectId
                                  select x).Single();
                    qIssue.Description = newDescription.Trim();
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

        #region Issue@Dates
        

        public static DateTime GetIssueDueDate(int projectId, int issueId)
        {
            if (projectId < 0 || issueId < 0)
                return DateTime.Now;

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from x in context.Issues
                                  where x.Id == issueId
                                  where x.Project.Id == projectId
                                  select x).Single();

                    return qIssue.DueDate;
                }
                catch (Exception ex)
                {
                    return DateTime.Now;
                }
            }
        }
     
        public static DateTime GetIssueCreationDate(int projectId, int issueId)
        {
            if (projectId < 0 || issueId < 0)
                return DateTime.Now;

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from x in context.Issues
                                  where x.Id == issueId
                                  where x.Project.Id == projectId
                                  select x).Single();

                    return qIssue.CreationDate;
                }
                catch (Exception ex)
                {
                    return DateTime.Now;
                }
            }
        }
      
        public static string EditIssueDueDate(int projectId, int issueId, DateTime newDate)
        {
            if (projectId < 0 || issueId < 0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from x in context.Issues
                                  where x.Id == issueId
                                  where x.Project.Id == projectId
                                  select x).Single();
                    qIssue.DueDate = newDate;
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

        #region Issue@Status
        public static string GetIssueStatus(int projectId, int issueId)
        {
            if (projectId < 0 || issueId < 0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from x in context.Issues
                                  where x.Id == issueId
                                  where x.Project.Id == projectId
                                  select x).Single();

                    return qIssue.Status.Name.ToString();
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

        public static KeyValuePair<int, string> GetIssueStatusNameWithId(int projectId, int issueId)
        {
            if (projectId < 0 || issueId < 0)
                return new KeyValuePair<int, string>(-1, "Selected operation failed:Wrond id params");

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from x in context.Issues
                                  where x.Id == issueId
                                  where x.Project.Id == projectId
                                  select x).Single();

                    return new KeyValuePair<int, string>(qIssue.Status.Id, qIssue.Status.Name);
                }
                catch (Exception ex)
                {
                    return new KeyValuePair<int, string>(-1, "Selected operation failed:Exception" + ex.Message);
                }
            }
        }
      

        public static string EditIssueStatus(int projectId, int issueId, int statusId)
        {
            if (projectId < 0 || issueId < 0 || statusId<0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from x in context.Issues
                                  where x.Id == issueId
                                  where x.Project.Id == projectId
                                  select x).Single();
                    var qStatus = (from x in context.Statuses where x.Id == statusId select x).Single();
                    qIssue.Status = qStatus;
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

        #region Issue@Priority
       
        public static KeyValuePair<int, string> GetIssuePriorityNameWithId(int projectId, int issueId)
        {
            if (projectId < 0 || issueId < 0)
                return new KeyValuePair<int, string>(-1, "Selected operation failed:Wrond id params");

            using (var context = new TeamworkDBContext())
            {
                try
                {

                    var qIssue2 = (from x in context.Issues
                                  where x.Id == issueId
                                  where x.Project.Id == projectId
                                  select x).Single();

                    return new KeyValuePair<int, string>(qIssue2.Priority.Id, qIssue2.Priority.Name);

                }
                catch (Exception ex)
                {
                    return new KeyValuePair<int, string>(-1, "Selected operation failed:Exception" + ex.Message);
                }
            }
        }
       

        public static string EditIssuePriority(int projectId, int issueId, int newPriorityId)
        {
            if (projectId < 0 || issueId < 0 || newPriorityId<0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from x in context.Issues
                                  where x.Id == issueId
                                  where x.Project.Id == projectId
                                  select x).Single();
                    var qPriority = (from x in context.Priorityes where x.Id == newPriorityId select x).Single();
                    qIssue.Priority = qPriority;
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

        #region Issue@Assignee
    

        public static List<KeyValuePair<int, string>> GetIssueAssigneesNameWithId(int projectId, int issueId)
        {
            if (projectId < 0 || issueId < 0)
                return null;

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from x in context.Issues
                                  where x.Id == issueId
                                  where x.Project.Id == projectId
                                  select x).Single();

                    return qIssue.Employees.Select(s => new KeyValuePair<int, string>(s.Id, s.Name + " " + s.Surname)).ToList();
                }
                catch
                {
                    return null;
                }
            }
        }

     

        public static string AddAssigneeToIssue(int projectId, int issueId, int assigneeId)
        {
            if (projectId < 0 || issueId < 0 || assigneeId < 0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from x in context.Issues
                                  where x.Id == issueId
                                  where x.Project.Id == projectId
                                  select x).Single();
                    var qAssignee = (from x in context.Employees where x.Id == assigneeId select x).Single();
                    qIssue.Employees.Add(qAssignee);
                    context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }

       

        public static string RemoveAssigneeFromIssue(int projectId, int issueId, int assigneeId)
        {
            if (projectId < 0 || issueId < 0 || assigneeId < 0)
                return "Selected operation failed";

            using (var context = new TeamworkDBContext())
            {
                try
                {
                    var qIssue = (from x in context.Issues
                                  where x.Id == issueId
                                  where x.Project.Id == projectId
                                  select x).Single();
                    var qAssignee = (from x in context.Employees where x.Id == assigneeId select x).Single();
                    qIssue.Employees.Remove(qAssignee);
                    context.SaveChanges();
                    return "Removed";
                }
                catch (Exception ex)
                {
                    return "Exception: " + ex.Message;
                }
            }
        }
        #endregion
        #endregion
    }
}
