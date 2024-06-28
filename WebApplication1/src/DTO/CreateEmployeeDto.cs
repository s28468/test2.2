namespace WebApplication1.DTOs;

public class CreateEmployeeDto
{
    public string Name { get; set; }
    public string Job { get; set; }
    public int ManagerId { get; set; }
    public decimal Salary { get; set; }
    public decimal Comission { get; set; }
    public int DepId { get; set; }
}
