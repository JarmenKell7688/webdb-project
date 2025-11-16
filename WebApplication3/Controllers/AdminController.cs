using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            // Get statistics
            ViewBag.TotalStudents = await _context.Users.OfType<Student>().CountAsync();
            ViewBag.TotalFaculty = await _context.Users.OfType<Faculty>().CountAsync();
            ViewBag.TotalRequests = await _context.Gatepasses.CountAsync();
            ViewBag.PendingRequests = await _context.Gatepasses
                .Where(g => g.Status == "Pending").CountAsync();

            ViewBag.AdminName = $"{user.FirstName} {user.LastName}";

            return View();
        }
    }
}