namespace MediMitra.DTO
{
    public class AddBookingVaccinationDTO
    {
        public String PatientName { get; set; } = String.Empty;
        public DateTime DOB { get; set; }
        public String Address { get; set; } = String.Empty;
        public DateTime BookingDate { get; set; }
        public int VaccinationId { get; set; }
    }
}
