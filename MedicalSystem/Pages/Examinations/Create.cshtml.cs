using MedicalSystem.Data;
using MedicalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.Pages.ExaminationPages;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Examination Examination { get; set; } = new Examination();

    public SelectList PatientList { get; set; } = default!;
    public SelectList DoctorList { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Examination.ScheduledAt = DateTime.Today.AddHours(8);

        await LoadListsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadListsAsync();
            return Page();
        }

        _context.Examinations.Add(Examination);
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
}