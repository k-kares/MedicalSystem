using System.ComponentModel.DataAnnotations;

namespace MedicalSystem.Models
{
    public class Disease
    {
        public int Id { get; set; }
        [Display(Name = "Naziv bolesti")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Opis bolesti")]
        public string Description { get; set; } = string.Empty;
    }
}
