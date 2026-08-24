using MedicalSystem.Data;
using MedicalSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Pages.ExaminationPages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Examination> Examination { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Examination = await _context.Examinations
            .Include(e => e.Patient)
            .Include(e => e.Doctor)
            .OrderBy(e => e.ScheduledAt)
            .ToListAsync();
    }
}