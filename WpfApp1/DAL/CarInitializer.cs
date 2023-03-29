using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WpfApp1.DAL
{
    public class CarInitializer : DropCreateDatabaseIfModelChanges<CarContext>{
        protected override void Seed(CarContext context)
            {
            Console.WriteLine("Seeding database...");
            context.Cars.Add(new Car("Audi", "R8", "Black"));
            context.Cars.Add(new Car("Kia", "Picanto", "Gray"));
            context.Cars.Add(new Car("Ford", "69", "Red"));
            context.SaveChanges();
            Console.WriteLine("Done seeding database.");

        }

    }
}
