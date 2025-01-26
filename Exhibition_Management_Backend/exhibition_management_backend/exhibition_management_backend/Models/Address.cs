using System;
using System.Collections.Generic;

namespace exhibition_management_backend.Models;

public partial class Address
{
    public int Id { get; set; }

    public string Addressline1 { get; set; } = null!;

    public string? Addressline2 { get; set; }

    public string? Addressline3 { get; set; }

    public string? Googlemapslink { get; set; }

    public virtual ICollection<Exhibition> Exhibitions { get; set; } = new List<Exhibition>();
}
