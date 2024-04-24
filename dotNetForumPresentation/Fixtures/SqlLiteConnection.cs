using DataAccess;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Fixtures
{
    public abstract class SqlLiteConnection: IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly ApplicationDbContext _dbContext;

        protected SqlLiteConnection()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(_connection)
                    .Options;
            var context = new ApplicationDbContext(options);
            context.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
