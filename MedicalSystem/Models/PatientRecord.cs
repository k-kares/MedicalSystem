namespace MedicalSystem.Models
{
    public class PatientRecord
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; } = null!;

        public int DiseaseId { get; set; }

        public Disease Disease { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
