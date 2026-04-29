using Hospital.Application.DTOs.DoctorSchedule;
using Hospital.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly IDoctorScheduleService _doctorScheduleService;

        public DoctorScheduleController(IDoctorScheduleService doctorScheduleService)
        {
            _doctorScheduleService = doctorScheduleService;
        }

        [HttpPost("{doctorId}")]
        public async Task<IActionResult> CreateDoctorSchedule(CreateDoctorScheduleDto model, string doctorId)
        {
            try
            {
                await _doctorScheduleService.CreateDoctorSchedule(model, doctorId);
                return Ok("Doctor schedule created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        [HttpPut("{doctorId}")]
        public async Task<IActionResult> UpdateDoctorSchedule(CreateDoctorScheduleDto model, string doctorId)
        {
            try
            {
                await _doctorScheduleService.UpdateDoctorSchedule(model, doctorId);
                return Ok("Doctor schedule updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        [HttpDelete("{doctorId}")]
        public async Task<IActionResult> DeleteDoctorSchedule(string doctorId)
        {
            try
            {
                await _doctorScheduleService.DeleteDoctorSchedule(doctorId);
                return Ok("Doctor schedule deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }
    }
}
