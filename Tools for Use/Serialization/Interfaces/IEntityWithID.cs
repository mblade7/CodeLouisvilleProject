using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLouisvilleLibrary.Serialization.Interfaces
{
    public interface IEntityWithID
    {
        public int ID { get; set; }
    }
}