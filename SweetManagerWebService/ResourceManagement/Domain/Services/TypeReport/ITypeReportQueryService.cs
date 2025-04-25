using SweetManagerWebService.ResourceManagement.Domain.Model.Queries.TypeReport;

namespace SweetManagerWebService.ResourceManagement.Domain.Services.TypeReport;

public interface ITypeReportQueryService
{
    Task<IEnumerable<Model.Entities.TypeReport>> Handle(GetAllTypesReportsQuery query);

    Task<Model.Entities.TypeReport?> Handle(GetTypeReportByIdQuery query);
}