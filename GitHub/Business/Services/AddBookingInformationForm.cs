using System.ComponentModel.DataAnnotations;

namespace Business.Services;

public class AddBookingInformationForm
{
    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public string EventId { get; set; } = null!;
    [Required]
    public string TicketId { get; set; } = null!;
    [Required]
    public int TicketQuantity { get; set; }

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
    //public List<TicketForm> Tickets { get; set; } = new List<TicketForm>();
}
