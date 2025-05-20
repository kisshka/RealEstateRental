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
using Excel = Microsoft.Office.Interop.Excel;

namespace Real_Estate_Rental_Practic.Controllers
{
    [Authorize]
    public class OwnersController : Controller
    {
        private readonly RealEstateRentalContext _context;

        public OwnersController(RealEstateRentalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Owners.ToListAsync());
        }

        public IActionResult Export()
        {
            var allOwners = _context.Owners.OrderBy(p => p.Surname).ToList();

            var application = new Excel.Application();
            application.Visible = false;
            application.SheetsInNewWorkbook = allOwners.Count;
            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);

            for (int i = 0; i < allOwners.Count(); i++)
            {
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets.Item[i + 1];
                string FIO = allOwners[i].Surname + " " + allOwners[i].Name + " " + allOwners[i].Patronymic;
                worksheet.Name = FIO;

                worksheet.Cells[1, 1] = "Город";
                worksheet.Cells[1, 2] = "Улица";
                worksheet.Cells[1, 3] = "Дом";
                worksheet.Cells[1, 4] = "Квартира";
                worksheet.Cells[1, 5] = "Количество комнат";
                worksheet.Cells[1, 6] = "Площадь";
                worksheet.Cells[1, 7] = "Минимальный срок аренды";
                worksheet.Cells[1, 8] = "Разрешено с детьми";
                worksheet.Cells[1, 9] = "Разрешено с животными";

                var estateObjects = _context.EstateObjects
                    .Where(p => p.IdOwner == allOwners[i].IdOwner)
                    .OrderBy(p => p.ObjectType)
                    .GroupBy(p => p.ObjectType)
                     .ToList();

                int rowIndex = 2;

                foreach (var group in estateObjects)
                {
                    Excel.Range range = worksheet.Range[worksheet.Cells[rowIndex, 1], worksheet.Cells[rowIndex, 10]];
                    range.Merge();
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;
                    range.Value = group.Key;
                    rowIndex++;

                    foreach (var estateObject in group)
                    {
                        worksheet.Cells[rowIndex, 1] = estateObject.Town;
                        worksheet.Cells[rowIndex, 2] = estateObject.Street;
                        worksheet.Cells[rowIndex, 3] = estateObject.Home;
                        worksheet.Cells[rowIndex, 4] = estateObject.Flat;
                        worksheet.Cells[rowIndex, 5] = estateObject.NumberOfRooms;
                        worksheet.Cells[rowIndex, 6] = estateObject.Square;
                        worksheet.Cells[rowIndex, 7] = estateObject.RentalPeriod;
                        worksheet.Cells[rowIndex, 8] = estateObject.ChildrenAllowed ? "Да" : "Нет";
                        worksheet.Cells[rowIndex, 9] = estateObject.AnimalsAllowed ? "Да" : "Нет";
                        rowIndex++;
                    }
                }
            }

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Owners.xlsx");
        
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
        workbook.SaveAs(filePath);
        workbook.Close();
        application.Quit();

        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Owners.xlsx");
        }

        public IActionResult ExportSuccess() {
            return View("~/Views/Shared/ExportSuccess.cshtml");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("IdOwner,Surname,Name,Patronymic,PhoneNumber,Town,Street,Home,Flat,EmailAddress")] Owner owner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(owner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(owner);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            return View(owner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOwner,Surname,Name,Patronymic,PhoneNumber,Town,Street,Home,Flat,EmailAddress")] Owner owner)
        {
            if (id != owner.IdOwner)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(owner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerExists(owner.IdOwner))
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
            return View(owner);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners
                .FirstOrDefaultAsync(m => m.IdOwner == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner != null)
            {
                _context.Owners.Remove(owner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerExists(int id)
        {
            return _context.Owners.Any(e => e.IdOwner == id);
        }
    }
}
