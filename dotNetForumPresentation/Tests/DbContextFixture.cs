using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class UnitTest1 : SqlLiteConnection
    {
        public UnitTest1()
        {
            DataSeeder.SeedData(_dbContext);
        }

        [Fact]
        public async Task SignleEntity_NoRelationships()
        {
            var mechanic = new Mechanic
            {
                Name = "Demo",
                Surname = "Demoson",
                DateOfBirth = DateTime.UtcNow,
            };

            await _dbContext.AddAsync(mechanic);

            await _dbContext.SaveChangesAsync();

            var mechanics = await _dbContext.Mechanics.ToListAsync();

            var query = _dbContext.Mechanics.ToQueryString();
        }

        [Fact]
        public async Task Update_SignleEntity_NoRelationships()
        {
            var mechanic = new Mechanic
            {
                Name = "Demo",
                Surname = "Demoson",
                DateOfBirth = DateTime.UtcNow,
            };

            await _dbContext.AddAsync(mechanic);

            await _dbContext.SaveChangesAsync();

            var mechanicEntity = await _dbContext.Mechanics
                                                 .Where(mechanic => mechanic.Name == "Demo")
                                                 .FirstOrDefaultAsync();

            var query = _dbContext.Mechanics
                                  .Where(mechanic => mechanic.Name == "Demo")
                                  .ToQueryString();

            mechanicEntity!.Name = mechanic.Name + "Updated";

            await _dbContext.SaveChangesAsync();

            var updatedMechanicEntity = await _dbContext.Mechanics
                                                        .Where(mechanic => mechanic.Name == "DemoUpdated")
                                                        .FirstOrDefaultAsync();
        }

        [Fact]
        public async Task Customer_Car_Engine_Mapping()
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
                }
            };

            await _dbContext.AddAsync(car);

            await _dbContext.SaveChangesAsync();

            var customers = await _dbContext.Customers.ToListAsync();

            var cars = await _dbContext.Cars.ToListAsync();

            var customerEntity = await _dbContext.Customers
                                                 .Where(customer => customer.Name == "James")
                                                 .FirstOrDefaultAsync();

            var carEntity = await _dbContext.Cars
                                            .Where(car => car.Manufacturer.Name == "BMW" &&
                                                          car.Engine.SerialNumber == "1234")
                                            .FirstOrDefaultAsync();

            var carEntityQuery = _dbContext.Cars
                                           .Where(car => car.Manufacturer.Name == "BMW" &&
                                                         car.Engine.SerialNumber == "1234")
                                           .ToQueryString();

            customerEntity!.Cars.Add(carEntity!);
            //car.Customer = customerEntity;
            //car.CustomerId = customerEntity!.Id;

            await _dbContext.SaveChangesAsync();

            var updatedCarEntity = await _dbContext.Cars
                                                   .Where(car => car.Manufacturer.Name == "BMW" &&
                                                                 car.Engine.SerialNumber == "1234")
                                                   .FirstOrDefaultAsync();
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
                }
            };

            var mechanic = new Mechanic
            {
                Name = "Peter",
                Surname = "Parker",
                DateOfBirth = DateTimeOffset.Now
            };

            await _dbContext.AddAsync(car);
            await _dbContext.AddAsync(mechanic);

            await _dbContext.SaveChangesAsync();

            var carMechanic = new CarMechanic
            {
                CarId = car.Id,
                MechanicId = mechanic.Id
            };

            await _dbContext.AddAsync(carMechanic);

            await _dbContext.SaveChangesAsync();

            // Ask why this will work

            car.CarMechanics.Where(carMechanic => carMechanic.CarId == car.Id).First().Completed = true;

            await _dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task EF_Tracking()
        {
            var customerEntity = await _dbContext.Customers
                                                 .Where(customer => customer.Name == "James")
                                                 .FirstOrDefaultAsync();

            customerEntity!.UpdateCredentials(customerEntity.Name + "Updated", customerEntity.Surname + "Updated");

            await _dbContext.SaveChangesAsync();

            var updatedCustomerEntity = await _dbContext.Customers
                                                        .AsNoTracking()
                                                        .Where(customer => customer.Name == "JamesUpdated")
                                                        .FirstOrDefaultAsync();

            updatedCustomerEntity!.UpdateCredentials(customerEntity.Name + "Updated2", customerEntity.Surname + "Updated2");

            await _dbContext.SaveChangesAsync();

            var customers = await _dbContext.Customers
                                            .ToListAsync();
        }

        [Fact]
        public async Task Complex_EF_Queries()
        {
            var carDetails = await _dbContext.Cars
                                             .Select(car => new
                                             {
                                                 car.EngineId,
                                                 car.Manufacturer.Id,
                                                 car.Engine.SerialNumber,
                                                 car.Manufacturer.Location
                                             })
                                             .OrderBy(x => x.SerialNumber)
                                             .ToListAsync();

            var carDetailsQuery = _dbContext.Cars
                                             .Select(car => new
                                             {
                                                 car.EngineId,
                                                 car.Manufacturer.Id,
                                                 car.Engine.SerialNumber,
                                                 car.Manufacturer.Location
                                             })
                                             .OrderBy(x => x.SerialNumber)
                                             .ToQueryString();


            var customerCarDetails = await _dbContext.Customers
                                                     .Select(customer => new
                                                     {
                                                         CustomerName = customer.Name + " " + customer.Surname,
                                                         CustomerCars = customer.Cars.Select(car => new
                                                         {
                                                             car.Id,
                                                             car.Manufacturer,
                                                             car.Engine.FeulEfficiency
                                                         })
                                                     }).ToListAsync();

            var customerCarDetailsQuery = _dbContext.Customers
                                                     .Select(customer => new
                                                     {
                                                         CustomerName = customer.Name + " " + customer.Surname,
                                                         CarDetails = customer.Cars.Select(car => new
                                                         {
                                                             car.Id,
                                                             car.Manufacturer,
                                                             car.Engine.FeulEfficiency
                                                         })
                                                     }).ToQueryString();

            var selectManyCustomerCars =  await _dbContext.Customers
                                                   .SelectMany(customer => customer.Cars,
                                                   (customer, car) => new
                                                   {
                                                       CustomerName = customer.Name + " " + customer.Surname,
                                                       CarDetails = new
                                                       {
                                                           car.Id,
                                                           car.Manufacturer,
                                                           car.Engine.FeulEfficiency
                                                       }
                                                   }).ToListAsync();

            var selectManyCustomerCarsQuery = _dbContext.Customers
                                                   .SelectMany(customer => customer.Cars,
                                                   (customer, car) => new
                                                   {
                                                       CustomerName = customer.Name + " " + customer.Surname,
                                                       CarDetails = new
                                                       {
                                                           car.Id,
                                                           car.Manufacturer,
                                                           car.Engine.FeulEfficiency
                                                       }
                                                   }).ToQueryString();
        }
    }
}