using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Models;
using MedicalSystem.Data;

namespace MedicalSystem.Pages.PatientRecordPages;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public PatientRecord PatientRecord { get; set; } = default!;

    public SelectList PatientList { get; set; } = default!;
    public SelectList DiseaseList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        PatientList = new SelectList(
            await _context.Patients
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync(),
            "Id",
            "LastName");

        DiseaseList = new SelectList(
            await _context.Diseases
                .OrderBy(d => d.Name)
                .ToListAsync(),
            "Id",
            "Name");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadListsAsync();
            return Page();
        }

        _context.PatientRecords.Add(PatientRecord);
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