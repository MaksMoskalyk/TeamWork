using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class Person
    {
        private int id;
        private string name;
        private string surname;
        private DateTime dateOfBirth;
        private int? citizenship_Id;
        private string gender;
        

        public Person() { }

        public Person(string name, string surname, DateTime dateOfBirth,
            int citizenship_Id, string gender)
        {
            this.name = name;
            this.surname = surname;
            this.dateOfBirth = dateOfBirth;
            this.citizenship_Id = citizenship_Id;
            this.gender = gender;
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

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }
		
		public string DateOfBirthText
        {
            get { return dateOfBirth.ToString("MM:dd:yyyy"); }
        }

        public int? Citizenship_Id
        {
            get { return citizenship_Id; }
            set { citizenship_Id = value; }
        }

        public string Gender
        {
            get { return gender; }
            set 
            { 
                if(value == "M" || value == "F")
                    gender = value;
            }
        }
    }
}
