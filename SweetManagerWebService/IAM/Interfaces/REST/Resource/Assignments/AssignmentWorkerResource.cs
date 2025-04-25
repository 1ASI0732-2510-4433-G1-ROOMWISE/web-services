namespace SweetManagerWebService.IAM.Interfaces.REST.Resource.Assignments;

public record AssignmentWorkerResource(int Id, int? WorkersAreasId, int? WorkersId, int? AdminsId,
    DateTime StartDate, DateTime FinalDate, string State);