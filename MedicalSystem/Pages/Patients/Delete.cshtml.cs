using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Models;
using MedicalSystem.Data;

namespace MedicalSystem.Pages.PatientPages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Patient Patient { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var patient = await _context.Patients.FirstOrDefaultAsync(m => m.Id == id);
        if (patient is null)
        {
            return NotFound();
        }
        else
        {
            Patient = patient;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var patient = await _context.Patients.FindAsync(id);
        if (patient != null)
        {
            Patient = patient;
            _context.Patients.Remove(Patient);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
