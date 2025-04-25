using SweetManagerWebService.Communication.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Entities.Assignments;
using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.IAM.Domain.Repositories.Roles;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.IAM.Infrastructure.Persistence.EFC.Repositories.Roles;

public class WorkerAreaRepository(SweetManagerContext context) : BaseRepository<WorkerArea>(context), IWorkerAreaRepository
{
    public async Task<IEnumerable<WorkerArea>> FindAllAsync(int hotelId)
        => await Task.Run(() => (
            from wa in Context.Set<WorkerArea>().ToList()
            join aw in Context.Set<AssignmentWorker>().ToList()
                on wa.Id equals aw.WorkersAreasId
            join ad in Context.Set<Admin>().ToList()
                on aw.AdminsId equals ad.Id
            join no in Context.Set<Notification>().ToList()
                on ad.Id equals no.AdminsId
            join ow in Context.Set<Owner>().ToList()
                on no.OwnersId equals ow.Id
            join ho in Context.Set<Hotel>().ToList()
                on ow.Id equals  ho.OwnersId
            where ho.Id.Equals(hotelId)
            select wa
        ).ToList());

    public async Task<WorkerArea?> FindByNameAsync(string name, int hotelId)
        => await Task.Run(() => (
            from wa in Context.Set<WorkerArea>().ToList()
            join aw in Context.Set<AssignmentWorker>().ToList()
                on wa.Id equals aw.WorkersAreasId
            join ad in Context.Set<Admin>().ToList()
                on wa.Id equals ad.Id
            join nt in Context.Set<Notification>().ToList()
                on ad.Id equals nt.AdminsId
            join ow in Context.Set<Owner>().ToList()
                on nt.OwnersId equals ow.Id
            join ho in Context.Set<Hotel>().ToList()
                on ow.Id equals ho.OwnersId
            where wa.Name.Equals(name) && ho.Id.Equals(hotelId)
            select wa
        ).FirstOrDefault());

    public async Task<int?> FindIdByNameAsync(string name, int hotelId)
        => await Task.Run(() => (
            from wa in Context.Set<WorkerArea>().ToList()
            join aw in Context.Set<AssignmentWorker>().ToList()
                on wa.Id equals aw.WorkersAreasId
            join ad in Context.Set<Admin>().ToList()
                on wa.Id equals ad.Id
            join nt in Context.Set<Notification>().ToList()
                on ad.Id equals nt.AdminsId
            join ow in Context.Set<Owner>().ToList()
                on nt.OwnersId equals ow.Id
            join ho in Context.Set<Hotel>().ToList()
                on ow.Id equals ho.OwnersId
            where wa.Name.Equals(name) && ho.Id.Equals(hotelId)
            select wa.Id
        ).FirstOrDefault());

    public async Task<string?> FindByWorkerIdAsync(int workerId)
    {
        Task<string?> queryAsync = new(() => (
            from wo in Context.Set<Worker>().ToList()
            join ass in Context.Set<AssignmentWorker>().ToList()
                on wo.Id equals ass.WorkersId
            join wor in Context.Set<WorkerArea>().ToList()
                on ass.WorkersAreasId equals wor.Id
            where wo.Id.Equals(workerId) && ass.FinalDate > DateTime.Now
            select wor.Name 
        ).FirstOrDefault());
        
        queryAsync.Start();

        var result = await queryAsync;

        return result;
    }
}