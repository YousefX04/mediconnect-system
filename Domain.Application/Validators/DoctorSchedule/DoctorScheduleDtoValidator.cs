using FluentValidation;
using Hospital.Application.DTOs.DoctorSchedule;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.Validators.DoctorSchedule
{
    public class DoctorScheduleDtoValidator : AbstractValidator<DoctorScheduleDto>
    {
        private static readonly string[] ValidDays =
        {
            "Sunday", "Monday", "Tuesday", "Wednesday",
            "Thursday", "Friday", "Saturday"
        };

        public DoctorScheduleDtoValidator()
        {
            RuleFor(x => x.DayOfWeek)
                .NotEmpty()
                .Must(d => ValidDays.Contains(d))
                .WithMessage("Invalid day of week");

            RuleFor(x => x.StartTime)
                .NotEmpty()
                .WithMessage("Start time is required");

            RuleFor(x => x.IsAvailable)
                .NotNull();
        }
    }
}
