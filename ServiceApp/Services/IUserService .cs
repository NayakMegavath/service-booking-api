using ServiceApp.DTOs;

namespace ServiceApp.Services
{
    public interface IUserService
    {
        Task<UserProfileBase> GetUserProfileAsync(int userId, string userType);
    }
}
