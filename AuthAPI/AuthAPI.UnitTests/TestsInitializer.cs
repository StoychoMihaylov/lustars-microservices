namespace AuthAPI.UnitTests
{
    using System;
    using AuthAPI.Data.Context;
    using AuthAPI.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TestsInitializer
    {
        protected IAuthDBContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<AuthDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Microsoft.EntityFrameworkCore.InMemory
                .Options;

            return new AuthDBContext(dbOptions);
        }
    }
}
