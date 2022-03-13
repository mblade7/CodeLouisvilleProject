using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeLouisvilleLibrary;
using UserInterface;
namespace Project
{
    class program
    {
        static void Main(string[] args)
        {
            CompensationTracker compensationTracker = new CompensationTracker();
            compensationTracker.Run();
        }
    }
}
