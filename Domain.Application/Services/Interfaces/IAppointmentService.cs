using Hospital.Application.DTOs.Appointment;
using System.Globalization;

namespace Hospital.Application.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<Guid> CreateAppointment(CreateAppointmentDto model);
        Task<List<GetPatientAppointmentsDto>> GetPatientAppointments(string patientId);
        Task<List<GetDoctorAppointmentsDto>> GetDoctorAppointments(string doctorId);
        Task<List<GetAdminAppointmentsDto>> GetTodayAppointments(int pageNumber = 1);
        Task<List<GetAdminAppointmentsDto>> GetTodayAppointmentsBySpecialization(string specializationName, int pageNumber = 1);
        Task<List<GetAdminAppointmentsDto>> GetTodayAppointmentsByDoctor(string doctorId, int pageNumber = 1);
        Task CompleteAppointmentStatus(string appointmentId);
        Task CancelAppointmentStatus(string appointmentId);
        Task<int> ExpectedNumber(string doctorId, DateTime appointmentDate);
    }
}
