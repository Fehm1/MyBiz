using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBiz.DAL;
using MyBiz.Models;
using MyBiz.ViewModels;
using System.Diagnostics;

namespace MyBiz.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                teams = _context.Teams.Include(x => x.Position).Where(x => x.IsDeleted == false).ToList(),
                settings = _context.Setting.ToList()
            };

            return View(homeViewModel);
        }
    }
}