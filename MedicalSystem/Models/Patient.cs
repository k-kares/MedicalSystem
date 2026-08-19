namespace MedicalSystem.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string OIB { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string ResidenceAddress { get; set; } = string.Empty;

        public string PermanentAddress { get; set; } = string.Empty;
    }
}
