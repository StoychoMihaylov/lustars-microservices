namespace ProfileAPI.UnitTests
{
    using System;
    using ProfileAPI.Data.Context;
    using Microsoft.EntityFrameworkCore;


    public class TestsInitializer
    {
        protected ProfileDBContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<ProfileDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Microsoft.EntityFrameworkCore.InMemory
                .Options;

            return new ProfileDBContext(dbOptions);
        }
    }
}
