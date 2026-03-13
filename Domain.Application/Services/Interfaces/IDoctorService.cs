using Hospital.Application.DTOs.Doctor;

namespace Hospital.Application.Services.Interfaces
{
    public interface IDoctorService
    {
        Task CreateDoctor(CreateDoctorDto model);
        Task UpdateDoctor(string id, UpdateDoctorDto model);
        Task DeleteDoctor(string id);
        Task<List<GetAllDoctorsDto>> GetAllDoctors(int pageNumber = 1);
        Task<GetDoctorDto> GetDoctor(string id);
    }
}
