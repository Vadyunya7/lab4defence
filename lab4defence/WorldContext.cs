using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4defence
{
    class WorldContext: DbContext 
    {

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Treaty> Treaties { get; set; }
        public WorldContext() : base("worldContext4_1")
        { }

    }
}
