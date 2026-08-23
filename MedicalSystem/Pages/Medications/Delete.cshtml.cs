using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Models;
using MedicalSystem.Data;

namespace MedicalSystem.Pages.MedicationPages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Medication Medication { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var medication = await _context.Medications.FirstOrDefaultAsync(m => m.Id == id);
        if (medication is null)
        {
            return NotFound();
        }
        else
        {
            Medication = medication;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var medication = await _context.Medications.FindAsync(id);
        if (medication != null)
        {
            Medication = medication;
            _context.Medications.Remove(Medication);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
