using SweetManagerWebService.ResourceManagement.Interfaces.REST.Resources.TypeReport;

namespace SweetManagerWebService.ResourceManagement.Interfaces.REST.Transform.TypeReport
{
    public class TypeReportResourceFromEntityAssembler
    {
        public static TypeReportResource ToResourceFromEntity(Domain.Model.Entities.TypeReport entity) =>
            new(entity.Id, entity.Name, entity.Name, null);
    }
}