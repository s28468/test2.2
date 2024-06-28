namespace WebApplication1.DTOs;

public class DepartmentDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public List<EmployeeDetailDto> Employees { get; set; }
}