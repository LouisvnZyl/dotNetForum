using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class UnitTest1 : SqlLiteConnection
    {
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
        public async Task MappingTest()
        {
            var customer = new Customer
            {
                Name = "Test",
                Surname = "Test",
                Age = "20"
            };

            var car = new Car
            {
                Brand = "BWM",
                Customer = customer,
            };

            await _dbContext.AddAsync(car);

            await _dbContext.SaveChangesAsync();

            var customers = await _dbContext.Customers.ToListAsync();

            Assert.Single(customers);
        }
    }
}