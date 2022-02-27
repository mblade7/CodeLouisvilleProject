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
            if(userSelection == "1")
            {
                double SalesPrice = Prompt4Double("What is the sales price: ");
                double GrossDollars = Prompt4Double("What is the gross dollar amount: ");
                double Margin = GrossDollars / SalesPrice;
                string margin = Margin.ToString("#0.##%");
                Console.WriteLine($" The margin is {margin}");
                if(Margin >= .46)
                {
                    double Compensation = GrossDollars * .16;
                    Console.WriteLine($"Your compensation is {Compensation}");
                }
                else if(Margin >= .42)
                {
                    double Compensation = GrossDollars * .12;
                    Console.WriteLine($"Your compensation is {Compensation}");
                }
                else
                {
                    double Compensation = GrossDollars * .08;
                    Console.WriteLine($"Your compensation is {Compensation}");
                }
            }
            return Prompt4YesNo("Would you like to do anything else (Y/N)?");
        }
    }
}
