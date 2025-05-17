using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.ResourceManagement.Domain.Model.Aggregates;
using SweetManagerWebService.ResourceManagement.Domain.Model.Commands;
using SweetManagerWebService.ResourceManagement.Domain.Model.Queries.Report;
using SweetManagerWebService.ResourceManagement.Domain.Services.Report;
using SweetManagerWebService.ResourceManagement.Interfaces.REST;
using SweetManagerWebService.ResourceManagement.Interfaces.REST.Resources.Report;

namespace SweetManagerWebService.Tests.UnitTests;

[TestFixture]
public class ReportsControllerTests
{
    // ✅ Test 1: Crear reporte exitosamente
    [Test]
    public async Task CreateReport_ReturnsOk()
    {
        var mockCommand = new Mock<IReportCommandService>();
        var mockQuery = new Mock<IReportQueryService>();

        var resource = new CreateReportResource(1, 1, 1, "Reporte título", "Descripción", "http://file.url");

        mockCommand.Setup(s => s.Handle(It.IsAny<CreateReportCommand>())).ReturnsAsync(true);

        var controller = new ReportsController(mockCommand.Object, mockQuery.Object);

        var result = await controller.CreateReport(resource);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        Assert.That(((OkObjectResult)result).Value, Is.True);
    }

    // ✅ Test 2: Falla en creación de reporte (retorna BadRequest)
    [Test]
    public async Task CreateReport_WhenFails_ReturnsBadRequest()
    {
        var mockCommand = new Mock<IReportCommandService>();
        var mockQuery = new Mock<IReportQueryService>();

        var resource = new CreateReportResource(1, 1, 1, "Fallido", "Descripción", "http://file.fail");

        mockCommand.Setup(s => s.Handle(It.IsAny<CreateReportCommand>())).ReturnsAsync(false);

        var controller = new ReportsController(mockCommand.Object, mockQuery.Object);

        var result = await controller.CreateReport(resource);

        Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Error creating report."));
    }

    // ✅ Test 3: Obtener todos los reportes por hotelId
    [Test]
    public async Task AllReports_ReturnsList()
    {
        var mockCommand = new Mock<IReportCommandService>();
        var mockQuery = new Mock<IReportQueryService>();

        var reports = new List<Report>
        {
            new Report(1, 1, 1, "url1", "titulo1", "desc1"),
            new Report(1, 1, 2, "url2", "titulo2", "desc2")
        };

        mockQuery.Setup(q => q.Handle(It.Is<GetAllReportsQuery>(x => x.HotelId == 10)))
                 .ReturnsAsync(reports);

        var controller = new ReportsController(mockCommand.Object, mockQuery.Object);

        var result = await controller.AllReports(10);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var list = ((OkObjectResult)result).Value as IEnumerable<ReportResource>;
        Assert.That(list, Is.Not.Null);
        Assert.That(list.Count(), Is.EqualTo(2));
    }

    // ✅ Test 4: Obtener reporte por ID válido
    [Test]
    public async Task ReportById_ValidId_ReturnsOk()
    {
        var mockCommand = new Mock<IReportCommandService>();
        var mockQuery = new Mock<IReportQueryService>();

        var report = new Report(1, 1, 1, "url", "Reporte", "desc");

        mockQuery.Setup(q => q.Handle(It.Is<GetReportByIdQuery>(x => x.Id == 1)))
                 .ReturnsAsync(report);

        var controller = new ReportsController(mockCommand.Object, mockQuery.Object);

        var result = await controller.ReportById(1);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var returned = ((OkObjectResult)result).Value as ReportResource;
        Assert.That(returned.Title, Is.EqualTo("Reporte"));
    }

    // ✅ Test 5: Reporte no encontrado por ID (retorna NotFound)
    [Test]
    public async Task ReportById_NotFound_ReturnsNotFound()
    {
        var mockCommand = new Mock<IReportCommandService>();
        var mockQuery = new Mock<IReportQueryService>();

        mockQuery.Setup(q => q.Handle(It.IsAny<GetReportByIdQuery>())).ReturnsAsync((Report?)null);

        var controller = new ReportsController(mockCommand.Object, mockQuery.Object);

        var result = await controller.ReportById(99);

        Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        var error = ((NotFoundObjectResult)result).Value;
        Assert.That(error.ToString(), Does.Contain("Report not found"));
    }
}
