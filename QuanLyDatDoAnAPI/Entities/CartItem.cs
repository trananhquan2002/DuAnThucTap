using System.ComponentModel.DataAnnotations;

namespace QuanLyDatDoAnAPI.Entities
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public int? ProductId { get; set; }
        public int? CartId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public Carts? Cart { get; set; }
        public Product? Product { get; set; }
    }
}