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
    public class InvestigationCaseStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvestigationCaseStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RiskCaseStatus
        public async Task<IActionResult> Index()
        {
              return _context.InvestigationCaseStatus != null ? 
                          View(await _context.InvestigationCaseStatus.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.RiskCaseStatus'  is null.");
        }

        // GET: RiskCaseStatus/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.InvestigationCaseStatus == null)
            {
                return NotFound();
            }

            var investigationCaseStatus = await _context.InvestigationCaseStatus
                .FirstOrDefaultAsync(m => m.InvestigationCaseStatusId == id);
            if (investigationCaseStatus == null)
            {
                return NotFound();
            }

            return View(investigationCaseStatus);
        }

        // GET: RiskCaseStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RiskCaseStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvestigationCaseStatus investigationCaseStatus)
        {
            _context.Add(investigationCaseStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: RiskCaseStatus/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.InvestigationCaseStatus == null)
            {
                return NotFound();
            }

            var investigationCaseStatus = await _context.InvestigationCaseStatus.FindAsync(id);
            if (investigationCaseStatus == null)
            {
                return NotFound();
            }
            return View(investigationCaseStatus);
        }

        // POST: RiskCaseStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, InvestigationCaseStatus investigationCaseStatus)
        {
            if (id != investigationCaseStatus.InvestigationCaseStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(investigationCaseStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvestigationCaseStatusExists(investigationCaseStatus.InvestigationCaseStatusId))
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
            return View(investigationCaseStatus);
        }

        // GET: RiskCaseStatus/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.InvestigationCaseStatus == null)
            {
                return NotFound();
            }

            var investigationCaseStatus = await _context.InvestigationCaseStatus
                .FirstOrDefaultAsync(m => m.InvestigationCaseStatusId == id);
            if (investigationCaseStatus == null)
            {
                return NotFound();
            }

            return View(investigationCaseStatus);
        }

        // POST: RiskCaseStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.InvestigationCaseStatus == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RiskCaseStatus'  is null.");
            }
            var investigationCaseStatus = await _context.InvestigationCaseStatus.FindAsync(id);
            if (investigationCaseStatus != null)
            {
                _context.InvestigationCaseStatus.Remove(investigationCaseStatus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvestigationCaseStatusExists(string id)
        {
          return (_context.InvestigationCaseStatus?.Any(e => e.InvestigationCaseStatusId == id)).GetValueOrDefault();
        }
    }
}
