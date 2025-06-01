using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class BookingsEntity
{
    [Key]
    public string BookingId { get; set; } = Guid.NewGuid().ToString();

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
    public string EventId {  get; set; } = null!;
    [Required]
    public string EventName { get; set; } = null!;
    [Required]
    public string EventCategory { get; set; } = null!;
    [Column(TypeName = "date")]
    public DateTime? EventDate { get; set; } 
    public DateTime? EventTime { get; set; }

    public DateTime BookingCreated { get; set; } = DateTime.Now;

    public ICollection<TicketEntity> Tickets { get; set; } = new List<TicketEntity>();
}