using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RiskCaseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public RiskCaseController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: RiskCases
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RiskCase.Include(r => r.RiskCaseStatus).Include(r => r.RiskCaseType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RiskCases/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.RiskCase == null)
            {
                return NotFound();
            }

            var riskCase = await _context.RiskCase
                .Include(r => r.RiskCaseStatus)
                .Include(r => r.RiskCaseType)
                .FirstOrDefaultAsync(m => m.RiskCaseId == id);
            if (riskCase == null)
            {
                return NotFound();
            }

            return View(riskCase);
        }

        // GET: RiskCases/Create
        public IActionResult Create()
        {
            ViewData["RiskCaseStatusId"] = new SelectList(_context.RiskCaseStatus, "RiskCaseStatusId", "Code");
            ViewData["RiskCaseTypeId"] = new SelectList(_context.RiskCaseType, "RiskCaseTypeId", "Code");
            return View();
        }

        // POST: RiskCases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RiskCase riskCase)
        {
            _context.Add(riskCase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: RiskCases/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.RiskCase == null)
            {
                return NotFound();
            }

            var riskCase = await _context.RiskCase.FindAsync(id);
            if (riskCase == null)
            {
                return NotFound();
            }
            ViewData["RiskCaseStatusId"] = new SelectList(_context.RiskCaseStatus, "RiskCaseStatusId", "Name", riskCase.RiskCaseStatusId);
            ViewData["RiskCaseTypeId"] = new SelectList(_context.RiskCaseType, "RiskCaseTypeId", "Name", riskCase.RiskCaseTypeId);
            return View(riskCase);
        }

        // POST: RiskCases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RiskCaseId,Name,Description,RiskCaseTypeId,RiskCaseStatusId,Created")] RiskCase riskCase)
        {
            if (id != riskCase.RiskCaseId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(riskCase);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RiskCaseExists(riskCase.RiskCaseId))
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

        // GET: RiskCases/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.RiskCase == null)
            {
                return NotFound();
            }

            var riskCase = await _context.RiskCase
                .Include(r => r.RiskCaseStatus)
                .Include(r => r.RiskCaseType)
                .FirstOrDefaultAsync(m => m.RiskCaseId == id);
            if (riskCase == null)
            {
                return NotFound();
            }

            return View(riskCase);
        }

        // POST: RiskCases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.RiskCase == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RiskCase'  is null.");
            }
            var riskCase = await _context.RiskCase.FindAsync(id);
            if (riskCase != null)
            {
                _context.RiskCase.Remove(riskCase);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile postedFile)
        {
            if (postedFile != null)
            {
                string path = Path.Combine(webHostEnvironment.WebRootPath, "upload-cases");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                string csvData = await System.IO.File.ReadAllTextAsync(filePath);
                DataTable dt = new DataTable();
                bool firstRow = true;
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            if (firstRow)
                            {
                                foreach (string cell in row.Split(','))
                                {
                                    dt.Columns.Add(cell.Trim());
                                }
                                firstRow = false;
                            }
                            else
                            {
                                dt.Rows.Add();
                                int i = 0;
                                foreach (string cell in row.Split(','))
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = cell.Trim();
                                    i++;
                                }
                            }
                        }
                    }
                }

                return View(new { DataTable = dt });
            }
             return View();
        }

        private bool RiskCaseExists(string id)
        {
            return (_context.RiskCase?.Any(e => e.RiskCaseId == id)).GetValueOrDefault();
        }
    }
}
