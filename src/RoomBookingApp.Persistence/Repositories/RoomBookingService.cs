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
        {
            throw new NotImplementedException();
        }

        public void Save(RoomBooking roomBooking)
        {
            throw new NotImplementedException();
        }

    }
}
