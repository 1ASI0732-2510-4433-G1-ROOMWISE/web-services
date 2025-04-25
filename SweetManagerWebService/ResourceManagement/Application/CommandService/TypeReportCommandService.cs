using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.ResourceManagement.Domain.Model.Commands;
using SweetManagerWebService.ResourceManagement.Domain.Model.Entities;
using SweetManagerWebService.ResourceManagement.Domain.Model.ValueObjects;
using SweetManagerWebService.ResourceManagement.Domain.Repositories;
using SweetManagerWebService.ResourceManagement.Domain.Services.TypeReport;

namespace SweetManagerWebService.ResourceManagement.Application.CommandService;

public class TypeReportCommandService(ITypeReportRepository typeReportRepository,
    IUnitOfWork unitOfWork) : ITypeReportCommandService
{
    public async Task<bool> Handle(SeedTypeReportsCommand command)
    {
        foreach (var typeReport in Enum.GetValues(typeof(ETypeReports)))
        {
            if (await typeReportRepository.FindByNameAsync(typeReport.ToString()!) is false)
            {
                await typeReportRepository.AddAsync(new TypeReport(typeReport.ToString()!));
            }
        }

        await unitOfWork.CompleteAsync();
        
        return true;
    }
}