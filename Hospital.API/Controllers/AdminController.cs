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

        public AdminController(IAdminService adminService, IAppointmentService appointmentService)
        {
            _adminService = adminService;
            _appointmentService = appointmentService;
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
    }
}
