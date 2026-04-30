using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.DTOs.MedicalRecord
{
    public class UpdateMedicalRecordDto
    {
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
    }
}
