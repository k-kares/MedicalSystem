using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Models;
using MedicalSystem.Data;

namespace MedicalSystem.Pages.DiseasePages;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
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
        Disease = disease;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Disease).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DiseaseExists(Disease.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool DiseaseExists(int id)
    {
        return _context.Diseases.Any(e => e.Id == id);
    }
}
