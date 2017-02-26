using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace TeamworkDB
{
    public class Employee : Person
    {
        private byte[] photo;
        private int? residence_Id;
        private string generalDescription;
        private string professionalDescription;
        private int? position_Id;


        public Employee() { }

        public Employee(string name, string surname, 
            DateTime dateOfBirth, int citizenship_Id, string gender,
            byte[] photo, int residence_Id,
            string generalDescription, string professionalDescription) :
            base(name, surname, dateOfBirth, citizenship_Id, gender)
        {
            this.photo = photo;
            this.residence_Id = residence_Id;
            this.generalDescription = generalDescription;
            this.professionalDescription = professionalDescription;
        }


        public byte[] Photo
        {
            get { return photo; }
            set { photo = value; }
        }

        public int? Residence_Id
        {
            get { return residence_Id; }
            set { residence_Id = value; }
        }

        public string GeneralDescription
        {
            get { return generalDescription; }
            set { generalDescription = value; }
        }

        public string ProfessionalDescription
        {
            get { return professionalDescription; }
            set { professionalDescription = value; }
        }

        public int? Position_Id
        {
            get { return position_Id; }
            set { position_Id = value; }
        }

        public virtual CountryAndCity CountryAndCity { get; set; }
        public virtual Citizenship Citizenship { get; set; }
        public virtual Position Position { get; set; }         
        public virtual ICollection<EmployeeAndMail> EmployeesAndMails { get; set; }
        public virtual ICollection<EmployeeAndPhone> EmployeesAndPhones { get; set; }
        public virtual ICollection<EmployeeAndWeb> EmployeesAndWebs { get; set; }
        public virtual ICollection<EmployeeAndEducation> EmployeesAndEducations { get; set; }
        public virtual ICollection<EmployeeAndLanguage> EmployeesAndLanguages { get; set; }
        public virtual ICollection<EmployeeAndExperience> EmployeesAndExperiences { get; set; }
        public virtual ICollection<EmployeeAndSkill> EmployeesAndSkills { get; set; }
        public virtual ICollection<EmployeeAndProject> EmployeesAndProjects { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
        public virtual ICollection<TaskComment> IssueComments { get; set; }    
        public virtual Account Account { get; set; }
        public string PhotoURL { get; set; }
    }
}
