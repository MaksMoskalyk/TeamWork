using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class SkillsGroup : IElement
    {
        private int id;
        private string name;
        private int? position_Id;


        public SkillsGroup() { }

        public SkillsGroup(string name, int position_Id)
        {
            this.name = name;
            this.position_Id = position_Id;
        }


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int? Position_Id
        {
            get { return position_Id; }
            set { position_Id = value; }
        }

        public virtual ICollection<Skill> Skills { get; set; }
        public virtual Position Position { get; set; }
    }
}
