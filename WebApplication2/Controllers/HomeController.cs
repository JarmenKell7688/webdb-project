using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using Microsoft.AspNetCore.Authorization;
using WebApplication2.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardViewModel();

            if (User.IsInRole("Admin"))
            {
                viewModel.PendingReservations = await _context.Reservations.CountAsync(r => r.Status == "Pending");
                viewModel.PendingGatePasses = await _context.GatePasses.CountAsync(g => g.Status == "Pending");
                viewModel.PendingLockerRequests = await _context.LockerRequests.CountAsync(l => l.Status == "Pending");
            }

            return View(viewModel);
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
