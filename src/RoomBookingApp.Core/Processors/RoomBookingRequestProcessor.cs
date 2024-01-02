using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Domain;
using RoomBookingApp.Domain.BaseModels;

namespace RoomBookingApp.Core.Processors;

public class RoomBookingRequestProcessor(IRoomBookingService roomBookingService) : IRoomBookingRequestProcessor
{
    readonly IRoomBookingService _roomBookingService = roomBookingService
            ?? throw new ArgumentNullException(nameof(roomBookingService));

    public async Task<RoomBookingResult> BookRoom(RoomBookingRequest? bookingRequest)
    {
        if (bookingRequest is not null)
        {
            IEnumerable<Room> availableRooms = await _roomBookingService.GetAvailableRooms(bookingRequest.Date);
            RoomBookingResult result = CreateRoomBookingObject<RoomBookingResult>(bookingRequest);

            if (availableRooms.Any())
            {
                Room room = availableRooms.First();
                RoomBooking roomBooking = CreateRoomBookingObject<RoomBooking>(bookingRequest);

                roomBooking.Id = room.Id;

                await _roomBookingService.Save(roomBooking);
                
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
