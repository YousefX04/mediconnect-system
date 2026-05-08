using Hospital.Application.DTOs.Doctor;
using Microsoft.AspNetCore.Http;

namespace Hospital.Application.Services.Interfaces
{
    public interface IDoctorService
    {
        Task CreateDoctor(CreateDoctorDto model);
        Task UpdateDoctor(string id, UpdateDoctorDto model);
        Task DeleteDoctor(string id);
        Task<List<GetAllDoctorsDto>> GetAllDoctors(string? specializationName = null);
        Task<GetDoctorDto> GetDoctor(string doctorId, string patientid);
        Task<GetDoctorDetailsDto> GetDoctor(string doctorId);
        Task<string> UploadProfilePictureAsync(string doctorId, IFormFile file);
        Task<List<GetDoctorNamesDto>> GetDoctorNames();
        Task<List<GetDoctorWorkingTodayDto>> GetDoctorsThatHasWorkToday();
    }
}
