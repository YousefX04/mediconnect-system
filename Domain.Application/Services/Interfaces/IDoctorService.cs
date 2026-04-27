using Hospital.Application.DTOs.Doctor;
using Microsoft.AspNetCore.Http;

namespace Hospital.Application.Services.Interfaces
{
    public interface IDoctorService
    {
        Task CreateDoctor(CreateDoctorDto model);
        Task UpdateDoctor(string id, UpdateDoctorDto model);
        Task DeleteDoctor(string id);
        Task<List<GetAllDoctorsDto>> GetAllDoctors(string? specializationName = null, int pageNumber = 1);
        Task<GetDoctorDto> GetDoctor(string doctorId,string patientid);
        Task<string> UploadProfilePictureAsync(string doctorId, IFormFile file);
    }
}
