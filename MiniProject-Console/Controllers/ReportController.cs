using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;

namespace MiniProject_Console.Controllers
{
    public class ReportController
    {
        public ReportController()
        {
        }

        public static void GetInventoryPerOfficeReport()
        {
            var items = Program.context.Products.Include(o => o.Office).Include(c => c.Category).AsEnumerable().GroupBy(p => p.Office.Name).ToList();

            foreach (IGrouping<string, Domain.Product> group in items)
            {
                Console.WriteLine();
                Program.ConsoleMessage(group.Key, "header");
                Console.WriteLine();
                Console.WriteLine("Total items: {0}", group.Count().ToString());
                Console.WriteLine();
                Console.WriteLine("Categories:");
                Console.WriteLine();
                foreach (var item in group.GroupBy(p => p.Category))
                {
                    Console.WriteLine("{0}: {1}", item.Key.Name, item.Count());
                    Console.WriteLine(" - total cost: {0}", item.Sum(p => p.Price));
                    Console.WriteLine();
                }
                Console.WriteLine("Total cost of inventory: {0}", group.Sum(p => p.Price));
                Console.WriteLine("Total average cost of inventory: {0}", group.Sum(p => p.Price) / group.Count());
                Program.ConsoleMessage("All prices in local currency", "info");
                Console.WriteLine();

            }
        }

        public static void GetInventoryNearEndOfWarranty()
        {
            var items = Program.context.Products.Include(o => o.Office).AsEnumerable().GroupBy(p => p.Office.Name).ToList();

            foreach (IGrouping<string, Domain.Product> group in items)
            {
                Console.WriteLine();
                Program.ConsoleMessage(group.Key, "header");
                Console.WriteLine();
                Console.WriteLine("Total items: {0}", group.Count().ToString());
                IEnumerable<Domain.Product> itemsWith6MonthsLeft = group.Where(p => p.PurchaseDate.AddMonths(30) < DateTime.Now);
                Console.WriteLine();
                Console.WriteLine("Less than 6 months warranty left: {0}", itemsWith6MonthsLeft.Count());
                Console.WriteLine();
                foreach (Domain.Product item in itemsWith6MonthsLeft)
                {
                    Console.WriteLine("{0} - Warranty end: {1}", item.Name, item.PurchaseDate.AddMonths(36).ToString("d/M/yyyy", CultureInfo.InvariantCulture));
                }
                Console.WriteLine();
                IEnumerable<Domain.Product> itemsWith3MonthsLeft = group.Where(p => p.PurchaseDate.AddMonths(33) < DateTime.Now);
                Console.WriteLine("Less than 3 months warranty left: {0}", itemsWith3MonthsLeft.Count());
                Console.WriteLine();
                foreach (Domain.Product item in itemsWith3MonthsLeft)
                {
                    Console.WriteLine("{0} - Warranty end: {1}", item.Name, item.PurchaseDate.AddMonths(36).ToString("d/M/yyyy", CultureInfo.InvariantCulture));
                }
            }
        }
    }
}
