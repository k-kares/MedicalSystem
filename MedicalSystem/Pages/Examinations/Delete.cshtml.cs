using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Models;
using MedicalSystem.Data;

namespace MedicalSystem.Pages.ExaminationPages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Examination Examination { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var examination = await _context.Examinations.FirstOrDefaultAsync(m => m.Id == id);
        if (examination is null)
        {
            return NotFound();
        }
        else
        {
            Examination = examination;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var examination = await _context.Examinations.FindAsync(id);
        if (examination != null)
        {
            Examination = examination;
            _context.Examinations.Remove(Examination);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
