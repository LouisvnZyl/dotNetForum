using DataAccess;
using Domain.Models;
using Domain.Models.Cars;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Tests
{
    public class SqlServerDbContextFixture : SqlServerConnection
    {
        private readonly ITestOutputHelper output;

        public SqlServerDbContextFixture(ITestOutputHelper output)
        {
            this.output = output;
        }

        private void LogToOutput(string test, string message)
        {
            output.WriteLine($"{test}:\n {message}");
        }

        [Fact]
        public void DoesMyContextBuild_IEIsMyMappingsValid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer(base.connectionstring)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
                    .Options;

            var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureCreated();
            Assert.True(dbContext.Database.CanConnect());
        }

        [Fact]
        public void ReadCustomer()
        {
            var query = _dbContext.Set<Customer>().Include(x => x.Cars).Where(c => c.Id > 0).ToQueryString();
            LogToOutput(nameof(ReadCustomer), query);
        }

        [Fact]
        public void ReadCarWithMechanic()
        {
            var query = _dbContext.Set<Car>().Include(x => x.Mechanics).Where(c => c.EngineId > 0).ToQueryString();
            LogToOutput(nameof(ReadCarWithMechanic),query);

        }

        /*
         * By convention, EF will not automatically scan for base or derived types; 
         * this means that if you want a CLR type in your hierarchy to be mapped, you must explicitly specify that type on your model. 
         * For example, specifying only the base type of a hierarchy will not cause EF Core to implicitly include all of its sub-types.
         */
        [Fact]
        public void ReadDerivedCarAndNoticeTheDiscriminator()
        {
            var query = _dbContext.Set<SchoolBus>().Where(c => c.EngineId > 0).ToQueryString();
            LogToOutput(nameof(ReadDerivedCarAndNoticeTheDiscriminator), query);
        }

        /*
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
                Brand = "BWM",
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
                Brand = "BWM",
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

            var sqlString = _dbContext.Cars.Where(car => car.Brand == "BMW").ToQueryString();

            Assert.Single(customers);
        }
        */
    }
}