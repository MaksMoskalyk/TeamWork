using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsModel
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float SizeKBytes { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public DateTime LoadDate { get; set; }
        public virtual Project Project { get; set; }

    }
}
