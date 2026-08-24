using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalSystem.Models;
using MedicalSystem.Data;

namespace MedicalSystem.Pages.PrescriptionPages;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Prescription Prescription { get; set; } = new Prescription();

    public SelectList PatientList { get; set; } = default!;
    public SelectList DoctorList { get; set; } = default!;
    public SelectList MedicationList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var prescription = await _context.Prescriptions
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .Include(p => p.Medication)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (prescription is null)
        {
            return NotFound();
        }

        Prescription = prescription;

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

        var prescriptionFromDatabase = await _context.Prescriptions
            .FirstOrDefaultAsync(p => p.Id == Prescription.Id);

        if (prescriptionFromDatabase is null)
        {
            return NotFound();
        }

        prescriptionFromDatabase.PatientId = Prescription.PatientId;
        prescriptionFromDatabase.DoctorId = Prescription.DoctorId;
        prescriptionFromDatabase.MedicationId = Prescription.MedicationId;
        prescriptionFromDatabase.Dose = Prescription.Dose;
        prescriptionFromDatabase.DoseUnit = Prescription.DoseUnit;
        prescriptionFromDatabase.Frequency = Prescription.Frequency;
        prescriptionFromDatabase.StartDate = Prescription.StartDate;
        prescriptionFromDatabase.EndDate = Prescription.EndDate;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PrescriptionExists(Prescription.Id))
            {
                return NotFound();
            }

            throw;
        }

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

    private bool PrescriptionExists(int id)
    {
        return _context.Prescriptions.Any(e => e.Id == id);
    }
}