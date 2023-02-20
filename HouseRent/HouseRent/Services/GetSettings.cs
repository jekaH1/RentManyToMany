using HouseRent.Context;
using HouseRent.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRent.Services
{
    public class GetSettings
    {
        private readonly AppDbContext _appDbContext;

        public GetSettings(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<SettingsRH>> GetSettingsAsnyc()
        {
            return await _appDbContext.SettingsRH.ToListAsync();
        }


    }
}
