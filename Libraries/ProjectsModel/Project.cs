using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsModel
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public virtual Duration Duration { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Lead Lead { get; set; }
        public virtual Stage Stage { get; set; }
        public virtual Objective Objective { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Type> Types { get; set; }
        public virtual ICollection<OperationSystem> OperationSystems { get; set; }

        public Project()
        {
            Files = new List<File>();
            Skills = new List<Skill>();
            Types = new List<Type>();
            OperationSystems = new List<OperationSystem>();
        }
    }
}
