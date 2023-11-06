using System.ComponentModel.DataAnnotations;

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
        public IEnumerable<Order>? Orders { get; set; }
    }
}