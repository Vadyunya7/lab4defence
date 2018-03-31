using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4defence
{
    class Treaty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get;set;}
        public virtual ICollection<Country> Countries { get; set; }

        public Treaty ()
        {
            Countries = new List<Country>();
        }
    }
}
