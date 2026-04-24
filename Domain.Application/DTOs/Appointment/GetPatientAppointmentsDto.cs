using Hospital.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.DTOs.Appointment
{
    public class GetPatientAppointmentsDto
    {
        public string DoctorName { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public string DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int QueueNumber { get; set; }
        public string Status { get; set; }

    }
}
