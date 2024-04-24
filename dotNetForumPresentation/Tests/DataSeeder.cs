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
            SeedCarMechanics(context);
        }

        private static void SeedCars(ApplicationDbContext context)
        {
            var cars = new List<Car>();

            context.Cars.AddRange(cars);
        }

        private static void SeedMechanics(ApplicationDbContext context)
        {
        }

        private static void SeedCustomers(ApplicationDbContext context)
        {
        }

        private static void SeedCarMechanics(ApplicationDbContext context)
        {
        }
    }
}
