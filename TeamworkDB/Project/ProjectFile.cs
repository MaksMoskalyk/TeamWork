using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamworkDB
{
    public class ProjectFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float SizeKBytes { get; set; }

        [MaxLength(10)]
        public string Extension { get; set; }

        [Column(TypeName = "text")]
        public string Path { get; set; }
        public DateTime LoadDate { get; set; }
        public virtual Project Project { get; set; }

    }
}
