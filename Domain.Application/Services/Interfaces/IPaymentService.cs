using Hospital.Application.DTOs.Payment;
using Hospital.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.Services.Interfaces
{
    public interface IPaymentService
    {
        Task CreatePayment(Guid appointmentId, CreatePaymentDto model);
        Task<List<PaymentDto>> GetPaymentsByAppointmentId(Guid appointmentId);
    }
}
