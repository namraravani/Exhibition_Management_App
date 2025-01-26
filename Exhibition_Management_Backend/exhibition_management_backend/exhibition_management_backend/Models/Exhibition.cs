using System;
using System.Collections.Generic;

namespace exhibition_management_backend.Models;

public class Exhibition
{
    public int Id { get; set; }

    public string Venuename { get; set; } = null!;

    public int? Addressid { get; set; }

    public DateOnly Startdate { get; set; }

    public DateOnly Enddate { get; set; }

    public TimeOnly Starttime { get; set; }

    public TimeOnly Endtime { get; set; }

    public int? Nooftables { get; set; }

    public string? Description { get; set; }

    public List<string>? Venueimages { get; set; }

    public string Bannerimage { get; set; } = null!;

    public string? Layoutimage { get; set; }

    public virtual Address? Address { get; set; }
}
