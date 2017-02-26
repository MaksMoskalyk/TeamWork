using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamworkDB
{
    public class Country : IElement
    {
        private int id;
        private string name;


        public Country() { }

        public Country(string name)
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

        public virtual ICollection<CountryAndCity> CountriesAndCities { get; set; }
    }
}
