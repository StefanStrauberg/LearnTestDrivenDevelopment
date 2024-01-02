using Moq;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Domain;
using Shouldly;

namespace RoomBookingApp.Core.Tests;

public class RoomBookingRequestProcessorTest
{
    readonly RoomBookingRequestProcessor _processor;
    readonly RoomBookingRequest _request;
    readonly Mock<IRoomBookingService> _roomBookingServiceMock;
    readonly List<Room> _availableRooms;

    public RoomBookingRequestProcessorTest()
    {
        _request = new RoomBookingRequest
        {
            FullName = "Test Name",
            Email = "test@request.com",
            Date = new DateTime(2021, 10, 20)
        };
        _roomBookingServiceMock = new Mock<IRoomBookingService>();
        _availableRooms = [ new() { Id = 1 } ];
        _roomBookingServiceMock.Setup(q => q.GetAvailableRooms(_request.Date))
            .ReturnsAsync(_availableRooms);
        _processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);
    }

    [Fact]
    public async Task Should_Return_Room_Booking_Response_With_Request_Values()
    {
        // Arrange
        // Act
        var result = await _processor.BookRoom(_request);

        // Assert
        result.ShouldNotBeNull();
        result.FullName.ShouldBe(_request.FullName);
        result.Email.ShouldBe(_request.Email);
        result.Date.ShouldBe(_request.Date);
    }

    [Fact]
    public async Task Should_Throw_Exception_For_Null_Request()
    {
        // Arrange
        // Act

        // Assert
        var exception = await Should.ThrowAsync<ArgumentNullException>(async () => await _processor.BookRoom(null));
        exception.ParamName.ShouldBe("bookingRequest");
    }

    [Fact]
    public async Task Should_Save_Room_Booking_Request()
    {
        // Arrange
        RoomBooking? savedBooking = null;
        _roomBookingServiceMock.Setup(q => q.SaveBooking(It.IsAny<RoomBooking>()))
            .Callback<RoomBooking>(booking => 
            { 
                savedBooking = booking;
            });

        // Act
        await _processor.BookRoom(_request);
        _roomBookingServiceMock.Verify(q => q.SaveBooking(It.IsAny<RoomBooking>()),
                                       Times.Once);

        // Assert
        savedBooking.ShouldNotBeNull();
        savedBooking.FullName.ShouldBe(_request.FullName);
        savedBooking.Email.ShouldBe(_request.Email);
        savedBooking.Date.ShouldBe(_request.Date);
        savedBooking.Id.ShouldBe(_availableRooms.First().Id);
    }

    [Fact]
    public async Task Should_Not_Save_Room_Booking_Request_If_None_Available()
    {
        _availableRooms.Clear();
        await _processor.BookRoom(_request);
        _roomBookingServiceMock.Verify(q => q.SaveBooking(It.IsAny<RoomBooking>()),
                                       Times.Never);
    }

    [Theory]
    [InlineData(BookingResultFlag.Failure, false)]
    [InlineData(BookingResultFlag.Success, true)]
    public async Task Should_Return_SuccessOrFailure_Flag_In_Result(BookingResultFlag bookingResultFlag,
                                                              bool isAvailable)
    {
        if (!isAvailable)
            _availableRooms.Clear();

        var result = await _processor.BookRoom(_request);
        bookingResultFlag.ShouldBe(result.Flag);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(null, false)]
    public async Task Should_Return_RoomBookingId_In_Result(int? roomBookingId,
                                                      bool isAvailable)
    {
        if (!isAvailable)
            _availableRooms.Clear();
        else
        {
            _roomBookingServiceMock.Setup(q => q.SaveBooking(It.IsAny<RoomBooking>()))
                .Callback<RoomBooking>(booking => 
                {
                    booking.Id = roomBookingId!.Value;
                });
        }
        
        var result = await _processor.BookRoom(_request);
        result.RoomBookingId.ShouldBe(roomBookingId);
    }
}
