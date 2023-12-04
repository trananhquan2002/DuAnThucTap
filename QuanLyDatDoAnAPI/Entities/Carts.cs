using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QuanLyDatDoAnAPI.Entities
{
    public class Carts
    {
        [Key]
        public int CartId { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Account? Account { get; set; }
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime? UpdateAt { get; set; }
        [JsonIgnore]
        public IEnumerable<CartItem>? CartItems { get; set; }
    }
}