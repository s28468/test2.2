namespace WebApplication1.DTOs;

public class EmployeeDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Job { get; set; }
    public EmployeeManagerDto Manager { get; set; }
    public int Grade { get; set; }
    public decimal Salary { get; set; }
    public decimal? Commission { get; set; }
    public DateTime HireDate { get; set; }
}