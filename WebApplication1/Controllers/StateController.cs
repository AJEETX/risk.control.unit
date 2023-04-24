using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StateController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RiskCaseStatus
        public async Task<IActionResult> Index()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Code");
            return _context.States != null ?
                        View(await _context.States.Include(s => s.Country).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.States'  is null.");
        }

        // GET: RiskCaseStatus/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.States == null)
            {
                return NotFound();
            }

            var state = await _context.States.Include(s => s.Country)
                .FirstOrDefaultAsync(m => m.StateId == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string countryId, string stateName, string stateCode)
        {
            _context.Add(new State
            {
                Name = stateName.Trim().ToUpper(),
                CountryId = countryId,
                Code = stateCode.Trim().ToUpper()
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: RiskCaseStatus/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.States == null)
            {
                return NotFound();
            }

            var state = await _context.States.FirstOrDefaultAsync(c => c.StateId == id);
            if (state == null)
            {
                return NotFound();
            }
            return View(state);
        }

        // POST: RiskCaseStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, State state)
        {
            if (id != state.StateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(state);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StateExists(state.StateId))
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
            return View(state);
        }

        // GET: RiskCaseStatus/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.States == null)
            {
                return NotFound();
            }

            var state = await _context.States
                .FirstOrDefaultAsync(m => m.StateId == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // POST: RiskCaseStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.States == null)
            {
                return Problem("Entity set 'ApplicationDbContext.States'  is null.");
            }
            var state = await _context.States.FindAsync(id);
            if (state != null)
            {
                _context.States.Remove(state);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StateExists(string id)
        {
            return (_context.States?.Any(e => e.StateId == id)).GetValueOrDefault();
        }
    }
}
