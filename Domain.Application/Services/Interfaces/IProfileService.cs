using Hospital.Application.DTOs.Profile;

namespace Hospital.Application.Services.Interfaces
{
    public interface IProfileService
    {
        Task<GetPatientProfileDto> GetPatientProfile(string id);
        Task UpdatePatientProfile(string id, UpdatePatientProfileDto model);
        Task<GetDoctorProfileDto> GetDoctorProfile(string id);
        Task UpdateDoctorProfile(string id, UpdateDoctorProfileDto model);
        Task ChangePassword(string id, ChangePasswordDto model);
    }
}
