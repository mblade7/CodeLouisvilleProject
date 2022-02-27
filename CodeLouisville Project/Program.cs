using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeLouisvilleClassLibrary;
using CodeLouisvilleLibrary;
using Compensation_Tracker;
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
