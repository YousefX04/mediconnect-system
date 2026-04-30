using FluentValidation;
using Hospital.Application.DTOs.MedicalRecord;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Application.Validators.MedicalRecord
{
    public class UpdateMedicalRecordDtoValidator : AbstractValidator<UpdateMedicalRecordDto>
    {
        public UpdateMedicalRecordDtoValidator()
        {
            RuleFor(mr => mr.Prescription)
                .NotEmpty().WithMessage("Prescription is required")
                .MaximumLength(500).WithMessage("Prescription cannot exceed 500 characters");

            RuleFor(mr => mr.Diagnosis)
                .NotEmpty().WithMessage("Diagnosis is required")
                .MaximumLength(500).WithMessage("Diagnosis cannot exceed 500 characters");
        }
    }
}
