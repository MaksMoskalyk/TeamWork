using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamworkDB;
using TeamworkDBEntity;

namespace StaffDatabaseUnit
{
    public class EducationTableUnit
    {
        private EducationFacility educationFacility;
        private EducationSpecialty specialty;
        private EducationEntitlingDocument entitlingDocument;
        private string dateOfGraduation;

        public EducationTableUnit()
        {
            educationFacility = new EducationFacility();
            specialty = new EducationSpecialty();
            entitlingDocument = new EducationEntitlingDocument();
            dateOfGraduation = "";
        }

        public EducationTableUnit(EducationFacility educationFacility, EducationSpecialty specialty,
            EducationEntitlingDocument entitlingDocument, string dateOfGraduation)
        {
            this.educationFacility = educationFacility;
            this.specialty = specialty;
            this.entitlingDocument = entitlingDocument;
            this.dateOfGraduation = dateOfGraduation;
        }

        public EducationFacility EducationFacility
        {
            get { return educationFacility; }
            set { educationFacility = value; }
        }

        public EducationSpecialty Specialty
        {
            get { return specialty; }
            set { specialty = value; }
        }

        public EducationEntitlingDocument EntitlingDocument
        {
            get { return entitlingDocument; }
            set { entitlingDocument = value; }
        }

        public string DateOfGraduation
        {
            get { return dateOfGraduation; }
            set { dateOfGraduation = value; }
        }
    }
}
