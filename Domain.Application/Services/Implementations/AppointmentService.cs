using FluentValidation;
using Hospital.Application.DTOs.Appointment;
using Hospital.Application.Services.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
using Hospital.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateAppointmentDto> _createAppointmentValidator;

        public AppointmentService(IUnitOfWork unitOfWork, IValidator<CreateAppointmentDto> createAppointmentValidator)
        {
            _unitOfWork = unitOfWork;
            _createAppointmentValidator = createAppointmentValidator;
        }

        public async Task CreateAppointment(CreateAppointmentDto model)
        {
            var result = _createAppointmentValidator.Validate(model);

            if (!result.IsValid)
                throw new Exception(result.ToString(","));

            var dayOfWeek = Enum.Parse<DayOfWeek>(model.DayOfWeek);

            var doctorScheduleTime = await _unitOfWork.DoctorSchedules
                .GetAsync(
                selector: x => new { x.StartTime, x.EndTime },
                filter: x => x.DoctorId == model.DoctorId && x.DayOfWeek == dayOfWeek                       
                );

            var appointmentStartTimes = await _unitOfWork.Appointments
                .GetAllAsync(
                selector: x => x.StartTime,
                filter: x => x.DoctorId == model.DoctorId && x.DayOfWeek == dayOfWeek && x.Status != Status.Completed
                );

            var appointment = new Appointment()
            {
                DoctorId = model.DoctorId,
                PatientId = model.PatientId,
                AppointmentDate = DateOnly.FromDateTime(DateTime.Now),
                DayOfWeek = dayOfWeek,
                Status = Status.Pending
            };

            if (appointmentStartTimes == null)
            {
                appointment.StartTime = doctorScheduleTime.StartTime;
                appointment.EndTime = doctorScheduleTime.StartTime.Add(TimeSpan.FromMinutes(30)); // Assuming 30 minutes duration
            }
            else
            {
                var appointmentLastStartTime = appointmentStartTimes.LastOrDefault();
                appointment.StartTime = appointmentLastStartTime.Add(TimeSpan.FromMinutes(30)); // Assuming 30 minutes duration
                appointment.EndTime = appointment.StartTime.Add(TimeSpan.FromMinutes(30)); // Assuming 30 minutes duration
            }

            await _unitOfWork.Appointments.AddAsync(appointment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<GetDoctorAppointmentsDto>> GetDoctorAppointments(string doctorId)
        {
            var appointments = await _unitOfWork.Appointments
                .GetAllAsync(
                filter: x => x.DoctorId == doctorId,
                selector: x => new GetDoctorAppointmentsDto
                {
                    PatientName = x.Patient.AppUser.FirstName + " " + x.Patient.AppUser.LastName,
                    AppointmentDate = x.AppointmentDate,
                    DayOfWeek = x.DayOfWeek.ToString(),
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Status = x.Status.ToString()
                });

            return appointments;
        }

        public async Task<List<GetPatientAppointmentsDto>> GetPatientAppointments(string patientId)
        {
            var appointments = await _unitOfWork.Appointments
                .GetAllAsync(
                filter: x => x.PatientId == patientId,
                selector: x => new GetPatientAppointmentsDto
                {
                    DoctorName = x.Doctor.AppUser.FirstName + " " + x.Doctor.AppUser.LastName,
                    AppointmentDate = x.AppointmentDate,
                    DayOfWeek = x.DayOfWeek.ToString(),
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Status = x.Status.ToString()
                });

            var sortedAppointments = appointments.OrderBy(a => a.AppointmentDate)
                                             .ThenBy(a => a.StartTime)
                                             .ToList();

            return sortedAppointments;
        }
    }
}
