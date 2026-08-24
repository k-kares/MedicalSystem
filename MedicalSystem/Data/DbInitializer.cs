using MedicalSystem.Models;

namespace MedicalSystem.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Doctors.Any())
        {
            return;
        }

        var doctors = new Doctor[]
        {
            new Doctor
            {
                FirstName = "Ana",
                LastName = "Kovač",
                Specialization = "Kardiologija"
            },
            new Doctor
            {
                FirstName = "Marko",
                LastName = "Horvat",
                Specialization = "Interna medicina"
            },
            new Doctor
            {
                FirstName = "Ivana",
                LastName = "Novak",
                Specialization = "Neurologija"
            }
        };

        context.Doctors.AddRange(doctors);
        context.SaveChanges();
    }
}