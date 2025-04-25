using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.IAM.Domain.Model.Commands.Role;
using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.IAM.Domain.Model.ValueObjects;
using SweetManagerWebService.IAM.Domain.Repositories.Roles;
using SweetManagerWebService.IAM.Domain.Services.Roles;

namespace SweetManagerWebService.IAM.Application.Internal.CommandServices.Roles;

public class RoleCommandService(IRoleRepository roleRepository, IUnitOfWork unitOfWork) : IRoleCommandService
{
    public async Task<bool> Handle(SeedRolesCommand command)
    {
        foreach (var role in Enum.GetValues(typeof(ERoles)))
        {
            if (await roleRepository.FindByName(role.ToString()!) is null)
            {
                await roleRepository.AddAsync(new Role(role.ToString()!));
            }
        }

        await unitOfWork.CompleteAsync();

        return true;
    }
}