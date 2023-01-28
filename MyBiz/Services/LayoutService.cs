using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBiz.DAL;
using MyBiz.Models;

namespace MyBiz.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public LayoutService(AppDbContext context, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<Setting>> GetSettings()
        {
            return await _context.Setting.ToListAsync();
        }

        public async Task<AppUser> GetUser()
        {
            AppUser admin = null;
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                admin = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
                return admin;
            }
            return null;
        }
    }
}
