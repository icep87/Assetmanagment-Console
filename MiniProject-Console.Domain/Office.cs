using System;
using System.Collections.Generic;

namespace MiniProject_Console.Domain
{
    public class Office
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();


    }
}
