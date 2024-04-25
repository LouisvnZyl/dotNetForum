using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class UnitTest1 : SqlLiteConnection
    {
        public UnitTest1()
        {
            DataSeeder.SeedData(_dbContext);
            //BogusSeeder bogusSeeder = new BogusSeeder(_dbContext, 12345);
            //bogusSeeder.SeedDatabaseWithBogusValues();
        }

        [Fact]
        public async Task Test1()
        {
            var connectionRestult = await _dbContext.Database.CanConnectAsync();

            Assert.True(connectionRestult);
        }

        [Fact]
        public async Task AddEntity()
        {
            var mechanic = new Mechanic
            {
                Name = "Test",
                Surname = "Test",
                DateOfBirth = DateTime.UtcNow,
            };

            await _dbContext.AddAsync(mechanic);

            await _dbContext.SaveChangesAsync();

            var mechanics = await _dbContext.Mechanics.ToListAsync();
        }

        [Fact]
        public async Task Customer_Car_Engine_Mapping()
        {
            var customer = new Customer
            {
                Name = "Test",
                Surname = "Test",
                Age = "20"
            };

            var car = new Car
            {
                Manufacturer = new Manufacturer
                {
                    Id = 1,
                    Name = "BMW",
                    Location = "Pretoria"
                },
                Customer = customer,
                Engine = new Engine
                {
                    SerialNumber = "1234"
                }
            };

            await _dbContext.AddAsync(car);

            await _dbContext.SaveChangesAsync();

            var customers = await _dbContext.Customers.ToListAsync();

            Assert.Single(customers);
        }

        [Fact]
        public async Task Car_Mechanic_Mapping()
        {
            var car = new Car
            {
                Manufacturer = new Manufacturer
                {
                    Name = "BMW",
                    Location = "Pretoria"
                },
                Engine = new Engine
                {
                    SerialNumber = "1234"
                },
                Mechanics = new List<Mechanic>
                {
                    new Mechanic
                    {
                        Name = "Peter",
                        Surname = "Parker",
                        DateOfBirth = DateTimeOffset.Now
                    }
                }
            };

            await _dbContext.AddAsync(car);

            await _dbContext.SaveChangesAsync();

            var customers = await _dbContext.Cars.ToListAsync();

            var sqlString = _dbContext.Cars
                                      .Where(car => car.Manufacturer.Name == "BMW")
                                      .Select(car => new
                                      {
                                          Name = car.Manufacturer.Name,
                                          Mechanic = car.Mechanics.Where(mechanic => mechanic.Name == "Peter").FirstOrDefault()
                                      })
                                      .ToQueryString();

            Assert.Single(customers);
        }
    }
}