using SweetManagerWebService.ResourceManagement.Domain.Model.Aggregates;
using SweetManagerWebService.ResourceManagement.Domain.Model.Queries.Report;
using SweetManagerWebService.ResourceManagement.Domain.Repositories;
using SweetManagerWebService.ResourceManagement.Domain.Services.Report;

namespace SweetManagerWebService.ResourceManagement.Application.QueryService;

public class ReportQueryService(IReportRepository reportRepository) : IReportQueryService
{
    public async Task<IEnumerable<Report>> Handle(GetAllReportsQuery query)
    {
        return await reportRepository.ListAsync();
    }

    public async Task<Report?> Handle(GetReportByIdQuery query)
    {
        return await reportRepository.FindByIdAsync(query.Id);
    }
}