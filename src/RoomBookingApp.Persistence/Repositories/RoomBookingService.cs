using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Domain;

namespace RoomBookingApp.Persistence.Repositories
{
    public class RoomBookingService(RoomBookingAppDbContext context) : IRoomBookingService
    {
        readonly RoomBookingAppDbContext _context = context
            ?? throw new ArgumentNullException(nameof(context));

        public async Task<IEnumerable<Room>> GetAvailableRooms(DateTime date)
            => await _context.Rooms
                             .Where(q => !q.RoomBookings.Any(x => x.Date == date))
                             .ToListAsync();

        public async Task Save(RoomBooking roomBooking)
        {
            await _context.RoomBookings.AddAsync(roomBooking);
            await _context.SaveChangesAsync();
        }

    }
}
