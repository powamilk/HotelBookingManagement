using System;
using System.Collections.Generic;

namespace HotelBookingManagement.Models;

public partial class Room
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public int Capacity { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
