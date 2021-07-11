using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace MiniProject_Console
{
    public class InventoryController
    {
        public InventoryController()
        {
        }

        public static void ListInventory()
        {
            Console.WriteLine("");
            Console.WriteLine("Loading....");
            List<Domain.Product> inventory = Program.context.Products.Include(c => c.Currency).Include(o => o.Office).Include(cat => cat.Category).OrderBy(x => x.Office.Name).ThenBy(x => x.PurchaseDate).ToList();

            Console.WriteLine();
            Console.WriteLine("Inventory");
            Program.ConsoleMessage("Sorted by office and then by purchase date", "info");
            foreach (Domain.Product item in inventory)
            {
                //Check warranty end period and color the item if necessary, yellow for 6 months, red for 3 months.
                DateTime warrantyEndDate = item.PurchaseDate.AddYears(3);

                if (warrantyEndDate.AddMonths(-3) < DateTime.Now)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (warrantyEndDate.AddMonths(-6) < DateTime.Now)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                };

                Console.WriteLine("\nID: {6} \nName: {0} \nType: {1} \nPrice: {2} \nCurrency: {3} \nPurchase date: {4} \nOffice name: {5}",
                                  item.Name,
                                  item.Category.Name,
                                  item.Price.ToString(),
                                  item.Currency.Name,
                                  item.PurchaseDate,
                                  item.Office.Name,
                                  item.Id);
                Console.ResetColor();

            }
            Console.WriteLine();
        }

        public static void AddInventoryItem()
        {
            Console.WriteLine("Please provide details about the item you like to add to inventory.");
            Console.WriteLine("");

            //
            //Office section
            //
            //Get a list of offices
            Domain.Office office = OfficeController.SelectOffice();

            //
            //Type section
            //
            //Get a list of device types
            Console.WriteLine("Select type for the product");
            Console.WriteLine("");
            Domain.Category category = CategoryController.SelectCategory();

            //
            //Name section
            //
            Console.Write("Name: ");
            string name = Console.ReadLine();

            //
            //Price section
            //
            // We need to pass the office object to the method as we need a conversion rate to the local office currency
            float price = SelectPrice(office);
            //
            //Purchase Date
            //
            string purchaseDate = SelectPurchaseDate();


            Domain.Product newProduct = new() { Name = name, OfficeId = office.Id, CategoryId = category.Id, CurrencyId = office.CurrencyId, Price = price, PurchaseDate = DateTime.ParseExact(purchaseDate, "d/M/yyyy", CultureInfo.InvariantCulture) };
            Program.context.Add(newProduct);
            Program.context.SaveChanges();

        }

        private static float SelectPrice(Domain.Office office)
        {

            float price = 0;
            bool priceCheck = true;

            while (priceCheck)
            {
                Console.Write("Please provide product price with decimals (999.99) in USD: ");
                string PriceString = Console.ReadLine();
                if (!Regex.IsMatch(PriceString, @"^\d+(\.\d{1,2})?$"))
                {
                    Program.ConsoleMessage("Incorrect price format, following formats are accepted: 9.99, 9", "warning");
                }
                else
                {
                    //Console ReadLine returns a string and we want to store the price as a float. Convert to float. 
                    price = float.Parse(PriceString);

                    //Check what currency has and if conversion is necessary
                    if (office.Currency.ShortName != "USD")
                    {
                        var localCurrency = Program.CurrencyConverter(office.Currency.ExchangeRate, price);
                        price = localCurrency;
                    }
                    //Exit the PriceCheck loop.
                    priceCheck = false;
                }
            }

            return price;
        }

        private static string SelectPurchaseDate()
        {

            string purchaseDate = "";
            bool dateCheck = true;

            while (dateCheck)
            {
                Console.Write("Please provide purchase date in following format DD/MM/YYYY: ");
                string dateString = Console.ReadLine();
                if (!Program.ValidateDate(dateString))
                {
                    Program.ConsoleMessage("Incorrect date", "warning");
                }
                else
                {
                    purchaseDate = dateString;
                    //Exit the DateCheck loop.
                    dateCheck = false;
                }
            }

            return purchaseDate;
        }


        public static void UpdateInventoryItem()
        {
            Domain.Product inventoryItem = GetInventoryItem();

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
                    { 2, "Office" },
                    {
                        3,
                        "Category"
                    },
                    { 4, "Purchase Date" },
                    {
                        5,
                        "Price"
                    }
                };

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
                        Program.ConsoleMessage($"Current Name: {inventoryItem.Name}", "header");
                        Console.Write("New Name:");
                        inventoryItem.Name = Console.ReadLine();
                        break;

                    case "2":
                        Program.ConsoleMessage($"Current Office: {inventoryItem.Office.Name}", "header");
                        Domain.Office office = OfficeController.SelectOffice();
                        inventoryItem.OfficeId = office.Id;
                        //We are not modifying the currency ID of the inventory item as we keep the original purchase price.
                        //The currency will only be changed if we change the price than the price will be converted to the new office currency
                        break;
                    case "3":
                        Program.ConsoleMessage($"Current Category: {inventoryItem.Category.Name}", "header");
                        Domain.Category category = CategoryController.SelectCategory();
                        inventoryItem.CategoryId = category.Id;
                        break;
                    case "4":
                        var currentPurchaseDate = inventoryItem.PurchaseDate.ToString("d/M/yyyy");
                        Program.ConsoleMessage($"Current Purchase Date: {currentPurchaseDate}", "header");
                        var purchaseDate = SelectPurchaseDate();
                        inventoryItem.PurchaseDate = DateTime.ParseExact(purchaseDate, "d/M/yyyy", CultureInfo.InvariantCulture);
                        break;
                    case "5":
                        Program.ConsoleMessage($"Current Price: {inventoryItem.Price} {inventoryItem.Currency.Name}", "header");
                        // We need to get the office object as we didn't eager loaded it with InventoryItem. 
                        inventoryItem.Price = SelectPrice(Program.context.Offices.Where(o => o.Id.Equals(inventoryItem.Office.Id)).Include(c => c.Currency).First());
                        inventoryItem.CurrencyId = inventoryItem.Office.CurrencyId;
                        break;
                    case "q":
                        updatingValues = false;
                        break;
                    default:
                        Program.ConsoleMessage("Incorrect column id, please try again", "warning");
                        break;
                }

                Program.context.Update(inventoryItem);
                Program.context.SaveChanges();
            }
        }

        private static Domain.Product GetInventoryItem()
        {
            InventoryController.ListInventory();

            Domain.Product inventoryItem = null;

            while (inventoryItem == null)
            {
                try
                {
                    //Ask for typeID 
                    Console.WriteLine("");
                    Console.Write("Inventory ID: ");
                    inventoryItem = Program.context.Products.Where(x => x.Id.Equals(int.Parse(Console.ReadLine()))).First();
                }
                catch (Exception ex)
                {
                    Program.ConsoleMessage("Incorrect Inventory ID, please try again", "warning");
                    Program.ConsoleMessage(ex.Message, "warning");
                }
            }

            return inventoryItem;
        }

        public static void RemoveInventoryItem()
        {
            Domain.Product inventoryItem = GetInventoryItem();

            Console.WriteLine("\nID: {6} \nName: {0} \nType: {1} \nPrice: {2} \nCurrency: {3} \nPurchase date: {4} \nOffice name: {5}",
                  inventoryItem.Name,
                  inventoryItem.Category.Name,
                  inventoryItem.Price.ToString(),
                  inventoryItem.Currency.Name,
                  inventoryItem.PurchaseDate,
                  inventoryItem.Office.Name,
                  inventoryItem.Id);

            Program.ConsoleMessage("Are you sure you want to delete the item?", "importantQuestion");
            Console.Write("y/n: ");
            string answer = Console.ReadLine().ToLower();
            if (answer == "y")
            {
                Program.ConsoleMessage("Removing item with ID {0}", inventoryItem.Id.ToString());
                Program.context.Remove(inventoryItem);
                Program.context.SaveChanges();
            }
            else if (answer == "n")
            {
                Program.ConsoleMessage("Item was not removed", "info");
            }
            else
            {
                Program.IncorrectInputWarning();
            }
        }

        public static List<Domain.Product> GetProductAtOffice(Domain.Office office)
        {
            return Program.context.Products.Where(p => p.Office == office).ToList();
        }
    }
}
