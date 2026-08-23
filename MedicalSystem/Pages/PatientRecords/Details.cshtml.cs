using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Models;
using MedicalSystem.Data;

namespace MedicalSystem.Pages.PatientRecordPages;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public PatientRecord PatientRecord { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var patientrecord = await _context.PatientRecords
            .Include(p => p.Patient)
            .Include(p => p.Disease)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (patientrecord is null)
        {
            return NotFound();
        }
        else
        {
            PatientRecord = patientrecord;
        }

        return Page();
    }
}
