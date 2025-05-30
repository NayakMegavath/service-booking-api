namespace ServiceApp.DTOs
{
    public class AddressDto
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        // Add other fields like Latitude, Longitude if stored
        public string FullAddress { get; set; } // For a formatted display string
    }

}
