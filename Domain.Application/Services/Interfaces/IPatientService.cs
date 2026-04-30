using Hospital.Application.DTOs.Patient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.Services.Interfaces
{
    public interface IPatientService
    {
        Task<List<GetPatientDto>> GetAllPatients();
    }
}
