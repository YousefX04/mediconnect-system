namespace Hospital.Application.DTOs.Doctor
{
    public class GetAllDoctorsDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public decimal ExperienceYears { get; set; }
    }
}
