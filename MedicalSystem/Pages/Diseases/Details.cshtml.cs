using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Models;
using MedicalSystem.Data;

namespace MedicalSystem.Pages.DiseasePages;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

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
}
