using RoomBookingApp.Core.Domain;
using RoomBookingApp.Domain;

namespace RoomBookingApp.Core.DataServices;

public interface IRoomBookingService
{
    IEnumerable<Room> GetAvailableRooms(DateTime date);
    void Save(RoomBooking roomBooking);
}