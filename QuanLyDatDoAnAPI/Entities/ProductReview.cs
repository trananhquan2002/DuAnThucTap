using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyDatDoAnAPI.Entities
{
    public class ProductReview
    {
        [Key]
        public int ProductReviewId { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public Account? Account { get; set; }
        public string? ContentRated { get; set; }
        public int? PointEvaluation { get; set; }
        public string? ContentSeen { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}