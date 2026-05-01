using Hospital.Application.DTOs.Receptionist;

namespace Hospital.Application.Services.Interfaces
{
    public interface IReceptionistService
    {
        Task CreateReceptionist(CreateReceptionistDto model);
    }
}
