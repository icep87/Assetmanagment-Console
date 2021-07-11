﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniProject_Console.Data;

namespace MiniProject_Console.Data.Migrations
{
    [DbContext(typeof(InventoryContext))]
    [Migration("20210702104428_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MiniProject_Console.Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Mobile Device"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Laptop Computers"
                        });
                });

            modelBuilder.Entity("MiniProject_Console.Domain.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("ExchangeRate")
                        .HasColumnType("real");

                    b.Property<DateTime>("ExchangeRateLatestUpdate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ExchangeRate = 0f,
                            ExchangeRateLatestUpdate = new DateTime(2021, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "United States dollar",
                            ShortName = "USD"
                        },
                        new
                        {
                            Id = 2,
                            ExchangeRate = 8.49f,
                            ExchangeRateLatestUpdate = new DateTime(2021, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Swedish Krona",
                            ShortName = "SEK"
                        },
                        new
                        {
                            Id = 3,
                            ExchangeRate = 3.79f,
                            ExchangeRateLatestUpdate = new DateTime(2021, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Polish Zloty",
                            ShortName = "PLN"
                        });
                });

            modelBuilder.Entity("MiniProject_Console.Domain.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("Offices");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "Sweden",
                            CurrencyId = 2,
                            Name = "Malmoe"
                        },
                        new
                        {
                            Id = 2,
                            Country = "Poland",
                            CurrencyId = 3,
                            Name = "Warsaw"
                        },
                        new
                        {
                            Id = 3,
                            Country = "USA",
                            CurrencyId = 1,
                            Name = "New York"
                        });
                });

            modelBuilder.Entity("MiniProject_Console.Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OfficeId")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("OfficeId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 2,
                            CurrencyId = 2,
                            Name = "MacBook Pro",
                            OfficeId = 1,
                            Price = 12500f,
                            PurchaseDate = new DateTime(2018, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            CurrencyId = 2,
                            Name = "iPhone 11",
                            OfficeId = 1,
                            Price = 12500f,
                            PurchaseDate = new DateTime(2021, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            CurrencyId = 2,
                            Name = "iPhone 11 Max",
                            OfficeId = 1,
                            Price = 18990f,
                            PurchaseDate = new DateTime(2020, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            CurrencyId = 3,
                            Name = "Macbook Air",
                            OfficeId = 2,
                            Price = 4500f,
                            PurchaseDate = new DateTime(2018, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 1,
                            CurrencyId = 3,
                            Name = "iPhone 12",
                            OfficeId = 2,
                            Price = 5000f,
                            PurchaseDate = new DateTime(2020, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("MiniProject_Console.Domain.Office", b =>
                {
                    b.HasOne("MiniProject_Console.Domain.Currency", "Currency")
                        .WithMany("Offices")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("MiniProject_Console.Domain.Product", b =>
                {
                    b.HasOne("MiniProject_Console.Domain.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniProject_Console.Domain.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniProject_Console.Domain.Office", "Office")
                        .WithMany("Products")
                        .HasForeignKey("OfficeId");

                    b.Navigation("Category");

                    b.Navigation("Currency");

                    b.Navigation("Office");
                });

            modelBuilder.Entity("MiniProject_Console.Domain.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("MiniProject_Console.Domain.Currency", b =>
                {
                    b.Navigation("Offices");
                });

            modelBuilder.Entity("MiniProject_Console.Domain.Office", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
