using SweetManagerWebService.IAM.Domain.Model.Commands.Role;
using SweetManagerWebService.IAM.Domain.Model.Queries;
using SweetManagerWebService.IAM.Domain.Services.Roles;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace SweetManagerWebService.IAM.Infrastructure.Population.Roles;

public class RolesInitializer(IRoleCommandService roleCommandService,
    IRoleQueryService roleQueryService,
    IWorkerAreaQueryService workerAreaQueryService,
    SweetManagerContext context)
{
    public async Task InitializeAsync()
    {
        // Check if the role table is empty

        var result = await roleQueryService.Handle(new GetAllRolesQuery());

        if (!result.Any())
        {
            // Prepopulate the empty table

            await roleCommandService.Handle(new SeedRolesCommand());
        }
    }

}