using exhibition_management_backend.Models;

namespace exhibition_management_backend.DTO
{
    public class AddressDTO
    {
        public int Id { get; set; }

        public string Addressline1 { get; set; }

        public string? Addressline2 { get; set; }

        public string? Addressline3 { get; set; }

        public string? Googlemapslink { get; set; }

        
    }
}
