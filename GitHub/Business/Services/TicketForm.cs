using System.ComponentModel.DataAnnotations;

namespace Business.Services;

public class TicketForm
{
    [Required]
    public string TicketCategoryId { get; set; } = null!;

    [Required]
    public string TicketCategory { get; set; } = null!;

    [Required]
    public decimal TicketPrice { get; set; }

    [Required]
    public int TicketQuantity { get; set; }
}
