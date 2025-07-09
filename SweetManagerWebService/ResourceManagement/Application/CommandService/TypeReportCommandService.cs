using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.ResourceManagement.Domain.Model.Commands;
using SweetManagerWebService.ResourceManagement.Domain.Model.Entities;
using SweetManagerWebService.ResourceManagement.Domain.Model.ValueObjects;
using SweetManagerWebService.ResourceManagement.Domain.Repositories;
using SweetManagerWebService.ResourceManagement.Domain.Services.TypeReport;

namespace SweetManagerWebService.ResourceManagement.Application.CommandService;

// Service class to handle type report-related commands
public class TypeReportCommandService(ITypeReportRepository typeReportRepository,
    IUnitOfWork unitOfWork) : ITypeReportCommandService
{
    // Method to handle seeding of type reports into the repository
    public async Task<bool> Handle(SeedTypeReportsCommand command)
    {
        // Loop through all values in the ETypeReports enum
        foreach (var typeReport in Enum.GetValues(typeof(ETypeReports)))
        {
            // Check if the type report already exists in the repository
            if (await typeReportRepository.FindByNameAsync(typeReport.ToString()!) is false)
            {
                // Add the type report to the repository if it doesn't exist
                await typeReportRepository.AddAsync(new TypeReport(typeReport.ToString()!));
            }
        }

        // Completes the unit of work to persist changes to the database
        await unitOfWork.CompleteAsync();
        
        // Returns true if the seed operation was successful
        return true;
    }
}
