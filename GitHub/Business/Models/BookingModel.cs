using Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models;

public class BookingModel
{
    public string BookingId { get; set; } = null!;
    public string BookingNumber { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string BookingFirstName { get; set; } = null!;
    public string BookingLastName { get; set; } = null!;
    public string BookingStreetName { get; set; } = null!;
    public string BookingCity { get; set; } = null!;
    public string BookingPostalCode { get; set; } = null!;
    public string BookingEmail { get; set; } = null!;
    public string BookingPhone { get; set; } = null!;
    public string EventId { get; set; } = null!;
    public string EventName { get; set; } = null!;
    public string EventCategory { get; set; } = null!;
    public DateTime? EventDate { get; set; }
    public DateTime? EventTime { get; set; }

    public ICollection<TicketModel> Tickets { get; set; } = new List<TicketModel>();
}
