using Microsoft.EntityFrameworkCore;
using SweetManagerWebService.Communication.Domain.Model.Aggregates;
using SweetManagerWebService.Communication.Domain.Repositories;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Entities.Assignments;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.Communication.Infrastructure.Persistence.EFC.Repositories
{
    public class NotificationRepository(SweetManagerContext context) : BaseRepository<Notification>(context), INotificationRepository
    {
        public async Task<IEnumerable<Notification>> FindByTypeNotificationIdAsync(int typeNotificationId)
            => await Task.Run(() => (
                from ntf in Context.Set<Notification>().ToList()
                join own in Context.Set<Owner>().ToList()
                    on ntf.OwnersId equals own.Id
                join htl in Context.Set<Hotel>().ToList()
                    on own.Id equals htl.OwnersId
                where ntf.TypesNotificationsId.Equals(typeNotificationId) 
                select ntf
            ).ToList());

        public async Task<IEnumerable<Notification>> FindAllByHotelIdAsync(int hotelId)
        {
            Task<IEnumerable<Notification>> queryAsync = new(() => (
                from ntf in Context.Set<Notification>().ToList()
                join ow in Context.Set<Owner>().ToList() on ntf.OwnersId equals ow.Id
                join ho in Context.Set<Hotel>().ToList() on ow.Id equals ho.OwnersId
                where ho.Id.Equals(hotelId)
                select ntf
            ).ToList());
            
            queryAsync.Start();

            var result = await queryAsync;

            return result;
        }

        public async Task<IEnumerable<Notification>> FindAllByWorkerIdAsync(int workerId)
        {
            Task<IEnumerable<Notification>> queryAsync = new(() => (
                from wo in Context.Set<Worker>().ToList()
                join ass in Context.Set<AssignmentWorker>().ToList() on wo.Id equals ass.WorkersId
                join ad in Context.Set<Admin>().ToList() on ass.AdminsId equals ad.Id
                join noti in Context.Set<Notification>().ToList() on ad.Id equals noti.AdminsId
                where wo.Id.Equals(workerId) && noti.OwnersId.Equals(null)
                select noti
            ));
            
            queryAsync.Start();

            var result = await queryAsync;

            return result;
        }

        public async Task<IEnumerable<Notification>> FindAllByHotelIdAndExitsOwnersIdAsync(int hotelId)
        {
            Task<IEnumerable<Notification>> queryAsync = new(() => (
                from no in Context.Set<Notification>().ToList()
                join ow in Context.Set<Owner>().ToList() on no.OwnersId equals ow.Id
                join ho in Context.Set<Hotel>().ToList() on ow.Id equals ho.OwnersId
                where ho.Id.Equals(hotelId)
                select no
            ));
            
            queryAsync.Start();

            var result = await queryAsync;
            
            return result.Where(n => n is { OwnersId: not null, AdminsId: null, WorkersId: null });
        }
    }
}