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
    public class LineOfBusinessController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LineOfBusinessController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RiskCaseTypes
        public async Task<IActionResult> Index()
        {
            return _context.LineOfBusiness != null ?
                        View(await _context.LineOfBusiness.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.RiskCaseType'  is null.");
        }

        // GET: RiskCaseTypes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.LineOfBusiness == null)
            {
                return NotFound();
            }

            var lineOfBusiness = await _context.LineOfBusiness
                .FirstOrDefaultAsync(m => m.LineOfBusinessId == id);
            if (lineOfBusiness == null)
            {
                return NotFound();
            }

            return View(lineOfBusiness);
        }

        // GET: RiskCaseTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RiskCaseTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LineOfBusiness lineOfBusiness)
        {
            _context.Add(lineOfBusiness);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: RiskCaseTypes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.LineOfBusiness == null)
            {
                return NotFound();
            }

            var lineOfBusiness = await _context.LineOfBusiness.FindAsync(id);
            if (lineOfBusiness == null)
            {
                return NotFound();
            }
            return View(lineOfBusiness);
        }

        // POST: RiskCaseTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, LineOfBusiness lineOfBusiness)
        {
            if (id != lineOfBusiness.LineOfBusinessId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lineOfBusiness);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!lineOfBusinessTypeExists(lineOfBusiness.LineOfBusinessId))
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
            return View(lineOfBusiness);
        }

        // GET: RiskCaseTypes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.LineOfBusiness == null)
            {
                return NotFound();
            }

            var lineOfBusiness = await _context.LineOfBusiness
                .FirstOrDefaultAsync(m => m.LineOfBusinessId == id);
            if (lineOfBusiness == null)
            {
                return NotFound();
            }

            return View(lineOfBusiness);
        }

        // POST: RiskCaseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.LineOfBusiness == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RiskCaseType'  is null.");
            }
            var lineOfBusiness = await _context.LineOfBusiness.FindAsync(id);
            if (lineOfBusiness != null)
            {
                _context.LineOfBusiness.Remove(lineOfBusiness);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool lineOfBusinessTypeExists(string id)
        {
            return (_context.LineOfBusiness?.Any(e => e.LineOfBusinessId == id)).GetValueOrDefault();
        }
    }
}
