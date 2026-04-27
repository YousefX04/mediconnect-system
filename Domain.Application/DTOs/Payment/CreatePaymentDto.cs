using Hospital.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.DTOs.Payment
{
    public class CreatePaymentDto
    {
        public string PaymentMethod { get; set; }
    }
}
