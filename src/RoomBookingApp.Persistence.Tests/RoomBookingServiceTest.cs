using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Domain;
using RoomBookingApp.Persistence.Repositories;
using Shouldly;

namespace RoomBookingApp.Persistence.Tests;

public class RoomBookingServiceTest
{
    public RoomBookingServiceTest()
    {
        
    }

    [Fact]
    public void Should_Return_Available_Rooms()
    {
        // Arrange
        var date = new DateTime(2021, 06, 09);

        var dbOptions = new DbContextOptionsBuilder<RoomBookingAppDbContext>().UseInMemoryDatabase("AvailableRoonTest")
                                                                              .Options;
        using var context = new RoomBookingAppDbContext(dbOptions);
        
        context.Add(new Room { Id = 1, Name = "Room 1" });
        context.Add(new Room { Id = 2, Name = "Room 2" });
        context.Add(new Room { Id = 3, Name = "Room 3" });

        context.Add(new RoomBooking { RoomId = 1, Date = date });
        context.Add(new RoomBooking { RoomId = 2, Date = date.AddDays(-1) });

        context.SaveChanges();

        var roomBookingService = new RoomBookingService(context);

        // Act
        var availableRooms = roomBookingService.GetAvailableRooms(date);

        // Assert
        availableRooms.Count().ShouldBe(2);
        availableRooms.Any(q => q.Id == 2).ShouldBe(true);
        availableRooms.Any(q => q.Id == 3).ShouldBe(true);
        availableRooms.Any(q => q.Id == 1).ShouldBe(false);
    }

    [Fact]
    public void Should_Save_Room_Booking()
    {
        var dbOptions = new DbContextOptionsBuilder<RoomBookingAppDbContext>().UseInMemoryDatabase("ShouldSaveTest")
                                                                              .Options;
        var roomBooking = new RoomBooking { RoomId = 1, Date = new DateTime(2021, 06, 09) };

        using var context = new RoomBookingAppDbContext(dbOptions);
        var roomBookingService = new RoomBookingService(context);
        roomBookingService.Save(roomBooking);

        var bookings = context.RoomBookings.ToList();
        var booking = bookings.ShouldHaveSingleItem();

        booking.Date.ShouldBe(roomBooking.Date);
        booking.RoomId.ShouldBe(roomBooking.RoomId);
    }
}