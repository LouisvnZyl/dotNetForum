using Bogus;
using DataAccess;
using Domain.Models;

public class BogusSeeder
{
    private readonly int _seedValue = 83733;
    private readonly bool _randomize = false;

    private readonly ApplicationDbContext _dbContext;
    public BogusSeeder(ApplicationDbContext dbContext, int seedValue, bool randomize = false)
    {
        _seedValue = seedValue;
        _randomize = randomize;

        _dbContext = dbContext;
    }

    public void SeedDatabaseWithBogusValues(int amountCars = 500,
                                            int amountCustomers = 300,
                                            int amountMechanics = 10,
                                            int amountEngines = 1000,
                                            int amountManufacturers = 200,
                                            int amountLinkedCarMechanics = 300)
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();

        List<Engine> engines = GenerateEngineData(amountEngines);
        List<Manufacturer> manufacturers = GenerateManufacturerData(amountManufacturers);

        List<Customer> customers = GenerateCustomerData(amountCustomers);
        List<Car> cars = GenerateCarData(amountCars, engines, manufacturers, customers);
        List<Mechanic> mechanics = GenerateMechanicData(amountMechanics);

        List<CarMechanic> carMechanics = LinkCarMechanic(amountLinkedCarMechanics, cars, mechanics);

        _dbContext.Engines.AddRange(engines);
        _dbContext.Manufacturers.AddRange(manufacturers);

        _dbContext.Customers.AddRange(customers);
        _dbContext.Cars.AddRange(cars);
        _dbContext.Mechanics.AddRange(mechanics);

        _dbContext.CarMechanics.AddRange(carMechanics);

        _dbContext.SaveChanges();
    }

    private List<Car> GenerateCarData(int amount,
                                   List<Engine> engines,
                                   List<Manufacturer> manufacturers,
                                   List<Customer> customers)
    {
        var data = new List<Car>();

        Faker<Car> fakerRules = new Faker<Car>("en_ZA")
            .RuleFor(property => property.Name, rule => rule.Vehicle.Model())
            .RuleFor(property => property.Type, rule => rule.Vehicle.Type())
            .RuleFor(property => property.Engine, rule => rule.PickRandom(engines))
            .RuleFor(property => property.Manufacturer, rule => rule.PickRandom(manufacturers))
            .RuleFor(property => property.Customer, rule => rule.PickRandom(customers))
            .UseSeed(_randomize ? Randomizer.Seed.Next() : _seedValue);

        for (var i = 0; i < amount; i++)
            data.Add(fakerRules.Generate());

        return data;
    }

    private List<Customer> GenerateCustomerData(int amount)
    {
        var data = new List<Customer>();

        Faker<Customer> fakerRules = new Faker<Customer>("en_ZA")
            .RuleFor(property => property.Name, rule => rule.Person.FirstName)
            .RuleFor(property => property.Surname, rule => rule.Person.LastName)
            .RuleFor(property => property.Email, rule => rule.Person.Email)
            //.RuleFor(property => property.Name, rule => rule.Name.FirstName())
            //.RuleFor(property => property.Surname, rule => rule.Name.LastName())
            .RuleFor(property => property.Age, rule => Randomizer.Seed.Next(18, 60).ToString())
            .UseSeed(_randomize ? Randomizer.Seed.Next() : _seedValue);

        for (var i = 0; i < amount; i++)
            data.Add(fakerRules.Generate());

        return data;
    }

    private List<Mechanic> GenerateMechanicData(int amount)
    {
        var data = new List<Mechanic>();

        Faker<Mechanic> fakerRules = new Faker<Mechanic>("en_ZA")
            .RuleFor(property => property.Name, rule => rule.Person.FirstName)
            .RuleFor(property => property.DateOfBirth, rule => rule.Person.DateOfBirth)
            //.RuleFor(property => property.DateOfBirth, rule => rule.Date.Past())
            .RuleFor(property => property.Surname, rule => rule.Person.LastName)
            //.UseDateTimeReference(DateTime.UtcNow.AddDays(-1))
            .UseSeed(_randomize ? Randomizer.Seed.Next() : _seedValue);

        for (var i = 0; i < amount; i++)
            data.Add(fakerRules.Generate());

        return data;
    }

    private List<Engine> GenerateEngineData(int amount)
    {
        var data = new List<Engine>();

        var fuelEfficiency = new List<string>() { "Good", "Great", "Awesome", "Nuclear Powered Monster Machine of Death" };

        Faker<Engine> fakerRules = new Faker<Engine>("en_ZA")
            .RuleFor(property => property.SerialNumber, rule => rule.Vehicle.Vin())
            .RuleFor(property => property.Type, rule => rule.Vehicle.Type())
            .RuleFor(property => property.FeulEfficiency, rule => rule.PickRandom(fuelEfficiency))
            .UseSeed(_randomize ? Randomizer.Seed.Next() : _seedValue);

        for (var i = 0; i < amount; i++)
            data.Add(fakerRules.Generate());

        return data;
    }

    private List<Manufacturer> GenerateManufacturerData(int amount)
    {
        var data = new List<Manufacturer>();

        Faker<Manufacturer> fakerRules = new Faker<Manufacturer>("en_ZA")
            .RuleFor(property => property.Name, rule => rule.Person.FirstName)
            .RuleFor(property => property.Location, rule => rule.Vehicle.Type())
            .UseSeed(_randomize ? Randomizer.Seed.Next() : _seedValue);

        for (var i = 0; i < amount; i++)
            data.Add(fakerRules.Generate());

        return data;
    }

    private List<CarMechanic> LinkCarMechanic(int amount, List<Car> cars, List<Mechanic> mechanics)
    {
        var carMechanic = new List<CarMechanic>();

        Faker<CarMechanic> fakerRules = new Faker<CarMechanic>("en_ZA")
            .RuleFor(property => property.Car, rule => rule.PickRandom(cars))
            .RuleFor(property => property.Mechanic, rule => rule.PickRandom(mechanics))
            .UseSeed(_randomize ? Randomizer.Seed.Next() : _seedValue);

        return carMechanic;
    }
}
