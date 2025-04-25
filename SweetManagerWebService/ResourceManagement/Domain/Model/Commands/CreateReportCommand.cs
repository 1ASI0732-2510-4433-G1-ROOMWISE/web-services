namespace SweetManagerWebService.ResourceManagement.Domain.Model.Commands;

public record CreateReportCommand(int TypesReportsId, int AdminsId, int WorkersId, string FileUrl, string Title, string Description);