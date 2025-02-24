using CarRentalAPI.Features.Booking.AggregateRoots;
using CarRentalAPI.Features.Booking.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Data
{
    public class CarDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CarBooking> Bookings { get; set; }

        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<CarBooking>()
                .HasKey(cb => cb.BookingId);

            #region Seed Categories
            List<CarCategory> categories = new List<CarCategory>()
            {
                new CarCategory
                {
                    Id = Guid.Parse("5f6c717c-0f81-43dd-82a1-0fc6543e1607"),
                    Category = "Small"
                },

                new CarCategory
                {
                    Id = Guid.Parse("6c8bfba9-99b4-4000-a54d-a52930a4aa7a"),
                    Category = "Medium"
                },
                new CarCategory
                {
                    Id = Guid.Parse("88a2c05c-5505-4a3b-a04d-fa0b47a0e513"),
                    Category = "Large"
                },
                new CarCategory
                {
                    Id = Guid.Parse("9c614176-ad05-42f5-8b92-60465d1459f1"),
                    Category = "SUV"
                },
                new CarCategory
                {
                    Id = Guid.Parse("07e34f61-e90c-47d3-b90c-a94607270864"),
                    Category = "Minivan"
                }
            };
            #endregion
            modelBuilder.Entity<CarCategory>()
                .HasData(categories);

            #region Seed Cars
            List<Car> cars = new List<Car>
            {
                new Car
                {
                    Id = Guid.Parse("40092e3e-625e-4262-8568-7c6fde1bb8f8"),
                    CategoryId = categories.First(c => c.Category.Equals("Small", StringComparison.OrdinalIgnoreCase)).Id,
                    RegistrationNumber = "ABC 15",
                    CurrentMeterReading = "12345",
                    Status = BookingStatus.Available.ToString()
                },
                new Car
                {
                    Id = Guid.Parse("a4e0cd0a-c5d9-4f8a-98e1-fc7d0c7d4c6d"),
                    CategoryId = categories.First(c => c.Category.Equals("Medium", StringComparison.OrdinalIgnoreCase)).Id,
                    RegistrationNumber = "DEF 23",
                    CurrentMeterReading = "34234",
                    Status = BookingStatus.Available.ToString()
                },
                new Car
                {
                    Id = Guid.Parse("502c070e-5245-4be8-b3af-18c0ce29298b"),
                    CategoryId = categories.First(c => c.Category.Equals("SUV", StringComparison.OrdinalIgnoreCase)).Id,
                    RegistrationNumber = "HGU 45",
                    CurrentMeterReading = "76967",
                    Status = BookingStatus.Available.ToString()
                },
                new Car
                {
                    Id = Guid.Parse("fb526a44-f175-4534-9091-2392e0420094"),
                    CategoryId = categories.First(c => c.Category.Equals("Minivan", StringComparison.OrdinalIgnoreCase)).Id,
                    RegistrationNumber = "OEP 25",
                    CurrentMeterReading = "64545",
                    Status = BookingStatus.Available.ToString()
                },
                new Car
                {
                    Id = Guid.Parse("ee56398f-38b7-4fce-9eb3-df84ba879f76"),
                    CategoryId = categories.First(c => c.Category.Equals("Medium", StringComparison.OrdinalIgnoreCase)).Id,
                    RegistrationNumber = "SUE 09",
                    CurrentMeterReading = "23134",
                    Status = BookingStatus.Booked.ToString()
                },
                new Car
                {
                    Id = Guid.Parse("8a2637ae-41dd-4625-b347-bbb4ca85546c"),
                    CategoryId = categories.First(c => c.Category.Equals("SUV", StringComparison.OrdinalIgnoreCase)).Id,
                    RegistrationNumber = "LOE /(",
                    CurrentMeterReading = "28413",
                    Status = BookingStatus.Booked.ToString()
                }
            };
            #endregion
            modelBuilder.Entity<Car>()
                .HasData(cars);
            
            #region SEED CUSTOMER DATA
            var customers = new List<Customer>
        {
            new Customer
            {
                Id = Guid.Parse("0941b04f-e282-40ca-9435-cd10a1a78b4f"),
                FirstName = "John",
                LastName = "Doe",
                SSN = "123-45-6789",
                Email = "john.doe@example.com",
                PhoneNumber = "+46 70 123 4567"
            },
            new Customer
            {
                Id = Guid.Parse("59466a50-6844-41d4-b0d2-05ec732a8afa"),
                FirstName = "Jane",
                LastName = "Smith",
                SSN = "987-65-4321",
                Email = "jane.smith@example.com",
                PhoneNumber = "+46 70 234 5678"
            },
            new Customer
            {
                Id = Guid.Parse("683f140f-9811-4292-97c7-5cd063df6b1b"),
                FirstName = "Emma",
                LastName = "Johnson",
                SSN = "111-22-3333",
                Email = "emma.johnson@example.com",
                PhoneNumber = "+46 70 345 6789"
            }
        };
            #endregion
            modelBuilder.Entity<Customer>()
                .HasData(customers);

        }
    }
}
