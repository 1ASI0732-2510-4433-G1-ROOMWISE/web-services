using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.ResourceManagement.Domain.Model.Commands;
using SweetManagerWebService.ResourceManagement.Domain.Repositories;
using SweetManagerWebService.ResourceManagement.Domain.Services.Report;

namespace SweetManagerWebService.ResourceManagement.Application.CommandService;

// Service class to handle report creation commands
public class ReportCommandService (IReportRepository reportRepository, IUnitOfWork unitOfWork) : IReportCommandService
{
    // Method to handle the creation of a new report
    public async Task<bool> Handle(CreateReportCommand command)
    {
        try
        {
            // Adds the new report to the repository asynchronously
            await reportRepository.AddAsync(new (command));
            
            // Completes the unit of work to persist changes to the database
            await unitOfWork.CompleteAsync();
            
            // Returns true if the report was successfully created
            return true;
        }
        catch (Exception)
        {
            // Returns false in case of an error
            return false;
        }
    }
}
