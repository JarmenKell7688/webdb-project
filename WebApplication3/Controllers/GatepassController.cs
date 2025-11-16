using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Authorize(Roles = "Student,Faculty")]
    public class GatepassController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GatepassController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: My Requests
        public async Task<IActionResult> MyRequests()
        {
            var userId = _userManager.GetUserId(User);
            var requests = await _context.Gatepasses
                .Where(g => g.UserId == userId)
                .OrderByDescending(g => g.RequestDate)
                .ToListAsync();

            return View(requests);
        }

        // GET: Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new GatepassViewModel
            {
                Name = $"{user.FirstName} {user.LastName}",
                DateOut = DateTime.Now,
                DateIn = DateTime.Now.AddDays(1)
            };

            // Set user-specific fields
            if (user is Student student)
            {
                model.CustomId = student.StudentId;
                model.Course = student.Program;
                model.Year = student.Year;
            }
            else if (user is Faculty faculty)
            {
                model.CustomId = faculty.FacultyId;
                model.Department = faculty.Department;
            }

            return View(model);
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GatepassViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var gatepass = new Gatepass
                {
                    UserId = user.Id,
                    Name = $"{user.FirstName} {user.LastName}",
                    Destination = model.Destination,
                    Purpose = model.Purpose,
                    DateOut = model.DateOut,
                    DateIn = model.DateIn,
                    Status = "Pending"
                };

                // Set user-specific fields
                if (user is Student student)
                {
                    gatepass.CustomId = student.StudentId;
                    gatepass.Course = student.Program;
                    gatepass.Year = student.Year;
                }
                else if (user is Faculty faculty)
                {
                    gatepass.CustomId = faculty.FacultyId;
                    gatepass.Department = faculty.Department;
                }

                _context.Gatepasses.Add(gatepass);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Gatepass request submitted successfully!";
                return RedirectToAction(nameof(MyRequests));
            }

            return View(model);
        }

        // GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var gatepass = await _context.Gatepasses
                .FirstOrDefaultAsync(m => m.Id == id);

            if (gatepass == null) return NotFound();

            // Check if user owns this request
            var userId = _userManager.GetUserId(User);
            if (gatepass.UserId != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return View(gatepass);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var gatepass = await _context.Gatepasses.FindAsync(id);
            if (gatepass == null) return NotFound();

            // Check if user owns this request
            var userId = _userManager.GetUserId(User);
            if (gatepass.UserId != userId)
            {
                return Forbid();
            }

            // Can't edit approved/denied requests
            if (gatepass.Status != "Pending")
            {
                TempData["Error"] = "Cannot edit requests that are already approved or denied.";
                return RedirectToAction(nameof(MyRequests));
            }

            var model = new GatepassViewModel
            {
                Id = gatepass.Id,
                Name = gatepass.Name,
                CustomId = gatepass.CustomId,
                Course = gatepass.Course,
                Year = gatepass.Year,
                Department = gatepass.Department,
                Destination = gatepass.Destination,
                Purpose = gatepass.Purpose,
                DateOut = gatepass.DateOut,
                DateIn = gatepass.DateIn
            };

            return View(model);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GatepassViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var gatepass = await _context.Gatepasses.FindAsync(id);
                    if (gatepass == null) return NotFound();

                    // Update only editable fields
                    gatepass.Destination = model.Destination;
                    gatepass.Purpose = model.Purpose;
                    gatepass.DateOut = model.DateOut;
                    gatepass.DateIn = model.DateIn;

                    _context.Update(gatepass);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Gatepass updated successfully!";
                    return RedirectToAction(nameof(MyRequests));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GatepassExists(model.Id))
                        return NotFound();
                    else
                        throw;
                }
            }
            return View(model);
        }

        // POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var gatepass = await _context.Gatepasses.FindAsync(id);
            if (gatepass == null) return NotFound();

            // Check if user owns this request
            var userId = _userManager.GetUserId(User);
            if (gatepass.UserId != userId)
            {
                return Forbid();
            }

            _context.Gatepasses.Remove(gatepass);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Gatepass deleted successfully!";
            return RedirectToAction(nameof(MyRequests));
        }

        private bool GatepassExists(int id)
        {
            return _context.Gatepasses.Any(e => e.Id == id);
        }
    }
}