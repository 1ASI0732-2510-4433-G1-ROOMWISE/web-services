using SweetManagerWebService.SupplyManagement.Domain.Model.Aggregates;
using SweetManagerWebService.SupplyManagement.Domain.Model.Commands;

namespace SweetManagerWebService.SupplyManagement.Domain.Services;

public interface ISupplyCommandService
{
    Task<bool> Handle(CreateSupplyCommand command);
    Task<bool> Handle(UpdateSupplyCommand command);
    Task <bool> Handle(DeleteSupplyCommand command);
}