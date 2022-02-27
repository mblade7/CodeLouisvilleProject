using CodeLouisvilleLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compensation_Tracker
{
    public class CompensationTracker : CodeLouisvilleAppBase
    {
        public CompensationTracker() : base("Compensation Tracker")
        {
        }

        protected override bool Main()
        {
            Menu menu = new Menu();
            menu.AddMenuItem("1","Check Compensation");
            menu.AddMenuItem("2", "Quit");

            string userSelection = Prompt4MenuItem("Select an option", menu);
            return Prompt4YesNo("Would you like to do anything else (Y/N)?");
        }
    }
}
