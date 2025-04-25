using SweetManagerWebService.ResourceManagement.Domain.Model.Entities;
using SweetManagerWebService.ResourceManagement.Domain.Model.Queries.TypeReport;
using SweetManagerWebService.ResourceManagement.Domain.Repositories;
using SweetManagerWebService.ResourceManagement.Domain.Services.TypeReport;

namespace SweetManagerWebService.ResourceManagement.Application.QueryService
{
    public class TypeReportQueryService : ITypeReportQueryService
    {
        private readonly ITypeReportRepository _typeReportRepository;

        public TypeReportQueryService(ITypeReportRepository typeReportRepository)
        {
            _typeReportRepository = typeReportRepository;
        }

        public async Task<IEnumerable<TypeReport>> Handle(GetAllTypesReportsQuery query)
        {
            return await _typeReportRepository.ListAsync();
        }

        public async Task<TypeReport?> Handle(GetTypeReportByIdQuery query)
        {
            return await _typeReportRepository.FindByIdAsync(query.Id);
        }
    }
}