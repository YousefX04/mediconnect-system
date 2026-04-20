using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.DTOs.Doctor
{
    public class GetDoctorScheduleDto
    {
        public string DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsAvailable { get; set; }
    }
}
