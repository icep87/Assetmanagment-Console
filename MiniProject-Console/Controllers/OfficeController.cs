using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MiniProject_Console.Controllers;

namespace MiniProject_Console
{
    public class OfficeController
    {
        public OfficeController()
        {
        }

        public static Domain.Office SelectOffice()
        {
            GetOfficeList();

            Domain.Office office = null;

            while (office == null)
            {
                try
                {
                    //Ask for officeID 
                    Console.WriteLine("");
                    Console.Write("Office ID: ");
                    int officeId = int.Parse(Console.ReadLine());
                    office = Program.context.Offices.Where(x => x.Id.Equals(officeId)).Include(c => c.Currency).First();
                }
                catch (Exception ex)
                {
                    Program.ConsoleMessage("Incorrect Office ID, please try again", "warning");
                    Program.ConsoleMessage(ex.Message, "warning");
                }
            }

            return office;
        }

        public static void GetOfficeList()
        {
            Console.WriteLine();
            Console.WriteLine("Select office location:");
            Console.WriteLine();

            foreach (Domain.Office item in Program.context.Offices)
            {
                Console.WriteLine("{0}) {1}", item.Id, item.Name);
            }
        }

        public static void AddOffice()
        {
            Console.WriteLine();
            Program.ConsoleMessage("Provide details about the new office", "info");
            //
            //Name section
            //
            Console.Write("Name: ");
            string name = Console.ReadLine();
            //
            //Country section
            //
            Console.Write("Country: ");
            string country = Console.ReadLine();
            //
            //Currency section
            //
            Domain.Currency currency = CurrencyController.SelectCurrency(newOption: true);

            Domain.Office office = new() { Name = name, Country = country, Currency = currency };
            Program.context.Add(office);
            Program.context.SaveChanges();
            Program.ConsoleMessage("New office has been added", "header");
            Console.WriteLine();


        }

        public static void UpdateOffice()
        {
            Domain.Office office = SelectOffice();

            bool updatingValues = true;

            while (updatingValues)
            {
                Console.WriteLine();
                Program.ConsoleMessage("What would you like to update: ", "header");
                Console.WriteLine();
                Dictionary<int, string> columnNames = new Dictionary<int, string>()
                {
                    {
                        1,
                        "Name"
                    },
                    {
                        2,
                        "Country"
                    }
                };
                Program.ConsoleMessage("You are not allowed to update currency as if office has been moved it's recommended to create new office", "info");

                foreach (KeyValuePair<int, string> item in columnNames)
                {
                    Console.WriteLine("{0}) {1}", item.Key, item.Value);
                }
                Program.ConsoleMessage("Write \"q\" to exit", "info");

                Console.WriteLine();

                Program.ConsoleMessage("Provide ID of the column to update", "info");
                Console.Write("Column to update: ");
                string columnKey = Console.ReadLine();
                Console.WriteLine();


                switch (columnKey)
                {
                    case "1":
                        Program.ConsoleMessage($"Current Name: {office.Name}", "header");
                        Console.Write("New Name:");
                        office.Name = Console.ReadLine();
                        break;

                    case "2":
                        Program.ConsoleMessage($"Current Country: {office.Country}", "header");
                        Console.Write("New Country:");
                        office.Country = Console.ReadLine();
                        break;
                    case "q":
                        updatingValues = false;
                        break;
                    default:
                        Program.ConsoleMessage("Incorrect column id, please try again", "warning");
                        break;
                }

                Program.context.Update(office);
                Program.context.SaveChanges();
            }
        }

        public static void RemoveOffice()
        {
            Domain.Office office = SelectOffice();

            Program.ConsoleMessage("Are you sure you want to delete the office?", "importantQuestion");
            Console.Write("y/n: ");
            string answer = Console.ReadLine().ToLower();
            if (answer == "y")
            {

                //Pre delete check, if office has inventory assigned to it deletetion is not possible.
                if (InventoryController.GetProductAtOffice(office).Any())
                {
                    Program.ConsoleMessage($"{office.Name} office has inventory assigned to it.", "warning");
                    Program.ConsoleMessage("Cannot remove office until inventory has been removed.", "warning");
                    Program.ConsoleMessage("Would you like to transfer the inventory to different office?", "importantQuestion");
                    Console.Write("y/n: ");
                    string moveAnswer = Console.ReadLine().ToLower();

                    if (moveAnswer == "y")
                    {
                        if (TransferOfficeInventory(office))
                        {
                            Program.ConsoleMessage("Inventory was moved to the new office", "info");
                            Program.context.Remove(office);
                            Program.context.SaveChanges();
                            Program.ConsoleMessage("Office was removed");
                            Console.WriteLine();
                        }
                        else
                        {
                            Program.ConsoleMessage("Failed to move the inventory", "warning");
                        };
                    }
                    else if (moveAnswer == "n")
                    {
                        Program.ConsoleMessage("Office was not removed", "info");
                    }
                    else
                    {
                        Program.IncorrectInputWarning();
                    }
                } else
                {
                    Program.context.Remove(office);
                    Program.context.SaveChanges();
                    Program.ConsoleMessage("Office was removed");
                    Console.WriteLine();
                }

            }
            else if (answer == "n")
            {
                Program.ConsoleMessage("Office was not removed", "info");
            }
            else
            {
                Program.IncorrectInputWarning();
            }
        }

        public static void GetOfficeListWithDetails()
        {

            Console.WriteLine("");
            Console.WriteLine("Loading....");

            var office = Program.context.Offices.Include(x => x.Currency).ToList();
            Console.WriteLine("");
            Console.WriteLine("Offices:");
            foreach (var item in office)
            {
                Console.WriteLine("\nName: {0}\nCountry: {1}\nCurrency: {2}", item.Name, item.Country, item.Currency.Name);
            }
            Console.WriteLine();
        }

        private static bool TransferOfficeInventory(Domain.Office office)
        {
            try
            {
                List<Domain.Product> inventoryItem = InventoryController.GetProductAtOffice(office);

                Domain.Office newOffice = SelectOffice();

                foreach (Domain.Product item in inventoryItem)
                {
                    item.Office = newOffice;
                }

                Program.context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Program.ConsoleMessage(ex.Message, "warning");
                return false;
            }


        }
    }
}
