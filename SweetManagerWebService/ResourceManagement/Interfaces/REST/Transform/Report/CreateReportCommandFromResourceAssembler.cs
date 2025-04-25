using SweetManagerWebService.ResourceManagement.Domain.Model.Commands;
using SweetManagerWebService.ResourceManagement.Interfaces.REST.Resources.Report;

namespace SweetManagerWebService.ResourceManagement.Interfaces.REST.Transform.Report
{
    public class CreateReportCommandFromResourceAssembler
    {
        public static CreateReportCommand ToCommandFromResource(CreateReportResource resource) =>
            new CreateReportCommand(resource.TypesReportsId, resource.AdminsId, resource.WorkersId, resource.FileUrl, resource.Title, resource.Description);
    }
}