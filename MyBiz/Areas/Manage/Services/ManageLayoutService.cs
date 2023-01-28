using Microsoft.AspNetCore.Identity;
using MyBiz.Models;

namespace MyBiz.Areas.Manage.Services
{
    public class ManageLayoutService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ManageLayoutService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AppUser> GetAdmin()
        {
            AppUser admin = null;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                admin = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                return admin;
            }
            return null;
        }
    }
}
