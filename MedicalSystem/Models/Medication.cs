using System.ComponentModel.DataAnnotations;

namespace MedicalSystem.Models
{
    public class Medication
    {
        public int Id { get; set; }
        [Display(Name = "Naziv lijeka")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Opis lijeka")]
        public string Description { get; set; } = string.Empty;
    }
}
