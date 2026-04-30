using Hospital.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _petientService;

        public AdminController(IAdminService adminService, IAppointmentService appointmentService, IPatientService petientService)
        {
            _adminService = adminService;
            _appointmentService = appointmentService;
            _petientService = petientService;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var dashboard = await _adminService.GetDashboard();
            return Ok(dashboard);
        }

        [HttpGet("appointments")]
        public async Task<IActionResult> GetAllAppointments(int pageNumber = 1)
        {
            var appointments = await _appointmentService.GetTodayAppointments(pageNumber);
            return Ok(appointments);
        }

        [HttpGet("appointments/specialization/{specializationName}")]
        public async Task<IActionResult> GetAppointmentsBySpecialization(string specializationName, int pageNumber = 1)
        {
            var appointments = await _appointmentService.GetTodayAppointmentsBySpecialization(specializationName, pageNumber);
            return Ok(appointments);
        }

        [HttpGet("appointments/doctor/{doctorId}")]
        public async Task<IActionResult> GetAppointmentsByDoctor(string doctorId, int pageNumber = 1)
        {
            var appointments = await _appointmentService.GetTodayAppointmentsByDoctor(doctorId, pageNumber);
            return Ok(appointments);
        }

        [HttpGet("patients")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _petientService.GetAllPatients();
            return Ok(patients);
        }
    }
}
