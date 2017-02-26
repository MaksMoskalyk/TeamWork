using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamworkDB
{
    public class TaskComment
    {
        public int Id { get; set; }

        [Column(TypeName = "text")]
        public string Text { get; set; }
        public virtual Employee Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual Issue Issue { get; set; }
    }
}
