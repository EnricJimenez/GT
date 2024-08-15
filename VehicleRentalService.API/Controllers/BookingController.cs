using Microsoft.AspNetCore.Mvc;
using VehicleRentalService.Application.Services;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly BookingService _bookingService;

    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost("rent")]
    public IActionResult RentVehicle([FromBody] RentRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _bookingService.RentVehicle(request.VehicleId, request.CustomerId);
            return Ok();
        }
        catch (Exception ex)
        {
            // Devuelve un error 500 con el mensaje de la excepción
            return StatusCode(500, $"Internal server error: {ex.Message} - {ex.StackTrace}");
        }
    }
}

public class RentRequest
{
    [Required]
    public string VehicleId { get; set; } = string.Empty;

    [Required]
    public string CustomerId { get; set; } = string.Empty;
}
