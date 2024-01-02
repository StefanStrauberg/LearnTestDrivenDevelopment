using RoomBookingApp.Domain;

namespace RoomBookingApp.Core.DataServices;

public interface IRoomBookingService
{
    Task<IEnumerable<Room>> GetAvailableRooms(DateTime date);
    Task SaveBooking(RoomBooking roomBooking);
}