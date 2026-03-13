using Hospital.Application.DTOs.Doctor;
using Hospital.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors(int pageNumber = 1)
        {
            try
            {
                var doctors = await _doctorService.GetAllDoctors(pageNumber);
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctor(string id)
        {
            try
            {
                var doctor = await _doctorService.GetDoctor(id);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(CreateDoctorDto model)
        {
            try
            {
                await _doctorService.CreateDoctor(model);
                return Ok("Doctor created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(string id, UpdateDoctorDto model)
        {
            try
            {
                await _doctorService.UpdateDoctor(id, model);
                return Ok("Doctor updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(string id)
        {
            try
            {
                await _doctorService.DeleteDoctor(id);
                return Ok("Doctor deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }
    }
}
