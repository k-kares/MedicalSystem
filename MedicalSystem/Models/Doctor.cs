using System.ComponentModel.DataAnnotations;

namespace MedicalSystem.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        [Display(Name = "Ime")]
        public string FirstName { get; set; } = string.Empty;
        
        [Display(Name = "Prezime")]
        public string LastName { get; set; } = string.Empty;
        [Display(Name = "Specijalizacija")]
        public string Specialization { get; set; } = string.Empty;
    }
}
