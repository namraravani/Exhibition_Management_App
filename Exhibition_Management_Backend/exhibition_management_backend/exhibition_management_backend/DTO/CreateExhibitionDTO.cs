using exhibition_management_backend.Models;

namespace exhibition_management_backend.DTO
{
    public class CreateExhibitionDTO
    {
       
        public string Venuename { get; set; } = null!;

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string GoogleMapsLink { get; set; }

        public DateTime Startdate { get; set; }

        public DateTime Enddate { get; set; }

        public TimeSpan Starttime { get; set; }

        public TimeSpan Endtime { get; set; }

        public int? Nooftables { get; set; }

        public string? Description { get; set; }

        public string[] Venueimages { get; set; }

        public string Bannerimage { get; set; } = null!;

        public string? Layoutimage { get; set; }

    }
}
