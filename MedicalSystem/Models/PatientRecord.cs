namespace MedicalSystem.Models
{
    public class PatientRecord
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public Patient? Patient { get; set; }

        public int DiseaseId { get; set; }

        public Disease? Disease { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
