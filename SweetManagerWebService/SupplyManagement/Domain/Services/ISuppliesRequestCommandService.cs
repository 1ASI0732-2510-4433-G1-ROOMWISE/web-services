using SweetManagerWebService.SupplyManagement.Domain.Model.Commands;

namespace SweetManagerWebService.SupplyManagement.Domain.Services;

public interface ISuppliesRequestCommandService
{
    Task<bool> Handle(CreateSuppliesRequestCommand command);
}