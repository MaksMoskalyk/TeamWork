using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamworkDB
{
    public class Issue
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [Column(TypeName="text")]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public virtual Employee Creator { get; set; }
        public virtual Status Status { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual TaskType Type { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<TaskFile> Files { get; set; }
        public virtual ICollection<TaskComment> Comments { get; set; }

    }
}
