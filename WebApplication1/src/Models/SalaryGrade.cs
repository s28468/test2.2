using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class SalaryGrade
    {
        [Key]
        public int Grade { get; set; }

        [Required]
        public int MinSalary { get; set; }

        [Required]
        public int MaxSalary { get; set; }
    }
}