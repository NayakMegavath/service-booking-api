using AutoMapper;
using ServiceApp.DTOs;
using ServiceApp.Helpers;
using ServiceApp.Model;
using ServiceApp.Repositories;

namespace ServiceApp.Services
{
    public class ServiceProfessionalService : IServiceProfessionalService
    {
        private readonly IServiceProfessionalRepository _repository;
        private readonly IMapper _mapper;
        public ServiceProfessionalService(IServiceProfessionalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ServiceProfessionalDto>> GetAllAsync()
        {
            var serviceProfessionals = await _repository.GetAllAsync();
            return _mapper.Map<List<ServiceProfessionalDto>>(serviceProfessionals);
        }

        public async Task<ServiceProfessionalDto?> GetByIdAsync(int id)
        {
            var professional = await _repository.GetByIdAsync(id);
            return _mapper.Map<ServiceProfessionalDto>(professional);
        }

        public async Task AddAsync(ServiceProfessionalDto dto)
        {
            var professional = _mapper.Map<ServiceProfessional>(dto);
            await _repository.AddAsync(professional);
        }

        public async Task UpdateAsync(ServiceProfessionalDto dto)
        {
            var professional = _mapper.Map<ServiceProfessional>(dto);
            await _repository.UpdateAsync(professional);
        }

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task RegisterClientAsync(ServiceProfessionalRegistrationDto dto)
        {
            var professional = _mapper.Map<ServiceProfessional>(dto);
            professional.PasswordHash = PasswordHelper.HashPassword(dto.Password);
            professional.Skills = string.Join(",", dto.Skills);
            await _repository.RegisterServiceProfessionalAsync(professional);
        }

        public async Task<ServiceProfessionalDto?> LoginAsync(UserLoginDto user)
        {
            var loginUser = await _repository.GetByEmailAsync(user.UserName);
            if (user == null) return null;

            var hashedInput = PasswordHelper.HashPassword(user.Password);
            if (user.Password != hashedInput)
                return null;

            return _mapper.Map<ServiceProfessionalDto>(user);
        }

        public async Task<ServiceProfessional?> GetByEmailAsync(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }

        public async Task<ServiceProfessional?> GetByPhoneAsync(string phoneNumber)
        {
            return await _repository.GetByPhoneAsync(phoneNumber);
        }

        public async Task<List<ServiceProfessionalDto>> GetAllByTypeAsync(string type)
        {
            var serviceProfessionals = await _repository.GetAllByTypeAsync(type);
            return _mapper.Map<List<ServiceProfessionalDto>>(serviceProfessionals);
        }

        public async Task<List<BookingHistoryDto>?> GetBookingHistoryByIdAsync(int id)
        {
            var serviceProfessional = await this.GetByIdAsync(id);
            if (serviceProfessional != null)
            {
                var history = await _repository.GetBookingHistoryByIdAsync(id);
                return _mapper.Map<List<BookingHistoryDto>>(history);
            }
            return null;
        }
    }
}
