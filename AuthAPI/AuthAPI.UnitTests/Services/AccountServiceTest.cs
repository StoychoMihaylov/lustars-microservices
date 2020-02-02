namespace AuthAPI.UnitTests.Services
{
    using Xunit;
    using System;
    using System.Linq;

    using AuthAPI.Data.Entities;
    using AuthAPI.Models.ViewModels;
    using AuthAPI.Services.Services;
    using AuthAPI.Models.BidingModels;

    public class AccountServiceTest : TestsInitializer
    {
        [Fact]
        public void CreateNewUserAccount_CalledWithCorrectInputData_ShouldReturnTrueAndUserCreated()
        {
            // Arrange
            var createdUser = new User();
            var db = this.GetDatabase();
            var service = new AccountService(db);

            RegisterUserBindingModel bm = new RegisterUserBindingModel()
            {
                Name = "Gosho",
                Email = "gosho@abv.bg",
                Password = "P@ssl0rd",
                ConfirmPassword = "P@ssl0rd"
            };

            // Act
            var tokenResponse = service.CreateNewUserAccount(bm);
            if (tokenResponse != null)
            {
                // Check if user is created.
                createdUser = db.Users.Single();
            }

            // Assert
            Assert.NotNull(createdUser);
            var userId = tokenResponse.UserId;
            Assert.IsType<Guid>(userId);
            Assert.NotEqual(0, userId.ToString().Length);
        }

        [Fact]
        public void LoginUser_CalledWithRelevantInputData_ShouldReturnTokenBearer()
        {
            // Arrange
            var userCredentials = new AccountCredentialsViewModel();
            var db = this.GetDatabase();
            var service = new AccountService(db);

            RegisterUserBindingModel bm = new RegisterUserBindingModel()
            {
                Name = "Gosho",
                Email = "gosho@abv.bg",
                Password = "P@ssl0rd",
                ConfirmPassword = "P@sslord"
            };

            LoginUserBindingModel loginForm = new LoginUserBindingModel()
            {
                Email = "gosho@abv.bg",
                Password = "P@ssl0rd"
            };

            // Act
            var isUserCreated = service.CreateNewUserAccount(bm);
            if (isUserCreated != null)
            {
                userCredentials = service.LoginUser(loginForm);
            }

            // Assert
            Assert.NotNull(userCredentials);
            var userId = userCredentials.UserId;
            Assert.IsType<Guid>(userId);
            Assert.NotEqual(0, userId.ToString().Length);
            var token = userCredentials.Token;
            Assert.NotEqual(0, token.Length);
        }
    }
}
