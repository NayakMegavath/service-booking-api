namespace ServiceApp.DTOs
{
    public class ClientLoginResponseDto
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public AddressDto RegisteredAddress { get; set; } 
    }
}
