using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProject_Console.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        [Column(TypeName = "date")]
        public DateTime PurchaseDate { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Office Office { get; set; }
        public int? OfficeId { get; set; }
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }
        
    }
}
