using ServiceApp.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceApp.DTOs
{
    public class BookingHistoryDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string ServiceType { get; set; } = string.Empty;
        public string Status { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? AmountPaid { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public int? ServiceProfessionalId { get; set; }
        public ServiceProfessional? ServiceProfessional { get; set; }
    }
}
