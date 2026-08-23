using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Models;
using MedicalSystem.Data;

namespace MedicalSystem.Pages.DiseasePages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Disease Disease { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var disease = await _context.Diseases.FirstOrDefaultAsync(m => m.Id == id);
        if (disease is null)
        {
            return NotFound();
        }
        else
        {
            Disease = disease;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var disease = await _context.Diseases.FindAsync(id);
        if (disease != null)
        {
            Disease = disease;
            _context.Diseases.Remove(Disease);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
