using SweetManagerWebService.ResourceManagement.Interfaces.REST.Resources.Report;

namespace SweetManagerWebService.ResourceManagement.Interfaces.REST.Transform.Report
{
    public class ReportResourceFromEntityAssembler
    {
        public static ReportResource ToResourceFromEntity(Domain.Model.Aggregates.Report entity) =>
            new(entity.Id, entity.TypesReportsId, entity.AdminsId, entity.WorkersId, entity.Title, entity.Description, entity.FileUrl);
    }
}