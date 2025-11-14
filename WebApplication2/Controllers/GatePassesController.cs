using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class GatePassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GatePassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GatePasses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GatePasses.Include(g => g.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GatePasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gatePass = await _context.GatePasses
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gatePass == null)
            {
                return NotFound();
            }

            return View(gatePass);
        }

        // GET: GatePasses/Create
        public IActionResult Create()
        {
            //ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Email");
            return View();
        }

        // POST: GatePasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,SchoolYear,Name,Address,ApplicationDate,StaffType,Department,CourseAndYear,VehiclePlateNo,RegistrationExpiryDate,VehicleType,Maker,Color,Status")] GatePass gatePass)
        {
            if (ModelState.IsValid)
            {
                gatePass.UserId= User.FindFirstValue(ClaimTypes.NameIdentifier);
                gatePass.Status = "Pending";
                gatePass.CreatedBy = User.Identity.Name;
                _context.Add(gatePass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Email", gatePass.UserId);
            return View(gatePass);
        }

        // GET: GatePasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gatePass = await _context.GatePasses.FindAsync(id);
            if (gatePass == null)
            {
                return NotFound();
            }

            if (id == null || !CanModify(id.Value, gatePass.CreatedBy)) return Forbid();

            //ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Email", gatePass.UserId);
            return View(gatePass);
        }

        // POST: GatePasses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,SchoolYear,Name,Address,ApplicationDate,StaffType,Department,CourseAndYear,VehiclePlateNo,RegistrationExpiryDate,VehicleType,Maker,Color,Status")] GatePass gatePass)
        {
            if (id != gatePass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Get the original record to preserve CreatedBy
                    var existingRequest = await _context.GatePasses.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);

                    // Preserve the original CreatedBy
                    gatePass.CreatedBy = existingRequest.CreatedBy;

                    _context.Update(gatePass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GatePassExists(gatePass.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
           // ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Email", gatePass.UserId);
            return View(gatePass);
        }

        // GET: GatePasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gatePass = await _context.GatePasses
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gatePass == null)
            {
                return NotFound();
            }

            if (id == null || !CanModify(id.Value, gatePass.CreatedBy)) return Forbid();

            return View(gatePass);
        }

        // POST: GatePasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gatePass = await _context.GatePasses.FindAsync(id);
            if (gatePass != null)
            {
                _context.GatePasses.Remove(gatePass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GatePassExists(int id)
        {
            return _context.GatePasses.Any(e => e.Id == id);
        }

        private bool CanModify(int id, string createdBy)
        {
            if (User.IsInRole("Admin")) return true;
            return User.Identity.Name == createdBy;
        }
    }
}
