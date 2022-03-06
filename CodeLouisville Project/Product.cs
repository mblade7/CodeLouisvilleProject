using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeLouisvilleLibrary.Serialization.Interfaces;

namespace Product
{
    public class Product : IEntityWithID
    {
       public int ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
       public string Name { get; set;}
       public double RegularPrice { get; set;}
       public double Cost { get; set;}
    }
}
