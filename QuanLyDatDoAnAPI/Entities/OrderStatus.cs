using System.ComponentModel.DataAnnotations;

namespace QuanLyDatDoAnAPI.Entities
{
    public class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }
        public string? StatusName { get; set; }
        public IEnumerable<Order>? Orders { get; set; }
    }
}