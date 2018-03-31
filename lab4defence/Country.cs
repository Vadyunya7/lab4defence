using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4defence
{
    class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Treaty> Treaties { get; set; }

        public Country()
        {
                Cities = new List<City>();                
                Treaties = new List<Treaty>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
