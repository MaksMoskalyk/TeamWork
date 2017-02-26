using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TeamworkDB
{
    public class ProjectCustomer
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

        public ProjectCustomer()
        {
            Projects = new List<Project>();
        }
    }
}
