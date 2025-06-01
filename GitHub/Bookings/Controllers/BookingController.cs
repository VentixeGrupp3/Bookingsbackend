using Bookings.Extensions.Middlewares.Apikeys;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController(IBookingService bookingService) : ControllerBase
    {
        private readonly IBookingService _bookingService = bookingService;


        //[ApiKeyAuthorize("User")]
        [HttpPost("user-create")]
        public async Task<IActionResult> UserCreateBooking([FromForm]AddBookingInformationForm formData) 
        {
            if (!ModelState.IsValid)
                return BadRequest(formData);

            var result = await _bookingService.CreateBookingUserAsync(formData);
            return result ? Ok(result) : BadRequest();
        }


        //[ApiKeyAuthorize("User")]
        [HttpGet("user-bookings/{userId}")]
        public async Task<IActionResult> GetAllBookingsByUserId(string userId)
        {
            var result = await _bookingService.GetAllBookingsByUserIdAsync(userId);
            return result == null ? NotFound() : Ok(result);
        }

        [ApiKeyAuthorize("User")]
        [HttpGet("user-bookingnumber/{bookingNumber}")]
        public async Task<IActionResult> UserGetBookingByBookingNumber(string bookingNumber)
        {
            var result = await _bookingService.GetBookingByBookingNumberAsync(bookingNumber);
            return result == null ? NotFound() : Ok( result);
        }

        //[ApiKeyAuthorize("User")]
        [HttpGet("bookingdetails/{bookingId}")]
        public async Task<IActionResult> GetSpecificUserBooking(string bookingId)
        {
            var result = await _bookingService.GetBookingByBookingIdAsync(bookingId);
            return result == null ? NotFound() : Ok(result);
        }

        [ApiKeyAuthorize("Admin")]
        [HttpPost("admin-create")]
        public async Task<IActionResult> AdminCreateBooking(AddBookingInformationForm formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(formData);

            var result = await _bookingService.CreateBookingAdminAsync(formData);
            return result ? Ok(result) : BadRequest();
        }

        [ApiKeyAuthorize("Admin")]
        [HttpDelete("admin-delete/{bookingId}")]
        public async Task<IActionResult> DeleteBooking(string bookingId)
        {
            var result = await _bookingService.DeleteBookingAsync(bookingId);
            return result ? Ok(result) : BadRequest();
        }

        [ApiKeyAuthorize("Admin")]
        [HttpGet("admin-get-all")]
        public async Task<IActionResult> GetAllBookings()
        {
            var result = await _bookingService.GetAllBookings();
            return result == null ? NotFound() : Ok(result);
        }

        [ApiKeyAuthorize("Admin")]
        [HttpGet("admin-specific-id/{bookingId}")]
        public async Task<IActionResult> GetSpecificBooking(string bookingId)
        {
            var result = await _bookingService.GetBookingByBookingIdAsync(bookingId);
            return result == null ? NotFound() : Ok(result);
        }

        [ApiKeyAuthorize("Admin")]
        [HttpGet("admin-bookingnumber/{bookingNumber}")]
        public async Task<IActionResult> AdminGetBookingByBookingNumber(string bookingNumber)
        {
            var result = await _bookingService.GetBookingByBookingNumberAsync(bookingNumber);
            return result == null ? NotFound() : Ok(result);
        }

        [ApiKeyAuthorize("Admin")]
        [HttpPut("admin-update/{bookingId}")]
        public async Task<IActionResult> UpdateBooking(UpdateBookingInformationForm formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(formData);

            var result = await _bookingService.UpdateProjectAsync(formData);
            return result ? Ok(result) : NotFound();
        }
    }
}
