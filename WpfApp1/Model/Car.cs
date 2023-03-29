using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Threading.Tasks.Dataflow;

namespace WpfApp1
{
    [Table("Cars")]

    public class Car
    {
        private int id;
        private string brand;
        private string model;
        private string color;

        public Car() { }

        public Car(int id, string brand, string model, string color)
        {
            this.id = id;
            this.brand = brand;
            this.model = model;
            this.color = color;
        }
        public Car(string brand, string model, string color)
        {
            this.brand = brand;
            this.model = model;
            this.color = color;
        }

       

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get { return id; } set { this.id = value; } }
        public string Brand { get { return brand; } set { this.brand = value; } }
        public string Model { get { return model; } set { this.model = value; } }
        public string Color { get { return color; } set { color = value; } }

        public override string ToString() => $"{Brand} {Model} - {Color}";

    }
}
