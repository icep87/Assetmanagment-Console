using System;
using System.Linq;

namespace MiniProject_Console
{
    public class CategoryController
    {
        public CategoryController()
        {
        }

        public static Domain.Category SelectCategory()
        {
            Console.WriteLine("Select type for the product");
            GetCategoryList();

            Domain.Category category = null;

            while (category == null)
            {
                try
                {
                    //Ask for typeID 
                    Console.WriteLine("");
                    Console.Write("Type ID: ");
                    int typeIndex = int.Parse(Console.ReadLine());
                    category = Program.context.Categories.Where(x => x.Id.Equals(typeIndex)).First();

                }
                catch (Exception ex)
                {
                    Program.ConsoleMessage("Incorrect Category ID, please try again", "warning");
                    Program.ConsoleMessage(ex.Message, "warning");

                }
            }

            return category;
        }

        public static void GetCategoryList()
        {
            Console.WriteLine("");
            Console.WriteLine("Categories");
            Console.WriteLine("");
            foreach (Domain.Category item in Program.context.Categories)
            {
                Console.WriteLine("{0}) {1}", item.Id, item.Name);
            }
            Console.WriteLine();
        }

        public static void AddCategory()
        {
            Program.ConsoleMessage("Provide details about the new category", "info");
            Console.WriteLine();
            //
            //Name section
            //
            Console.Write("Name: ");
            //Program.ConsoleMessage("For example United States Dollar; Euro", "info");
            string name = Console.ReadLine();
            
            Domain.Category category = new() { Name = name};
            Program.context.Add(category);
            Program.context.SaveChanges();
            Program.ConsoleMessage("Saved the new category", "header");
            Console.WriteLine();
        }
    }
}
