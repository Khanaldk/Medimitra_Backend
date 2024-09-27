using System.ComponentModel.DataAnnotations;

namespace MediMitra.Models
{
    public class Vaccination
    {
        [Key]
        public int VaccinationId { get; set; }
        public String VaccinationName { get; set; } = string.Empty;
        public String VaccinationType { get; set; } = string.Empty;
        public String Location { get; set; } = string.Empty;
        public int VaccinationDose { get; set; } 
        public String AgeGroup { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
