using FluentValidation;
using Hospital.Application.DTOs.Authentication;
using Hospital.Application.ExternalServices;
using Hospital.Application.Services.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UserLoginDto> _loginValidator;
        private readonly IValidator<UserRegisterDto> _regitserValidator;
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtService _jwtService;

        public AuthService(IUnitOfWork unitOfWork, IValidator<UserLoginDto> loginValidator, UserManager<AppUser> userManager, JwtService jwtService, IValidator<UserRegisterDto> registerValidator)
        {
            _unitOfWork = unitOfWork;
            _loginValidator = loginValidator;
            _userManager = userManager;
            _jwtService = jwtService;
            _regitserValidator = registerValidator;
        }

        public async Task<AuthDto> Login(UserLoginDto model)
        {
            var result = _loginValidator.Validate(model);

            if (!result.IsValid)
                throw new Exception(result.ToString(","));

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                throw new Exception("Email or password is incorrect!");

            var isAuthenticated = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isAuthenticated)
                throw new Exception("Email or password is incorrect!");

            var userRole = await _userManager.GetRolesAsync(user);

            var token = _jwtService.GenerateToken(
                new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = userRole.First()
                });

            var authDto = new AuthDto
            {
                Token = token
            };

            return authDto;
        }

        public async Task Register(UserRegisterDto model)
        {
            var result = _regitserValidator.Validate(model);

            if (!result.IsValid)
                throw new Exception(result.ToString(","));

            var user = new AppUser
            {
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                EmailConfirmed = true
            };

            var creationResult = await _userManager.CreateAsync(user, model.Password);

            if (!creationResult.Succeeded)
                throw new Exception(string.Join(",", creationResult.Errors.Select(e => e.Description)));

            var roleResult = await _userManager.AddToRoleAsync(user, "Patient");

            if (!roleResult.Succeeded)
                throw new Exception(string.Join(",", roleResult.Errors.Select(e => e.Description)));

            var patient = new Patient()
            {
                UserId = user.Id,
                BloodType = model.BloodType,
                Height = model.Height,
                Weight = model.Weight,
                EmergencyContact = model.EmergencyContact
            };

            await _unitOfWork.Patients.AddAsync(patient);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
