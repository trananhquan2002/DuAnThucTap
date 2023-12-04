using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuanLyDatDoAnAPI.Entities
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        [JsonIgnore]
        public int? ProductId { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }
        [JsonIgnore]
        public int? CartId { get; set; }
        [JsonIgnore]
        public Carts? Cart { get; set; }
        [JsonIgnore]
        public int? Quantity { get; set; }
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime? UpdateAt { get; set; }
    }
}