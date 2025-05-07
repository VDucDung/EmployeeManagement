public class EmployeeDto
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int? DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; }
}