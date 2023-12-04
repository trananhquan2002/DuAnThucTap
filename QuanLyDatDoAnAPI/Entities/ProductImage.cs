using System.ComponentModel.DataAnnotations;

namespace QuanLyDatDoAnAPI.Entities
{
    public class ProductImage
    {
        [Key]
        public int ProductImageId { get; set; }
        public string Title { get; set; }
        public string ImageProduct { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}