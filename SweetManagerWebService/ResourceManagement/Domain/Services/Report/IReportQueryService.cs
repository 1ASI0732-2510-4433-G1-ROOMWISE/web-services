using SweetManagerWebService.ResourceManagement.Domain.Model.Queries.Report;

namespace SweetManagerWebService.ResourceManagement.Domain.Services.Report;

public interface IReportQueryService
{
    Task<IEnumerable<Model.Aggregates.Report>> Handle(GetAllReportsQuery query);

    Task<Model.Aggregates.Report?> Handle(GetReportByIdQuery query);
}