using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.DTOs.Admin
{
    public class AdminDashboardDto
    {
        public int TotalPatients { get; set; }
        public int TotalDoctors { get; set; }
        public int TotalAppointments { get; set; }
        public int totalCompletedAppointments { get; set; }
        public int totalCancelledAppointments { get; set; }
        public int totalpendingAppointments { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
