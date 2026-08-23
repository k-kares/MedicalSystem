using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Models;
using MedicalSystem.Data;

namespace MedicalSystem.Pages.PatientRecordPages;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public PatientRecord PatientRecord { get; set; } = default!;

    public SelectList PatientList { get; set; } = default!;
    public SelectList DiseaseList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var patientRecord = await _context.PatientRecords
            .Include(p => p.Patient)
            .Include(p => p.Disease)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (patientRecord is null)
        {
            return NotFound();
        }

        PatientRecord = patientRecord;

        await LoadListsAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadListsAsync();
            return Page();
        }

        var patientRecordFromDatabase = await _context.PatientRecords
            .FirstOrDefaultAsync(p => p.Id == PatientRecord.Id);

        if (patientRecordFromDatabase is null)
        {
            return NotFound();
        }

        patientRecordFromDatabase.PatientId = PatientRecord.PatientId;
        patientRecordFromDatabase.DiseaseId = PatientRecord.DiseaseId;
        patientRecordFromDatabase.StartDate = PatientRecord.StartDate;
        patientRecordFromDatabase.EndDate = PatientRecord.EndDate;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private async Task LoadListsAsync()
    {
        PatientList = new SelectList(
            (await _context.Patients
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .Select(p => new
                {
                    p.Id,
                    FullName = p.FirstName + " " + p.LastName
                })
                .ToListAsync()),
               "Id",
               "FullName",
               PatientRecord.PatientId);

        DiseaseList = new SelectList(
            await _context.Diseases
                .OrderBy(d => d.Name)
                .ToListAsync(),
            "Id",
            "Name",
            PatientRecord.DiseaseId);
    }
}