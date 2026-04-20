using Hospital.Application.DTOs.Appointment;
using Hospital.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(CreateAppointmentDto model)
        {
            try
            {
                await _appointmentService.CreateAppointment(model);
                return Ok("Appointment created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetAppointmentsByPatientId(string patientId)
        {
            try
            {
                var appointments = await _appointmentService.GetPatientAppointments(patientId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetAppointmentsByDoctorId(string doctorId)
        {
            try
            {
                var appointments = await _appointmentService.GetDoctorAppointments(doctorId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }
    }
}
