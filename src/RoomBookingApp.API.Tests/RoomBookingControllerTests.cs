using Microsoft.AspNetCore.Mvc;
using Moq;
using RoomBookingApp.API.Controllers;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using Shouldly;

namespace RoomBookingApp.API.Tests;

public class RoomBookingControllerTests
{
    readonly Mock<IRoomBookingRequestProcessor> _roomBookingService;
    readonly RoomBookingController _controller;
    readonly RoomBookingRequest _request;
    readonly RoomBookingResult _result;

    public RoomBookingControllerTests()
    {
        _roomBookingService = new Mock<IRoomBookingRequestProcessor>();
        _controller = new RoomBookingController(_roomBookingService.Object);
        _request = new RoomBookingRequest();
        _result = new RoomBookingResult();

        _roomBookingService.Setup(x => x.BookRoom(_request))
                           .ReturnsAsync(_result);
    }

    [Theory]
    [InlineData(1, true, typeof(OkObjectResult))]
    [InlineData(0, false, typeof(BadRequestObjectResult))]
    public async Task Should_Call_Booking_Method_When_Valid(int expectedMethodCalls,
                                                            bool isModelValid,
                                                            Type expectedActionResultType)
    {
        // Arrange
        if (!isModelValid)
            _controller.ModelState.AddModelError("Key", "ErrorMessage");
        
        // Act
        var result = await _controller.BookRoom(_request);

        // Assert
        result.ShouldBeOfType(expectedActionResultType);
        _roomBookingService.Verify(x => x.BookRoom(_request), Times.Exactly(expectedMethodCalls));
    }
}