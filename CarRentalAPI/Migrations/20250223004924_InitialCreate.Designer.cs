﻿// <auto-generated />
using System;
using CarRentalAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarRentalAPI.Migrations
{
    [DbContext(typeof(CarDbContext))]
    [Migration("20250223004924_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CarRentalAPI.Features.Booking.AggregateRoots.Car", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("CurrentMeterReading")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = new Guid("40092e3e-625e-4262-8568-7c6fde1bb8f8"),
                            CategoryId = new Guid("5f6c717c-0f81-43dd-82a1-0fc6543e1607"),
                            CurrentMeterReading = "12345",
                            RegistrationNumber = "ABC 15",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("a4e0cd0a-c5d9-4f8a-98e1-fc7d0c7d4c6d"),
                            CategoryId = new Guid("6c8bfba9-99b4-4000-a54d-a52930a4aa7a"),
                            CurrentMeterReading = "34234",
                            RegistrationNumber = "DEF 23",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("502c070e-5245-4be8-b3af-18c0ce29298b"),
                            CategoryId = new Guid("9c614176-ad05-42f5-8b92-60465d1459f1"),
                            CurrentMeterReading = "76967",
                            RegistrationNumber = "HGU 45",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("fb526a44-f175-4534-9091-2392e0420094"),
                            CategoryId = new Guid("07e34f61-e90c-47d3-b90c-a94607270864"),
                            CurrentMeterReading = "64545",
                            RegistrationNumber = "OEP 25",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("ee56398f-38b7-4fce-9eb3-df84ba879f76"),
                            CategoryId = new Guid("6c8bfba9-99b4-4000-a54d-a52930a4aa7a"),
                            CurrentMeterReading = "23134",
                            RegistrationNumber = "SUE 09",
                            Status = 1
                        },
                        new
                        {
                            Id = new Guid("8a2637ae-41dd-4625-b347-bbb4ca85546c"),
                            CategoryId = new Guid("9c614176-ad05-42f5-8b92-60465d1459f1"),
                            CurrentMeterReading = "28413",
                            RegistrationNumber = "LOE /(",
                            Status = 1
                        });
                });

            modelBuilder.Entity("CarRentalAPI.Features.Booking.AggregateRoots.CarBooking", b =>
                {
                    b.Property<Guid>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("PickUpDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("BookingId");

                    b.HasIndex("CarId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("CarRentalAPI.Features.Booking.AggregateRoots.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SSN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0941b04f-e282-40ca-9435-cd10a1a78b4f"),
                            Email = "john.doe@example.com",
                            FirstName = "John",
                            LastName = "Doe",
                            PhoneNumber = "+46 70 123 4567",
                            SSN = "123-45-6789"
                        },
                        new
                        {
                            Id = new Guid("59466a50-6844-41d4-b0d2-05ec732a8afa"),
                            Email = "jane.smith@example.com",
                            FirstName = "Jane",
                            LastName = "Smith",
                            PhoneNumber = "+46 70 234 5678",
                            SSN = "987-65-4321"
                        },
                        new
                        {
                            Id = new Guid("683f140f-9811-4292-97c7-5cd063df6b1b"),
                            Email = "emma.johnson@example.com",
                            FirstName = "Emma",
                            LastName = "Johnson",
                            PhoneNumber = "+46 70 345 6789",
                            SSN = "111-22-3333"
                        });
                });

            modelBuilder.Entity("CarRentalAPI.Features.Booking.ValueObjects.CarCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CarCategory");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5f6c717c-0f81-43dd-82a1-0fc6543e1607"),
                            Category = "Small"
                        },
                        new
                        {
                            Id = new Guid("6c8bfba9-99b4-4000-a54d-a52930a4aa7a"),
                            Category = "Medium"
                        },
                        new
                        {
                            Id = new Guid("88a2c05c-5505-4a3b-a04d-fa0b47a0e513"),
                            Category = "Large"
                        },
                        new
                        {
                            Id = new Guid("9c614176-ad05-42f5-8b92-60465d1459f1"),
                            Category = "SUV"
                        },
                        new
                        {
                            Id = new Guid("07e34f61-e90c-47d3-b90c-a94607270864"),
                            Category = "Minivan"
                        });
                });

            modelBuilder.Entity("CarRentalAPI.Features.Booking.AggregateRoots.Car", b =>
                {
                    b.HasOne("CarRentalAPI.Features.Booking.ValueObjects.CarCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CarRentalAPI.Features.Booking.AggregateRoots.CarBooking", b =>
                {
                    b.HasOne("CarRentalAPI.Features.Booking.AggregateRoots.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRentalAPI.Features.Booking.AggregateRoots.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Customer");
                });
#pragma warning restore 612, 618
        }
    }
}
