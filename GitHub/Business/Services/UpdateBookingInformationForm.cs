using Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Services;

public class UpdateBookingInformationForm
{
    [Required]
    public string BookingId { get; set; } = null!;

    [Required]
    public string BookingNumber { get; set; } = null!;

    [Required] 
    public string UserId { get; set; } = null!;

    [Required]
    public string BookingFirstName { get; set; } = null!;
    [Required]
    public string BookingLastName { get; set; } = null!;
    [Required]
    public string BookingStreetName { get; set; } = null!;
    [Required]
    public string BookingCity { get; set; } = null!;
    [Required]
    public string BookingPostalCode { get; set; } = null!;
    [Required]
    public string BookingEmail { get; set; } = null!;
    [Required]
    public string BookingPhone { get; set; } = null!;

    [Required]
    public string EventId { get; set; } = null!;
    [Required]
    public string EventName { get; set; } = null!;
    [Required]
    public string EventCategory { get; set; } = null!;
    public DateTime? EventDate { get; set; }
    public DateTime? EventTime { get; set; }

    public ICollection<TicketEntity> Tickets { get; set; } = new List<TicketEntity>();
}
