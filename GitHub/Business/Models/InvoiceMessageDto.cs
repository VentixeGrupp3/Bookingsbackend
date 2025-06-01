using Business.Models;

namespace Infrastructure.Models;

public class InvoiceMessageDto
{
    public string BookingId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string BookingEmail { get; set; } = default!;
    public string BookingNumber { get; set; } = default!;
    public string? BookingPhone { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string BookingAddress { get; set; } = default!;
    public string EventId { get; set; } = default!;
    public string EventName { get; set; } = default!;
    public string EventOwnerName { get; set; } = default!;
    public string EventOwnerEmail { get; set; } = default!;
    public string EventOwnerAddress { get; set; } = default!;
    public string EventOwnerPhone { get; set; } = default!;
    public List<InvoiceTicketDto> Tickets { get; set; } = new();
}
