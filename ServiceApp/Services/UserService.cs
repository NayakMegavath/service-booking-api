using ServiceApp.DTOs;
using ServiceApp.Repositories;

namespace ServiceApp.Services
{
    public class UserService : IUserService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IServiceProfessionalRepository _serviceProfessionalRepository;

        public UserService(IClientRepository clientRepository, IServiceProfessionalRepository serviceProfessionalRepository)
        {
            _clientRepository = clientRepository;
            _serviceProfessionalRepository = serviceProfessionalRepository;
        }

        public async Task<UserProfileBase> GetUserProfileAsync(int userId, string userType)
        {
            if (userType == "Client")
            {
                var client = await _clientRepository.GetByIdAsync(userId);
                if (client != null)
                {
                    return new ClientProfile
                    {
                        Id = client.Id,
                        FirstName = client.FirstName,
                        LastName = client.LastName,
                        Email = client.Email,
                    };
                }
            }
            else if (userType == "ServiceProfessional")
            {
                var pro = await _serviceProfessionalRepository.GetByIdAsync(userId);
                if (pro != null)
                {
                    return new ServiceProfessionalProfile
                    {
                        Id = pro.Id,
                        FirstName = pro.FirstName,
                        LastName = pro.LastName,
                        Email = pro.Email,
                    };
                }
            }
            return null;
        }
    }
}
