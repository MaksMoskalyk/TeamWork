﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class Position : IElement
    {
        private int id;
        private string name;


        public Position() { }

        public Position(string name)
        {
            this.name = name;
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

        public virtual ICollection<EmployeeAndExperience> EmployeesAndExperiences { get; set; }
        public virtual ICollection<SkillsGroup> SkillsGroups { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
