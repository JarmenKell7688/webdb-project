using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Data;

namespace WebApplication3.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // GET: Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Generate Student ID (format: 200000+)
                var studentId = await GenerateStudentId();

                var student = new Student
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Program = model.Program,
                    Year = model.Year,
                    StudentId = studentId,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(student, model.Password);

                if (result.Succeeded)
                {
                    // Assign Student role
                    await _userManager.AddToRoleAsync(student, "Student");

                    // Sign in the user
                    await _signInManager.SignInAsync(student, isPersistent: false);

                    return RedirectToAction("Index", "Student");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // GET: Login
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Get user and redirect based on role
                    var user = await _userManager.FindByEmailAsync(model.Email);

                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "Faculty"))
                    {
                        return RedirectToAction("Index", "Faculty");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "Student"))
                    {
                        return RedirectToAction("Index", "Student");
                    }

                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        // POST: Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // Helper: Generate Student ID
        private async Task<string> GenerateStudentId()
        {
            var lastStudent = _context.Users
                .OfType<Student>()
                .OrderByDescending(s => s.StudentId)
                .FirstOrDefault();

            if (lastStudent == null)
            {
                return "200001"; // First student
            }

            var lastId = int.Parse(lastStudent.StudentId);
            return (lastId + 1).ToString();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}