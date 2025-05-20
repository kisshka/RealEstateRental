
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;
using Real_Estate_Rental_Practic.Models;
using Real_Estate_Rental_Practic.Models.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace Real_Estate_Rental_Practic.Controllers
{
   [Authorize]
    public class EstateAgenciesController : Controller
    {
        private readonly RealEstateRentalContext _context;

        public EstateAgenciesController(RealEstateRentalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstateAgencies.ToListAsync());
        }

    public IActionResult Export()
    {
        var allAgencies = _context.EstateAgencies.OrderBy(p => p.AgencyName).ToList();

        var application = new Excel.Application();
        application.Visible = false;
        application.SheetsInNewWorkbook = allAgencies.Count;
        Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);

        for (int i = 0; i < allAgencies.Count(); i++)
        {
            Excel.Worksheet worksheet = (Worksheet)application.Worksheets.Item[i + 1];
            worksheet.Name = allAgencies[i].AgencyName;

            worksheet.Cells[1, 1] = "Фамилия";
            worksheet.Cells[1, 2] = "Имя";
            worksheet.Cells[1, 3] = "Отчество";
            worksheet.Cells[1, 4] = "Номер телефона";
            worksheet.Cells[1, 5] = "E-mail";
            worksheet.Cells[1, 6] = "Количество сделок";

            var realtors = _context.Realtors
                .Where(p => p.IdEstateAgency == allAgencies[i].IdEstateAgency)
                .OrderBy(p => p.Surname)
                .ToList();

            int rowIndex = 2;
            int totalDeals = 0;

            foreach (var realtor in realtors)
            {
                int dealsCount = _context.EstateRentals.Count(e => e.IdRealtor == realtor.IdRealtor);
                totalDeals += dealsCount;

                worksheet.Cells[rowIndex, 1] = realtor.Surname;
                worksheet.Cells[rowIndex, 2] = realtor.Name;
                worksheet.Cells[rowIndex, 3] = realtor.Patronymic;
                worksheet.Cells[rowIndex, 4] = realtor.PhoneNumber.ToString();
                worksheet.Cells[rowIndex, 5] = realtor.EmailAddress;
                worksheet.Cells[rowIndex, 6] = dealsCount;

                rowIndex++;
            }

            Excel.Range range = worksheet.Range[worksheet.Cells[rowIndex, 1], worksheet.Cells[rowIndex, 5]];
            range.Merge();
            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            range.Font.Bold = true;
            range.Value = "Итого сделок:";
            worksheet.Cells[rowIndex, 6] = totalDeals;
        }

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Agencies.xlsx");

        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
            workbook.SaveAs(filePath);
            workbook.Close();
            application.Quit();

            // Отправка файла пользователю
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Agencies.xlsx");

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
        public async Task<IActionResult> Create([Bind("IdEstateAgency,AgencyName,PhoneNumber,DirectorSurname,DirectorName,DirectorPatronymic,Town,Street,Home,Flat,Comission")] EstateAgency estateAgency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estateAgency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estateAgency);
        }
        
        [Authorize(Roles = "Admin,AgencyManager")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estateAgency = await _context.EstateAgencies.FindAsync(id);
            if (estateAgency == null)
            {
                return NotFound();
            }
            return View(estateAgency);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstateAgency,AgencyName,PhoneNumber,DirectorSurname,DirectorName,DirectorPatronymic,Town,Street,Home,Flat,Comission")] EstateAgency estateAgency)
        {
            if (id != estateAgency.IdEstateAgency)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estateAgency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstateAgencyExists(estateAgency.IdEstateAgency))
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
            return View(estateAgency);
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estateAgency = await _context.EstateAgencies
                .FirstOrDefaultAsync(m => m.IdEstateAgency == id);
            if (estateAgency == null)
            {
                return NotFound();
            }

            return View(estateAgency);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estateAgency = await _context.EstateAgencies.FindAsync(id);
            if (estateAgency != null)
            {
                _context.EstateAgencies.Remove(estateAgency);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstateAgencyExists(int id)
        {
            return _context.EstateAgencies.Any(e => e.IdEstateAgency == id);
        }
    }
}
