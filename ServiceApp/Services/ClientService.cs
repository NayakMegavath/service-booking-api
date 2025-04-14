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
        private readonly IMapper _mapper;
        public ClientService(IClientRepository repository, IMapper mapper)
        {
            _repository = repository;
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
            var client = _mapper.Map<Client>(dto);
            await _repository.UpdateAsync(client);
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
    }
}
