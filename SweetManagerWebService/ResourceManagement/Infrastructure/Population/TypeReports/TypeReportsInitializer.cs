using SweetManagerWebService.ResourceManagement.Domain.Model.Commands;
using SweetManagerWebService.ResourceManagement.Domain.Model.Queries.TypeReport;
using SweetManagerWebService.ResourceManagement.Domain.Services.TypeReport;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace SweetManagerWebService.ResourceManagement.Infrastructure.Population.TypeReports;

public class TypeReportsInitializer(ITypeReportCommandService typeReportCommandService,
    ITypeReportQueryService typeReportQueryService, SweetManagerContext context)
{
    public async Task InitializeAsync()
    {
        // Check if the type reports table is empty
        var result = await typeReportQueryService.Handle(new GetAllTypesReportsQuery());

        if (!result.Any())
        {
            // Prepopulate the empty table

            await typeReportCommandService.Handle(new SeedTypeReportsCommand());

        }
    }
}