using System.ComponentModel.DataAnnotations;

namespace MedicalSystem.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Display(Name = "Ime")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Prezime")]
        public string LastName { get; set; } = string.Empty;

        public string OIB { get; set; } = string.Empty;
        [Display(Name = "Datum Rođenja")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Spol")]
        public string Gender { get; set; } = string.Empty;
        [Display(Name = "Adresa Prebivališta")]
        public string ResidenceAddress { get; set; } = string.Empty;
        [Display(Name = "Adresa Stalnog Boravišta")]
        public string PermanentAddress { get; set; } = string.Empty;
    }
}
