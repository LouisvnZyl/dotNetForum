using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public abstract class SqlServerConnection : IDisposable
    {
        protected readonly string connectionstring = "Server=localhost;Database=netForum;Integrated Security=SSPI;TrustServerCertificate=true";

        protected readonly ApplicationDbContext _dbContext;

        protected SqlServerConnection()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer(connectionstring)
                    .Options;

            this._dbContext = new ApplicationDbContext(options);
            this._dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
