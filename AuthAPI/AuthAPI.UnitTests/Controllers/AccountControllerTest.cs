namespace AuthAPI.UnitTests.Controllers
{
    using AuthAPI.App.Controllers;
    using AuthAPI.Models.BidingModels;
    using AuthAPI.Models.ViewModels;
    using AuthAPI.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System;
    using Xunit;

    public class AccountControllerTest
    {
        [Fact]
        public void Post_RegisterAndLogin_ShouldReturnStatusCode201()
        {
            // Arrange
            var userId = "12354321-3123-1122-4332-123456789231";

            var bidingModel = new RegisterUserBindingModel()
            {
                Name = "Gosho",
                Email = "gosho@abv.bg",
                Password = "P@ssl0rd",
                ConfirmPassword = "P@ssl0rd"
            };
            var userCredentials = new AccountCredentialsViewModel()
            {
                UserId = new Guid(userId),
                Token = "Token-Token-Token"
            };

            var serviceMock = new Mock<IAccountService>();

            serviceMock
                .Setup(s => s.CreateNewUserAccount(bidingModel))
                .Returns(userCredentials);

            var loggerMock = new Mock<ILogger<AccountController>>();

            var controller = new AccountController(loggerMock.Object, serviceMock.Object);

            // Act
            var response = controller.RegisterAndLogin(bidingModel);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            var result = response as OkObjectResult;
            var model = result.Value as AccountCredentialsViewModel;
            var modelId = model.UserId;
            Assert.Equal(new Guid(userId), modelId);
        }
    }
}
