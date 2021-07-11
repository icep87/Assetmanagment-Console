using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MiniProject_Console.Controllers
{
    public class CurrencyController
    {
        public CurrencyController()
        {
        }

        public static Domain.Currency SelectCurrency(bool newOption = false)
        {

            Domain.Currency currency = null;

            while (currency == null)
            {
                GetCurrencyList();

                try
                {
                    //Ask for CurrencyID 
                    Console.WriteLine("");
                    if (newOption)
                    {
                        Program.ConsoleMessage("To add new currency write \"0\"", "info");
                    }
                    Console.Write("Currency ID: ");
                    int currencyID = int.Parse(Console.ReadLine());
                    if (currencyID == 0)
                    {
                        AddCurrency();
                    }
                    else
                    {
                        currency = Program.context.Currencies.Where(x => x.Id.Equals(currencyID)).First();
                    }


                }
                catch (Exception ex)
                {
                    Program.ConsoleMessage("Incorrect Office ID, please try again", "warning");
                    Program.ConsoleMessage(ex.Message, "warning");
                }
            }

            return currency;
        }

        public static void GetCurrencyList()
        {

            Console.WriteLine();
            Console.WriteLine("Select currency:");
            Console.WriteLine();

            foreach (Domain.Currency item in Program.context.Currencies)
            {
                Console.WriteLine("{0}) {1}", item.Id, item.Name);
            }
        }

        public static void GetCurrencyListWithDetails()
        {
            Console.WriteLine("");
            Console.WriteLine("Currencies");
            Program.ConsoleMessage("Exchange rates towards USD", "info");
            Console.WriteLine();

            List<Domain.Currency> currency = Program.context.Currencies.ToList();
            foreach (var item in currency)
            {
                Console.WriteLine("\nName: {0}\nRate: {1}", item.Name, item.ExchangeRate);
            }
            Console.WriteLine();
        }

        public static void AddCurrency()
        {

            Program.ConsoleMessage("Provide details about the new currency", "info");
            Console.WriteLine();
            //
            //Name section
            //
            Console.Write("Full name: ");
            //Program.ConsoleMessage("For example United States Dollar; Euro", "info");
            string name = Console.ReadLine();
            //
            //ShortName section
            //
            Console.Write("Short name: ");
            //Program.ConsoleMessage("For example USD; EUR, SEK", "info");
            string shortName = Console.ReadLine();
            //
            //Exchange Rate section
            //
            float exchangeRate = SelectExchangeRate();

            Domain.Currency currency = new() { Name = name, ShortName = shortName, ExchangeRate = exchangeRate, ExchangeRateLatestUpdate = DateTime.Now };
            Program.context.Add(currency);
            Program.context.SaveChanges();
            Program.ConsoleMessage("Saved the new currency", "header");
            Console.WriteLine();
        }

        public static void UpdateCurrency()
        {
            Domain.Currency currency = SelectCurrency();

            float price = SelectExchangeRate();

            currency.ExchangeRate = price;
            Program.context.Update(currency);
            Program.context.SaveChanges();

            Program.ConsoleMessage($"Updated exchange rate for {currency.ShortName} to {currency.ExchangeRate}", "header");
            Console.WriteLine();
        }


        private static float SelectExchangeRate()
        {

            float price = 0;
            bool priceCheck = true;

            while (priceCheck)
            {
                Console.Write("Please provide exchange price with decimals (9.99) against USD: ");
                string priceString = Console.ReadLine();
                if (!Regex.IsMatch(priceString, @"^\d+(\.\d{1,2})?$"))
                {
                    Program.ConsoleMessage("Incorrect price format, following formats are accepted: 9.99, 9", "warning");
                }
                else
                {
                    //Console ReadLine returns a string and we want to store the price as a float. Convert to float. 
                    price = float.Parse(priceString);

                    //Exit the PriceCheck loop.
                    priceCheck = false;
                }
            }

            return price;
        }
    }
}
