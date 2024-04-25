using DataAccess;
using Domain.Models;

namespace Tests
{
    public static class DataSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            SeedCars(context);
            SeedMechanics(context);
            SeedCustomers(context);

            context.SaveChanges();
        }

        private static void SeedCars(ApplicationDbContext context)
        {
            var cars = new List<Car>
            {
                new Car
                {
                    Engine = new Engine
                    {
                        FeulEfficiency = "6.4l/100km",
                        SerialNumber = "00001",
                        Size = "1.2L",
                        Type = "V8"
                    },
                    Manufacturer = new Manufacturer
                    {
                        Name = "BMW",
                        Location ="Pretoria"
                    }
                },
                new Car
                {
                    Engine = new Engine
                    {
                        FeulEfficiency = "8.4l/100km",
                        SerialNumber = "00002",
                        Size = "2L",
                        Type = "V8"
                    },
                    Manufacturer = new Manufacturer
                    {
                        Name = "BMW",
                        Location ="Pretoria"
                    },
                    Customer = new Customer
                    {
                        Name = "Louis",
                        Surname = "van Zyl",
                        Age = "25"
                    }
                },
                new Car
                {
                    Engine = new Engine
                    {
                        FeulEfficiency = "8.4l/100km",
                        SerialNumber = "00002",
                        Size = "2L",
                        Type = "V8"
                    },
                    Manufacturer = new Manufacturer
                    {
                        Name = "BMW",
                        Location ="Pretoria"
                    },
                    Customer = new Customer
                    {
                        Name = "Louis",
                        Surname = "van Zyl",
                        Age = "25"
                    },
                    Mechanics = new List<Mechanic>
                    {
                        new Mechanic {
                        Name = "Peter",
                        Surname = "Parker",
                        DateOfBirth = DateTimeOffset.Now
                        }
                    }
                }
            };

            context.Cars.AddRange(cars);
        }

        private static void SeedMechanics(ApplicationDbContext context)
        {
            var mechancis = new List<Mechanic>
            {
                new Mechanic
                {
                        Name = "Ben",
                        Surname = "Parker",
                        DateOfBirth = DateTimeOffset.Now
                },
                new Mechanic
                {
                        Name = "Allan",
                        Surname = "Walker",
                        DateOfBirth = DateTimeOffset.Now
                },
                new Mechanic
                {
                        Name = "James",
                        Surname = "Peterson",
                        DateOfBirth = DateTimeOffset.Now
                }
            };

            context.Mechanics.AddRange(mechancis);
        }

        private static void SeedCustomers(ApplicationDbContext context)
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    Name = "James",
                    Surname = "Palmer",
                    Age = "47"
                },
                new Customer
                {
                    Name = "Laura",
                    Surname = "Kenny",
                    Age = "32"
                },
                new Customer
                {
                    Name = "Kerry",
                    Surname = "Kloster",
                    Age = "20"
                }
            };

            context.Customers.AddRange(customers);
        }
    }
}
