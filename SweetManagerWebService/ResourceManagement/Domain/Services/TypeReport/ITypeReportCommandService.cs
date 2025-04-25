using SweetManagerWebService.ResourceManagement.Domain.Model.Commands;

namespace SweetManagerWebService.ResourceManagement.Domain.Services.TypeReport;

public interface ITypeReportCommandService
{
    Task<bool> Handle(SeedTypeReportsCommand command);
    
}