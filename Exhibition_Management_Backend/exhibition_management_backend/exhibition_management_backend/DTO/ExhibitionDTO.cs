namespace exhibition_management_backend.DTO
{
    public class ExhibitionDTO
    {
        public int Id { get; set; }
        public string Venuename { get; set; } = null!;

        public string Startdate { get; set; }

        public string Enddate { get; set; }

        public TimeSpan Starttime { get; set; }

        public TimeSpan Endtime { get; set; }

        public string Bannerimage { get; set; } = null!;
    }
}
