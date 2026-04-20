using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.DTOs.Appointment
{
    public class CreateAppointmentDto
    {
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string DayOfWeek { get; set; }
    }
}
