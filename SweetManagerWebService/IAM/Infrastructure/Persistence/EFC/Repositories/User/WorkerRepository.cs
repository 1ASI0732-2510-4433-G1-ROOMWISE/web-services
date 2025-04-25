using Microsoft.EntityFrameworkCore;
using SweetManagerWebService.Communication.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Repositories.Users;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.IAM.Infrastructure.Persistence.EFC.Repositories.User;

public class WorkerRepository(SweetManagerContext context) : BaseRepository<Worker>(context), IWorkerRepository
{
    public async Task<IEnumerable<Worker>> FindAllByHotelId(int hotelId)
        => await Task.Run(() => (
            from wo in Context.Set<Worker>().ToList()
            join nt in Context.Set<Notification>().ToList()
                on wo.Id equals nt.WorkersId
            join ow in Context.Set<Owner>().ToList()
                on nt.OwnersId equals ow.Id
            join ho in Context.Set<Hotel>().ToList()
                on ow.Id equals ho.OwnersId
            where ho.Id.Equals(hotelId)
            select wo
        ).ToList());

    public async Task<Worker?> FindById(int id)
        => await Task.Run(() => (
            from wo in Context.Set<Worker>().ToList()
            where wo.Id.Equals(id)
            select wo
        ).FirstOrDefault());

    public async Task<Worker?> FindByEmail(string email)
        => await Task.Run(() => (
            from wo in Context.Set<Worker>().ToList()
            where wo.Email.Equals(email)
            select wo
        ).FirstOrDefault());

    public async Task<int?> FindIdByEmail(string email)
        => await Task.Run(() => (
            from wo in Context.Set<Worker>().ToList()
            where wo.Email.Equals(email)
            select wo.Id
        ).FirstOrDefault());

    public async Task<bool> ExecuteUpdateWorkerEmailAsync(string email, int id)
        => await Context.Set<Worker>().Where(w => w.Id.Equals(id))
            .ExecuteUpdateAsync(w => w.SetProperty(p => p.Email, email)) > 0;

    public async Task<bool> ExecuteUpdateWorkerPhoneAsync(int phone, int id)
        => await Context.Set<Worker>().Where(w => w.Id.Equals(id))
            .ExecuteUpdateAsync(w => w.SetProperty(p => p.Phone, phone)) > 0;

    public async Task<int?> FindHotelIdByWorkerId(int id)
        => await Task.Run(() => (
            from wo in Context.Set<Worker>().ToList()
            join nt in Context.Set<Notification>().ToList()
                on wo.Id equals nt.WorkersId
            join ow in Context.Set<Owner>().ToList()
                on nt.OwnersId equals ow.Id
            join ho in Context.Set<Hotel>().ToList()
                on ow.Id equals ho.OwnersId
            where wo.Id.Equals(id)
            select ho.Id
        ).FirstOrDefault());
}