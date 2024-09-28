using MediMitra.Data;
using MediMitra.DTO;
using MediMitra.Models;
using MediMitra.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MediMitra.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingVaccinationController : ControllerBase
    {
        private readonly BookingVaccinationServices _bookingVaccinationServices;
        public BookingVaccinationController(ApplicationDbContext dbContext, IConfiguration configuration, BookingVaccinationServices bookingVaccinationServices)
        {
            _bookingVaccinationServices = bookingVaccinationServices;
        }

      
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateBooking(AddBookingVaccinationDTO booking)
        {
            if (booking == null)
            {
                return BadRequest("Invalid booking data.");
            }
            String userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            String Email = User.FindFirstValue(ClaimTypes.Email);
            var addedBooking = await _bookingVaccinationServices.CreateVaccinationBooking(booking,userId,Email);
            if (addedBooking.Status)
            {
                return StatusCode(StatusCodes.Status201Created, addedBooking);
            }
            return StatusCode(StatusCodes.Status400BadRequest, addedBooking);
        }


    }
}

