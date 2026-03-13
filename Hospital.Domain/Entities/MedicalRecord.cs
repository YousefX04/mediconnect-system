namespace Hospital.Domain.Entities
{
    public class MedicalRecord
    {
        public Guid MedicalRecordId { get; set; }
        public Patient Patient { get; set; }
        public string PatientId { get; set; }
        public Doctor Doctor { get; set; }
        public string DoctorId { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public DateTime VisitDate { get; set; }
    }
}
