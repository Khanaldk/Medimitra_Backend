using System.ComponentModel.DataAnnotations;

namespace MediMitra.Models
{
    public class BookingVaccination
    {
        [Key]
        public int BookingId { get; set; }
        public String UserId { get; set; }=String.Empty;
        public String PatientName { get; set; } = String.Empty; 
        public DateTime DOB { get; set; }
        public String Address { get; set; } = String.Empty;
        public DateTime BookingDate { get; set; }
        public int VaccinationId { get; set; }
        public string Token { get; set; } = string.Empty;
        public BookingStatus Status { get; set; } // Enum: Booked, Served
    }
    public enum BookingStatus
    {
        Booked,
        Served
    }
}
