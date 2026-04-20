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

        [HttpPost]
        public async Task<IActionResult> CreateDoctorSchedule(CreateDoctorScheduleDto model, string DoctorId)
        {
            try
            {
                await _doctorScheduleService.CreateDoctorSchedule(model, DoctorId);
                return Ok("Doctor schedule created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }
    }
}
