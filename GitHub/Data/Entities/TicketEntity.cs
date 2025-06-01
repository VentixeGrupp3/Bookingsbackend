using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class TicketEntity
{
    [Key]
    public string TicketId { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string BookingId { get; set; } = null!;
    [Required]
    [ForeignKey(nameof(BookingId))]
    public BookingsEntity Booking { get; set; } = null!;


    [Required]
    public string TicketCategoryId { get; set; } = null!;

    public string TicketCategory { get; set; } = null!;
    [Column(TypeName = "decimal(18,2)")]
    public decimal TicketPrice { get; set; }
    public int TicketQuantity { get; set; }
}
