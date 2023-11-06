using System.ComponentModel.DataAnnotations;

namespace QuanLyDatDoAnAPI.Entities
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public double? PriceTotal { get; set; }
        public int? Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public Product? Product { get; set; }
        public Order? Order { get; set; }
    }
}