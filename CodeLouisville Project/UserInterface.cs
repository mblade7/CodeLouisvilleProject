using CodeLouisvilleLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeLouisvilleLibrary.Serialization;

namespace UserInterface
{
    public class CompensationTracker : CodeLouisvilleAppBase
    {
        public CompensationTracker() : base("Compensation Tracker")
        {
        }

        protected override bool Main()
        {
            Menu menu = new Menu();
            menu.AddMenuItem("1", "Check Compensation");
            menu.AddMenuItem("2", "Update Product");
            menu.AddMenuItem("3", "Search for Product");
            menu.AddMenuItem("4", "Quit");

            string userSelection = Prompt4MenuItem("Select an option", menu);
            if (userSelection == "1")
            {
                double SalesPrice = Prompt4Double("What is the sales price: ");
                double GrossDollars = Prompt4Double("What is the gross dollar amount: ");
                double Margin = GrossDollars / SalesPrice;
                string margin = Margin.ToString("#0.##%");
                Console.WriteLine($" The margin is {margin}");
                if (Margin >= .46)
                {
                    double Compensation = GrossDollars * .16;
                    Console.WriteLine($"Your compensation is {Compensation}");
                }
                else if (Margin >= .42)
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
            if (userSelection == "2")
            {
                CreateProduct().Wait();
            }
            if (userSelection == "3")
            {
                SearchforProduct().Wait();
            }
            return Prompt4YesNo("Would you like to do anything else (Y/N)?");
        }

        private const string C_ProductList = "ProductList.json";
        private static EntitySerializationService<Products.Product> ProductRepo = new EntitySerializationService<Products.Product>(C_ProductList);

        static async Task CreateProduct(string[] args)
        {
            await CreateProduct();

            IEnumerable<Products.Product> items = await ProductRepo.GetAllAsync();
            foreach (Products.Product product in items)
                Console.WriteLine(items);
        }
        static async Task CreateProduct()
        {
            if (File.Exists(C_ProductList))
                File.Delete(C_ProductList);
            Products.Product Firstproduct = new Products.Product();
            Firstproduct.ID = 1;
            Firstproduct.Name = "Beddy Bye";
            Firstproduct.RegularPrice = 2999.99;
            Firstproduct.Cost = 1500;

            await ProductRepo.SaveAsync(Firstproduct);

            Products.Product Secondproduct = new Products.Product();
            Secondproduct.ID = 2;
            Secondproduct.Name = "Almost Time";
            Secondproduct.RegularPrice = 149.99;
            Secondproduct.Cost = 90;

            await ProductRepo.SaveAsync(Secondproduct);

            Products.Product Thirdproduct = new Products.Product();
            Thirdproduct.ID = 3;
            Thirdproduct.Name = "Shush Now";
            Thirdproduct.RegularPrice = 7999.99;
            Thirdproduct.Cost = 5000;

            await ProductRepo.SaveAsync(Thirdproduct);


        }
        static async Task SearchforProduct(string[] args)
        {
            await SearchforProduct();

            IEnumerable<Products.Product> items = await ProductRepo.GetAllAsync();
            foreach (Products.Product product in items)
                Console.WriteLine(items);
        }

        static async Task SearchforProduct()
        {
            int userSelection = Prompt4Integer("Which item would you like to look up?");
            await ProductRepo.GetByID(userSelection);
        }
    }
}

