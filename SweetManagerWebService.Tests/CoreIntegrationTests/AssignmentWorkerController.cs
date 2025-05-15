using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.IAM.Domain.Model.Commands.Assignments;
using SweetManagerWebService.IAM.Domain.Model.Entities.Assignments;
using SweetManagerWebService.IAM.Domain.Model.Queries;
using SweetManagerWebService.IAM.Domain.Services.Assignments;
using SweetManagerWebService.IAM.Interfaces.REST;
using SweetManagerWebService.IAM.Interfaces.REST.Resource.Assignments;

namespace SweetManagerWebService.Tests.CoreIntegrationTests;

[TestFixture]
public class AssignmentWorkerControllerTests
{
    private Mock<IAssignmentWorkerCommandService> _mockAssignmentWorkerCommandService;
    private Mock<IAssignmentWorkerQueryService> _mockAssignmentWorkerQueryService;
    private AssignmentWorkerController _assignmentWorkerController;

    [SetUp]
    public void SetUp()
    {
        _mockAssignmentWorkerCommandService = new Mock<IAssignmentWorkerCommandService>();
        _mockAssignmentWorkerQueryService = new Mock<IAssignmentWorkerQueryService>();
        _assignmentWorkerController = new AssignmentWorkerController(
            _mockAssignmentWorkerCommandService.Object,
            _mockAssignmentWorkerQueryService.Object);
    }

    [Test]
    public async Task CreateAssignmentWorker_WithValidData_ReturnsOkResult()
    {
        // Arrange
        var validResource = new CreateAssignmentWorkerResource(
            1, // WorkerAreasId
            2, // WorkersId
            3, // AdminsId
            "2025-05-15", // StartDate
            "2025-06-15", // FinalDate
            "ACTIVE" // State
        );

        _mockAssignmentWorkerCommandService
            .Setup(service => service.Handle(It.IsAny<CreateAssignmentWorkerCommand>()))
            .ReturnsAsync(true);

        // Act
        var actionResult = await _assignmentWorkerController.CreateAssignmentWorker(validResource);

        // Assert
        var okResult = actionResult as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo("Assignment worker created correctly!"));
    }

    [Test]
    public async Task CreateAssignmentWorker_WithInvalidData_ReturnsBadRequest()
    {
        // Arrange
        var invalidResource = new CreateAssignmentWorkerResource(
            1, // WorkerAreasId
            2, // WorkersId
            3, // AdminsId
            "2025-06-15", // StartDate later than FinalDate
            "2025-05-15", // FinalDate
            "ACTIVE" // State
        );

        _mockAssignmentWorkerCommandService
            .Setup(service => service.Handle(It.IsAny<CreateAssignmentWorkerCommand>()))
            .ThrowsAsync(new Exception("Start date must be before final date"));

        // Act
        var actionResult = await _assignmentWorkerController.CreateAssignmentWorker(invalidResource);

        // Assert
        var badRequestResult = actionResult as BadRequestObjectResult;
        Assert.That(badRequestResult, Is.Not.Null);
        Assert.That(badRequestResult.Value, Is.EqualTo("Start date must be before final date"));
    }
    

    [Test]
    public async Task GetAssignmentWorkersByWorkerId_WhenExceptionOccurs_ReturnsBadRequest()
    {
        // Arrange
        var workerId = 2;
        var errorMessage = "Error retrieving assignments";

        _mockAssignmentWorkerQueryService
            .Setup(service => service.Handle(It.IsAny<GetAssignmentWorkerByWorkerIdQuery>()))
            .ThrowsAsync(new Exception(errorMessage));

        // Act
        var actionResult = await _assignmentWorkerController.GetAssignmentWorkersByWorkerId(workerId);

        // Assert
        var badRequestResult = actionResult as BadRequestObjectResult;
        Assert.That(badRequestResult, Is.Not.Null);
        Assert.That(badRequestResult.Value, Is.EqualTo(errorMessage));
    }

    

    [Test]
    public async Task GetAssignmentWorkersByAdminId_WhenExceptionOccurs_ReturnsBadRequest()
    {
        // Arrange
        var adminId = 3;
        var errorMessage = "Error retrieving admin assignments";

        _mockAssignmentWorkerQueryService
            .Setup(service => service.Handle(It.IsAny<GetAssignmentWorkerByAdminIdQuery>()))
            .ThrowsAsync(new Exception(errorMessage));

        // Act
        var actionResult = await _assignmentWorkerController.GetAssignmentWorkersByAdminId(adminId);

        // Assert
        var badRequestResult = actionResult as BadRequestObjectResult;
        Assert.That(badRequestResult, Is.Not.Null);
        Assert.That(badRequestResult.Value, Is.EqualTo(errorMessage));
    }
    
    [Test]
    public async Task GetAssignmentWorkersByWorkerAreaId_WhenExceptionOccurs_ReturnsBadRequest()
    {
        // Arrange
        var workerAreaId = 1;
        var errorMessage = "Error retrieving worker area assignments";

        _mockAssignmentWorkerQueryService
            .Setup(service => service.Handle(It.IsAny<GetAssignmentWorkerByWorkerAreaIdQuery>()))
            .ThrowsAsync(new Exception(errorMessage));

        // Act
        var actionResult = await _assignmentWorkerController.GetAssignmentWorkersByWorkerAreaId(workerAreaId);

        // Assert
        var badRequestResult = actionResult as BadRequestObjectResult;
        Assert.That(badRequestResult, Is.Not.Null);
        Assert.That(badRequestResult.Value, Is.EqualTo(errorMessage));
    }
}