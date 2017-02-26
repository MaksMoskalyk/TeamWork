using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamworkDB
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreationDate { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }
        public virtual ProjectDuration Duration { get; set; }
        public virtual ProjectCustomer Customer { get; set; }
        public virtual ProjectStage Stage { get; set; }
        public virtual ProjectObjective Objective { get; set; }
        public virtual ICollection<ProjectFile> Files { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<ProjectType> Types { get; set; }
        public virtual ICollection<OperationSystem> OperationSystems { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
        public virtual ICollection<EmployeeAndProject> EmployeesAndProjects { get; set; }

        public Project()
        {
            Files = new List<ProjectFile>();
            Skills = new List<Skill>();
            Types = new List<ProjectType>();
            OperationSystems = new List<OperationSystem>();
            Issues = new List<Issue>();
        }
    }
}
