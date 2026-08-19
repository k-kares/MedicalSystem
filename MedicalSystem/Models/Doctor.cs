namespace MedicalSystem.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Specialization { get; set; } = string.Empty;
    }
}
