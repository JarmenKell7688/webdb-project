using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Authorize(Roles = "Faculty")]
    public class FacultyController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public FacultyController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var faculty = user as Faculty;

            ViewBag.FacultyId = faculty?.FacultyId;
            ViewBag.FullName = $"{faculty?.FirstName} {faculty?.LastName}";
            ViewBag.Department = faculty?.Department;

            return View();
        }
    }
}