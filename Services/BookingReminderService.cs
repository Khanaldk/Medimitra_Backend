//using MailKit.Net.Smtp;
//using MediMitra.Data;
//using MediMitra.DTO;
//using MediMitra.Models;
//using Microsoft.EntityFrameworkCore;
//using MimeKit;

//namespace MediMitra.Services
//{
//    public class BookingReminderService : BackgroundService
//    {
//        private readonly IServiceScopeFactory _scopeFactory;
//        private readonly ILogger<BookingReminderService> _logger;
//        private readonly IConfiguration _configuration;

//        public BookingReminderService(IServiceScopeFactory scopeFactory, ILogger<BookingReminderService> logger, IConfiguration configuration)
//        {
//            _scopeFactory = scopeFactory;
//            _logger = logger;
//            _configuration = configuration;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                try
//                {
//                    using (var scope = _scopeFactory.CreateScope())
//                    {
//                        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//                        var currentDate = DateOnly.FromDateTime(DateTime.Now);
                   
//                        var bookings = await dbContext.bookingVaccinations
//                                .Where(b => b.BookingDate == currentDate)
//                                    .Include(b => b.Vaccination)
//                                    .ToListAsync();


//                        Console.WriteLine(bookings.Count);
//                        foreach (var matchingBooking in bookings)
//                        {
//                            var user = await dbContext.registerModels.FirstOrDefaultAsync(u => u.Id.ToString() == matchingBooking.UserId);
//                            if(user != null)
//                            {
//                                var message = new MimeMessage();
//                                message.From.Add(new MailboxAddress("Vaccination Service", _configuration["SmtpSettings:Username"]));
//                                message.To.Add(new MailboxAddress("", user.Email));
//                                message.Subject = "Upcoming Vaccination Reminder";
//                                message.Body = new TextPart("plain")
//                                {
//                                    Text = $@"
//                Dear {user.Username},

//                This is a friendly reminder for your upcoming vaccination appointment:

//                - **Vaccination Name**: {matchingBooking.Vaccination.VaccinationName}
//                - **Type**: {matchingBooking.Vaccination.VaccinationType}
//                - **Dose**: {matchingBooking.Vaccination.VaccinationDose}
//                - **Age Group**: {matchingBooking.Vaccination.AgeGroup}
//                - **Scheduled Date**: {matchingBooking.BookingDate:yyyy-MM-dd}
//                - **Status**: {matchingBooking.Vaccination.Status}

//                Please make sure to attend your scheduled appointment on time.

//                Thank you,
//                Vaccination Service Team
//            "
//                                };

//                                // Send the email
//                                using (var client = new SmtpClient())
//                                {
//                                    await client.ConnectAsync(
//                                        _configuration["SmtpSettings:Server"],
//                                        int.Parse(_configuration["SmtpSettings:Port"]),
//                                        MailKit.Security.SecureSocketOptions.StartTls);

//                                    await client.AuthenticateAsync(
//                                        _configuration["SmtpSettings:Username"],
//                                        _configuration["SmtpSettings:Password"]);

//                                    await client.SendAsync(message);
                                   
//                                    _logger.LogInformation($"Sending notification to UserId {matchingBooking.UserId} for Booking Vaccination {matchingBooking.Vaccination.VaccinationName}");

//                                    await client.DisconnectAsync(true);
//                                }

//                            }
//                        }


//                    }
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogError(ex, "Error occurred in BookingReminderService");
//                }

             
//                await Task.Delay(TimeSpan.FromSeconds(1000), stoppingToken);
//            }
//        }
//    }

//}
