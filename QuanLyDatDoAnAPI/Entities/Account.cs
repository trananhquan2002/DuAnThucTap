using System.ComponentModel.DataAnnotations;

namespace QuanLyDatDoAnAPI.Entities
{
    public class Account
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Avatar { get; set; }
        public string? Password { get; set; }
        public int? Status { get; set; }
        public int? DecentralizationId { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenExpiry { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public Decentralization? Decentralization { get; set; }
        public IEnumerable<ProductReview>? ProductReviews { get; set; }
        public IEnumerable<Carts>? Carts { get; set; }
        public IEnumerable<Order>? Orders { get; set; }
    }
}