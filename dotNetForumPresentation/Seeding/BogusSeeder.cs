using Bogus;
using DataAccess;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

public class BogusSeeder
{
    //Change this value to change the seed, this is to keep the data constant
    private readonly int _seedValue = 83733;
    private readonly bool _randomize = false; // if this is is set to true the data will be random each time

    Randomizer _randomizer = new Randomizer();

    private readonly ApplicationDbContext _dbContext;
    private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

    public BogusSeeder( int seedValue, bool randomize = false)
    {
        _seedValue = seedValue;
        _randomize = randomize;

        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Server=ERNESSMI-PC\\MSSQLSERVER2;Database=netForum;;user id=sa;password=P@ssword123;TrustServerCertificate=true")
                .Options;

        this._dbContext = new ApplicationDbContext(options);
        this._dbContext.Database.EnsureCreated();
    }

    //This method can be used to generate multiple random values on demand, need 10k records np 
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

        //Comment this out to show seeding is constant
        _dbContext.Engines.AddRange(engines);
        Console.WriteLine("Engines are Running");
        _dbContext.Manufacturers.AddRange(manufacturers);
        Console.WriteLine("Manufactures are making stuff!!!");

        _dbContext.Customers.AddRange(customers);
        Console.WriteLine("Customers are consuming");
        _dbContext.Cars.AddRange(cars);
        Console.WriteLine("Ferrari Lamborghini ..... Cars are Generated");
        _dbContext.Mechanics.AddRange(mechanics);
        Console.WriteLine("Mechanics are seeded");

        _dbContext.CarMechanics.AddRange(carMechanics);

        _dbContext.SaveChanges();

        Console.WriteLine("Done soos rys in Japan");
    }

    private List<Car> GenerateCarData(int amount,
                                   List<Engine> engines,
                                   List<Manufacturer> manufacturers,
                                   List<Customer> customers)
    {
        var data = new List<Car>();

        Faker<Customer> fakerRulesForCustomer = new Faker<Customer>("en_ZA")
                .RuleFor(property => property.Name, rule => rule.Person.FirstName)
                .RuleFor(property => property.Surname, rule => rule.Person.LastName)
                .RuleFor(property => property.Email, rule => rule.Person.Email)
                .RuleFor(property => property.Age, rule => Randomizer.Seed.Next(18, 60).ToString())
                .UseSeed(_randomize ? Randomizer.Seed.Next() : _seedValue);

        Faker<Car> fakerRules = new Faker<Car>("en_ZA")
            .RuleFor(property => property.Name, rule => rule.Vehicle.Model())
            .RuleFor(property => property.Type, rule => rule.Vehicle.Type())
            //This chooses a random Foreign Key Value from data that is already generated
            .RuleFor(property => property.Engine, rule => rule.PickRandom(engines))
            .RuleFor(property => property.Manufacturer, rule => rule.PickRandom(manufacturers))
            //You can nest the generation of entities or values in this way as well to avoid passing around values
            .RuleFor(property => property.Customer, rule => fakerRulesForCustomer.Generate())
            .UseSeed(_randomize ? Randomizer.Seed.Next() : _seedValue);

        for (var i = 0; i < amount; i++)
            data.Add(fakerRules.Generate());

        return data;
    }

    private List<Customer> GenerateCustomerData(int amount)
    {
        var data = new List<Customer>();

        Faker<Customer> fakerRules = new Faker<Customer>("en_ZA")
            //.RuleFor(property => property.Name, rule => rule.Person.FirstName)
            //.RuleFor(property => property.Surname, rule => rule.Person.LastName)
            .RuleFor(property => property.Email, rule => rule.Person.Email)
            .RuleFor(property => property.Name, rule => rule.Name.FirstName())
            .RuleFor(property => property.Surname, rule => rule.Name.LastName())
            .RuleFor(property => property.Age, rule => rule.Random.Int(20, 80).ToString())
            .UseSeed(_randomize ? Randomizer.Seed.Next() : _seedValue);

        for (var i = 0; i < amount; i++)
            data.Add(fakerRules.Generate());

        return data;
    }

    private List<Mechanic> GenerateMechanicData(int amount)
    {
        var data = new List<Mechanic>();

        //Can generate data in different locals, this name and last name will be in Korean
        Faker<Mechanic> fakerRules = new Faker<Mechanic>("ko")
            .RuleFor(property => property.Name, rule => rule.Person.FirstName)
            .RuleFor(property => property.DateOfBirth, rule => rule.Person.DateOfBirth)
            //This will choose a random date in the recent past ( 2 - 3 years back max )
            //.RuleFor(property => property.DateOfBirth, rule => rule.Date.Past())
            .RuleFor(property => property.Surname, rule => rule.Person.LastName)
            .UseSeed(_randomize ? Randomizer.Seed.Next() : _seedValue); 

        for (var i = 0; i < amount; i++)
            data.Add(fakerRules.Generate());

        return data;
    }

    private enum FuelEfficiency
    {
        Good,
        Great,
        Awesome
    }

    private List<Engine> GenerateEngineData(int amount)
    {
        var data = new List<Engine>();

        var fuelEfficiency = new List<string>() { "Good", "Great", "Awesome", "Nuclear Powered Monster Machine of Death" };

        ///Have an Enum no problem, Bogus is smart enough to treat it like a list    
        Faker<Engine> fakerRules = new Faker<Engine>("en_ZA")
            .RuleFor(property => property.SerialNumber, rule => rule.Vehicle.Vin())
            .RuleFor(property => property.Type, rule => rule.Vehicle.Type())
            // You can specify your own list of values and let Bogus randomly choose a value from it
            //.RuleFor(property => property.FeulEfficiency, rule => rule.PickRandom(fuelEfficiency))
            ///Have an Enum no problem, Bogus is smart enough to treat it like a list    
            .RuleFor(property => property.FeulEfficiency, rule => rule.PickRandom<FuelEfficiency>().ToString())
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
        var data = new List<CarMechanic>();

        Faker<CarMechanic> fakerRules = new Faker<CarMechanic>("en_ZA")
            .RuleFor(property => property.Car, rule => rule.PickRandom(cars))
            .RuleFor(property => property.Mechanic, rule => rule.PickRandom(mechanics))
            .UseSeed(_randomize ? Randomizer.Seed.Next() : _seedValue);

        for (var i = 0; i < amount; i++)
            data.Add(fakerRules.Generate());

        return data;
    }
}
