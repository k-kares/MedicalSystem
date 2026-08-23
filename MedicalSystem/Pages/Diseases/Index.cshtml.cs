using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Models;
using MedicalSystem.Data;

namespace MedicalSystem.Pages.DiseasePages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Disease> Disease { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Disease = await _context.Diseases.ToListAsync();
    }
}
