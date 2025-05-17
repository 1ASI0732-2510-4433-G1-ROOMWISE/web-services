using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SweetManagerWebService.ResourceManagement.Domain.Model.Entities;
using SweetManagerWebService.ResourceManagement.Domain.Model.Queries.TypeReport;
using SweetManagerWebService.ResourceManagement.Domain.Services.TypeReport;
using SweetManagerWebService.ResourceManagement.Interfaces.REST;
using SweetManagerWebService.ResourceManagement.Interfaces.REST.Resources.TypeReport;

namespace SweetManagerWebService.Tests.UnitTests;

[TestFixture]
public class TypesReportsControllerTests
{
    // ✅ Test 1: Obtener todos los tipos de reportes exitosamente
    [Test]
    public async Task AllTypesReports_ReturnsList()
    {
        var mockQuery = new Mock<ITypeReportQueryService>();

        var types = new List<TypeReport>
        {
            new TypeReport("MAINTENANCE"),
            new TypeReport("SECURITY")
        };

        mockQuery.Setup(q => q.Handle(It.IsAny<GetAllTypesReportsQuery>())).ReturnsAsync(types);

        var controller = new TypesReportsController(mockQuery.Object);

        var result = await controller.AllTypesReports();

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var list = ((OkObjectResult)result).Value as IEnumerable<TypeReportResource>;
        Assert.That(list, Is.Not.Null);
        Assert.That(list.Count(), Is.EqualTo(2));
    }

    

    // ✅ Test 3: Obtener tipo de reporte por ID válido
    [Test]
    public async Task TypeReportById_ReturnsCorrectItem()
    {
        var mockQuery = new Mock<ITypeReportQueryService>();

        var typeReport = new TypeReport("SECURITY");

        mockQuery.Setup(q => q.Handle(It.Is<GetTypeReportByIdQuery>(x => x.Id == 5)))
                 .ReturnsAsync(typeReport);

        var controller = new TypesReportsController(mockQuery.Object);

        var result = await controller.TypeReportById(5);

        Assert.That(result, Is.TypeOf<OkObjectResult>());
        var resource = ((OkObjectResult)result).Value as TypeReportResource;
        Assert.That(resource.Title, Is.EqualTo("SECURITY"));
    }

    

    // ✅ Test 5: ID válido pero TypeReport no existe (retorna NotFound)
    [Test]
    public async Task TypeReportById_NotFound_ReturnsNotFound()
    {
        var mockQuery = new Mock<ITypeReportQueryService>();

        mockQuery.Setup(q => q.Handle(It.IsAny<GetTypeReportByIdQuery>()))
                 .ReturnsAsync((TypeReport?)null);

        var controller = new TypesReportsController(mockQuery.Object);

        var result = await controller.TypeReportById(99);

        Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        var value = ((NotFoundObjectResult)result).Value;
        Assert.That(value.ToString(), Does.Contain("TypeReport not found"));
    }
}
