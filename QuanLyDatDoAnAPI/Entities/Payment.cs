using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuanLyDatDoAnAPI.Entities
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public string? PaymentMethod { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        [JsonIgnore]
        public IEnumerable<Order>? Orders { get; set; }
    }
}