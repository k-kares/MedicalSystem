namespace MedicalSystem.Models
{
    public class Prescription
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; } = null!;

        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; } = null!;

        public int MedicationId { get; set; }

        public Medication Medication { get; set; } = null!;

        public decimal Dose { get; set; }

        public string DoseUnit { get; set; } = string.Empty;

        public string Frequency { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
