using RoomBookingApp.Core.Models;

namespace RoomBookingApp.Core.Processors;

public class RoomBookingRequestProcessor
{
    public RoomBookingRequestProcessor()
    {
    }

    public RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
    {
        return bookingRequest is null
            ? throw new ArgumentNullException(nameof(bookingRequest))
            : new RoomBookingResult
        {
            FullName = bookingRequest.FullName,
            Email = bookingRequest.Email,
            Date = bookingRequest.Date,
        };
    }
}