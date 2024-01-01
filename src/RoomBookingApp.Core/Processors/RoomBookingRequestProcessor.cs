using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Models;

namespace RoomBookingApp.Core.Processors;

public class RoomBookingRequestProcessor(IRoomBookingService roomBookingService)
{
    readonly IRoomBookingService _roomBookingService = roomBookingService
            ?? throw new ArgumentNullException(nameof(roomBookingService));

    public RoomBookingResult BookRoom(RoomBookingRequest? bookingRequest)
    {
        if (bookingRequest is not null)
        {
            _roomBookingService.Save(CreateRoomBookingObject<RoomBooking>(bookingRequest));
            return CreateRoomBookingObject<RoomBookingResult>(bookingRequest);
        }

        throw new ArgumentNullException(nameof(bookingRequest));
    }

    static TRoomBooking CreateRoomBookingObject<TRoomBooking>(RoomBookingRequest bookingRequest) 
        where TRoomBooking : RoomBookingBase, new()
        => new()
        {
            FullName = bookingRequest.FullName,
            Email = bookingRequest.Email,
            Date = bookingRequest.Date,
        };
}