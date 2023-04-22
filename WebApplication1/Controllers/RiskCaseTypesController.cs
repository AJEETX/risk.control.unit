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
    public class RiskCaseTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RiskCaseTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RiskCaseTypes
        public async Task<IActionResult> Index()
        {
            return _context.RiskCaseType != null ?
                        View(await _context.RiskCaseType.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.RiskCaseType'  is null.");
        }

        // GET: RiskCaseTypes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.RiskCaseType == null)
            {
                return NotFound();
            }

            var riskCaseType = await _context.RiskCaseType
                .FirstOrDefaultAsync(m => m.RiskCaseTypeId == id);
            if (riskCaseType == null)
            {
                return NotFound();
            }

            return View(riskCaseType);
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
        public async Task<IActionResult> Create(RiskCaseType riskCaseType)
        {
            _context.Add(riskCaseType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: RiskCaseTypes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.RiskCaseType == null)
            {
                return NotFound();
            }

            var riskCaseType = await _context.RiskCaseType.FindAsync(id);
            if (riskCaseType == null)
            {
                return NotFound();
            }
            return View(riskCaseType);
        }

        // POST: RiskCaseTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RiskCaseTypeId,Name,Code,Created")] RiskCaseType riskCaseType)
        {
            if (id != riskCaseType.RiskCaseTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(riskCaseType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RiskCaseTypeExists(riskCaseType.RiskCaseTypeId))
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
            return View(riskCaseType);
        }

        // GET: RiskCaseTypes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.RiskCaseType == null)
            {
                return NotFound();
            }

            var riskCaseType = await _context.RiskCaseType
                .FirstOrDefaultAsync(m => m.RiskCaseTypeId == id);
            if (riskCaseType == null)
            {
                return NotFound();
            }

            return View(riskCaseType);
        }

        // POST: RiskCaseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.RiskCaseType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RiskCaseType'  is null.");
            }
            var riskCaseType = await _context.RiskCaseType.FindAsync(id);
            if (riskCaseType != null)
            {
                _context.RiskCaseType.Remove(riskCaseType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RiskCaseTypeExists(string id)
        {
            return (_context.RiskCaseType?.Any(e => e.RiskCaseTypeId == id)).GetValueOrDefault();
        }
    }
}
