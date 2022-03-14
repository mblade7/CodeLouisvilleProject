using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeLouisvilleLibrary.Serialization.Interfaces;

namespace Products
{
    public class Product : IEntityWithID
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double RegularPrice { get; set; }
        public double Cost { get; set; }
        public double MarginPercentage
        {
            get
            {
                return ((double)RegularPrice - (double)Cost) / (double)RegularPrice;
            }
        }
    }
}
