using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Models;
using MedicalSystem.Data;

namespace MedicalSystem.Pages.PrescriptionPages;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Prescription Prescription { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var prescription = await _context.Prescriptions.FirstOrDefaultAsync(m => m.Id == id);
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
}
