using SweetManagerWebService.IAM.Domain.Model.Queries;

namespace SweetManagerWebService.IAM.Domain.Services.Users.Owner;

public interface IOwnerQueryService
{
    Task<Model.Aggregates.Owner?> Handle(GetAllUsersQuery query);

    Task<Model.Aggregates.Owner?> Handle(GetUserByIdQuery query);

    Task<Model.Aggregates.Owner?> Handle(GetUserByEmailQuery query);

    Task<int?> Handle(GetUserIdByEmailQuery query);
    
}