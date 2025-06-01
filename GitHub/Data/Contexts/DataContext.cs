using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public virtual DbSet<BookingsEntity> Bookings { get; set; }
    public virtual DbSet<TicketEntity> Tickets { get; set; }
}
