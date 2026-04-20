using Hospital.Application.DTOs.DoctorSchedule;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.Services.Interfaces
{
    public interface IDoctorScheduleService
    {
        Task CreateDoctorSchedule(CreateDoctorScheduleDto model, string DoctorId);
    }
}
