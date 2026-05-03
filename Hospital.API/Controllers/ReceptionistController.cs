using Hospital.Application.DTOs.Receptionist;
using Hospital.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionistController : ControllerBase
    {
        private readonly IReceptionistService _receptionistService;

        public ReceptionistController(IReceptionistService receptionistService)
        {
            _receptionistService = receptionistService;
        }

        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetReceptionistByDoctorId(string doctorId)
        {
            try
            {
                var receptionist = await _receptionistService.GetReceptionistByDoctorId(doctorId);
                return Ok(receptionist);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateReceptionist(CreateReceptionistDto model)
        {
            try
            {
                await _receptionistService.CreateReceptionist(model);
                return Ok("Receptionist created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }
    }
}
