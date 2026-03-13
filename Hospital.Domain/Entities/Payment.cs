using Hospital.Domain.Enums;

namespace Hospital.Domain.Entities
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public Appointment Appointment { get; set; }
        public Guid AppointmentId { get; set; }
        public decimal Amount { get; set; }
        public Status Status { get; set; }
    }
}
