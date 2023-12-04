using System.ComponentModel.DataAnnotations;

namespace QuanLyDatDoAnAPI.Entities
{
    public class Decentralization
    {
        [Key]
        public int DecentralizationId { get; set; }
        public string AuthorityName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IEnumerable<Account>? Accounts { get; set; }
    }
}