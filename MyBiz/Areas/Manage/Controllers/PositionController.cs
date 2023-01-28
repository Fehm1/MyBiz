using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBiz.DAL;
using MyBiz.Helpers;
using MyBiz.Models;
using System.Data;

namespace MyBiz.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _context;

        public PositionController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            IQueryable<Position> positions = _context.Position.Where(x => x.IsDeleted == false).AsQueryable();

            var paginatedList = PaginatedList<Position>.Create(positions, 4, page);

            return View(paginatedList);
        }

        public IActionResult DeletedTable(int page = 1)
        {
            IQueryable<Position> positions = _context.Position.Where(x => x.IsDeleted == true).AsQueryable();

            var paginatedList = PaginatedList<Position>.Create(positions, 4, page);

            return View(paginatedList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Position position)
        {
            if (!ModelState.IsValid) return View(position);

            _context.Position.Add(position);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Update(int id)
        {
            Position position = _context.Position.FirstOrDefault(x => x.Id == id);
            if (position == null) return NotFound();

            return View(position);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Position position)
        {
            if (!ModelState.IsValid) return View(position);

            Position existPosition = _context.Position.FirstOrDefault(x => x.Id == position.Id);
            if (existPosition == null) return NotFound();

            existPosition.Name = position.Name;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult SoftDelete(int id)
        {
            Position position = _context.Position.FirstOrDefault(x => x.Id == id);
            if (position == null) return NotFound();

            position.IsDeleted = true;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Restore(int id)
        {
            Position position = _context.Position.FirstOrDefault(x => x.Id == id);
            if (position == null) return NotFound();

            position.IsDeleted = false;

            _context.SaveChanges();

            return RedirectToAction("deletedtable");
        }

        public IActionResult HardDelete(int id)
        {
            Position position = _context.Position.FirstOrDefault(x => x.Id == id);
            if (position == null) return NotFound();

            _context.Position.Remove(position);

            _context.SaveChanges();

            return RedirectToAction("deletedtable");
        }
    }
}
