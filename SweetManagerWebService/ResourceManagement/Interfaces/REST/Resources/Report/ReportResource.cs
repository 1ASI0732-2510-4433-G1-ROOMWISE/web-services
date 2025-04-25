namespace SweetManagerWebService.ResourceManagement.Interfaces.REST.Resources.Report;

public record ReportResource (int Id, int TypesReportsId, int AdminsId, int WorkersId, string Title, string Description,  string FileUrl);