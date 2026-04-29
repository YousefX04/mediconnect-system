using Hospital.Application.DTOs.Admin;
using Hospital.Application.Services.Interfaces;
using Hospital.Domain.Enums;
using Hospital.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AdminDashboardDto> GetDashboard()
        {
            var totalDoctors = await _unitOfWork.Doctors.CountAsync();
            var totalPatients = await _unitOfWork.Patients.CountAsync();
            var totalAppointments = await _unitOfWork.Appointments.CountAsync();
            var totalCompletedAppointments = await _unitOfWork.Appointments.CountAsync(a => a.Status == Status.Completed);
            var totalCancelledAppointments = await _unitOfWork.Appointments.CountAsync(a => a.Status == Status.Cancelled);
            var totalPendingAppointments = await _unitOfWork.Appointments.CountAsync(a => a.Status == Status.Pending);

            var totalRevenue = await _unitOfWork.Appointments
                .SumAsync(
                filter: a => a.Status == Status.Completed,
                selector: a => a.Doctor.ConsultationFee
                );

            var dashoard = new AdminDashboardDto
            {
                TotalDoctors = totalDoctors,
                TotalPatients = totalPatients,
                TotalAppointments = totalAppointments,
                totalCompletedAppointments = totalCompletedAppointments,
                totalCancelledAppointments = totalCancelledAppointments,
                totalpendingAppointments = totalPendingAppointments,
                TotalRevenue = totalRevenue
            };

            return dashoard;
        }
    }
}
