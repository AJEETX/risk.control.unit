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
    public class ClientCompanyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientCompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClientCompanies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClientCompany.Include(c => c.Country).Include(c => c.PinCode).Include(c => c.State);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClientCompanies/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ClientCompany == null)
            {
                return NotFound();
            }

            var clientCompany = await _context.ClientCompany
                .Include(c => c.Country)
                .Include(c => c.PinCode)
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.ClientCompanyId == id);
            if (clientCompany == null)
            {
                return NotFound();
            }

            return View(clientCompany);
        }

        // GET: ClientCompanies/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "Name");
            return View();
        }

        // POST: ClientCompanies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientCompany clientCompany)
        {
            if (clientCompany is not null)
            {
                _context.Add(clientCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "Name", clientCompany.CountryId);
            ViewData["PinCodeId"] = new SelectList(_context.PinCode, "PinCodeId", "Name", clientCompany.PinCodeId);
            ViewData["StateId"] = new SelectList(_context.State, "StateId", "Name", clientCompany.StateId);
            return View(clientCompany);
        }

        // GET: ClientCompanies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ClientCompany == null)
            {
                return NotFound();
            }

            var clientCompany = await _context.ClientCompany.FindAsync(id);
            if (clientCompany == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "Name", clientCompany.CountryId);
            ViewData["PinCodeId"] = new SelectList(_context.PinCode, "PinCodeId", "Name", clientCompany.PinCodeId);
            ViewData["StateId"] = new SelectList(_context.State, "StateId", "Name", clientCompany.StateId);
            return View(clientCompany);
        }

        // POST: ClientCompanies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ClientCompany clientCompany)
        {
            if (id != clientCompany.ClientCompanyId)
            {
                return NotFound();
            }

            if (clientCompany is not null)
            {
                try
                {
                    _context.Update(clientCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientCompanyExists(clientCompany.ClientCompanyId))
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
            ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "Name", clientCompany.CountryId);
            ViewData["PinCodeId"] = new SelectList(_context.PinCode, "PinCodeId", "Name", clientCompany.PinCodeId);
            ViewData["StateId"] = new SelectList(_context.State, "StateId", "Name", clientCompany.StateId);
            return View(clientCompany);
        }

        // GET: ClientCompanies/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ClientCompany == null)
            {
                return NotFound();
            }

            var clientCompany = await _context.ClientCompany
                .Include(c => c.Country)
                .Include(c => c.PinCode)
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.ClientCompanyId == id);
            if (clientCompany == null)
            {
                return NotFound();
            }

            return View(clientCompany);
        }

        // POST: ClientCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ClientCompany == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ClientCompany'  is null.");
            }
            var clientCompany = await _context.ClientCompany.FindAsync(id);
            if (clientCompany != null)
            {
                _context.ClientCompany.Remove(clientCompany);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientCompanyExists(string id)
        {
          return (_context.ClientCompany?.Any(e => e.ClientCompanyId == id)).GetValueOrDefault();
        }
    }
}
