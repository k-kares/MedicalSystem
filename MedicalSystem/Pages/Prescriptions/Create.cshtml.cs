using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Models;
using MedicalSystem.Data;

namespace MedicalSystem.Pages.PrescriptionPages;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Prescription Prescription { get; set; } = new Prescription();

    public SelectList PatientList { get; set; } = default!;
    public SelectList DoctorList { get; set; } = default!;
    public SelectList MedicationList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
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

        _context.Prescriptions.Add(Prescription);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private async Task LoadListsAsync()
    {
        PatientList = new SelectList(
            await _context.Patients
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .Select(p => new
                {
                    p.Id,
                    FullName = p.FirstName + " " + p.LastName
                })
                .ToListAsync(),
            "Id",
            "FullName",
            Prescription.PatientId);

        DoctorList = new SelectList(
            await _context.Doctors
                .OrderBy(d => d.LastName)
                .ThenBy(d => d.FirstName)
                .Select(d => new
                {
                    d.Id,
                    FullName = d.FirstName + " " + d.LastName
                })
                .ToListAsync(),
            "Id",
            "FullName",
            Prescription.DoctorId);

        MedicationList = new SelectList(
            await _context.Medications
                .OrderBy(m => m.Name)
                .ToListAsync(),
            "Id",
            "Name",
            Prescription.MedicationId);
    }
}