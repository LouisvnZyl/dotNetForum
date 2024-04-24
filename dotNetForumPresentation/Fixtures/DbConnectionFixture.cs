using Xunit;

namespace Fixtures
{
    public class DbConnectionFixture : SqlLiteConnection
    {
        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            var connectionRestult = await _dbContext.Database.CanConnectAsync();

            Assert.True(connectionRestult);
        }

    }
}
