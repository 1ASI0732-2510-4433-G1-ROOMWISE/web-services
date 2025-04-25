using SweetManagerWebService.Communication.Domain.Model.Entities;
using SweetManagerWebService.Communication.Domain.Model.Queries.TypeNotification;
using SweetManagerWebService.Communication.Domain.Repositories;
using SweetManagerWebService.Communication.Domain.Services.TypeNotification;

namespace SweetManagerWebService.Communication.Application.QueryService
{
    public class TypeNotificationQueryService : ITypeNotificationQueryService
    {
        private readonly ITypeNotificationRepository _typeNotificationRepository;

        public TypeNotificationQueryService(ITypeNotificationRepository typeNotificationRepository)
        {
            _typeNotificationRepository = typeNotificationRepository;
        }

        public async Task<IEnumerable<TypeNotification>> Handle(GetAllTypesNotificationsQuery query)
        {
            return await _typeNotificationRepository.ListAsync();
        }

        public async Task<TypeNotification?> Handle(GetTypeNotificationByIdQuery query)
        {
            return await _typeNotificationRepository.FindByIdAsync(query.Id);
        }
    }
}