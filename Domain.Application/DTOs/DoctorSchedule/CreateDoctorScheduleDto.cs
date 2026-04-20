using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.DTOs.DoctorSchedule
{
    public class CreateDoctorScheduleDto
    {
        public List<DoctorScheduleDto> DoctorSchedules { get; set; } = new List<DoctorScheduleDto>();
    }
}
