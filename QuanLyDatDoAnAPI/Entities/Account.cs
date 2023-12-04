using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuanLyDatDoAnAPI.Entities
{
    public class Account
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        [JsonIgnore]
        public string? FullName { get; set; }
        [JsonIgnore]
        public string? Phone { get; set; }
        [JsonIgnore]
        public string? Email { get; set; }
        [JsonIgnore]
        public string? Address { get; set; }
        [JsonIgnore]
        public string? Avatar { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public int Status { get; set; }
        [JsonIgnore]
        public int? DecentralizationId { get; set; }
        [JsonIgnore]
        public Decentralization? Decentralization { get; set; }
        [JsonIgnore]
        public string? ResetPasswordToken { get; set; }
        [JsonIgnore]
        public DateTime? ResetPasswordTokenExpiry { get; set; }
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public IEnumerable<ProductReview>? ProductReviews { get; set; }
        [JsonIgnore]
        public IEnumerable<Order>? Orders { get; set; }
        [JsonIgnore]
        public IEnumerable<Carts>? Carts { get; set; }
    }
}