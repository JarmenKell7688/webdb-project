using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        // Updated constructor - includes both UserManager and Context
        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateFaculty()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFaculty(string email, string password, string firstName, string lastName, string department, string employeeId)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                ModelState.AddModelError("", "Email, password, first name, and last name are required");
                return View();
            }

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                FirstName = firstName,
                LastName = lastName,
                Department = department,
                EmployeeId = (300000 + new Random().Next(1, 100000)).ToString()
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Faculty");
                TempData["Success"] = "Faculty account created successfully!";
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }

        // Approve Reservation
        [HttpPost]
        public async Task<IActionResult> ApproveReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                reservation.Status = "Approved";
                await _context.SaveChangesAsync();
                TempData["Success"] = "Reservation approved!";
            }
            return RedirectToAction("Index", "Reservations");
        }

        // Deny Reservation
        [HttpPost]
        public async Task<IActionResult> DenyReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                reservation.Status = "Denied";
                await _context.SaveChangesAsync();
                TempData["Success"] = "Reservation denied!";
            }
            return RedirectToAction("Index", "Reservations");
        }

        // Approve Gate Pass
        [HttpPost]
        public async Task<IActionResult> ApproveGatePass(int id)
        {
            var gatePass = await _context.GatePasses.FindAsync(id);
            if (gatePass != null)
            {
                gatePass.Status = "Approved";
                await _context.SaveChangesAsync();
                TempData["Success"] = "Gate pass approved!";
            }
            return RedirectToAction("Index", "GatePasses");
        }

        // Deny Gate Pass
        [HttpPost]
        public async Task<IActionResult> DenyGatePass(int id)
        {
            var gatePass = await _context.GatePasses.FindAsync(id);
            if (gatePass != null)
            {
                gatePass.Status = "Denied";
                await _context.SaveChangesAsync();
                TempData["Success"] = "Gate pass denied!";
            }
            return RedirectToAction("Index", "GatePasses");
        }

        // Approve Locker Request
        [HttpPost]
        public async Task<IActionResult> ApproveLockerRequest(int id)
        {
            var locker = await _context.LockerRequests.FindAsync(id);
            if (locker != null)
            {
                locker.Status = "Approved";
                await _context.SaveChangesAsync();
                TempData["Success"] = "Locker request approved!";
            }
            return RedirectToAction("Index", "LockerRequests");
        }

        // Deny Locker Request
        [HttpPost]
        public async Task<IActionResult> DenyLockerRequest(int id)
        {
            var locker = await _context.LockerRequests.FindAsync(id);
            if (locker != null)
            {
                locker.Status = "Denied";
                await _context.SaveChangesAsync();
                TempData["Success"] = "Locker request denied!";
            }
            return RedirectToAction("Index", "LockerRequests");
        }

        public async Task<IActionResult> Faculty()
        {
            var faculty = new List<ApplicationUser>();
            var allUsers = await _userManager.Users.ToListAsync();

            foreach (var user in allUsers)
            {
                if (await _userManager.IsInRoleAsync(user, "Faculty"))
                {
                    faculty.Add(user);
                }
            }

            return View(faculty);
        }
    }
}