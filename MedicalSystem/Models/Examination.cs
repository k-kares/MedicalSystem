namespace MedicalSystem.Models
{
    public class Examination
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public Patient? Patient { get; set; }

        public int DoctorId { get; set; }

        public Doctor? Doctor { get; set; }

        public ExaminationType Type { get; set; }

        public DateTime ScheduledAt { get; set; }
    }
}