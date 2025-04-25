using SweetManagerWebService.IAM.Domain.Model.Commands;
using SweetManagerWebService.IAM.Domain.Model.Commands.Authentication.User;

namespace SweetManagerWebService.IAM.Domain.Services.Users.Admin;

public interface IAdminCommandService
{
    Task<bool> Handle(SignUpUserCommand command);

    Task<bool> Handle(UpdateUserCommand command);

    Task<dynamic?> Handle(SignInCommand command);
    
}