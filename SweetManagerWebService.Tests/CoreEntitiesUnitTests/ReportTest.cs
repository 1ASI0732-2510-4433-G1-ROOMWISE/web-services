using SweetManagerWebService.ResourceManagement.Domain.Model.Aggregates;
using SweetManagerWebService.ResourceManagement.Domain.Model.Commands;
using SweetManagerWebService.ResourceManagement.Domain.Model.ValueObjects;

namespace SweetManagerWebService.Tests.CoreEntitiesUnitTests;

[TestFixture]
public class ReportTests
{
    [Test]
    public void Report_Constructor_WithParameters_ShouldInitializeProperties()
    {
        // Arrange
        var typesReportsId = 1;
        var adminsId = 2;
        var workersId = 3;
        var fileUrl = "http://example.com/reports/maintenance.pdf";
        var title = "Reporte de mantenimiento mensual";
        var description = "Reporte detallado del mantenimiento realizado en mayo 2025";

        // Act
        var report = new Report(
            typesReportsId,
            adminsId,
            workersId,
            fileUrl,
            title,
            description
        );

        // Assert
        Assert.That(report.TypesReportsId, Is.EqualTo(typesReportsId));
        Assert.That(report.AdminsId, Is.EqualTo(adminsId));
        Assert.That(report.WorkersId, Is.EqualTo(workersId));
        Assert.That(report.FileUrl, Is.EqualTo(fileUrl));
        Assert.That(report.Title, Is.EqualTo(title));
        Assert.That(report.Description, Is.EqualTo(description));
    }

    [Test]
    public void Report_DefaultConstructor_ShouldInitializeWithDefaultValues()
    {
        // Act
        var report = new Report();

        // Assert
        Assert.That(report.TypesReportsId, Is.EqualTo(0));
        Assert.That(report.AdminsId, Is.EqualTo(0));
        Assert.That(report.WorkersId, Is.EqualTo(0));
        Assert.That(report.FileUrl, Is.EqualTo(string.Empty));
        Assert.That(report.Title, Is.EqualTo(string.Empty));
        Assert.That(report.Description, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Report_Constructor_WithCommand_ShouldInitializeProperties()
    {
        // Arrange
        var command = new CreateReportCommand(
            TypesReportsId: (int)ETypeReports.MAINTENANCE,
            AdminsId: 5,
            WorkersId: 10,
            FileUrl: "http://example.com/reports/security-report.pdf",
            Title: "Informe de seguridad",
            Description: "Informe de incidencias de seguridad del primer trimestre"
        );

        // Act
        var report = new Report(command);

        // Assert
        Assert.That(report.TypesReportsId, Is.EqualTo(command.TypesReportsId));
        Assert.That(report.AdminsId, Is.EqualTo(command.AdminsId));
        Assert.That(report.WorkersId, Is.EqualTo(command.WorkersId));
        Assert.That(report.FileUrl, Is.EqualTo(command.FileUrl));
        Assert.That(report.Title, Is.EqualTo(command.Title));
        Assert.That(report.Description, Is.EqualTo(command.Description));
    }

    [Test]
    public void Report_Constructor_WithWrongTypeReport_ShouldFailValidation()
    {
        // Arrange
        var invalidTypeReportId = 99; // Un ID que no corresponde a ningún tipo de reporte válido
        var adminsId = 2;
        var workersId = 3;
        var fileUrl = "http://example.com/reports/invalid.pdf";
        var title = "Reporte inválido";
        var description = "Este reporte tiene un tipo inválido";

        // Act
        var report = new Report(
            invalidTypeReportId,
            adminsId,
            workersId,
            fileUrl,
            title,
            description
        );

        // Assert - Esta prueba fallará intencionalmente
        // Solo son válidos los tipos definidos en ETypeReports (0, 1, 2)
        Assert.That(report.TypesReportsId, Is.LessThan(Enum.GetValues(typeof(ETypeReports)).Length));
    }
}