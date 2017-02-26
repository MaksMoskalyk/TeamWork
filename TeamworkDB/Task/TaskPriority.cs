using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TeamworkDB
{
    public class Priority
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
        public Priority()
        {
            Issues = new List<Issue>();
        }
    }
}
