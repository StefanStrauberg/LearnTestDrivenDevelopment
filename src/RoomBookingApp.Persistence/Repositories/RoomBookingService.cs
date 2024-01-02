using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Domain;

namespace RoomBookingApp.Persistence.Repositories
{
    public class RoomBookingService(RoomBookingAppDbContext context) : IRoomBookingService
    {
        readonly RoomBookingAppDbContext _context = context
            ?? throw new ArgumentNullException(nameof(context));


        public IEnumerable<Room> GetAvailableRooms(DateTime date)
            => _context.Rooms
                       .Where(q => !q.RoomBookings.Any(x => x.Date == date))
                       .ToList();

        public void Save(RoomBooking roomBooking)
        {
            _context.RoomBookings.Add(roomBooking);
            _context.SaveChanges();
        }

    }
}
