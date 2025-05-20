using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Real_Estate_Rental_Practic.Models;
using Real_Estate_Rental_Practic.Models.Data;

namespace Real_Estate_Rental_Practic.Controllers
{
    [Authorize]
    public class EstateRentalsController : Controller
    {
        private readonly RealEstateRentalContext _context;

        public EstateRentalsController(RealEstateRentalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var realEstateRentalContext = _context.EstateRentals.Include(e => e.IdEstateObjectNavigation).Include(e => e.IdRealtorNavigation).Include(e => e.IdUserNavigation);
            return View(await realEstateRentalContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["IdEstateObject"] = new SelectList(_context.EstateObjects, "IdEstateObject", "IdEstateObject");
            ViewData["IdRealtor"] = new SelectList(_context.Realtors, "IdRealtor", "IdRealtor");
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "IdUser");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstateRental,RentBeginning,RentEnding,CostPerMonth,Deposit,IdEstateObject,IdRealtor,IdUser")] EstateRental estateRental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estateRental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstateObject"] = new SelectList(_context.EstateObjects, "IdEstateObject", "IdEstateObject", estateRental.IdEstateObject);
            ViewData["IdRealtor"] = new SelectList(_context.Realtors, "IdRealtor", "IdRealtor", estateRental.IdRealtor);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "IdUser", estateRental.IdUser);
            return View(estateRental);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estateRental = await _context.EstateRentals.FindAsync(id);
            if (estateRental == null)
            {
                return NotFound();
            }
            ViewData["IdEstateObject"] = new SelectList(_context.EstateObjects, "IdEstateObject", "IdEstateObject", estateRental.IdEstateObject);
            ViewData["IdRealtor"] = new SelectList(_context.Realtors, "IdRealtor", "IdRealtor", estateRental.IdRealtor);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "IdUser", estateRental.IdUser);
            return View(estateRental);
        
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstateRental,RentBeginning,RentEnding,CostPerMonth,Deposit,IdEstateObject,IdRealtor,IdUser")] EstateRental estateRental)
        {
            if (id != estateRental.IdEstateRental)
            {
                return NotFound();
            }
            if (estateRental.RentEnding.HasValue && estateRental.RentBeginning > estateRental.RentEnding.Value)
            {
                ModelState.AddModelError(nameof(EstateRental.RentBeginning), "Дата начала аренды должна быть раньше даты окончания.");
            }
            if (ModelState.IsValid)
            {
                _context.Update(estateRental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstateObject"] = new SelectList(_context.EstateObjects, "IdEstateObject", "IdEstateObject", estateRental.IdEstateObject);
            ViewData["IdRealtor"] = new SelectList(_context.Realtors, "IdRealtor", "IdRealtor", estateRental.IdRealtor);
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "IdUser", estateRental.IdUser);
            return View(estateRental);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estateRental = await _context.EstateRentals
                .Include(e => e.IdEstateObjectNavigation)
                .Include(e => e.IdRealtorNavigation)
                .Include(e => e.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdEstateRental == id);
            if (estateRental == null)
            {
                return NotFound();
            }

            return View(estateRental);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estateRental = await _context.EstateRentals.FindAsync(id);
            if (estateRental != null)
            {
                _context.EstateRentals.Remove(estateRental);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstateRentalExists(int id)
        {
            return _context.EstateRentals.Any(e => e.IdEstateRental == id);
        }
    }
}
