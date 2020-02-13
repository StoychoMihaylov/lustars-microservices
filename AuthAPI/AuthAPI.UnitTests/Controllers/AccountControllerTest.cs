namespace AuthAPI.UnitTests.Controllers
{
    using Moq;
    using Xunit;
    using System;
    using Microsoft.AspNetCore.Mvc;

    using AuthAPI.App.Controllers;
    using AuthAPI.Models.BidingModels;
    using AuthAPI.Models.ViewModels;
    using AuthAPI.Services.Interfaces;

    public class AccountControllerTest
    {
        private LoginUserBindingModel InitializeLogInBm()
        {
            return new LoginUserBindingModel()
            {
                Email = "gosho@abv.bg",
                Password = "P@ssw0rd",
            };
        }

        private RegisterUserBindingModel InitializeRegisterBm()
        {
            return new RegisterUserBindingModel()
            {
                Name = "Gosho",
                Email = "gosho@abv.bg",
                Password = "P@ssw0rd",
                ConfirmPassword = "P@ssw0rd"
            };
        }

        private AccountCredentialsViewModel InitializeUserCredentials(string userId)
        {
            return new AccountCredentialsViewModel()
            {
                UserId = new Guid(userId),
                Token = "Token-Token-Token"
            };
        }

        [Fact]
        public void Post_RegisterAndLogin_ShouldReturnStatusCode201()
        {
            // Arrange
            var userId = "12354321-3123-1122-4332-123456789231";

            var bidingModel = InitializeRegisterBm();
            var userCredentials = InitializeUserCredentials(userId);

            var serviceMock = new Mock<IAccountService>();
            serviceMock
                .Setup(s => s.CreateNewUserAccount(bidingModel))
                .Returns(userCredentials);

            var controller = new AccountController(serviceMock.Object);

            // Act
            var response = controller.RegisterAndLogin(bidingModel);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<ObjectResult>(response);
            var result = response as ObjectResult;
            var model = result.Value as AccountCredentialsViewModel;
            var modelId = model.UserId;
            Assert.Equal(new Guid(userId), modelId);
        }

        [Fact]
        public void Post_Login_ShouldReturnStatusCodeUserCredentials_StausCode200()
        {
            // Arrange
            var userId = "12354321-3123-1122-4332-123456789231";

            var bidingModel = InitializeLogInBm();
            var userCredentials = InitializeUserCredentials(userId);

            var serviceMock = new Mock<IAccountService>();
            serviceMock
                .Setup(s => s.LoginUser(bidingModel))
                .Returns(userCredentials);

            var controller = new AccountController(serviceMock.Object);

            // Act
            var response = controller.Login(bidingModel);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<ObjectResult>(response);
            var result = response as ObjectResult;
            var model = result.Value as AccountCredentialsViewModel;
            var modelId = model.UserId;
            Assert.Equal(new Guid(userId), modelId);
        }
    }
}
