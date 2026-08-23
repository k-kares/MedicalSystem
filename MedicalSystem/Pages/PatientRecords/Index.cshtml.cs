using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Models;
using MedicalSystem.Data;

namespace MedicalSystem.Pages.PatientRecordPages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<PatientRecord> PatientRecord { get; set; } = default!;

    public async Task OnGetAsync()
    {
        PatientRecord = await _context.PatientRecords
            .Include(p => p.Patient)
            .Include(p => p.Disease)
            .ToListAsync();
    }
}
