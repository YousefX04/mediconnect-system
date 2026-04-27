using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.DTOs.Doctor
{
    public class UploadDoctorImageDto
    {
        public IFormFile File { get; set; }
    }
}
