using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuanLyDatDoAnAPI.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [JsonIgnore]
        public int? ProductTypeId { get; set; }
        [JsonIgnore]
        public ProductType? ProductType { get; set; }
        [JsonIgnore]
        public string? NameProduct { get; set; }
        [JsonIgnore]
        public double? Price { get; set; }
        public string? AvartarImageProduct { get; set; }
        [JsonIgnore]
        public string? Title { get; set; }
        [JsonIgnore]
        public int? Discount { get; set; }
        [JsonIgnore]
        public int? Status { get; set; }
        [JsonIgnore]
        public int? NumberOfViews { get; set; }
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime? UpdateAt { get; set; }
        [JsonIgnore]
        public IEnumerable<ProductReview>? ProductReviews { get; set; }
        [JsonIgnore]
        public IEnumerable<CartItem>? CartItems { get; set; }
        [JsonIgnore]
        public IEnumerable<OrderDetail>? OrderDetails { get; set; }
    }
}