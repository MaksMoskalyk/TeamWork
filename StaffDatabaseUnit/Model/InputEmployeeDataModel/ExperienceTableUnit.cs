using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamworkDB;
using TeamworkDBEntity;

namespace StaffDatabaseUnit
{
    public class ExperienceTableUnit
    {
        private Position position;
        private Company company;
        private string dateOfHiring;
        private string dateOfDismissal;

        public ExperienceTableUnit()
        {
            position = new Position();
            company = new Company();
            dateOfHiring = "";
            dateOfDismissal = "";
        }

        public ExperienceTableUnit(Position position, Company company,
            string dateOfHiring, string dateOfDismissal)
        {
            this.position = position;
            this.company = company;
            this.dateOfHiring = dateOfHiring;
            this.dateOfDismissal = dateOfDismissal;
        }

        public Position Position
        {
            get { return position; }
            set { position = value; }
        }

        public Company Company
        {
            get { return company; }
            set { company = value; }
        }

        public string DateOfHiring
        {
            get { return dateOfHiring; }
            set { dateOfHiring = value; }
        }

        public string DateOfDismissal
        {
            get { return dateOfDismissal; }
            set { dateOfDismissal = value; }
        }
    }
}
