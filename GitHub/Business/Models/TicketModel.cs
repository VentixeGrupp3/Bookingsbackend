using Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models;

public class TicketModel
{
    public string TicketId { get; set; } = null!;

    public string BookingId { get; set; } = null!;

    public string TicketCategory { get; set; } = null!;
    public decimal TicketPrice { get; set; }
    public int TicketQuantity { get; set; }
}
