using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Department
    {
        [Key]
        public int DepID { get; set; }

        [Required]
        [MaxLength(50)]
        public string DepName { get; set; }

        [Required]
        [MaxLength(50)]
        public string DepLocation { get; set; }

        [Timestamp]
        public byte[] ConcurrencyToken { get; set; }
    }
}