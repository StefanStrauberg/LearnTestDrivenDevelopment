using Microsoft.AspNetCore.Mvc;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RoomBookingController(IRoomBookingRequestProcessor roomBookingService) : ControllerBase
{
    private IRoomBookingRequestProcessor _roomBookingProcessor = roomBookingService
        ?? throw new ArgumentNullException(nameof(roomBookingService));

    [HttpPost]
    public async Task<IActionResult> BookRoom(RoomBookingRequest request)
    {
        if (ModelState.IsValid)
        {
            var result = await _roomBookingProcessor.BookRoom(request);
            if (result.Flag == BookingResultFlag.Success)
                return Ok(result);
                
            ModelState.AddModelError(nameof(RoomBookingRequest.Date), "No rooms available for given date.");
        }
        return BadRequest(ModelState);
    }
}
