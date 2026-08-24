using MedicalSystem.Data;
using MedicalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Pages.ExaminationPages;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Examination Examination { get; set; } = new Examination();

    public SelectList PatientList { get; set; } = default!;
    public SelectList DoctorList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var examination = await _context.Examinations
            .Include(e => e.Patient)
            .Include(e => e.Doctor)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (examination is null)
        {
            return NotFound();
        }

        Examination = examination;

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

        var examinationFromDatabase = await _context.Examinations
            .FirstOrDefaultAsync(e => e.Id == Examination.Id);

        if (examinationFromDatabase is null)
        {
            return NotFound();
        }

        examinationFromDatabase.PatientId = Examination.PatientId;
        examinationFromDatabase.DoctorId = Examination.DoctorId;
        examinationFromDatabase.Type = Examination.Type;
        examinationFromDatabase.ScheduledAt = Examination.ScheduledAt;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ExaminationExists(Examination.Id))
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
            Examination.PatientId);

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
            Examination.DoctorId);
    }

    private bool ExaminationExists(int id)
    {
        return _context.Examinations.Any(e => e.Id == id);
    }
}