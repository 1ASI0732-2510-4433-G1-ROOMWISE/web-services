using Microsoft.EntityFrameworkCore;
using SweetManagerWebService.Profiles.Domain.Model.Entities;
using SweetManagerWebService.Profiles.Domain.Repositories;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SweetManagerWebService.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class HotelRepository : BaseRepository<Hotel>, IHotelRepository
{
    private readonly SweetManagerContext _context;

    public HotelRepository(SweetManagerContext context)  : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Hotel>> GetAllAsync()
    {
        return await _context.Set<Hotel>().ToListAsync();
    }

    public async Task<Hotel?> GetByOwnersIdAsync(int ownerId)
    {
        return await _context.Set<Hotel>().FirstOrDefaultAsync(tr=> tr.OwnersId == ownerId);
    }

    public async Task<bool> UpdateHotelStateAsync(int id, string name, int phone, string email)
    {
        var result = await _context.Set<Hotel>()
            .Where(h => h.Id == id)  
            .ExecuteUpdateAsync(h => h
                    .SetProperty(u => u.Name, name)    
                    .SetProperty(u => u.Phone, phone)  
                    .SetProperty(u => u.Email, email)  
            );

        return result > 0;
    }



}