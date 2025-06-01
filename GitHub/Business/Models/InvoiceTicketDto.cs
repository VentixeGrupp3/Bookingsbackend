namespace Business.Models;

public class InvoiceTicketDto
{
    public string TicketCategory { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
