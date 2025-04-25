using SweetManagerWebService.Communication.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.ResourceManagement.Domain.Model.Aggregates;
using SweetManagerWebService.ResourceManagement.Domain.Repositories;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.ResourceManagement.Infrastructure.Persistence.EFC.Repositories
{
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        private readonly SweetManagerContext _context;

        public ReportRepository(SweetManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Report>> FindByTypeReportIdAsync(int HotelId)
        {
            return await Task.Run(() => (
                from rprt in _context.Set<Report>().ToList()
                join wkr in _context.Set<Worker>().ToList()
                    on rprt.WorkersId equals wkr.Id
                join notification in _context.Set<Notification>().ToList()
                    on wkr.Id equals notification.WorkersId
                join ow in _context.Set<Owner>().ToList()
                    on notification.OwnersId equals ow.Id
                join htls in _context.Set<Hotel>().ToList()
                    on ow.Id equals htls.OwnersId
                where  htls.Id == HotelId
                select rprt
            ).ToList());
        }

 
    }
}