using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class EstateObjectsController : Controller
    {
        private readonly RealEstateRentalContext _context;

        public EstateObjectsController(RealEstateRentalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var realEstateRentalContext = _context.EstateObjects.Include(e => e.IdOwnerNavigation);
            return View(await realEstateRentalContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["IdOwner"] = new SelectList(_context.Owners, "IdOwner", "IdOwner");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("IdEstateObject,ObjectType,Town,Street,Home,Flat,CittyArea,NumberOfRooms,Square,RentalPeriod,IdOwner,ChildrenAllowed,AnimalsAllowed")] EstateObject estateObject)
        {
            if(ModelState.IsValid) 
            {
                _context.Add(estateObject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOwner"] = new SelectList(_context.Owners, "IdOwner", "IdOwner", estateObject.IdOwner);
            return View(estateObject);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estateObject = await _context.EstateObjects.FindAsync(id);
            if (estateObject == null)
            {
                return NotFound();
            }
            ViewData["IdOwner"] = new SelectList(_context.Owners, "IdOwner", "IdOwner", estateObject.IdOwner);
            return View(estateObject);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstateObject,ObjectType,Town,Street,Home,Flat,CittyArea,NumberOfRooms,Square,RentalPeriod,IdOwner,ChildrenAllowed,AnimalsAllowed")] EstateObject estateObject)
        {
            if (id != estateObject.IdEstateObject)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                _context.Update(estateObject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(nameof(EstateObject.Square), "*Неверное значение.");
            ViewData["IdOwner"] = new SelectList(_context.Owners, "IdOwner", "IdOwner", estateObject.IdOwner);
            return View(estateObject);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estateObject = await _context.EstateObjects
                .Include(e => e.IdOwnerNavigation)
                .FirstOrDefaultAsync(m => m.IdEstateObject == id);
            if (estateObject == null)
            {
                return NotFound();
            }

            return View(estateObject);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estateObject = await _context.EstateObjects.FindAsync(id);
            if (estateObject != null)
            {
                _context.EstateObjects.Remove(estateObject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstateObjectExists(int id)
        {
            return _context.EstateObjects.Any(e => e.IdEstateObject == id);
        }
    }
}
