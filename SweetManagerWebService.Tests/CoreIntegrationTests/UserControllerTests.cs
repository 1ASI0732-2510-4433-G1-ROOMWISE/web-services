using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.IAM.Application.Internal.OutboundServices.ACL;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Commands.Authentication.User;
using SweetManagerWebService.IAM.Domain.Model.Queries;
using SweetManagerWebService.IAM.Domain.Services.Users.Admin;
using SweetManagerWebService.IAM.Domain.Services.Users.Owner;
using SweetManagerWebService.IAM.Domain.Services.Users.Worker;
using SweetManagerWebService.IAM.Interfaces.REST;
using SweetManagerWebService.IAM.Interfaces.REST.Resource.Authentication.User;

namespace SweetManagerWebService.Tests.CoreIntegrationTests;

[TestFixture]
public class UserControllerTests
{
    [Test]
    public async Task GetOwnerById_WithValidId_ReturnsOkWithUserResource()
    {
        var mockOwnerQueryService = new Mock<IOwnerQueryService>();
        var ownerId = 1;
        var expectedOwner = new Owner(ownerId, "owneruser", "Owner", "User", 1, 1234567890, "owner@example.com", "ACTIVE");

        mockOwnerQueryService
            .Setup(service => service.Handle(It.Is<GetUserByIdQuery>(query => query.Id == ownerId)))
            .ReturnsAsync(expectedOwner);

        var userController = new UserController(
            null, null, null, null, null, mockOwnerQueryService.Object, null, null);

        var actionResult = await userController.GetOwnerById(ownerId);

        var okResult = actionResult as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);

        var userResource = okResult.Value as UserResource;
        Assert.That(userResource, Is.Not.Null);
        Assert.That(userResource.Id, Is.EqualTo(expectedOwner.Id));
    }

    [Test]
    public async Task GetAllAdmins_WithValidHotelId_ReturnsOkWithAdminResources()
    {
        var mockAdminQueryService = new Mock<IAdminQueryService>();
        var hotelId = 1;
        var expectedAdmins = new List<Admin>
        {
            new(1, "admin1", "admin1@example.com", 2, "Admin", "One", 1234567890, "ACTIVE"),
            new(2, "admin2", "admin2@example.com", 2, "Admin", "Two", 1234567891, "ACTIVE")
        };

        mockAdminQueryService
            .Setup(service => service.Handle(It.Is<GetAllUsersQuery>(query => query.HotelId == hotelId)))
            .ReturnsAsync(expectedAdmins);

        var userController = new UserController(
            null, null, null, mockAdminQueryService.Object, null, null, null, null);

        var actionResult = await userController.GetAdmins(hotelId);

        var okResult = actionResult as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);

        var adminResources = okResult.Value as IEnumerable<UserResource>;
        Assert.That(adminResources, Is.Not.Null);
        Assert.That(adminResources.Count(), Is.EqualTo(expectedAdmins.Count));
    }
    

    [Test]
    public async Task UpdateAdmin_WithValidData_ReturnsOkResult()
    {
        var mockAdminCommandService = new Mock<IAdminCommandService>();
        var updateResource = new UpdateUserResource(1, "Email", "newemail@example.com");

        mockAdminCommandService
            .Setup(service => service.Handle(It.IsAny<UpdateUserCommand>()))
            .ReturnsAsync(true);

        var userController = new UserController(
            null, mockAdminCommandService.Object, null, null, null, null, null, null);

        var actionResult = await userController.UpdateAdmin(updateResource);

        var okResult = actionResult as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo("User updated correctly!"));
    }
}