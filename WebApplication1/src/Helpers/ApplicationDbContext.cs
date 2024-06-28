using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Helpers
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<SalaryGrade> SalaryGrades { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepID);
                entity.ToTable("Departments");
                entity.Property(e => e.DepName)
                      .HasMaxLength(50)
                      .IsRequired();
                entity.Property(e => e.DepLocation)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.HasIndex(e => e.DepName)
                      .IsUnique();

                entity.Property(e => e.ConcurrencyToken)
                      .IsRowVersion();
            });

            modelBuilder.Entity<SalaryGrade>(entity =>
            {
                entity.HasKey(e => e.Grade);
                entity.ToTable("SalaryGrades");
                entity.Property(e => e.MinSalary)
                      .IsRequired();
                entity.Property(e => e.MaxSalary)
                      .IsRequired();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpID);
                entity.ToTable("Employees");
                entity.Property(e => e.EmpName)
                      .HasMaxLength(100)
                      .IsRequired();
                entity.Property(e => e.JobName)
                      .HasMaxLength(50)
                      .IsRequired();
                entity.Property(e => e.HireDate)
                      .IsRequired();
                entity.Property(e => e.Salary)
                      .HasColumnType("decimal(10, 2)")
                      .IsRequired();
                entity.Property(e => e.Commission)
                      .HasColumnType("decimal(7, 2)");

                entity.HasOne(e => e.Manager)
                      .WithMany()
                      .HasForeignKey(e => e.ManagerID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Department)
                      .WithMany()
                      .HasForeignKey(e => e.DepID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.ConcurrencyToken)
                      .IsRowVersion();
            });
        }
    }
}
