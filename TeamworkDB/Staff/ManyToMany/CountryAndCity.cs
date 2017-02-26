using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class CountryAndCity
    {
        private int id;
        private int country_Id;
        private int city_Id;


        public CountryAndCity() { }

        public CountryAndCity(int country_Id, int city_Id)
        {
            this.country_Id = country_Id;
            this.city_Id = city_Id;
        }


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Country_Id
        {
            get { return country_Id; }
            set { country_Id = value; }
        }

        public int City_Id
        {
            get { return city_Id; }
            set { city_Id = value; }
        }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual Country Country { get; set; }
        public virtual City City { get; set; }
    }
}
