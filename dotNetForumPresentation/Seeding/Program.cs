namespace Seeding;

internal class Program
{
    static void Main(string[] args)
    {
        BogusSeeder bogusSeeder = new BogusSeeder(1234);

        bogusSeeder.SeedDatabaseWithBogusValues();

        Console.ReadKey(); 
    }
}
