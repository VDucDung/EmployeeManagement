public class DepartmentDto
{
    public int DepartmentId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
    public int EmployeeCount { get; set; }
}