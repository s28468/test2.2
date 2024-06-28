using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Employee
    {
        [Key]
        public int EmpID { get; set; }

        [Required]
        [MaxLength(100)]
        public string EmpName { get; set; }

        [Required]
        [MaxLength(50)]
        public string JobName { get; set; }

        public int? ManagerID { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Salary { get; set; }

        [Column(TypeName = "decimal(7, 2)")]
        public decimal? Commission { get; set; }

        [Required]
        public int DepID { get; set; }

        [ForeignKey("ManagerID")]
        public Employee Manager { get; set; }

        [ForeignKey("DepID")]
        public Department Department { get; set; }

        [Timestamp]
        public byte[] ConcurrencyToken { get; set; }
    }
}