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
    public class AddressController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddressController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Address
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Address.Include(a => a.Country).Include(a => a.PinCode).Include(a => a.State);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Address/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Address == null)
            {
                return NotFound();
            }

            var address = await _context.Address
                .Include(a => a.Country)
                .Include(a => a.PinCode)
                .Include(a => a.State)
                .FirstOrDefaultAsync(m => m.AddressId == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Address/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "Name");
            return View();
        }

        // POST: Address/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Address address)
        {
            if (address is not null)
            {
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            GetCountryStateEdit(address);
            return View(address);
        }

        // GET: Address/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Address == null)
            {
                return NotFound();
            }

            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            GetCountryStateEdit(address);
            return View(address);
        }

        private void GetCountryStateEdit(Address address)
        {
            ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "Name", address.CountryId);
            ViewData["PinCodeId"] = new SelectList(_context.PinCode, "PinCodeId", "Name", address.PinCodeId);
            ViewData["StateId"] = new SelectList(_context.State, "StateId", "Name", address.StateId);
        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AddressId,Branch,Addressline,City,StateId,CountryId,PinCodeId")] Address address)
        {
            if (id != address.AddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.AddressId))
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
            ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "Name", address.CountryId);
            ViewData["PinCodeId"] = new SelectList(_context.PinCode, "PinCodeId", "Name", address.PinCodeId);
            ViewData["StateId"] = new SelectList(_context.State, "StateId", "Name", address.StateId);
            return View(address);
        }

        // GET: Address/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Address == null)
            {
                return NotFound();
            }

            var address = await _context.Address
                .Include(a => a.Country)
                .Include(a => a.PinCode)
                .Include(a => a.State)
                .FirstOrDefaultAsync(m => m.AddressId == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Address == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Address'  is null.");
            }
            var address = await _context.Address.FindAsync(id);
            if (address != null)
            {
                _context.Address.Remove(address);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(string id)
        {
          return (_context.Address?.Any(e => e.AddressId == id)).GetValueOrDefault();
        }
    }
}
