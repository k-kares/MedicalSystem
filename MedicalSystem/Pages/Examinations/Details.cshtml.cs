using MedicalSystem.Data;
using MedicalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Pages.ExaminationPages;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Examination Examination { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var examination = await _context.Examinations
            .Include(e => e.Patient)
            .Include(e => e.Doctor)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (examination is null)
        {
            return NotFound();
        }

        Examination = examination;

        return Page();
    }
}