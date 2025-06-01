using Data.Contexts;
using Data.Entities;

namespace Data.Repositories;

public interface IBookingRepository : IBaseRepository<BookingsEntity>
{

}

public class BookingRepository(DataContext context) : BaseRepository<BookingsEntity>(context), IBookingRepository
{

}
