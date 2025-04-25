using SweetManagerWebService.Commerce.Domain.Model.Commands.Contracts;

namespace SweetManagerWebService.Commerce.Domain.Services.Contracts;

public interface IContractOwnerCommandService
{
    Task<bool> Handle(CreateContractOwnerCommand command);
    
}