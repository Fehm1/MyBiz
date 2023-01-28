using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBiz.DAL;
using MyBiz.Helpers;
using MyBiz.Models;

namespace MyBiz.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            IQueryable<Setting> settings = _context.Setting.AsQueryable();

            var paginatedList = PaginatedList<Setting>.Create(settings, 5, page);
 
            return View(paginatedList);
        }

        public IActionResult Update(int id)
        {
            Setting setting = _context.Setting.FirstOrDefault(x => x.Id == id);
            if (setting == null) return NotFound();

            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Setting setting)
        {
            if (!ModelState.IsValid) return View(setting);

            Setting existSetting = _context.Setting.FirstOrDefault(x => x.Id == setting.Id);
            if (existSetting == null) return NotFound();

            existSetting.Value = setting.Value;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
