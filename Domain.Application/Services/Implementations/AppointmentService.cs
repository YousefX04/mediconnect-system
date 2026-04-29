using FluentValidation;
using Hospital.Application.DTOs.Appointment;
using Hospital.Application.Services.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
using Hospital.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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

            var lastQueueNumber = await _unitOfWork.Appointments
                .GetLastQueueNumberAsync(model.DoctorId, DateTime.Now);

            var appointment = new Appointment()
            {
                DoctorId = model.DoctorId,
                PatientId = model.PatientId,
                AppointmentDate = DateOnly.FromDateTime(DateTime.Now),
                DayOfWeek = dayOfWeek,
                QueueNumber = lastQueueNumber + 1,
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
                    AppointmentId = x.AppointmentId,
                    PatientName = x.Patient.AppUser.FirstName + " " + x.Patient.AppUser.LastName,
                    AppointmentDate = x.AppointmentDate,
                    DayOfWeek = x.DayOfWeek.ToString(),
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    QueueNumber = x.QueueNumber,
                    Status = x.Status.ToString()
                });

            var sortedAppointments = appointments.OrderBy(a => a.AppointmentDate)
                                                .ThenBy(a => a.StartTime)
                                                .ToList();

            return sortedAppointments;
        }

        public async Task<List<GetPatientAppointmentsDto>> GetPatientAppointments(string patientId)
        {
            var appointments = await _unitOfWork.Appointments
                .GetAllAsync(
                filter: x => x.PatientId == patientId,
                selector: x => new GetPatientAppointmentsDto
                {
                    AppointmentId = x.AppointmentId,
                    DoctorName = x.Doctor.AppUser.FirstName + " " + x.Doctor.AppUser.LastName,
                    AppointmentDate = x.AppointmentDate,
                    DayOfWeek = x.DayOfWeek.ToString(),
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    QueueNumber = x.QueueNumber,
                    Status = x.Status.ToString()
                });

            var sortedAppointments = appointments.OrderBy(a => a.AppointmentDate)
                                             .ThenBy(a => a.StartTime)
                                             .ToList();

            return sortedAppointments;
        }

        public async Task CompleteAppointmentStatus(string appointmentId)
        {
            var appointment = await _unitOfWork.Appointments.GetAsync(x => x.AppointmentId.ToString() == appointmentId);

            if (appointment == null)
                throw new Exception("Appointment Not Found");

            if(appointment.Status == Status.Completed)
                throw new Exception("Appointment is already completed");

            appointment.Status = Status.Completed;

            await _unitOfWork.Appointments.Update(appointment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CancelAppointmentStatus(string appointmentId)
        {
            var appointment = await _unitOfWork.Appointments.GetAsync(x => x.AppointmentId.ToString() == appointmentId);

            if (appointment == null)
                throw new Exception("Appointment Not Found");

            if (appointment.Status == Status.Cancelled)
                throw new Exception("Appointment is already cancelled");

            appointment.Status = Status.Cancelled;

            await _unitOfWork.Appointments.Update(appointment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<GetAdminAppointmentsDto>> GetTodayAppointments(int pageNumber)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);  

            var appointments = await _unitOfWork.Appointments.GetAllAsync(
                filter: a => a.AppointmentDate == today,
                selector: a => new GetAdminAppointmentsDto
                {
                    AppointmentId = a.AppointmentId,
                    PatientName = a.Patient.AppUser.FirstName + " " + a.Patient.AppUser.LastName,
                    DoctorName = a.Doctor.AppUser.FirstName + " " + a.Doctor.AppUser.LastName,
                    AppointmentDate = a.AppointmentDate,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    Status = a.Status.ToString()
                },
                pageNumber: pageNumber,
                pageSize: 20
            );

            return appointments;
        }
    }
}
