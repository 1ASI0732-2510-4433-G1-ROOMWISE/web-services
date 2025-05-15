using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Commands;
using SweetManagerWebService.IAM.Domain.Model.Commands.Authentication.Credential;
using SweetManagerWebService.IAM.Domain.Model.Commands.Authentication.User;
using SweetManagerWebService.IAM.Domain.Services.Credential.Admin;
using SweetManagerWebService.IAM.Domain.Services.Credential.Owner;
using SweetManagerWebService.IAM.Domain.Services.Credential.Worker;
using SweetManagerWebService.IAM.Domain.Services.Users.Admin;
using SweetManagerWebService.IAM.Domain.Services.Users.Owner;
using SweetManagerWebService.IAM.Domain.Services.Users.Worker;
using SweetManagerWebService.IAM.Interfaces.REST;
using SweetManagerWebService.IAM.Interfaces.REST.Resource.Authentication.User;

namespace SweetManagerWebService.Tests.CoreIntegrationTests;

[TestFixture]
public class AuthenticationControllerTests
{
    
    [Test]
    public async Task SignIn_WithInvalidRoleId_ReturnsBadRequest()
    {
        // Arrange
        var mockOwnerCommandService = new Mock<IOwnerCommandService>();
        var mockAdminCommandService = new Mock<IAdminCommandService>();
        var mockWorkerCommandService = new Mock<IWorkerCommandService>();
        var mockAdminCredentialCommandService = new Mock<IAdminCredentialCommandService>();
        var mockWorkerCredentialCommandService = new Mock<IWorkerCredentialCommandService>();
        var mockOwnerCredentialCommandService = new Mock<IOwnerCredentialCommandService>();

        var invalidSignInResource = new SignInResource(
            "user@example.com",
            "password",
            5 // Invalid RoleId
        );

        var authenticationController = new AuthenticationController(
            mockAdminCommandService.Object,
            mockAdminCredentialCommandService.Object,
            mockWorkerCommandService.Object,
            mockWorkerCredentialCommandService.Object,
            mockOwnerCommandService.Object,
            mockOwnerCredentialCommandService.Object);

        // Act
        var actionResult = await authenticationController.SignIn(invalidSignInResource);

        // Assert
        Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task SignUpAdmin_WithValidData_ReturnsOkResult()
    {
        // Arrange
        var mockOwnerCommandService = new Mock<IOwnerCommandService>();
        var mockAdminCommandService = new Mock<IAdminCommandService>();
        var mockWorkerCommandService = new Mock<IWorkerCommandService>();
        var mockAdminCredentialCommandService = new Mock<IAdminCredentialCommandService>();
        var mockWorkerCredentialCommandService = new Mock<IWorkerCredentialCommandService>();
        var mockOwnerCredentialCommandService = new Mock<IOwnerCredentialCommandService>();

        var validSignUpResource = new SignUpUserResource(
            1, // Id
            "adminuser", // Username
            "Admin", // Name
            "User", // Surname
            "admin@example.com", // Email
            1234567890, // Phone
            "Active", // State
            "StrongPassw0rd!" // Password
        );

        mockAdminCommandService
            .Setup(service => service.Handle(It.IsAny<SignUpUserCommand>()))
            .ReturnsAsync(true);

        mockAdminCredentialCommandService
            .Setup(service => service.Handle(It.IsAny<CreateUserCredentialCommand>()))
            .ReturnsAsync(true);

        var authenticationController = new AuthenticationController(
            mockAdminCommandService.Object,
            mockAdminCredentialCommandService.Object,
            mockWorkerCommandService.Object,
            mockWorkerCredentialCommandService.Object,
            mockOwnerCommandService.Object,
            mockOwnerCredentialCommandService.Object);

        // Act
        var actionResult = await authenticationController.SignUpAdmin(validSignUpResource);

        // Assert
        var okResult = actionResult as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo("User created correctly!"));
    }

    [Test]
    public async Task SignUpWorker_WithValidData_ReturnsOkResult()
    {
        // Arrange
        var mockOwnerCommandService = new Mock<IOwnerCommandService>();
        var mockAdminCommandService = new Mock<IAdminCommandService>();
        var mockWorkerCommandService = new Mock<IWorkerCommandService>();
        var mockAdminCredentialCommandService = new Mock<IAdminCredentialCommandService>();
        var mockWorkerCredentialCommandService = new Mock<IWorkerCredentialCommandService>();
        var mockOwnerCredentialCommandService = new Mock<IOwnerCredentialCommandService>();

        var validSignUpResource = new SignUpUserResource(
            1, // Id
            "workeruser", // Username
            "Worker", // Name
            "User", // Surname
            "worker@example.com", // Email
            1234567890, // Phone
            "Active", // State
            "StrongPassw0rd!" // Password
        );

        mockWorkerCommandService
            .Setup(service => service.Handle(It.IsAny<SignUpUserCommand>()))
            .ReturnsAsync(true);

        mockWorkerCredentialCommandService
            .Setup(service => service.Handle(It.IsAny<CreateUserCredentialCommand>()))
            .ReturnsAsync(true);

        var authenticationController = new AuthenticationController(
            mockAdminCommandService.Object,
            mockAdminCredentialCommandService.Object,
            mockWorkerCommandService.Object,
            mockWorkerCredentialCommandService.Object,
            mockOwnerCommandService.Object,
            mockOwnerCredentialCommandService.Object);

        // Act
        var actionResult = await authenticationController.SignUpWorker(validSignUpResource);

        // Assert
        var okResult = actionResult as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo("User created correctly!"));
    }

    [Test]
    public async Task SignUpAdmin_WithInvalidData_ReturnsBadRequest()
    {
        // Arrange
        var mockOwnerCommandService = new Mock<IOwnerCommandService>();
        var mockAdminCommandService = new Mock<IAdminCommandService>();
        var mockWorkerCommandService = new Mock<IWorkerCommandService>();
        var mockAdminCredentialCommandService = new Mock<IAdminCredentialCommandService>();
        var mockWorkerCredentialCommandService = new Mock<IWorkerCredentialCommandService>();
        var mockOwnerCredentialCommandService = new Mock<IOwnerCredentialCommandService>();

        var invalidSignUpResource = new SignUpUserResource(
            1, // Id
            "adminuser", // Username
            "Admin", // Name
            "User", // Surname
            "invalid-email", // Invalid Email
            1234567890, // Phone
            "Active", // State
            "weak" // Weak password
        );

        mockAdminCommandService
            .Setup(service => service.Handle(It.IsAny<SignUpUserCommand>()))
            .ThrowsAsync(new Exception("Invalid email address"));

        var authenticationController = new AuthenticationController(
            mockAdminCommandService.Object,
            mockAdminCredentialCommandService.Object,
            mockWorkerCommandService.Object,
            mockWorkerCredentialCommandService.Object,
            mockOwnerCommandService.Object,
            mockOwnerCredentialCommandService.Object);

        // Act
        var actionResult = await authenticationController.SignUpAdmin(invalidSignUpResource);

        // Assert
        var badRequestResult = actionResult as BadRequestObjectResult;
        Assert.That(badRequestResult, Is.Not.Null);
        Assert.That(badRequestResult.Value, Is.EqualTo("Invalid email address"));
    }

    [Test]
    public async Task SignUpOwner_WithValidData_ReturnsOkResult()
    {
        // Arrange
        var mockOwnerCommandService = new Mock<IOwnerCommandService>();
        var mockAdminCommandService = new Mock<IAdminCommandService>();
        var mockWorkerCommandService = new Mock<IWorkerCommandService>();
        var mockAdminCredentialCommandService = new Mock<IAdminCredentialCommandService>();
        var mockWorkerCredentialCommandService = new Mock<IWorkerCredentialCommandService>();
        var mockOwnerCredentialCommandService = new Mock<IOwnerCredentialCommandService>();

        var validSignUpResource = new SignUpUserResource(
            1, // Id
            "owneruser", // Username
            "Owner", // Name
            "User", // Surname
            "owner@example.com", // Email
            1234567890, // Phone
            "Active", // State
            "StrongPassw0rd!" // Password
        );

        mockOwnerCommandService
            .Setup(service => service.Handle(It.IsAny<SignUpUserCommand>()))
            .ReturnsAsync(true);

        mockOwnerCredentialCommandService
            .Setup(service => service.Handle(It.IsAny<CreateUserCredentialCommand>()))
            .ReturnsAsync(true);

        var authenticationController = new AuthenticationController(
            mockAdminCommandService.Object,
            mockAdminCredentialCommandService.Object,
            mockWorkerCommandService.Object,
            mockWorkerCredentialCommandService.Object,
            mockOwnerCommandService.Object,
            mockOwnerCredentialCommandService.Object);

        // Act
        var actionResult = await authenticationController.SignUpOwner(validSignUpResource);

        // Assert
        var okResult = actionResult as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo("User created correctly!"));
    }
}