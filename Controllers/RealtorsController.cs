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
    public class RealtorsController : Controller
    {
        private readonly RealEstateRentalContext _context;

        public RealtorsController(RealEstateRentalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var realEstateRentalContext = _context.Realtors.Include(r => r.IdEstateAgencyNavigation);
            return View(await realEstateRentalContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["IdEstateAgency"] = new SelectList(_context.EstateAgencies, "IdEstateAgency", "IdEstateAgency");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("IdRealtor,Surname,Name,Patronymic,PhoneNumber,EmailAddress,IdEstateAgency")] Realtor realtor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(realtor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdEstateAgency"] = new SelectList(_context.EstateAgencies, "IdEstateAgency", "IdEstateAgency", realtor.IdEstateAgency);
            return View(realtor);
        }


        [Authorize(Roles = "Admin,AgencyManager")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        { 
            if (id == null)
            {
                return NotFound();
            }

            var realtor = await _context.Realtors.FindAsync(id);
            if (realtor == null)
            {
                return NotFound();
            }

            ViewData["IdEstateAgency"] = new SelectList(_context.EstateAgencies, "IdEstateAgency", "IdEstateAgency", realtor.IdEstateAgency);
            return View(realtor);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("IdRealtor,Surname,Name,Patronymic,PhoneNumber,EmailAddress,IdEstateAgency")] Realtor realtor)
        {
            if (id != realtor.IdRealtor)
            {
                return NotFound();
            }

            ViewData["IdEstateAgency"] = new SelectList(_context.EstateAgencies, "IdEstateAgency", "IdEstateAgency", realtor.IdEstateAgency);

            if (ModelState.IsValid)
            {

                _context.Update(realtor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(realtor);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realtor = await _context.Realtors
                .Include(r => r.IdEstateAgencyNavigation)
                .FirstOrDefaultAsync(m => m.IdRealtor == id);
            if (realtor == null)
            {
                return NotFound();
            }

            return View(realtor);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var realtor = await _context.Realtors.FindAsync(id);
            if (realtor != null)
            {
                _context.Realtors.Remove(realtor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RealtorExists(int id)
        {
            return _context.Realtors.Any(e => e.IdRealtor == id);
        }
    }
}
