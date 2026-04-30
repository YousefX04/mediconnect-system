using Hospital.Application.DTOs.MedicalRecord;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.Services.Interfaces
{
    public interface IMedicalRecordService
    {
        Task CreateMedicalRecord(CreateMedicalRecordDto model);
        Task UpdateMedicalRecord(Guid medicalRecordId, UpdateMedicalRecordDto model);
        Task<GetMedicalRecordDto> GetByAppointmentId(Guid appointmentId);
    }
}
