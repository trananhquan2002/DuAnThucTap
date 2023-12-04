using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QuanLyDatDoAnAPI.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int? PaymentId { get; set; }
        [JsonIgnore]
        public Payment? Payment { get; set; }
        [JsonIgnore]
        public double? OriginalPrice { get; set; }
        public double? ActualPrice { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int? OrderStatusId { get; set; }
        [JsonIgnore]
        public virtual OrderStatus? Status { get; set; }
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        [ForeignKey("UserId")]
        public Account? Account { get; set; }
        public IEnumerable<OrderDetail>? OrderDetails { get; set; }
    }
}