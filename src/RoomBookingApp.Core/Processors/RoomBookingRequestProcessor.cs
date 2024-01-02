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
            IEnumerable<Room> availableRooms = _roomBookingService.GetAvailableRooms(bookingRequest.Date);
            RoomBookingResult result = CreateRoomBookingObject<RoomBookingResult>(bookingRequest);

            if (availableRooms.Any())
            {
                Room room = availableRooms.First();
                RoomBooking roomBooking = CreateRoomBookingObject<RoomBooking>(bookingRequest);

                roomBooking.Id = room.Id;

                _roomBookingService.Save(roomBooking);
                
                result.RoomBookingId = roomBooking.Id;
                result.Flag = Enums.BookingResultFlag.Success;
            }
            else
                result.Flag = Enums.BookingResultFlag.Failure;
            
            return result;
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