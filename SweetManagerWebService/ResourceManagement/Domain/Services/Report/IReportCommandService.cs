using SweetManagerWebService.ResourceManagement.Domain.Model.Commands;

namespace SweetManagerWebService.ResourceManagement.Domain.Services.Report;

public interface IReportCommandService
{
    Task<bool> Handle (CreateReportCommand command);
}