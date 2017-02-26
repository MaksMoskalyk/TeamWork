using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TeamworkDB
{
    public class OperationSystem
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

        public OperationSystem()
        {
            Projects = new List<Project>();
        }
    }
}
