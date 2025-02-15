﻿using exhibition_management_backend.Models;

namespace exhibition_management_backend.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class ExhibitionAddressDTO
    {
        [Required]
        public string Venuename { get; set; } = null!;

        [Required]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string GoogleMapsLink { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Invalid date format (yyyy-MM-dd).")]
        public string Startdate { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Invalid date format (yyyy-MM-dd).")]
        public string Enddate { get; set; }

        [Required]
        public TimeSpan Starttime { get; set; }

        [Required]
        public TimeSpan Endtime { get; set; }

        public int? Nooftables { get; set; }
        public string? Description { get; set; }
        public string[]? Venueimages { get; set; }

        [Required]
        public string Bannerimage { get; set; } = null!;

        public string? Layoutimage { get; set; }
    }

}
