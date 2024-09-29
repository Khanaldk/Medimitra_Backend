using MediMitra.Filters;
using System.ComponentModel.DataAnnotations;

namespace MediMitra.DTO
{
    public class AddBookingVaccinationDTO
    {
        [Required(ErrorMessage = "Patient Name is required.")]
        [StringLength(100, ErrorMessage = "Patient Name cannot exceed 100 characters.")]
        [ValidateUsernameValidationAttribute]
        public string PatientName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        public DateOnly DOB { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Booking Date is required.")]
        [DataType(DataType.Date)]
        public DateOnly BookingDate { get; set; }

        [Required(ErrorMessage = "Vaccination ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Vaccination ID must be a positive number.")]
        public int VaccinationId { get; set; }
    }
}
