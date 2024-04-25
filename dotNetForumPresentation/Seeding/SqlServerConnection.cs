using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Seeding
{
    public class SqlServerConnection : IDisposable
    {
        protected readonly string connectionstring = "Server=ERNESSMI-PC\\MSSQLSERVER2;Database=netForum;;user id=sa;password=P@ssword123;TrustServerCertificate=true";

        protected readonly ApplicationDbContext _dbContext;

        public SqlServerConnection()
        {

            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
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
