using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MiniProject_Console.Domain;

namespace MiniProject_Console.Data
{

    public class InventoryContext : DbContext
    {

        public DbSet<Office> Offices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var configuration = new ConfigurationBuilder()
              .AddJsonFile($"appsettings.json", false, true).Build();

            options.UseSqlServer(configuration.GetConnectionString("InventoryDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Currency>().HasData(new Currency[]
            {
                new Currency {Id=1, Name="United States dollar", ShortName = "USD", ExchangeRate = 0.00f, ExchangeRateLatestUpdate = Convert.ToDateTime("2021-07-01")},
                new Currency {Id=2, Name="Swedish Krona", ShortName = "SEK", ExchangeRate = 8.49f, ExchangeRateLatestUpdate = Convert.ToDateTime("2021-07-01")},
                new Currency {Id=3, Name="Polish Zloty", ShortName = "PLN", ExchangeRate = 3.79f, ExchangeRateLatestUpdate = Convert.ToDateTime("2021-07-01")}
            });

            modelBuilder.Entity<Office>().HasData(new Office[]
            {
                new Office{Id=1, Name="Malmoe", Country="Sweden", CurrencyId=2 },
                new Office{Id=2, Name = "Warsaw", Country = "Poland", CurrencyId=3},
                new Office{Id=3, Name = "New York", Country = "USA", CurrencyId=1}
            });

            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category{Id=1, Name="Mobile Device"},
                new Category{Id=2, Name="Laptop Computers"}
            });

            modelBuilder.Entity<Product>().HasData(new Product[]
            {
                new Product{Id=1, Name="MacBook Pro", CategoryId=2, CurrencyId=2, OfficeId=1, PurchaseDate= Convert.ToDateTime("2018-07-20"), Price = 12500f},
                new Product{Id=2, Name="iPhone 11", CategoryId=1, CurrencyId=2, OfficeId=1, PurchaseDate= Convert.ToDateTime("2021-04-09"), Price = 12500f},
                new Product{Id=3, Name="iPhone 11 Max", CategoryId=1, CurrencyId=2, OfficeId=1, PurchaseDate= Convert.ToDateTime("2020-11-30"), Price = 18990f},
                new Product{Id=4, Name="Macbook Air", CategoryId=1, CurrencyId=3, OfficeId=2, PurchaseDate= Convert.ToDateTime("2018-10-20"), Price = 4500f},
                new Product{Id=5, Name="iPhone 12", CategoryId=1, CurrencyId=3, OfficeId=2, PurchaseDate= Convert.ToDateTime("2020-12-15"), Price = 5000f}
            });
        }
    }
}
