namespace Tests
{
    public class UnitTest1 : SqlLiteConnection
    {
        [Fact]
        public async Task Test1()
        {
            var connectionRestult = await _dbContext.Database.CanConnectAsync();

            Assert.True(connectionRestult);
        }
    }
}