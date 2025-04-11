namespace ServiceApp.Services
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(string userId, string email, string role);
    }
}
