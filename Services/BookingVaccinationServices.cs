using MailKit.Net.Smtp;
using MediMitra.Data;
using MediMitra.DTO;
using MediMitra.Models;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Security.Claims;

namespace MediMitra.Services
{
    public class BookingVaccinationServices
    {
        private readonly Queue<int> _bookingQueue;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private const int MaxCapacity = 0; // or whatever default value you want

        public BookingVaccinationServices(ApplicationDbContext context, IConfiguration configuration)
        {
            _bookingQueue = new Queue<int>(MaxCapacity);
            _context = context;
            _configuration = configuration;
        }

        // Enqueue a new booking
        public async Task<Response<BookingVaccination>> CreateVaccinationBooking(AddBookingVaccinationDTO booking,String userId,String Email)
        {
            if (IsQueueFull())
            {
                return new Response<BookingVaccination> { Status = false, Message = "Booking queue is full, unable to add more bookings." };
            }
            var bookingCount = await _context.bookingVaccinations.CountAsync();
          
            var token = GenerateToken(bookingCount + 1);

            var bookVaccination = new BookingVaccination
            {
                PatientName = booking.PatientName,
                DOB=booking.DOB,
                Address=booking.Address,
                BookingDate = booking.BookingDate,
                VaccinationId=booking.VaccinationId,
                Status=BookingStatus.Booked,
                Token= token,
                UserId= userId
            };

            var vaccinationpart=await _context.vaccinations.FirstOrDefaultAsync(v=>v.VaccinationId==booking.VaccinationId);

            if (vaccinationpart==null) 
            {
                return new Response<BookingVaccination> { Status = false, Message = "VaccinationId not Found!" };
            }
            // Add to the queue
            _bookingQueue.Enqueue(bookVaccination.BookingId);
            Console.WriteLine($"Booking ID {bookVaccination.BookingId} enqueued successfully. Current Queue Size: {_bookingQueue.Count}");
            

            // Save to database
            await _context.bookingVaccinations.AddAsync(bookVaccination);
            await _context.SaveChangesAsync();


            //logic for sending email 
            //try
            //{
            //    var message = new MimeMessage();
            //    message.From.Add(new MailboxAddress("Durga Khanal", _configuration["SmtpSettings:Username"]));
            //    message.To.Add(new MailboxAddress("", Email));
            //    message.Subject = "नयाँ खोप सूचना!";
            //    message.Body = new TextPart("plain")
            //    {
            //        Text =$"Dear {bookVaccination.PatientName},\r\n\r\nतपाईंको खोप बुकिङ सफलतापूर्वक पुष्टि भएको छ। बुकिङ विवरणहरू तल छन्:\r\n\r\n- नाम: {bookVaccination.PatientName}\r\n- जन्म मिति: {bookVaccination.DOB}\r\n - खोपको नाम: {vaccinationpart.VaccinationName}\r\n- खोपको प्रकार: {vaccinationpart.VaccinationType}\r\n- खोपको मात्रा: {vaccinationpart.VaccinationDose}\r\n- ठेगाना: {bookVaccination.Address}\r\n- बुकिङ मिति: {bookVaccination.BookingDate}\r\n- टोकन नम्बर: {bookVaccination.Token}\r\n\r\nकृपया सेवा लिन आउँदा यो टोकन साथमा ल्याउनुहोस्। धन्यवाद!\r\n\r\nशुभेच्छा,\r\nमेडिमित्र टिम"
            //    };

            //    using (var client = new SmtpClient())
            //    {
            //        await client.ConnectAsync(_configuration["SmtpSettings:Server"], int.Parse(_configuration["SmtpSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
            //        await client.AuthenticateAsync(_configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Password"]);
            //        await client.SendAsync(message);
            //        await _context.SaveChangesAsync();
            //        await client.DisconnectAsync(true);


            //    }
            //}
            //catch (Exception ex)
            //{
            //    return new Response<BookingVaccination> { Status = false, Message = ex.Message };
            //}

            return new Response<BookingVaccination> { Status = true, Message = $"Vaccination Booked and Token sent successfully!", Data = bookVaccination };
        }
        private string GenerateToken(int bookingId)
        {
            return bookingId.ToString("D4"); 
        }
        // Check if the queue is full
        private bool IsQueueFull()
        {
            return _bookingQueue.Count >= MaxCapacity;
        }
        // Check if the queue is empty
        private bool IsQueueEmpty()
        {
            return _bookingQueue.Count == 0;
        }
    }

}

