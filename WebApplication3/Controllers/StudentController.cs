using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var student = user as Student;

            ViewBag.StudentId = student?.StudentId;
            ViewBag.FullName = $"{student?.FirstName} {student?.LastName}";
            ViewBag.Program = student?.Program;
            ViewBag.Year = student?.Year;

            return View();
        }
    }
}