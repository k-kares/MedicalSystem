using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Models;
using MedicalSystem.Data;

namespace MedicalSystem.Pages.PatientRecordPages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public PatientRecord PatientRecord { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var patientrecord = await _context.PatientRecords.FirstOrDefaultAsync(m => m.Id == id);
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var patientrecord = await _context.PatientRecords.FindAsync(id);
        if (patientrecord != null)
        {
            PatientRecord = patientrecord;
            _context.PatientRecords.Remove(PatientRecord);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
