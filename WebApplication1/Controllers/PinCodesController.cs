using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PinCodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PinCodesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PinCodes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PinCode.Include(p => p.Country).Include(p => p.State);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PinCodes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.PinCode == null)
            {
                return NotFound();
            }

            var pinCode = await _context.PinCode
                .Include(p => p.Country)
                .Include(p => p.State)
                .FirstOrDefaultAsync(m => m.PinCodeId == id);
            if (pinCode == null)
            {
                return NotFound();
            }

            return View(pinCode);
        }

        // GET: PinCodes/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "Name");
            return View();
        }

        // POST: PinCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PinCode pinCode)
        {
                _context.Add(pinCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: PinCodes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.PinCode == null)
            {
                return NotFound();
            }

            var pinCode = await _context.PinCode.FindAsync(id);
            if (pinCode == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "Name", pinCode.CountryId);
            ViewData["StateId"] = new SelectList(_context.State.Where(s => s.CountryId == pinCode.CountryId ), "StateId", "Name", pinCode?.StateId);
            return View(pinCode);
        }

        // POST: PinCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, PinCode pinCode)
        {
            if (id != pinCode.PinCodeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pinCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PinCodeExists(pinCode.PinCodeId))
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
            ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "Name", pinCode.CountryId);
            ViewData["StateId"] = new SelectList(_context.State.Where(s => s.CountryId == pinCode.CountryId ), "StateId", "Name", pinCode?.StateId);
            return View(pinCode);
        }

        // GET: PinCodes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PinCode == null)
            {
                return NotFound();
            }

            var pinCode = await _context.PinCode
                .Include(p => p.Country)
                .Include(p => p.State)
                .FirstOrDefaultAsync(m => m.PinCodeId == id);
            if (pinCode == null)
            {
                return NotFound();
            }

            return View(pinCode);
        }

        // POST: PinCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PinCode == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PinCode'  is null.");
            }
            var pinCode = await _context.PinCode.FindAsync(id);
            if (pinCode != null)
            {
                _context.PinCode.Remove(pinCode);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PinCodeExists(string id)
        {
          return (_context.PinCode?.Any(e => e.PinCodeId == id)).GetValueOrDefault();
        }
    }
}
