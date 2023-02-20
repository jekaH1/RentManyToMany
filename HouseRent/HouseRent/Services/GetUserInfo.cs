using HouseRent.Context;
using HouseRent.Models;
using Microsoft.AspNetCore.Identity;

namespace HouseRent.Services
{
    public class GetUserInfo
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public GetUserInfo(AppDbContext appDbContext,IHttpContextAccessor httpContextAccessor,UserManager<AppUser> userManager)
        {
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<AppUser> GetAppUser()
        {
            AppUser user = null;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                return user;
            }
            return null;
        }
    }
}
