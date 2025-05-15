using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.IAM.Domain.Model.Commands.Role;
using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.IAM.Domain.Model.Queries;
using SweetManagerWebService.IAM.Domain.Services.Roles;
using SweetManagerWebService.IAM.Interfaces.REST;
using SweetManagerWebService.IAM.Interfaces.REST.Resource.Assignments;
using SweetManagerWebService.IAM.Interfaces.REST.Resource.Authentication.Role;

namespace SweetManagerWebService.Tests.CoreIntegrationTests;

[TestFixture]
public class WorkerAreaControllerTests
{
    private Mock<IWorkerAreaCommandService> _mockWorkerAreaCommandService;
    private Mock<IWorkerAreaQueryService> _mockWorkerAreaQueryService;
    private WorkerAreaController _workerAreaController;

    [SetUp]
    public void SetUp()
    {
        _mockWorkerAreaCommandService = new Mock<IWorkerAreaCommandService>();
        _mockWorkerAreaQueryService = new Mock<IWorkerAreaQueryService>();
        _workerAreaController = new WorkerAreaController(
            _mockWorkerAreaCommandService.Object,
            _mockWorkerAreaQueryService.Object);
    }

    [Test]
    public async Task CreateWorkerArea_WithValidData_ReturnsOkResult()
    {
        // Arrange
        var validResource = new CreateWorkAreaResource(
            "Cleaning",
            1 // HotelId
        );

        _mockWorkerAreaCommandService
            .Setup(service => service.Handle(It.IsAny<CreateWorkAreaCommand>()))
            .ReturnsAsync(true);

        // Act
        var actionResult = await _workerAreaController.CreateWorkerArea(validResource);

        // Assert
        var okResult = actionResult as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo("Worker Area created correctly!"));
    }

    [Test]
    public async Task CreateWorkerArea_WithInvalidData_ReturnsBadRequest()
    {
        // Arrange
        var invalidResource = new CreateWorkAreaResource(
            "", // Invalid empty name
            1 // HotelId
        );

        _mockWorkerAreaCommandService
            .Setup(service => service.Handle(It.IsAny<CreateWorkAreaCommand>()))
            .ThrowsAsync(new Exception("Worker area name cannot be empty"));

        // Act
        var actionResult = await _workerAreaController.CreateWorkerArea(invalidResource);

        // Assert
        var badRequestResult = actionResult as BadRequestObjectResult;
        Assert.That(badRequestResult, Is.Not.Null);
        Assert.That(badRequestResult.Value, Is.EqualTo("Worker area name cannot be empty"));
    }

    [Test]
    public async Task GetAllWorkerAreas_ReturnsOkResultWithWorkerAreas()
    {
        // Arrange
        var hotelId = 1;
        var workerAreas = new List<WorkerArea>
        {
            new("Cleaning"),
            new( "Maintenance")
        };

        _mockWorkerAreaQueryService
            .Setup(service => service.Handle(It.IsAny<GetAllWorkerAreasByHotelIdQuery>()))
            .ReturnsAsync(workerAreas);

        // Act
        var actionResult = await _workerAreaController.GetAllWorkerAreas(hotelId);

        // Assert
        var okResult = actionResult as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        
        var resources = okResult.Value as IEnumerable<WorkAreaResource>;
        Assert.That(resources, Is.Not.Null);
        Assert.That(resources.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task GetWorkerAreaByName_WithNonExistingName_ReturnsBadRequest()
    {
        // Arrange
        var nonExistingName = "NonExisting";
        var hotelId = 1;

        _mockWorkerAreaQueryService
            .Setup(service => service.Handle(It.IsAny<GetWorkerAreaByNameAndHotelIdQuery>()))
            .ReturnsAsync((WorkerArea?)null);

        // Act
        var actionResult = await _workerAreaController.GetWorkerAreaByName(nonExistingName, hotelId);

        // Assert
        var badRequestResult = actionResult as BadRequestObjectResult;
        Assert.That(badRequestResult, Is.Not.Null);
        Assert.That(badRequestResult.Value, Is.EqualTo("Any work area has the given name"));
    }

    
    [Test]
    public async Task GetWorkerAreasByWorkerId_WithNonExistingWorkerId_ReturnsOkWithEmptyMessage()
    {
        // Arrange
        var nonExistingWorkerId = 999;

        _mockWorkerAreaQueryService
            .Setup(service => service.Handle(It.IsAny<GetWorkerAreaByWorkerId>()))
            .ReturnsAsync((string?)null);

        // Act
        var actionResult = await _workerAreaController.GetWorkerAreasByWorkerId(nonExistingWorkerId);

        // Assert
        var okResult = actionResult as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo("Empty"));
    }

    [Test]
    public async Task GetWorkerAreasByWorkerId_WhenExceptionOccurs_ReturnsBadRequest()
    {
        // Arrange
        var workerId = 1;
        var errorMessage = "Error retrieving worker area";

        _mockWorkerAreaQueryService
            .Setup(service => service.Handle(It.IsAny<GetWorkerAreaByWorkerId>()))
            .ThrowsAsync(new Exception(errorMessage));

        // Act
        var actionResult = await _workerAreaController.GetWorkerAreasByWorkerId(workerId);

        // Assert
        var badRequestResult = actionResult as BadRequestObjectResult;
        Assert.That(badRequestResult, Is.Not.Null);
        Assert.That(badRequestResult.Value, Is.EqualTo(errorMessage));
    }
}