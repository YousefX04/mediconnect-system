using Hospital.Application.DTOs.MedicalRecord;
using Hospital.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        [HttpGet("{appointmentId}")]
        public async Task<IActionResult> GetByAppointmentId(Guid appointmentId)
        {
            try
            {
                var record = await _medicalRecordService.GetByAppointmentId(appointmentId);
                return Ok(record);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedicalRecord(CreateMedicalRecordDto model)
        {
            try
            {
                await _medicalRecordService.CreateMedicalRecord(model);
                return Ok("Medical record created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }

        [HttpPut("{medicalRecordId}")]
        public async Task<IActionResult> UpdateMedicalRecord(Guid medicalRecordId, UpdateMedicalRecordDto model)
        {
            try
            {
                await _medicalRecordService.UpdateMedicalRecord(medicalRecordId, model);
                return Ok("Medical record updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = ex.Message });
            }
        }
    }
}
