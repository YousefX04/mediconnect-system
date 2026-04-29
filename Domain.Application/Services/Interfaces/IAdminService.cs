using Hospital.Application.DTOs.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.Services.Interfaces
{
    public interface IAdminService
    {
        Task<AdminDashboardDto> GetDashboard();
    }
}
