using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBiz.DAL;
using MyBiz.Helpers;
using MyBiz.Models;
using System.Data;

namespace MyBiz.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.Positions = _context.Position.Where(x => x.IsDeleted == false);

            IQueryable<Team> teams = _context.Teams.Include(x => x.Position).Where(x => x.IsDeleted == false).AsQueryable();

            var paginatedList = PaginatedList<Team>.Create(teams, 4, page);

            return View(paginatedList);
        }

        public IActionResult DeletedTable(int page = 1)
        {
            ViewBag.Positions = _context.Position.Where(x => x.IsDeleted == false);

            IQueryable<Team> teams = _context.Teams.Include(x => x.Position).Where(x => x.IsDeleted == true).AsQueryable();

            var paginatedList = PaginatedList<Team>.Create(teams, 4, page);

            return View(paginatedList);
        }

        public IActionResult Create()
        {
            ViewBag.Positions = _context.Position.Where(x => x.IsDeleted == false);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Team team)
        {
            ViewBag.Positions = _context.Position.Where(x => x.IsDeleted == false);

            if (!ModelState.IsValid) return View(team);

            if (team.ImageFormFile != null)
            {
                if (team.ImageFormFile.ContentType != "image/jpeg" && team.ImageFormFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFormFile", "Image format must be png or jpeg!");
                    return View(team);
                }

                if (team.ImageFormFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFormFile", "Image size must be less than 2MB!");
                    return View(team);
                }

                team.ImageUrl = FileManager.Save(_env.WebRootPath, "uploads/teams", team.ImageFormFile);
            }
            else
            {
                ModelState.AddModelError("ImageFormFile", "Image is required!");
                return View(team);
            }

            _context.Teams.Add(team);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Update(int id)
        {
            ViewBag.Positions = _context.Position.Where(x => x.IsDeleted == false);

            Team team = _context.Teams.Include(x=>x.Position).FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
            if (team == null) return NotFound();

            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Team team)
        {
            ViewBag.Positions = _context.Position.Where(x => x.IsDeleted == false);

            if (!ModelState.IsValid) return View(team);

            Team existTeam = _context.Teams.Include(x => x.Position).FirstOrDefault(x => x.Id == team.Id && x.IsDeleted == false);
            if (existTeam == null) return NotFound();

            if (team.ImageFormFile != null)
            {
                if (team.ImageFormFile.ContentType != "image/jpeg" && team.ImageFormFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFormFile", "Image format must be png or jpeg!");
                    return View(team);
                }

                if (team.ImageFormFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFormFile", "Image size must be less than 2MB!");
                    return View(team);
                }

                FileManager.Delete(_env.WebRootPath, "uploads/teams", existTeam.ImageUrl);
                existTeam.ImageUrl = FileManager.Save(_env.WebRootPath, "uploads/teams", team.ImageFormFile);

            }

            existTeam.FullName = team.FullName;
            existTeam.PositionId = team.PositionId;
            existTeam.Description = team.Description;
            existTeam.FacebookUrl = team.FacebookUrl;
            existTeam.InstagramUrl = team.InstagramUrl;
            existTeam.TwitterUrl = team.TwitterUrl;
            existTeam.LinkedInUrl = team.LinkedInUrl;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult SoftDelete(int id)
        {
            Team team = _context.Teams.Include(x => x.Position).FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
            if (team == null) return NotFound();

            team.IsDeleted = true;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Restore(int id)
        {
            Team team = _context.Teams.Include(x => x.Position).FirstOrDefault(x => x.Id == id && x.IsDeleted == true);
            if (team == null) return NotFound();

            team.IsDeleted = false;

            _context.SaveChanges();

            return RedirectToAction("deletedtable");
        }

        public IActionResult HardDelete(int id)
        {
            Team team = _context.Teams.Include(x => x.Position).FirstOrDefault(x => x.Id == id && x.IsDeleted == true);
            if (team == null) return NotFound();

            _context.Teams.Remove(team);

            FileManager.Delete(_env.WebRootPath, "uploads/teams", team.ImageUrl);

            _context.SaveChanges();

            return RedirectToAction("deletedtable");
        }
    }
}
