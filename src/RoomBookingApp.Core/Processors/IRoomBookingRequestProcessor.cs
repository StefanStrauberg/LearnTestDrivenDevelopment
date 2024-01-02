using RoomBookingApp.Core.Models;

namespace RoomBookingApp.Core.Processors;

public interface IRoomBookingRequestProcessor
{
    Task<RoomBookingResult> BookRoom(RoomBookingRequest? bookingRequest);
}