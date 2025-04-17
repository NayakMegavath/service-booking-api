namespace ServiceApp.DTOs
{
    public abstract class UserProfileBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ClientId { get; set; }
        public string ServiceProfessionalId { get; set; }
    }

    public class ClientProfile : UserProfileBase
    {
        public string SpecificClientProperty { get; set; }
    }

    public class ServiceProfessionalProfile : UserProfileBase
    {
        public string SpecificProProperty { get; set; }
    }
}
