using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.DTOs.MedicalRecord
{
    public class GetMedicalRecordDto
    {
        public Guid MedicalRecordId { get; set; }
        public Guid AppointmentId { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
