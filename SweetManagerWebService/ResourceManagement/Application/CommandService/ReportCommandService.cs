using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.ResourceManagement.Domain.Model.Commands;
using SweetManagerWebService.ResourceManagement.Domain.Repositories;
using SweetManagerWebService.ResourceManagement.Domain.Services.Report;

namespace SweetManagerWebService.ResourceManagement.Application.CommandService;

public class ReportCommandService (IReportRepository reportRepository, IUnitOfWork unitOfWork) : IReportCommandService
{
    public async Task<bool> Handle(CreateReportCommand command)
    {
        try
        {
            await reportRepository.AddAsync(new (command));
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}