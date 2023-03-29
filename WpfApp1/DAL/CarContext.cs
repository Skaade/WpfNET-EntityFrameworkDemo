using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WpfApp1.DAL
{
    public class CarContext : DbContext
    {
        public CarContext() : base("Cars"){ 
        }
        public DbSet<Car> Cars { get; set; }
    }
}
