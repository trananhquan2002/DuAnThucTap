using System.ComponentModel.DataAnnotations;

namespace QuanLyDatDoAnAPI.Entities
{
    public class Carts
    {
        [Key]
        public int CartId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public Account? Account { get; set; }
        public IEnumerable<CartItem>? CartItems { get; set; }
    }
}