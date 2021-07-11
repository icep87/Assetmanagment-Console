using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProject_Console.Domain
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public float ExchangeRate { get; set; }
        [Column(TypeName = "date")]
        public DateTime ExchangeRateLatestUpdate { get; set; }
        public List<Office> Offices { get; set; } = new List<Office>();

    }
}
