using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ServiceApp.DTOs;
using ServiceApp.Helpers;
using ServiceApp.Model;
using ServiceApp.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace ServiceApp.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        private readonly IServiceProfessionalRepository _serviceProfessionalRepository;
        private readonly IMapper _mapper;
        public ClientService(IClientRepository repository, IServiceProfessionalRepository serviceProfessionalRepository, IMapper mapper)
        {
            _repository = repository;
            _serviceProfessionalRepository = serviceProfessionalRepository;
            _mapper = mapper;
        }


        public async Task<List<ClientDto>> GetAllAsync()
        {
            var clients = await _repository.GetAllAsync();
            return _mapper.Map<List<ClientDto>>(clients);
        }

        public async Task<ClientDto?> GetByIdAsync(int id)
        {
            var client = await _repository.GetByIdAsync(id);
            return _mapper.Map<ClientDto>(client);
        }

        public async Task AddAsync(ClientDto dto)
        {
            var client = _mapper.Map<Client>(dto);
            await _repository.AddAsync(client);
        }

        public async Task UpdateAsync(ClientDto dto)
        {
            var existingClient = await this.GetClientByIdAsync(dto.Id);
            if (existingClient != null)
            {
                existingClient.MiddleName = dto.MiddleName;
                existingClient.Email = dto.Email;
                existingClient.Address1 = dto.Address1;
                existingClient.Address2 = dto.Address2;
                existingClient.City = dto.City;
                existingClient.Zip = dto.Zip;
                await _repository.UpdateAsync(existingClient);
            }
        }

        public async Task DirectUpdateAsync(Client dto)
        {
          await _repository.UpdateAsync(dto);
        }

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task RegisterClientAsync(ClientRegistrationDto dto)
        {
            var client = _mapper.Map<Client>(dto);
            client.PasswordHash = PasswordHelper.HashPassword(dto.Password);
            await _repository.RegisterClientAsync(client);
        }

        public async Task<ClientDto?> LoginAsync(UserLoginDto user)
        {
            var client = await _repository.GetByEmailAsync(user.UserName);
            if (client == null)
                return null;

            if (client == null) return null;

            // Use your custom hash check mechanism
            var hashedInput = PasswordHelper.HashPassword(user.Password);
            if (client.PasswordHash != hashedInput)
                return null;
            return _mapper.Map<ClientDto>(user);
        }

        public async Task<Client?> GetByEmailAsync(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }

        public async Task<Client?> GetByPhoneAsync(string phoneNumber)
        {
            return await _repository.GetByPhoneAsync(phoneNumber);
        }

        public async Task<List<BookingHistoryDto>?> GetBookingHistoryByIdAsync(int id)
        {
            var client = await this.GetByIdAsync(id);
            if (client != null)
            {
                var history = await _repository.GetBookingHistoryByIdAsync(id);

                var result = new List<BookingHistoryDto>();

                foreach (var booking in history)
                {
                    var dto = _mapper.Map<BookingHistoryDto>(booking);

                    // Get ServiceProfessional and map to DTO
                    var serviceProfessional = await _serviceProfessionalRepository.GetByIdAsync(booking.ServiceProfessionalId);
                    if (serviceProfessional != null)
                    {
                        dto.ServiceProfessional = _mapper.Map<ServiceProfessionalDto>(serviceProfessional);
                    }

                    result.Add(dto);
                }

                return result;
            }

            return null;
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
           return await _repository.GetByIdAsync(id);
        }
    }
}
