using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Models;
using MedicalSystem.Data;

namespace MedicalSystem.Pages.PrescriptionPages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Prescription Prescription { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var prescription = await _context.Prescriptions
             .Include(p => p.Patient)
             .Include(p => p.Doctor)
             .Include(p => p.Medication)
             .FirstOrDefaultAsync(m => m.Id == id);
        if (prescription is null)
        {
            return NotFound();
        }
        else
        {
            Prescription = prescription;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var prescription = await _context.Prescriptions
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .Include(p => p.Medication)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (prescription != null)
        {
            Prescription = prescription;
            _context.Prescriptions.Remove(Prescription);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
