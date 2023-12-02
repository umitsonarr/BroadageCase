using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.Scores.Include(x => x.Status)
                .Include(x => x.Tournament)
                .Include(x => x.Status)
                .Include(x => x.AwayTeamGoal)
                .Include(x => x.AwayTeamGoal.AwayTeam)
                .Include(x => x.AwayTeamGoal.Goal)
                .Include(x => x.HomeTeamGoal)
                .Include(x => x.HomeTeamGoal.HomeTeam)
                .Include(x => x.HomeTeamGoal.Goal)
                .Include(x => x.Stage)
                .Include(x => x.Round)
                .Include(x => x.Minute)
                .ToList();
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}