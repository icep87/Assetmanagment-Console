using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MiniProject_Console.Data;
using System.Text.RegularExpressions;
using MiniProject_Console.Controllers;

namespace MiniProject_Console
{
    class Program
    {
        public static InventoryContext context = new();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the inventory managment application");
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("1) Inventory");
                Console.WriteLine("2) Offices");
                Console.WriteLine("3) Currencies");
                Console.WriteLine("4) Reports");
                Console.WriteLine("5) Categories");
                ConsoleMessage("Write \"q\" to exit", "info");
                Console.WriteLine();
                Console.Write("Please choose action: ");

                string actionMethod = Console.ReadLine();

                if (actionMethod == "1")
                {
                    while (true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("1) Add to Inventory");
                        Console.WriteLine("2) List Inventory");
                        Console.WriteLine("3) Update Inventory item");
                        Console.WriteLine("4) Delete Inventory item");
                        ConsoleMessage("Write \"q\" to exit", "info");
                        Console.WriteLine();
                        Console.Write("Please choose action: ");

                        string inventoryAction = Console.ReadLine();

                        if (inventoryAction == "1")
                        {
                            InventoryController.AddInventoryItem();
                            Console.WriteLine();
                        }
                        else if (inventoryAction == "2")
                        {
                            InventoryController.ListInventory();
                            Console.WriteLine();
                        }
                        else if (inventoryAction == "3")
                        {
                            InventoryController.UpdateInventoryItem();
                            Console.WriteLine();
                        }
                        else if (inventoryAction == "4")
                        {
                            InventoryController.RemoveInventoryItem();
                            Console.WriteLine();
                        }
                        else if (inventoryAction.ToLower() == "q")
                        {
                            InventoryController.ListInventory();
                            Console.WriteLine();
                            break;
                        }
                        else
                        {
                            IncorrectInputWarning();
                        }
                    }
                }
                else if (actionMethod == "2")
                {

                    Console.WriteLine();
                    Console.WriteLine("1) Add Office");
                    Console.WriteLine("2) List Office");
                    Console.WriteLine("3) Update Office");
                    Console.WriteLine("4) Delete Office");
                    ConsoleMessage("Write \"q\" to exit", "info");
                    Console.WriteLine();
                    Console.Write("Please choose action: ");

                    string officeAction = Console.ReadLine();

                    if (officeAction == "1")
                    {
                        OfficeController.AddOffice();
                    }
                    else if (officeAction == "2")
                    {
                        OfficeController.GetOfficeListWithDetails();
                    }
                    else if (officeAction == "3")
                    {
                        OfficeController.UpdateOffice();
                    }
                    else if (officeAction == "4")
                    {
                        OfficeController.RemoveOffice();
                    }
                    else if (officeAction.ToLower() == "q")
                    {
                        break;
                    }
                    else
                    {
                        IncorrectInputWarning();
                    }
                }
                else if (actionMethod == "3")
                {
                    Console.WriteLine();
                    Console.WriteLine("1) Add Currency");
                    Console.WriteLine("2) List Currencies");
                    Console.WriteLine("3) Update Exchange rate ");
                    ConsoleMessage("Write \"q\" to exit", "info");
                    Console.WriteLine();
                    Console.Write("Please choose action: ");

                    string currencyAction = Console.ReadLine();
                    if (currencyAction == "1")
                    {
                        CurrencyController.AddCurrency();
                    }
                    else if (currencyAction == "2")
                    {
                        CurrencyController.GetCurrencyListWithDetails();
                    }
                    else if (currencyAction == "3")
                    {
                        CurrencyController.UpdateCurrency();
                    }
                    else if (currencyAction.ToLower() == "q")
                    {
                        break;
                    }
                    else
                    {
                        IncorrectInputWarning();
                    }
                }
                else if (actionMethod == "4")
                {
                    Console.WriteLine();
                    Console.WriteLine("1) Inventory near end of warraty");
                    Console.WriteLine("2) Inventory per office");
                    ConsoleMessage("Write \"q\" to exit", "info");
                    Console.WriteLine();
                    Console.Write("Please choose action: ");
                    string reportAction = Console.ReadLine();
                    if (reportAction == "1")
                    {
                        ReportController.GetInventoryNearEndOfWarranty();
                    }
                    else if (reportAction == "2")
                    {
                        ReportController.GetInventoryPerOfficeReport();
                    }

                }
                else if (actionMethod == "5")
                {
                    Console.WriteLine();
                    Console.WriteLine("1) Add Category");
                    Console.WriteLine("2) List Categories");
                    ConsoleMessage("Write \"q\" to exit", "info");
                    Console.WriteLine();
                    Console.Write("Please choose action: ");

                    string currencyAction = Console.ReadLine();
                    if (currencyAction == "1")
                    {
                        CategoryController.AddCategory();
                    }
                    else if (currencyAction == "2")
                    {   
                        CategoryController.GetCategoryList();
                    }
                    else if (currencyAction.ToLower() == "q")
                    {
                        break;
                    }
                    else
                    {
                        IncorrectInputWarning();
                    }
                }
                else if (actionMethod.ToLower() == "q")
                {
                    Console.WriteLine();
                    break;
                }
                else
                {
                    IncorrectInputWarning();
                }
            }

        }
        //Helpers


        public static void ConsoleMessage(string message, string messageType = null)
        {
            switch (messageType)
            {
                case "info":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case "warning":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "header":
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case "importantQuestion":
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    break;
            }

            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static float CurrencyConverter(float CurrencyExchangeRate, float Price)
        {
            float localCurrency = Price * CurrencyExchangeRate;

            return localCurrency;
        }

        public static void IncorrectInputWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Incorrect input, please choose correction action");
            Console.ResetColor();
        }

        public static bool ValidateDate(string date)
        {
            string pattern = @"^(0[1-9]|[12][0-9]|3[01])\/(1[0-2]|0?[1-9])\/(?:[0-9]{2})?[0-9]{2}$";
            if (Regex.IsMatch(date, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}

