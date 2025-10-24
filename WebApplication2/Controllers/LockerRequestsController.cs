using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class LockerRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LockerRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LockerRequests
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LockerRequests.Include(l => l.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LockerRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lockerRequest = await _context.LockerRequests
                .Include(l => l.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lockerRequest == null)
            {
                return NotFound();
            }

            return View(lockerRequest);
        }

        // GET: LockerRequests/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Email");
            return View();
        }

        // POST: LockerRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,Name,IdNumber,LockerNumber,Semester,ContactNumber,ApplicationDate,Status")] LockerRequest lockerRequest)
        {
            if (ModelState.IsValid)
            {
                lockerRequest.StudentId = 1; // Temporary - we'll link to actual user later
                lockerRequest.Status = "Pending";
                _context.Add(lockerRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Email", lockerRequest.StudentId);
            return View(lockerRequest);
        }

        // GET: LockerRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lockerRequest = await _context.LockerRequests.FindAsync(id);
            if (lockerRequest == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Email", lockerRequest.StudentId);
            return View(lockerRequest);
        }

        // POST: LockerRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,Name,IdNumber,LockerNumber,Semester,ContactNumber,ApplicationDate,Status")] LockerRequest lockerRequest)
        {
            if (id != lockerRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lockerRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LockerRequestExists(lockerRequest.Id))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Email", lockerRequest.StudentId);
            return View(lockerRequest);
        }

        // GET: LockerRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lockerRequest = await _context.LockerRequests
                .Include(l => l.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lockerRequest == null)
            {
                return NotFound();
            }

            return View(lockerRequest);
        }

        // POST: LockerRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lockerRequest = await _context.LockerRequests.FindAsync(id);
            if (lockerRequest != null)
            {
                _context.LockerRequests.Remove(lockerRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LockerRequestExists(int id)
        {
            return _context.LockerRequests.Any(e => e.Id == id);
        }
    }
}
